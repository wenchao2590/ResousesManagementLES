﻿
CREATE PROC  [LES].[PROC_WMM_NOTIFICATION_IMPORT_SUBMITBOX] 
	@NOTIFICATION_ID int,
	@PART_NO nvarchar(50),
	@BARCODE_DATA NVARCHAR(50),
	@ActualQty numeric(18,2),
	@Create_User NVARCHAR(50),
	@NUM numeric(18,2)
AS
BEGIN
	IF EXISTS(SELECT 1 FROM LES.TM_WMM_NOTIFICATION_DETAIL_IMPORT 
	WHERE NOTIFICATIONID=@NOTIFICATION_ID AND PART_NO=@PART_NO)
	BEGIN
		IF EXISTS(SELECT 1 FROM LES.TM_WMM_NOTIFICATION_DETAIL_SCAN 
		WHERE NOTIFICATIONID=@NOTIFICATION_ID AND BARCODE_DATA=@BARCODE_DATA)
		BEGIN
			UPDATE LES.TM_WMM_NOTIFICATION_DETAIL_SCAN
			SET ActualQty = @ActualQty
			WHERE NOTIFICATIONID=@NOTIFICATION_ID AND BARCODE_DATA=@BARCODE_DATA			
		END
		ELSE
		BEGIN
			INSERT INTO LES.TM_WMM_NOTIFICATION_DETAIL_SCAN
			(
				NOTIFICATIONID,
				BARCODE_DATA,
				PART_NO,
				ActualQty
			)
			VALUES
			(
				@NOTIFICATION_ID,@BARCODE_DATA,@PART_NO,@ActualQty
			)
		END;
		UPDATE A
		SET A.REAL_NUM=(SELECT SUM(B.ActualQty) FROM LES.TM_WMM_NOTIFICATION_DETAIL_SCAN B 
		WHERE B.NOTIFICATIONID = A.NOTIFICATIONID AND B.PART_NO = A.PART_NO),
		UPDATE_DATE=GETDATE(),UPDATE_USER=@Create_User   --, BARCODE_DATA = BARCODE_DATA + ',' + @BARCODE_DATA
		FROM LES.TM_WMM_NOTIFICATION_DETAIL_IMPORT A 
		WHERE A.NOTIFICATIONID=@NOTIFICATION_ID AND PART_NO=@PART_NO
	END
	ELSE
	BEGIN

	
		INSERT INTO LES.TM_WMM_NOTIFICATION_DETAIL_SCAN
		(
			NOTIFICATIONID,
			BARCODE_DATA,
			PART_NO,
			ActualQty
		)
		VALUES
		(
			@NOTIFICATION_ID,
			@BARCODE_DATA,
			@PART_NO,
			@ActualQty
		)


	DECLARE @PLANT NVARCHAR(5),@Wm_No NVARCHAR(10),@Zone_No NVARCHAR(20),@DLOC NVARCHAR(30),@PART_CNAME NVARCHAR(50),@TRAN_ID INT
	SELECT @TRAN_ID=TRAN_ID,@PLANT=PLANT,@Wm_No=WM_NO,@Zone_No=ZONE_NO,@DLOC=DLOC,@PART_CNAME=PART_CNAME FROM LES.TT_WMM_NOTIFICATION_DETAIL 
	WHERE  NOTIFICATIONID=@NOTIFICATION_ID AND PART_NO=@PART_NO
		
		INSERT INTO LES.TM_WMM_NOTIFICATION_DETAIL_IMPORT 
		(TRAN_ID,Create_Date,Create_User,ULOC,Num,PART_CNAME,PART_NO,REAL_NUM,Notificationid,PLANT,WM_NO,ZONE_NO,BARCODE_DATA) 
		VALUES
		(@TRAN_ID,GETDATE(),@Create_User,@DLOC,@NUM,@PART_CNAME,@PART_NO,@ActualQty,
		@NOTIFICATION_ID,@PLANT,@Wm_No,@Zone_No,@BARCODE_DATA)
	END
END