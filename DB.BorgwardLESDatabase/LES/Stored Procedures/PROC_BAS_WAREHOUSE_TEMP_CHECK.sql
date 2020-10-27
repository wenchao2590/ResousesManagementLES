
/********************************************************************/
/*                                                                  */
/*   Project Name:  LES System                          */
/*   Program Name:  [PROC_BAS_WAREHOUSE_TEMP_CHECK]                       */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       andy	2015-06-09   				       */
/********************************************************************/
create PROC [LES].[PROC_BAS_WAREHOUSE_TEMP_CHECK]
AS
	BEGIN TRANSACTION;

	
	BEGIN TRY

	UPDATE [LES].TE_BAS_WAREHOUSE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '工厂不正确;'
	WHERE [PLANT] NOT IN (SELECT [PLANT] FROM [LES].[TM_BAS_PLANT])

	UPDATE [LES].TE_BAS_WAREHOUSE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '仓库编码不正确;'
	WHERE [WAREHOUSE] IS NULL OR LEN([WAREHOUSE]) < 1 

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