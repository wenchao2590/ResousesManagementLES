
/********************************************************************/
/*   Project Name:  TWD                                               */
/*   Program Name:  [V_TWD_BARCODE]*/
/*   Called By:     TWD条码                                       */
/*    Author:       yingxuefeng                                      */
/********************************************************************/
CREATE VIEW [LES].[V_TWD_BARCODE]
AS 
SELECT A.PLANT,
	   f.SAP_PLANT_CODE,
       A.ASSEMBLY_LINE,
       SUPPLIER_NAME,
       BARCODE_DETAIL_ID,
       TWD_RUNSHEET_SN,
       A.SUPPLIER_NUM,
       PART_NO,
       BARCODE_DATA,
       IDENTIFY_PART_NO,
       PART_CNAME,
       BOX_PARTS,
       PICKUP_SEQ_NO,
       RDC_DLOC,
       MEASURING_UNIT_NO,
       INNER_LOCATION,
       LOCATION,
       STORAGE_LOCATION,
       A.INHOUSE_PACKAGE_MODEL,
       B.INHOUSE_PACKAGE_MODEL_NAME,
       REQUIRED_INBOUND_PACKAGE_QTY,
       BARCODE_TYPE,
       BATTH_NO,
       PRINT_TIMES,
       PRINT_DATE,
       PACHAGE_TYPE,
       A.COMMENTS,
       PRINT_TYPE,
       a.TWD_RUNSHEET_NO
FROM   (
           SELECT b.PLANT,
                  b.ASSEMBLY_LINE,
                  c.SUPPLIER_NAME,
                  BARCODE_DETAIL_ID,
                  a.TWD_RUNSHEET_SN,
                  a.SUPPLIER_NUM,
                  PART_NO,
                  BARCODE_DATA,
                  IDENTIFY_PART_NO,
                  PART_CNAME,
                  a.BOX_PARTS,
                  PICKUP_SEQ_NO,
                  RDC_DLOC,
                  MEASURING_UNIT_NO,
                  INNER_LOCATION,
                  LOCATION,
                  STORAGE_LOCATION,
                  INHOUSE_PACKAGE_MODEL,
                  REQUIRED_INBOUND_PACKAGE_QTY,
                  BARCODE_TYPE,
                  BATTH_NO,
                  PRINT_TIMES,
                  PRINT_DATE,
                  a.PACHAGE_TYPE,
                  a.COMMENTS,
                  1 AS PRINT_TYPE,
                  b.TWD_RUNSHEET_NO AS TWD_RUNSHEET_NO
           FROM   [LES].[TT_TWD_RUNSHEET_BARCODE] a
                  JOIN [LES].[TT_TWD_RUNSHEET] b
                       ON  a.TWD_RUNSHEET_SN = b.TWD_RUNSHEET_SN
                  JOIN [LES].[TM_BAS_SUPPLIER] c
                       ON  c.SUPPLIER_NUM = a.SUPPLIER_NUM
           
           UNION ALL
           
           SELECT d.PLANT,
                  d.ASSEMBLY_LINE,
                  e.SUPPLIER_NAME,
                  BARCODE_DETAIL_ID,
                  d.TWD_RUNSHEET_SN,
                  d.SUPPLIER_NUM,
                  PART_NO,
                  BARCODE_DATA,
                  IDENTIFY_PART_NO,
                  PART_CNAME,
                  d.BOX_PARTS,
                  PICKUP_SEQ_NO,
                  RDC_DLOC,
                  MEASURING_UNIT_NO,
                  INNER_LOCATION,
                  LOCATION,
                  STORAGE_LOCATION,
                  INHOUSE_PACKAGE_MODEL,
                  REQUIRED_INBOUND_PACKAGE_QTY,
                  BARCODE_TYPE,
                  BATTH_NO,
                  PRINT_TIMES,
                  PRINT_DATE,
                  d.PACHAGE_TYPE,
                  d.COMMENTS,
                  2 AS PRINT_TYPE,
                  '' AS TWD_RUNSHEET_NO
           FROM   [LES].TT_TWD_MANUAL_BARCODE d
                  JOIN [LES].[TM_BAS_SUPPLIER] e
                       ON  e.SUPPLIER_NUM = d.SUPPLIER_NUM
       )A
       LEFT  JOIN [LES].[TT_TWD_PACKAGE_MODEL] B
            ON  A.INHOUSE_PACKAGE_MODEL = B.[INHOUSE_PACKAGE_MODEL]
            
       LEFT JOIN  [LES].[TM_BAS_PLANT] f ON f.PLANT=a.PLANT