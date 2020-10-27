/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_SPM_TRAYDETAIL_UNBIND]							 */
/*   Called By:     by the PDA													 */
/*   Purpose:       拆托处理													 */
/*   author:        孙述霄	2017-08-08   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_TRAYDETAIL_UNBIND]
(
    @TrayNo NVARCHAR(20),					--托号
	@LoginName NVARCHAR(50),				--处理人
	@Result INT OUTPUT,						--执行结果，0表示成功，-1表示失败
	@ResultMessage NVARCHAR(4000) OUTPUT 	--结果消息
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN

        DECLARE @TrayStatus INT
		DECLARE @TRAYINFOID BIGINT
		DECLARE @PLANT NVARCHAR(8)
		DECLARE @WMNO NVARCHAR(16)
		DECLARE @ZONENO NVARCHAR(32)
		DECLARE @DLOC NVARCHAR(32)
		DECLARE @TARGETWM NVARCHAR(16)
		DECLARE @TARGETZONE NVARCHAR(32)
		DECLARE @TARGETDLOC NVARCHAR(32)
		DECLARE @BILLNO NVARCHAR(64)
		DECLARE @PARTNO NVARCHAR(32)
		DECLARE @PARTCNAME NVARCHAR(128)
		DECLARE @PARTQTY INT
		DECLARE @BOXQTY INT
		DECLARE @BATCHNO NVARCHAR(32)

		SET @Result = 0
		SET @ResultMessage = ''

		IF NOT EXISTS (SELECT 1 FROM [LES].[TM_SPM_TRAY] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo AND [VALID_FLAG] = 1)
			BEGIN
				SET @Result=1
				SET @ResultMessage='扫描的托标签在数据库中不存在,请扫描正确的托标签！'
			END

		IF @Result = 0
			BEGIN
				SELECT @TrayStatus = [TRAY_STATUS] FROM [LES].[TT_SPM_TRAY_INFO] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo AND [VALID_FLAG] = 1
				SET @TrayStatus = ISNULL(@TrayStatus, 0)
				IF @TrayStatus = 0
					BEGIN
						SET @Result=1
						SET @ResultMessage='扫描的托标签在状态表中不存在,请扫描正确的托标签！'
					END

				IF @TrayStatus = 1
					BEGIN
						SET @Result=1
						SET @ResultMessage='扫描的托标签未组托，请确认！'
					END

				IF @TrayStatus = 3
					BEGIN
						SET @Result=1
						SET @ResultMessage='扫描的托标签已报废，请确认！'
					END

				IF @TrayStatus = 4
					BEGIN
						SET @Result=1
						SET @ResultMessage='扫描的托标签已上架，请确认！'
					END
			END

		IF @Result = 0
			BEGIN
				SET @TARGETWM = ''
				SET @TARGETZONE = ''
				SET @TARGETDLOC = ''

				SELECT
					@TRAYINFOID = [ID],
					@PLANT = [PLANT],
					@WMNO = [WM_NO],
					@ZONENO = [ZONE_NO],
					@DLOC = [DLOC],
					@BILLNO = [BILL_NO],
					@PARTNO = [PART_NO],
					@PARTCNAME = [PART_CNAME],
					@PARTQTY = [NUM],
					@BOXQTY = [BOX_NUM],
					@BATCHNO = [BATCH_NO]
				FROM[LES].[TT_SPM_TRAY_INFO] WITH (NOLOCK)
				WHERE [TRAY_NO] = @TrayNo AND [VALID_FLAG] = 1

				UPDATE [LES].[TT_SPM_TRAY_INFO] WITH (ROWLOCK)
				SET [PLANT] = '',
					[WM_NO] = '',
					[ZONE_NO] = '',
					[DLOC] = '',
					[BILL_NO] = '',
					[PART_NO] = '',
					[PART_CNAME] = '',
					[NUM] = NULL,
					[BOX_NUM] = NULL,
					[BIND_TIME] = NULL,
					[TRAY_STATUS] = 0,
					[BATCH_NO] = '',
					[COMMENTS] = '',
					[UPDATE_USER] = @LoginName,
					[UPDATE_DATE] = GETDATE()
				WHERE [TRAY_NO] = @TrayNo AND [VALID_FLAG] = 1

				UPDATE [LES].[TT_SPM_TRAY_DETAIL] WITH (ROWLOCK)
				SET [BIND_FLAG] = 0,
					[UNBIND_TIME] = GETDATE(),
					[UPDATE_USER] = @LoginName,
					[UPDATE_DATE] = GETDATE()
				WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1

				--写入日志
				INSERT INTO [LES].[TT_SPM_TRAY_LOG]
				SELECT
					@TRAYINFOID,
					@TrayNo,
					@PLANT,
					@WMNO,
					@ZONENO,
					@DLOC,
					@TARGETWM,
					@TARGETZONE,
					@TARGETDLOC,
					@BILLNO,
					2,
					@PARTNO,
					@PARTCNAME,
					@PARTQTY,
					@BOXQTY,
					@BATCHNO,
					1,
					GETDATE(),
					1,
					@LoginName,
					GETDATE(),
					@LoginName,
					GETDATE()
			END
        COMMIT TRAN
    END TRY
    BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION

		SET @Result = 1
		SET @ResultMessage = ERROR_MESSAGE()

		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS],  [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_SPM_TRAYDETAIL_UNBIND]', 'Procedure' ,@ResultMessage ,ERROR_LINE()
    END CATCH
END