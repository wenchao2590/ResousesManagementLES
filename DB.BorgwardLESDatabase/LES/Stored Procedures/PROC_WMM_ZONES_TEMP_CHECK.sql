/********************************************************************/
/*                                                                  */
/*   Project Name:  LES System                          */
/*   Program Name:  [PROC_WMM_ZONES_TEMP_CHECK]                       */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       andy	2015-06-09   				       */
/********************************************************************/
CREATE PROC [LES].[PROC_WMM_ZONES_TEMP_CHECK]
AS
	BEGIN TRANSACTION;

	
	BEGIN TRY

	UPDATE [LES].TE_WMM_ZONES_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '工厂不正确;'
	WHERE [PLANT] NOT IN (SELECT [PLANT] FROM [LES].[TM_BAS_PLANT])

	UPDATE [LES].TE_WMM_ZONES_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '仓库不正确;'
	WHERE [WM_NO] NOT IN (SELECT [WAREHOUSE] FROM [LES].[TM_BAS_WAREHOUSE])
	--工厂与仓库联动关系验证
	UPDATE [LES].TE_WMM_ZONES_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '工厂与仓库联动关系不正确;'
	WHERE ID NOT IN 
	(SELECT TE.ID FROM [LES].[TM_BAS_WAREHOUSE] TM INNER JOIN [LES].TE_WMM_ZONES_TEMP TE 
	ON TM.PLANT=TE.PLANT AND TM.WAREHOUSE=TE.WM_NO)

	UPDATE [LES].TE_WMM_ZONES_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '存储区编码不正确;'
	WHERE [ZONE_NO] IS NULL OR LEN([ZONE_NO]) < 1 

	UPDATE [LES].TE_WMM_ZONES_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '存储区类型不正确;'
	WHERE IS_MANAGE NOT IN (SELECT DETAIL_CODE FROM [LES].[TC_SYS_CODE_DETAIL] WHERE [CODE_NAME] = 'wms_is_manage')

	UPDATE [LES].TE_WMM_ZONES_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '翻包存储区编号不属于当前工厂下的仓库;'
	WHERE (REPACKAGE_ZONE <> NULL OR REPACKAGE_ZONE <> '' ) AND REPACKAGE_ZONE NOT IN 
	(SELECT REPACKAGE_NO FROM [LES].[TT_WMM_REPACKAGE_HEAD] TT 
	INNER JOIN [LES].TE_WMM_ZONES_TEMP TE ON TT.PLANT=TE.PLANT AND TT.WM_NO=TE.WM_NO 
	AND TT.REPACKAGE_NO=TE.REPACKAGE_ZONE)


	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber
			,ERROR_SEVERITY() AS ErrorSeverity
			,ERROR_STATE() AS ErrorState
			,ERROR_PROCEDURE() AS ErrorProcedure
			,ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;

		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH;

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;