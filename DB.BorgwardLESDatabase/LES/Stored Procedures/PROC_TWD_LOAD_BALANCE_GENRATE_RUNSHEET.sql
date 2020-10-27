
-- =============================================
-- Author:		<Yuan,,Shian>
-- Create date: <2014-10-30,,>
-- Description:	<零件配载优化,,>
-- =============================================
CREATE PROCEDURE [LES].[PROC_TWD_LOAD_BALANCE_GENRATE_RUNSHEET]
	@Plant			NVARCHAR(5),
	@AssemblyLine	NVARCHAR(10),
	@SupplierNum	NVARCHAR(12),
	@BoxParts		NVARCHAR(10),
	@WindowTime		DATETIME
AS
BEGIN
	DECLARE @NOW	DATETIME,
			@DAY	INT,
			@OptimalGrp NVARCHAR(32)	-- 取最有效的最优配载模板名称
			--@OptimalQty	INT

	SET @NOW = GETDATE()
	
	--@PartTable里面存放的零件应该是要以汇总的零件为准，因为有可能把多个时间窗口的零件放在一起处理，这样的话，零件就会有重复了。
	--所以这里要改，还有手动生A500成拉动单的功能也要做修改

	-- 取出上次以来没有处理过拆分的零件
	DECLARE @PartTable TABLE
	(
		InterfaceId		INT,			-- 主键
		BoxParts		NVARCHAR(10),	-- 零件类
		PackCount		INT,			-- 需求件数
		RequriedPack	INT,			-- 需求箱数
		PartNo			NVARCHAR(20),	-- 零件号
		OrderCalDate	DATETIME,		-- 计划过M100点日期
		InBoundPackageModel	NVARCHAR(30),	-- 入厂包装箱型
		InBoundPackage	INT,			-- 入厂包装数量
		AddQty			INT,			-- 需要增补的零件箱数
		AccQty			INT				-- 累计多拉箱数，需要写回A500的
	)
	-- 将零件分为两类存在@LegalParts临时表
	--		一种是IsNeed为1的,本次需要拉动的零件, 
	--		一种是IsNeed为2的,本次不需要拉动的,因为上次的拉动的零件已经足够本次使用
	DECLARE @LegalParts TABLE
	(
		BoxParts		NVARCHAR(10),	-- 零件类
		InBoundPackageModel	NVARCHAR(30),-- 入厂包装箱型
		PartNo			NVARCHAR(20),	-- 零件号
		RequriedPack	INT,			-- 需求箱数
		IsNeed			BIT				-- 是否需要再拉零件,1-需要,0-不需要
	)
	-- 去除无效的配载模板（如果同一个箱型在不同的分组里，那么后一个分组的配载模板将无效）	
	DECLARE @TempResult TABLE
	(
		GroupName		NVARCHAR(32),	-- 分组编码
		InBoundBoxType	NVARCHAR(32),	-- 入厂包装箱型
		TopQty			INT				-- 最优数量
	)

	-- 零件日消耗数量
	DECLARE @PartConsume TABLE
	(
		Plant			NVARCHAR(5),	-- 工厂编码
		PartNo			NVARCHAR(20),	-- 零件号
		ConsumeQty		INT				-- 消耗数量
	)

	-- 取出供应商窗口时间内需要拉动的零件信息
	INSERT INTO @PartTable
	SELECT INTERFACE_ID, BOX_PARTS, PACK_COUNT, REQURIED_PACK, PART_NO, ORDER_CAL_DATE, INBOUND_PACKAGE_MODEL, INBOUND_PACKAGE, 0 AS AddQty, 0 AS AccQty
	FROM [LES].[TE_TWD_A500_MATERIAL_CONSUME]
	WHERE INTERFACE_STATUS = 0
		AND PLANT = @Plant
		AND ASSEMBLY_LINE = @AssemblyLine
		AND SUPPLIER_NUM = @SupplierNum
		AND BOX_PARTS = @BoxParts
		AND ORDER_CAL_DATE <= @WindowTime

	-- 过滤掉本次需求数为 0 的零件（上次多拉的零件够本次的需求）
	INSERT INTO @LegalParts
	SELECT BoxParts, InBoundPackageModel, PartNo, SUM(RequriedPack) AS RequriedPack, CASE WHEN SUM(RequriedPack) > 0 THEN 1 ELSE 0 END AS IsNeed
	FROM @PartTable
	GROUP BY BoxParts, InBoundPackageModel, PartNo

	-- 求出有效配载优化组
	SET @OptimalGrp = 
	(
		SELECT TOP 1 GROUP_CLS--, SUM(Deviation) AS TopQtyDev
		FROM
		(
			SELECT D.*, T.GROUP_CLS, T.TOP_QTY - D.RequriedPack AS Deviation
			FROM [LES].[TT_TWD_LOAD_BALANCE_TEMPLATE] T 
			LEFT JOIN 
			(
				SELECT BoxParts,
					InBoundPackageModel,
					SUM(RequriedPack) AS RequriedPack,
					COUNT(1) OVER(PARTITION BY 0) AS BoxTypeTotal
				FROM @LegalParts
				WHERE IsNeed = 1
				GROUP BY BoxParts, InBoundPackageModel
			) D ON T.INBOUND_PACKAGE_MODEL = D.InBoundPackageModel
				AND T.BOX_PARTS = D.BoxParts
			WHERE T.PLANT = @Plant
				AND T.ASSEMBLY_LINE = @AssemblyLine
				AND T.SUPPLIER_NUM = @SupplierNum
			--ORDER BY T.GroupName
		) A
		GROUP BY GROUP_CLS
		HAVING	COUNT(1) = MAX(A.BoxTypeTotal) -- 关联出来的条数与零件箱型个数相同的且不存在需求数大于最优数的保留
				AND SUM(CASE WHEN Deviation > 0 THEN 0 ELSE Deviation END) >= 0
		ORDER BY SUM(Deviation) ASC
	)

	-- 是否有找到配载优化组
	IF @OptimalGrp IS NOT NULL
	BEGIN
		-- 求出哪些零件，同时属于多个分组（这些要去除掉），因为一个某个箱型属于A组之后，那么B组肯定将无法满足条件，因此需要去掉一组
		INSERT INTO @TempResult
		SELECT GROUP_CLS AS GroupName, INBOUND_PACKAGE_MODEL AS InBoundBoxType, TOP_QTY AS TopQty
		FROM [LES].[TT_TWD_LOAD_BALANCE_TEMPLATE]
		WHERE GROUP_CLS = @OptimalGrp

		-- 求出当日零件消耗量情况
		INSERT INTO @PartConsume
		SELECT [PLANT],[PART_NO],SUM(ISNULL([BESI3_QTY],0)) AS ConsumeQty
		FROM [LES].[TT_TWD_BESI3_REQUEST]
		WHERE PLANT=@Plant
			AND SUPPLIER_NUM=@SupplierNum
			AND DATEDIFF(DAY,@NOW,REQUEST_DATE) BETWEEN 0 AND 6
		GROUP BY [PLANT], [PART_NO]

		-- 求出需要新增箱数的零件及数量
		UPDATE @PartTable SET AddQty = R.TopQty - T.TotalRequriedPack, AccQty = R.TopQty - T.TotalRequriedPack
		FROM
		@PartTable PT,
		(
			-- 关联零件表与当天零件消耗表
			-- 然后根据包装箱型做为分区
			--	   求这个箱型内总的需求箱数（同一个箱型内的每个零件都有相同的汇总值，该值是这个箱型内所有零件需求的汇总，通OVER关键字实现）
			--	   以及生成这个箱型内的零件（按消耗量降序，消耗量最大的序号为1）的序号
			SELECT P.InBoundPackageModel,
				P.PartNo,
				-- 汇总该箱型内所有零件需求的总箱数
				SUM(P.RequriedPack) OVER(PARTITION BY P.InBoundPackageModel) AS TotalRequriedPack,
				-- 为该箱型内按消耗量从大到小排序编号，消耗最大的RankId等于1
				ROW_NUMBER()		OVER(PARTITION BY P.InBoundPackageModel ORDER BY C.ConsumeQty DESC) AS RankId
			FROM @LegalParts P
			LEFT JOIN @PartConsume C ON P.PartNo = C.PartNo
			WHERE P.IsNeed = 1	-- 为1的零件才是本次需要多拉的零件
		) T
		-- 只更新最大消耗量零件的多拉数(RankId=1)
		JOIN @TempResult R ON T.RankId = 1 AND T.InBoundPackageModel = R.InBoundBoxType
		WHERE PT.InBoundPackageModel = T.InBoundPackageModel AND PT.PartNo = T.PartNo AND PT.RequriedPack > 0
	END
	
	-- 更新 上次多拉的需求箱数 减去 本次需求箱数 得到剩余多拉数, AccQty为历史多拉箱数字段
	-- 被更新的零件是本次不需要再拉的,因为上次多拉的已经够本次使用了
	UPDATE A SET AccQty = AccQty + ABS(T.RequriedPack)
	FROM @PartTable A, @LegalParts T
	WHERE T.IsNeed = 0 AND A.InBoundPackageModel = T.InBoundPackageModel AND A.PartNo = T.PartNo AND A.RequriedPack > 0

	BEGIN TRANSACTION
	BEGIN TRY
		-- 将A500零件消耗表的数据插入到TWD零件消耗表（更新箱数）
		INSERT INTO [LES].[TI_TWD_MATERIAL_CONSUME]
			(	[PLANT_ZONE],		[WORKSHOP],			[ASSEMBLY_LINE],		[PLANT],		[LOCATION],		[REQUEST_TIME],		[INTERFACE_STATUS],
				[PROCESS_TIME],		[PART_NO],			[INDENTIFY_PART_NO],	[PART_CNAME],	[PART_ENAME],	[SUPPLIER_NUM],		[DOCK],				[BOX_PARTS],	[INTERFACE_TYPE],
				[PACK_COUNT],		[REQURIED_PACK],	[INBOUND_PACKAGE_MODEL],
				[INBOUND_PACKAGE],	[MEASURING_UNIT_NO],[EXPECTED_ARRIVAL_TIME],[RDC_DLOC],		[PICKUP_SEQ_NO],
				[SEQUENCE_NO],		[IS_ORGANIZE_SHEET],[SEND_STATUS],			[SEND_TIME],	[IS_CANCEL],	[UPDATE_DATE],		[UPDATE_USER],
				[CREATE_DATE],		[CREATE_USER],		[COMMENTS],				[INHOUSE_PACKAGE_MODEL],		[INHOUSE_PACKAGE])
		SELECT
			  A.[PLANT_ZONE],		A.[WORKSHOP],			A.[ASSEMBLY_LINE],		A.[PLANT],		A.[LOCATION],	A.[REQUEST_TIME],	A.[INTERFACE_STATUS],
			  A.[PROCESS_TIME],		A.[PART_NO],			A.[INDENTIFY_PART_NO],	A.[PART_CNAME],	A.[PART_ENAME],	A.[SUPPLIER_NUM],	A.[DOCK],		A.[BOX_PARTS],	A.[INTERFACE_TYPE],
			  -- 添加零件件数
			  A.[PACK_COUNT] + (A.[REQURIED_PACK] + T.[AddQty]) * ISNULL(A.[INBOUND_PACKAGE],0),
			  -- 添加零件箱数
			  A.[REQURIED_PACK] + T.[AddQty],
			  A.[INBOUND_PACKAGE_MODEL],
			  A.[INBOUND_PACKAGE],	A.[MEASURING_UNIT_NO],  A.[EXPECTED_ARRIVAL_TIME],A.[RDC_DLOC],	A.[PICKUP_SEQ_NO],
			  A.[SEQUENCE_NO],		A.[IS_ORGANIZE_SHEET],	A.[SEND_STATUS],		A.[SEND_TIME],	A.[IS_CANCEL],	A.[UPDATE_DATE],	A.[UPDATE_USER],
			  A.[CREATE_DATE],		A.[CREATE_USER],		A.[COMMENTS],			A.[INHOUSE_PACKAGE_MODEL],		A.[INHOUSE_PACKAGE]
		FROM @PartTable T
		JOIN [LES].[TE_TWD_A500_MATERIAL_CONSUME] A ON T.InterfaceId = A.INTERFACE_ID
		WHERE T.RequriedPack > 0	-- 负箱数的零件不写到[LES].[TI_TWD_MATERIAL_CONSUME]表
	
		-- 更新处理过的零件状态INTERFACE_STATUS为1，处理时间为现在时间
		UPDATE A SET
			INTERFACE_STATUS = 1,
			PROCESS_TIME = GETDATE()
		FROM [LES].[TE_TWD_A500_MATERIAL_CONSUME] A,
			@PartTable T
		WHERE A.INTERFACE_ID = T.InterfaceId

		-- 回写负的多拉箱数
		INSERT INTO [LES].[TE_TWD_A500_MATERIAL_CONSUME]
			([PLANT_ZONE] ,[WORKSHOP] ,[PLANT] ,[ASSEMBLY_LINE] ,[LOCATION] ,[REQUEST_TIME] ,[INTERFACE_STATUS] ,[PROCESS_TIME] ,[PART_NO] ,[INDENTIFY_PART_NO] ,[PART_CNAME] ,[PART_ENAME] ,[SUPPLIER_NUM] ,[DOCK] ,[BOX_PARTS] ,[INTERFACE_TYPE] ,[PACK_COUNT] ,[REQURIED_PACK] ,[INBOUND_PACKAGE_MODEL] ,[INBOUND_PACKAGE] ,[MEASURING_UNIT_NO] ,[EXPECTED_ARRIVAL_TIME] ,[RDC_DLOC] ,[PICKUP_SEQ_NO] ,[SEQUENCE_NO] ,[IS_ORGANIZE_SHEET] ,[SEND_STATUS] ,[SEND_TIME] ,[IS_CANCEL] ,[UPDATE_DATE] ,[UPDATE_USER] ,[CREATE_DATE] ,[CREATE_USER] ,[COMMENTS] ,[INHOUSE_PACKAGE_MODEL] ,[INHOUSE_PACKAGE] ,[A500_TIME_SN] ,[KNR] ,[ORDER_ID] ,[ORDER_CAL_DATE])
		SELECT A.[PLANT_ZONE] ,A.[WORKSHOP] ,A.[PLANT] ,A.[ASSEMBLY_LINE] ,A.[LOCATION] ,A.[REQUEST_TIME] ,A.[INTERFACE_STATUS] ,A.[PROCESS_TIME] ,A.[PART_NO] ,A.[INDENTIFY_PART_NO] ,A.[PART_CNAME] ,A.[PART_ENAME] ,A.[SUPPLIER_NUM] ,A.[DOCK] ,A.[BOX_PARTS] ,A.[INTERFACE_TYPE] ,
			0 - T.AccQty * A.INBOUND_PACKAGE,
			0 - T.AccQty,
			A.[INBOUND_PACKAGE_MODEL] ,A.[INBOUND_PACKAGE] ,A.[MEASURING_UNIT_NO] ,A.[EXPECTED_ARRIVAL_TIME] ,A.[RDC_DLOC] ,A.[PICKUP_SEQ_NO] ,A.[SEQUENCE_NO] ,A.[IS_ORGANIZE_SHEET] ,A.[SEND_STATUS] ,A.[SEND_TIME] ,A.[IS_CANCEL] ,A.[UPDATE_DATE] ,A.[UPDATE_USER] ,A.[CREATE_DATE] ,A.[CREATE_USER] ,A.[COMMENTS] ,A.[INHOUSE_PACKAGE_MODEL] ,A.[INHOUSE_PACKAGE] ,A.[A500_TIME_SN] ,A.[KNR] ,A.[ORDER_ID] ,A.[ORDER_CAL_DATE]
		FROM @PartTable T
		JOIN [LES].[TE_TWD_A500_MATERIAL_CONSUME] A ON T.InterfaceId = A.INTERFACE_ID
		WHERE T.AccQty > 0

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] (TIME_STAMP, [APPLICATION], [METHOD], CLASS,  EXCEPTION_MESSAGE, ERROR_CODE)
		SELECT GETDATE(),'TWD','PROC_TWD_LOAD_BALANCE_GENRATE_RUNSHEET','PROCEDURE',ERROR_MESSAGE(),ERROR_LINE()

	END CATCH
END