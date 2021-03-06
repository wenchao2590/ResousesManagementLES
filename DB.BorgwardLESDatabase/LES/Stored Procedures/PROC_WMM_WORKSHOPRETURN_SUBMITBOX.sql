﻿
CREATE PROC  [LES].[PROC_WMM_WORKSHOPRETURN_SUBMITBOX] 
	@TRAN_NO NVARCHAR(50),
	@PART_NO nvarchar(50),
	@PART_CNAME NVARCHAR(100),
	@BARCODE_DATA NVARCHAR(50),
	@ActualQty DECIMAL(18,2),
	@Create_User NVARCHAR(50),
	@DLOC NVARCHAR(30),
	@TARGET_DLOC NVARCHAR(30)
AS
BEGIN
--BEGIN TRAN
--BEGIN TRY
	DECLARE @LASTNUM DECIMAL,@DETAILS_ID INT
	SELECT @DETAILS_ID= TRAN_ID,@LASTNUM=NUM FROM LES.TM_WMM_TRAN_DETAILS WHERE BARCODE_DATA=@BARCODE_DATA AND TRAN_TYPE=4 AND TRAN_STATE=0
	IF(@DETAILS_ID IS NOT NULL)
	BEGIN
		UPDATE LES.TT_WMM_TRAN_DETAIL SET NUM=NUM-@LASTNUM+@ActualQty 
		WHERE TRAN_NO=@TRAN_NO AND PART_NO=@PART_NO
		UPDATE LES.TM_WMM_TRAN_DETAILS SET NUM=@ActualQty WHERE TRAN_ID=@DETAILS_ID
	END
	ELSE
	BEGIN
		DECLARE @Plant NVARCHAR(5),@Wm_No NVARCHAR(10),@Zone_No NVARCHAR(20),
			@TARGET_WM NVARCHAR(10),@TARGET_Zone NVARCHAR(20),@TRAN_ID INT
		SELECT @Plant=PLANT,@Wm_No=S_WM_NO,@Zone_No=S_ZONE_NO,@TARGET_WM=O_WM_NO,@TARGET_Zone=O_ZONE_NO,@TRAN_ID=TRAN_ID
			FROM LES.TT_WMM_TRAN_HEAD WHERE TRAN_NO=@TRAN_NO 
		IF EXISTS(SELECT 1 FROM LES.TT_WMM_TRAN_DETAIL WHERE TRAN_NO=@TRAN_NO AND PART_NO=@PART_NO)
		BEGIN
			UPDATE LES.TT_WMM_TRAN_DETAIL SET NUM=NUM + @ActualQty ,BOX_NUM=BOX_NUM+1
			WHERE TRAN_NO=@TRAN_NO AND PART_NO=@PART_NO
			INSERT INTO LES.TM_WMM_TRAN_DETAILS 
			(Barcode_Data ,Part_No,Box_Num,Num,Plant,Wm_No,Zone_No,Dloc,TARGET_WM,TARGET_ZONE,TARGET_DLOC, Tran_State ,Tran_Type,Tran_No,CREATE_USER,CREATE_DATE)
			VALUES
			(@BARCODE_DATA,@PART_NO,1,@ActualQty,@Plant,@Wm_No,@Zone_No,@DLOC,@TARGET_WM,@TARGET_Zone,@TARGET_DLOC,0,4,@TRAN_NO,@Create_User,GETDATE())
		END
		ELSE
		BEGIN
			INSERT INTO LES.TT_WMM_TRAN_DETAIL (Box_Num,Create_Date,CREATE_USER,D_Dloc,Num,Part_Cname,Part_No,S_Dloc,TRAN_ID,Tran_No)
			VALUES(1,GETDATE(),@Create_User,@DLOC,@ActualQty,@PART_CNAME,@PART_NO,@TARGET_DLOC,@TRAN_ID,@TRAN_NO)
			
			INSERT INTO LES.TM_WMM_TRAN_DETAILS 
			(Barcode_Data ,Part_No,Box_Num,Num,Plant,Wm_No,Zone_No,Dloc,TARGET_WM,TARGET_ZONE,TARGET_DLOC, Tran_State ,Tran_Type,Tran_No,CREATE_USER,CREATE_DATE)
			VALUES
			(@BARCODE_DATA,@PART_NO,1,@ActualQty,@Plant,@Wm_No,@Zone_No,@DLOC,@TARGET_WM,@TARGET_Zone,@TARGET_DLOC,0,4,@TRAN_NO,@Create_User,GETDATE())
		END
	END
--END TRY   
--BEGIN CATCH   
--IF @@trancount > 0   
--ROLLBACK TRAN   
--END CATCH  
--if @@trancount > 0   
--COMMIT TRAN  
END