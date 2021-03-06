﻿

-- =============================================
-- Author:		Scott
-- Create date: 2017/9/20
-- Description:	VMI收货入库后，记录明细到VMI交易表
-- =============================================
CREATE PROCEDURE LES.PROC_SPM_VMI_RECEIVE_TRAN_LOG
	@RECEIVE_NO NVARCHAR(50),
	@LOGIN_USERNAME NVARCHAR(50)
AS
BEGIN
	DECLARE @RECEIVE_ID INT;
	DECLARE @MODIFICATION_CODE NVARCHAR(50)=N'VMIInput'

	SELECT @RECEIVE_ID=[RECEIVE_ID] FROM [LES].[TT_WMM_VMI_RECEIVE] WITH(NOLOCK)
	WHERE [RECEIVE_NO]=@RECEIVE_NO

	INSERT INTO [LES].[TT_SPM_VMI_TRAN_DETAIL]
	(
		[BILL_NO],
		[PLANT],
		[WM_NO],
		[ZONE_NO],
		[D_PLANT],
		[D_WM_NO],
		[D_ZONE_NO],
		[SUPPLIER_NUM],
		[PART_NO],
		[PART_CNAME],
		[PACKAGE_MODEL],
		[DLOC],
		[D_DLOC],
		[PACKAGE],
		[NUM],
		[BOX_NUM],
		[MODIFICATION_CODE],
		[CREATE_USER],
		[CREATE_DATE]
	)
	SELECT 
		@RECEIVE_NO,
		TW.PLANT,
		TW.WM_NO,
		TW.ZONE_NO,
		TW.PLANT,
		TW.WM_NO,
		TW.ZONE_NO,
		TW.SUPPLIER_NUM,
		TW.PART_NO,
		TW.PART_CNAME,
		TW.PACKAGE_MODEL,
		TW.DLOC,
		TW.DLOC,
		TW.PACKAGE,
		ISNULL(TW.Current_QTY,0),
		ISNULL(TW.Current_BOX_NUM,0),
		@MODIFICATION_CODE,
		@LOGIN_USERNAME,
		GETDATE()
	FROM [LES].[TT_WMM_VMI_RECEIVE_DETAIL] AS TW WITH(NOLOCK)
	WHERE [RECEIVE_ID]=@RECEIVE_ID
END