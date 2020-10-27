
--SAP接口_MM_待开票信息发布ORDER_NO  MEASURING_UNIT_NO
CREATE PROCEDURE [LES].[PROC_INTERFACE_SAP_LES_REQUEST] 
AS
BEGIN TRAN
BEGIN TRY
	DECLARE @SEQ_ID BIGINT;
	DECLARE @Record_Num INT;
	DECLARE @Record_ID INT;
	DECLARE @MATNR VARCHAR(20);--零件（物料)号
	DECLARE @MBLNR VARCHAR(30);--物料凭证
	DECLARE @MJAHR VARCHAR(30);--物料凭证年度
	DECLARE @ZEILE VARCHAR(30);--物料凭证行项目
	DECLARE @POSNR NVARCHAR(18);--行项目
	DECLARE @VBELN NVARCHAR(10);--订单号码
	DECLARE @WERKS NVARCHAR(5);--工厂

	DECLARE REQUEST_CURS1 CURSOR FOR
	SELECT TOP 2000
		[SEQ_ID],
		ISNULL([MBLNR], '') AS [MBLNR],
		[MJAHR],
		[ZEILE],
		[POSNR],
		[VBELN],--jinmiao20180115
		[MATNR],
		[WERKS]
	FROM [LES].[TI_SPM_UNPAY_IN] WITH (NOLOCK)
	WHERE ISNULL([PROCESS_FLAG], 0) = 0
	ORDER BY [SEQ_ID]

	OPEN REQUEST_CURS1
	FETCH NEXT FROM REQUEST_CURS1 INTO @SEQ_ID,@MBLNR,@MJAHR,@ZEILE,@POSNR,@VBELN,@MATNR,@WERKS
	WHILE( @@fetch_status = 0 )
	BEGIN
		SELECT
			@Record_Num=COUNT(1) 
		FROM [LES].[TT_SPM_INVOICE_REQUEST] WITH (NOLOCK)
		WHERE ISNULL([PART_INENTITY], '')=@MBLNR --物料凭证
		AND [PART_INENTITY_YEAR]=@MJAHR--物料凭证年度
		AND [PART_INENTITY_ITEMNO]=@ZEILE 
		AND [ITEM_NO]=@POSNR 
		AND [PLAN_NO]=@VBELN --jinmiao20180115
		AND [PART_NO]=@MATNR --零件（物料)号
		AND [PLANT]=@WERKS

		IF(@Record_Num>0)
		BEGIN
	
			SELECT TOP 1
				@Record_ID=[SEQ_ID]
			FROM [LES].[TT_SPM_INVOICE_REQUEST] WITH (NOLOCK)
			WHERE ISNULL([PART_INENTITY], '')=@MBLNR        --物料凭证
			AND [PART_INENTITY_YEAR]=@MJAHR   --物料凭证年度
			AND [PART_INENTITY_ITEMNO]=@ZEILE 
			AND [ITEM_NO]=@POSNR 
			AND [PLAN_NO]=@VBELN --jinmiao20180115
			AND [PART_NO]=@MATNR --零件（物料)号
			AND [PLANT]=@WERKS

			--更新
			UPDATE t_request 
			   SET --PLAN_NO=t_UNPAY_IN.VBELN    --订单号码
				  --,ITEM_NO=t_UNPAY_IN.POSNR    --行项目号码
				  PART_NO=t_UNPAY_IN.MATNR    --零件（物料)号
				  ,ACTUAL_INHOUSE_PACKAGE_QTY=t_UNPAY_IN.LGMNG --实际数量
				  ,MEASURING_UNIT_NO=t_UNPAY_IN.MEINS --数量单位
				  ,SUPPLIER_NUM=t_UNPAY_IN.LIFNR   --供应商代码
				  ,PLANT=t_UNPAY_IN.WERKS          --工厂
				  ,RECIEVE_DOCK=t_UNPAY_IN.LGORT   --存储区
				  ,ACCOUNT_START_DATE=t_UNPAY_IN.BUDAT--过账日期
				  --,PART_INENTITY=t_UNPAY_IN.MBLNR   --物料凭证
				  --,PART_INENTITY_YEAR=t_UNPAY_IN.MJAHR--物料凭证年度
				  --,PART_INENTITY_ITEMNO=t_UNPAY_IN.ZEILE --物料凭证行项目
				  ,SHEET_TYPE=t_UNPAY_IN.LFART  --交货单类型
				  --,SETTLE_AMOUNT=t_UNPAY_IN.AMOUNT
				  ,PART_CNAME=t_UNPAY_IN.MAKTX_ZH --中文物料描述
				  ,SUPPLIER_NUM2 = ISNULL(t_UNPAY_IN.Emlif,'')--外协供应商代码
				  ,SAP_AMOUNT=t_UNPAY_IN.AMOUNT--sap开票金额
				  ,WAERS=t_UNPAY_IN.WAERS--币种
				  ,MWSKZ=t_UNPAY_IN.MWSKZ--税码
				  ,SCHPR=t_UNPAY_IN.SCHPR--暂估价
				  ,PO_NO=t_UNPAY_IN.EBELN--原始采购订单
				  ,PO_ITEMNO=t_UNPAY_IN.EBELP--原始采购订单项目
				  ,ASN_NO = t_UNPAY_IN.ASN--ASN单号
				  ,UPDATE_DATE=GETDATE()
				  ,UPDATE_USER='SAP'
				  ,Invoice_Type = CASE WHEN t_UNPAY_IN.AMOUNT = 0 THEN 1 ELSE 0 END
			  FROM LES.TI_SPM_UNPAY_IN t_UNPAY_IN (NOLOCK)
					INNER JOIN LES.TT_SPM_INVOICE_REQUEST t_request ON (
															ISNULL(t_request.PLAN_NO,'')=ISNULL(t_UNPAY_IN.VBELN,'')
															AND ISNULL(t_request.PART_INENTITY,'')=ISNULL(t_UNPAY_IN.MBLNR,'')
															AND t_request.PART_INENTITY_YEAR=t_UNPAY_IN.MJAHR 
															AND t_request.PART_INENTITY_ITEMNO=t_UNPAY_IN.ZEILE 
															AND t_request.ITEM_NO=t_UNPAY_IN.POSNR 
														)
														--ON (t_request.PART_INENTITY=t_UNPAY_IN.MBLNR
					--                                        AND t_request.PART_INENTITY_YEAR=t_UNPAY_IN.MJAHR 
					--                                        AND t_request.PART_INENTITY_ITEMNO=t_UNPAY_IN.ZEILE 
														--	AND t_request.PART_NO=t_UNPAY_IN.MATNR 
					   --                                     AND t_request.PLANT=t_UNPAY_IN.WERKS)

			  WHERE t_UNPAY_IN.SEQ_ID=@SEQ_ID 
				AND t_request.SEQ_ID=@Record_ID
				AND t_request.Invoice_Type!=1	--如果已开发票，则不更新

			 --SELECT PART_INENTITY,PART_INENTITY_YEAR,PART_INENTITY_ITEMNO FROM LES.TT_SPM_INVOICE_REQUEST 
		END
		ELSE
		BEGIN
			--插入
			INSERT INTO 
			LES.TT_SPM_INVOICE_REQUEST(PLAN_NO--VBELN 
									  ,ITEM_NO--POSNR
									  ,PART_NO--MATNR 零件号
									  ,ACTUAL_INHOUSE_PACKAGE_QTY--LGMNG
									  ,MEASURING_UNIT_NO--MEINS --数量单位
									  ,SUPPLIER_NUM--LIFNR  --供应商代码
									  ,PLANT--WERKS 
									  ,RECIEVE_DOCK--LGORT  --库存地点
									  ,ACCOUNT_START_DATE--BUDAT 过账日期
									  ,PART_INENTITY--MBLNR 物料凭证
									  ,SETTLE_AMOUNT--总金额
									  ,PART_INENTITY_YEAR--MJAHR  物料凭证年度
									  ,PART_INENTITY_ITEMNO--ZEILE 物料凭证行项目
									  ,PLAN_RUNSHEET_NO--''
									  ,SUPPLIER_NAME--''
									  ,IDOC--''
									  ,PART_CNAME--MAKTX_ZH
									  ,SUPPLIER_NUM2--Emlif
									  ,SHEET_TYPE--LFART
									  ,CREATE_USER--'admin'
									  ,CREATE_DATE--GETDATE()
									  ,SAP_AMOUNT--AMOUNT
									  ,WAERS--WAERS
									  ,MWSKZ--MWSKZ
									  ,SCHPR--SCHPR
									  ,CURRENCY--WAERS
									  ,PO_NO--EBELN
									  ,PO_ITEMNO--EBELP
									  ,ASN_NO
									  ,Invoice_Type)
								SELECT ISNULL(VBELN,'')
									  ,POSNR
									  ,MATNR
									  ,LGMNG
									  ,MEINS
									  ,LIFNR
									  ,WERKS
									  ,LGORT
									  ,BUDAT
									  ,ISNULL(MBLNR,'')
									  ,NULL
									  ,MJAHR
									  ,ZEILE
									  ,''
									  ,''
									  ,''
									  ,ISNULL(MAKTX_ZH,'')
									  ,ISNULL(Emlif,'')
									  ,LFART
									  ,'admin'
									  ,GETDATE()
									  ,AMOUNT
									  ,WAERS
									  ,MWSKZ
									  ,SCHPR
									  ,WAERS
									  ,EBELN
									  ,EBELP
									  ,ASN
									  ,CASE WHEN AMOUNT = 0 THEN 1 ELSE 0 END 
								  FROM LES.TI_SPM_UNPAY_IN (NOLOCK)
								 WHERE SEQ_ID=@SEQ_ID
		END

		--更新状态
		UPDATE LES.TI_SPM_UNPAY_IN
		SET PROCESS_FLAG=1,
			PROCESS_TIME = GETDATE()
		WHERE SEQ_ID=@SEQ_ID  

		FETCH NEXT FROM REQUEST_CURS1 INTO @SEQ_ID,@MBLNR,@MJAHR,@ZEILE,@POSNR,@VBELN,@MATNR,@WERKS
	END
	
	COMMIT TRAN
END TRY
BEGIN CATCH
	--回滚事务
	ROLLBACK TRAN

	--记录错误信息
	INSERT INTO [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
	SELECT GETDATE(),'INTERFACE','PROC_INTERFACE_SAP_LES_REQUEST','Procedure',ISNULL(ERROR_MESSAGE(),'')+';SEQ_ID:'+ISNULL(@SEQ_ID,''),ERROR_LINE()

	--更新状态 
	UPDATE LES.TI_SPM_UNPAY_IN  
	SET PROCESS_FLAG=9
		,PROCESS_TIME = GETDATE() 
	WHERE SEQ_ID=@SEQ_ID 
END CATCH

CLOSE  REQUEST_CURS1
DEALLOCATE REQUEST_CURS1