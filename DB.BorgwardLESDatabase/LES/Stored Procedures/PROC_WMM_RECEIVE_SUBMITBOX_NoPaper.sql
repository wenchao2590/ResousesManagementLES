﻿



CREATE PROC [LES].[PROC_WMM_RECEIVE_SUBMITBOX_NoPaper] 
	@RECEIVE_ID int,
	@PART_NO nvarchar(50),
	@BARCODE_DATA NVARCHAR(50),
	@ActualQty int,
	@Create_User NVARCHAR(50)
AS
BEGIN
BEGIN TRAN
BEGIN TRY
	SET @ActualQty = ISNULL(@ActualQty, 0)
	
	IF EXISTS(SELECT * FROM LES.TT_WMM_RECEIVE_DETAIL WHERE RECEIVE_ID=@RECEIVE_ID AND PART_NO=@PART_NO)
	BEGIN
		IF EXISTS(SELECT 1 FROM [LES].[TM_WMM_TRAN_DETAILS] WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=1 AND TRAN_STATE=0)
		BEGIN
			DECLARE @LASTNUM DECIMAL(18,2)
			SELECT @LASTNUM=ISNULL(NUM,0) FROM [LES].[TM_WMM_TRAN_DETAILS] WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=1 AND TRAN_STATE=0
			
			UPDATE LES.TT_WMM_RECEIVE_DETAIL 
			SET REQUIRED_QTY=ISNULL(Current_QTY, 0)-@LASTNUM+@ActualQty,
				Current_QTY =ISNULL(Current_QTY, 0)-@LASTNUM+@ActualQty
			WHERE RECEIVE_ID=@RECEIVE_ID AND PART_NO=@PART_NO
			
			DECLARE @RECEIVE_NO NVARCHAR(50)
			SELECT @RECEIVE_NO=RECEIVE_NO FROM LES.TT_WMM_RECEIVE WHERE RECEIVE_ID=@RECEIVE_ID
	
			UPDATE LES.TM_WMM_TRAN_DETAILS SET NUM=@ActualQty WHERE TRAN_NO=@RECEIVE_NO AND BARCODE_DATA=@BARCODE_DATA
		END
		ELSE
		BEGIN
			UPDATE LES.TT_WMM_RECEIVE_DETAIL 
			SET 
			REQUIRED_QTY=ISNULL(Current_QTY, 0)+@ActualQty,
			Current_QTY =ISNULL(Current_QTY, 0)+@ActualQty,
			REQUIRED_BOX_NUM =ISNULL(Current_BOX_NUM, 0)+1,
			Current_BOX_NUM =ISNULL(Current_BOX_NUM, 0)+1
			WHERE RECEIVE_ID=@RECEIVE_ID AND PART_NO=@PART_NO
			
			INSERT INTO LES.TM_WMM_TRAN_DETAILS (
				BARCODE_DATA ,
				PART_NO,
				BOX_NUM,
				Num,
				Plant,
				WM_NO,
				ZONE_NO,
				Dloc,
				PACKAGE_MODEL,
				PACKAGE,
				Tran_State,
				Supplier_Num,
				BOX_PARTS,
				Part_Cname,
				MEASURING_UNIT_NO,
				Tran_Type,
				Tran_No,
				Create_Date,
				Create_User,
				Tran_Date)
				select
				@BARCODE_DATA,--BARCODE_DATA
				@PART_NO,--PART_NO
				1,--BOX_NUM
				@ActualQty,--Num
				A.PLANT,--Plant
				A.WM_NO,--WM_NO
				A.ZONE_NO,--ZONE_NO
				B.DLOC,--Dloc
				B.PACKAGE_MODEL,--PACKAGE_MODEL
				B.PACKAGE,--PACKAGE
				0,--Tran_State
				A.SUPPLIER_NUM,--Supplier_Num
				B.BOX_PARTS,
				B.PART_CNAME,--Part_Cname
				B.MEASURING_UNIT_NO,--MEASURING_UNIT_NO
				1,--Tran_Type
				A.RECEIVE_NO,--Tran_No
				GETDATE(),--Create_Date
				@Create_User,--Create_User
				GETDATE()--Tran_Date
				from les.TT_WMM_RECEIVE A
				inner join les.TT_WMM_RECEIVE_DETAIL B on A.RECEIVE_ID=B.RECEIVE_ID AND B.PART_NO=@PART_NO
				where A.RECEIVE_ID=@RECEIVE_ID
		END
	END
	ELSE
	BEGIN
		--DECLARE @Plant NVARCHAR(5),@Wm_No NVARCHAR(10),@Zone_No NVARCHAR(20),@DLOC NVARCHAR(30),@Package_Model NVARCHAR(30),@BOX_PARTS NVARCHAR(10)
		--DECLARE @PACKAGE INT
		--SELECT @Plant=PLANT,@Wm_No=WM_NO,@Zone_No=ZONE_NO,@RECEIVE_NO=RECEIVE_NO FROM LES.TT_WMM_RECEIVE WHERE RECEIVE_ID=@RECEIVE_ID
		--DECLARE @SupplierNum NVARCHAR(12),@MEASURING_UNIT_NO NVARCHAR(20),@PartCname NVARCHAR(50)
		--SELECT 
		--@SupplierNum=SUPPLIER_NUM,
		--@DLOC=DLOC,
		--@MEASURING_UNIT_NO=PART_UNITS,
		--@PartCname=PART_CNAME,
		--@PACKAGE = PACKAGE
		--FROM LES.TM_BAS_PARTS_STOCK WHERE ZONE_NO=@Zone_No AND PART_NO=@PART_NO

		INSERT INTO LES.TT_WMM_RECEIVE_DETAIL (
			Receive_Id,
			REQUIRED_BOX_NUM,
			REQUIRED_QTY,
			Current_BOX_NUM ,
			Current_QTY ,
			Create_Date,
			Part_Cname,
			Part_No,
			MEASURING_UNIT_NO ,
			Plant,
			Wm_No,
			Zone_No,
			DLOC, 
			Create_User,
			package,
			PACKAGE_MODEL,
			SUPPLIER_NUM)
			select 
			@RECEIVE_ID,
			1,
			@ActualQty,
			1,
			@ActualQty,
			GETDATE(),
			B.PART_CNAME,
			@PART_NO,
			B.PART_UNITS,
			A.PLANT,
			A.WM_NO,
			A.ZONE_NO,
			B.DLOC,
			@Create_User,
			B.PACKAGE,
			B.PACKAGE_MODEL,
			B.SUPPLIER_NUM
			from LES.TT_WMM_RECEIVE A
			inner join LES.TM_BAS_PARTS_STOCK B ON A.PLANT=B.PLANT AND A.ZONE_NO=B.ZONE_NO AND B.PART_NO=@PART_NO
			WHERE A.RECEIVE_ID=@RECEIVE_ID
		
		INSERT INTO LES.TM_WMM_TRAN_DETAILS (
				BARCODE_DATA ,
				PART_NO,
				BOX_NUM,
				Num,
				Plant,
				WM_NO,
				ZONE_NO,
				Dloc,
				PACKAGE_MODEL,
				PACKAGE,
				Tran_State,
				Supplier_Num,
				Part_Cname,
				MEASURING_UNIT_NO,
				Tran_Type,
				Tran_No,
				Create_Date,
				Create_User,
				Tran_Date)
				select
				@BARCODE_DATA,--BARCODE_DATA
				@PART_NO,--PART_NO
				1,--BOX_NUM
				@ActualQty,--Num
				A.PLANT,--Plant
				A.WM_NO,--WM_NO
				A.ZONE_NO,--ZONE_NO
				B.DLOC,--Dloc
				B.PACKAGE_MODEL,--PACKAGE_MODEL
				B.PACKAGE,--PACKAGE
				0,--Tran_State
				A.SUPPLIER_NUM,--Supplier_Num
				B.PART_CNAME,--Part_Cname
				B.PART_UNITS,--MEASURING_UNIT_NO
				1,--Tran_Type
				A.RECEIVE_NO,--Tran_No
				GETDATE(),--Create_Date
				@Create_User,--Create_User
				GETDATE()--Tran_Date
				from les.TT_WMM_RECEIVE A
				inner join LES.TM_BAS_PARTS_STOCK B ON A.PLANT=B.PLANT AND A.ZONE_NO=B.ZONE_NO AND B.PART_NO=@PART_NO
				where A.RECEIVE_ID=@RECEIVE_ID
	END
END TRY   
BEGIN CATCH   
IF @@trancount > 0   
ROLLBACK TRAN   
END CATCH  
if @@trancount > 0   
COMMIT TRAN  
END