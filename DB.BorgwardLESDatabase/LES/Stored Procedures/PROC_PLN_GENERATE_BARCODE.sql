﻿
CREATE PROC [LES].[PROC_PLN_GENERATE_BARCODE]
(
	@GenerateDate datetime
)
as
		set nocount on
		declare detail_crsr cursor  fast_forward  for 
			select SUPPLIER_NUM,PART_NO,IDENTIFY_PART_NO,PART_CNAME,BOX_PARTS
				,PICKUP_SEQ_NO,RDC_DLOC,MEASURING_UNIT_NO,INBOUND_PACKAGE_MODEL,REQUIRED_INBOUND_PACKAGE_QTY
				,REQUIRED_INBOUND_PACKAGE,[INBOUND_PACKAGE]
				from LES.TT_TWD_RUNSHEET_DETAIL where TWD_RUNSHEET_SN = @GenerateDate


		declare @SUPPLIER_NUM nvarchar(12)
		declare @PART_NO nvarchar(20)
		declare @IDENTIFY_PART_NO nvarchar(20)
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
		
		Select @BATCHNO=CONVERT(varchar(100), GETDATE(), 112)
		open detail_crsr
		fetch next from detail_crsr into @SUPPLIER_NUM,@PART_NO,@IDENTIFY_PART_NO,@PART_CNAME,@BOX_PARTS,@PICKUP_SEQ_NO
			,@RDC_DLOC,@MEASURING_UNIT_NO,@INHOUSE_PACKAGE_MODEL,@REQUIRED_INBOUND_PACKAGE_QTY,@REQUIRED_INBOUND_PACKAGE,@INBOUND_PACKAGE

		while( @@fetch_status = 0 )
		begin
			while( @REQUIRED_INBOUND_PACKAGE >0)
			begin
				declare @barCodeNo nvarchar(32)
				exec LES.[PROC_TWD_GET_NEXT_BARCODE_SEQUENCE] 'BARCODE', @barCodeNo output
				INSERT INTO [LES].[TT_TWD_RUNSHEET_BARCODE]
				   ([TWD_RUNSHEET_SN]
				   ,[SUPPLIER_NUM]
				   ,[PART_NO]
				   ,[BARCODE_DATA]
				   ,[IDENTIFY_PART_NO]
				   ,[PART_CNAME]
				   ,[BOX_PARTS]
				   ,[PICKUP_SEQ_NO]
				   ,[RDC_DLOC]
				   ,[MEASURING_UNIT_NO]
				   ,[INHOUSE_PACKAGE_MODEL]
				   ,[REQUIRED_INBOUND_PACKAGE_QTY]
				   ,[COMMENTS],[BARCODE_TYPE],BATTH_NO)
				VALUES(1,@SUPPLIER_NUM,@PART_NO,@barCodeNo,@IDENTIFY_PART_NO,@PART_CNAME,@BOX_PARTS,@PICKUP_SEQ_NO,@RDC_DLOC,@MEASURING_UNIT_NO
						,@INHOUSE_PACKAGE_MODEL,@INBOUND_PACKAGE,'','PLAN',@BATCHNO)
					

				set @REQUIRED_INBOUND_PACKAGE = @REQUIRED_INBOUND_PACKAGE -1
			end
			fetch next from detail_crsr into @SUPPLIER_NUM,@PART_NO,@IDENTIFY_PART_NO,@PART_CNAME,@BOX_PARTS,@PICKUP_SEQ_NO
				,@RDC_DLOC,@MEASURING_UNIT_NO,@INHOUSE_PACKAGE_MODEL,@REQUIRED_INBOUND_PACKAGE_QTY,@REQUIRED_INBOUND_PACKAGE,@INBOUND_PACKAGE
			
		end

		close detail_crsr
		deallocate detail_crsr
		set nocount off