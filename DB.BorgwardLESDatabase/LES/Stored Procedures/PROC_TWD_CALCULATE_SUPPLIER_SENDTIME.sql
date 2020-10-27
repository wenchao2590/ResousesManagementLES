/**********************************************************************/
/*   Project Name:  TWD                                               */
/*   Program Name:  [LES].[PROC_TWD_CALCULATE_SUPPLIER_SENDTIME]      */
/*   Called By:     by web and windows service		                  */
/*   Purpose:       This stored procedure calculate supplier sendtime */
/*   Author:        shenjinkui                                        */
/**********************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_CALCULATE_SUPPLIER_SENDTIME]
(
	@filter NVARCHAR(MAX) --过滤条件
)
AS
BEGIN
	DECLARE @InsertCount INT
	SET @InsertCount=0

	--获得当前时间
	DECLARE @CurrentTime DATETIME
	SET @CurrentTime = GETDATE()

	--获得当前日期
	DECLARE @CurrentDate NVARCHAR(10)
	SET @CurrentDate = CONVERT(NVARCHAR(10), @CurrentTime, 120)

	--获得当天开始时间 如：2011-08-02 00:00:00
	DECLARE @CurrentStartDate NVARCHAR(20)
	SET @CurrentStartDate = @CurrentDate + ' 00:00:00'

	--获得当天结束时间 如：2011-08-02 23:59:59
	DECLARE @CurrenteEndDate NVARCHAR(20)
	SET @CurrenteEndDate = @CurrentDate + ' 23:59:59'

	--按流水线计算窗口时间
	IF LEN(@filter) > 0
		BEGIN
			DECLARE @SQL NVARCHAR(MAX)   --查询语句
			DECLARE @count INT
			SET @SQL = 'SELECT @a=count(0) FROM [LES].[TT_TWD_SUPPLIER_SENDTIME] WITH (NOLOCK) WHERE [WORK_DAY] = ''' + @CurrentDate + '''' + @filter
			EXEC sp_executesql @SQL, N'@a int output', @count OUTPUT
			IF @count <= 0
				BEGIN
					--当前流水线没有工作时间，不做运算
					SELECT @InsertCount
					RETURN
				END
		END
	ELSE
		BEGIN
			IF NOT EXISTS
			(
				SELECT TOP 1 * FROM [LES].[TT_TWD_SUPPLIER_SENDTIME] WITH (NOLOCK) WHERE [WORK_DAY] = @CurrentDate
			)
				BEGIN
					--当前没有工作时间，不做运算
					SELECT @InsertCount
					RETURN
				END
		END

	--删除数据已经计算的窗口时间
	DECLARE @del_sql NVARCHAR(MAX)
	SET @del_sql = 'DELETE FROM [LES].[TT_TWD_SUPPLIER_SENDTIME] WITH (ROWLOCK) WHERE [WORK_DAY] > ''' + @CurrentDate + '''' + @filter
	EXEC(@del_sql)

	--创建临时表
	CREATE TABLE #temp
	(
		[SUPPLIER_SEND_TIME_SN] [INT] NOT NULL,
		[WORKSHOP] [NVARCHAR](4) NULL,
		[PLANT_ZONE] [NVARCHAR](5) NULL,
		[ASSEMBLY_LINE] [NVARCHAR](10) NOT NULL,
		[PLANT] [NVARCHAR](5) NOT NULL,
		[SUPPLIER_NUM] [NVARCHAR](12) NOT NULL,
		[PART_TYPE] [INT] NOT NULL,
		[BOX_PARTS] [NVARCHAR](10) NOT NULL,
		[WORK_DAY] [DATETIME] NULL,
		[SEND_TIME] [DATETIME] NOT NULL,
		[WINDOW_TIME] [DATETIME] NOT NULL,
		[LAST_SEND_TIME] [DATETIME] NULL,
		[SEND_TIME_STATUS] [INT] NULL,
		[IS_OVERRIDE] [INT] NULL,
		[TIME_TYPE] [INT] NULL,
		[COMMENTS] [NVARCHAR](200) NULL,
		[UPDATE_DATE] [DATETIME] NULL,
		[UPDATE_USER] [NVARCHAR](50) NULL,
		[CREATE_DATE] [DATETIME] NOT NULL,
		[CREATE_USER] [NVARCHAR](50) NOT NULL
	)

	--获取当天数据作为模板
	DECLARE @template_sql NVARCHAR(MAX)
	--modify by【运维】xhm 2014/08/11【CMDB编号：CR-LES-20140807】start
	--窗口时间计算服务在根据当天窗口时间生成未来2天窗口时间时添加条件，
	--只有活动与测试状态的TWD零件类才能生成对应的未来2天的窗口时间。
	--停用与删除的TWD零件类不再生成窗口时间
	SET @template_sql = '
	INSERT INTO #temp
	SELECT
		[SUPPLIER_SEND_TIME_SN],
		[WORKSHOP],
		[PLANT_ZONE],
		[ASSEMBLY_LINE],
		[PLANT],
		[SUPPLIER_NUM],
		[PART_TYPE],
		[BOX_PARTS],
		[WORK_DAY],
		[SEND_TIME],
		[WINDOW_TIME],
		[LAST_SEND_TIME],
		[SEND_TIME_STATUS],
		[IS_OVERRIDE],
		[TIME_TYPE],
		[COMMENTS],
		[UPDATE_DATE],
		[UPDATE_USER],
		[CREATE_DATE],
		[CREATE_USER]
	FROM [LES].[TT_TWD_SUPPLIER_SENDTIME] A WITH (NOLOCK)
	WHERE [WORK_DAY] >= ''' + @CurrentStartDate + '''
	AND [WORK_DAY] <= ''' + @CurrenteEndDate + '''
	AND EXISTS
	(
		SELECT
			*
		FROM [LES].[TM_TWD_BOX_PARTS] B WITH (NOLOCK) 
		WHERE A.[PLANT] = B.[PLANT]
		AND A.[ASSEMBLY_LINE] = B.[ASSEMBLY_LINE]
		AND A.[SUPPLIER_NUM] = B.[SUPPLIER_NUM]
		AND A.[BOX_PARTS] = B.[BOX_PARTS]
		AND B.[BOX_PARTS_STATE] <> 3 --状态{1：活动，2：测试，3：停用}
	)
	' + @filter
	--modify by【运维】xhm 2014/08/11 end
	exec(@template_sql)


	--更新窗口时间
	DECLARE @Plant NVARCHAR(5)
	DECLARE @AssemblyLine NVARCHAR(20)
	DECLARE @SendTime DATETIME
	DECLARE @FindSendTime DATETIME
	DECLARE @FindSendTimeCount INT
	DECLARE @WorkDay DATETIME
	DECLARE @WindowTime DATETIME
	DECLARE @WorkScheculeCount INT
	DECLARE @SendTimeSn INT
	DECLARE @BoxParts NVARCHAR(20)
	DECLARE @SupplierNum NVARCHAR(20)
	DECLARE @LoadTime INT
	DECLARE @TransTime INT
	DECLARE @DelayTime INT
	DECLARE @UnLoadTime INT

	DECLARE @NEXTDAY DATETIME
	DECLARE @DAY INT
	DECLARE @Time INT

	--第二天
	SELECT * INTO #temp1 FROM #temp

	--检查发送时间是否在工作日历
	DECLARE sendtime_cursor CURSOR FOR
	SELECT 
		[SEND_TIME],
		[SUPPLIER_SEND_TIME_SN],
		[WORK_DAY],
		[PLANT],
		[ASSEMBLY_LINE],
		[SUPPLIER_NUM],
		[BOX_PARTS],
		[WINDOW_TIME]
	FROM #temp1
	OPEN sendtime_cursor 	
	--开始循环游标变量  
	FETCH NEXT FROM sendtime_cursor INTO 
	@SendTime, @SendTimeSn, @WorkDay, @Plant, @AssemblyLine, @SupplierNum, @BoxParts, @WindowTime
	WHILE @@FETCH_STATUS = 0 --返回被 FETCH  语句执行的最后游标的状态，而不是任何当前被连接打开的游标的状态。  
		BEGIN 
			SET @LoadTime = 0
			SET @DelayTime = 0
			SET @TransTime = 0
			SET @UnLoadTime = 0
			SELECT
				@LoadTime = [LOAD_TIME],
				@DelayTime = [DELAY_TIME],
				@TransTime = [TRANSPORT_TIME],
				@UnLoadTime = [UNLOAD_TIME]
			FROM [LES].[TM_TWD_BOX_PARTS] WITH (NOLOCK)
			WHERE [PLANT] = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine
			AND [BOX_PARTS] = @BoxParts AND [SUPPLIER_NUM] = @SupplierNum
			SET @LoadTime = ISNULL(@LoadTime, 0)
			SET @DelayTime = ISNULL(@DelayTime, 0)
			SET @TransTime = ISNULL(@TransTime, 0)
			SET @UnLoadTime = ISNULL(@UnLoadTime, 0)

			SELECT TOP 1 @NEXTDAY = MIN([DATE]) FROM [LES].[TM_BAS_WORK_SCHEDULE] WITH (NOLOCK)
			WHERE [PLANT] = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [DATE] > @WorkDay

			SET @DAY = DATEDIFF(DAY, @WorkDay, @NEXTDAY)
			SET @WindowTime = DATEADD(DAY, @DAY, @WindowTime)
		 
			SET @Time = @LoadTime + @DelayTime + @TransTime + @UnLoadTime
			SET @SendTime = [LES].[FN_GET_TWD_SENDTIME](@Plant, @AssemblyLine, @WindowTime, @Time)

			UPDATE #temp1
				SET [SEND_TIME_STATUS] = 0,
				[SEND_TIME] = @SendTime,
				[WINDOW_TIME] = DATEADD(DAY, @DAY, [WINDOW_TIME]),
				[WORK_DAY] = @NEXTDAY,
				[LAST_SEND_TIME] = NULL,
				[CREATE_DATE]=GETDATE()
			WHERE [SUPPLIER_SEND_TIME_SN] = @SendTimeSn

			--开始循环游标变量  
			FETCH NEXT FROM sendtime_cursor INTO 
			@SendTime, @SendTimeSn, @WorkDay, @Plant, @AssemblyLine, @SupplierNum, @BoxParts, @WindowTime
		END
	CLOSE sendtime_cursor--关闭游标  
	DEALLOCATE sendtime_cursor--释放游标  

	--第三天
	SELECT * INTO #temp2 from #temp1

	--检查发送时间是否在工作日历
	DECLARE sendtime_cursor CURSOR FOR
	SELECT 
		[SEND_TIME],
		[SUPPLIER_SEND_TIME_SN],
		[WORK_DAY],
		[PLANT],
		[ASSEMBLY_LINE],
		[SUPPLIER_NUM],
		[BOX_PARTS],
		[WINDOW_TIME]
	FROM #temp2
	OPEN sendtime_cursor 	
	--开始循环游标变量  
	FETCH NEXT FROM sendtime_cursor INTO 
	@SendTime, @SendTimeSn, @WorkDay, @Plant, @AssemblyLine, @SupplierNum, @BoxParts, @WindowTime
	WHILE @@FETCH_STATUS = 0 --返回被 FETCH  语句执行的最后游标的状态，而不是任何当前被连接打开的游标的状态。  
		BEGIN 
			SET @LoadTime = 0
			SET @DelayTime = 0
			SET @TransTime = 0
			SET @UnLoadTime = 0
			SELECT
				@LoadTime = [LOAD_TIME],
				@DelayTime = [DELAY_TIME],
				@TransTime = [TRANSPORT_TIME],
				@UnLoadTime = [UNLOAD_TIME]
			FROM [LES].[TM_TWD_BOX_PARTS] WITH (NOLOCK)
			WHERE [PLANT] = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine
			AND [BOX_PARTS] = @BoxParts AND [SUPPLIER_NUM] = @SupplierNum
			SET @LoadTime = ISNULL(@LoadTime, 0)
			SET @DelayTime = ISNULL(@DelayTime, 0)
			SET @TransTime = ISNULL(@TransTime, 0)
			SET @UnLoadTime = ISNULL(@UnLoadTime, 0)

			SELECT TOP 1 @NEXTDAY = MIN([DATE]) FROM [LES].[TM_BAS_WORK_SCHEDULE] WITH (NOLOCK)
			WHERE [PLANT] = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [DATE] > @WorkDay

			SET @DAY = DATEDIFF(DAY, @WorkDay, @NEXTDAY)
			SET @WindowTime = DATEADD(DAY, @DAY, @WindowTime)
		 
			SET @Time = @LoadTime + @DelayTime + @TransTime + @UnLoadTime
			SET @SendTime = [LES].[FN_GET_TWD_SENDTIME](@Plant, @AssemblyLine, @WindowTime, @Time)

			UPDATE #temp2
				SET [SEND_TIME_STATUS] = 0,
				[SEND_TIME] = @SendTime,
				[WINDOW_TIME] = DATEADD(DAY, @DAY, [WINDOW_TIME]),
				[WORK_DAY] = DATEADD(DD, @DAY, [WORK_DAY]),
				[LAST_SEND_TIME] = NULL,
				[CREATE_DATE]=GETDATE()
			WHERE [SUPPLIER_SEND_TIME_SN] = @SendTimeSn

			--开始循环游标变量  
			FETCH NEXT FROM sendtime_cursor INTO 
			@SendTime, @SendTimeSn, @WorkDay, @Plant, @AssemblyLine, @SupplierNum, @BoxParts, @WindowTime
		END
	CLOSE sendtime_cursor--关闭游标  
	DEALLOCATE sendtime_cursor--释放游标  

	INSERT INTO #temp1 SELECT * FROM #temp2

	--插入已经计算的窗口时间
	INSERT INTO [LES].[TT_TWD_SUPPLIER_SENDTIME]
	(
		[WORKSHOP],
		[PLANT_ZONE],
		[ASSEMBLY_LINE],
		[PLANT],
		[SUPPLIER_NUM],
		[PART_TYPE],
		[BOX_PARTS],
		[WORK_DAY],
		[SEND_TIME],
		[WINDOW_TIME],
		[LAST_SEND_TIME],
		[SEND_TIME_STATUS],
		[IS_OVERRIDE],
		[TIME_TYPE],
		[COMMENTS],
		[UPDATE_DATE],
		[UPDATE_USER],
		[CREATE_DATE],
		[CREATE_USER]
	)
	SELECT 
		[WORKSHOP],
		[PLANT_ZONE],
		[ASSEMBLY_LINE],
		[PLANT],
		[SUPPLIER_NUM],
		[PART_TYPE],
		[BOX_PARTS],
		[WORK_DAY],
		[SEND_TIME],
		[WINDOW_TIME],
		[LAST_SEND_TIME],
		[SEND_TIME_STATUS],
		[IS_OVERRIDE],
		[TIME_TYPE],
		[COMMENTS],
		NULL,
		NULL,
		GETDATE(),
		'[PROC_TWD_CALCULATE_SUPPLIER_SENDTIME]'
	FROM  #temp1
  
	SELECT @InsertCount = COUNT(1) FROM #temp1
    
	DROP TABLE #temp1
	DROP TABLE #temp2
	DROP TABLE #temp

	SELECT @InsertCount
END