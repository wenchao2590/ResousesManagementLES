﻿

/********************************************************************/
/*   Project Name:  [PROC_RP_STOCKOUT_INVENTORY_COMPARISON]						*/
/*   Program Name:													*/
/*   Called By:     by the 	Day		2017-12-18				*/
/*   Purpose:       报表-14欠交与库存对比报表								*/
/*   author:       吴庆超	2017-12-11   				        */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_RP_STOCKOUT_INVENTORY_COMPARISON] (
    @BEGINDATETIME DATETIME,
    @ENDDATETIME DATETIME,
    @CREATE_USER VARCHAR(20),
    @COMMENTS VARCHAR(200))
AS
BEGIN

    INSERT INTO LES.RP_STOCKOUT_INVENTORY_COMPARISON (PLAN_RUNSHEET_SN, PLANT, PLANT_ZONE,WM_NO, ZONE_NO, SUPPLIER_NUM, SUPPLIER_NAME,
                                             PART_NO, PART_CNAME, STOCKOUT_NUM, EXPECTED_ARRIVAL_TIME, DATE_DAY,
                                             SHEET_STATUS, RUNSHEET_TYPE, AVAILBLE_STOCKS,DISPO, COMMENTS, UPDATE_DATE,
                                             UPDATE_USER, CREATE_DATE, CREATE_USER)
 
	 SELECT TABLEA.PLAN_RUNSHEET_SN, C.PLANT,TABLEA.PLANT_ZONE, C.WM_NO, C.ZONE_NO, TABLEA.SUPPLIER_NUM, TABLEA.SUPPLIER_NAME,
                                             C.PART_NO, TABLEA.PART_CNAME, TABLEA.STOCKOUT_NUM, EXPECTED_ARRIVAL_TIME,TABLEA.DATE_DAY,
											 TABLEA.SHEET_STATUS,TABLEA.RUNSHEET_TYPE,C.AVAILBLE_STOCKS,TABLEA.DISPO,@COMMENTS AS COMMENTS  ,N'' AS UPDATE_DATE ,N''AS UPDATE_USER ,GETDATE() AS CREATE_DATE,@CREATE_USER AS CREATE_USER
	  FROM (  ( SELECT A.PLAN_RUNSHEET_SN, A.PLANT,A.PLANT_ZONE, B.WM_NO, B.ZONE_NO, A.SUPPLIER_NUM,s.SUPPLIER_NAME, B.PART_NO, B.PART_CNAME,
					  (B.REQUIRED_INHOUSE_PACKAGE
					   - CASE
							  WHEN B.ACTUAL_INHOUSE_PACKAGE_QTY IS NULL THEN '0'
							  WHEN B.ACTUAL_INHOUSE_PACKAGE_QTY > 0 THEN B.ACTUAL_INHOUSE_PACKAGE_QTY END) AS STOCKOUT_NUM,
					  A.EXPECTED_ARRIVAL_TIME, CONVERT(CHAR(10), A.EXPECTED_ARRIVAL_TIME, 23) AS DATE_DAY, 
					  A.SHEET_STATUS, a.RUNSHEET_TYPE, b.DISPO
				 FROM LES.TT_SPM_DELIVERY_RUNSHEET A
				 inner JOIN LES.TT_SPM_DELIVERY_RUNSHEET_DETAIL B
				   ON A.PLAN_RUNSHEET_SN = B.PLAN_RUNSHEET_SN --AND a.SEND_STATUS=9
				  AND A.RUNSHEET_TYPE NOT IN ( -3, -11, -15, -22, -32, -50, -51 )
				  LEFT JOIN LES.TM_BAS_SUPPLIER S
				  ON S.SUPPLIER_NUM = A.SUPPLIER_NUM
				  --AND A.SHEET_STATUS     <> 10
				  ) AS TABLEA
	  LEFT JOIN (SELECT PLANT, PART_NO, WM_NO, ZONE_NO, SUM(AVAILBLE_STOCKS) AS AVAILBLE_STOCKS
				   FROM LES.TT_WMS_STOCKS
				  GROUP BY PLANT, PART_NO, WM_NO, ZONE_NO) C
		ON TABLEA.WM_NO   = C.WM_NO
	   AND TABLEA.ZONE_NO = C.ZONE_NO
	   AND TABLEA.PART_NO = C.PART_NO
	   AND C.PLANT        = TABLEA.PLANT)
           WHERE EXPECTED_ARRIVAL_TIME > @BEGINDATETIME
             AND EXPECTED_ARRIVAL_TIME < @ENDDATETIME
         --WHERE A.CREATE_DATE>'2017-10-11 00:00:00.000' AND A.CREATE_DATE<'2017-10-12 00:00:00.000'
          
END;