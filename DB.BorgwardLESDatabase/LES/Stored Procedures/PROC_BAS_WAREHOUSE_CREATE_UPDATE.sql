

/********************************************************************/
/*                                                                  */
/*   Project Name:  LES System                         */
/*   Program Name:  [PROC_BAS_WAREHOUSE_CREATE_UPDATE]             */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       andy	2015-06-09     				       */
/********************************************************************/
create PROCEDURE [LES].[PROC_BAS_WAREHOUSE_CREATE_UPDATE]
 
AS

BEGIN

INSERT INTO [LES].[TM_BAS_WAREHOUSE]
	(
	[WAREHOUSE]
	,[WAREHOUSE_NAME]
	,[PLANT]
	,[COMMENTS]
	,[CREATE_USER]
	,[CREATE_DATE]
	,[UPDATE_USER]
	,[UPDATE_DATE]
	)
	SELECT distinct 
		[WAREHOUSE]
        ,[WAREHOUSE_NAME]
        ,[PLANT]
        ,[COMMENTS]
        ,TE.[CREATE_USER]
        ,TE.[CREATE_DATE]
        ,TE.[UPDATE_USER]
        ,TE.[UPDATE_DATE]
	FROM 
		[LES].[TE_BAS_WAREHOUSE_TEMP] TE
	WHERE 
		TE.VALID_FLAG = 1
		AND TE.WAREHOUSE NOT IN (SELECT WAREHOUSE FROM [LES].[TM_BAS_WAREHOUSE])

UPDATE 
	[LES].[TM_BAS_WAREHOUSE] 
SET
    [WAREHOUSE] = TE.[WAREHOUSE]
    ,[WAREHOUSE_NAME] = TE.[WAREHOUSE_NAME]
    ,[PLANT] = TE.[PLANT]
    ,[COMMENTS] = TE.[COMMENTS]
    ,[UPDATE_USER] = TE.[CREATE_USER]
    ,[UPDATE_DATE] = TE.[CREATE_DATE]
FROM 
	[LES].[TE_BAS_WAREHOUSE_TEMP] TE,
	[LES].[TM_BAS_WAREHOUSE] TT
WHERE 
	TE.VALID_FLAG = 1 
	AND TE.WAREHOUSE = TT.WAREHOUSE


END