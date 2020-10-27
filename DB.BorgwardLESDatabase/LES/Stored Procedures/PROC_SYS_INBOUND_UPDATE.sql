-- =============================================
-- Author:		XINPENGZHANG
-- Create date: 2017-10-12
-- Description:	UPDATE INBOUND;INSERT INBOUND_DETAIL
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_INBOUND_UPDATE]
	@FID uniqueidentifier,
	@EXECUTERESULT int,
	@MODIFYUSER nvarchar(32)
AS
BEGIN
	IF @EXECUTERESULT IN (1,5)
	BEGIN
		UPDATE LES.TI_SYS_INBOUND SET EXECUTE_RESULT = @EXECUTERESULT,EXECUTE_START_TIME=GETDATE(),MODIFY_USER = @MODIFYUSER,MODIFY_DATE = GETDATE() WHERE FID = @FID
	END
	ELSE
	BEGIN
		UPDATE LES.TI_SYS_INBOUND SET EXECUTE_RESULT = @EXECUTERESULT,EXECUTE_END_TIME=GETDATE(),MODIFY_USER = @MODIFYUSER,MODIFY_DATE = GETDATE() WHERE FID = @FID
	END

	INSERT INTO [LES].[TH_SYS_INBOUND_DETAIL]
           ([FID]
           ,[INBOUND_FID]
           ,[EXECUTE_RESULT]
           ,[EXECUTE_START_TIME]
           ,[EXECUTE_END_TIME]
           ,[SOURCE_XML]
           ,[ERROR_CODE]
           ,[ERROR_DESC]
           ,[VALID_FLAG]
           ,[CREATE_USER]
           ,[CREATE_DATE])
     VALUES
           (@FID
           ,NULL
           ,@EXECUTERESULT
           ,GETDATE()
           ,GETDATE()
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,NULL
           ,GETDATE()) 
END