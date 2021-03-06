﻿--SAP接口_MM_发票确认
CREATE PROCEDURE [LES].[PROC_INTERFACE_SAP_LES_INVOICE]
AS

BEGIN TRAN
BEGIN TRY
	DECLARE @DealSEQ BIGINT 
	SELECT @DealSEQ=MAX(m.SEQ_ID) FROM LES.TT_SPM_INVOICE_EXEC e,LES.TI_SPM_INVOICE_CONFIRM m 
		WHERE e.INVOICE_NO=m.INVOICE_NO
		  AND e.SUPPLIER_NUM=m.LIFNR 
		  AND ISNULL(m.DEAL_FLAG,0)=0
		  
	UPDATE LES.TT_SPM_INVOICE_EXEC 
	   SET INVOICE_STATUS=0
	      ,INVOICE_REASON=m.CONFIRM_REASON
		  ,UPDATE_DATE=GETDATE()
	  FROM LES.TT_SPM_INVOICE_EXEC e,LES.TI_SPM_INVOICE_CONFIRM m 
	 WHERE e.INVOICE_NO=m.INVOICE_NO
       AND e.SUPPLIER_NUM=m.LIFNR 
	   AND ISNULL(m.DEAL_FLAG,0)=0 
	   AND m.PROCESS_FLAG='E'
	   AND m.SEQ_ID<=@DealSEQ

	UPDATE LES.TT_SPM_INVOICE_EXEC 
	   SET INVOICE_STATUS=1
	      ,INVOICE_REASON='发票校验成功!'
		  ,UPDATE_DATE=GETDATE()
	  FROM LES.TT_SPM_INVOICE_EXEC e,LES.TI_SPM_INVOICE_CONFIRM m 
	 WHERE e.INVOICE_NO=m.INVOICE_NO
       AND e.SUPPLIER_NUM=m.LIFNR 
	   AND ISNULL(m.DEAL_FLAG,0)=0 
	   AND m.PROCESS_FLAG='S'
	   AND m.SEQ_ID<=@DealSEQ

	UPDATE LES.TI_SPM_INVOICE_CONFIRM 
		SET DEAL_FLAG=1
			,PROCESS_TIME=GETDATE()
		FROM LES.TT_SPM_INVOICE_EXEC e,LES.TI_SPM_INVOICE_CONFIRM m 
		WHERE e.INVOICE_NO=m.INVOICE_NO
		  AND e.SUPPLIER_NUM=m.LIFNR 
		  AND ISNULL(m.DEAL_FLAG,0)=0
		  AND m.SEQ_ID<=@DealSEQ
		 
	COMMIT TRAN
END TRY
BEGIN CATCH
	--出错，则返回执行不成功，回滚事务
	ROLLBACK TRAN
	--记录错误信息
	DECLARE @ERROR_ID NVARCHAR(MAX)
	SET @ERROR_ID = (
		SELECT CAST(SEQ_ID AS NVARCHAR(50))+','  FROM (
			SELECT m.SEQ_ID FROM LES.TT_SPM_INVOICE_EXEC e,LES.TI_SPM_INVOICE_CONFIRM m 
				WHERE e.INVOICE_NO=m.INVOICE_NO
				  AND e.SUPPLIER_NUM=m.LIFNR 
				  AND ISNULL(m.DEAL_FLAG,0)=0
				  AND m.SEQ_ID<=@DealSEQ
			) AS T
			FOR XML PATH('')
		)
	INSERT INTO [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
		SELECT getdate(),'INTERFACE','PROC_INTERFACE_SAP_LES_INVOICE','Procedure',ISNULL(error_message(),'')+';SEQ_ID:'+IsNull(@ERROR_ID,''),ERROR_LINE()
	--避免错误数据重复执行
	UPDATE LES.TI_SPM_INVOICE_CONFIRM 
		SET DEAL_FLAG=9
			,PROCESS_TIME=GETDATE()
		FROM LES.TT_SPM_INVOICE_EXEC e,LES.TI_SPM_INVOICE_CONFIRM m 
		WHERE e.INVOICE_NO=m.INVOICE_NO
		  AND e.SUPPLIER_NUM=m.LIFNR 
		  AND ISNULL(m.DEAL_FLAG,0)=0
		  AND m.SEQ_ID<=@DealSEQ
END CATCH