/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_WMM_RECEIVE_RESET_TRAY]							 */
/*   Called By:     by the PDA													 */
/*   Purpose:       重置PDA扫托收货信息											 */
/*   author:        孙述霄	2017-08-18   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_WMM_RECEIVE_RESET_TRAY]
(
    @ReceiveNo NVARCHAR(50),				--收货单号
	@LoginName NVARCHAR(50),				--处理人
	@result INT OUTPUT,
	@ResultMessage NVARCHAR(4000) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

		SET @Result = 0
		SET @ResultMessage = ''

		IF EXISTS(SELECT 1 FROM [LES].[TT_SPM_TRAY_INFO] WITH (NOLOCK) WHERE [BILL_NO] = @ReceiveNo AND [TRAY_STATUS] = 5)
			BEGIN
				UPDATE A
				SET A.[Current_QTY] = 0,
					A.[Current_BOX_NUM] = 0,
					A.[UPDATE_DATE] = GETDATE(),
					A.[UPDATE_USER] = @LoginName
				FROM [LES].[TT_WMM_RECEIVE_DETAIL] A WITH (ROWLOCK)
				INNER JOIN [LES].[TT_WMM_RECEIVE] B WITH (NOLOCK) ON A.[RECEIVE_ID] = B.[RECEIVE_ID]
				WHERE B.[RECEIVE_NO] = @ReceiveNo

				UPDATE [LES].[TT_SPM_TRAY_INFO] WITH (ROWLOCK) SET [TRAY_STATUS] = 2 WHERE [BILL_NO] = @ReceiveNo AND [TRAY_STATUS] = 5
			END

		COMMIT TRANSACTION
	END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION

        SET @Result = 1
		SET @ResultMessage = ERROR_MESSAGE()
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS],  [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_WMM_RECEIVE_RESET_TRAY]', 'Procedure', @ResultMessage, ERROR_LINE()
    END CATCH
END