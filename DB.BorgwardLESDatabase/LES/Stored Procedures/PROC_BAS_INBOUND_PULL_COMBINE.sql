﻿
/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  PROC_BAS_INBOUND_PULL_COMBINE                */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_BAS_INBOUND_PULL_COMBINE]
 
AS

BEGIN

/*
BEGIN TRY
	
	UPDATE [LES].[TS_SYS_COMBINE_CONTROL] set [ISPASS]=1 where [SEQID]=1
	waitfor delay '00:00:20'
END TRY
BEGIN CATCH
      INSERT  INTO [LES].[TS_SYS_EXCEPTION] (time_stamp,[application],[METHOD],class,exception_message,error_code)
                SELECT  GETDATE(),'INBOUNDCOMBINE','PROC_BAS_INBOUND_PULL_COMBINE','Procedure',ERROR_MESSAGE(),ERROR_LINE()
    RETURN -1
END CATCH   
*/
BEGIN TRY

BEGIN TRAN
if(exists (select [INBOUND_IDENTITY] from  LES.TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT))
begin
	truncate table [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]

	INSERT INTO [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]
			   ([PLANT],[ASSEMBLY_LINE],[PLANT_ZONE],[WORKSHOP]
			   ,[SUPPLIER_NUM],[TRANS_SUPPLIER_NUM],[MODEL],[PART_NO]
			   ,[INDENTIFY_PART_NO],[AMOUNTRATIO],[EXTERIOR_COLOR],[INTERNAL_COLOR]
			   ,[HAND_KEPT_RECORD],[COLOR_CONTROL_PATCH],[VWS]
			   ,[RAND],[SECTION],[PART_OPTION],[PRODUCTION_NUMBER]
			   ,[START_PRODUCTION_DATE],[CANCEL_NUMBER],[END_PRODUCTION_DATE]
			   ,[MODEL_YEAR],[DOSAGE],[MEASURING_UNIT_NO], AMOUNT_FLAG, [DATA_DATE]
			   ,[VORSERIE],[PART_CNAME],[PART_ENAME],[LOAD_FLAG]
			   ,[ZP_FLAG],[REQUIREMENT_FLAG],[LOGICAL_PK],[ASSEMBLY_FLAG]
			   ,[ASSEMBLY_FLAG_RECRUIT],[MARK] ,[PURCHASE_STYLE]
			   ,[VALID_FLAG],[INBOUND_MODE],[INBOUND_LOGISTIC_MODE],[INBOUND_SYSTEM_MODE]
			   ,[EFFECTIVE_STATUS],[START_EFFECTIVE_DATE],[INBOUND_PART_CLASS],[DOCK]
			   ,[INBOUND_PACKAGE_MODEL],[INBOUND_PACKAGE],[IS_SPLIT],[TERMAL_PK]
			   ,[DIFF_FLAG],[COMMENTS]
			   ,[UPDATE_DATE],[UPDATE_USER],[CREATE_DATE],[CREATE_USER])
	           
	SELECT		[PLANT],[ASSEMBLY_LINE],[PLANT_ZONE],[WORKSHOP]
			   ,[SUPPLIER_NUM],[TRANS_SUPPLIER_NUM],[MODEL],[PART_NO]
			   ,[INDENTIFY_PART_NO],[AMOUNTRATIO],[EXTERIOR_COLOR],[INTERNAL_COLOR]
			   ,[HAND_KEPT_RECORD],[COLOR_CONTROL_PATCH],[VWS]
			   ,[RAND],[SECTION],[PART_OPTION],[PRODUCTION_NUMBER]
			   ,[START_PRODUCTION_DATE],[CANCEL_NUMBER],[END_PRODUCTION_DATE]
			   ,[MODEL_YEAR],[DOSAGE],[MEASURING_UNIT_NO], AMOUNT_FLAG, [DATA_DATE]
			   ,[VORSERIE],[PART_CNAME],[PART_ENAME],[LOAD_FLAG]
			   ,[ZP_FLAG],[REQUIREMENT_FLAG],[LOGICAL_PK],[ASSEMBLY_FLAG]
			   ,[ASSEMBLY_FLAG_RECRUIT],[MARK] ,[PURCHASE_STYLE]
			   ,[VALID_FLAG],[INBOUND_MODE],[INBOUND_LOGISTIC_MODE],[INBOUND_SYSTEM_MODE]
			   ,[EFFECTIVE_STATUS],[START_EFFECTIVE_DATE],[INBOUND_PART_CLASS],[DOCK]
			   ,[INBOUND_PACKAGE_MODEL],[INBOUND_PACKAGE],[IS_SPLIT],[TERMAL_PK]
			   ,[DIFF_FLAG],[COMMENTS]
			   ,[UPDATE_DATE],[UPDATE_USER],[CREATE_DATE],[CREATE_USER]     
	FROM      LES.TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT
	   
	   
	  
	
end		  
COMMIT TRAN
RETURN  0 --成功

END TRY
BEGIN CATCH
    ROLLBACK TRAN
          INSERT  INTO [LES].[TS_SYS_EXCEPTION] (time_stamp,[application],[METHOD],class,exception_message,error_code)
                SELECT  GETDATE(),'INHOUSECOMBINE','PROC_BAS_INBOUND_PULL_COMBINE','Procedure',ERROR_MESSAGE(),ERROR_LINE()
   
    RETURN -1
END CATCH   
	
END