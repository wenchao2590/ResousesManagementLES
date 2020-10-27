-- =============================================
-- Author:		PENG-Daniel.HOU
-- Create date: 2016/8/5
-- Description:	重新生成JIS拉动单对应的出库单
-- 1. 删除出库单子表对应单号的数据
-- 2. 重新生成数据出库单子表数据
-- =============================================
CREATE PROCEDURE [dbo].[PROC_TMP_RECREATE_WMMOUTPUT_FROM_JISRUNSHEETNO]
	-- Add the parameters for the stored procedure here
	@JIS_RUNSHEET_SN INT,
	@OUTPUT_ID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	if (@OUTPUT_ID is null or @JIS_RUNSHEET_SN is null)
	begin
		return
	end

	delete from [LES].[TT_WMM_OUTPUT_DETAIL] where [OUTPUT_ID]=@OUTPUT_ID
	EXEC [dbo].[PROC_TMP_INSERT_OUTPUT] @RunsheetSn=@JIS_RUNSHEET_SN,@OutPutId=@OUTPUT_ID


END