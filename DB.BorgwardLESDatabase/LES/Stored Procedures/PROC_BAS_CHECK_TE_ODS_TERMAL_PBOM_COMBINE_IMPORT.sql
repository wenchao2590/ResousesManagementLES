
/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  PROC_BAS_CHECK_TE_ODS_PBOM                       */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/
create PROC [LES].[PROC_BAS_CHECK_TE_ODS_TERMAL_PBOM_COMBINE_IMPORT]
(@batch_no nvarchar(20)
)
AS
	BEGIN TRANSACTION;

	
	BEGIN TRY

	UPDATE [LES].TE_ODS_TERMAL_PBOM_COMBINE_IMPORT
	SET VALID_FLAG = 0,
	ERROR_MSG = ERROR_MSG + '主键重复;'
	WHERE 
	BATCH_NO =@batch_no  AND
	LOGICAL_PK IN(
	SELECT LOGICAL_PK  FROM [LES].TE_ODS_TERMAL_PBOM_COMBINE_IMPORT WHERE BATCH_NO =@batch_no  GROUP BY LOGICAL_PK HAVING COUNT(1) > 1)

	UPDATE [LES].TE_ODS_TERMAL_PBOM_COMBINE_IMPORT
	SET VALID_FLAG = 0,
	ERROR_MSG = ERROR_MSG + '不是LES物流零件号;'
	WHERE 
	BATCH_NO =@batch_no  AND
	LOGICAL_PK IN(
	SELECT LOGICAL_PK  FROM [LES].TE_ODS_TERMAL_PBOM_COMBINE_IMPORT WHERE  CHARINDEX('LES',PART_NO) = 0)

	UPDATE [LES].TE_ODS_TERMAL_PBOM_COMBINE_IMPORT
	SET VALID_FLAG = 0
	WHERE 
	BATCH_NO <> @batch_no AND
	LOGICAL_PK IN(
	SELECT LOGICAL_PK FROM [LES].TE_ODS_TERMAL_PBOM_COMBINE_IMPORT WHERE BATCH_NO = @batch_no  AND VALID_FLAG IS NULL)

	UPDATE [LES].TE_ODS_TERMAL_PBOM_COMBINE_IMPORT
	SET VALID_FLAG = 1
	WHERE 
	BATCH_NO =  @batch_no AND VALID_FLAG IS NULL


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