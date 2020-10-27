﻿


/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  [PROC_BAS_INHOUSE_PULL_COMBINE_PCS_COUNTER]             */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_BAS_INHOUSE_PULL_COMBINE_PCS_COUNTER]
 (
	@LogicalPK nvarchar(50) ,
	@TermalPK nvarchar(50) 
 )
AS

BEGIN


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
    WHERE A.LOGICAL_PK = @LogicalPK AND A.TERMAL_PK = @TermalPK AND not exists (SELECT * FROM 
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
    (SELECT [PLANT],[ASSEMBLY_LINE],[LOCATION],[PART_NO],[IN_PLANT_LOGISTIC_PART_CLASS],MIN([INHOUSE_PACKAGE_MODEL])AS [INHOUSE_PACKAGE_MODEL],MIN([INHOUSE_PACKAGE]) AS [INHOUSE_PACKAGE] 
    FROM [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD] 
    WHERE [IN_PLANT_SYSTEM_MODE]='PCS' AND LOGICAL_PK = @LogicalPK AND TERMAL_PK = @TermalPK
    GROUP BY   [PLANT],[ASSEMBLY_LINE],[LOCATION],[PART_NO],[IN_PLANT_LOGISTIC_PART_CLASS]) AS INHOUSE_PULL_GROUP
    ON A.[PLANT]=INHOUSE_PULL_GROUP.[PLANT] AND A.[ASSEMBLY_LINE]=INHOUSE_PULL_GROUP.[ASSEMBLY_LINE] AND 
		  A.[LOCATION]=INHOUSE_PULL_GROUP.[LOCATION] and A.[PART_NO]=INHOUSE_PULL_GROUP.[PART_NO] AND A.[IN_PLANT_LOGISTIC_PART_CLASS]=INHOUSE_PULL_GROUP.[IN_PLANT_LOGISTIC_PART_CLASS]
    WHERE  (A.[INHOUSE_PACKAGE]!=INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE] 
         OR A.[INHOUSE_PACKAGE_MODEL]!=INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE_MODEL])
		  
--删除计数器数据

    

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

 

	
END