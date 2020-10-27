/************************************************/
/* Author:		孙述霄							*/
/* Create date: 2017-08-31						*/
/* Description:	组托移库上架处理				*/
/************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_TRAYDETAIL_MOVE]
(
    @TrayNo NVARCHAR(20),
	@SourceWM NVARCHAR(32),
	@SourceZone NVARCHAR(32),
	@TargetWM NVARCHAR(32),
	@TargetZone NVARCHAR(32),
	@xml XML,
	@LoginName NVARCHAR(50),
    @Result INT OUTPUT,
	@ResultMessage NVARCHAR(4000) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN
        DECLARE @idoc INT
		DECLARE @NUM INT
		DECLARE @BOXNUM INT
		DECLARE @BATCHNO NVARCHAR(100)
		DECLARE @PLANT NVARCHAR(5)
		DECLARE @BILLNO NVARCHAR(32)
		DECLARE @PARTNO NVARCHAR(20)
		DECLARE @PARTCNAME NVARCHAR(100)
		DECLARE @SOURCEDLOC NVARCHAR(32)
		DECLARE @TRAYINFOID BIGINT
		DECLARE @DLOCSTATUS INT
		DECLARE @ISDYNAMICDLOC INT
		DECLARE @ISOUTPUTSOLE INT
		DECLARE @ISCREATETASK INT
		DECLARE @PACKAGE INT
		DECLARE	@OutPutId BIGINT
        EXEC sp_xml_preparedocument @idoc OUTPUT, @xml

		SET @Result = 0
		SET @ResultMessage = ''

		SET @PLANT = ''
		SET @BATCHNO = ''
		SET @DLOCSTATUS = 0
		SET @ISDYNAMICDLOC = 0
		SET @ISOUTPUTSOLE = 0
		SET @ISCREATETASK = 0
		
		SELECT @PLANT = [PLANT], @ISDYNAMICDLOC=[IS_DYNAMIC_DLOC], @ISOUTPUTSOLE=[IS_OUTPUT_SOLE] FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK) WHERE [WM_NO] = @TargetWM AND [ZONE_NO] = @TargetZone

        INSERT INTO [LES].[TT_SPM_TRAY_DETAIL]
        (
			[TRAY_NO],
			[BARCODE_DATA],
			[PLANT],
			[WM_NO],
			[ZONE_NO],
			[DLOC],
			[PART_NO],
			[PART_CNAME],
			[NUM],
			[BIND_TIME],
			[UNBIND_TIME],
			[BATCH_NO],
			[TRAY_ID],
			[BAR_ID],
			[BIND_FLAG],
			[VALID_FLAG],
			[COMMENTS],
			[CREATE_USER],
			[CREATE_DATE],
			[UPDATE_USER],
			[UPDATE_DATE]
		)
        SELECT
			[TRAY_NO],
			[BARCODE_DATA],
			@PLANT AS [PLANT],
			@TargetWM AS [WM_NO],
			@TargetZone AS [ZONE_NO],
			'' AS [DLOC],
			[PART_NO],
			[PART_CNAME],
			[NUM],
			GETDATE() AS [BIND_TIME],
			NULL AS [UNBIND_TIME],
			[BATCH_NO],
			[TRAY_ID],
			[BAR_ID],
			[BIND_FLAG],
			[VALID_FLAG],
			[COMMENTS],
			[CREATE_USER],
			GETDATE() AS [CREATE_DATE],
			[UPDATE_USER],
			GETDATE() AS [UPDATE_DATE]
        FROM OPENXML (@idoc, '/XmlTable/rows',2)
		WITH
		(  
			[TRAY_NO] NVARCHAR(20),
			[BARCODE_DATA] NVARCHAR(50),
			[PLANT] NVARCHAR(5),
			[WM_NO] NVARCHAR(10),
			[ZONE_NO] NVARCHAR(20),
			[DLOC] NVARCHAR(30),
			[PART_NO] NVARCHAR(20),
			[PART_CNAME] NVARCHAR(100),
			[NUM] INT,
			[BATCH_NO] NVARCHAR(20),
			[TRAY_ID] BIGINT,
			[BAR_ID] BIGINT,
			[BIND_FLAG] INT,
			[VALID_FLAG] INT,
			[COMMENTS] NVARCHAR(200),
			[CREATE_USER] NVARCHAR(50),
			[UPDATE_USER] NVARCHAR(50)
		)

		SELECT @NUM = SUM(ISNULL([NUM], 0)), @BOXNUM = COUNT(1), @PARTNO = MAX([PART_NO]), @PARTCNAME = MAX([PART_CNAME]), @TRAYINFOID = MAX([TRAY_ID]) FROM [LES].[TT_SPM_TRAY_DETAIL] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1

		UPDATE [LES].[TT_SPM_TRAY_DETAIL] WITH (ROWLOCK) SET [BAR_ID] = (SELECT [BARCODE_DETAIL_ID] FROM [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] WITH (NOLOCK) WHERE [BARCODE_DATA] = [LES].[TT_SPM_TRAY_DETAIL].[BARCODE_DATA]) WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1 AND ISNULL([BAR_ID], '') = ''

		--获取批次号
		SELECT @BATCHNO = MIN(ISNULL([BATTH_NO],'')) FROM [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] WITH (NOLOCK) WHERE [BARCODE_DETAIL_ID] IN (SELECT [BAR_ID] FROM [LES].[TT_SPM_TRAY_DETAIL] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1) AND ISNULL([BATTH_NO], '') <> ''
		SET @BATCHNO = ISNULL(@BATCHNO, '')

		UPDATE [LES].[TT_SPM_TRAY_DETAIL] WITH (ROWLOCK) SET [BATCH_NO] = @BATCHNO WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1
		UPDATE [LES].[TT_SPM_TRAY_INFO] WITH (ROWLOCK) SET [TRAY_STATUS] = 2, [NUM] = @NUM, [BOX_NUM] = @BOXNUM, [BIND_TIME] = GETDATE(), [BILL_NO] = '', [PART_NO] = @PARTNO, [PART_CNAME] = @PARTCNAME, [PLANT] = @PLANT, [WM_NO] = @TargetWM, [ZONE_NO] = @TargetZone, [DLOC] = '', [BATCH_NO] = @BATCHNO  WHERE [TRAY_NO] = @TrayNo

		SET @PACKAGE = NULL
		SELECT @ISCREATETASK=[IS_CREATE_TASK], @PACKAGE = [PACKAGE] FROM [LES].[TM_BAS_PARTS_STOCK] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @TargetWM AND [ZONE_NO] = @TargetZone AND [PART_NO] = @PARTNO
		IF @ISCREATETASK = 1
			BEGIN
				IF ISNULL(@PACKAGE, 0) = 0
					BEGIN
						SET @Result = 1
						SET @ResultMessage = '目标仓库对应的零件仓库信息中标准包装数为空或者为零'
					END 

				IF @Result = 0
					BEGIN
						SET @PACKAGE = NULL
						SELECT @PACKAGE = [PACKAGE] FROM [LES].[TM_BAS_PARTS_STOCK] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @SourceWM AND [ZONE_NO] = @SourceZone AND [PART_NO] = @PARTNO
						IF @PACKAGE IS NULL
							BEGIN
								SET @Result = 1
								SET @ResultMessage = '源仓库/存储区不存在指定零件信息'
							END
						IF @Result = 0
							BEGIN
								IF @PACKAGE = 0
									BEGIN
										SET @Result = 1
										SET @ResultMessage = '源仓库对应的零件仓库信息中标准包装数为空或者为零'
									END
							END
					END

				IF @Result = 0
					BEGIN
						SET @BILLNO = 'CZ' + RIGHT(REPLACE(REPLACE(REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 120), '-', ''), ':', ''), ' ', ''), 12)
						SELECT TOP 1 @SOURCEDLOC = (CASE WHEN EXISTS(SELECT 1 FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @SourceWM AND [ZONE_NO] = @SourceZone AND [IS_DYNAMIC_DLOC] = 1) THEN '' ELSE [DLOC] END) FROM [LES].[TM_BAS_PARTS_STOCK] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @SourceWM AND [ZONE_NO] = @SourceZone AND [PART_NO] = @PARTNO
						SET @SOURCEDLOC = ISNULL(@SOURCEDLOC, '')
						--生成出库单主表
						INSERT INTO [LES].[TT_WMM_OUTPUT]
						(
							[OUTPUT_NO], 
							[PLANT],
							[SUPPLIER_NUM],
							[WM_NO],
							[ZONE_NO],
							[SEND_TIME],
							[OUTPUT_TYPE],
							[CONFIRM_FLAG],
							[PLANT_ZONE],
							[RUNSHEET_CODE],
							[CREATE_DATE],
							[CREATE_USER]
						)
						VALUES
						(
							@BILLNO,
							@PLANT,
							@SourceZone,
							@SourceWM,
							@SourceZone,
							GETDATE(),
							1,
							1,
							@TargetZone,
							'CZO',
							GETDATE(),
							@LoginName
						)
						SELECT @OutPutId = SCOPE_IDENTITY()
						--生成出库单明细表
						INSERT INTO [LES].[TT_WMM_OUTPUT_DETAIL]
						(
							[OUTPUT_ID], 
							[PLANT],
							[SUPPLIER_NUM],
							[WM_NO],
							[ZONE_NO],
							[DLOC],
							[TRAN_NO],
							[TARGET_WM],
							[TARGET_ZONE],
							[TARGET_DLOC],
							[PART_NO],
							[PART_CNAME],
							[PACK_COUNT],
							[REQUIRED_BOX_NUM],
							[REQUIRED_QTY],
							[ACTUAL_BOX_NUM],
							[ACTUAL_QTY],
							[PACKAGE_MODEL],
							[PACKAGE],
							[NUM],
							[BOX_NUM],
							[PART_ENAME],
							[CREATE_DATE],
							[CREATE_USER]
						)
						SELECT TOP 1
							@OutPutId,
							[PLANT],
							[ZONE_NO],
							[WM_NO],
							[ZONE_NO],
							[DLOC],
							@BILLNO,
							@TargetWM,
							@TargetZone,
							(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @TargetWM AND [ZONE_NO] = @TargetZone AND [PART_NO] = @PARTNO) AS [TARGET_DLOC],
							@PARTNO,
							[PART_CNAME],
							[PACKAGE],
							@BOXNUM,
							@NUM,
							NULL,
							NULL,
							[PACKAGE_MODEL],
							[PACKAGE],
							@NUM,
							@BOXNUM,
							[PART_ENAME],
							GETDATE(),
							@LoginName
						FROM [LES].[TM_BAS_PARTS_STOCK] WITH (NOLOCK)
						WHERE [PLANT] = @PLANT AND [WM_NO] = @SourceWM AND [ZONE_NO] = @SourceZone AND [PART_NO] = @PARTNO

						IF @ISDYNAMICDLOC = 1 AND @ISOUTPUTSOLE = 1
							BEGIN
								--生成车载上架任务
								EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', 70, @PARTNO, @BOXNUM, @NUM, @TrayNo, @PLANT, @SourceWM, @SourceZone, @SOURCEDLOC, @TargetWM, @TargetZone, '', @LoginName
							END
						ELSE
							BEGIN
								--生成车载移库任务
								EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', 30, @PARTNO, @BOXNUM, @NUM, @TrayNo, @PLANT, @SourceWM, @SourceZone, @SOURCEDLOC, @TargetWM, @TargetZone, '', @LoginName
							END

						--写入日志表
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
							@TRAYINFOID,
							@TrayNo,
							@PLANT,
							@SourceWM,
							@SourceZone,
							@SOURCEDLOC,
							@TargetWM,
							@TargetZone,
							'',
							@BILLNO,
							2,
							@PARTNO,
							@PARTCNAME,
							@NUM,
							@BOXNUM,
							@BATCHNO,
							0,
							NULL,
							1,
							@LoginName,
							GETDATE(),
							@LoginName,
							GETDATE()
					END
			END
		ELSE
			BEGIN
				IF @Result = 0
					BEGIN
						--写入日志表
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
							@TRAYINFOID,
							@TrayNo,
							@PLANT,
							@SourceWM,
							@SourceZone,
							'',
							@TargetWM,
							@TargetZone,
							'',
							'',
							1,
							@PARTNO,
							@PARTCNAME,
							@NUM,
							@BOXNUM,
							@BATCHNO,
							0,
							NULL,
							1,
							@LoginName,
							GETDATE(),
							@LoginName,
							GETDATE()
					END
			END

		IF @Result = 0
			COMMIT TRAN
		ELSE
			ROLLBACK TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION

        SET @Result = 1
		SET @ResultMessage = ERROR_MESSAGE()
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS],  [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_SPM_TRAYDETAIL_MOVE]', 'Procedure', @ResultMessage, ERROR_LINE()
    END CATCH
END