-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	TWD 生成条码
-- Modified Info: 
-- 添加履历号 added on  2014-09-18 by Caodaowei
-- =============================================
CREATE PROC [LES].[PROC_SYS_GENERATE_BARCODE]
(
	@runsheetSN int,
	@cls int
)
as
		set nocount on
		declare detail_crsr cursor  fast_forward  for 
			select SUPPLIER_NUM,PART_NO,PART_CNAME,BOX_PARTS
				,PICKUP_SEQ_NO,RDC_DLOC,MEASURING_UNIT_NO,INHOUSE_PACKAGE_MODEL,REQUIRED_INHOUSE_PACKAGE_QTY
				,REQUIRED_INHOUSE_PACKAGE,INHOUSE_PACKAGE
				from LES.TT_SPM_DELIVERY_RUNSHEET_DETAIL where PLAN_RUNSHEET_SN = @runsheetSN


	
		declare @SUPPLIER_NUM nvarchar(12)
		declare @PART_NO nvarchar(20)
		declare @PART_CNAME nvarchar(100)
		declare @BOX_PARTS nvarchar(10)
		declare @PICKUP_SEQ_NO int
		declare @RDC_DLOC varchar(20)
		declare @MEASURING_UNIT_NO nvarchar(1)
		declare @INHOUSE_PACKAGE_MODEL nvarchar(30)
		declare @REQUIRED_INBOUND_PACKAGE_QTY int
		declare @REQUIRED_INBOUND_PACKAGE int
		declare @BATCHNO nvarchar(30)
		declare @INBOUND_PACKAGE int
		declare @DELIVERY_LOCATION nvarchar(12)
		DECLARE @ResumeNo NVARCHAR(10) --added on  2014-09-18 by Caodaowei
		DECLARE @Plant NVARCHAR(5) --added on  2014-09-18 by Caodaowei
		DECLARE @AssembleLine NVARCHAR(10) --added on  2014-09-18 by Caodaowei
		declare @RUNSHEET_DETAIL_ID Int
		Declare @BarCode_SUPPLIER_NUM nvarchar(12)
		
		SELECT @DELIVERY_LOCATION=[DELIVERY_LOCATION_NO]
      FROM [LES].TT_SPM_DELIVERY_RUNSHEET where PLAN_RUNSHEET_SN = @runsheetSN

		Select @BATCHNO=CONVERT(varchar(100), GETDATE(), 112)
		open detail_crsr
		fetch next from detail_crsr into @SUPPLIER_NUM,@PART_NO,@PART_CNAME,@BOX_PARTS,@PICKUP_SEQ_NO
			,@RDC_DLOC,@MEASURING_UNIT_NO,@INHOUSE_PACKAGE_MODEL,@REQUIRED_INBOUND_PACKAGE_QTY,@REQUIRED_INBOUND_PACKAGE,@INBOUND_PACKAGE			

		while( @@fetch_status = 0 )
		begin

			SELECT TOP 1 @plant=PLANT,@AssembleLine=ASSEMBLY_LINE FROM LES.TT_SPM_DELIVERY_RUNSHEET WHERE PLAN_RUNSHEET_SN=@runsheetSN

			/*BarCode_SUPPLIER_NUM与SUPPLIER_NUM一致 Andy:2015-07-15*/
			--if (len(isnull(@BOX_PARTS_SHEET,''))>0)
			--begin
			--select @BarCode_SUPPLIER_NUM=[SUPPLIER_NUM] from LES.TM_TWD_BOX_PARTS where PLANT=@Plant and [BOX_PARTS]=@BOX_PARTS_SHEET
			--print '1'
			--end

			while( @REQUIRED_INBOUND_PACKAGE >0)
			begin
				declare @barCodeNo nvarchar(32)
				DECLARE @CreatedOn DATETIME
				exec LES.[PROC_SYS_GET_NEXT_BARCODE_SEQUENCE] 'BARCODE', @barCodeNo OUTPUT
				--Start Add
				SET @CreatedOn=getdate()
				SET @ResumeNo=''			
				
				/*ResumeNo现已不用 Andy:2015-07-15*/
				--SELECT TOP 1 @ResumeNo=RESUMENO FROM LES.TM_TWD_PART_RESUME
				--WHERE PLANT=@Plant and PART_NO=@PART_NO 
					--AND START_EFFECTIVE_DATE<=@CreatedOn AND EXPIRE_DATE>=@CreatedOn AND ASSEMBLY_LINE=@AssembleLine
				--End Add  on 2014-09-18 by Caodaowei

				INSERT INTO [LES].TT_SPM_DELIVERY_RUNSHEET_BARCODE
				   ([PLAN_ASN_RUNSHEET_NO]
				   ,[SUPPLIER_NUM]
				   ,[PART_NO]
				   ,[BARCODE_DATA]
				   ,[PART_CNAME]
				   ,[BOX_PARTS]
				   ,[PICKUP_SEQ_NO]
				   ,[RDC_DLOC]
				   ,[MEASURING_UNIT_NO]
				   ,[INHOUSE_PACKAGE_MODEL]
				   ,[REQUIRED_INBOUND_PACKAGE_QTY]
				   ,[BARCODE_TYPE],BATTH_NO,[CREATE_DATE],[STORAGE_LOCATION])
				VALUES(@runsheetSN,@SUPPLIER_NUM,@PART_NO,@barCodeNo,@PART_CNAME,@BOX_PARTS,@PICKUP_SEQ_NO,@RDC_DLOC,@MEASURING_UNIT_NO
						,@INHOUSE_PACKAGE_MODEL,@INBOUND_PACKAGE,'PLAN',@BATCHNO,@CreatedOn,@DELIVERY_LOCATION)
					

				set @REQUIRED_INBOUND_PACKAGE = @REQUIRED_INBOUND_PACKAGE -1
			end
			fetch next from detail_crsr into @SUPPLIER_NUM,@PART_NO,@PART_CNAME,@BOX_PARTS,@PICKUP_SEQ_NO
				,@RDC_DLOC,@MEASURING_UNIT_NO,@INHOUSE_PACKAGE_MODEL,@REQUIRED_INBOUND_PACKAGE_QTY,@REQUIRED_INBOUND_PACKAGE,@INBOUND_PACKAGE
			
		end

		close detail_crsr
		deallocate detail_crsr
		set nocount off