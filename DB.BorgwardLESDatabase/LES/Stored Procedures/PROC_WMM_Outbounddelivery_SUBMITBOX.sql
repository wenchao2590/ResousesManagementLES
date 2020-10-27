




CREATE PROC [LES].[PROC_WMM_Outbounddelivery_SUBMITBOX] 
	@Outbounddelivery_ID int,
	@PART_NO nvarchar(50),
	@BARCODE_DATA NVARCHAR(50),
	@ActualQty int,
	@Create_User NVARCHAR(50)
AS
BEGIN
BEGIN TRAN
BEGIN TRY
	set @ActualQty = isnull(@ActualQty,0)
	IF EXISTS(SELECT 1 FROM [LES].[TM_WMM_TRAN_DETAILS] WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=7 AND TRAN_STATE=0)
	BEGIN
		DECLARE @LASTNUM DECIMAL(18,2)
		SELECT @LASTNUM=ISNULL(NUM,0) FROM [LES].[TM_WMM_TRAN_DETAILS] WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=7 AND TRAN_STATE=0
		
		UPDATE LES.TT_WMM_Outbounddelivery_DETAIL 
		SET CURRENT_OUTBOUND_PACKAGE_QTY =ISNULL(CURRENT_OUTBOUND_PACKAGE_QTY,0)-@LASTNUM+@ActualQty
		WHERE Outbounddelivery_ID=@Outbounddelivery_ID AND PART_NO=@PART_NO
		
		DECLARE @Outbounddelivery_NO NVARCHAR(50)
		SELECT @Outbounddelivery_NO=Outbounddelivery_NO FROM LES.TT_WMM_Outbounddelivery WHERE Outbounddelivery_ID=@Outbounddelivery_ID

		UPDATE LES.TM_WMM_TRAN_DETAILS SET NUM=@ActualQty WHERE TRAN_NO=@Outbounddelivery_NO AND BARCODE_DATA=@BARCODE_DATA
	END
	ELSE
	BEGIN
		UPDATE LES.TT_WMM_Outbounddelivery_DETAIL 
		SET 
		CURRENT_OUTBOUND_PACKAGE_QTY =ISNULL(CURRENT_OUTBOUND_PACKAGE_QTY, 0)+@ActualQty,
		CURRENT_OUTBOUND_PACKAGE =ISNULL(CURRENT_OUTBOUND_PACKAGE, 0)+1 
		WHERE Outbounddelivery_ID=@Outbounddelivery_ID AND PART_NO=@PART_NO
		
		INSERT INTO LES.TM_WMM_TRAN_DETAILS (
				BARCODE_DATA ,
				PART_NO,
				BOX_NUM ,
				Num ,
				Plant ,
				WM_NO ,
				ZONE_NO,
				Dloc,
				PACKAGE_MODEL,
				PACKAGE,
				Tran_State ,
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
				B.BOX_PARTS,--BOX_PARTS
				B.PART_CNAME,--Part_Cname
				B.MEASURING_UNIT_NO,--MEASURING_UNIT_NO
				7,--Tran_Type
				A.Outbounddelivery_NO,--Tran_No
				GETDATE(),--Create_Date
				@Create_User,--Create_User
				GETDATE()--Tran_Date
				from les.TT_WMM_Outbounddelivery A
					inner join les.TT_WMM_Outbounddelivery_DETAIL B on A.Outbounddelivery_ID=B.Outbounddelivery_ID AND B.PART_NO=@PART_NO
					where A.Outbounddelivery_ID=@Outbounddelivery_ID
	END
END TRY   
BEGIN CATCH   
IF @@trancount > 0   
ROLLBACK TRAN   
END CATCH  
if @@trancount > 0   
COMMIT TRAN  
END