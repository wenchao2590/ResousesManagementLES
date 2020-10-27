﻿CREATE PROC [LES].[PROC_WMM_RECEIVE_SUBMITBOX] 
	@RECEIVE_ID int,
	@PART_NO nvarchar(50),
	@BARCODE_DATA NVARCHAR(50),
	@ActualQty int,
	@Create_User NVARCHAR(50)
AS
BEGIN
BEGIN TRAN
BEGIN TRY
	DECLARE @Plant NVARCHAR(5),@Wm_No NVARCHAR(10),@Zone_No NVARCHAR(20),@DLOC NVARCHAR(30),@RECEIVE_NO NVARCHAR(50)
	DECLARE @PACKAGE INT
	SELECT @Plant=PLANT,@Wm_No=WM_NO,@Zone_No=ZONE_NO,@RECEIVE_NO=RECEIVE_NO FROM LES.TT_WMM_RECEIVE WHERE RECEIVE_ID=@RECEIVE_ID
	DECLARE @SupplierNum NVARCHAR(12),@MEASURING_UNIT_NO NVARCHAR(20),@PartCname NVARCHAR(50)
	SELECT @SupplierNum=SUPPLIER_NUM,@DLOC=DLOC,@MEASURING_UNIT_NO=PART_UNITS,@PartCname=PART_CNAME,@PACKAGE = PACKAGE
	FROM LES.TM_BAS_PARTS_STOCK WHERE ZONE_NO=@Zone_No AND PART_NO=@PART_NO
	IF EXISTS(SELECT * FROM LES.TT_WMM_RECEIVE_DETAIL WHERE RECEIVE_ID=@RECEIVE_ID AND PART_NO=@PART_NO)
	BEGIN
		IF EXISTS(SELECT 1 FROM [LES].[TM_WMM_TRAN_DETAILS] WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=1 AND TRAN_STATE=0)
		BEGIN
			DECLARE @LASTNUM DECIMAL(18,2)
			SELECT @LASTNUM=NUM FROM [LES].[TM_WMM_TRAN_DETAILS] WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=1 AND TRAN_STATE=0
			UPDATE LES.TT_WMM_RECEIVE_DETAIL 
			SET --ACTUAL_QTY=ACTUAL_QTY-@LASTNUM+@ActualQty,
				ACTUAL_QTY =(
				CASE 
				WHEN ISNULL(ACTUAL_QTY, 0) = 0 THEN 0-@LASTNUM+@ActualQty
				ELSE ACTUAL_QTY-@LASTNUM+@ActualQty
				END)
			WHERE RECEIVE_ID=@RECEIVE_ID AND PART_NO=@PART_NO

			UPDATE LES.TM_WMM_TRAN_DETAILS SET NUM=@ActualQty WHERE TRAN_NO=@RECEIVE_NO AND BARCODE_DATA=@BARCODE_DATA
		END
		ELSE
		BEGIN
			UPDATE LES.TT_WMM_RECEIVE_DETAIL 
			SET 
			--ACTUAL_QTY=ACTUAL_QTY+@ActualQty,
			ACTUAL_QTY =(
				CASE 
				WHEN ISNULL(ACTUAL_QTY, 0) = 0 THEN 0+@ActualQty
				ELSE ACTUAL_QTY+@ActualQty
				END),
			--ACTUAL_BOX_NUM=ACTUAL_BOX_NUM+1 
			ACTUAL_BOX_NUM =(
				CASE 
				WHEN ISNULL(ACTUAL_BOX_NUM, 0) = 0 THEN 0+1
				ELSE ACTUAL_BOX_NUM+1 
				END)
			WHERE RECEIVE_ID=@RECEIVE_ID AND PART_NO=@PART_NO
			
			INSERT INTO LES.TM_WMM_TRAN_DETAILS (BARCODE_DATA ,PART_NO,BOX_NUM ,Num ,Plant ,WM_NO ,
									ZONE_NO,Dloc,Tran_State ,Supplier_Num,Part_Cname,Tran_Type,Tran_No,Create_Date,Create_User,Tran_Date)
			VALUES(@BARCODE_DATA,@PART_NO,1,@ActualQty,@Plant,@Wm_No,@Zone_No,@DLOC,0,@SupplierNum,@PartCname,1,@RECEIVE_NO,GETDATE(),@Create_User,GETDATE())
		END
	END
	ELSE
	BEGIN
		--IF NOT EXISTS(SELECT * FROM [LES].[TM_WMM_TRAN_DETAILS] WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=1 AND TRAN_STATE=0)
		INSERT INTO LES.TT_WMM_RECEIVE_DETAIL (Receive_Id ,Actual_Box_Num ,Actual_Qty ,Create_Date,Part_Cname ,Part_No,MEASURING_UNIT_NO ,Plant,Wm_No,Zone_No,DLOC, Create_User,package)
		VALUES( @RECEIVE_ID,1,@ActualQty,GETDATE(),@PartCname,@PART_NO,@MEASURING_UNIT_NO,@Plant,@Wm_No,@Zone_No,@DLOC,@Create_User,@PACKAGE)
		INSERT INTO LES.TM_WMM_TRAN_DETAILS (BARCODE_DATA ,PART_NO,BOX_NUM ,Num ,Plant ,WM_NO ,ZONE_NO,Dloc,
											Tran_State ,Supplier_Num,Part_Cname,Tran_Type,Tran_No,Create_Date,Create_User,Tran_Date)
		VALUES(@BARCODE_DATA,@PART_NO,1,@ActualQty,@Plant,@Wm_No,@Zone_No,@DLOC,0,@SupplierNum,@PartCname,1,@RECEIVE_NO,GETDATE(),@Create_User,GETDATE())
	END
END TRY   
BEGIN CATCH   
IF @@trancount > 0   
ROLLBACK TRAN   
END CATCH  
if @@trancount > 0   
COMMIT TRAN  
END