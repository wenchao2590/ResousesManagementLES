﻿CREATE VIEW [LES].[V_BAS_PULL_ORDERS_PARTS_RESULT]
AS
SELECT   LES.TT_BAS_PULL_ORDERS.KNR, LES.TT_BAS_PULL_ORDERS.WERK, LES.TT_BAS_PULL_ORDERS.MODEL, 
                LES.TT_BAS_PULL_ORDERS.FARBAU, LES.TT_BAS_PULL_ORDERS.FARBIN, LES.TT_BAS_PULL_ORDERS.VORSERIE, 
                LES.TM_ODS_ORDER_PART_RESULTS.UNIT_ID, LES.TM_ODS_ORDER_PART_RESULTS.QUANTITY, 
                LES.TM_ODS_ORDER_PART_RESULTS.PART_NO
FROM      LES.TT_BAS_PULL_ORDERS INNER JOIN
                LES.TM_ODS_ORDER_PART_RESULTS ON 
                LES.TT_BAS_PULL_ORDERS.SIGNATURE = LES.TM_ODS_ORDER_PART_RESULTS.SIGNATURE