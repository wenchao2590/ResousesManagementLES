
/********************************************************************/
/*   Project Name:  ODS                                               */
/*   Program Name:  [TV_ODS_PCS_RUNSHEET_DETAIL_STATISTICS]*/
/*   Called By:     RunSheet明细统计                                  */
/*    Author:       yingxuefeng                                      */
/********************************************************************/
CREATE VIEW [LES].[TV_ODS_PCS_RUNSHEET_DETAIL_STATISTICS]
AS
SELECT [PLANT]
      ,[ASSEMBLY_LINE] 
      ,[SUPPLIER_NUM]
      ,[PART_NO] 
      ,[BOX_PARTS]      
      ,SUM([REQUIRED_INHOUSE_PACKAGE]) AS REQUIRED_INHOUSE_PACKAGE
      ,SUM([REQUIRED_INHOUSE_PACKAGE_QTY]) REQUIRED_INHOUSE_PACKAGE_QTY
  FROM [LES].[TT_PCS_RUNSHEET_DETAIL]
GROUP BY PLANT,ASSEMBLY_LINE,SUPPLIER_NUM,PART_NO,BOX_PARTS