

--SAP接口_MM_移库接口
CREATE PROCEDURE [LES].[PROC_INTERFACE_LES_SAP_WMSTRANOUT]
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			
			--取出待处理的TRAN_ID
			DECLARE @TranId TABLE(TRAN_ID INT) 

			INSERT INTO @TranId
			SELECT TOP 100 TRAN_ID 
			  FROM LES.TT_WMM_TRAN_HEAD 
			 WHERE ISNULL(ERP_FLAG,0)=0 
			   AND TRAN_STATUS=2 --已经完成
			 ORDER BY TRAN_ID ASC


			--插入数据SEQ_ID,d.TRAN_ID,
			INSERT INTO LES.TI_WMS_TRAN_OUT(MATNR
			                               ,WERKS
										   ,LGMNG
										   ,MEINS
										   ,LGORT
										   ,UMLGO
										   ,BWART
										   ,KONTO
										   ,KOSTL
										   ,AUFNR
										   ,WBS
										   ,BUDAT
										   ,OPTIM
										   ,OPRTR
										   ,CREATE_DATE
										   ,CREATE_USER
										   ,ZSERIAL)
			SELECT d.PART_NO
			      ,h.PLANT
				  ,d.NUM
				  ,t_parts.PART_UNITS
				  ,h.S_ZONE_NO
				  ,h.O_ZONE_NO
				  --客户需要临时将55调整为Z11,case h.TRAN_TYPE when '55' then 'Z55' when '307' then 'Z01' when '11' then 'Z11' else '311' end
				  ,case h.TRAN_TYPE When '55'  Then 'Z11' 
				                    When '307' Then 'Z01' 
									When '11'  Then 'Z11' 
									When '405' Then 'Z05'
									When '407' Then 'Z07'
									When '411' Then 'Z11'
									When '451' Then 'Z51'
									When '455' Then 'Z55'
									When '417' Then 'Z17'
									When '418' Then 'Z18'
									When '457' Then 'Z57'
									Else '311' end
				  ,h.FINANCIAL_CODE
				  ,h.COST_CODE
				  ,h.INTERNAL_CODE
				  ,h.WBS_CODE
				  ,h.TRAN_TIME  --,CONVERT(VARCHAR(8),h.PUBLISH_TIME,112)
				  ,h.TRAN_TIME
				  ,h.TRAN_KEEPER
				  ,GETDATE()
				  ,'admin'
				  ,LES.FN_GETRANDSTR(19)
			 FROM LES.TT_WMM_TRAN_HEAD h 
	   INNER JOIN LES.TT_WMM_TRAN_DETAIL d ON (h.TRAN_ID = d.TRAN_ID) 
				--ON h.TRAN_NO = d.TRAN_NO  
			--JOIN LES.TM_BAS_WAREHOUSE w 
			--	ON h.O_WM_NO=w.WAREHOUSE
		INNER JOIN (SELECT DISTINCT PLANT,PART_NO,PART_UNITS FROM LES.TM_BAS_MAINTAIN_PARTS) t_parts ON (d.PART_NO=t_parts.PART_NO AND H.plant = t_parts.PLANT)
			--WHERE ISNULL(h.ERP_FLAG,0)=0 AND h.TRAN_STATUS=2
			WHERE h.TRAN_ID IN (SELECT TRAN_ID FROM @TranId)


			--更新数据
			UPDATE LES.TT_WMM_TRAN_HEAD 
			   SET ERP_FLAG=1
			--WHERE  ISNULL(ERP_FLAG,0)=0 AND TRAN_STATUS=2
			 WHERE TRAN_ID IN (SELECT TRAN_ID FROM @TranId) 

			COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
	ROLLBACK TRANSACTION

	DECLARE @ERROR_TRANNO NVARCHAR(MAX)
	SET @ERROR_TRANNO = (SELECT CAST(TRAN_ID AS NVARCHAR(50))+',' FROM @TranId FOR XML PATH(''))

	--记录错误信息
	INSERT INTO [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
	SELECT getdate(),'INTERFACE','PROC_INTERFACE_LES_SAP_WMSTRANOUT','Procedure',ISNULL(error_message(),'')+';TRAN_ID:'+IsNull(@ERROR_TRANNO,''),ERROR_LINE()
	
	--避免错误数据重复执行
	UPDATE  LES.TT_WMM_TRAN_HEAD SET ERP_FLAG=9 WHERE TRAN_ID IN (SELECT TRAN_ID FROM @TranId) 

	END CATCH
END