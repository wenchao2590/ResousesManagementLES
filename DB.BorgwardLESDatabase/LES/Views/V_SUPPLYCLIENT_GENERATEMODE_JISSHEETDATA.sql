
/********************************************************************/
/*   Project Name:  JIS                                               */
/*   Program Name:  [V_SUPPLYCLIENT_GENERATEMODE_JISSHEETDATA]*/
/*   Called By:     供应商生成JIS单                                  */
/*    Author:       yingxuefeng                                      */
/********************************************************************/
CREATE VIEW [LES].[V_SUPPLYCLIENT_GENERATEMODE_JISSHEETDATA]
AS
SELECT     Tj.JIS_RUNSHEET_SN, Tj.JIS_RUNSHEET_NO, Tj.JIS_RUNSHEET_TIME, Tj.PLANT, Tj.ASSEMBLY_LINE, Tr.RACK_CNAME AS rack_description, 
                      Tr.IS_ARRANGEMENT_EMPTY, Tr.WAREHOUSE, Tj.LOCATION, Tj.ESTIMATED_ARRIVAL_TIME, Tj.CARS, Tj.RECKONING_NO, Tj.TRANS_SUPPLIER_NUM, Tj.RACK, 
                      Tj.JIS_SUPPLIER_SN, Tj.DOCK, Tj.FIRST_TIME, Tj.EXPECTED_ARRIVAL_TIME, Tj.SUPPLIER_CONFIRM_TIME, Tj.ACTUAL_ARRIVAL_TIME, Tj.PRINT_TYPE, 
                      Tj.FORMAT, Tj.START_RUNNING_NO, Tj.END_RUNNING_NO, Tj.COMMENTS, Tj.CREATE_DATE, Tj.FEEDBACK, Tj.BOOKKEEPER, Tj.REDO_FLAG, 
                      Tj.JIS_RUNSHEET_STATUS, Tj.SEND_STATUS, Tj.SEND_TIME, Tj.FAX_STATUS, Tj.FAX_TIME, Tj.SUPPLY_STATUS, Tj.SUPPLY_TIME, Tj.SAP_FLAG, Tj.RETRY_TIMES, 
                      SUBSTRING(Tj.JIS_RUNSHEET_NO, 1, LEN(Tj.JIS_RUNSHEET_NO) - 2) AS jis_runsheet_no1, SUBSTRING(Tj.JIS_RUNSHEET_NO, 11, 2) AS jis_runsheet_no2, 
                      Tj.WMS_SEND_STATUS, Ts.SUPPLIER_NAME, Tr.RACK_CNAME, Tr.RACK_ROW, Tr.RACK_COLUMN, Tr.UNLOADBLDG, Tj.SUPPLIER_NUM, Tj.PLANT_ZONE
FROM         LES.TT_JIS_RUNSHEET AS Tj INNER JOIN
                      LES.TM_JIS_RACK AS Tr ON Tj.PLANT = Tr.PLANT AND Tj.ASSEMBLY_LINE = Tr.ASSEMBLY_LINE AND Tj.TRANS_SUPPLIER_NUM = Tr.TRANSPORT_SUPPLIER AND
                       Tj.RACK = Tr.RACK INNER JOIN
                      LES.TM_BAS_SUPPLIER AS Ts ON Tj.TRANS_SUPPLIER_NUM = Ts.SUPPLIER_NUM