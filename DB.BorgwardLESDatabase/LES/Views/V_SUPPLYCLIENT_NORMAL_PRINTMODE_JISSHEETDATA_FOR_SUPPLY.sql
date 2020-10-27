


/*******************************************************************
                                                                  
   Project Name:  Production Pull System                          
   Program Name:  V_SUPPLYCLIENT_NORMAL_PRINTMODE_JISSHEETDATA                       
   Called By:     by the Page										
   Purpose:       服务供应商							       
   author:       wangchanghong	2011-06-28   				       
*******************************************************************/
CREATE VIEW [LES].[V_SUPPLYCLIENT_NORMAL_PRINTMODE_JISSHEETDATA_FOR_SUPPLY]
AS
SELECT     Rs.SEND_SEQ, Tj.JIS_RUNSHEET_SN, Tj.JIS_RUNSHEET_NO, Tj.JIS_RUNSHEET_TIME, Tj.PLANT, Tj.ASSEMBLY_LINE, Tr.RACK_CNAME AS rack_description, 
                      Tr.IS_ARRANGEMENT_EMPTY, Tr.WAREHOUSE, Tj.LOCATION, Tj.ESTIMATED_ARRIVAL_TIME, Tj.CARS, Tj.RECKONING_NO, Tj.SUPPLIER_NUM, Tj.RACK, 
                      Tj.JIS_SUPPLIER_SN, Tj.DOCK, Tj.FIRST_TIME, Tj.TRANS_SUPPLIER_NUM, Tj.SUPPLY_STATUS, Tj.EXPECTED_ARRIVAL_TIME, Tj.SUPPLIER_CONFIRM_TIME, 
                      Tj.ACTUAL_ARRIVAL_TIME, Tj.PRINT_TYPE, Tj.FORMAT, Tj.START_RUNNING_NO, Tj.END_RUNNING_NO, Tj.COMMENTS, Tj.CREATE_DATE, Tj.FEEDBACK, 
                      Tj.BOOKKEEPER, Tj.REDO_FLAG, Tj.JIS_RUNSHEET_STATUS, Rs.SEND_STATUS, Tj.SEND_TIME, Tj.FAX_STATUS, Tj.FAX_TIME, Tj.SUPPLY_TIME, Tj.SAP_FLAG, 
                      Tj.RETRY_TIMES, SUBSTRING(Tj.JIS_RUNSHEET_NO, 1, LEN(Tj.JIS_RUNSHEET_NO) - 2) AS jis_runsheet_no1, SUBSTRING(Tj.JIS_RUNSHEET_NO, 11, 2) 
                      AS jis_runsheet_no2, Ts.SUPPLIER_NAME, Tr.RACK_CNAME, Tr.RACK_ROW, Tr.RACK_COLUMN, Tr.UNLOADBLDG, Tj.PLANT_ZONE,Rs.SUPPLIER_NUM as supplier_num_Services
FROM         LES.TT_JIS_RUNSHEET AS Tj INNER JOIN
                      LES.TT_JIS_RUNSHEET_SEND_SUPPLY AS Rs ON Tj.JIS_RUNSHEET_SN = Rs.JIS_RUNSHEET_SN INNER JOIN
                      LES.TM_JIS_RACK AS Tr ON Tj.PLANT = Tr.PLANT AND Tj.ASSEMBLY_LINE = Tr.ASSEMBLY_LINE AND Tj.SUPPLIER_NUM = Tr.SUPPLIER_NUM AND 
                      Tj.RACK = Tr.RACK INNER JOIN
                      LES.TM_BAS_SUPPLIER AS Ts ON Tj.SUPPLIER_NUM = Ts.SUPPLIER_NUM