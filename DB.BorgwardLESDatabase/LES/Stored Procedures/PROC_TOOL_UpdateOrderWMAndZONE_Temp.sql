﻿

CREATE Procedure [LES].[PROC_TOOL_UpdateOrderWMAndZONE_Temp]
(
    @VBELN as nvarchar(12),
	@WM_NO as nvarchar(10),
	@ZONE_NO as  nvarchar(20)
)
AS

Begin Transaction Tran_UpdateOrderWMAndZONE
Begin Try

--修改4600000206订单
--Select * from [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH(NOLOCK)
--Where VBELN='4600000206'

Update [LES].[TI_SPM_DELIVERY_RUNSHEET_IN]
   Set LGORT=@ZONE_NO 
Where VBELN=@VBELN

--PROCESS_FLAG=1 已经处理

--计划送货单
--Select * from LES.TT_SPM_DELIVERY_RUNSHEET 
--Where PLAN_RUNSHEET_NO='4600000206'


Update LES.TT_SPM_DELIVERY_RUNSHEET 
   Set PLANT_ZONE=@ZONE_NO
 Where PLAN_RUNSHEET_NO=@VBELN

 --计划送货单明细
--Select * from LES.TT_SPM_DELIVERY_RUNSHEET_DETAIL
--Where PLAN_RUNSHEET_SN=5661

Update  LES.TT_SPM_DELIVERY_RUNSHEET_DETAIL
  Set WM_NO=@WM_NO
     ,ZONE_NO=@ZONE_NO
Where PLAN_RUNSHEET_SN=(Select TOP 1 PLAN_RUNSHEET_SN From LES.TT_SPM_DELIVERY_RUNSHEET 
                         Where PLAN_RUNSHEET_NO=@VBELN)


--退货的单（如果是退货单）
--Select * from [LES].[TT_WMM_RETURN]
--Where RETURN_NO='4600000206'

Update [LES].[TT_WMM_RETURN]
   Set WM_NO=@WM_NO
      ,ZONE_NO=@ZONE_NO
Where RETURN_NO=@VBELN

--退货单明细如果是退货单
--Select * from [LES].[TT_WMM_RETURN_DETAIL]

Update [LES].[TT_WMM_RETURN_DETAIL]
   Set WM_NO=@WM_NO
      ,ZONE_NO=@ZONE_NO
Where RETURN_ID=(Select top 1 RETURN_ID From [LES].[TT_WMM_RETURN]
                  Where RETURN_NO=@VBELN)

--BARCode
--Select * from LES.TT_SPM_DELIVERY_RUNSHEET_BARCODE

--收货单表
--Select * from LES.TT_WMM_RECEIVE
--  Where RECEIVE_NO=@VBELN

Update LES.TT_WMM_RECEIVE
   Set WM_NO=@WM_NO
      ,ZONE_NO=@ZONE_NO
  Where RECEIVE_NO=@VBELN

--收货单明细表
--Select * from LES.TT_WMM_RECEIVE_DETAIL
--Where RECEIVE_ID=(Select TOP 1 RECEIVE_ID from LES.TT_WMM_RECEIVE Where RECEIVE_NO=@VBELN)


Update LES.TT_WMM_RECEIVE_DETAIL
   Set WM_NO=@WM_NO
      ,ZONE_NO=@ZONE_NO
  Where RECEIVE_ID=(Select TOP 1 RECEIVE_ID from LES.TT_WMM_RECEIVE Where RECEIVE_NO=@VBELN)


--每次提交批次表（张恒添加）
--Select * from LES.TT_WMM_RECEIVE_DETAIL_LOG

--通知SAP接口表，由每次提交批次表生成
--Select * from LES.TI_WMS_RECIVE_OUT

Update LES.TI_WMS_RECIVE_OUT
   Set LGORT=@ZONE_NO
 Where VBELN=@VBELN


--提交后进入此表，修改状态,整理库存访问此表
--Select *  from  LES.TM_WMM_TRAN_DETAILS Where TRAN_NO=@VBELN

Update LES.TM_WMM_TRAN_DETAILS
   Set WM_NO=@WM_NO
      ,ZONE_NO=@ZONE_NO
 Where TRAN_NO=@VBELN

--提交操作日志表
--Select * From  LES.TM_WMM_TRAN_DETAILS_LOG Where TRAN_NO=@VBELN

Update LES.TM_WMM_TRAN_DETAILS_LOG
   Set WM_NO=@WM_NO
      ,ZONE_NO=@ZONE_NO
 Where TRAN_NO=@VBELN

 COMMIT Transaction Tran_UpdateOrderWMAndZONE
 Print('执行成功！')

 End Try
 Begin Catch
   Rollback Transaction Tran_UpdateOrderWMAndZONE
   Print('Error Message:' + error_message())
   Print('执行失败！')
 End Catch