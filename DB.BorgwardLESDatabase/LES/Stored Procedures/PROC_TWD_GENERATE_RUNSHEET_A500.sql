
-- =============================================
-- Author:		袁世安
-- Create date: 2014-11-25
-- Description:	TWD 生成拉动单
-- =============================================
CREATE PROC [LES].[PROC_TWD_GENERATE_RUNSHEET_A500]
AS
BEGIN
    DECLARE @PLANT NVARCHAR(5),
			@ASSEMBLYLINE NVARCHAR(10),
			@SUPPLIERNUM NVARCHAR(12),
			@SENDTIME DATETIME,
			@WINDOWTIME DATETIME,
			@BOXPARTS NVARCHAR(10),
			@DOCKNAME NVARCHAR(10),
			@BOXSTATE INT,
			@PLANT_ZONE NVARCHAR(10),
			@SUGGESTTIME DATETIME,
			@WMSSTATE INT,
			@NOW DATETIME,
			@ISORGANIZESHEET INT,
			@SEND_TIME_SN INT,
			@CACULATE_TYPE INT,
			
			@RunSheetType INT,
			@RunsheetNo NVARCHAR(30),
			@MaxWindowTime DATETIME,

			@ResultCode	INT,
			@ResultMsg NVARCHAR(1024),

			-- 取最有效的最优配载模板名称
			@OptimalGrp NVARCHAR(32)

	-- 取出上次以来没有处理过拆分的零件
	DECLARE @PartTable TABLE
	(
		PART_NO				NVARCHAR(20),	-- 零件号
		INDENTIFY_PART_NO	NVARCHAR(20),	-- 零件号
		PART_CNAME			NVARCHAR(100),
		PART_ENAME			NVARCHAR(100),
		INBOUND_PACKAGE		INT,			-- 标准包装数
		MEASURING_UNIT_NO	NVARCHAR(1),
		INBOUND_PACKAGE_MODEL NVARCHAR(30),	-- 入厂包装箱型
		SUPPLIER_NUM		NVARCHAR(12),	-- 供应商
		BOX_PARTS			NVARCHAR(10),	-- 零件类
		PACK_COUNT			INT,			-- 零件需求件数
		REQUIRED_PACK		INT,			-- 需求箱数
		AddQty				INT,			-- 需要增补的零件箱数
		HistoryAddQty		INT				-- 累计多拉箱数，需要写回A500的
	)
	-- 将零件分为两类存在@LegalParts临时表
	--		一种是IsNeed为1的,本次需要拉动的零件, 
	--		一种是IsNeed为2的,本次不需要拉动的,因为上次的拉动的零件已经足够本次使用
	DECLARE @LegalParts TABLE
	(
		BOX_PARTS		NVARCHAR(10),	-- 零件类
		INBOUND_PACKAGE_MODEL	NVARCHAR(30),-- 入厂包装箱型
		PART_NO			NVARCHAR(20),	-- 零件号
		REQUIRED_PACK	INT,			-- 需求箱数
		IsNeed			BIT				-- 是否需要再拉零件,1-需要,0-不需要
	)
	-- 零件日消耗数量
	DECLARE @PartConsume TABLE
	(
		PLANT			NVARCHAR(5),	-- 工厂编码
		SUPPLIER_NUM	NVARCHAR(12),	-- 供应商
		PART_NO			NVARCHAR(20),	-- 零件号
		CONSUMEQTY		INT				-- 消耗数量
	)
	
	-- 求出最近一周的零件消耗量情况
	INSERT INTO @PartConsume
	SELECT [PLANT],[SUPPLIER_NUM], [PART_NO],SUM(ISNULL([BESI3_QTY],0)) AS ConsumeQty
	FROM [LES].[TT_TWD_BESI3_REQUEST]
	WHERE DATEDIFF(DAY,@NOW,REQUEST_DATE) BETWEEN 0 AND 6
	GROUP BY [PLANT], [SUPPLIER_NUM], [PART_NO]

	-- 返回算出最大的窗口时间
	SET @MaxWindowTime = ISNULL(
	(	--找大于最大需求时间的最小窗口时间
		SELECT MIN(SEND_TIME)
		FROM [LES].[TT_TWD_SUPPLIER_SENDTIME] WITH(NOLOCK)
		WHERE SEND_TIME >= 
		(
			SELECT MAX(REQUEST_TIME) FROM LES.TI_TWD_A500_MATERIAL_CONSUME
			WHERE IS_ORGANIZE_SHEET = 2
    			AND [INTERFACE_TYPE] <> 3			-- 3 为手工拉动，不用处理
				AND (INHOUSE_PACKAGE_MODEL IS NULL OR LEN(INHOUSE_PACKAGE_MODEL) < 1) --ADD BY CAODAOWEI FOR 排除组单零件类
		)
		AND (SEND_TIME_STATUS IS NULL OR SEND_TIME_STATUS = 0)
	), 
	(	--如果找不到大于最大需求时间的最小窗口时间,则取最大的可用窗口时间
		SELECT MAX(SEND_TIME)
		FROM [LES].[TT_TWD_SUPPLIER_SENDTIME] WITH(NOLOCK)
		WHERE (SEND_TIME_STATUS IS NULL OR SEND_TIME_STATUS = 0)
	) )
	-- 如果找不到可用的窗口时间，则结束组单
	IF @MaxWindowTime IS NULL
	BEGIN
		SET @ResultCode = 1
		SET @ResultMsg = '没有可用的窗口时间，不进行生成A500拉动单操作'
	END
	BEGIN
		-- 为最大的窗口时间增加10毫秒，以便于SendTime可以小于他
		SET @MaxWindowTime = DATEADD(MILLISECOND,10,@MaxWindowTime)
	
		--找到所有时间小于当前时间未处理的窗口时间、按照供应商分组
		DECLARE SENDTIME_CURSOR CURSOR FAST_FORWARD FOR
		SELECT SUPPLIER_SEND_TIME_SN, PLANT, ASSEMBLY_LINE, SUPPLIER_NUM, BOX_PARTS, SEND_TIME, WINDOW_TIME
		FROM LES.TT_TWD_SUPPLIER_SENDTIME WITH(NOLOCK)
		WHERE SEND_TIME <= @MaxWindowTime AND (SEND_TIME_STATUS IS NULL OR SEND_TIME_STATUS = 0 )
		--GROUP BY PLANT,ASSEMBLY_LINE,SUPPLIER_NUM,BOX_PARTS -- 不能汇总时间窗口，需求不同
		ORDER BY SEND_TIME ASC	-- 时间窗口从近到远，从早到晚

		OPEN SENDTIME_CURSOR

		BEGIN TRY	
		BEGIN TRANSACTION
			-- 清空临时表变量数据
			DELETE FROM @PartTable
			DELETE FROM @LegalParts
			DELETE FROM @PartConsume
			
			-- 打开游标，取得第一个数据
			FETCH NEXT FROM SENDTIME_CURSOR INTO @SEND_TIME_SN, @PLANT , @ASSEMBLYLINE , @SUPPLIERNUM ,@BOXPARTS, @SENDTIME, @WINDOWTIME
			WHILE( @@FETCH_STATUS = 0 )
			BEGIN	
    			DECLARE @BOXPARTSNAME NVARCHAR(100),
    					@DOCK NVARCHAR(10),
    					@TRANSSUPPLIERNUM NVARCHAR(20),
    					@WAREHOUSE NVARCHAR(20),
    					@TRANSPORTTIME INT,
						@ConsumeCount INT,
    					@RunsheetId INT
				
    			--找到供应商对应的零件类
    			--DECLARE BOXPARTS_CURSOR CURSOR FAST_FORWARD FOR
    			SELECT @BOXPARTSNAME = BOX_PARTS_NAME,
					@DOCK = DOCK,
					@TRANSSUPPLIERNUM = TRANS_SUPPLIER_NUM,
					@WAREHOUSE = WAREHOUSE,
					@TRANSPORTTIME = [TRANSPORT_TIME],
					@BOXSTATE = [BOX_PARTS_STATE],
					@ISORGANIZESHEET = ISNULL([IS_ORGANIZE_SHEET],0),
					@CACULATE_TYPE = CACULATE_TYPE
				FROM LES.TM_TWD_BOX_PARTS WITH(NOLOCK)
				WHERE PLANT = @PLANT
					AND ASSEMBLY_LINE = @ASSEMBLYLINE
					AND SUPPLIER_NUM = @SUPPLIERNUM
					AND BOX_PARTS = @BOXPARTS
					AND CACULATE_TYPE = 5		-- 5 代表是A500的零件类
					AND BOX_PARTS_STATE <> 3	-- 3 MEAN THAT RACK IS INVALID

				-- 该零件类不存在,或者该零件类不是A500的零件类
				IF ISNULL(@CACULATE_TYPE,0) <> 5
				BEGIN
					-- 读取下一个零件类处理
					FETCH NEXT FROM SENDTIME_CURSOR INTO @SEND_TIME_SN, @PLANT, @ASSEMBLYLINE, @SUPPLIERNUM, @BOXPARTS, @SENDTIME, @WINDOWTIME

					-- 结束本次生成操作
					CONTINUE
				END

				-- 清空该值,以免下一次未找到零件类时, @CACULATE_TYPE还保持原值
				SET @CACULATE_TYPE = NULL

				-- 查找DOCK名称
				SELECT @DOCKNAME = [DOCK_NAME]
				FROM [LES].[TM_BAS_DOCK] WITH(NOLOCK)
				WHERE PLANT = @PLANT
					AND ASSEMBLY_LINE = @ASSEMBLYLINE
					AND [DOCK] = @DOCK
				
				-- 关闭,不统计受影响的行数功能
				SET NOCOUNT OFF
		
				-- 将需要的生成拉动单的零件，提取到临时表
				INSERT INTO @PartTable(PART_NO,INDENTIFY_PART_NO,PART_CNAME,PART_ENAME,INBOUND_PACKAGE,MEASURING_UNIT_NO,INBOUND_PACKAGE_MODEL,SUPPLIER_NUM,BOX_PARTS,PACK_COUNT,REQUIRED_PACK)
				SELECT PART_NO,INDENTIFY_PART_NO,PART_CNAME,PART_ENAME,INBOUND_PACKAGE,MEASURING_UNIT_NO,INBOUND_PACKAGE_MODEL,SUPPLIER_NUM,BOX_PARTS,SUM(PACK_COUNT) AS TotalCount,SUM(PACK_COUNT)/INBOUND_PACKAGE AS REQUIRED_PACK
				FROM LES.TI_TWD_A500_MATERIAL_CONSUME
    			WHERE [IS_ORGANIZE_SHEET] = 2 
    				AND ([SEND_STATUS] = 0 OR [SEND_STATUS] IS NULL) 
    				AND [BOX_PARTS]= @BOXPARTS
    				AND [PLANT] = @PLANT
    				AND [ASSEMBLY_LINE] = @ASSEMBLYLINE
    				AND [INTERFACE_TYPE] <> 3			-- 3 为手工拉动，不用处理
					AND [REQUEST_TIME] <= @WINDOWTIME	-- 把所有需求时间小于当前窗口时间的统计出来，生成拉动
					AND (INHOUSE_PACKAGE_MODEL IS NULL OR LEN(INHOUSE_PACKAGE_MODEL) < 1) --ADD BY CAODAOWEI FOR 排除组单零件类
				GROUP BY PART_NO,
					INDENTIFY_PART_NO,
					PART_CNAME,
					PART_ENAME,
					INBOUND_PACKAGE,
					MEASURING_UNIT_NO,
					INBOUND_PACKAGE_MODEL,
					SUPPLIER_NUM,
					BOX_PARTS --相同零件多个需求合并到一个明细中
				-- 获取受影响的行数
				SET @ConsumeCount = @@ROWCOUNT

				-- 开启不统计受影响的行数
				SET NOCOUNT ON

				--PRINT '@CONSUMECOUNT:' + CONVERT(NVARCHAR(10), @CONSUMECOUNT)
				SET @WmsState=@BoxState
				IF(@ConsumeCount>0)
				BEGIN
					SET @RunSheetType = 6	-- A500拉动单（需要写脚本更新）
				END
				ELSE
				BEGIN
					-- 读取下一个零件类处理
					FETCH NEXT FROM SENDTIME_CURSOR INTO @SEND_TIME_SN, @PLANT, @ASSEMBLYLINE, @SUPPLIERNUM, @BOXPARTS, @SENDTIME, @WINDOWTIME

					-- 如果没有需要拉动的零件 结束本次生成操作
					CONTINUE
				END
			
				SELECT @Plant_Zone=[PLANT_ZONE]
				FROM [LES].[TM_BAS_ASSEMBLY_LINE] WITH(NOLOCK)
				WHERE [ASSEMBLY_LINE]=@AssemblyLine

				-- 将已经提取的零件数据，更新为已生成拉动单的状态。
				UPDATE LES.TI_TWD_A500_MATERIAL_CONSUME 
				SET [IS_ORGANIZE_SHEET] = 1,
					[PROCESS_TIME] = GETDATE()
				WHERE [IS_ORGANIZE_SHEET] =2
					AND [BOX_PARTS]= @boxParts
					AND [PLANT] = @plant
					AND [ASSEMBLY_LINE] = @assemblyLine
					AND [INTERFACE_TYPE] <> 3 --3为手工拉动，不用处理
					AND [REQUEST_TIME] <= @WINDOWTIME
					AND (INHOUSE_PACKAGE_MODEL IS NULL OR LEN(INHOUSE_PACKAGE_MODEL) < 1) --add by caodaowei for 排除组单零件类
				
				-- *************************
				-- * BEGIN 开始配载优化操作 *
				-- *************************

				-- 过滤掉本次需求数为 0 的零件（上次多拉的零件够本次的需求）
				INSERT INTO @LegalParts
				SELECT BOX_PARTS, INBOUND_PACKAGE_MODEL, PART_NO, SUM(REQUIRED_PACK) AS RequriedPack, CASE WHEN SUM(REQUIRED_PACK) > 0 THEN 1 ELSE 0 END AS IsNeed
				FROM @PartTable
				GROUP BY BOX_PARTS, INBOUND_PACKAGE_MODEL, PART_NO
				
				-- 求出有效配载优化组
				SET @OptimalGrp = 
				(
					SELECT TOP 1 GROUP_CLS--, SUM(Deviation) AS TopQtyDev
					FROM
					(
						SELECT D.*, T.GROUP_CLS, T.TOP_QTY - D.REQUIRED_PACK AS RequiredTotal
						FROM [LES].[TT_TWD_LOAD_BALANCE_TEMPLATE] T 
						LEFT JOIN 
						(
							SELECT BOX_PARTS,
								INBOUND_PACKAGE_MODEL,
								SUM(REQUIRED_PACK) AS REQUIRED_PACK,
								-- 统计出，总共有多少条BOX_PARTS, INBOUND_PACKAGE_MODEL的数据，
								-- 但因为BOX_PARTS是相同的，因此，可以认为是统计出，有多少个箱型的数据
								COUNT(1) OVER(PARTITION BY 0) AS BoxTypeTotal
							FROM @LegalParts
							WHERE IsNeed = 1
							GROUP BY BOX_PARTS, INBOUND_PACKAGE_MODEL
						) D ON T.INBOUND_PACKAGE_MODEL = D.INBOUND_PACKAGE_MODEL
							AND T.BOX_PARTS = D.BOX_PARTS
						WHERE T.PLANT = @Plant
							AND T.ASSEMBLY_LINE = @AssemblyLine
							AND T.SUPPLIER_NUM = @SupplierNum
						--ORDER BY T.GroupName
					) A
					GROUP BY GROUP_CLS
					HAVING	COUNT(1) = MAX(A.BoxTypeTotal)	-- 关联出来的条数与零件箱型个数相同的且不存在需求数大于最优数的保留
							AND SUM(CASE WHEN RequiredTotal > 0 THEN 0 ELSE RequiredTotal END) >= 0	-- 要求，最优数大于需求总箱数
					ORDER BY SUM(RequiredTotal) ASC			-- 按箱数相差数从小到大排序，这样TOP 1 就可以得到相差数最小的匹配组
				)

				-- 找到配载优化组，则进行优化操作
				IF @OptimalGrp IS NOT NULL
				BEGIN
					-- 减去上次多接箱数后, 需求数仍为正数的零件去关联最优配载模板及消耗表,求出本次需要多拉的零件及需要多拉的箱数
					UPDATE @PartTable SET AddQty = R.TOP_QTY - T.REQUIRED_PACK_TOTAL, HistoryAddQty = R.TOP_QTY - T.REQUIRED_PACK_TOTAL
					FROM
					@PartTable PT,
					(
						-- 关联零件表与当天零件消耗表
						-- 然后根据包装箱型做为分区
						--	   求这个箱型内总的需求箱数（同一个箱型内的每个零件都有相同的汇总值，该值是这个箱型内所有零件需求的汇总，通OVER关键字实现）
						--	   以及生成这个箱型内的零件（按消耗量降序，消耗量最大的序号为1）的序号
						SELECT P.INBOUND_PACKAGE_MODEL,
							P.PART_NO,
							-- 汇总该箱型内所有零件需求的总箱数
							SUM(P.REQUIRED_PACK)	OVER(PARTITION BY P.INBOUND_PACKAGE_MODEL) AS REQUIRED_PACK_TOTAL,
							-- 为该箱型内按消耗量从大到小排序编号，消耗最大的RankId等于1
							ROW_NUMBER()			OVER(PARTITION BY P.INBOUND_PACKAGE_MODEL ORDER BY C.CONSUMEQTY DESC) AS RankId
						FROM @LegalParts P
						LEFT JOIN @PartConsume C ON	C.PLANT=@PLANT AND C.SUPPLIER_NUM=@SUPPLIERNUM AND P.PART_NO = C.PART_NO
						WHERE P.IsNeed = 1	-- 为1的零件才是本次需要多拉的零件
					) T
					-- 只更新最大消耗量零件的多拉数(RankId=1)
					JOIN [LES].[TT_TWD_LOAD_BALANCE_TEMPLATE] R ON T.RankId = 1
						AND R.GROUP_CLS = @OptimalGrp
						AND T.INBOUND_PACKAGE_MODEL = R.INBOUND_PACKAGE_MODEL
					WHERE PT.INBOUND_PACKAGE_MODEL = T.INBOUND_PACKAGE_MODEL AND PT.PART_NO = T.PART_NO AND PT.REQUIRED_PACK > 0
				END

				-- 上次多拉的箱数仍然够本次需求时, 多拉的计数器,应该更新为 上次多拉数 减去 本次需求箱数(实际上HistoryAddQty为0)
				-- T.REQUIRED_PACK = @LegalParts.REQUIRED_PACK 为上次多拉零件和本次需求零件汇总后的总数且为负数
				UPDATE A SET HistoryAddQty = HistoryAddQty + ABS(T.REQUIRED_PACK)
				FROM @PartTable A, @LegalParts T
				WHERE T.IsNeed = 0	-- 为汇总后结果为负数的记录才更新(即,上次多拉的数足够本次使用)
					AND A.INBOUND_PACKAGE_MODEL = T.INBOUND_PACKAGE_MODEL
					AND A.PART_NO = T.PART_NO	-- 这两个条件确定两表关联
					-- 1. A.REQUIRED_PACK = 0	则说明,本次需求为0,本次多拉数等于上次多拉数,不需要更新HistoryAddQty
					-- 2. A.REQUIRED_PACK = -N	则说明,这条记录是多拉的回写记录,不用更新HistoryAddQty
					-- 3. A.REQUIRED_PACK = +N	则说明,这条记录是本次需求记录数,需要更新HistoryAddQty,用于将来产生回写多拉箱数的记录
					AND A.REQUIRED_PACK > 0
				
				-- 回写负的多拉箱数
				INSERT INTO [LES].[TI_TWD_A500_MATERIAL_CONSUME]
					([PLANT_ZONE] ,[WORKSHOP] ,[PLANT] ,[ASSEMBLY_LINE] ,[LOCATION] ,[REQUEST_TIME] ,[INTERFACE_STATUS] ,[PROCESS_TIME] ,[PART_NO] ,[INDENTIFY_PART_NO] ,[PART_CNAME] ,[PART_ENAME] ,[SUPPLIER_NUM] ,[DOCK] ,[BOX_PARTS] ,[INTERFACE_TYPE] ,[PACK_COUNT] ,[REQURIED_PACK] ,[INBOUND_PACKAGE_MODEL] ,[INBOUND_PACKAGE] ,[MEASURING_UNIT_NO] ,[EXPECTED_ARRIVAL_TIME] ,[RDC_DLOC] ,[PICKUP_SEQ_NO] ,[SEQUENCE_NO] ,[IS_ORGANIZE_SHEET] ,[SEND_STATUS] ,[SEND_TIME] ,[IS_CANCEL] ,[UPDATE_DATE] ,[UPDATE_USER] ,[CREATE_DATE] ,[CREATE_USER] ,[COMMENTS] ,[INHOUSE_PACKAGE_MODEL] ,[INHOUSE_PACKAGE])
				SELECT @PLANT_ZONE, NULL AS [WORKSHOP], 
					@PLANT,						-- Is Next Process Conditions
					@ASSEMBLYLINE,				-- Is Next Process Conditions
					NULL AS [LOCATION],
					'2000-01-01' AS [REQUEST_TIME],	-- Is Next Process Conditions
					0 AS [INTERFACE_STATUS],	-- Is Next Process Conditions
					NULL AS [PROCESS_TIME], A.[PART_NO], A.[INDENTIFY_PART_NO], A.[PART_CNAME], A.[PART_ENAME], A.[SUPPLIER_NUM], @DOCK, 
					A.[BOX_PARTS],				-- Is Next Process Conditions
					0 AS [INTERFACE_TYPE],
					0 - A.HistoryAddQty * A.INBOUND_PACKAGE,
					0 - A.HistoryAddQty,
					A.[INBOUND_PACKAGE_MODEL] ,A.[INBOUND_PACKAGE] ,A.[MEASURING_UNIT_NO] ,NULL AS [EXPECTED_ARRIVAL_TIME] ,NULL AS [RDC_DLOC] ,NULL AS [PICKUP_SEQ_NO] ,NULL AS [SEQUENCE_NO] ,
					2 AS [IS_ORGANIZE_SHEET],	-- Is Next Process Conditions
					NULL AS [SEND_STATUS],		-- Is Next Process Conditions
					NULL AS [SEND_TIME] ,NULL AS [IS_CANCEL] ,NULL AS [UPDATE_DATE] ,NULL AS [UPDATE_USER] ,NULL AS [CREATE_DATE] ,NULL AS [CREATE_USER] ,NULL AS [COMMENTS] ,
					NULL AS [INHOUSE_PACKAGE_MODEL],-- Is Next Process Conditions
					NULL AS [INHOUSE_PACKAGE]
				FROM @PartTable A
				WHERE A.HistoryAddQty > 0
				
				-- *************************
				-- *  END 结束配载优化操作  *
				-- *************************

				--获取拉动单号
				EXEC LES.PROC_TWD_GET_RUNSHEET_NO
					@PLANT,
					@ASSEMBLYLINE,
					@SUPPLIERNUM,
					@RUNSHEETNO OUTPUT

				-- 计算送货时间
				SELECT @SuggestTime = DATEADD(MI,-ISNULL(@TransportTime,0),@WindowTime)
				-- 插入拉动单主表数据
				INSERT INTO [LES].[TT_TWD_RUNSHEET]
    			(
					[TWD_RUNSHEET_NO]
    				,[PLANT]
    				,[ASSEMBLY_LINE]
    				,[WORKSHOP]
    				,[PLANT_ZONE]
    				,[PUBLISH_TIME]
    				,[RUNSHEET_TYPE]
    				,[SUPPLIER_NUM]
    				,[SUPPLIER_SN]
    				,[DOCK]
    				,[DELIVERY_LOCATION]
    				,[BOX_PARTS]
    				,[PART_TYPE]
    				,[UNLOADING_TIME]
    				,[EXPECTED_ARRIVAL_TIME]
    				,[SUGGEST_DELIVERY_TIME]
    				,[ACTUAL_ARRIVAL_TIME]
    				,[VERIFY_TIME]
    				,[REJECT_REASON]
    				,[TRANS_SUPPLIER_NUM]
    				,[FEEDBACK]
    				,[SHEET_STATUS]
    				,[SEND_TIME]
    				,[SEND_STATUS]
    				,[OPERATON_USER]
    				,[CHECK_USER]
    				,[RETRY_TIMES]
    				,[SUPPLY_TIME]
    				,[SUPPLY_STATUS]
    				,[FAX_TIME]
    				,[FAX_STATUS]
    				,[SAP_FLAG]
    				,[SAP_FLAG2]
    				,[RECKONING_NO]
    				,[WMS_SEND_TIME]
    				,[WMS_SEND_STATUS]
    				,[COMMENTS]
    				,[UPDATE_DATE]
    				,[UPDATE_USER]
    				,[CREATE_DATE]
    				,[CREATE_USER]
    				,[GENERATE_TYPE])
    			VALUES
				(
    				@RunsheetNo----<TWD_RUNSHEET_NO, varchar(18),>
    				,@Plant --<PLANT, nvarchar(5),>
    				,@AssemblyLine --<ASSEMBLY_LINE, nvarchar(10),>
    				,NULL --<WORKSHOP, nvarchar(4),>
    				,@Plant_zone --<PLANT_ZONE, nvarchar(10),>
    				,GETDATE() --<PUBLISH_TIME, datetime,>
    				,@RunSheetType  --<RUNSHEET_TYPE, int,>
    				,@SupplierNum  --<SUPPLIER_NUM, nvarchar(12),>
    				,0 --尚不明确含义<SUPPLIER_SN, int,>
    				,@DockName--<DOCK, nvarchar(10),>
    				,@WAREHOUSE --<DELIVERY_LOCATION, nvarchar(50),>
    				,@BoxParts --<BOX_PARTS, nvarchar(10),>
    				,0 --尚不明确含义<PART_TYPE, int,>
    				,NUll --<UNLOADING_TIME, int,>
    				,@WindowTime --<EXPECTED_ARRIVAL_TIME, datetime,>
    				,@SuggestTime --需求尚未确定<SUGGEST_DELIVERY_TIME, datetime,>
    				,NULL --<ACTUAL_ARRIVAL_TIME, datetime,>
    				,NULL --<VERIFY_TIME, datetime,>
    				,NULL --<REJECT_REASON, nvarchar(200),>
    				,@TransSupplierNum --<TRANS_SUPPLIER_NUM, nvarchar(8),>
    				,NULL --<FEEDBACK, nvarchar(100),>
    				,2 ---<SHEET_STATUS, int,>
    				,GETDATE() --<SEND_TIME, datetime,>
    				,0--@BoxState --<SEND_STATUS, int,>	-- 0为默认的初始化状态， 当修改更新完箱数，点击发布数据时，这个状态要修改成为1，变成可以发送给供应商。
    				,NULL --<OPERATON_USER, nvarchar(10),>
    				,NULL --<CHECK_USER, nvarchar(10),>
    				,0 --<RETRY_TIMES, int,>
    				,NULL --<SUPPLY_TIME, datetime,>
    				,0 --<SUPPLY_STATUS, int,>
    				,NULL --<FAX_TIME, datetime,>
    				,0 --<FAX_STATUS, int,>
    				,0 --<SAP_FLAG, int,>
    				,0 ---<SAP_FLAG2, int,>
    				,NULL --<RECKONING_NO, nvarchar(30),>
    				,NULL --<WMS_SEND_TIME, datetime,>
    				,0--@WmsState --<WMS_SEND_STATUS, int,>	-- 0为默认的初始化状态， 当修改更新完箱数，点击发布数据时，这个状态要修改成为1，变成可以发送给供应商。
    				,'' --<COMMENTS, nvarchar(200),>
    				,NULL --<UPDATE_DATE, datetime,>
    				,NULL --<UPDATE_USER, nvarchar(50),>
    				,getdate() --<CREATE_DATE, datetime,>
    				,'TWD A500 RUNSHEET ENGINE'--<CREATE_USER, nvarchar(50),>
    				,1	--@Isorganizesheet	-- 延迟生成送货单
				)

				-- 获取拉动单号,自增标识
    			SELECT @RunsheetId = SCOPE_IDENTITY()
				IF(  @ConsumeCount > 0 )
				BEGIN
					--PRINT '插入明细'
					INSERT INTO [LES].[TT_TWD_RUNSHEET_DETAIL]
					(
						[TWD_RUNSHEET_SN]
						,[PLANT]
						,[ASSEMBLY_LINE]
						,[SUPPLIER_NUM]
						,[PART_NO]
						,[IDENTIFY_PART_NO]
						,[PART_CNAME]
						,[PART_ENAME]
						,[DOCK]
						,[BOX_PARTS]
						,[SEQUENCE_NO]
						,[PICKUP_SEQ_NO]
						,[RDC_DLOC]
						,[INBOUND_PACKAGE]
						,[MEASURING_UNIT_NO]
						,[INBOUND_PACKAGE_MODEL]
						,[PACK_COUNT]
						,[REQUIRED_INBOUND_PACKAGE]
						,[REQUIRED_INBOUND_PACKAGE_QTY]
						,[ACTUAL_INBOUND_PACKAGE]
						,[ACTUAL_INBOUND_PACKAGE_QTY]
						,[BARCODE_DATA]
						,[COMMENTS]
						,SUPPLIER_NUM_SHEET
						,BOX_PARTS_SHEET
					)
					SELECT
						@RunsheetId--(<TWD_RUNSHEET_SN, int,>
						,@Plant --<PLANT, nvarchar(5),>
						,@AssemblyLine --<ASSEMBLY_LINE, nvarchar(10),>
						,@SupplierNum --<SUPPLIER_NUM, nvarchar(12),>
						,PART_NO --, nvarchar(20),>
						,INDENTIFY_PART_NO --, nvarchar(20),>
						,PART_CNAME --, nvarchar(100),>
						,PART_ENAME --, nvarchar(100),>
						,ISNULL(@Dock,'')--<DOCK, nvarchar(10),>
						,@boxParts --<BOX_PARTS, nvarchar(10),>
						,0 --<SEQUENCE_NO, int,>
						,0 --<PICKUP_SEQ_NO, int,>
						,NULL --<RDC_DLOC, varchar(20),>
						,INBOUND_PACKAGE--, int,>
						,MEASURING_UNIT_NO --, nvarchar(1),>
						,INBOUND_PACKAGE_MODEL--, nvarchar(30),>
						,([REQUIRED_PACK] + [AddQty]) * ISNULL([INBOUND_PACKAGE],0)--, int,>
						,[REQUIRED_PACK] + [AddQty] --<REQUIRED_INBOUND_PACKAGE, int,>
						,([REQUIRED_PACK] + [AddQty]) * ISNULL([INBOUND_PACKAGE],0) --<REQUIRED_INBOUND_PACKAGE_QTY, int,>
						,0--<ACTUAL_INBOUND_PACKAGE, int,>
						,0--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
						,NULL --<BARCODE_DATA, nvarchar(50),>
						,NULL --<COMMENTS, nvarchar(200),>)
						,SUPPLIER_NUM
						,BOX_PARTS
					FROM @PartTable
					WHERE REQUIRED_PACK > 0
			
					--PRINT '插入到供货列表'
					INSERT INTO [LES].[TT_TWD_RUNSHEET_SEND_SUPPLY]
					(
						[TWD_RUNSHEET_SN]
						,[TWD_RUNSHEET_NO]
						,[PUBLISH_TIME]
						,[SUPPLIER_NUM]
						,[BOX_PARTS]
						,[SEND_TIME]
						,[SEND_STATUS]
						,[COMMENTS]
						,[CREATE_DATE]
						,[CREATE_USER]
					)
					SELECT [TWD_RUNSHEET_SN]
						,A.[TWD_RUNSHEET_NO]
						,A.[PUBLISH_TIME]
						,B.[SUPPLIER_NUM]
						,A.[BOX_PARTS]
						,NULL
						,1
						,NULL
						,GETDATE()
						,'TWD AUTO'
					FROM [LES].[TT_TWD_RUNSHEET] A
					INNER JOIN [LES].[TM_TWD_SUPPLY_BOX_PARTS] B ON A.[PLANT]=b.[PLANT]
						AND A.[ASSEMBLY_LINE] = B.[ASSEMBLY_LINE]
						AND A.[BOX_PARTS] = B.[BOX_PARTS]
					WHERE A.[TWD_RUNSHEET_SN] = @RunsheetId
		
					UPDATE A
					SET A.[ORDER_NO]=B.[AGREEMENT_NO],
						A.[ITEM_NO]=B.[PROJECT]
					FROM [LES].[TT_TWD_RUNSHEET_DETAIL] A,
						 [LES].[TI_BAS_SUPPLIER_SOURCE_LIST] B
					WHERE A.[PLANT]=B.[PLANT]
						AND A.[SUPPLIER_NUM] = B.[SUPPLIER_NUM]
						AND A.[PART_NO] = B.[PART_NO]
						AND A.[TWD_RUNSHEET_SN] = @RunsheetId
						--modify by【运维】xhm 2014/08/04【CMDB编号：CR-LES-20140807】start
						--零件货源匹配使用期望到达时间
						--modify by【运维】hx 2014/08/04【CMDB编号：CR-LES-20140402】start
						--修改起始有效期和结束有效期只比较日期
						--AND	CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),23)) BETWEEN START_EFFECTIVE_DATE AND END_EFFECTIVE_DATE
						AND	CONVERT(DATETIME,CONVERT(VARCHAR(10),@WINDOWTIME,23)) BETWEEN START_EFFECTIVE_DATE AND END_EFFECTIVE_DATE
						--modify by【运维】hx 2014/04/01 end
						--modify by【运维】xhm 2014/08/04 end
				END
				ELSE IF 1=2	-- 据说不生成空白单，因此1=2，具体需要调整
				BEGIN
					--PRINT 'NULL'
					INSERT INTO [LES].[TT_TWD_RUNSHEET_DETAIL]
					(
						[TWD_RUNSHEET_SN]
					   ,[PLANT]
					   ,[ASSEMBLY_LINE]
					   ,[SUPPLIER_NUM]
					   ,[PART_NO]
					   ,[IDENTIFY_PART_NO]
					   ,[PART_CNAME]
					   ,[PART_ENAME]
					   ,[DOCK]
					   ,[BOX_PARTS]
					   ,[SEQUENCE_NO]
					   ,[PICKUP_SEQ_NO]
					   ,[RDC_DLOC]
					   ,[INBOUND_PACKAGE]
					   ,[MEASURING_UNIT_NO]
					   ,[INBOUND_PACKAGE_MODEL]
					   ,[PACK_COUNT]
					   ,[REQUIRED_INBOUND_PACKAGE]
					   ,[REQUIRED_INBOUND_PACKAGE_QTY]
					   ,[ACTUAL_INBOUND_PACKAGE]
					   ,[ACTUAL_INBOUND_PACKAGE_QTY]
					   ,[BARCODE_DATA]
					   ,[COMMENTS])
					VALUES
					(
						@runsheetId--(<TWD_RUNSHEET_SN, int,>
					   ,@plant --<PLANT, nvarchar(5),>
					   ,@assemblyLine --<ASSEMBLY_LINE, nvarchar(10),>
					   ,@supplierNum --<SUPPLIER_NUM, nvarchar(12),>
					   ,'******' --, nvarchar(20),>
					   ,'******' --, nvarchar(20),>
					   ,'******' --, nvarchar(100),>
					   ,'******' --, nvarchar(100),>
					   ,ISNULL(@dockName,'') --<DOCK, nvarchar(10),>
					   ,@boxParts --<BOX_PARTS, nvarchar(10),>
					   ,0 --<SEQUENCE_NO, int,>
					   ,0 --<PICKUP_SEQ_NO, int,>
					   ,NULL --<RDC_DLOC, varchar(20),>
					   ,0--INBOUND_PACKAGE--, int,>
					   ,'1' ---MEASURING_UNIT_NO --, nvarchar(1),>
					   ,''--INBOUND_PACKAGE_MODEL--, nvarchar(30),>
					   ,0--sum(PACK_COUNT)--, int,>
					   ,0--sum(PACK_COUNT/INBOUND_PACKAGE) --<REQUIRED_INBOUND_PACKAGE, int,>
					   ,0 --sum(PACK_COUNT) --<REQUIRED_INBOUND_PACKAGE_QTY, int,>
					   ,0--<ACTUAL_INBOUND_PACKAGE, int,>
					   ,0--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
					   ,NULL --<BARCODE_DATA, nvarchar(50),>
					   ,NULL --<COMMENTS, nvarchar(200),>)
					)
				END
		
				-- 需要在该A500拉动单，被确认以及修改数量之后，点击发布数据，才能生成条码和送货单
				----生成BARCODE
				--EXEC LES.PROC_TWD_GENERATE_BARCODE @runsheetId
		
				----生成送货单
				--IF(@RunSheetType=1) AND (@Isorganizesheet=0)
				--BEGIN
				--	EXEC [LES].[PROC_TWD_MANUAL_GEN_RECKONING_SHEET] @runsheetId
				--END

    				--设置窗口时间中状态
    			UPDATE LES.TT_TWD_SUPPLIER_SENDTIME 
    			SET LAST_SEND_TIME = GETDATE() , SEND_TIME_STATUS = 1
				WHERE SUPPLIER_SEND_TIME_SN = @SEND_TIME_SN AND BOX_PARTS = @BOXPARTS
    			--WHERE SEND_TIME < @now AND (SEND_TIME_STATUS = 0 OR SEND_TIME_STATUS IS NULL) AND BOX_PARTS = @BOXPARTS

				-- 读取下一个零件类处理
				FETCH NEXT FROM SENDTIME_CURSOR INTO @SEND_TIME_SN, @PLANT, @ASSEMBLYLINE, @SUPPLIERNUM, @BOXPARTS, @SENDTIME, @WINDOWTIME
			END

			CLOSE SENDTIME_CURSOR
			DEALLOCATE SENDTIME_CURSOR
	
			COMMIT TRANSACTION
			
			SET @ResultCode = 0
			SET @ResultMsg = '完成A500拉动单操作'
		END TRY
		BEGIN CATCH
			--出错，则返回执行不成功，回滚事务
			ROLLBACK TRANSACTION
			--记录错误信息
			INSERT INTO [LES].[TS_SYS_EXCEPTION] (TIME_STAMP, [APPLICATION], [METHOD], CLASS,  EXCEPTION_MESSAGE, ERROR_CODE)
			SELECT GETDATE(),'TWD','[LES].[PROC_TWD_GENERATE_RUNSHEET_A500]','PROCEDURE',ERROR_MESSAGE() + ' Error Line:' + CONVERT(NVARCHAR(10),ERROR_LINE()),ERROR_NUMBER()
			
			SET @ResultCode = ERROR_NUMBER()
			SET @ResultMsg = ERROR_MESSAGE() + 'Error Line:' + CONVERT(NVARCHAR(10),ERROR_LINE())
		END CATCH
	END

	SELECT @ResultCode AS Code, @ResultMsg AS Msg
END