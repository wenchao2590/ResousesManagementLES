

CREATE PROCEDURE [LES].[PROC_TWD_UPDATE_COUNTER] 
AS
    SET NOCOUNT ON
    DECLARE @rowCount INT
	SELECT @rowCount = COUNT(1) FROM [LES].[TE_TWD_MATERIAL_CONSUME_TEMP]
	PRINT @rowCount 
	DECLARE @flag INT
	SET @flag = 1
	DECLARE @updateCount INT
	
	DECLARE @plant NVARCHAR(5)
	DECLARE @assemblyLine NVARCHAR(10)
	DECLARE @partNo NVARCHAR(20)
	DECLARE @packCount INT
	DECLARE @breakPointStatus INT
	DECLARE @breakPointNo NVARCHAR(10)
	DECLARE @model NVARCHAR(100)
	DECLARE @consumeModelNo NVARCHAR(10)
	DECLARE @supplier_num NVARCHAR(10)

	DECLARE @BreakPointType INT,
			@Break_Time		DATETIME,
			@Remain_Count	INT,
			@DiffCount		INT,	-- 计数器
			@ModifyCount	INT,	-- 入厂包装数量
			@ActulCount		INT		-- 实际剩余数量，此处被用做，是否已经做过告警的标识（为-1，表示已告警过）

	WHILE( @flag <= @rowCount )
	BEGIN
		SELECT	@plant = PLANT,
				@assemblyLine = ASSEMBLY_LINE,
				@partNo = PART_NO,
				@packCount = PACK_COUNT,
				@consumeModelNo=MODEL,
				@supplier_num=SUPPLIER_NUM
		FROM [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] 
		WHERE ID = @flag
		--print @plant
		--print @assemblyLine
		--print @partNo
		--print @packCount
		--如果存在断点的零件，那么要做相应的替换
		-- 20141011 shianyuan 修改， 添加切换类型的过滤
		--if(exists(select * from LES.TM_BAS_INBOUND_BREAKPOINT_PART where PLANT=@plant and  ASSEMBLY_LINE=@assemblyLine  and PART_NO=@partNo and datediff(mi,break_time,getdate())>0 ))
		--begin
		--	select @breakPointStatus=[BREAKPOINT_STATUS],@breakPointNo=[INBOUND_BREAKPOINT_NO],@model=[MODEL] from LES.TM_BAS_INBOUND_BREAKPOINT_PART
		--	where PLANT=@plant and  ASSEMBLY_LINE=@assemblyLine  and PART_NO=@partNo and datediff(mi,break_time,getdate())>0

		SELECT	@breakPointStatus=[BREAKPOINT_STATUS],
				@breakPointNo=[INBOUND_BREAKPOINT_NO],
				@model=[MODEL],
				@BreakPointType = BREAKPOINT_TYPE,
				@Break_Time = BREAK_TIME,
				@Remain_Count = ISNULL(REMAIN_COUNT,0),
				@DiffCount = ISNULL(DIFFERENT_COUNT,0),
				@ModifyCount = ISNULL(MODIFY_REMAIN_COUNT,0),
				@ActulCount = ISNULL(ACTUAL_REMAIN_COUNT,0)
		FROM LES.TM_BAS_INBOUND_BREAKPOINT_PART where PLANT=@plant and  ASSEMBLY_LINE=@assemblyLine  and PART_NO=@partNo
		IF ISNULL(@breakPointNo,0) != 0 AND @BreakPointType = 1 AND DATEDIFF(MI,@Break_Time,getdate()) > 0--按时间切换
		BEGIN
			if(@breakPointStatus!=1)
			BEGIN
				update LES.TM_BAS_INBOUND_BREAKPOINT_PART SET BREAKPOINT_STATUS = 1 where [INBOUND_BREAKPOINT_NO]=@breakPointNo
			END
			--替换零件
			IF((LEN(@model)=0) OR (CHARINDEX(@consumeModelNo, @model) > 0))
			BEGIN
				UPDATE A
				SET	CURRENT_PART_COUNT = CURRENT_PART_COUNT + (@packCount *LES.FN_GET_QUOTA(@plant,@partNo,@supplier_num)* A.[AMOUNTRATIO] / 100.0),
					[UPDATE_DATE] = GETDATE()
				FROM [LES].[TT_TWD_CONSUME_COUNTER] A
				INNER JOIN LES.TM_BAS_INBOUND_BREAKPOINT_DETAIL B ON A.[PART_NO] = B.[PART_NO]
				WHERE B.[INBOUND_BREAKPOINT_NO] = @breakPointNo
					AND A.PLANT = @plant
					AND A.ASSEMBLY_LINE = @assemblyLine
					AND A.SUPPLIER_NUM=@supplier_num
			END
		END
		ELSE IF ISNULL(@breakPointNo,0) != 0 AND @BreakPointType = 2 AND DATEDIFF(MI,@Break_Time,getdate()) >= 0 --按剩余量切换
		BEGIN
			IF(@breakPointStatus!=1 AND @Remain_Count-@packCount <=0 )
			BEGIN
				UPDATE LES.TM_BAS_INBOUND_BREAKPOINT_PART set BREAKPOINT_STATUS = 1 where [INBOUND_BREAKPOINT_NO]=@breakPointNo
			END

			--替换零件
			IF((LEN(@model)=0) OR (CHARINDEX(@consumeModelNo, @model) > 0))
			BEGIN
				-- 剩余量组不了一个整箱的需求
				IF @Remain_Count + @DiffCount < @ModifyCount
				BEGIN
					IF @ActulCount <> -1		-- 实际剩余数量，此处被用做，是否已经做过告警的标识（为-1，表示已告警过）
					BEGIN
						-- 发送告警邮件
						INSERT INTO [LES].[TT_SYS_MAIL_SEND_LIST] ([SYS_ID],[ALARM_NAME],[ALARM_SUBJECT],[MAIL_BODY],[CC_MAIL_GROUP],[MAILS],[SEND_STATUS],[SEND_DATE],[CREATE_USER],[CREATE_DATE])
						VALUES
						(
							1009,
							'INBOUND 断点切换告警',
							'INBOUND 断点按剩余量切换告警',
							'PLANT=''' + @plant + ''', ASSEMBLYLINE=''' + @assemblyLine + ''', PARTNO=''' + @partNo + ''', 剩余量=' + CONVERT(NVARCHAR(10), @Remain_Count) + ', 计数器='+ CONVERT(NVARCHAR(10), @DiffCount),
							NULL,
							NULL,
							0,
							NULL,
							'PROC_TWD_UPDATE_COUNTER',
							GETDATE()
						)

						-- 更新已通知标志
						UPDATE LES.TM_BAS_INBOUND_BREAKPOINT_PART SET ACTUAL_REMAIN_COUNT = -1 WHERE [INBOUND_BREAKPOINT_NO]=@breakPointNo
					END
				END
				ELSE IF @DiffCount + @packCount >= @ModifyCount
				BEGIN
					-- 如果计数器计计满一个标准箱时，插入一个消耗到零件消耗表
					INSERT INTO [LES].[TI_TWD_MATERIAL_CONSUME]
					   ([PLANT_ZONE]
					   ,[WORKSHOP]
					   ,[ASSEMBLY_LINE]
					   ,[PLANT]
					   ,[LOCATION]
					   ,[REQUEST_TIME]
					   ,[INTERFACE_STATUS]
					   ,[PROCESS_TIME]
					   ,[PART_NO]
					   ,[INDENTIFY_PART_NO]
					   ,[PART_CNAME]
					   ,[PART_ENAME]
					   ,[SUPPLIER_NUM]
					   ,[DOCK]
					   ,[BOX_PARTS]
					   ,[INTERFACE_TYPE]
					   ,[PACK_COUNT]
					   ,[REQURIED_PACK]
					   ,[INBOUND_PACKAGE_MODEL]
					   ,[INBOUND_PACKAGE]
					   ,[MEASURING_UNIT_NO]
					   ,[IS_ORGANIZE_SHEET]
					   ,[CREATE_DATE]
					   ,[CREATE_USER]
					   ,INHOUSE_PACKAGE_MODEL)
					SELECT TOP 1
						A.PLANT_ZONE,
						A.WORKSHOP,
						A.ASSEMBLY_LINE,
						A.PLANT,
						NULL,
						GETDATE(),
						6,
						NULL,
						A.[PART_NO],
						A.[INDENTIFY_PART_NO] ,
						B.[PART_CNAME] ,
						B.[PART_ENAME] ,
						@supplier_num,
						B.[DOCK],
						B.[BOX_PARTS],
						0,--[INTERFACE_TYPE]
						@ModifyCount,
						1,
						[INBOUND_PACKAGE_MODEL],
						@ModifyCount,
						1,
						2,
						GETDATE(),
						'TWD UPDATE COUNTER',
						NULL
					FROM [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] A
					JOIN
					(
						SELECT TOP 1 PART_NO, INBOUND_PART_CLASS AS [BOX_PARTS], [DOCK], PART_CNAME, PART_ENAME
						FROM [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD] B
						WHERE PLANT=@plant AND ASSEMBLY_LINE=@assemblyLine AND PART_NO=@partNo AND ISNULL(INBOUND_PART_CLASS,'')<>''
					) B ON A.ID = @flag AND A.PART_NO = B.PART_NO

					UPDATE LES.TM_BAS_INBOUND_BREAKPOINT_PART 
					SET REMAIN_COUNT = REMAIN_COUNT - @packCount,					-- 扣减剩余量
						DIFFERENT_COUNT = @DiffCount + @packCount - @ModifyCount		-- 重置计数器
					WHERE [INBOUND_BREAKPOINT_NO] = @breakPointNo
				END
				ELSE
				BEGIN
					UPDATE LES.TM_BAS_INBOUND_BREAKPOINT_PART 
					SET REMAIN_COUNT = REMAIN_COUNT - @packCount,	-- 扣减剩余量
						DIFFERENT_COUNT = @DiffCount + @packCount	-- 累加计数器
					WHERE [INBOUND_BREAKPOINT_NO] = @breakPointNo
				END
			END
		END
		ELSE
		BEGIN 
			UPDATE [LES].[TT_TWD_CONSUME_COUNTER] 
			SET CURRENT_PART_COUNT = CURRENT_PART_COUNT+(@packCount*LES.FN_GET_QUOTA(@plant,@partNo,@supplier_num)*[AMOUNTRATIO] / 100.0),
				[UPDATE_DATE]=getdate()
			WHERE PLANT = @plant and ASSEMBLY_LINE = @assemblyLine and PART_NO = @partNo AND SUPPLIER_NUM=@supplier_num
		END
		SET @updateCount = @@rowCount
		IF(@updateCount <> 1)
		BEGIN
			PRINT 'ERROR'
			--updatecount 为0 的时候说明计数表中缺少数据
			
			--updateCount 大于1的时候说明计数表中有重复数据
		END

		SET @breakPointStatus = NULL	-- 用于清空数据，防止下次使用时，被污染
		SET @breakPointNo = NULL
		SET @model = NULL
		SET @BreakPointType = NULL
		SET @Break_Time = NULL
		SET @Remain_Count = NULL
		SET @DiffCount = NULL
		SET @ModifyCount = NULL
		SET @ActulCount = NULL

		SET @flag = @flag + 1
	END