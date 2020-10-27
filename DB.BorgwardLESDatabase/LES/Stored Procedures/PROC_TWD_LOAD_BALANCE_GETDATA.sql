
-- =============================================
-- Author:		<Yuan,,Shian>
-- Create date: <2014-10-30,,>
-- Description:	<零件配载优化,,>
-- =============================================
CREATE PROCEDURE [LES].[PROC_TWD_LOAD_BALANCE_GETDATA]
AS
BEGIN
	DECLARE @NOW	DATETIME,
			@DAY	INT

	SET @NOW = GETDATE()

	-- 取出配置值，该值指明需要用到供应商多少天的窗口时间
	SELECT @DAY = [PARAMETER1] FROM [LES].[TM_SYS_APPLICATION_CONFIGURATION] WHERE [APPLICATION]='TWD_LOAD_BALANCE_DAY_VALUE'
	
	--找到所有时间小于当前时间未处理的窗口时间、按照供应商分组
	SELECT PLANT, ASSEMBLY_LINE, SUPPLIER_NUM, BOX_PARTS, SEND_TIME, WINDOW_TIME
	FROM [LES].[TT_TWD_SUPPLIER_SENDTIME]
	WHERE SEND_TIME > @NOW										-- 取发送时间比现在大的
		AND DATEDIFF(DAY, @NOW, SEND_TIME) <= @DAY					-- 取发送时间比现在晚指定配置时间的
		AND (SEND_TIME_STATUS IS NULL OR SEND_TIME_STATUS = 0 )		-- 取没有生成过拉动单的窗口时间
	ORDER BY SEND_TIME											-- 按送货窗口时间升序排序

END