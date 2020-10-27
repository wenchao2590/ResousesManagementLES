﻿CREATE PROC [LES].[PROC_LargeScreen_PackUp](@WarningSecond int)
AS
BEGIN
	SELECT 
	RIGHT(REPACKAGE_NO,7)  AS REPACKAGE_NO,
	[REPACKAGE_ROUTE] AS [ROUTE],
	[REPACKAGE_TIME],
	DATEDIFF(mi,REPACKAGE_TIME,GETDATE()) AS DIFF 
	 FROM les.TT_WMM_REPACKAGE_HEAD 
	 WHERE ISNULL(COUNT_STATUS,0)!=2
	 AND DATEDIFF(ss,REPACKAGE_TIME,GETDATE())>=@WarningSecond
	 AND CREATE_DATE>='2016-12-16'
END;