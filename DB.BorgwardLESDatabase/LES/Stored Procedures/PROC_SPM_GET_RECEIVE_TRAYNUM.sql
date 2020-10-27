/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_SPM_GET_RECEIVE_TRAYNUM]						 */
/*   Called By:     by the PDA													 */
/*   Purpose:       获取入库托数												 */
/*   author:        孙述霄	2017-09-12   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_GET_RECEIVE_TRAYNUM]
(
    @BillNo NVARCHAR(50),					--单据号
	@ReceivableNum INT OUTPUT,				--应收托数
	@ReceivedNum INT OUTPUT					--已收托数
)
AS
BEGIN
	SELECT
		@ReceivableNum = COUNT(1)
	FROM [LES].[TT_SPM_TRAY_INFO] WITH (NOLOCK)
	WHERE [BILL_NO] = @BillNo

	SELECT
		@ReceivedNum = COUNT(1)
	FROM [LES].[TT_SPM_TRAY_INFO] WITH (NOLOCK)
	WHERE [BILL_NO] = @BillNo AND [TRAY_STATUS] = 5

	SET @ReceivableNum = ISNULL(@ReceivableNum, 0)
	SET @ReceivedNum = ISNULL(@ReceivedNum, 0)
END