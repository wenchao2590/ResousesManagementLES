

CREATE PROC [LES].[PROC_WMM_OUTBOUNDDELIVERY_GENERATE_BARCODE]
(
	@OUTBOUNDDELIVERY_ID INT
)
as
		set nocount on
		declare detail_crsr cursor fast_forward  for 
			select M.OUTBOUNDDELIVERY_NO,D.WM_NO,D.ZONE_NO,D.DLOC,D.PART_NO,D.PART_CNAME,D.PACKAGE_MODEL,D.PACKAGE,
				D.REQUIRED_OUTBOUND_PACKAGE_QTY,D.SUPPLIER_NUM,P.PART_UNITS AS MEASURING_UNIT_NO,M.PLANT,P.PART_NICKNAME,S.DLOC,M.DOCK
			from  LES.TT_WMM_OUTBOUNDDELIVERY_DETAIL (NOLOCK) D
			LEFT JOIN LES.TT_WMM_OUTBOUNDDELIVERY (NOLOCK) M ON D.OUTBOUNDDELIVERY_ID = M.OUTBOUNDDELIVERY_ID
			LEFT JOIN LES.TM_BAS_MAINTAIN_PARTS (NOLOCK) P ON D.PART_NO = P.PART_NO
			--web 入库的时候会传递package, 所有不用考虑package为Null的情况
			LEFT JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON S.ZONE_NO = D.ZONE_NO AND S.PART_NO = D.PART_NO
			where  M.OUTBOUNDDELIVERY_ID = @OUTBOUNDDELIVERY_ID

			--SELECT NUM,ACTUAL_QTY, PACKAGE, * FROM LES.TT_WMM_RECEIVE_DETAIL
			--SELECT PACKAGE_MODEL,PACKAGE, * FROM LES.TM_BAS_PARTS_STOCK

		declare @OUTBOUNDDELIVERY_NO nvarchar(20)
		declare @WM_NO nvarchar(10)
		declare @ZONE_NO nvarchar(20)
		declare @DLOC nvarchar(20)
		declare @PART_NO nvarchar(30)
		declare @PART_CNAME nvarchar(100)
		declare @PACKAGE_MODEL nvarchar(30)
		declare @PACKAGE int
		declare @NUM INT
		declare @MEASURING_UNIT_NO nvarchar(10)
		--DECLARE @BOX_NUM INT
		DECLARE @SUPPLIER_NUM NVARCHAR(24)
		declare @PLANT nvarchar(5)
		declare @PART_NICKNAME nvarchar(50)
		declare @DOCK nvarchar(10)

		open detail_crsr
		fetch next from detail_crsr into @OUTBOUNDDELIVERY_NO,@WM_NO,@ZONE_NO,@DLOC,@PART_NO,@PART_CNAME,@PACKAGE_MODEL,@PACKAGE,
			@NUM,@SUPPLIER_NUM,@MEASURING_UNIT_NO,@PLANT,@PART_NICKNAME,@DLOC,@DOCK
		
		declare @TOTAL INT
		set @TOTAL = @NUM;
		while( @@fetch_status = 0 )
		begin
			--WHILE(@TOTAL >= @PACKAGE)
			while(@TOTAL > 0)
			begin
				declare @barCodeNo nvarchar(32) 
				exec LES.[PROC_SYS_GET_NEXT_BARCODE_SEQUENCE] 'BARCODE', @barCodeNo OUTPUT

				--IF(@TOTAL >0)
				-- BEGIN
				--  SET @NUM = @TOTAL
				-- END
    --            ELSE
				-- BEGIN
				--  SET @NUM = @PACKAGE
				-- END
				  
				INSERT INTO [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE]
				   ([PLAN_ASN_RUNSHEET_NO]
				   ,[PART_NO]
				   ,[PART_CNAME]
				   ,[BARCODE_DATA]
				   ,INNER_LOCATION
				   ,[REQUIRED_INBOUND_PACKAGE_QTY]
				   ,SUPPLIER_NUM
				   ,SUPPLIER_NAME
				   ,[CREATE_DATE]
				   ,[MEASURING_UNIT_NO]
				   ,BOX_PARTS,[PLANT]
				   ,PART_NICKNAME
				   ,STORAGE_LOCATION
				   ,DOCK
				   ,INHOUSE_PACKAGE_MODEL)
				VALUES(
					@OUTBOUNDDELIVERY_NO
					,@PART_NO
					,@PART_CNAME
					,@barCodeNo
					,@DLOC
					,@PACKAGE
					,ISNULL(@SUPPLIER_NUM, '')
					,''
					,GETDATE()
					,ISNULL(@MEASURING_UNIT_NO, '')
					,''
					,@PLANT
					,@PART_NICKNAME
					,@DLOC
					,@DOCK
					,@PACKAGE_MODEL)

				set @TOTAL = @TOTAL - @PACKAGE
			end
					
			fetch next from detail_crsr into @OUTBOUNDDELIVERY_NO,@WM_NO,@ZONE_NO,@DLOC,@PART_NO,@PART_CNAME,@PACKAGE_MODEL,@PACKAGE,
			@NUM,@SUPPLIER_NUM,@MEASURING_UNIT_NO,@PLANT,@PART_NICKNAME,@DLOC,@DOCK

			set @TOTAL = @NUM;
		end

		close detail_crsr
		deallocate detail_crsr
		set nocount off