/************************************************************************/
/*   Program Name:  [LES].[PROC_INSERT_OUTPUT_ONBOARDTASK]				*/
/*   Called By:     procedure											*/
/*   Description:	针对出库单生成车载任务								*/
/*   Author:		孙述霄 2017-08-14									*/
/************************************************************************/
CREATE PROCEDURE [LES].[PROC_INSERT_OUTPUT_ONBOARDTASK]
(
	@OutputId BIGINT,
	@Type NVARCHAR(10)
)
AS
BEGIN
	DECLARE @OUTPUT_NO NVARCHAR(50)			--出库单编号
	DECLARE @PLANT AS NVARCHAR(5)			--工厂
	DECLARE @WM_NO AS NVARCHAR(10)			--源仓库
	DECLARE @ZONE_NO AS NVARCHAR(20)		--源存储区
	DECLARE @DLOC AS NVARCHAR(30)			--源库位
	DECLARE @TARGET_WM AS NVARCHAR(10)		--目标仓库
	DECLARE @TARGET_ZONE AS NVARCHAR(20)	--目标存储区
	DECLARE @TARGET_DLOC AS NVARCHAR(30)	--目标库位
	DECLARE @PART_NO AS NVARCHAR(20)		--零件号
	DECLARE @PART_CNAME AS NVARCHAR(20)		--零件名称
	DECLARE @PACKAGE AS INT					--出库包装数
	DECLARE @TRAY_INFO_ID AS BIGINT			--托ID号
	DECLARE @TRAY_NO AS NVARCHAR(20)		--托号
	DECLARE @BATCH_NO AS NVARCHAR(20)		--批次号
	DECLARE @NUM AS INT						--托件数
	DECLARE @BOX_NUM AS INT					--托箱数
	DECLARE @TEMPNUM AS INT					--零时件数
	DECLARE @TEMPBOXNUM AS INT				--零件箱数
	DECLARE @TEMPRENUM AS INT				--零时需求件数
	DECLARE @IS_DYNAMIC_DLOC AS INT			--是否动态库位
	DECLARE @IS_OUTPUT_SOLE AS INT			--是否上下架
	DECLARE @TRAY_OUT_ISALL AS INT			--是否整托出库
	DECLARE @OUTPUT_DETAIL_ID AS INT		--出库明细ID
	DECLARE @REQUIRED_QTY AS INT			--需求件数
	DECLARE @TASK_TYPE AS INT				--车载任务类型

	SELECT @OUTPUT_NO = [OUTPUT_NO] FROM [LES].[TT_WMM_OUTPUT] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OutputId
	SELECT TOP 1 @PLANT = [PLANT], @WM_NO = [WM_NO], @ZONE_NO = [ZONE_NO], @DLOC = [DLOC], @TARGET_WM = [TARGET_WM], @TARGET_ZONE = [TARGET_ZONE], @TARGET_DLOC = [TARGET_DLOC] FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OutputId

	IF @Type <> 'JIS'
		BEGIN
			SELECT @IS_DYNAMIC_DLOC = [IS_DYNAMIC_DLOC], @IS_OUTPUT_SOLE = [IS_OUTPUT_SOLE] FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @WM_NO AND [ZONE_NO] = @ZONE_NO
			SET @IS_DYNAMIC_DLOC = ISNULL(@IS_DYNAMIC_DLOC, 0)
			SET @IS_OUTPUT_SOLE = ISNULL(@IS_OUTPUT_SOLE, 0)
			IF @IS_DYNAMIC_DLOC = 1
				BEGIN
					IF @IS_OUTPUT_SOLE = 1
						SET @TASK_TYPE = 20	--下架
					ELSE
						SET @TASK_TYPE = 30	--移库

					SELECT @OUTPUT_DETAIL_ID = MIN([OUTPUT_DETAIL_ID]) FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OutputId
					WHILE @OUTPUT_DETAIL_ID IS NOT NULL
						BEGIN
							SET @TEMPNUM = 0
							SET @TEMPBOXNUM = 0
							SET @TRAY_OUT_ISALL = 0
							SELECT @PART_NO = [PART_NO], @PART_CNAME = [PART_CNAME], @REQUIRED_QTY = [REQUIRED_QTY], @PACKAGE = [INHOUSE_PACKAGE] FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_DETAIL_ID] = @OUTPUT_DETAIL_ID
							SELECT @TRAY_OUT_ISALL = [TRAY_OUT_ISALL] FROM [LES].[TM_BAS_PARTS_STOCK] WHERE [PLANT] = @PLANT AND [WM_NO] = @WM_NO AND [ZONE_NO] = @ZONE_NO AND [PART_NO] = @PART_NO
							SET @TRAY_OUT_ISALL = ISNULL(@TRAY_OUT_ISALL, 0)
							SET @TEMPRENUM = @REQUIRED_QTY
							SET @PACKAGE = ISNULL(@PACKAGE, 1)

							WHILE @TEMPRENUM > 0
								BEGIN
									SET @TRAY_INFO_ID = NULL
									SELECT TOP 1 @TRAY_INFO_ID = [ID], @TRAY_NO = [TRAY_NO], @NUM = [NUM], @BOX_NUM = [BOX_NUM], @BATCH_NO = [BATCH_NO], @DLOC = [DLOC] FROM [LES].[TT_SPM_TRAY_INFO] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @WM_NO AND [ZONE_NO] = @ZONE_NO AND [PART_NO] = @PART_NO AND ([TRAY_STATUS] = 2 OR [TRAY_STATUS] = 4) AND ISNULL([DLOC], '') <> '' AND ISNULL([BATCH_NO], '') <> '' ORDER BY [BATCH_NO]
									IF @TRAY_INFO_ID IS NULL
										BREAK

									IF @TEMPRENUM >= @NUM
										BEGIN
											SET @TEMPRENUM = @TEMPRENUM - @NUM
											SET @TEMPNUM = @TEMPNUM + @NUM
											SET @TEMPBOXNUM = @TEMPBOXNUM + @BOX_NUM
											--生成下架或移库任务
											EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', @TASK_TYPE, @PART_NO, @BOX_NUM, @NUM, @TRAY_NO, @PLANT, @WM_NO, @ZONE_NO, @DLOC, @TARGET_WM, @TARGET_ZONE, @TARGET_DLOC, 'admin'
										END
									ELSE
										BEGIN
											IF @TRAY_OUT_ISALL = 1
												BEGIN
													--整托出库
													SET @TEMPRENUM = 0
													SET @TEMPNUM = @TEMPNUM + @NUM
													SET @TEMPBOXNUM = @TEMPBOXNUM + @BOX_NUM
													EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', @TASK_TYPE, @PART_NO, @BOX_NUM, @NUM, @TRAY_NO, @PLANT, @WM_NO, @ZONE_NO, @DLOC, @TARGET_WM, @TARGET_ZONE, @TARGET_DLOC, 'admin'
												END
											ELSE
												BEGIN
													--拆托
													SET @NUM = @TEMPRENUM
													SET @BOX_NUM = CEILING(@NUM / @PACKAGE)
													SET @TEMPNUM = @TEMPNUM + @TEMPRENUM
													SET @TEMPBOXNUM = @TEMPBOXNUM + @BOX_NUM
													SET @TEMPRENUM = 0
													EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', 40, @PART_NO, @BOX_NUM, @NUM, @TRAY_NO, @PLANT, @WM_NO, @ZONE_NO, @DLOC, @TARGET_WM, @TARGET_ZONE, @TARGET_DLOC, 'admin'
												END
										END

									--生成任务日志
									INSERT INTO [LES].[TT_SPM_TRAY_LOG]
									(
										[TRAY_INFO_ID],
										[TRAY_NO],
										[PLANT],
										[WM_NO],
										[ZONE_NO],
										[DLOC],
										[TARGET_WM],
										[TARGET_ZONE],
										[TARGET_DLOC],
										[ORDER_NO],
										[ORDER_TYPE],
										[PART_NO],
										[PART_CNAME],
										[PART_QTY],
										[BOX_QTY],
										[BATCH_NO],
										[DEAL_STATUS],
										[DEAL_TIME],
										[VALID_FLAG],
										[CREATE_USER],
										[CREATE_DATE],
										[MODIFY_USER],
										[MODIFY_DATE]
									)
									SELECT
										@TRAY_INFO_ID,
										@TRAY_NO,
										@PLANT,
										@WM_NO,
										@ZONE_NO,
										@DLOC,
										@TARGET_WM,
										@TARGET_ZONE,
										@TARGET_DLOC,
										@OUTPUT_NO,
										2,
										@PART_NO,
										@PART_CNAME,
										@NUM,
										@BOX_NUM,
										@BATCH_NO,
										0,
										NULL,
										1,
										'admin',
										GETDATE(),
										'admin',
										GETDATE()

									--预占用当前托
									UPDATE [LES].[TT_SPM_TRAY_INFO] WITH (ROWLOCK) SET [TRAY_STATUS] = 5 WHERE [ID] = @TRAY_INFO_ID
								END

							SELECT @OUTPUT_DETAIL_ID = MIN([OUTPUT_DETAIL_ID]) FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OutputId AND [OUTPUT_DETAIL_ID] > @OUTPUT_DETAIL_ID
						END
				END
			ELSE
				BEGIN
					SELECT @OUTPUT_DETAIL_ID = MIN([OUTPUT_DETAIL_ID]) FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OutputId
					WHILE @OUTPUT_DETAIL_ID IS NOT NULL
						BEGIN
							--获取明细信息
							SELECT @PART_NO = [PART_NO], @PART_CNAME = [PART_CNAME], @NUM = [REQUIRED_QTY], @BOX_NUM = [REQUIRED_BOX_NUM] FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_DETAIL_ID] = @OUTPUT_DETAIL_ID
							SET @TRAY_NO = @OUTPUT_NO + RIGHT('0000' + CAST(@OUTPUT_DETAIL_ID AS NVARCHAR), 4)

							--生成托盘信息
							INSERT INTO [LES].[TT_SPM_TRAY_INFO]
							(
								[TRAY_NO],
								[PLANT],
								[WM_NO],
								[ZONE_NO],
								[DLOC],
								[BILL_NO],
								[PART_NO],
								[PART_CNAME],
								[NUM],
								[BOX_NUM],
								[BIND_TIME],
								[TRAY_STATUS],
								[BATCH_NO],
								[VALID_FLAG],
								[COMMENTS],
								[CREATE_USER],
								[CREATE_DATE],
								[UPDATE_USER],
								[UPDATE_DATE]
							)
							SELECT
								@TRAY_NO,
								@PLANT,
								@WM_NO,
								@ZONE_NO,
								@DLOC,
								@OUTPUT_NO,
								@PART_NO,
								@PART_CNAME,
								@NUM,
								@BOX_NUM,
								GETDATE(),
								5,
								'',
								1,
								'',
								'admin',
								GETDATE(),
								'admin',
								GETDATE()

							SELECT @TRAY_INFO_ID = SCOPE_IDENTITY()

							--生成任务日志
							INSERT INTO [LES].[TT_SPM_TRAY_LOG]
							(
								[TRAY_INFO_ID],
								[TRAY_NO],
								[PLANT],
								[WM_NO],
								[ZONE_NO],
								[DLOC],
								[TARGET_WM],
								[TARGET_ZONE],
								[TARGET_DLOC],
								[ORDER_NO],
								[ORDER_TYPE],
								[PART_NO],
								[PART_CNAME],
								[PART_QTY],
								[BOX_QTY],
								[BATCH_NO],
								[DEAL_STATUS],
								[DEAL_TIME],
								[VALID_FLAG],
								[CREATE_USER],
								[CREATE_DATE],
								[MODIFY_USER],
								[MODIFY_DATE]
							)
							SELECT
								@TRAY_INFO_ID,
								@TRAY_NO,
								@PLANT,
								@WM_NO,
								@ZONE_NO,
								@DLOC,
								@TARGET_WM,
								@TARGET_ZONE,
								@TARGET_DLOC,
								@OUTPUT_NO,
								2,
								@PART_NO,
								@PART_CNAME,
								@NUM,
								@BOX_NUM,
								'',
								0,
								NULL,
								1,
								'admin',
								GETDATE(),
								'admin',
								GETDATE()

							--生成PCS车载任务
							EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', 80, @PART_NO, @BOX_NUM, @NUM, @TRAY_NO, @PLANT, @WM_NO, @ZONE_NO, @DLOC, @TARGET_WM, @TARGET_ZONE, @TARGET_DLOC, 'admin'
							
							SELECT @OUTPUT_DETAIL_ID = MIN([OUTPUT_DETAIL_ID]) FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OutputId AND [OUTPUT_DETAIL_ID] > @OUTPUT_DETAIL_ID
						END
				END
		END
	ELSE
		BEGIN
			--JIS车载任务
			--获取零件数量
			SELECT @NUM = SUM([NUM]) FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OutputId

			--生成托盘信息
			INSERT INTO [LES].[TT_SPM_TRAY_INFO]
			(
				[TRAY_NO],
				[PLANT],
				[WM_NO],
				[ZONE_NO],
				[DLOC],
				[BILL_NO],
				[PART_NO],
				[PART_CNAME],
				[NUM],
				[BOX_NUM],
				[BIND_TIME],
				[TRAY_STATUS],
				[BATCH_NO],
				[VALID_FLAG],
				[COMMENTS],
				[CREATE_USER],
				[CREATE_DATE],
				[UPDATE_USER],
				[UPDATE_DATE]
			)
			SELECT
				@OUTPUT_NO,
				@PLANT,
				@WM_NO,
				@ZONE_NO,
				@DLOC,
				@OUTPUT_NO,
				'',
				'',
				@NUM,
				1,
				GETDATE(),
				5,
				'',
				1,
				'',
				'admin',
				GETDATE(),
				'admin',
				GETDATE()

			SELECT @TRAY_INFO_ID = SCOPE_IDENTITY()

			--生成任务日志
			INSERT INTO [LES].[TT_SPM_TRAY_LOG]
			(
				[TRAY_INFO_ID],
				[TRAY_NO],
				[PLANT],
				[WM_NO],
				[ZONE_NO],
				[DLOC],
				[TARGET_WM],
				[TARGET_ZONE],
				[TARGET_DLOC],
				[ORDER_NO],
				[ORDER_TYPE],
				[PART_NO],
				[PART_CNAME],
				[PART_QTY],
				[BOX_QTY],
				[BATCH_NO],
				[DEAL_STATUS],
				[DEAL_TIME],
				[VALID_FLAG],
				[CREATE_USER],
				[CREATE_DATE],
				[MODIFY_USER],
				[MODIFY_DATE]
			)
			SELECT
				@TRAY_INFO_ID,
				@OUTPUT_NO,
				@PLANT,
				@WM_NO,
				@ZONE_NO,
				@DLOC,
				@TARGET_WM,
				@TARGET_ZONE,
				@TARGET_DLOC,
				@OUTPUT_NO,
				0,
				'',
				'',
				@NUM,
				1,
				'',
				0,
				NULL,
				1,
				'admin',
				GETDATE(),
				'admin',
				GETDATE()

			--生成JIS车载任务
			EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', 30, '', 1, @NUM, @OUTPUT_NO, @PLANT, @WM_NO, @ZONE_NO, @DLOC, @TARGET_WM, @TARGET_ZONE, @TARGET_DLOC, 'admin'
		END
END