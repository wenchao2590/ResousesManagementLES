


/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  PROC_BAS_INBOUND_PULL_COMBINE_TWD_COUNTER                */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       wangchanghong	2011-06-20   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_BAS_INBOUND_PULL_COMBINE_TWD_COUNTER]
 (
	@LogicalPK nvarchar(50) ,
	@TermalPK nvarchar(50) 
 )
AS

BEGIN


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
           ,[CREATE_DATE])
       
	  SELECT distinct  A.[PLANT]
      ,A.[ASSEMBLY_LINE]
      ,A.[SUPPLIER_NUM]
      ,A.[MODEL]
      ,A.[PART_NO]
      ,A.[INDENTIFY_PART_NO]
      ,A.[AMOUNTRATIO]
      ,A.[INBOUND_PART_CLASS]
      ,A.[MEASURING_UNIT_NO]
      ,A.[PART_CNAME]
      ,A.[PART_ENAME]
      ,A.[DOCK]
      ,A.[INBOUND_PACKAGE_MODEL]
      ,A.[INBOUND_PACKAGE]
      ,0 
      ,getdate()
    FROM [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]  A
    WHERE A.LOGICAL_PK = @LogicalPK AND A.TERMAL_PK = @TermalPK AND
    
    not exists (SELECT * FROM 
		   [LES].[TT_TWD_CONSUME_COUNTER] B WHERE A.[PLANT]=B.[PLANT] AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
		  A.[MODEL]=B.[MODEL] AND A.[PART_NO]=B.[PART_NO]) AND  A.[INBOUND_SYSTEM_MODE]='TWD' 


--更新计数器
/*UPDATE A SET  A.[INHOUSE_PACKAGE_MODEL]=B.[INHOUSE_PACKAGE_MODEL]
           ,A.[INHOUSE_PACKAGE]=B.[INHOUSE_PACKAGE]
    FROM [LES].[TT_TWD_CONSUME_COUNTER] A INNER JOIN 
    [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD]  B  ON A.[PLANT]=B.[PLANT] AND A.[ASSEMBLY_LINE]=B.[ASSEMBLY_LINE] AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND
		  A.[LOCATION]=B.[LOCATION] and A.[PART_NO]=B.[PART_NO]
    INNER JOIN [LES].[TM_PCS_ROUTE_BOX_PARTS] C ON A.[PLANT]=C.[PLANT] AND A.[ASSEMBLY_LINE]=C.[ASSEMBLY_LINE]  and A.IN_PLANT_LOGISTIC_PART_CLASS=c.BOX_PARTS
    WHERE B.[IN_PLANT_SYSTEM_MODE]='PCS'  AND (A.[INHOUSE_PACKAGE]!=B.[INHOUSE_PACKAGE] OR A.[INHOUSE_PACKAGE_MODEL]!=B.[INHOUSE_PACKAGE_MODEL])
*/		  
--删除计数器数据



 

	
END