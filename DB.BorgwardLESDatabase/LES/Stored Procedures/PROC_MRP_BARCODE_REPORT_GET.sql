
CreaTE PROCEDURE [LES].[PROC_MRP_BARCODE_REPORT_GET]

(
	@REQUEST_LIST NVARCHAR(MAX)	
)

AS
BEGIN

SELECT B.*,P.SUPPLIER_NUM,P.SUPPLIER_NAME,C.PART_NO,C.PART_CNAME,C.INBOUND_PACKAGE
FROM LES.TT_MRP_REQUEST_LIST_BARCODE AS B
INNER JOIN LES.TT_MRP_PARTS_LIST_CONFIRM AS C ON  B.CLIENT = C.CLIENT 
                                                 AND B.SEND_PORT = C.SEND_PORT 
                                                 AND B.IDOC = C.IDOC
                                                 AND B.LINE_NO = C.LINE_NO
INNER JOIN LES.TT_MRP_DELIVERY_PLAN_CONFIRM AS P ON B.CLIENT = P.CLIENT 
                                                 AND B.SEND_PORT = P.SEND_PORT 
                                                 AND B.IDOC = P.IDOC


END