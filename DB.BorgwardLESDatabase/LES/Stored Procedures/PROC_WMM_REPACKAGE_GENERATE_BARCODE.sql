CREATE PROC [LES].[PROC_WMM_REPACKAGE_GENERATE_BARCODE]
(
	@RepackgeNo nvarchar(50)
)
as

		set nocount on
		declare detail_crsr cursor fast_forward  for 
			select
				D.REPACKAGE_NO,
				D.WM_NO,
				D.ZONE_NO,
				D.ULOC, 
				D.PART_NO,
				D.PART_CNAME,
				D.PACKAGE_MODEL,
				D.PACKAGE,
				D.INHOUSE_PACKAGE_MODEL,
				D.INHOUSE_PACKAGE,
				D.NUM,
				P.SUPPLIER_NUM,
				H.PLANT,
				P.PART_UNITS AS MEASURING_UNIT_NO,	
				D.ULOC	AS STORAGE_LOCATION,
				S.Supper_Zone_Dloc AS INNER_LOCATION,
				P.PART_NICKNAME
			from 
				LES.TT_WMM_REPACKAGE_DETAIL (NOLOCK) D
				LEFT JOIN LES.TT_WMM_REPACKAGE_HEAD (NOLOCK) H ON D.REPACKAGE_ID = H.REPACKAGE_ID
				LEFT JOIN LES.TM_BAS_MAINTAIN_PARTS (NOLOCK) P ON D.PART_NO = P.PART_NO
				LEFT JOIN [LES].TM_BAS_PARTS_STOCK (NOLOCK) S 
				ON S.PART_NO = D.PART_NO and S.WM_NO=D.WM_NO and S.ZONE_NO=D.ZONE_NO and S.PLANT=H.PLANT
			where D.REPACKAGE_NO=(case when (PATINDEX('%[A-Za-z]%', @RepackgeNo)=1) then @RepackgeNo else '000000'+@RepackgeNo end)
			
		declare @REPACKAGE_NO nvarchar(50)
		declare @WM_NO nvarchar(10)
		declare @ZONE_NO nvarchar(20)
		declare @ULOC nvarchar(30)
		declare @PART_NO nvarchar(30)
		declare @PART_CNAME nvarchar(100)
		declare @PACKAGE_MODEL nvarchar(30)
		declare @PACKAGE int
		declare @INHOUSE_PACKAGE_MODEL nvarchar(30)
		declare @INHOUSE_PACKAGE int
		declare @NUM numeric(18,2)
		DECLARE @SUPPLIER_NUM nvarchar(20)
		DECLARE @PLANT NVARCHAR(10)
		DECLARE @MEASURING_UNIT_NO NVARCHAR(20)
		declare @STORAGE_LOCATION nvarchar(30)--库位地址
		declare @INNER_LOCATION nvarchar(50)--超市地址
		declare @PART_NICKNAME nvarchar(50)
		declare @LINE_POSITION nvarchar(100)--线边地址

		open detail_crsr
		fetch next from detail_crsr into 
			@REPACKAGE_NO,
			@WM_NO,
			@ZONE_NO,
			@ULOC,
			@PART_NO,
			@PART_CNAME,
			@PACKAGE_MODEL,
			@PACKAGE,
			@INHOUSE_PACKAGE_MODEL,
			@INHOUSE_PACKAGE,
			@NUM,
			@SUPPLIER_NUM,
			@PLANT,
			@MEASURING_UNIT_NO,
			@STORAGE_LOCATION,
			@INNER_LOCATION,
			@PART_NICKNAME
		
		declare @TOTAL numeric(18,2)
		--set @TOTAL = @NUM * @PACKAGE;
		SET @TOTAL = @NUM
		while( @@fetch_status = 0 )
		begin
			while(@TOTAL > 0)
			begin
				declare @barCodeNo nvarchar(32) 
				DECLARE @CreatedOn DATETIME
				exec LES.[PROC_SYS_GET_NEXT_BARCODE_SEQUENCE] 'BARCODE', @barCodeNo OUTPUT
				SET @CreatedOn=getdate()

				--获取目的存储区
				declare @O_ZONE_NO nvarchar(100)
				select top 1 @O_ZONE_NO=REPACKAGE_ZONE from LES.TM_WMM_ZONES where ZONE_NO=@ZONE_NO

				--根据目的存储区获取 生产线 然后再加上生产线 到拉动关系中获取 线边地址
				select top 1 @LINE_POSITION=L.STORAGE_LOCATION from 
				[LES].TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD L
			 	JOIN LES.TM_WMM_ZONES Z ON Z.ZONE_NO=L.SUPPLIER_NUM
				JOIN [LES].TM_BAS_PARTS_STOCK (NOLOCK) S 
				ON S.WM_NO=Z.WM_NO and S.ZONE_NO=Z.ZONE_NO and S.PLANT=Z.PLANT
				and S.PART_NO = L.PART_NO and L.ASSEMBLY_LINE=S.ASSEMBLY_LINE
				where L.PLANT=@PLANT and L.PART_NO=@PART_NO and L.SUPPLIER_NUM=ISNULL(@O_ZONE_NO, '') 

				INSERT INTO [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE]
				   ([PLAN_ASN_RUNSHEET_NO]
				   ,[PART_NO]
				   ,[PART_CNAME]
				   ,[BARCODE_DATA]
				   ,[INHOUSE_PACKAGE_MODEL]
				   ,[REQUIRED_INBOUND_PACKAGE_QTY]
				   ,[CREATE_DATE]
				   ,SUPPLIER_NUM
				   ,SUPPLIER_NAME
				   ,BOX_PARTS
				   ,MEASURING_UNIT_NO
				   ,[TWD_RUNSHEET_NO]
				   ,PLANT
				   ,STORAGE_LOCATION
				   ,INNER_LOCATION
				   ,PART_NICKNAME
				   ,[LINE_POSITION]
				   )
				VALUES(
					@RepackgeNo,
					@PART_NO,
					@PART_CNAME,
					@barCodeNo,
					@INHOUSE_PACKAGE_MODEL,
					@INHOUSE_PACKAGE,
					@CreatedOn,
					ISNULL(@SUPPLIER_NUM, ''),
					'',
					'',
					ISNULL(@MEASURING_UNIT_NO, ''),
					@REPACKAGE_NO,
					@PLANT,
					@STORAGE_LOCATION,
					@INNER_LOCATION,
					@PART_NICKNAME,
					@LINE_POSITION)
			
				--set @TOTAL = @TOTAL - @INHOUSE_PACKAGE
				set @TOTAL = @TOTAL - 1
			end
					
			fetch next from detail_crsr into 
				@REPACKAGE_NO,
				@WM_NO,
				@ZONE_NO,
				@ULOC,
				@PART_NO,
				@PART_CNAME,
				@PACKAGE_MODEL,
				@PACKAGE,
				@INHOUSE_PACKAGE_MODEL,
				@INHOUSE_PACKAGE,
				@NUM,
				@SUPPLIER_NUM,
				@Plant,
				@MEASURING_UNIT_NO,
				@STORAGE_LOCATION,
				@INNER_LOCATION,
				@PART_NICKNAME
			set @TOTAL = @NUM
		end
		
		-- 翻包单确认后打印新的翻包箱条码
		INSERT INTO LES.TT_WMS_AUTOPRINT_BARCODE
			([PLAN_ASN_RUNSHEET_NO]
			,[PART_NO]
			,[PART_CNAME]
			,[BARCODE_DATA]
			,[INHOUSE_PACKAGE_MODEL]
			,[REQUIRED_INBOUND_PACKAGE_QTY]
			,[CREATE_DATE]
			,SUPPLIER_NUM)
			SELECT
				[PLAN_ASN_RUNSHEET_NO]
				,[PART_NO]
				,[PART_CNAME]
				,[BARCODE_DATA]
				,[INHOUSE_PACKAGE_MODEL]
				,[REQUIRED_INBOUND_PACKAGE_QTY]
				,[CREATE_DATE]
				,SUPPLIER_NUM
			FROM
				[LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE]
			WHERE
				[PLAN_ASN_RUNSHEET_NO] = @RepackgeNo

		close detail_crsr
		deallocate detail_crsr
		set nocount off