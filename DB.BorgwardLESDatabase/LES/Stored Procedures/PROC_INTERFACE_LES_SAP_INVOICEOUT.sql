﻿
--SAP接口_MM_发票上传给SAP接收
CREATE PROCEDURE [LES].[PROC_INTERFACE_LES_SAP_INVOICEOUT]
AS

DECLARE @INVOICE_NO NVARCHAR(200)
DECLARE @CURRENCY NVARCHAR(50)
DECLARE @ID NVARCHAR(19)

BEGIN	
	BEGIN TRANSACTION
	BEGIN TRY
		DECLARE INVOICE_CURS1 CURSOR FOR
			SELECT TOP 500 INVOICE_NO
		            ,CURRENCY 
				FROM LES.TT_SPM_INVOICE_EXEC 
				WHERE DEAL_DATE IS NULL
		OPEN INVOICE_CURS1
		FETCH NEXT FROM INVOICE_CURS1 INTO @INVOICE_NO,@CURRENCY
		WHILE(@@fetch_status=0)
		BEGIN
			--税务需要的ID
			--SET @ID=LES.FN_GETRANDSTR(19)

			--插入数据  
			INSERT INTO LES.TI_SPM_INVOICE_OUT(
					VBELN
					,POSNR
					,MATNR
					,LGMNG
					,MEINS
					,LIFNR
					,WERKS
					,BUDAT
					,MBLNR
					,MJAHR
					,ZEILE
					,BUDAT1
					,INVOICE_NO
					,INVOICE_AMOUNT
					,INVOICE_MONEY
					,LFART
					,CREATE_DATE
					,CREATE_USER
					,Z_SERIAL
					,SETTLE_AMOUNT
					,SAP_AMOUNT
					,TAX
					,MWSKZ
					,SCHPR
					,ID
				)
				SELECT	r.PLAN_NO
						,r.ITEM_NO
						,r.PART_NO
						,p.ACTUAL_INHOUSE_PACKAGE_QTY
						--,r.ACTUAL_INHOUSE_PACKAGE_QTY
						,r.MEASURING_UNIT_NO
						,r.SUPPLIER_NUM
						,r.PLANT
						,CONVERT(varchar(10),r.ACCOUNT_START_DATE,112)
						,r.PART_INENTITY
						,r.PART_INENTITY_YEAR
						,r.PART_INENTITY_ITEMNO
						,CONVERT(varchar(10),p.INVOICE_DATE, 112)
						,p.INVOICE_NO
						,p.INVOICE_AMOUNT
						,@CURRENCY
						,r.SHEET_TYPE
						,GETDATE()
						,'admin'
						,LES.FN_GETRANDSTR(19)
						,p.SETTLE_AMOUNT
						,p.SAP_AMOUNT
						,p.TAX
						,r.MWSKZ
						,r.SCHPR
						,p.OPERATION_ID
					FROM LES.TT_SPM_INVOICE_REQUEST r  
						Inner Join LES.TT_SPM_PAY_PLAN p ON (r.SEQ_ID=p.SEQ_ID)
					WHERE p.INVOICE_NO=@INVOICE_NO
						--r.PART_INENTITY=p.PART_INENTITY 
						--AND r.PART_INENTITY_YEAR=p.PART_INENTITY_YEAR 
						--AND r.PART_INENTITY_ITEMNO=p.PART_INENTITY_ITEMNO 
						--AND r.PART_NO=p.PART_NO 
						--AND r.PLANT=p.PLANT)
						--WHERE r.INVOICE_NO=@INVOICE_NO

			--更新数据
			UPDATE LES.TT_SPM_INVOICE_EXEC 
				SET DEAL_DATE=GETDATE() 
				WHERE INVOICE_NO=@INVOICE_NO
					And DEAL_DATE Is Null
		 
			FETCH NEXT FROM INVOICE_CURS1 INTO  @INVOICE_NO,@CURRENCY
		END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息  
		INSERT INTO [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
			SELECT getdate(),'INTERFACE','PROC_INTERFACE_LES_SAP_INVOICEOUT','Procedure',ISNULL(error_message(),'')+';INVOICE_NO:'+IsNull(@INVOICE_NO,''),ERROR_LINE()
		--标记错误数据
		UPDATE LES.TT_SPM_INVOICE_EXEC 
			SET DEAL_DATE='1900-1-1'
			WHERE INVOICE_NO=@INVOICE_NO
				And DEAL_DATE Is Null
	END CATCH

	CLOSE  INVOICE_CURS1
	DEALLOCATE INVOICE_CURS1
END