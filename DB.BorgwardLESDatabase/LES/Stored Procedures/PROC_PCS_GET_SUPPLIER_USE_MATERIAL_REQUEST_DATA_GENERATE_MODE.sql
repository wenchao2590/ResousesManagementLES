
/********************************************************************/
/*                                                                  */
/*   Project Name:  PROC_PCS_GET_SUPPLIER_USE_MATERIAL_REQUEST_DATA_GENERATE_MODE   */
/*   Program Name:               */
/*   Called By:     by the 		*/
/*   Purpose:       get request data      */
/*   author:       xuehaijun	2011-07-25   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_GET_SUPPLIER_USE_MATERIAL_REQUEST_DATA_GENERATE_MODE]
(
	@TopCount		 int,
	@SupplierNum	 nvarchar(12),
	@WmsSendStatus	 int
)
AS
BEGIN
	DECLARE @SqlStr nvarchar(2000)
	set @SqlStr='SELECT TOP '+ Convert(nvarchar(20),@TopCount,20) + ' INTERFACE_ID, ASSEMBLY_LINE, PLANT, LOCATION, REQUEST_TIME, 
            INTERFACE_STATUS, PROCESS_TIME, PART_NO, PART_CNAME, PART_ENAME,RIGHT(''0000000000'' + rtrim(SUPPLIER_NUM),10) as SUPPLIER_NUM, DOCK, BOX_PARTS, 
            INTERFACE_TYPE, PACK_COUNT, INHOUSE_PACKAGE_MODEL, INHOUSE_PACKAGE, MEASURING_UNIT_NO, 
            EXPECTED_ARRIVAL_TIME, RDC_DLOC,COMMENTS,CREATE_DATE 
   FROM LES.TI_PCS_MATERIAL_REQUESTS 
   WHERE supplier_num = '+ @SupplierNum +' and (wms_send_status = ' + Convert(nvarchar(20),@WmsSendStatus,20) + ' or wms_send_status=5)
   ORDER BY CREATE_DATE '
   
   EXECUTE sp_executesql @SqlStr

END