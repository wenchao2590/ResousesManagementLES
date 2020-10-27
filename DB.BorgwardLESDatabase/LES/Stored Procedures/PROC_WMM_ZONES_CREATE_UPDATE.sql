/********************************************************************/
/*                                                                  */
/*   Project Name:  LES System                         */
/*   Program Name:  [PROC_WMM_ZONES_CREATE_UPDATE]             */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       andy	2015-06-09     				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_WMM_ZONES_CREATE_UPDATE]
 
AS

BEGIN

INSERT INTO [LES].[TM_WMM_ZONES]
	(
	[PLANT]
	,[WM_NO]
	,[ZONE_NO]
	,[ZONE_NAME]
	,[IS_MANAGE]
	,[IS_IM]
	,[IM_LOCATION]
	,[STOCK_PLACE_NO]
	,[IS_MIX]
	,[IS_NEGATIVE]
	,[IS_STOCK_CHECK]
	,[STRATEGY]
	,[REPACKAGE_ZONE]
	,[COMMENTS]
	,[CREATE_USER]
	,[CREATE_DATE]
	,[UPDATE_USER]
	,[UPDATE_DATE]
	,IS_DYNAMIC_DLOC
	,IS_OUTPUT_SOLE
	,OVERFLOW_DLOC
	)
	SELECT distinct 
		[PLANT]
		,[WM_NO]
		,[ZONE_NO]
		,[ZONE_NAME]
		,[IS_MANAGE]
		,[IS_IM]
		,[IM_LOCATION]
		,[STOCK_PLACE_NO]
		,[IS_MIX]
		,[IS_NEGATIVE]
		,[IS_STOCK_CHECK]
		,[STRATEGY]
		,TE.[REPACKAGE_ZONE]
		,TE.[COMMENTS]
        ,TE.[CREATE_USER]
        ,TE.[CREATE_DATE]
        ,TE.[UPDATE_USER]
        ,TE.[UPDATE_DATE]
        ,TE.IS_DYNAMIC_DLOC
        ,TE.IS_OUTPUT_SOLE
        ,TE.OVERFLOW_DLOC
	FROM 
		[LES].[TE_WMM_ZONES_TEMP] TE
	WHERE 
		TE.VALID_FLAG = 1
		AND TE.ZONE_NO NOT IN (SELECT ZONE_NO FROM [LES].[TM_WMM_ZONES])
	
UPDATE 
	[LES].[TM_WMM_ZONES] 
SET
	--[PLANT] = TE.[PLANT]  在修改时 工厂与仓库不允许修改
	--,[WM_NO] = TE.[WM_NO],
	[ZONE_NO] = TE.[ZONE_NO]
	,[ZONE_NAME] = TE.[ZONE_NAME]
	,[IS_MANAGE] = TE.[IS_MANAGE]
	,[IS_IM] = TE.[IS_IM]
	,[IM_LOCATION] = TE.[IM_LOCATION]
	,[STOCK_PLACE_NO] = TE.[STOCK_PLACE_NO]
	,[IS_MIX] = TE.[IS_MIX]
	,[IS_NEGATIVE] = TE.[IS_NEGATIVE]
	,[IS_STOCK_CHECK] = TE.[IS_STOCK_CHECK]
	,[STRATEGY] = TE.[STRATEGY]
	,[COMMENTS] = TE.[COMMENTS]
	,[REPACKAGE_ZONE]=TE.[REPACKAGE_ZONE]
    ,[UPDATE_USER] = TE.[CREATE_USER]
    ,[UPDATE_DATE] = TE.[CREATE_DATE]
	,IS_DYNAMIC_DLOC=TE.IS_DYNAMIC_DLOC
    ,IS_OUTPUT_SOLE = TE.IS_OUTPUT_SOLE
    ,OVERFLOW_DLOC = TE.OVERFLOW_DLOC
FROM 
	[LES].[TE_WMM_ZONES_TEMP] TE,
	[LES].[TM_WMM_ZONES] TT
WHERE 
	TE.VALID_FLAG = 1 
	AND TE.ZONE_NO = TT.ZONE_NO


END