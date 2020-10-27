﻿
/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  PROC_BAS_INBOUND_PULL_COMBINE                */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_BAS_INBOUND_PULL_COMBINE_BY_ASSEMBLYLINE]
(
 	@PLANT nvarchar(5),
 	@ASSEMBLY_LINE  nvarchar(10)
) 
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
	delete from  [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD] 
	WHERE  PLANT = @PLANT AND ASSEMBLY_LINE = @ASSEMBLY_LINE

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
	WHERE PLANT = @PLANT AND ASSEMBLY_LINE = @ASSEMBLY_LINE   
	   
	  
	--插入到TWD计数器

	INSERT INTO [LES].[TT_TWD_CONSUME_COUNTER]
			   ([PLANT] --*关键
			   ,[ASSEMBLY_LINE] --*关键
			   ,[SUPPLIER_NUM]       --*关键
			   ,[MODEL] --*关键
			   ,[PART_NO] --*关键
			   ,[INDENTIFY_PART_NO]
			   ,[AMOUNTRATIO]
			   ,[INBOUND_PART_CLASS]
			   ,[MEASURING_UNIT_NO]
			   ,[PART_CNAME]
			   ,[PART_ENAME]
			   ,[DOCK] --*关键
			   ,[INBOUND_PACKAGE_MODEL]
			   ,[INBOUND_PACKAGE]
			   ,[CURRENT_PART_COUNT]
			   ,[CREATE_DATE]
			   , [INHOUSE_PACKAGE_MODEL])
	       
		  SELECT distinct  A.[PLANT]
		  ,A.[ASSEMBLY_LINE]
		  ,A.[SUPPLIER_NUM]
		  ,min(A.[MODEL])
		  ,A.[PART_NO]
		  ,min(A.[INDENTIFY_PART_NO])
		  ,min(A.[AMOUNTRATIO])
		  ,A.[INBOUND_PART_CLASS]
		  ,min(A.[MEASURING_UNIT_NO])
		  ,min(A.[PART_CNAME])
		  ,min(A.[PART_ENAME])
		  ,min(A.[DOCK])
		  ,min(A.[INBOUND_PACKAGE_MODEL])
		  ,min(A.[INBOUND_PACKAGE])
		  ,0 
		  ,getdate()
		   ,min(A.MARK)
		FROM [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]  A WHERE not exists (SELECT * FROM 
			   [LES].[TT_TWD_CONSUME_COUNTER] B WHERE A.[PLANT]=B.[PLANT] AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
			  A.[INBOUND_PART_CLASS]=B.[INBOUND_PART_CLASS] AND A.[PART_NO]=B.[PART_NO]) AND  A.[INBOUND_SYSTEM_MODE]='TWD' 
		 GROUP BY 
		 A.[PLANT]
		  ,A.[ASSEMBLY_LINE]
		  ,A.[SUPPLIER_NUM]
		  --,A.[MODEL]
		  ,A.[PART_NO]
		  ,A.[INBOUND_PART_CLASS]		  


	--更新计数器
	UPDATE A SET  A.[INBOUND_PACKAGE]=B.[INBOUND_PACKAGE],[CURRENT_PART_COUNT]=0,A.[PALLET_COUNT]=B.[INBOUND_PACKAGE]*A.[PALLET_PACKAGE]
		FROM [LES].[TT_TWD_CONSUME_COUNTER] A INNER JOIN 
		[LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]  B  ON A.[PLANT]=B.[PLANT] AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
			  A.[INBOUND_PART_CLASS]=B.[INBOUND_PART_CLASS] and A.[PART_NO]=B.[PART_NO]
		where 	(A.[INBOUND_PACKAGE]!=B.[INBOUND_PACKAGE] )
		
		
			UPDATE A SET  A.[INBOUND_PACKAGE_MODEL]=B.[INBOUND_PACKAGE_MODEL]
		FROM [LES].[TT_TWD_CONSUME_COUNTER] A INNER JOIN 
		[LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]  B  ON A.[PLANT]=B.[PLANT] AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
			  A.[INBOUND_PART_CLASS]=B.[INBOUND_PART_CLASS] and A.[PART_NO]=B.[PART_NO]
		where 	(A.[INBOUND_PACKAGE_MODEL]!=B.[INBOUND_PACKAGE_MODEL])
		
		--更新计数器--用量比例，中英文名称变更
		UPDATE A SET  A.AMOUNTRATIO=B.AMOUNTRATIO,A.PART_CNAME=B.PART_CNAME,A.PART_ENAME=B.PART_ENAME
		FROM [LES].[TT_TWD_CONSUME_COUNTER] A 
		INNER JOIN [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]  B  ON A.[PLANT]=B.[PLANT] 
		AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] 
		AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] 
		AND A.[INBOUND_PART_CLASS]=B.[INBOUND_PART_CLASS] 
		AND A.[PART_NO]=B.[PART_NO]
		WHERE 	(A.AMOUNTRATIO!=B.AMOUNTRATIO OR A.PART_CNAME!=B.PART_CNAME OR A.PART_ENAME!=B.PART_ENAME)
	   
	   --更新组单零件类 
		UPDATE A SET  A.[INHOUSE_PACKAGE_MODEL]=B.[MARK]
        FROM [LES].[TT_TWD_CONSUME_COUNTER] A INNER JOIN 
        [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]  B  ON A.[PLANT]=B.[PLANT] AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
              A.[INBOUND_PART_CLASS]=B.[INBOUND_PART_CLASS] and A.[PART_NO]=B.[PART_NO]
        where   (A.[INHOUSE_PACKAGE_MODEL]!=B.[MARK] )
			  
			  
	--删除计数器数据
	delete from [LES].[TT_TWD_CONSUME_COUNTER]  where not	exists (SELECT * FROM 
			   [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD] B WHERE [LES].[TT_TWD_CONSUME_COUNTER].[PLANT]=B.[PLANT] AND [LES].[TT_TWD_CONSUME_COUNTER].[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND [LES].[TT_TWD_CONSUME_COUNTER].[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
			  [LES].[TT_TWD_CONSUME_COUNTER].[INBOUND_PART_CLASS]=B.[INBOUND_PART_CLASS] and [LES].[TT_TWD_CONSUME_COUNTER].[PART_NO]=B.[PART_NO] AND  B.[INBOUND_SYSTEM_MODE]='TWD' )  
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