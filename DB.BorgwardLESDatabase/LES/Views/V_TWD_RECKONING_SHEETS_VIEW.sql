
/********************************************************************/
/*   Project Name:  TWD                                              */
/*   Program Name:  [V_TWD_RECKONING_SHEETS_VIEW]*/
/*   Called By:     TWD清算单视图                                    */
/*    Author:       yingxuefeng                                      */
/********************************************************************/
CREATE VIEW [LES].[V_TWD_RECKONING_SHEETS_VIEW]
AS
    SELECT  b.ASSEMBLY_LINE ,
            b.BOX_PARTS ,
            b.PUBLISH_TIME  ,
            b.SUGGEST_DELIVERY_TIME  ,
            b.RUNSHEET_TYPE  ,
            b.DOCK ,
            b.DELIVERY_LOCATION 
			--modify by【运维】hx 2014/04/01【CMDB编号：CR-LES-20140402】start
			--修改工厂、期望到达时间、拉动单号、实际到送时间字段从送货单中获取
			--,b.PLANT 
			--,b.EXPECTED_ARRIVAL_TIME
			--,b.TWD_RUNSHEET_NO
			--,b.ACTUAL_ARRIVAL_TIME
            ,a.[EXPECTED_ARRIVAL_TIME]
		  ,a.[TWD_RUNSHEET_NO]
		  ,a.[PLANT]
		  ,a.[ACTUAL_ARRIVAL_TIME]
		  --modify by【运维】hx 2014/04/01 end
          ,a.[RECKONING_SN]
		  ,a.[TWD_RUNSHEET_SN]
		  ,a.[RECKONING_NO]
		  ,a.[DELIVERY_ORDER]
		  ,a.[ORDER_NO]
		  ,a.[RECEIVE_LOCATION]
		  ,a.[SUPPLIER_NUM]
		  ,a.[SUPPLIER_TRANS]
		  ,a.[WMS_SEND_STATUS]
		  ,a.[TRANS_TYPE]
		  ,a.[WMS_SEND_TIME]
		  ,a.[UNLOAD_PLACE]
		  ,a.[LOAD_PLACE]
		  ,a.[PULL_TYPE]
		  ,a.[PULL_DETAIL]
		  ,a.[RECEIVED_DATE]
		  ,a.[SUPPLY_STATUS]
		  ,a.[SUPPLY_CONFIRM_DATE]
		  ,a.[FIRST_CONFIRM_STATUS]
		  ,a.[FIRST_CONFIRM_DATE]
		  ,a.[SECOND_CONFIRM_STATUS]
		  ,a.[SECOND_CONFIRM_DATE]
		  ,a.[COMMENTS]
		  ,a.[UPDATE_DATE]
		  ,a.[UPDATE_USER]
		  ,a.[CREATE_DATE]
		  ,a.[CREATE_USER]
		  ,a.[PACKAGE_TYPE]
		  ,a.[WHAREHOUSE]
		  ,a.[INVOICENO]
		  ,a.[REC_STATUS]
		  ,a.[SEND_TIME]
		  ,a.[SEND_STATUS]
    FROM    LES.TT_TWD_RECKONING_SHEETS a
    LEFT JOIN LES.TT_TWD_RUNSHEET b ON b.TWD_RUNSHEET_SN = a.TWD_RUNSHEET_SN