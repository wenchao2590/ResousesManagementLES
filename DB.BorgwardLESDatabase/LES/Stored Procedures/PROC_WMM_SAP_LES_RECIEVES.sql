


--SAP接口_MM_盘点计划下达
CREATE PROCEDURE [LES].[PROC_WMM_SAP_LES_RECIEVES]
AS
DECLARE @IBLNR nvarchar(10);--盘点凭证
DECLARE @GJAHR VARCHAR(4);--年度
DECLARE @WERKS nvarchar(4);--工厂代码
DECLARE @LGORT nvarchar(18);--库存地点
DECLARE @MATNR nvarchar(18);--物料号
DECLARE @GIDAT DATETIME;--计划盘点日期
DECLARE @MENGE  numeric(18,2);--账面库存数量
DECLARE @MEINS nvarchar(8);--单位
DECLARE @XLOEK nvarchar(1);--删除标记
DECLARE @XDIFF NVARCHAR(1);--差异调整状态
DECLARE @XZAEL VARCHAR(1);--盘点状态


--1.0根据接口表中的删除标记，删除对应的盘点通知
DECLARE NOTIFICATION_CURS1  CURSOR FOR SELECT IBLNR
                                             ,WERKS
											 ,GJAHR
											 ,LGORT
											 ,GIDAT
											 ,MATNR 
										FROM LES.TI_WMS_NOTIFICATION_IN 
	                                   WHERE ISNULL(PROCESS_FLAG,0)=0 
									     AND ISNULL(XLOEK,'')='X'  
OPEN NOTIFICATION_CURS1
FETCH NEXT FROM NOTIFICATION_CURS1 INTO @IBLNR,@WERKS,@GJAHR,@LGORT,@GIDAT,@MATNR
WHILE( @@fetch_status = 0 )
BEGIN
	DECLARE @NOTIFICATION_ID int;
	SET @NOTIFICATION_ID=-1
	SELECT  @NOTIFICATION_ID=NOTIFICATION_ID 
	  FROM LES.TT_WMM_NOTIFICATION_HEAD t_head 
	 WHERE t_head.NOTIFICATION_NO=@IBLNR --盘点凭证
	   AND t_head.PLANT=@WERKS --工厂代码
	   AND t_head.ZONE_NO=@LGORT --库存地点
	   AND CAST(YEAR(t_head.CREATE_DATE) AS varchar(4))=@GJAHR--年度
	
	IF(@NOTIFICATION_ID!=-1)
	BEGIN
		DELETE FROM LES.TT_WMM_NOTIFICATION_DETAIL 
		WHERE NOTIFICATIONID=@NOTIFICATION_ID 
		  AND PART_NO=@MATNR
    
		DELETE FROM LES.TM_WMM_NOTIFICATION_DETAIL_IMPORT 
		 WHERE NOTIFICATIONID=@NOTIFICATION_ID 
		   AND PART_NO=@MATNR

		--1.0.1 如果盘点通知单子表中记录为空则删除主表记录
		DELETE FROM LES.TT_WMM_NOTIFICATION_HEAD  
		 WHERE FROM_SAP=1 
		   AND NOT EXISTS(SELECT * 
		                    FROM LES.TT_WMM_NOTIFICATION_DETAIL 
						   WHERE NOTIFICATIONID=@NOTIFICATION_ID)
	END
    
	 
FETCH NEXT FROM NOTIFICATION_CURS1 INTO @IBLNR,@WERKS,@GJAHR,@LGORT,@GIDAT,@MATNR
END
CLOSE  NOTIFICATION_CURS1
DEALLOCATE NOTIFICATION_CURS1 --第一个游标结束

--1.1接口表状态更改
Update LES.TI_WMS_NOTIFICATION_IN 
   SET PROCESS_FLAG=1,PROCESS_TIME=GETDATE()
 WHERE ISNULL(PROCESS_FLAG,0)=0 
   AND ISNULL(XLOEK,'')='X' 

--2.0 插入正常记录
DECLARE NOTIFICATION_CURS2 CURSOR FOR SELECT IBLNR
                                            ,WERKS
											,GJAHR
											,LGORT
											,GIDAT
											,ISNULL(XZAEL,'') XZAEL
											,ISNULL(XDIFF,'') XDIFF 
									    FROM LES.TI_WMS_NOTIFICATION_IN 
	                                   WHERE ISNULL(PROCESS_FLAG,0)=0 
									     AND ISNULL(XLOEK,'')='' 
	                                GROUP BY IBLNR,WERKS,GJAHR,LGORT,GIDAT,ISNULL(XZAEL,''),ISNULL(XDIFF,'')
OPEN NOTIFICATION_CURS2
FETCH NEXT FROM NOTIFICATION_CURS2 INTO @IBLNR,@WERKS,@GJAHR,@LGORT,@GIDAT,@XZAEL,@XDIFF
WHILE( @@fetch_status = 0 )
BEGIN
	DECLARE @RecordNum INT;
	--获取接口表中零件在库存中的条目数量
	SELECT @RecordNum=COUNT(1)
	 FROM LES.TI_WMS_NOTIFICATION_IN t_notification --接口表
	INNER JOIN LES.TT_WMS_STOCKS t_STOCKS ON (t_notification.WERKS=t_STOCKS.PLANT AND t_notification.LGORT=t_STOCKS.ZONE_NO AND t_notification.MATNR=t_STOCKS.PART_NO)
	INNER JOIN LES.TM_WMM_ZONES t_ZONES ON (t_STOCKS.ZONE_NO=t_ZONES.ZONE_NO AND t_ZONES.PLANT=t_STOCKS.PLANT)
	 WHERE t_notification.IBLNR=@IBLNR 
	   AND t_notification.WERKS=@WERKS 
	   AND t_notification.GJAHR=@GJAHR 
	   AND t_notification.LGORT=@LGORT
	   AND ISNULL(t_notification.PROCESS_FLAG,0)=0

	   print(@RecordNum)

	IF(@RecordNum>0)--如果数量大于0（零件在库存中），执行下面代码
	BEGIN
		DECLARE @NOTIFICATION_ID2 BIGINT;
		SET @NOTIFICATION_ID2=-1

		--插入通知单主表,并获取最新id
		SELECT TOP 1 @NOTIFICATION_ID2=NOTIFICATION_ID 
		 FROM LES.TT_WMM_NOTIFICATION_HEAD 
		WHERE NOTIFICATION_NO=@IBLNR --盘点凭证
		  AND PLANT=@WERKS --工厂代码
		  AND ZONE_NO=@LGORT --库存地点
		  AND FROM_SAP=1 --来源
		  AND DateName(year,CREATE_DATE)=@GJAHR--年度
		
		IF(@NOTIFICATION_ID2=-1)--在盘点通知单中不存在主表记录
		BEGIN
			INSERT INTO LES.TT_WMM_NOTIFICATION_HEAD(NOTIFICATION_NO
			                                        ,PLANT
													,PLANT_ZONE
													,WM_NO
													,ZONE_NO
													,COUNT_TIME
													,FROM_SAP
													,CREATE_DATE
													,CREATE_USER
													,COUNT_STATUS
													,COUNT_TYPE
													,EMERGENCY_TYPE)
			                                 Select  @IBLNR
											        ,@WERKS
													,@LGORT
													,WM_NO
													,@LGORT
													,@GIDAT
													,1
													,GETDATE()
													,'admin'
													,1
													,1
													,1
			                                    FROM LES.TM_WMM_ZONES WHERE ZONE_NO=@LGORT AND PLANT=@WERKS	

			SELECT @NOTIFICATION_ID2=SCOPE_IDENTITY();
			
			--根据主表id和库存记录,插入详细表
			INSERT INTO LES.TT_WMM_NOTIFICATION_DETAIL(NOTIFICATIONID
			                                          ,PLANT
			                                          ,WM_NO
													  ,PART_NO
													  ,PART_CNAME
													  ,PART_NICKNAME
													  ,ZONE_NO
													  ,DLOC
													  ,NUM
													  ,MEINS
													  ,STOCK_IDENTITY)
			                           SELECT DISTINCT @NOTIFICATION_ID2
									                  ,t_notification.WERKS									                  ,t_ZONES.WM_NO
													  ,t_STOCKS.PART_NO
													  ,t_PARTS.PART_CNAME
													  ,t_PARTS.PART_NICKNAME
													  ,t_ZONES.ZONE_NO
													  ,t_STOCKS.DLOC
													  ,t_notification.MENGE
													  ,t_notification.MEINS
													  ,t_STOCKS.STOCK_IDENTITY
				                                  FROM LES.TI_WMS_NOTIFICATION_IN t_notification --接口表
											INNER JOIN LES.TT_WMS_STOCKS t_STOCKS ON (t_notification.WERKS=t_STOCKS.PLANT AND t_notification.LGORT=t_STOCKS.ZONE_NO AND t_notification.MATNR=t_STOCKS.PART_NO)--库存
											INNER JOIN LES.TM_WMM_ZONES t_ZONES ON (t_STOCKS.ZONE_NO=t_ZONES.ZONE_NO AND t_ZONES.PLANT=t_STOCKS.PLANT)  --存储区
											INNER JOIN LES.TM_BAS_MAINTAIN_PARTS t_PARTS ON (t_notification.WERKS=t_PARTS.PLANT AND t_notification.MATNR=t_PARTS.PART_NO)
												WHERE t_notification.IBLNR=@IBLNR 
												  AND t_notification.WERKS=@WERKS 
												  AND t_notification.GJAHR=@GJAHR 
												  AND t_notification.LGORT=@LGORT 
												  AND ISNULL(t_notification.PROCESS_FLAG,0)=0 
											END
        ELSE --在盘点通知单中存在主表记录
		BEGIN
			UPDATE LES.TT_WMM_NOTIFICATION_HEAD 
			   SET COUNT_TIME=@GIDAT 
			 WHERE NOTIFICATION_ID=@NOTIFICATION_ID2

			--根据主表id和库存记录,插入详细表
			INSERT INTO LES.TT_WMM_NOTIFICATION_DETAIL(NOTIFICATIONID
			                                          ,PLANT
			                                          ,WM_NO,PART_NO
													  ,PART_CNAME
													  ,PART_NICKNAME
													  ,ZONE_NO
													  ,DLOC
													  ,NUM
													  ,MEINS
													  ,STOCK_IDENTITY)
									   SELECT DISTINCT @NOTIFICATION_ID2
									                  ,t_notification.WERKS			
									                  ,t_ZONES.WM_NO
													  ,t_STOCKS.PART_NO
													  ,t_PARTS.PART_CNAME
													  ,t_PARTS.PART_NICKNAME
													  ,t_ZONES.ZONE_NO
													  ,t_STOCKS.DLOC
													  ,t_notification.MENGE
													  ,t_notification.MEINS
													  ,t_STOCKS.STOCK_IDENTITY
											      FROM LES.TI_WMS_NOTIFICATION_IN t_notification --接口表
												INNER JOIN LES.TT_WMS_STOCKS t_STOCKS ON (t_notification.WERKS=t_STOCKS.PLANT AND t_notification.LGORT=t_STOCKS.ZONE_NO AND t_notification.MATNR=t_STOCKS.PART_NO)--库存
												INNER JOIN LES.TM_WMM_ZONES t_ZONES ON (t_STOCKS.ZONE_NO=t_ZONES.ZONE_NO AND t_ZONES.PLANT=t_STOCKS.PLANT)--存储区
												INNER JOIN LES.TM_BAS_MAINTAIN_PARTS t_PARTS ON (t_notification.WERKS=t_PARTS.PLANT AND t_notification.MATNR=t_PARTS.PART_NO)
												WHERE t_notification.IBLNR=@IBLNR 
												  AND t_notification.WERKS=@WERKS 
												  AND t_notification.GJAHR=@GJAHR 
												  AND t_notification.LGORT=@LGORT 
												  AND ISNULL(t_notification.PROCESS_FLAG,0)=0 
												  AND t_STOCKS.STOCK_IDENTITY NOT IN(SELECT STOCK_IDENTITY 
												                                       FROM  LES.TT_WMM_NOTIFICATION_DETAIL 
																					  Where NOTIFICATIONID=@NOTIFICATION_ID2)

			UPDATE t_DETAIL 
			   SET NUM=t_Interface_stock.MENGE
			      ,MEINS=t_Interface_stock.MEINS 
			 FROM  LES.TT_WMM_NOTIFICATION_DETAIL t_DETAIL 
			 INNER JOIN (SELECT DISTINCT t_ZONES.WM_NO
			                            ,t_STOCKS.PART_NO
										,t_STOCKS.PART_CNAME
										,t_ZONES.ZONE_NO
										,t_STOCKS.DLOC
										,t_notification.MENGE
										,t_notification.MEINS
										,t_STOCKS.STOCK_IDENTITY
				                    FROM LES.TI_WMS_NOTIFICATION_IN t_notification --接口表
									INNER JOIN LES.TT_WMS_STOCKS t_STOCKS ON (t_notification.WERKS=t_STOCKS.PLANT AND t_notification.LGORT=t_STOCKS.ZONE_NO AND t_notification.MATNR=t_STOCKS.PART_NO) --库存
									INNER JOIN LES.TM_WMM_ZONES t_ZONES ON (t_STOCKS.ZONE_NO=t_ZONES.ZONE_NO AND t_ZONES.PLANT=t_STOCKS.PLANT)--存储区
									WHERE t_notification.IBLNR=@IBLNR 
									  AND t_notification.WERKS=@WERKS 
									  AND t_notification.GJAHR=@GJAHR 
									  AND t_notification.LGORT=@LGORT 
									  AND ISNULL(t_notification.PROCESS_FLAG,0)=0) t_Interface_stock ON (t_Interface_stock.STOCK_IDENTITY = t_DETAIL.STOCK_IDENTITY AND t_DETAIL.NOTIFICATIONID=@NOTIFICATION_ID2)
		END;


		--2.1更新接口表中对应条目状态
		UPDATE LES.TI_WMS_NOTIFICATION_IN 
		   SET PROCESS_FLAG=1,PROCESS_TIME=GETDATE()
		 WHERE SEQ_ID IN(SELECT SEQ_ID 
		                   FROM LES.TI_WMS_NOTIFICATION_IN t_notification --接口表
					      INNER JOIN LES.TT_WMS_STOCKS t_STOCKS ON (t_notification.WERKS=t_STOCKS.PLANT AND t_notification.LGORT=t_STOCKS.ZONE_NO AND t_notification.MATNR=t_STOCKS.PART_NO)--库存
					      INNER JOIN LES.TM_WMM_ZONES t_ZONES ON (t_STOCKS.ZONE_NO=t_ZONES.ZONE_NO AND t_ZONES.PLANT=t_STOCKS.PLANT)--存储区
					      WHERE t_notification.IBLNR=@IBLNR 
						    AND t_notification.WERKS=@WERKS 
						    AND t_notification.GJAHR=@GJAHR 
						    AND t_notification.LGORT=@LGORT ) 
		    AND ISNULL(PROCESS_FLAG,0)=0
	END;
		 
FETCH NEXT FROM NOTIFICATION_CURS2 INTO @IBLNR,@WERKS,@GJAHR,@LGORT,@GIDAT,@XZAEL,@XDIFF
END
CLOSE  NOTIFICATION_CURS2
DEALLOCATE NOTIFICATION_CURS2