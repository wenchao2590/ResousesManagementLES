
-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	发送mail
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_INSERT_SEND_MAIL_LIST]
(
	@SupplierNum   nvarchar(12),
	@PkNo		   nvarchar(50),
	@ModuleName    nvarchar(100),
	@Is3PLSupplier int
)
AS
BEGIN
	declare @SysId							 int
	declare @Plant							 nvarchar(5)
	declare @AssemblyLine					 nvarchar(10)
	declare @SupplierName					 nvarchar(200)
	declare @AlarmName						 nvarchar(200)
	declare @AlarmSubject					 nvarchar(200)
	declare @MailBodyMsg					 nvarchar(200)
	declare @MsgCreateUser					 nvarchar(200)
	/*
	IF(@SupplierNum <> '')
	BEGIN
		SELECT @SupplierName = SUPPLIER_NAME FROM LES.TM_BAS_SUPPLIER WHERE SUPPLIER_NUM=@SupplierNum
	END
	
	IF(@Is3PLSupplier = 0)
		SET @MsgCreateUser='SVW.LES.CommonSupplierClient'
	ELSE IF(@Is3PLSupplier = 1)
		SET @MsgCreateUser='SVW.LES.3PLSupplierPrintClient'
		
	IF(@ModuleName = 'JIS' OR @ModuleName='TWD' OR @ModuleName='PCS')
	BEGIN
		SET @SysId = 1006
		SELECT TOP 1 @Plant=PLANT,@AssemblyLine=ASSEMBLY_LINE FROM LES.TT_JIS_RUNSHEET WHERE JIS_RUNSHEET_NO=@PkNo
		SET @MailBodyMsg = 'From: ATLES,供应商:' + @SupplierNum + '(' + @SupplierName + '),' + @ModuleName +'拉动单[编号:' + @PkNo + ']打印失败。'
	END
	ELSE IF(@ModuleName = 'JIS_PREVIEW_DATA')
	BEGIN
		SET @SysId = 1003
		SELECT TOP 1 @Plant=PLANT,@AssemblyLine=ASSEMBLY_LINE FROM LES.TT_JIS_PREVIEW_DATA WHERE PREVIEW_DATA_SN=@PkNo
		SET @MailBodyMsg = 'From: ATLES,供应商:' + @SupplierNum + '(' + @SupplierName + '),' + @ModuleName +'JIS预览信息[序号:' + @PkNo + ']发送失败。'
	END
	ELSE IF(@ModuleName = 'PCS_MATERIAL_REQUEST')
	BEGIN
		SET @SysId = 1004
		SELECT TOP 1 @Plant=PLANT,@AssemblyLine=ASSEMBLY_LINE FROM LES.TI_PCS_MATERIAL_REQUESTS WHERE INTERFACE_ID=@PkNo
		SET @MailBodyMsg = 'From: ATLES,供应商:' + @SupplierNum + '(' + @SupplierName + '),' + @ModuleName +'PCS拉动指令[序号:' + @PkNo + ']发送失败。'
	END
	
	SELECT @AlarmName=ALARM_NAME,@AlarmSubject=ALARM_SUBJECT FROM LES.TM_SYS_ALARM_MAIL_CONFIG WHERE SYS_ID=@SysId
	if(@SysId is not null)
	begin
		INSERT INTO LES.TT_SYS_MAIL_SEND_LIST
		SELECT @Plant,NULL,@AssemblyLine,NULL,@SysId,@AlarmName,@AlarmSubject,@MailBodyMsg,NULL,NULL,0,NULL,@MsgCreateUser,GETDATE(),NULL,NULL
	end
	*/
END