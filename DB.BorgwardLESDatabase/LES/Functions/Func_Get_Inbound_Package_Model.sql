
-- ================================================
-- Template generated from Template Explorer using:
-- Create Scalar Function (New Menu).SQL
-- =============================================
-- Author:		yinxuefeng
-- Create date: 2013-07-09
-- Description:	获取零件工位器具号
-- =============================================
CREATE FUNCTION [LES].[Func_Get_Inbound_Package_Model]
(
	@partNo nvarchar(20)--零件号
	,@plant nvarchar(5)--工厂
	,@assemblyLine nvarchar(10)--流水线
	,@boxPart nvarchar(20)--零件类
	,@supplierNum NVARCHAR(12)--供应商
)
RETURNS NVARCHAR(30)
AS
BEGIN
	DECLARE @INBOUND_PACKAGE_MODEL NVARCHAR(30)

	SELECT TOP 1  @INBOUND_PACKAGE_MODEL = INBOUND_PACKAGE_MODEL FROM LES.TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD
	WHERE PLANT=@plant AND ASSEMBLY_LINE=@assemblyLine AND PART_NO=@partNo
	AND SUPPLIER_NUM=@supplierNum
	AND INBOUND_PART_CLASS=@boxPart
	--AND MODEL='60D' 
	IF(@INBOUND_PACKAGE_MODEL IS NULL)
		SET @INBOUND_PACKAGE_MODEL=''
	RETURN @INBOUND_PACKAGE_MODEL

END