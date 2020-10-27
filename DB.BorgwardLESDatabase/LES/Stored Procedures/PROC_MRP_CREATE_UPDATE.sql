

/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  PROC_BAS_CHECK_TE_INHOUSE_MAINTAIN_LOGISTIC_STANDARD                        */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/

Create PROC [LES].[PROC_MRP_CREATE_UPDATE]
 
AS

UPDATE [LES].[TT_MRP_DELIVERY_PLAN_CONFIRM]
   SET [CLIENT] = A.[CLIENT]
      ,[SEND_PORT] = A.[SEND_PORT]
      ,[IDOC] =  A.[IDOC]
      ,[SUPPLIER_NUM]=  A.[SUPPLIER_NUM]
      ,[SUPPLIER_NAME]=  A.[SUPPLIER_NAME]
      ,[PLANT]=  A.[PLANT]
      ,[CONTRACT_NO]=  A.[CONTRACT_NO]
      ,[PLANT_NAME]=  A.[PLANT_NAME]
      ,[COMMENTS]=  A.[COMMENTS]
      ,[CREATE_USER]=  A.[CREATE_USER]
      ,[CREATE_DATE]=  A.[CREATE_DATE]
      ,[UPDATE_USER]=  A.[UPDATE_USER]
      ,[UPDATE_DATE]=  A.[UPDATE_DATE] 
FROM [LES].TI_MRP_DELIVERY_PLAN  AS A       
INNER JOIN LES.[TT_MRP_DELIVERY_PLAN_CONFIRM] AS B ON A.CLIENT = B.CLIENT 
                     AND A.SEND_PORT = B.SEND_PORT 
                     AND A.IDOC = B.IDOC
 

INSERT INTO [LES].[TT_MRP_DELIVERY_PLAN_CONFIRM]
           ([CLIENT]
           ,[SEND_PORT]
           ,[IDOC]
           ,[SUPPLIER_NUM]
           ,[SUPPLIER_NAME]
           ,[PLANT]
           ,[CONTRACT_NO]
           ,[PLANT_NAME]
           ,[COMMENTS]
           ,[CREATE_USER]
           ,[CREATE_DATE]
           ,[UPDATE_USER]
           ,[UPDATE_DATE])
SELECT	    A.[CLIENT]
           ,A.[SEND_PORT]
           ,A.[IDOC]
           ,A.[SUPPLIER_NUM]
           ,A.[SUPPLIER_NAME]
           ,A.[PLANT]
           ,A.[CONTRACT_NO]
           ,A.[PLANT_NAME]
           ,A.[COMMENTS]
           ,A.[CREATE_USER]
           ,A.[CREATE_DATE]
           ,A.[UPDATE_USER]
           ,A.[UPDATE_DATE]
FROM [LES].TI_MRP_DELIVERY_PLAN  AS A 
LEFT JOIN LES.[TT_MRP_DELIVERY_PLAN_CONFIRM] AS B ON A.CLIENT = B.CLIENT 
                     AND A.SEND_PORT = B.SEND_PORT 
                     AND A.IDOC = B.IDOC
WHERE B.CLIENT IS NULL

 
 UPDATE [LES].[TT_MRP_PARTS_LIST_CONFIRM]
   SET [CLIENT] = A.[CLIENT]
      ,[SEND_PORT] = A.[SEND_PORT]
      ,[IDOC] = A.[IDOC]
      ,[LINE_NO] = A.[LINE_NO]
      ,[PART_NO] = A.[PART_NO]
      ,[PART_CNAME] = A.[PART_CNAME]
      ,[TRANS_SUPPLIER_NUM] = A.[TRANS_SUPPLIER_NUM]
      ,[MATERIAL_STATE] = A.[MATERIAL_STATE]
      ,[PLANNING_CLERK] = A.[PLANNING_CLERK]
      ,[PLANNING_CLERK_NAME] = A.[PLANNING_CLERK_NAME]
      ,[PLACE_OF_DELIVERY] = A.[PLACE_OF_DELIVERY]
      ,[UNIT] = A.[UNIT]
      ,[ACCUMULATE_PLAN_AMOUNT] = A.[ACCUMULATE_PLAN_AMOUNT]
      ,[ACCUMULATE_DELIVERY_AMOUNT] = A.[ACCUMULATE_DELIVERY_AMOUNT]
      ,[LAST_DELIVERY_AMOUNT] = A.[LAST_DELIVERY_AMOUNT]
      ,[LAST_DELIVERY_DATE] = A.[LAST_DELIVERY_DATE]
      ,[LAST_IDOC] = A.[LAST_IDOC]
      ,[COMMENTS] = A.[COMMENTS]
      ,[CREATE_USER] = A.[CREATE_USER]
      ,[CREATE_DATE] = A.[CREATE_DATE]
      ,[UPDATE_USER] = A.[UPDATE_USER]
      ,[UPDATE_DATE] = A.[UPDATE_DATE]                    
FROM [LES].[TI_MRP_PARTS_LIST]  AS A       
INNER JOIN LES.[TT_MRP_PARTS_LIST_CONFIRM] AS B ON A.CLIENT = B.CLIENT 
                     AND A.SEND_PORT = B.SEND_PORT 
                     AND A.IDOC = B.IDOC
                     AND A.LINE_NO = B.LINE_NO
                     
                     
INSERT INTO [LES].[TT_MRP_PARTS_LIST_CONFIRM]
           ([CLIENT]
           ,[SEND_PORT]
           ,[IDOC]
           ,[LINE_NO]
           ,[PART_NO]
           ,[PART_CNAME]
           ,[TRANS_SUPPLIER_NUM]
           ,[MATERIAL_STATE]
           ,[PLANNING_CLERK]
           ,[PLANNING_CLERK_NAME]
           ,[PLACE_OF_DELIVERY]
           ,[UNIT]
           ,[ACCUMULATE_PLAN_AMOUNT]
           ,[ACCUMULATE_DELIVERY_AMOUNT]
           ,[LAST_DELIVERY_AMOUNT]
           ,[LAST_DELIVERY_DATE]
           ,[LAST_IDOC]
           ,[CONFIRM_STATUS]
           ,[INBOUND_PACKAGE_MODEL]
           ,[INBOUND_PACKAGE]
           ,[COMMENTS]
           ,[CREATE_USER]
           ,[CREATE_DATE]
           ,[UPDATE_USER]
           ,[UPDATE_DATE])
SELECT		A.[CLIENT]
           ,A.[SEND_PORT]
           ,A.[IDOC]
           ,A.[LINE_NO]
           ,A.[PART_NO]
           ,A.[PART_CNAME]
           ,A.[TRANS_SUPPLIER_NUM]
           ,A.[MATERIAL_STATE]
           ,A.[PLANNING_CLERK]
           ,A.[PLANNING_CLERK_NAME]
           ,A.[PLACE_OF_DELIVERY]
           ,A.[UNIT]
           ,A.[ACCUMULATE_PLAN_AMOUNT]
           ,A.[ACCUMULATE_DELIVERY_AMOUNT]
           ,A.[LAST_DELIVERY_AMOUNT]
           ,A.[LAST_DELIVERY_DATE]
           ,A.[LAST_IDOC]
           ,NULL
           ,NULL
           ,NULL
           ,A.[COMMENTS]
           ,A.[CREATE_USER]
           ,A.[CREATE_DATE]
           ,A.[UPDATE_USER]
           ,A.[UPDATE_DATE]
FROM [LES].[TI_MRP_PARTS_LIST]  AS A       
LEFT JOIN LES.[TT_MRP_PARTS_LIST_CONFIRM] AS B ON A.CLIENT = B.CLIENT 
                     AND A.SEND_PORT = B.SEND_PORT 
                     AND A.IDOC = B.IDOC
                     AND A.LINE_NO = B.LINE_NO
                     
 WHERE B.CLIENT IS NULL
  
  
  UPDATE [LES].[TT_MRP_REQUEST_LIST_CONFIRM]
   SET [CLIENT] = A.[CLIENT]
      ,[SEND_PORT] = A.[SEND_PORT]
      ,[IDOC] = A.[IDOC]
      ,[LINE_NO] = A.[LINE_NO]
      ,[DELIVERY_DATE] = A.[DELIVERY_DATE]
      ,[PLAN_AMOUNT] = A.[PLAN_AMOUNT]
      ,[TRANS_SUPPLIER_NUM] = A.[TRANS_SUPPLIER_NUM]
      ,[DELIVERY_AMOUNT] = A.[DELIVERY_AMOUNT]
      ,[CHANGE_AMOUNT] = A.[CHANGE_AMOUNT]
      ,[UN_DELIVERY_AMOUNT] = A.[UN_DELIVERY_AMOUNT]
      ,[ACCUMULATE_UN_DELIVERY_AMOUNT] = A.[ACCUMULATE_UN_DELIVERY_AMOUNT]
      ,[COMMENTS] = A.[COMMENTS]
      ,[CREATE_USER] = A.[CREATE_USER]
      ,[CREATE_DATE] = A.[CREATE_DATE]
      ,[UPDATE_USER] = A.[UPDATE_USER]
      ,[UPDATE_DATE] = A.[UPDATE_DATE]
FROM [LES].TI_MRP_REQUEST_LIST  AS A   
INNER JOIN LES.TT_MRP_REQUEST_LIST_CONFIRM AS B ON A.CLIENT = B.CLIENT 
                     AND A.SEND_PORT = B.SEND_PORT 
                     AND A.IDOC = B.IDOC
                     AND A.LINE_NO = B.LINE_NO
                     AND A.DELIVERY_DATE = B.DELIVERY_DATE 
                     
INSERT INTO [LES].[TT_MRP_REQUEST_LIST_CONFIRM]
           ([CLIENT]
           ,[SEND_PORT]
           ,[IDOC]
           ,[LINE_NO]
           ,[DELIVERY_DATE]
           ,[PLAN_AMOUNT]
           ,[TRANS_SUPPLIER_NUM]
           ,[DELIVERY_AMOUNT]
           ,[CHANGE_AMOUNT]
           ,[UN_DELIVERY_AMOUNT]
           ,[ACCUMULATE_UN_DELIVERY_AMOUNT]
           ,[SUPPLY_CONFIRM_AMOUNT]
           ,[PLANER_CONFIRM_AMOUNT]
           ,[SUPPLY_CONFIRM_DATE]
           ,[PLANER_CONFIRM_DATE]
           ,[CONFIRM_STATUS]
           ,[FREEZE_TERM]
           ,[LOGICAL_PK]
           ,[COMMENTS]
           ,[CREATE_USER]
           ,[CREATE_DATE]
           ,[UPDATE_USER]
           ,[UPDATE_DATE])
SELECT		A.[CLIENT]
           ,A.[SEND_PORT]
           ,A.[IDOC]
           ,A.[LINE_NO]
           ,A.[DELIVERY_DATE]
           ,A.[PLAN_AMOUNT]
           ,A.[TRANS_SUPPLIER_NUM]
           ,A.[DELIVERY_AMOUNT]
           ,A.[CHANGE_AMOUNT]
           ,A.[UN_DELIVERY_AMOUNT]
           ,A.[ACCUMULATE_UN_DELIVERY_AMOUNT]
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,A.[COMMENTS]
           ,A.[CREATE_USER]
           ,A.[CREATE_DATE]
           ,A.[UPDATE_USER]
           ,A.[UPDATE_DATE]
FROM [LES].TI_MRP_REQUEST_LIST  AS A   
LEFT JOIN LES.TT_MRP_REQUEST_LIST_CONFIRM AS B ON A.CLIENT = B.CLIENT 
                     AND A.SEND_PORT = B.SEND_PORT 
                     AND A.IDOC = B.IDOC
                     AND A.LINE_NO = B.LINE_NO
                     AND A.DELIVERY_DATE = B.DELIVERY_DATE 
 WHERE B.CLIENT IS NULL