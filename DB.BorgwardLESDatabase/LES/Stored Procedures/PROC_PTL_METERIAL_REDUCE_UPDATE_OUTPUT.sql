﻿/********************************************************************/
/*   Project Name:  PTL												*/
/*   Program Name:  [LES].[PROC_PTL_METERIAL_REDUCE_UPDATE_OUTPUT]	*/
/*   Called By:     window service									*/
/*   Author:        孙述霄											*/
/*   Date:			2017-11-17										*/
/*   Note:			PTL物料扣减更新出库数量							*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PTL_METERIAL_REDUCE_UPDATE_OUTPUT]
(
	@FID UNIQUEIDENTIFIER,
	@LOGINNAME NVARCHAR(32)
)
AS
BEGIN
	SET XACT_ABORT ON
	SET NOCOUNT ON
	BEGIN TRY

		BEGIN TRANSACTION
			--执行中
			EXEC [LES].[PROC_SYS_INBOUND_UPDATE] @FID, 1, @LOGINNAME

			--将数据插入历史数据表
			INSERT INTO LES.TI_MID_PTL_MATERIAL_REDUCE_HISTORY(FID,OrderNo,OrderStatus,VIN,SeqNo,PartNo,PartName,Qty,BoxPart,OrderSyncTime,CreateTime,PLANT,WM_NO)
			SELECT A.FID,A.OrderNo,A.OrderStatus,A.VIN,A.SeqNo,A.PartNo,A.PartName,A.Qty,A.BoxPart,A.CreateTime,GETDATE(),PLANT,B.WM_NO FROM LES.TI_MID_PTL_MATERIAL_REDUCE A WITH(NOLOCK) 
			JOIN LES.TT_WMM_OUTPUT B WITH (NOLOCK) ON A.OrderNo = B.OUTPUT_NO
			 WHERE FID = @FID

			--更新出库数量
			UPDATE C SET C.ACTUAL_QTY = ISNULL(C.ACTUAL_QTY,0) + ISNULL(A.Qty,0)
			FROM LES.TI_MID_PTL_MATERIAL_REDUCE A WITH(NOLOCK)
			JOIN LES.TT_WMM_OUTPUT B WITH (NOLOCK) ON A.OrderNo = B.OUTPUT_NO
			JOIN LES.TT_WMM_OUTPUT_DETAIL C WITH(NOLOCK) ON B.OUTPUT_ID = C.OUTPUT_ID AND A.PartNo = C.PART_NO AND A.BoxPart = C.TARGET_ZONE
			WHERE A.FID = @FID

			--写入TRAN_DETAILS
			INSERT INTO LES.TM_WMM_TRAN_DETAILS
			(
				TRAN_NO,PLANT,BATCH_NO,PART_NO,BARCODE_DATA,
				WM_NO,ZONE_NO,DLOC,TARGET_WM,TARGET_ZONE,
				TARGET_DLOC,MEASURING_UNIT_NO,PACKAGE,NUM,BOX_NUM,
				TRAN_STATE,TRAN_DATE,SUPPLIER_NUM,PART_CNAME,BOX_PARTS,
				TRAN_TYPE,CREATE_USER,CREATE_DATE
			)
			SELECT A.OrderNo,B.PLANT,NULL,C.PART_NO,C.BARCODE_DATA,
				B.WM_NO,B.ZONE_NO,C.DLOC,C.TARGET_WM,C.TARGET_ZONE,
				C.TARGET_DLOC,C.MEASURING_UNIT_NO,C.PACKAGE,C.NUM,C.BOX_NUM,
				0,GETDATE(),C.SUPPLIER_NUM,C.PART_CNAME,C.BOX_PARTS,2,
				@LOGINNAME,GETDATE()
			 FROM LES.TI_MID_PTL_MATERIAL_REDUCE A WITH(NOLOCK)
			JOIN LES.TT_WMM_OUTPUT B WITH (NOLOCK) ON A.OrderNo = B.OUTPUT_NO
			JOIN LES.TT_WMM_OUTPUT_DETAIL C WITH(NOLOCK) ON B.OUTPUT_ID = C.OUTPUT_ID AND A.PartNo = C.PART_NO AND A.BoxPart = C.TARGET_ZONE
			WHERE A.FID = @FID 

			--取已关单的订单
			SELECT DISTINCT OrderNo INTO #CLOSEORDER FROM LES.TI_MID_PTL_MATERIAL_REDUCE A WITH(NOLOCK) WHERE A.FID = @FID AND OrderStatus = 1

			--物料扣减单据状态为已关单则更新出库单状态为已关闭。
			UPDATE A SET CONFIRM_FLAG = 10 FROM LES.TT_WMM_OUTPUT A WITH(NOLOCK) WHERE OUTPUT_NO IN (SELECT OrderNo FROM #CLOSEORDER)
			--物料扣减单据状态为已关单则更新拉动单状态为已关闭。
			UPDATE A SET Sheet_Status = 1 FROM LES.TT_SPS_RUNSHEET A WITH(NOLOCK) WHERE SPS_RUNSHEET_NO IN (SELECT OrderNo FROM #CLOSEORDER)

			--执行成功
			EXEC [LES].[PROC_SYS_INBOUND_UPDATE] @FID, 2, @LOGINNAME
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'PTL', '[LES].[PROC_PTL_METERIAL_REDUCE_UPDATE_OUTPUT]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()

		--执行失败
		EXEC [LES].[PROC_SYS_INBOUND_UPDATE] @FID, 4, @LOGINNAME
	END CATCH
END