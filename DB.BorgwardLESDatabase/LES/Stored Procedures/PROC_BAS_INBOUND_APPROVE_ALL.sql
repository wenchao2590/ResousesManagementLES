﻿

/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  PROC_BAS_INBOUND_APPROVE_ALL                 */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/

CREATE PROCEDURE [LES].[PROC_BAS_INBOUND_APPROVE_ALL]
 
AS

BEGIN

	--TYPE 1:新增 2:修改 3：删除 

--新增结果
INSERT INTO [LES].TM_BAS_MAINTAIN_INBOUND_LOGISTIC_STANDARD
           ([PLANT],[ASSEMBLY_LINE],[PLANT_ZONE],[WORKSHOP]
           ,[SUPPLIER_NUM],[TRANS_SUPPLIER_NUM],[MODEL],[PART_NO]
           ,[COLOR_DEPARTMENT],[HAND_KEPT_RECORD],[COLOR_CONTROL_PATCH],[VWS]
           ,[MODEL_YEAR],[PART_CNAME],[PART_ENAME],[INBOUND_MODE]
           ,[INBOUND_LOGISTIC_MODE],[INBOUND_SYSTEM_MODE],[START_EFFECTIVE_DATE],[INBOUND_PART_CLASS]
           ,[DOCK],[INBOUND_PACKAGE_MODEL],[INBOUND_PACKAGE],[LOAD_FLAG]
           ,[ZP_FLAG],[REQUIREMENT_FLAG],[ASSEMBLY_FLAG],[ASSEMBLY_FLAG_RECRUIT]
           ,[MARK],[PURCHASE_STYLE],[DELETE_FLAG],[PLATFORM_CHANGE_DATE]
           ,[IS_SPLIT],[LOGICAL_PK],[DIFF_FLAG],VALID_FLAG
           ,[COMMENTS],[UPDATE_DATE],[UPDATE_USER],[CREATE_DATE]
           ,[CREATE_USER])
SELECT TE.[PLANT],TE.[ASSEMBLY_LINE],TE.[PLANT_ZONE],TE.[WORKSHOP]
      ,TE.[SUPPLIER_NUM],TE.[TRANS_SUPPLIER_NUM],TE.[MODEL],TE.[PART_NO]
      ,TE.[COLOR_DEPARTMENT],TE.[HAND_KEPT_RECORD],TE.[COLOR_CONTROL_PATCH],TE.[VWS]
      ,TE.[MODEL_YEAR],TE.[PART_CNAME],TE.[PART_ENAME],TE.[INBOUND_MODE]
      ,TE.[INBOUND_LOGISTIC_MODE],TE.[INBOUND_SYSTEM_MODE],TE.[START_EFFECTIVE_DATE],TE.[INBOUND_PART_CLASS]
      ,TE.[DOCK],TE.[INBOUND_PACKAGE_MODEL],TE.[INBOUND_PACKAGE],TE.[LOAD_FLAG]
      ,TE.[ZP_FLAG],TE.[REQUIREMENT_FLAG],TE.[ASSEMBLY_FLAG],TE.[ASSEMBLY_FLAG_RECRUIT]
      ,TE.[MARK],TE.[PURCHASE_STYLE],TE.[DELETE_FLAG],TE.[PLATFORM_CHANGE_DATE]
      ,TE.[IS_SPLIT],TE.[LOGICAL_PK],TE.DIFF_FLAG,1
      ,TE.[COMMENTS],TE.[UPDATE_DATE],TE.[UPDATE_USER],TE.[CREATE_DATE]
      ,TE.[CREATE_USER]
FROM    LES.TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF AS TE
WHERE  TE.DIFF_FLAG = 1
	
	
--修改
UPDATE [LES].[TM_BAS_MAINTAIN_INBOUND_LOGISTIC_STANDARD]
   SET 
       [PLANT] = TE.[PLANT]
      ,[ASSEMBLY_LINE] = TE.[ASSEMBLY_LINE]
      ,[PLANT_ZONE] = TE.[PLANT_ZONE]
      ,[WORKSHOP] = TE.[WORKSHOP]
      ,[SUPPLIER_NUM] = TE.[SUPPLIER_NUM]
      ,[TRANS_SUPPLIER_NUM] = TE.[TRANS_SUPPLIER_NUM]
      ,[MODEL] = TE.[MODEL]
      ,[PART_NO] = TE.[PART_NO]
      ,[COLOR_DEPARTMENT] = TE.[COLOR_DEPARTMENT]
      ,[HAND_KEPT_RECORD] = TE.[HAND_KEPT_RECORD]
      ,[COLOR_CONTROL_PATCH] = TE.[COLOR_CONTROL_PATCH]
      ,[VWS] = TE.[VWS]
      ,[MODEL_YEAR] = TE.[MODEL_YEAR]
      ,[PART_CNAME] = TE.[PART_CNAME]
      ,[PART_ENAME] = TE.[PART_ENAME]
      ,[INBOUND_MODE] = TE.[INBOUND_MODE]
      ,[INBOUND_LOGISTIC_MODE] = TE.[INBOUND_LOGISTIC_MODE]
      ,[INBOUND_SYSTEM_MODE] = TE.[INBOUND_SYSTEM_MODE]
      ,[START_EFFECTIVE_DATE] = TE.[START_EFFECTIVE_DATE]
      ,[INBOUND_PART_CLASS] = TE.[INBOUND_PART_CLASS]
      ,[DOCK] = TE.[DOCK]
      ,[INBOUND_PACKAGE_MODEL] = TE.[INBOUND_PACKAGE_MODEL]
      ,[INBOUND_PACKAGE] =TE.[INBOUND_PACKAGE]
      ,[LOAD_FLAG] = TE.[LOAD_FLAG]
      ,[ZP_FLAG] = TE.[ZP_FLAG]
      ,[REQUIREMENT_FLAG] = TE.[REQUIREMENT_FLAG]
      ,[ASSEMBLY_FLAG] = TE.[ASSEMBLY_FLAG]
      ,[ASSEMBLY_FLAG_RECRUIT] = TE.[ASSEMBLY_FLAG_RECRUIT]
      ,[MARK] = TE.[MARK]
      ,[PURCHASE_STYLE] = TE.[PURCHASE_STYLE]
      ,[DELETE_FLAG] = TE.[DELETE_FLAG]
      ,[PLATFORM_CHANGE_DATE] = TE.[PLATFORM_CHANGE_DATE]
      ,[IS_SPLIT] = TE.[IS_SPLIT]
      ,[DIFF_FLAG] = TE.[DIFF_FLAG]
      ,[COMMENTS] = TE.[COMMENTS]
      ,[UPDATE_DATE] = TE.[UPDATE_DATE]
      ,[UPDATE_USER] = TE.[UPDATE_USER]
      ,[CREATE_DATE] = TE.[CREATE_DATE]
      ,[CREATE_USER] = TE.[CREATE_USER]
              
FROM LES.TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF  AS TE,
   LES.TM_BAS_MAINTAIN_INBOUND_LOGISTIC_STANDARD AS TM 
WHERE TE.LOGICAL_PK = TM.LOGICAL_PK  
AND TE.[DIFF_FLAG] = 2
 
--删除


UPDATE [LES].[TM_BAS_MAINTAIN_INBOUND_LOGISTIC_STANDARD] 
SET DIFF_FLAG = 3
FROM [LES].[TM_BAS_MAINTAIN_INBOUND_LOGISTIC_STANDARD] AS TM,
     [LES].TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF AS TE
WHERE TM.LOGICAL_PK = TE.LOGICAL_PK  
 AND TE.[DIFF_FLAG] = 3 


--更新全部FLAG
UPDATE [LES].TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF 
SET CONFIRM_FLAG = 1 --,CONFIRM_DATE = GETDATE()
	
END