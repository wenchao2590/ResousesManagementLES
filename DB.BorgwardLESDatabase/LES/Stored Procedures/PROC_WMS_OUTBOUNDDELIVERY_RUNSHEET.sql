


CREATE PROCEDURE [LES].[PROC_WMS_OUTBOUNDDELIVERY_RUNSHEET]
AS
BEGIN
	Set    NOCOUNT    ON;
	Set XACT_ABORT ON;

	Declare @SEQ_ID as INT
	Declare @VBELN as NVARCHAR(10)        --交货单号
	Declare @ERDAT as NVARCHAR(8)         --交货单创建日期
	Declare @ERZET as NVARCHAR(6)		  --交货单创建时间
	Declare @LFART as NVARCHAR(4)         --交货单类型
	Declare @KUNNR as  NVARCHAR(10)       --送达方
	Declare @LFDAT as  NVARCHAR(8)        --期望送达日期
	Declare @LFUHR as  NVARCHAR(6)        --期望送达时间
	Declare @KUNAME as  NVARCHAR(60)      --送达方名称
	Declare @KUADRC as  NVARCHAR(60)      --送达方地址
	Declare @LXRNAM as  NVARCHAR(40)      --联系人
	Declare @LXRTEL as  NVARCHAR(30)      --联系人电话
	Declare @LIFNR as  NVARCHAR(16)       --供应商代码
	Declare @LINAME as  NVARCHAR(60)      --供应商名称
	Declare @LIADRC as  NVARCHAR(60)      --供应商地址

	Declare @TEXT1 as  NVARCHAR(200)
	Declare @DEAL_FLAG as INT
	Declare @PROCESS_FLAG as INT
	Declare @PROCESS_TIME as DATETIME
	Declare @COMMENTS as NVARCHAR(200)

	DECLARE @ProcessVBELNTable Table (SEQ_ID INT, VBELN NVARCHAR(20))

	Begin  Tran

	Begin Try
		Insert INTO @ProcessVBELNTable(SEQ_ID,VBELN)
		Select TOP 500 SEQ_ID,VBELN
		From  (SELECT SEQ_ID=MAX(SEQ_ID),VBELN  --目前调整为每次处理500个订单
				 FROM LES.TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN With(NOLOCK)
				Where IsNull(PROCESS_FLAG,0)=0 --未处理的数据
				GROUP BY VBELN) A --,LIFNR,ZDOCK)
			Order By SEQ_ID ASC

		--处理器具报警信息
		Declare @event_detail NVARCHAR(4000)
		Set @event_detail='';
		Select @event_detail = @event_detail + '销售出库单号：' + VBELN + ',器具号：' + ZYSQJC + '在器具库存信息中不存在;' 
		from
		(   Select Distinct Top 50 VBELN,ZYSQJC
			  From LES.TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN
			 Where IsNull(PROCESS_FLAG,0)=0 
			   And VBELN IN (Select VBELN From @ProcessVBELNTable)
			   And ZYSQJC Not In (Select PACKAGE_NO 
								   From [LES].[TM_RPM_PACKAGE_STOCKS])
		) A

		IF(@event_detail <> '')
		Begin
			Insert into [LES].[TL_SYS_EVENT_LOG]
							 ([EVENT_TIME]
							 ,[EVENT_ID]
							 ,[EVENT_SOURCE]
							 ,[EVENT_STATE]
							 ,[EVENT_TYPE]
							 ,[EVENT_LEVEL]
							 ,[EVENT_DETAIL])
					 Values(  Getdate()
							 ,301
							 ,'SAP下发销售出库单数据错误'
							 ,0
							 ,10200
							 ,2
							 ,@event_detail)
		End

		--处理零件报警
		Set @event_detail = '';
		Select @event_detail = @event_detail + '销售出库单号：' + VBELN + ',零件号：' + MATNR + ',存储区：' + LGORT + '在零件仓库信息中不存在;' 
		from
		(	Select distinct top 50 VBELN
								  ,MATNR
								  ,LGORT
			FROM LES.TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN B 
			Where IsNull(PROCESS_FLAG,0)=0  
			  And B.VBELN IN (Select VBELN From @ProcessVBELNTable)
			  And B.MATNR Not in (Select C.PART_NO 
									From LES.TM_BAS_PARTS_STOCK C 
								   Where C.PLANT=B.WERKS 
									 And C.ZONE_NO=B.LGORT)
		) A

		IF(@event_detail <> '')
		Begin
			Insert into [LES].[TL_SYS_EVENT_LOG]
						([EVENT_TIME]
						,[EVENT_ID]
						,[EVENT_SOURCE]
						,[EVENT_STATE]
						,[EVENT_TYPE]
						,[EVENT_LEVEL]
						,[EVENT_DETAIL])
				 Values(getdate()
						,301
						,'SAP下发销售出库单数据错误'
						,0
						,10200
						,2
						,@event_detail)
		End

		--处理零件数报警
		Set @event_detail = '';
		Select @event_detail = @event_detail + '销售出库单号：' + VBELN + ',零件号：' + MATNR + '数量小于0，不能正常组单;' 
		from
		(  Select distinct top 50 VBELN
								 ,MATNR
	 		 FROM LES.TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN B 
			Where IsNull(B.PROCESS_FLAG,0)=0 
			  And B.VBELN IN (Select VBELN From @ProcessVBELNTable)
			  And ISNULL(B.LFIMG,0)<0
		) A

		IF(@event_detail <> '')
		Begin
			--针对数量<0的订单，整个订单都不处理
			Update LES.TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN
			   Set PROCESS_FLAG=1
				  ,PROCESS_TIME=Getdate()
			 Where VBELN IN (Select VBELN From @ProcessVBELNTable)
			   And VBELN IN (Select VBELN 
							   From LES.TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN 
							  Where IsNull(PROCESS_FLAG,0)=0 
								And ISNULL(LFIMG,0)<0)

			Update LES.TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN
			   Set PROCESS_FLAG=1
				  ,PROCESS_TIME=Getdate()
			 Where VBELN IN (Select VBELN From @ProcessVBELNTable)
			   And IsNull(PROCESS_FLAG,0)=0 
			   And ISNULL(LFIMG,0)<0 

			Insert Into [LES].[TL_SYS_EVENT_LOG]
					   ([EVENT_TIME]
					   ,[EVENT_ID]
					   ,[EVENT_SOURCE]
					   ,[EVENT_STATE]
					   ,[EVENT_TYPE]
					   ,[EVENT_LEVEL]
					   ,[EVENT_DETAIL])
				Values(	Getdate()
					   ,301
					   ,'SAP下发销售出库单数据错误'
					   ,0
					   ,10200
					   ,2
					   ,@event_detail)
		 End


		--定义游标
		DECLARE RUNSHEET_CUR CURSOR FOR
		SELECT [SEQ_ID]
			  ,[VBELN]
			  ,[ERDAT]
			  ,[ERZET]
			  ,[LFART]
			  ,[KUNNR]
			  ,[LFDAT]
			  ,[LFUHR]
			  ,[KUNAME]
			  ,[KUADRC]
			  ,[LXRNAM]
			  ,[LXRTEL]
			  ,[LIFNR]
			  ,[LINAME]
			  ,[LIADRC]
			  ,[TEXT1]
			  ,[DEAL_FLAG]
			  ,[PROCESS_FLAG]
			  ,[PROCESS_TIME]
			  ,[COMMENTS]
			  --,[CREATE_USER]
			  --,[CREATE_DATE]
			  --,[UPDATE_USER]
			  --,[UPDATE_DATE]
		  FROM [LES].[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN]
		 WHERE ISNULL(PROCESS_FLAG,0)=0 --未处理的数据
		   And VBELN IN (Select VBELN From @ProcessVBELNTable)

		OPEN RUNSHEET_CUR
		FETCH  NEXT  FROM RUNSHEET_CUR INTO @SEQ_ID
										   ,@VBELN
										   ,@ERDAT
										   ,@ERZET
										   ,@LFART
										   ,@KUNNR
										   ,@LFDAT
										   ,@LFUHR
										   ,@KUNAME
										   ,@KUADRC
										   ,@LXRNAM
										   ,@LXRTEL
										   ,@LIFNR
										   ,@LINAME
										   ,@LIADRC
										   ,@TEXT1
										   ,@DEAL_FLAG
										   ,@PROCESS_FLAG
										   ,@PROCESS_TIME
										   ,@COMMENTS
		WHILE( @@fetch_status = 0 )
		BEGIN
			PRINT('开始处理销售出库单：' + @VBELN)
			DECLARE @RecordeNum as INT--记录数
			SELECT @RecordeNum=IsNull(COUNT(1),0) FROM [LES].[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN] WHERE VBELN=@VBELN AND ISNULL(PROCESS_FLAG,0)=1 And SEQ_ID<>@SEQ_ID
		
			IF(@RecordeNum > 0)
			BEGIN
				--记录日志
				Insert Into [LES].[TL_SYS_EVENT_LOG]
						   ([EVENT_TIME]
						   ,[EVENT_ID]
						   ,[EVENT_SOURCE]
						   ,[EVENT_STATE]
						   ,[EVENT_TYPE]
						   ,[EVENT_LEVEL]
						   ,[EVENT_DETAIL])
					Values(	Getdate()
						   ,301
						   ,'SAP下发销售出库单数据错误'
						   ,0
						   ,10200
						   ,2
						   ,'SEQ_ID：'+CAST(@SEQ_ID AS VARCHAR(20))+' 销售出库单号：' + @VBELN + ' 已经存在，无法处理，直接更新处理状态为：1；' )

				 --更新处理状态
				 Update LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN] 
					Set PROCESS_FLAG=1
					   ,PROCESS_TIME=Getdate() 
				  Where VBELN=@VBELN
					And ISNULL(PROCESS_FLAG,0)=0 

				 Update LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN] 
					Set PROCESS_FLAG=1
					   ,PROCESS_TIME=Getdate() 
				  Where SEQ_ID=@SEQ_ID

			END
			ELSE
			BEGIN	
				DECLARE @TRAN_TIME as DATETIME
				DECLARE @EXPECT_ARRIVAL_TIME as DATETIME
				DECLARE @s as VARCHAR(30)
				IF(IsNull(@ERDAT,'')<>'')
				Begin
					SET @s=@ERDAT+ISNUll(@ERZET,'000000')
					SET @TRAN_TIME = left(@s,4)+'-'+substring(@s,5,2)+'-'+substring(@s,7,2)+' '+SUBSTRING(@s,9,2)+':'+substring(@s,11,2)+':'+substring(@s,13,2)
				End

				IF(IsNull(@LFDAT,'')<>'')
				Begin
					SET @s=@LFDAT+ISNUll(@LFUHR,'000000')
					SET @EXPECT_ARRIVAL_TIME = left(@s,4)+'-'+substring(@s,5,2)+'-'+substring(@s,7,2)+' '+SUBSTRING(@s,9,2)+':'+substring(@s,11,2)+':'+substring(@s,13,2)
				End
				--从相关业务表中找到变量值 
				Declare @PLANT as nvarchar(5)
				Declare @WM_NO as nvarchar(10)
				Declare @DOCK as nvarchar(10)
				Declare @ZONE_NO as nvarchar(20)
				Declare @SUPPLIER_NUM as nvarchar(16)
				Declare @TRANS_SUPPLIER_NUM as nvarchar(20)
				Declare @PartType as int

				Select Top 1 @PLANT=A.WERKS
							,@WM_NO=D.WM_NO     
							,@ZONE_NO=A.LGORT
							,@SUPPLIER_NUM=B.LIFNR
						--	,@PartType=C.PART_CLS
				  From LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN] A 
				  LEFT JOIN LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN] B ON (A.VBELN=B.VBELN AND IsNull(A.PROCESS_FLAG,0)=IsNull(B.PROCESS_FLAG,0))
				--LEFT JOIN LES.TM_BAS_PARTS_STOCK C ON (C.PLANT=A.WERKS AND C.PART_NO=A.MATNR AND C.ZONE_NO=A.LGORT)		  
				  LEFT JOIN LES.TM_WMM_ZONES D ON(A.WERKS=D.PLANT And A.LGORT=D.ZONE_NO)
				  WHERE A.VBELN=@VBELN
					And ISNULL(A.PROCESS_FLAG,0)=0

				Declare @OrderType as INT
				IF (@LFART='ZNR1' Or @LFART='ZNR2' Or @LFART='T')
					Set @OrderType=-3 --退货单
				Else IF (@LFART='NLC1' Or @LFART='NLC2' Or @LFART='J')
					Set @OrderType=-1 --订货单
				Else
					Set @OrderType=-6 --其他

				IF (@OrderType=-3)  --退货单
				BEGIN
					--创建退货单
					--EXEC LES.PROC_WMM_CREATERETURN @InsertId
					INSERT INTO [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN]
								([OUTBOUNDDELIVERYRETURN_NO]
								,[RETURN_TYPE]
								,[PLANT]
								,[WM_NO]
								,[ZONE_NO]
								,[DOCK]
								,[SUPPLIER_NUM]
								,[SUPPLIER_NAME]
								,[SUPPLIER_ADDRESS]
								,[TRANS_SUPPLIER_NUM]
								,[RETURN_COMPANY_NUM]
								,[RETURN_COMPANY_NAME]
								,[RETURN_COMPANY_ADDRESS]
								,[RETURNER]
								,[PHONENUM]
								,[TRAN_TIME]
								,[EXPECT_ARRIVAL_TIME]
								,[ACTUAL_ARRIVAL_TIME]
								,[CONFIRM_FLAG]
								,[OPRTR]
								,[ERP_FLAG]
								,[COMMENTS]
								,[CREATE_USER]
								,[CREATE_DATE]
								,[UPDATE_USER]
								,[UPDATE_DATE])
							VALUES
								(@VBELN
								,@OrderType
								,@PLANT
								,@WM_NO
								,@ZONE_NO
								,@DOCK
								,@LIFNR
								,@LINAME
								,@LIADRC
								,@TRANS_SUPPLIER_NUM
								,@KUNNR
								,@KUNAME
								,@KUADRC
								,@LXRNAM
								,@LXRTEL
								,@TRAN_TIME
								,@EXPECT_ARRIVAL_TIME
								,Null
								,1
								,Null
								,0
								,@TEXT1
								,'admin'
								,Getdate()
								,Null
								,Null)
               
					Declare @OUTBOUNDDELIVERYRETURN_ID as BIGINT
					Set @OUTBOUNDDELIVERYRETURN_ID=@@IDENTITY

					INSERT INTO [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN_DETAIL]
									([OUTBOUNDDELIVERYRETURN_ID]
									,[OUTBOUNDDELIVERYRETURN_NO]
									,[PLANT]
									,[ASSEMBLY_LINE]
									,[WM_NO]
									,[ZONE_NO]
									,[DLOC]
									,[DOCK]
									,[SUPPLIER_NUM]
									,[PACKAGE_MODEL]
									,[MEASURING_UNIT_NO]
									,[PACKAGE]
									,[NUM]
									,[BOX_NUM]
									,[PART_NO]
									,[IDENTIFY_PART_NO]
									,[PART_CNAME]
									,[PART_ENAME]
									,[PART_TYPE]
									,[BOX_PARTS]
									,[REQUIRED_OUTBOUND_PACKAGE]
									,[REQUIRED_OUTBOUND_PACKAGE_QTY]
									,[ACTUAL_OUTBOUND_PACKAGE]
									,[ACTUAL_OUTBOUND_PACKAGE_QTY]
									,[CURRENT_OUTBOUND_PACKAGE]
									,[CURRENT_OUTBOUND_PACKAGE_QTY]
									,[PO_NO]
									,[POSNR]
									,[PACKAGE_UTENSIL]
									,[CAPACITY]
									,[OPRTR]
									,[COMMENTS]
									,[CREATE_USER]
									,[CREATE_DATE]
									,[UPDATE_USER]
									,[UPDATE_DATE]
									,[RETURN_REPORT_FLAG])
						SELECT @OUTBOUNDDELIVERYRETURN_ID  --[@OUTBOUNDDELIVERYRETURN_ID]
									,@VBELN                --[OUTBOUNDDELIVERY_NO]
									,A.WERKS               --[PLANT]
									,B.ASSEMBLY_LINE       --[ASSEMBLY_LINE]
									,@WM_NO                --[WM_NO]
									,A.LGORT               --[ZONE_NO]
									,B.DLOC                --[DLOC]
									,NULL                  --[DOCK]
									,@SUPPLIER_NUM         --[SUPPLIER_NUM]
									,B.PACKAGE_MODEL       --[PACKAGE_MODEL]
									,MEINS                 --[MEASURING_UNIT_NO]
									,A.ZYSQJQ              --[PACKAGE]
									,Null                  --[NUM]
									,Null                  --[BOX_NUM]
									,MATNR                 --[PART_NO]
									,NULL                  --[IDENTIFY_PART_NO]
									,B.PART_CNAME          --[PART_CNAME]
									,B.PART_ENAME          --[PART_ENAME]
									,B.PART_CLS            --[PART_TYPE]
									,Null                  --[BOX_PARTS]
									,Case When IsNull(A.ZYSQJQ,0)>0 Then IsNull(CEILING(A.LFIMG/(A.ZYSQJQ*1.0)),0)  
											Else 0 End                 --[REQUIRED_OUTBOUND_PACKAGE]
									,LFIMG                           --[REQUIRED_OUTBOUND_PACKAGE_QTY]
									,NULL                            --[ACTUAL_OUTBOUND_PACKAGE]
									,NULL                            --[ACTUAL_OUTBOUND_PACKAGE_QTY]
									,Case When IsNull(A.ZYSQJQ,0)>0 Then IsNull(CEILING(A.LFIMG/(A.ZYSQJQ*1.0)),0)  
											Else 0 End               --[CURRENT_OUTBOUND_PACKAGE] 
									,LFIMG                           --[CURRENT_OUTBOUND_PACKAGE_QTY]  按照陈向峰要求，将当前收获数修改为需求数
									,A.EBELN						 --[PO_NO]
									,A.EBELP						 --[POSNR]
									,A.ZYSQJC						--[PACKAGE_UTENSIL]
									,A.ZYSQJQ						--[CAPACITY]								
									,NULL                           --[OPRTR]
									,NULL                           --[COMMENTS]
									,'admin'
									,Getdate()
									,NULL
									,NULL
									,0
								FROM LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN] A 
								LEFT JOIN LES.TM_BAS_PARTS_STOCK B ON (B.PLANT=A.WERKS AND B.PART_NO=A.MATNR AND B.ZONE_NO=A.LGORT)
								WHERE VBELN=@VBELN
									And ISNULL(PROCESS_FLAG,0)=0

				END
				ELSE IF(@OrderType=-1)
				BEGIN
					--不生成条码表信息

					--创建收货入库单
					--EXEC LES.PROC_WMM_CREATERECEIVE @InsertId
           
					INSERT INTO [LES].[TT_WMM_OUTBOUNDDELIVERY]
								([OUTBOUNDDELIVERY_NO]
								,[OUTBOUNDDELIVERY_TYPE]
								,[PLANT]
								,[WM_NO]
								,[DOCK]
								,[ZONE_NO]
								,[SUPPLIER_NUM]
								,[SUPPLIER_NAME]
								,[SUPPLIER_ADDRESS]
								,[TRANS_SUPPLIER_NUM]
								,[RECEIVE_COMPANY_NUM]
								,[RECEIVE_COMPANY_NAME]
								,[RECEIVE_COMPANY_ADDRESS]
								,[RECEIVEER]
								,[PHONENUM]
								,[TRAN_TIME]
								,[ACTUAL_OUTBOUNDDELIVERY_TIME]
								,[EXPECT_ARRIVAL_TIME]
								,[ACTUAL_ARRIVAL_TIME]
								,[CONFIRM_FLAG]
								,[OPRTR]
								,[ERP_FLAG]
								,[COMMENTS]
								,[CREATE_USER]
								,[CREATE_DATE]
								,[UPDATE_USER]
								,[UPDATE_DATE])
							VALUES
								(@VBELN
								,@OrderType
								,@PLANT
								,@WM_NO
								,@DOCK
								,@ZONE_NO
								,@LIFNR
								,@LINAME
								,@LIADRC
								,@TRANS_SUPPLIER_NUM
								,@KUNNR
								,@KUNAME
								,@KUADRC
								,@LXRNAM
								,@LXRTEL
								,@TRAN_TIME
								,Null
								,@EXPECT_ARRIVAL_TIME
								,Null
								,1
								,Null
								,0
								,@TEXT1
								,'admin'
								,Getdate()
								,Null
								,Null)
               
					Declare @OUTBOUNDDELIVERY_ID as BIGINT
					Set @OUTBOUNDDELIVERY_ID=@@IDENTITY

					INSERT INTO [LES].[TT_WMM_OUTBOUNDDELIVERY_DETAIL]
										([OUTBOUNDDELIVERY_ID]
										,[OUTBOUNDDELIVERY_NO]
										,[PLANT]
										,[ASSEMBLY_LINE]
										,[WM_NO]
										,[ZONE_NO]
										,[DLOC]
										,[DOCK]
										,[SUPPLIER_NUM]
										,[MEASURING_UNIT_NO]
										,[PACKAGE_MODEL]
										,[PACKAGE]
										,[NUM]
										,[BOX_NUM]
										,[PART_NO]
										,[IDENTIFY_PART_NO]
										,[PART_CNAME]
										,[PART_ENAME]
										,[PART_TYPE]
										,[BOX_PARTS]
										,[REQUIRED_OUTBOUND_PACKAGE]
										,[REQUIRED_OUTBOUND_PACKAGE_QTY]
										,[ACTUAL_OUTBOUND_PACKAGE]
										,[ACTUAL_OUTBOUND_PACKAGE_QTY]
										,[CURRENT_OUTBOUND_PACKAGE]
										,[CURRENT_OUTBOUND_PACKAGE_QTY]
										,[PO_NO]
										,[POSNR]
										,[PACKAGE_UTENSIL]
										,[CAPACITY]
										,[OPRTR]
										,[COMMENTS]
										,[CREATE_USER]
										,[CREATE_DATE]
										,[UPDATE_USER]
										,[UPDATE_DATE])
									SELECT @OUTBOUNDDELIVERY_ID  --[OUTBOUNDDELIVERY_ID]
										,@VBELN                --[OUTBOUNDDELIVERY_NO]
										,WERKS                 --[PLANT]
										,B.ASSEMBLY_LINE       --[ASSEMBLY_LINE]
										,@WM_NO                --[WM_NO]
										,@ZONE_NO             --[ZONE_NO]
										,B.DLOC                --[DLOC]
										,NULL                  --[DOCK]
										,@SUPPLIER_NUM         --[SUPPLIER_NUM]						
										,MEINS                 --[MEASURING_UNIT_NO]
										,B.PACKAGE_MODEL       --[PACKAGE_MODEL]
										,A.ZYSQJQ              --[PACKAGE]
										,Null                  --[NUM]
										,Null                  --[BOX_NUM]
										,MATNR                 --[PART_NO]
										,NULL                  --[IDENTIFY_PART_NO]
										,B.PART_CNAME          --[PART_CNAME]
										,B.PART_ENAME          --[PART_ENAME]
										,B.PART_CLS             --[PART_TYPE]
										,Null                  --[BOX_PARTS]
										,CASE WHEN ISNULL(A.ZYSQJQ,0)>0 THEN IsNull(CEILING(A.LFIMG/(A.ZYSQJQ*1.0)),0)  
												Else 0 End                 --[REQUIRED_OUTBOUND_PACKAGE]
										,LFIMG                           --[REQUIRED_OUTBOUND_PACKAGE_QTY]
										,NULL                            --[ACTUAL_OUTBOUND_PACKAGE]
										,NULL                            --[ACTUAL_OUTBOUND_PACKAGE_QTY]
										,CASE WHEN ISNULL(A.ZYSQJQ,0)>0 THEN IsNull(CEILING(A.LFIMG/(A.ZYSQJQ*1.0)),0)  
												Else 0 End               --[CURRENT_OUTBOUND_PACKAGE]
										,LFIMG                           --[CURRENT_OUTBOUND_PACKAGE_QTY] 按照陈向峰要求，将当前收获数修改为需求数
										,A.EBELN
										,A.EBELP
										,A.ZYSQJC
										,A.ZYSQJQ
										,NULL                            --[OPRTR]
										,NULL                            --[COMMENTS]
										,'admin'
										,Getdate()
										,NULL
										,NULL
									FROM LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN] A 
									LEFT JOIN LES.TM_BAS_PARTS_STOCK B ON (A.WERKS=B.PLANT AND A.MATNR=B.PART_NO AND B.ZONE_NO=A.LGORT)
									WHERE VBELN=@VBELN
										And ISNULL(PROCESS_FLAG,0)=0

									
										Insert Into [LES].[TL_SYS_EVENT_LOG]
											([EVENT_TIME]
											,[EVENT_ID]
											,[EVENT_SOURCE]
											,[EVENT_STATE]
											,[EVENT_TYPE]
											,[EVENT_LEVEL]
											,[EVENT_DETAIL])
											select Getdate()
											,301
											,'SAP下发销售出库零件仓库信息不存在'
											,0
											,10200
											,2
											,'SEQ_ID：'+Cast(@SEQ_ID as varchar(20))+' 销售出库单号：' + @VBELN + ' 零件号：' + A.MATNR + '的零件仓库信息不存在' 
										 from  LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN] A
										 LEFT JOIN LES.TM_BAS_PARTS_STOCK B ON (A.WERKS=B.PLANT AND A.MATNR=B.PART_NO AND B.ZONE_NO=A.LGORT)
										 WHERE VBELN=@VBELN
										And ISNULL(PROCESS_FLAG,0)=0 And ISNULL(B.ZONE_NO,'')=''
										
				END
				Else IF(@OrderType=-6)
				Begin
					--记录日志
					Insert Into [LES].[TL_SYS_EVENT_LOG]
								([EVENT_TIME]
								,[EVENT_ID]
								,[EVENT_SOURCE]
								,[EVENT_STATE]
								,[EVENT_TYPE]
								,[EVENT_LEVEL]
								,[EVENT_DETAIL])
						Values(	Getdate()
								,301
								,'SAP下发销售出库单数据错误'
								,0
								,10200
								,2
								,'SEQ_ID：'+Cast(@SEQ_ID as varchar(20))+' 销售出库单号：' + @VBELN + ' 交货单类型LFART：' + @LFART + ' 不在枚举范围（NLC1/NLC2/ZNR1/ZNR2/J/T）' )
				End


				--更新处理状态
				Update LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN] 
					Set PROCESS_FLAG=1
						,PROCESS_TIME=Getdate() 
					Where VBELN=@VBELN
					And ISNULL(PROCESS_FLAG,0)=0 

				Update LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN] 
					Set PROCESS_FLAG=1
						,PROCESS_TIME=Getdate() 
					Where SEQ_ID=@SEQ_ID
			END
			FETCH  NEXT  FROM RUNSHEET_CUR INTO @SEQ_ID
											   ,@VBELN
											   ,@ERDAT
											   ,@ERZET
											   ,@LFART
											   ,@KUNNR
											   ,@LFDAT
											   ,@LFUHR
											   ,@KUNAME
											   ,@KUADRC
											   ,@LXRNAM
											   ,@LXRTEL
											   ,@LIFNR
											   ,@LINAME
											   ,@LIADRC
											   ,@TEXT1
											   ,@DEAL_FLAG
											   ,@PROCESS_FLAG
											   ,@PROCESS_TIME
											   ,@COMMENTS
		END
		
		Commit Tran

	End Try
	Begin Catch

		Rollback Tran
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
		SELECT getdate(),'INTERFACE','PROC_WMS_OUTBOUNDDELIVERY_RUNSHEET','Procedure',ISNULL(error_message(),'')+';TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN.SEQ_ID:'+IsNull(@SEQ_ID,'')+',TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN.VBELN'+IsNull(@VBELN,''),ERROR_LINE()
		--更新错误状态
		Update LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN] 
			Set PROCESS_FLAG=9
				,PROCESS_TIME=Getdate() 
			Where VBELN=@VBELN
			And ISNULL(PROCESS_FLAG,0)=0 

		Update LES.[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN] 
			Set PROCESS_FLAG=9
				,PROCESS_TIME=Getdate() 
			Where SEQ_ID=@SEQ_ID

	End Catch

	CLOSE  RUNSHEET_CUR
	DEALLOCATE RUNSHEET_CUR
END