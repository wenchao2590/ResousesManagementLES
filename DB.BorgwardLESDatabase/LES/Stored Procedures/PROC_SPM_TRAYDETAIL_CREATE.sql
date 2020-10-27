/************************************************/
/* Author:		孙述霄							*/
/* Create date: 2017-08-07						*/
/* Description:	组托处理						*/
/************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_TRAYDETAIL_CREATE]
(
    @TrayNo NVARCHAR(20),
	@BillNo NVARCHAR(50),
	@Dloc NVARCHAR(32),
	@WmNo NVARCHAR(32),
	@ZoneNo NVARCHAR(32),
	@xml XML,
	@LoginName NVARCHAR(50),
    @result INT OUTPUT,
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
		DECLARE @TARGETWM NVARCHAR(10)
		DECLARE @TARGETZONE NVARCHAR(32)
		DECLARE @TARGETDLOC NVARCHAR(32)
		DECLARE @PARTNO NVARCHAR(20)
		DECLARE @PARTCNAME NVARCHAR(100)
		DECLARE @OVERFLOWDLOC NVARCHAR(32)
		DECLARE @TRAYINFOID BIGINT
		DECLARE @DLOCSTATUS INT
		DECLARE @ISDYNAMICDLOC INT
		DECLARE @ISOUTPUTSOLE INT
		DECLARE @ISCREATETASK INT
		DECLARE @ISADD INT
        EXEC sp_xml_preparedocument @idoc OUTPUT, @xml

		SET @Result = 0
		SET @ResultMessage = ''

		SET @PLANT = ''
		SET @TARGETWM = ''
		SET @TARGETZONE = ''
		SET @TARGETDLOC = ''
		SET @BATCHNO = ''
		SET @OVERFLOWDLOC = ''
		SET @DLOCSTATUS = 0
		SET @ISDYNAMICDLOC = 0
		SET @ISOUTPUTSOLE = 0
		SET @ISCREATETASK = 0
		IF ISNULL(@BillNo, '') <> ''
			BEGIN
				SET @WmNo = ''
				SET @ZoneNo = ''
				SELECT @PLANT = MAX([PLANT]), @WmNo = MAX([WM_NO]), @ZoneNo = MAX([ZONE_NO]), @TARGETWM = MAX([TARGET_WM]), @TARGETZONE = MAX([TARGET_ZONE]), @TARGETDLOC = MAX([TARGET_DLOC]) FROM [LES].[TT_WMM_RECEIVE_DETAIL] WITH (NOLOCK) WHERE [RECEIVE_ID] IN (SELECT [RECEIVE_ID] FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK) WHERE [RECEIVE_NO] = @BillNo)
				SELECT @OVERFLOWDLOC = [OVERFLOW_DLOC] FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo
			END
		ELSE
			BEGIN
				IF @WmNo <> '' AND @ZoneNo <> ''
					BEGIN
						SELECT @PLANT = [PLANT], @OVERFLOWDLOC = [OVERFLOW_DLOC], @ISDYNAMICDLOC=[IS_DYNAMIC_DLOC], @ISOUTPUTSOLE=[IS_OUTPUT_SOLE] FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK) WHERE [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo
					END
			END

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
			@WmNo AS [WM_NO],
			@ZoneNo AS [ZONE_NO],
			@Dloc AS [DLOC],
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

		IF ISNULL(@BillNo, '') = ''
			BEGIN
				SELECT @BATCHNO = MIN(ISNULL([BATTH_NO],'')) FROM [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] WITH (NOLOCK) WHERE [BARCODE_DETAIL_ID] IN (SELECT [BAR_ID] FROM [LES].[TT_SPM_TRAY_DETAIL] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1) AND ISNULL([BATTH_NO], '') <> ''
				SET @BATCHNO = ISNULL(@BATCHNO, '')
				--收货上架批次号
				IF @WmNo <> '' AND @ZoneNo <> ''
					BEGIN
						SET @BATCHNO = REPLACE(REPLACE(REPLACE(CONVERT(NVARCHAR(20), GETDATE(), 120), '-', ''), ':', ''), ' ', '')
					END
			END
		UPDATE [LES].[TT_SPM_TRAY_DETAIL] WITH (ROWLOCK) SET [BATCH_NO] = @BATCHNO WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1
		UPDATE [LES].[TT_SPM_TRAY_INFO] WITH (ROWLOCK) SET [TRAY_STATUS] = 2, [NUM] = @NUM, [BOX_NUM] = @BOXNUM, [BIND_TIME] = GETDATE(), [BILL_NO] = @BillNo, [PART_NO] = @PARTNO, [PART_CNAME] = @PARTCNAME, [PLANT] = @PLANT, [WM_NO] = @WmNo, [ZONE_NO] = @ZoneNo, [DLOC] = @Dloc, [BATCH_NO] = @BATCHNO  WHERE [TRAY_NO] = @TrayNo

		IF ISNULL(@BillNo, '') <> '' AND ISNULL(@Dloc, '') <> ''
			BEGIN
				IF @OVERFLOWDLOC <> @Dloc
					BEGIN
						SELECT
							@DLOCSTATUS = [DLOC_STATUS]
						FROM [LES].[TR_BAS_PART_TRAY_STOCK] WITH (NOLOCK)
						WHERE [PLANT] = @PLANT AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc AND ([PART_NO] = @PARTNO OR ISNULL([PART_NO], '') = '')

						SET @DLOCSTATUS = ISNULL(@DLOCSTATUS, 0)
					END

				IF @DLOCSTATUS = 2
					BEGIN
						SET @Result=1
						SET @ResultMessage='动态库位[' + @Dloc + ']已经被占用！'
					END

				IF @Result = 0
					BEGIN
						SET @ISADD = 0
						IF @OVERFLOWDLOC = @Dloc
							BEGIN
								SET @ISADD = 1
							END
						ELSE
							BEGIN
								IF NOT EXISTS (SELECT 1 FROM [LES].[TR_BAS_PART_TRAY_STOCK] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc)
									BEGIN
										SET @ISADD = 1
									END
							END
						IF @ISADD = 1
							BEGIN
								INSERT INTO [LES].[TR_BAS_PART_TRAY_STOCK]
								(
									[PLANT],
									[WM_NO],
									[ZONE_NO],
									[DLOC],
									[PART_NO],
									[TRAY_NO],
									[BATCH_NO],
									[NUM],
									[BOX_NUM],
									[DLOC_STATUS],
									[VALID_FLAG],
									[CREATE_USER],
									[CREATE_DATE],
									[MODIFY_USER],
									[MODIFY_DATE]
								)
								SELECT
									@PLANT,
									@WmNo,
									@ZoneNo,
									@Dloc,
									@PARTNO,
									@TrayNo,
									@BATCHNO,
									@NUM,
									@BOXNUM,
									1,
									1,
									@LoginName,
									GETDATE(),
									@LoginName,
									GETDATE()
							END
						ELSE
							BEGIN
								UPDATE [LES].[TR_BAS_PART_TRAY_STOCK] WITH (ROWLOCK)
								SET [PART_NO] = @PARTNO,
									[TRAY_NO] = @TrayNo,
									[BATCH_NO] = @BATCHNO,
									[NUM] = @NUM,
									[BOX_NUM] = @BOXNUM,
									[DLOC_STATUS] = 1,
									[VALID_FLAG] = 1,
									[MODIFY_USER] = @LoginName,
									[MODIFY_DATE] = GETDATE()
								WHERE [PLANT] = @PLANT AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc
							END
					END
			END
		ELSE
			BEGIN
				IF @WmNo <> '' AND @ZoneNo <> ''
					BEGIN
						--更新箱标签批次号
						UPDATE [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] WITH (ROWLOCK)
						SET [WM_NO] = @WmNo,
							[ZONE_NO] = @ZoneNo,
							[BATTH_NO] = @BATCHNO,
							[IN_BATCH_NO] = @BATCHNO,
							[BARCODE_STATUS] = 1
						WHERE [BARCODE_DATA] IN (SELECT [BARCODE_DATA] FROM [LES].[TT_SPM_TRAY_DETAIL] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1)

						SELECT @ISCREATETASK=IS_CREATE_TASK FROM [LES].[TM_BAS_PARTS_STOCK] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [PART_NO] = @PARTNO
						IF @ISCREATETASK = 1 AND @ISDYNAMICDLOC = 1
							BEGIN
								IF @ISOUTPUTSOLE = 1
									BEGIN
										--生成车载上架任务
										EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', 10, @PARTNO, @BOXNUM, @NUM, @TrayNo, @PLANT, '', '', '', @WmNo, @ZoneNo, '', @LoginName
									END
								ELSE
									BEGIN
										--生成车载入库任务
										EXEC [LES].[PROC_CREATE_ONBOARD_TASK] '', 50, @PARTNO, @BOXNUM, @NUM, @TrayNo, @PLANT, '', '', '', @WmNo, @ZoneNo, '', @LoginName
									END
							END
					END
			END

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
					@WmNo,
					@ZoneNo,
					@Dloc,
					@TARGETWM,
					@TARGETZONE,
					@TARGETDLOC,
					@BillNo,
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
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_SPM_TRAYDETAIL_CREATE]', 'Procedure', @ResultMessage, ERROR_LINE()
    END CATCH
END