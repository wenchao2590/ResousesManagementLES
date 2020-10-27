--SAP接口_MM_盘点结果

CREATE PROCEDURE [LES].[PROC_INTERFACE_LES_SAP_MRPDETAIL_WMSNOTIFICATIONOUT]
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
		--插入数据SEQ_ID,d.TRAN_ID,
		INSERT INTO LES.TI_WMS_NOTIFICATION_OUT(IBLNR
		                                       ,GJAHR
											   ,WERKS
											   ,LGORT
											   ,MATNR
											   ,GIDAT
											   ,MENGE
											   ,MEINS
											   ,COMMENTS
											   ,CREATE_USER
											   ,CREATE_DATE)
		                                 SELECT h.NOTIFICATION_NO
										       ,YEAR(GETDATE())
											   ,h.PLANT
											   ,SUBSTRING(h.ZONE_NO,0,18)
											   ,SUBSTRING(d.PART_NO,0,18)
											   ,h.COUNT_TIME
											   ,d.REAL_NUM
											   ,de.MEINS
											   ,d.COMMENTS
											   ,'admin'
											   ,GETDATE()
		                                   FROM LES.TT_WMM_NOTIFICATION_HEAD h 
		                             INNER JOIN LES.TM_WMM_NOTIFICATION_DETAIL_IMPORT d ON (h.NOTIFICATION_ID=d.NOTIFICATIONID AND h.COUNT_STATUS>=3 AND ISNULL(h.ERP_FLAG,0)=0 AND h.FROM_SAP=1)
									 INNER JOIN LES.TT_WMM_NOTIFICATION_DETAIL de ON(d.NOTIFICATIONID=de.NOTIFICATIONID And h.PLANT=de.PLANT And h.ZONE_NO=de.ZONE_NO And d.PART_NO=de.PART_NO)
		                          
		--更新数据
		UPDATE LES.TT_WMM_NOTIFICATION_HEAD 
		   SET ERP_FLAG=1 
		 WHERE COUNT_STATUS>=3 
		   AND ISNULL(ERP_FLAG,0)=0
		   AND FROM_SAP=1
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
	ROLLBACK TRANSACTION
--记录错误信息
	INSERT INTO [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
	SELECT getdate(),'INTERFACE','PROC_INTERFACE_LES_SAP_MRPDETAIL_WMSNOTIFICATIONOUT','Procedure',error_message(),ERROR_LINE()

	END CATCH
END