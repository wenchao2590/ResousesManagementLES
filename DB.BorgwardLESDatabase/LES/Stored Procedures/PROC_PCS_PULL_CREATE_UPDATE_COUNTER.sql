



/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  [PROC_BAS_INHOUSE_PULL_CREATE_UPDATE]             */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_PULL_CREATE_UPDATE_COUNTER]
 
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
      ,A.[PART_NO]
      ,1
      ,1
      ,MIN(A.[PART_CNAME])
      ,MIN(A.[PART_ENAME])

      ,A.[LOCATION]
      ,A.INHOUSE_PART_CLASS

      ,MIN(A.[INBOUND_PACKAGE_MODEL])
      ,MIN(A.[INBOUND_PACKAGE])
      ,MIN(A.[INBOUND_PACKAGE])
	  
    FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD]  A 
    INNER JOIN [LES].[TM_PCS_ROUTE_BOX_PARTS] C ON A.[PLANT]=C.[PLANT] AND A.[ASSEMBLY_LINE]=C.[ASSEMBLY_LINE]  AND A.IN_PLANT_LOGISTIC_PART_CLASS=c.BOX_PARTS
    WHERE NOT EXISTS (SELECT * FROM 
		   [LES].[TT_PCS_COUNTER] B WHERE A.[PLANT]=B.[PLANT] AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND C.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
		  A.[LOCATION]=B.[LOCATION] AND A.[PART_NO]=B.[PART_NO] ) AND  A.[INHOUSE_SYSTEM_MODE]='PCS' 
	GROUP BY 
    A.[PLANT]
      ,A.[ASSEMBLY_LINE]
      ,C.[SUPPLIER_NUM]
      --,A.[MODEL]
      ,A.[PART_NO]
      ,A.[LOCATION]
      ,A.INHOUSE_PART_CLASS

	 
--更新计数器
UPDATE  A
SET     A.[INHOUSE_PACKAGE_MODEL] = INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE_MODEL] ,
        A.[INHOUSE_PACKAGE] = INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE]
FROM    [LES].[TT_PCS_COUNTER] A
        INNER JOIN ( SELECT [PLANT] ,
                            [ASSEMBLY_LINE] ,
                            [LOCATION] ,
                            [PART_NO] ,
                            [IN_PLANT_LOGISTIC_PART_CLASS] ,
                            MIN([INHOUSE_PACKAGE_MODEL]) AS [INHOUSE_PACKAGE_MODEL] ,
                            MIN([INHOUSE_PACKAGE]) AS [INHOUSE_PACKAGE]
                     FROM   [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD]
                     WHERE  [IN_PLANT_SYSTEM_MODE] = 'PCS'
                     GROUP BY [PLANT] ,
                            [ASSEMBLY_LINE] ,
                            [LOCATION] ,
                            [PART_NO] ,
                            [IN_PLANT_LOGISTIC_PART_CLASS]
                   ) AS INHOUSE_PULL_GROUP ON A.[PLANT] = INHOUSE_PULL_GROUP.[PLANT]
                                              AND A.[ASSEMBLY_LINE] = INHOUSE_PULL_GROUP.[ASSEMBLY_LINE]
                                              AND A.[LOCATION] = INHOUSE_PULL_GROUP.[LOCATION]
                                              AND A.[PART_NO] = INHOUSE_PULL_GROUP.[PART_NO]
                                              AND A.[IN_PLANT_LOGISTIC_PART_CLASS] = INHOUSE_PULL_GROUP.[IN_PLANT_LOGISTIC_PART_CLASS]
WHERE   ( A.[INHOUSE_PACKAGE] != INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE]
          OR A.[INHOUSE_PACKAGE_MODEL] != INHOUSE_PULL_GROUP.[INHOUSE_PACKAGE_MODEL]
        );
		  
--删除计数器数据
DELETE FROM [LES].[TT_PCS_COUNTER]  WHERE NOT	EXISTS (SELECT * FROM 
		   [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD] B WHERE [LES].[TT_PCS_COUNTER].[PLANT]=B.[PLANT] AND [LES].[TT_PCS_COUNTER].[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND 
		  [LES].[TT_PCS_COUNTER].[LOCATION]=B.[LOCATION] AND [LES].[TT_PCS_COUNTER].[PART_NO]=B.[PART_NO] AND [LES].[TT_PCS_COUNTER].[IN_PLANT_LOGISTIC_PART_CLASS]=B.[IN_PLANT_LOGISTIC_PART_CLASS] AND  B.[IN_PLANT_SYSTEM_MODE]='PCS'  )  


 UPDATE [LES].[TS_SYS_COMBINE_CONTROL] SET [ISPASS]=0 WHERE [SEQID]=1



  
	
END