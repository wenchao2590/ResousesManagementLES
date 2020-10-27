


CREATE PROCEDURE [LES].[PROC_SPM_SAP_LES_GENERAL_RUNSHEET]
AS
BEGIN
Set    NOCOUNT    ON;
Set XACT_ABORT ON;

DECLARE @VBELN nvarchar(12);
--DECLARE @POSNR nvarchar(18);
DECLARE @POSNR numeric(18,3);
DECLARE @LIFNR nvarchar(10);
DECLARE @LFDAT nvarchar(10);
DECLARE @LFUHR nvarchar(10);
DECLARE @WERKS varchar(4);
DECLARE @EMLIF nvarchar(10);
DECLARE @ZDOCK nvarchar(10);
DECLARE @DISPO nvarchar(20);
DECLARE @MATNR nvarchar(18);
DECLARE @STRAS nvarchar(100);
DECLARE @LGMNG numeric(18,3);
DECLARE @MEINS nvarchar(8);
DECLARE @LGORT nvarchar(18);
DECLARE @MAKTX_ZH nvarchar(40);
DECLARE @LFART nvarchar(4);
DECLARE @LESTYPE nvarchar(1);
DECLARE @BSTRF numeric(18,3);
DECLARE @ZVERSION nvarchar(4);
DECLARE @EXPECTED_ARRIVAL_TIME DATETIME;

declare @STRAS_FROM nvarchar(60)
declare @GROES nvarchar(32)
declare @NORMT nvarchar(18)
declare @BISMT nvarchar(18)

DECLARE @ProcessVBELNTable Table (SEQ_ID BIGINT, VBELN NVARCHAR(20))


Begin  Tran

Begin Try

	Insert INTO @ProcessVBELNTable(SEQ_ID,VBELN)
	Select TOP 500 SEQ_ID,VBELN
	From  (SELECT SEQ_ID=MAX(SEQ_ID),VBELN  --目前调整为每次处理500个订单
	         FROM LES.TI_SPM_DELIVERY_RUNSHEET_IN --with(nolock)
	        Where IsNull(PROCESS_FLAG,0) = 0 --未处理的数据
	          And LESTYPE='3'
	        GROUP BY VBELN) A --,LIFNR,ZDOCK)
		Order By SEQ_ID ASC

	--更新处理状态
	Update LES.TI_SPM_DELIVERY_RUNSHEET_IN 
	   Set PROCESS_FLAG = 0 
	 WHERE PROCESS_FLAG is null
	   And VBELN IN (Select VBELN From @ProcessVBELNTable)

	--更新包装数
	Update LES.TI_SPM_DELIVERY_RUNSHEET_IN 
	   Set BSTRF = 0 
	 WHERE BSTRF is null
	   And PROCESS_FLAG = 0
	   And VBELN IN (Select VBELN From @ProcessVBELNTable)

	--处理供应商报警
	declare @event_detail nvarchar(4000)
	set @event_detail = '';
	select @event_detail = @event_detail + '订单号：' + VBELN + ',供应商：' + LIFNR + '在用户信息中不存在;' 
	from
	(
		Select distinct top 50 VBELN,LIFNR
		  FROM LES.TI_SPM_DELIVERY_RUNSHEET_IN with(nolock)
		 Where PROCESS_FLAG=0 
		   And VBELN IN (Select VBELN From @ProcessVBELNTable)
		   And LIFNR not in (select user_login_name from [LES].[TS_SYS_USER])
	) A

	if @event_detail <> ''
	begin
		insert into [LES].[TL_SYS_EVENT_LOG]
		([EVENT_TIME],[EVENT_ID],[EVENT_SOURCE],[EVENT_STATE],[EVENT_TYPE],[EVENT_LEVEL],[EVENT_DETAIL])
		values(	getdate(),301,'SAP下发订单数据错误',0,200,2,@event_detail)
	end


	--定义游标
	DECLARE RUNSHEET_CUR CURSOR FOR
	SELECT VBELN
	       ,case when POSNR='' then null else POSNR END POSNR
		   ,LIFNR
		   ,LFUHR
		   ,WERKS
		   ,EMLIF
		   ,ZDOCK
		   ,DISPO
		   ,LFART
		   ,LESTYPE
		   ,ZVERSION
		   ,STRAS
		   ,LFDAT
		   ,LFUHR
		   ,MEINS
		   ,LGORT
		   ,STRAS_FROM
		   ,GROES
		   ,NORMT
		   ,BISMT
	  FROM LES.TI_SPM_DELIVERY_RUNSHEET_IN with(nolock)
	 WHERE SEQ_ID IN (Select SEQ_ID From @ProcessVBELNTable)
	 Order BY SEQ_ID ASC

	OPEN RUNSHEET_CUR
	FETCH  NEXT  FROM RUNSHEET_CUR INTO @VBELN,@POSNR,@LIFNR,@LFUHR,@WERKS,@EMLIF,@ZDOCK,@DISPO,@LFART,@LESTYPE,@ZVERSION,@STRAS,@LFDAT,@LFUHR,@MEINS,@LGORT,@STRAS_FROM,@GROES,@NORMT,@BISMT
	WHILE( @@fetch_status = 0 )
	BEGIN
	Print('开始处理订单' + @VBELN)
		DECLARE @recordenum INT--记录数
		DECLARE @InsertId BIGINT;
		SELECT @recordenum=COUNT(*) FROM LES.TT_SPM_GENERAL_PURCHASE_RUNSHEET WHERE PLAN_RUNSHEET_NO=@VBELN --AND	SUPPLIER_NUM=@LIFNR AND DOCK=@ZDOCK
		DECLARE @s VARCHAR(30);
		SET @s=@LFDAT+@LFUHR
		DECLARE @Status INT;
		SET @EXPECTED_ARRIVAL_TIME = left(@s,4)+'-'+substring(@s,5,2)+'-'+substring(@s,7,2)+' '+SUBSTRING(@s,9,2)+':'+substring(@s,11,2)+':'+substring(@s,13,2)
		--PRINT CONVERT(varchar(20),@EXPECTED_ARRIVAL_TIME,120)

		IF(@LFART='EL')
			SET @Status=-1
		ELSE IF(@LFART='ZCRS')
			SET @Status=-2
		ELSE IF(@LFART='YRTN')
			SET @Status=-3
		ELSE IF(@LFART='YURG')
				SET @Status=-4
		ELSE IF(@LFART='ZOVS')
				SET @Status=-5

		ELSE IF(@LFART='SRTN')
		     SET  @Status=-11
		ELSE IF(@LFART='SURG')
		     SET  @Status=-12
		ELSE IF(@LFART='ZSPS')
		     SET  @Status=-13
		ELSE IF(@LFART='ZPJ1')
		     SET  @Status=-14
		ELSE IF(@LFART='ZPJR')
		     SET  @Status=-15  --以上废弃中

		ELSE IF(@LFART='EL')--交货单收货
		     SET  @Status=-21
		ELSE IF(@LFART='RL')--交货单退货
		     SET  @Status=-22

		ELSE IF(@LFART='NB')--采购订单收货
		     SET  @Status=-31
		ELSE IF(@LFART='RB')--采购订单退货
		     SET  @Status=-32

		ELSE 
			   SET @Status=-6

		IF @recordenum>0
		BEGIN
			--print '1'
			--ELSE
			--    SET @Status
			 UPDATE LES.TT_SPM_GENERAL_PURCHASE_RUNSHEET 
			    SET PLAN_NO=@POSNR
				   ,EXPECTED_ARRIVAL_TIME=@EXPECTED_ARRIVAL_TIME
				   ,PLANT=@WERKS
				   ,TRANS_SUPPLIER_NUM=@EMLIF
				   ,DOCK=@ZDOCK
				   ,WHAREHOUSE=@STRAS
				   ,RUNSHEET_TYPE=@Status
				   ,SHEET_Done_STATUS=isnull(SHEET_Done_STATUS,1)
			  WHERE PLAN_RUNSHEET_NO=@VBELN 
			 --   AND SUPPLIER_NUM=@LIFNR
				--AND DOCK=@ZDOCK

			 --更新处理状态
			 Update LES.TI_SPM_DELIVERY_RUNSHEET_IN 
			    Set PROCESS_FLAG = 1
				   ,PROCESS_TIME = getdate() 
		      WHERE VBELN = @VBELN
			    And PROCESS_FLAG = 0
			 --print '2'
		END
		ELSE
		BEGIN
			--添加主表信息
   INSERT INTO LES.TT_SPM_GENERAL_PURCHASE_RUNSHEET(PLAN_RUNSHEET_NO
			                                        ,PLAN_NO
													,SUPPLIER_NUM
													,EXPECTED_ARRIVAL_TIME
													,PLANT
													,TRANS_SUPPLIER_NUM
													,DOCK
													,WHAREHOUSE
													,PUBLISH_TIME
													,RUNSHEET_TYPE
													,LES_TYPE
													,ORDER_TYPE
													,SHEET_STATUS
													,CREATE_DATE
													,CREATE_USER
													,PLANT_ZONE
													,SHEET_Done_STATUS)
			                                 VALUES(@VBELN
											       ,@POSNR
												   ,@LIFNR
												   ,@EXPECTED_ARRIVAL_TIME
												   --,@LFUHR
												   ,@WERKS
												   ,@EMLIF
												   ,@ZDOCK
												   ,@STRAS
												   ,getdate()
												   ,@Status
												   ,@LESTYPE
												   ,@LFART
												   ,1
												   ,getdate()
												   ,'admin'
												   ,@LGORT
												   ,1)
			SELECT @InsertId= SCOPE_IDENTITY()

			 --添加明细信息		 
		 INSERT INTO LES.TT_SPM_GENERAL_PURCHASE_RUNSHEET_DETAIL
						([PLAN_RUNSHEET_SN]
						,PLANT
						,SUPPLIER_NUM
						,PART_NO
						,PART_CNAME
						,DOCK
						,MEASURING_UNIT_NO
						,REQUIRED_INHOUSE_PACKAGE
						,REQUIRED_INHOUSE_PACKAGE_QTY
						,IDOC
						,WM_NO
						,ZONE_NO
						,DLOC
						,INHOUSE_PACKAGE_MODEL
						,INHOUSE_PACKAGE
						,DISPO
						,COMMENTS
						,LOADING_ADDRESS
						,[STANDARD]
						,MODEL
						,BRAND)
			      SELECT @InsertId
				        ,WERKS
						,LIFNR
						,MATNR
						,MAKTX_ZH
						,ZDOCK
						,MEINS
						,LGMNG
						,Case When IsNull(BSTRF,0)=0 Then Null Else CEILING(LGMNG/(BSTRF*1.0)) End
			            ,'IDOC'
						,C.WM_NO--B.WM_NO
						,A.LGORT--B.ZONE_NO
						,B.DLOC
						,A.ZYSQJC--B.PACKAGE_MODEL
						,BSTRF
						,A.DISPO
						,A.COMMENTS
						,@STRAS_FROM
						,@GROES
						,@BISMT
						,@NORMT
			       FROM LES.TI_SPM_DELIVERY_RUNSHEET_IN A with(nolock)
			  LEFT JOIN LES.TM_BAS_PARTS_STOCK B with(nolock) ON (B.PLANT = A.WERKS AND B.PART_NO = A.MATNR AND B.ZONE_NO = A.LGORT)
			  LEFT JOIN LES.TM_WMM_ZONES C with(nolock) ON C.PLANT = A.WERKS AND C.ZONE_NO = B.ZONE_NO --AND C.IS_MANAGE IN (10,60) --= @ZONE_TYPE  收货仓库为10，退货仓库为60
			 WHERE A.VBELN = @VBELN
			   And A.PROCESS_FLAG=0


			 --更新处理状态
			 Update LES.TI_SPM_DELIVERY_RUNSHEET_IN 
			    Set PROCESS_FLAG = 1
				   ,PROCESS_TIME = getdate() 
			  WHERE VBELN = @VBELN
			    And PROCESS_FLAG = 0
		END
		FETCH  NEXT  FROM RUNSHEET_CUR INTO @VBELN,@POSNR,@LIFNR,@LFUHR,@WERKS,@EMLIF,@ZDOCK,@DISPO,@LFART,@LESTYPE,@ZVERSION,@STRAS,@LFDAT,@LFUHR,@MEINS,@LGORT,@STRAS_FROM,@GROES,@NORMT,@BISMT
	END
	CLOSE  RUNSHEET_CUR
	DEALLOCATE RUNSHEET_CUR
	Commit Tran
End Try
Begin Catch

	Rollback Tran

	insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
	select getdate(),'INTERFACE','PROC_SPM_SAP_LES_GENERAL_RUNSHEET','Procedure',ISNULL(error_message(),'')+';VBELN:'+IsNull(@VBELN,''),ERROR_LINE()

	--更新处理状态
	Update LES.TI_SPM_DELIVERY_RUNSHEET_IN 
			    Set PROCESS_FLAG = 9
				   ,PROCESS_TIME = getdate() 
			  WHERE VBELN = @VBELN
			    And ISNULL(PROCESS_FLAG,0) = 0

	--Print(error_message())


End Catch

END