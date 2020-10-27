﻿
/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  [PROC_BAS_INHOUSE_PULL_CREATE_UPDATE]             */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_BAS_INHOUSE_PULL_CREATE_UPDATE]
 
AS

BEGIN

    
UPDATE [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD]
   SET [PLANT] = TE.[PLANT]
      ,[ASSEMBLY_LINE] = TE.[ASSEMBLY_LINE]
      ,[PLANT_ZONE] = TE.[PLANT_ZONE]
      ,[WORKSHOP] = TE.[WORKSHOP]
      ,[SUPPLIER_NUM] = TE.[SUPPLIER_NUM]
      ,[TRANS_SUPPLIER_NUM] = TE.[TRANS_SUPPLIER_NUM]
      ,[MODEL] = TE.[MODEL]
      ,[PART_NO] = TE.[PART_NO]
      ,[INDENTIFY_PART_NO] = TE.[INDENTIFY_PART_NO]
      ,[AMOUNTRATIO] = TE.[AMOUNTRATIO]
      ,[EXTERIOR_COLOR] = TE.[EXTERIOR_COLOR]
      ,[INTERNAL_COLOR] = TE.[INTERNAL_COLOR]
      ,[HAND_KEPT_RECORD] = TE.[HAND_KEPT_RECORD]
      ,[COLOR_CONTROL_PATCH] = TE.[COLOR_CONTROL_PATCH]
      ,[VWS] = TE.[VWS]
      ,[RAND] = TE.[RAND]
      ,[SECTION] = TE.[SECTION]
      ,[PART_OPTION] = TE.[PART_OPTION]
      ,[PRODUCTION_NUMBER] =TE.[PRODUCTION_NUMBER]
      ,[START_PRODUCTION_DATE] = TE.[START_PRODUCTION_DATE]
      ,[CANCEL_NUMBER] = TE.[CANCEL_NUMBER]
      ,[END_PRODUCTION_DATE] = TE.[END_PRODUCTION_DATE]
      ,[MODEL_YEAR] = TE.[MODEL_YEAR]
      ,[DOSAGE] = TE.[DOSAGE]
      ,[MEASURING_UNIT_NO] = TE.[MEASURING_UNIT_NO]
      ,[AMOUNT_FLAG] = TE.[AMOUNT_FLAG]
      ,[DATA_DATE] = TE.[DATA_DATE]
      ,[VORSERIE] = TE.[VORSERIE]
      ,[PART_CNAME] = TE.[PART_CNAME]
      ,[PART_ENAME] = TE.[PART_ENAME]
      ,[PART_NICKNAME] = TE.[PART_NICKNAME]
      ,[LOAD_FLAG] = TE.[LOAD_FLAG]
      ,[ZP_FLAG] = TE.[ZP_FLAG]
      ,[REQUIREMENT_FLAG] = TE.[REQUIREMENT_FLAG]
      ,[ASSEMBLY_FLAG] = TE.[ASSEMBLY_FLAG]
      ,[ASSEMBLY_FLAG_RECRUIT] = TE.[ASSEMBLY_FLAG_RECRUIT]
      ,[PURCHASE_STYLE] = TE.[PURCHASE_STYLE]
      ,[LOGICAL_PK] = TE.[LOGICAL_PK]
      ,[VALID_FLAG] = TE.[VALID_FLAG]
      ,[WORKSHOP_SECTION] = TE.[WORKSHOP_SECTION]
      ,[LOCATION] = TE.[LOCATION]
      ,[IN_PLANT_LOGISTIC_MODE] = TE.[IN_PLANT_LOGISTIC_MODE]
      ,[IN_PLANT_SYSTEM_MODE] = TE.[IN_PLANT_SYSTEM_MODE]
      ,[IN_PLANT_LOGISTIC_PART_CLASS] = TE.[IN_PLANT_LOGISTIC_PART_CLASS]
      ,[INHOUSE_MODE] = TE.[INHOUSE_MODE]
      ,[INHOUSE_SYSTEM_MODE] = TE.[INHOUSE_SYSTEM_MODE]
      ,[EFFECTIVE_STATUS] = TE.[EFFECTIVE_STATUS]
      ,[START_EFFECTIVE_DATE] = TE.[START_EFFECTIVE_DATE]
      ,[INHOUSE_PART_CLASS] = TE.[INHOUSE_PART_CLASS]
      ,[DOCK] = TE.[DOCK]
      ,[INHOUSE_PACKAGE_MODEL] = TE.[INHOUSE_PACKAGE_MODEL]
      ,[INHOUSE_PACKAGE] = TE.[INHOUSE_PACKAGE]
      ,[DIFF_FLAG] = TE.[DIFF_FLAG]
      ,[TERMAL_PK] = TE.[TERMAL_PK]
      ,BUSINESS_PK = TE.BUSINESS_PK
      ,[COMMENTS] = TE.[COMMENTS]
      ,[CREATE_USER] = TE.[CREATE_USER]
      ,[CREATE_DATE] = TE.[CREATE_DATE]
      ,[UPDATE_USER] = TE.[UPDATE_USER]
      ,[UPDATE_DATE] = TE.[UPDATE_DATE]
     
FROM LES.TE_BAS_TMP_INHOUSE_PULL_LOGISTIC_STANDARD AS TE,
     [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD] AS TM
 WHERE TE.LOGICAL_PK = TM.LOGICAL_PK AND TE.TERMAL_PK = TM.TERMAL_PK

INSERT INTO [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD]
       ([PLANT],[ASSEMBLY_LINE],[PLANT_ZONE],[WORKSHOP]
       ,[SUPPLIER_NUM],[TRANS_SUPPLIER_NUM],[MODEL],[PART_NO]
       ,[INDENTIFY_PART_NO],[AMOUNTRATIO],[EXTERIOR_COLOR],[INTERNAL_COLOR]
       ,[HAND_KEPT_RECORD],[COLOR_CONTROL_PATCH],[VWS],[RAND]
       ,[SECTION],[PART_OPTION],[PRODUCTION_NUMBER],[START_PRODUCTION_DATE]
       ,[CANCEL_NUMBER],[END_PRODUCTION_DATE],[MODEL_YEAR],[DOSAGE]
       ,[MEASURING_UNIT_NO],[AMOUNT_FLAG],[DATA_DATE],[VORSERIE]
       ,[PART_CNAME],[PART_ENAME],[PART_NICKNAME],[LOAD_FLAG]
       ,[ZP_FLAG],[REQUIREMENT_FLAG],[ASSEMBLY_FLAG],[ASSEMBLY_FLAG_RECRUIT]
       ,[PURCHASE_STYLE],[VALID_FLAG],[WORKSHOP_SECTION]
       ,[LOCATION],[IN_PLANT_LOGISTIC_MODE],[IN_PLANT_SYSTEM_MODE],[IN_PLANT_LOGISTIC_PART_CLASS]
       ,[INHOUSE_MODE],[INHOUSE_SYSTEM_MODE],[EFFECTIVE_STATUS],[START_EFFECTIVE_DATE]
       ,[INHOUSE_PART_CLASS],[DOCK],[INHOUSE_PACKAGE_MODEL]
       ,[INHOUSE_PACKAGE],[DIFF_FLAG],TERMAL_PK,[COMMENTS]
       ,[UPDATE_DATE],[UPDATE_USER],[CREATE_DATE],[CREATE_USER],LOGICAL_PK,BUSINESS_PK)
SELECT 
       TE.[PLANT],TE.[ASSEMBLY_LINE],TE.[PLANT_ZONE],TE.[WORKSHOP]
      ,TE.[SUPPLIER_NUM],TE.[TRANS_SUPPLIER_NUM],TE.[MODEL],TE.[PART_NO]
      ,TE.[INDENTIFY_PART_NO],TE.[AMOUNTRATIO],TE.[EXTERIOR_COLOR],TE.[INTERNAL_COLOR]
      ,TE.[HAND_KEPT_RECORD],TE.[COLOR_CONTROL_PATCH],TE.[VWS],TE.[RAND]
      ,TE.[SECTION],TE.[PART_OPTION],TE.[PRODUCTION_NUMBER],TE.[START_PRODUCTION_DATE]
      ,TE.[CANCEL_NUMBER],TE.[END_PRODUCTION_DATE],TE.[MODEL_YEAR],TE.[DOSAGE]
      ,TE.[MEASURING_UNIT_NO],TE.[AMOUNT_FLAG],TE.[DATA_DATE],TE.[VORSERIE]
      ,TE.[PART_CNAME],TE.[PART_ENAME],TE.[PART_NICKNAME],TE.LOAD_FLAG
      ,TE.[ZP_FLAG],TE.[REQUIREMENT_FLAG],TE.[ASSEMBLY_FLAG],TE.[ASSEMBLY_FLAG_RECRUIT]
      ,TE.[PURCHASE_STYLE],TE.VALID_FLAG ,TE.[WORKSHOP_SECTION]
      ,TE.[LOCATION],TE.[IN_PLANT_LOGISTIC_MODE],TE.[IN_PLANT_SYSTEM_MODE],TE.[IN_PLANT_LOGISTIC_PART_CLASS]
      ,TE.[INHOUSE_MODE],TE.[INHOUSE_SYSTEM_MODE],TE.[EFFECTIVE_STATUS],TE.[START_EFFECTIVE_DATE]
      ,TE.[INHOUSE_PART_CLASS],TE.[DOCK],TE.[INHOUSE_PACKAGE_MODEL]
      ,TE.[INHOUSE_PACKAGE],TE.[DIFF_FLAG],TE.TERMAL_PK,TE.[COMMENTS]
      ,TE.[UPDATE_DATE],TE.[UPDATE_USER],TE.[CREATE_DATE] ,TE.[CREATE_USER],TE.LOGICAL_PK,TE.BUSINESS_PK
  FROM [LES].[TE_BAS_TMP_INHOUSE_PULL_LOGISTIC_STANDARD] AS TE
  LEFT JOIN  LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD AS TM  ON  TE.LOGICAL_PK = TM.LOGICAL_PK AND TE.TERMAL_PK = TM.TERMAL_PK
                            
       
  WHERE   TE.VALID_FLAG = 1 AND TM.LOGICAL_PK IS NULL
  
--插入到PCS计数器

INSERT INTO [LES].[TT_PCS_COUNTER]
           ([PLANT]           --*关键
           ,[ASSEMBLY_LINE]       --*关键
            ,[SUPPLIER_NUM]       --*关键
           ,[PART_NO]         --*关键
           ,[INDENTIFY_PART_NO]
           ,[AMOUNTRATIO]          
           ,[MEASURING_UNIT_NO]
           ,[PART_CNAME]
           ,[PART_ENAME]
           ,[LOCATION]            --*关键
           ,[IN_PLANT_LOGISTIC_PART_CLASS] --*关键
           ,[INHOUSE_PACKAGE_MODEL]
           ,[INHOUSE_PACKAGE]
           ,[CURRENT_PART_COUNT])         
           SELECT  
	   A.[PLANT]
      ,A.[ASSEMBLY_LINE]
      ,C.[SUPPLIER_NUM]
      ,A.[PART_NO]
      ,A.[INDENTIFY_PART_NO]
      ,min(A.[AMOUNTRATIO])
      ,min(A.[MEASURING_UNIT_NO])
      ,Min(A.[PART_CNAME])
      ,Min(A.[PART_ENAME])

      ,A.[LOCATION]
      ,A.[IN_PLANT_LOGISTIC_PART_CLASS]

      ,MIN(A.[INHOUSE_PACKAGE_MODEL])
      ,MIN(A.[INHOUSE_PACKAGE])
      ,MIN(A.[INHOUSE_PACKAGE])
    FROM [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD]  A 
    INNER JOIN [LES].[TM_PCS_ROUTE_BOX_PARTS] C ON A.[PLANT]=C.[PLANT] AND A.[ASSEMBLY_LINE]=C.[ASSEMBLY_LINE]  and A.IN_PLANT_LOGISTIC_PART_CLASS=c.BOX_PARTS
    WHERE not exists (SELECT * FROM 
		   [LES].[TT_PCS_COUNTER] B WHERE A.[PLANT]=B.[PLANT] AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND C.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
		  A.[LOCATION]=B.[LOCATION] and A.[PART_NO]=B.[PART_NO] ) AND  A.[IN_PLANT_SYSTEM_MODE]='PCS' 
	GROUP BY 
    A.[PLANT]
      ,A.[ASSEMBLY_LINE]
      ,C.[SUPPLIER_NUM]
      --,A.[MODEL]
      ,A.[PART_NO]
      ,A.[INDENTIFY_PART_NO]
      ,A.[LOCATION]
      ,A.[IN_PLANT_LOGISTIC_PART_CLASS]

	 
--更新计数器
UPDATE A SET  A.[INHOUSE_PACKAGE_MODEL]=INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE_MODEL]
           ,A.[INHOUSE_PACKAGE]=INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE]
    FROM [LES].[TT_PCS_COUNTER] A INNER JOIN 
    (SELECT [PLANT],[ASSEMBLY_LINE],[LOCATION],[PART_NO],[IN_PLANT_LOGISTIC_PART_CLASS],MIN([INHOUSE_PACKAGE_MODEL])AS [INHOUSE_PACKAGE_MODEL],MIN([INHOUSE_PACKAGE]) AS [INHOUSE_PACKAGE] FROM [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD] WHERE [IN_PLANT_SYSTEM_MODE]='PCS'
    GROUP BY   [PLANT],[ASSEMBLY_LINE],[LOCATION],[PART_NO],[IN_PLANT_LOGISTIC_PART_CLASS]) AS INHOUSE_PULL_GROUP
    ON A.[PLANT]=INHOUSE_PULL_GROUP.[PLANT] AND A.[ASSEMBLY_LINE]=INHOUSE_PULL_GROUP.[ASSEMBLY_LINE] AND 
		  A.[LOCATION]=INHOUSE_PULL_GROUP.[LOCATION] and A.[PART_NO]=INHOUSE_PULL_GROUP.[PART_NO] AND A.[IN_PLANT_LOGISTIC_PART_CLASS]=INHOUSE_PULL_GROUP.[IN_PLANT_LOGISTIC_PART_CLASS]
    WHERE (A.[INHOUSE_PACKAGE]!=INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE] OR A.[INHOUSE_PACKAGE_MODEL]!=INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE_MODEL])
		  
--删除计数器数据
delete from [LES].[TT_PCS_COUNTER]  where not	exists (SELECT * FROM 
		   [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD] B WHERE [LES].[TT_PCS_COUNTER].[PLANT]=B.[PLANT] AND [LES].[TT_PCS_COUNTER].[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND 
		  [LES].[TT_PCS_COUNTER].[LOCATION]=B.[LOCATION] and [LES].[TT_PCS_COUNTER].[PART_NO]=B.[PART_NO] AND [LES].[TT_PCS_COUNTER].[IN_PLANT_LOGISTIC_PART_CLASS]=B.[IN_PLANT_LOGISTIC_PART_CLASS] AND  B.[IN_PLANT_SYSTEM_MODE]='PCS'  )  

--插入到JIS变更表
--INSERT INTO [LES].[TE_BAS_INHOUSE_CHANGE_PULL]
--           ([PART_NO],[LOGICAL_PK],[DIFF_FLAG],[TERMAL_PK]
--           ,[COMMENTS]
--           ,[CREATE_USER],[CREATE_DATE],[UPDATE_USER],[UPDATE_DATE])
--SELECT Termal.PART_NO,Termal.LOGICAL_PK,0,Termal.Termal_PK,
--       null,
--       null,getdate(),null,null
--FROM [LES].[V_BAS_INHOUSEPULL_COMBINE] AS Termal
--WHERE (INHOUSE_SYSTEM_MODE = 'JIS' OR IN_PLANT_SYSTEM_MODE = 'JIS')
--AND (Termal.Termal_Diff_Flag In (2,3,4)  AND  Termal.Inhouse_Diff_Flag In(1,2,3))
 

--更新Inhouse拉动数据
/*
DELETE  [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD]   
FROM [LES].[V_BAS_INHOUSEPULL_COMBINE] AS Termal,
     LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD AS TM    
WHERE   Termal.Termal_PK = TM.TERMAL_PK --AND Termal.VALID_FLAG = 1 
        AND (Termal.Termal_Diff_Flag = 4 OR Termal.Inhouse_Diff_Flag = 3)
       
--更新状态Termal/PBOM合并表、更新状态Inhouse物流标准表

UPDATE  [LES].TM_ODS_TERMAL_PBOM_COMBINE_RESULT
SET     DIFF_FLAG = 0
FROM [LES].TM_ODS_TERMAL_PBOM_COMBINE_RESULT AS Termal
WHERE Termal.DIFF_FLAG <> 0

UPDATE  [LES].TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD
SET     DIFF_FLAG = 0
FROM [LES].TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD AS TM
WHERE TM.DIFF_FLAG <> 0
*/
 UPDATE [LES].[TS_SYS_COMBINE_CONTROL] set [ISPASS]=0 where [SEQID]=1



  
	
END