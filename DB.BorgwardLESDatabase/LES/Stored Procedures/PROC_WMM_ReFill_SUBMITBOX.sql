-- =============================================
-- Author:		sunhaiyang
-- Create date: <Create Date,,>
-- Description:	翻包补货
-- =============================================
CREATE PROCEDURE [LES].[PROC_WMM_ReFill_SUBMITBOX] 
	@REPACKAGE_NO nvarchar(50),
	@PART_NO nvarchar(50),
	@BARCODE_DATA NVARCHAR(50),
	@ActualQty int,
	@Create_User NVARCHAR(50),
	@TargetWm  NVARCHAR(50),
	@TargetZone NVARCHAR(50)
AS
BEGIN
BEGIN TRAN
BEGIN TRY
	set @ActualQty = isnull(@ActualQty,0)
	  DECLARE @TRAN_ID int
			SELECT top 1 @TRAN_ID=TRAN_ID FROM LES.TT_WMM_REPACKAGE_DETAIL with (nolock) WHERE REPACKAGE_NO=@REPACKAGE_NO AND PART_NO=@PART_NO

	    IF EXISTS(SELECT 1 FROM [LES].[TM_WMM_TRAN_DETAILS]  with (nolock) WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=6 AND TRAN_STATE=0)
		BEGIN
			DECLARE @LASTNUM DECIMAL(18,2)
			SELECT @LASTNUM=NUM FROM [LES].[TM_WMM_TRAN_DETAILS]  with (nolock) WHERE BARCODE_DATA=@BARCODE_DATA  AND TRAN_TYPE=6 AND TRAN_STATE=0
			
			UPDATE LES.TT_WMM_REPACKAGE_DETAIL with (rowlock) SET ACTUAL_QTY=ISNULL(ACTUAL_QTY,0)-@LASTNUM+@ActualQty
			WHERE TRAN_ID=@TRAN_ID 
			--WHERE REPACKAGE_NO=@REPACKAGE_NO AND PART_NO=@PART_NO
			
			--DECLARE @OUTPUT_NO NVARCHAR(50)
			--SELECT @OUTPUT_NO=OUTPUT_NO FROM LES.TT_WMM_OUTPUT with (nolock) WHERE OUTPUT_ID=@OUTPUT_ID

			UPDATE LES.TM_WMM_TRAN_DETAILS with (rowlock) SET NUM=@ActualQty
			WHERE TRAN_NO=@REPACKAGE_NO AND BARCODE_DATA=@BARCODE_DATA

				insert into [LES].[ReFillTrace](REPACKAGE_NO,REPACKAGE_ID,PART_NO,BARCODE_DATA,ActualQty,Create_User,Create_Time,Operation) 
				values (@REPACKAGE_NO,0,@PART_NO,@BARCODE_DATA,@ActualQty,@Create_User,getdate(),'UpdateNum')

		END
		ELSE
		BEGIN

			UPDATE LES.TT_WMM_REPACKAGE_DETAIL with (rowlock) SET 
			ACTUAL_QTY=ISNULL(ACTUAL_QTY,0)+@ActualQty
			WHERE TRAN_ID=@TRAN_ID
            --WHERE REPACKAGE_NO=@REPACKAGE_NO AND PART_NO=@PART_NO

			--翻包前数据
			INSERT INTO LES.TM_WMM_TRAN_DETAILS (
				BARCODE_DATA ,
				PART_NO,
				BOX_NUM ,
				Num ,
				Plant ,
				WM_NO ,
				ZONE_NO,
				PACKAGE_MODEL,
				PACKAGE,
				Tran_State ,
				Part_Cname,
				Tran_Type,
				Tran_No,
				Create_Date,
				Create_User,
				Tran_Date,
				TARGET_WM,
				TARGET_ZONE)
				select
				@BARCODE_DATA,--BARCODE_DATA
				@PART_NO,--PART_NO
				1,--BOX_NUM
				@ActualQty,--Num
				A.PLANT,--Plant
				A.WM_NO,--WM_NO
				A.ZONE_NO,--ZONE_NO
				B.PACKAGE_MODEL,--PACKAGE_MODEL
				B.PACKAGE,--PACKAGE
				0,--Tran_State
				B.PART_CNAME,--Part_Cname
				5,--Tran_Type
				A.REPACKAGE_NO,--Tran_No
				GETDATE(),--Create_Date
				@Create_User,--Create_User
				GETDATE(),--Tran_Date
				@TargetWm,
				@TargetZone
				from les.TT_WMM_REPACKAGE_HEAD A with (nolock)
					inner join les.TT_WMM_REPACKAGE_DETAIL B with (nolock) on A.REPACKAGE_ID=B.REPACKAGE_ID 
			    where A.REPACKAGE_NO=@REPACKAGE_NO AND B.PART_NO=@PART_NO

				--翻包后数据
				INSERT INTO LES.TM_WMM_TRAN_DETAILS (
				BARCODE_DATA ,
				PART_NO,
				BOX_NUM ,
				Num ,
				Plant ,
				WM_NO ,
				ZONE_NO,
				PACKAGE_MODEL,
				PACKAGE,
				Tran_State ,
				Part_Cname,
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
				@TargetWm,--WM_NO
				@TargetZone,--ZONE_NO
				B.INBOUND_PACKAGE_MODEL,--PACKAGE_MODEL
				B.INBOUND_PACKAGE,--PACKAGE
				0,--Tran_State
				B.PART_CNAME,--Part_Cname
				6,--Tran_Type
				A.REPACKAGE_NO,--Tran_No
				GETDATE(),--Create_Date
				@Create_User,--Create_User
				GETDATE()
				from les.TT_WMM_REPACKAGE_HEAD A with (nolock)
					inner join les.TT_WMM_REPACKAGE_DETAIL B with (nolock) on A.REPACKAGE_ID=B.REPACKAGE_ID 
			    where A.REPACKAGE_NO=@REPACKAGE_NO AND B.PART_NO=@PART_NO

					insert into [LES].[ReFillTrace](REPACKAGE_NO,REPACKAGE_ID,PART_NO,BARCODE_DATA,ActualQty,Create_User,Create_Time,Operation) 
				values (@REPACKAGE_NO,0,@PART_NO,@BARCODE_DATA,@ActualQty,@Create_User,getdate(),'Add')
		END
END TRY   
BEGIN CATCH   
IF @@trancount > 0   
begin 
ROLLBACK TRAN  
	insert into [LES].[ReFillTrace](REPACKAGE_NO,REPACKAGE_ID,PART_NO,BARCODE_DATA,ActualQty,Create_User,Create_Time,Operation) 
				values (@REPACKAGE_NO,0,@PART_NO,@BARCODE_DATA,@ActualQty,@Create_User,getdate(),'RollBack')
	exec [LES].[PROC_WMM_ReFill_SUBMITBOX] @REPACKAGE_NO,@PART_NO,@BARCODE_DATA,@ActualQty,@Create_User,@TargetWm,@TargetZone
end 
END CATCH  
if @@trancount > 0   
COMMIT TRAN  
END