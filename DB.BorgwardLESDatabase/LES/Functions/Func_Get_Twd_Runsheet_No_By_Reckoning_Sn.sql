
-- =============================================
-- Author:		yinxuefeng
-- Create date: 2013-07-09
-- Description:	获取拉动单号,用于取
-- =============================================
CREATE FUNCTION [LES].[Func_Get_Twd_Runsheet_No_By_Reckoning_Sn]
(
	@RECKONING_SN INT--结算单SN
)
RETURNS NVARCHAR(22)
AS
BEGIN
	DECLARE @TWD_RUNSHEET_NO NVARCHAR(22)

	SELECT TOP 1 @TWD_RUNSHEET_NO = TWD_RUNSHEET_NO FROM LES.TT_TWD_RUNSHEET_PACKAGE_DETAIL
	WHERE RECKONING_SN=@RECKONING_SN
	IF(@TWD_RUNSHEET_NO IS NULL)
		SET @TWD_RUNSHEET_NO=''
	RETURN @TWD_RUNSHEET_NO
END