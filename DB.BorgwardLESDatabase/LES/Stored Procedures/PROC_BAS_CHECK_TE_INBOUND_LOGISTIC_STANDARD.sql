﻿

/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  PROC_BAS_CHECK_TE_INBOUND_LOGISTIC_STANDARD                          */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/

CREATE PROC [LES].[PROC_BAS_CHECK_TE_INBOUND_LOGISTIC_STANDARD]
(@batch_no nvarchar(20)
)
AS
	BEGIN TRANSACTION;

	
	BEGIN TRY

	UPDATE [LES].TE_BAS_TEMP_INBOUND_LOGISTIC_STANDARD
	SET VALID_FLAG = 0,
	ERROR_MSG = ERROR_MSG + '主键重复;'
	WHERE 
	BATCH_NO =@batch_no  AND
	LOGICAL_PK IN(
	SELECT LOGICAL_PK  FROM [LES].TE_BAS_TEMP_INBOUND_LOGISTIC_STANDARD WHERE BATCH_NO =@batch_no  GROUP BY LOGICAL_PK HAVING COUNT(1) > 1)

	UPDATE [LES].[TE_BAS_TEMP_INBOUND_LOGISTIC_STANDARD]
	SET VALID_FLAG = 0
	WHERE 
	BATCH_NO <> @batch_no AND
	LOGICAL_PK IN(
	SELECT LOGICAL_PK FROM [LES].[TE_BAS_TEMP_INBOUND_LOGISTIC_STANDARD] WHERE BATCH_NO = @batch_no  AND VALID_FLAG IS NULL)

	UPDATE [LES].[TE_BAS_TEMP_INBOUND_LOGISTIC_STANDARD]
	SET VALID_FLAG = 0
	WHERE 
	BATCH_NO =  @batch_no AND ERROR_MSG <> ''
		
	--UPDATE [LES].[TE_BAS_TEMP_INBOUND_LOGISTIC_STANDARD]
	--SET VALID_FLAG = 0,
	--ERROR_MSG = ERROR_MSG + '入厂方式只允许TWD'
	--WHERE  INBOUND_SYSTEM_MODE <> '' AND INBOUND_SYSTEM_MODE NOT IN( 'TWD') 
	
 --  	UPDATE [LES].[TE_BAS_TEMP_INBOUND_LOGISTIC_STANDARD]
	--SET VALID_FLAG = 0,
	--ERROR_MSG = ERROR_MSG + '入厂TWD零件类为空或不存在;'
	--WHERE 	LOGICAL_PK IN(
	--SELECT LOGICAL_PK FROM [LES].[TE_BAS_TEMP_INBOUND_LOGISTIC_STANDARD] AS INBOUND
 --                        LEFT JOIN LES.TM_TWD_BOX_PARTS AS BOX ON 
 --                                   INBOUND.INBOUND_PART_CLASS = BOX.BOX_PARTS 
 --                               AND INBOUND.PLANT = BOX.PLANT 
 --                               AND INBOUND.ASSEMBLY_LINE = BOX.ASSEMBLY_LINE
	--WHERE  INBOUND_SYSTEM_MODE = 'TWD' AND BOX.BOX_PARTS IS NULL)  -- 有TWD标记但在TWD零件类中未找到	
	
	 

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