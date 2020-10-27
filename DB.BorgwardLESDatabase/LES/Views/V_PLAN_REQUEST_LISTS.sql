



/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  [V_TI_VEHICLE_STATUS]                        */
/*   Called By:     by the Page										*/
/*   Purpose:       生成计划请求							       */
/*   author:       wangchanghong	2011-06-28   				       */
/********************************************************************/
CREATE view [LES].[V_PLAN_REQUEST_LISTS] as


select a.PLAN_SN,c.PLANT,a.SUPPLIER_NUM, a.SUPPLIER_NAME,a.CONTRACT_NO,b.PART_NO,b.PART_CNAME,b.MATERIAL_STATE,
		  b.PLANNING_CLERK,b.PLANNING_CLERK_NAME,b.PLACE_OF_DELIVERY,b.UNIT,b.ACCUMULATE_PLAN_AMOUNT,
		  b.ACCUMULATE_DELIVERY_AMOUNT,b.LAST_DELIVERY_AMOUNT,b.LAST_DELIVERY_DATE,b.LAST_IDOC,
          a.CLIENT,a.SEND_PORT,b.LINE_NO,a.IDOC,b.INBOUND_PACKAGE
from LES.TT_MRP_DELIVERY_PLAN_CONFIRM a
	 inner join LES.TT_MRP_PARTS_LIST_CONFIRM b on a.CLIENT=b.CLIENT and a.SEND_PORT=b.SEND_PORT and a.IDOC=b.IDOC
	 inner join LES.TM_BAS_PLANT c ON a.PLANT = c.SAP_PLANT_CODE