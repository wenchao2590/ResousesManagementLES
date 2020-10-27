-- =============================================
-- Author:		XINPENGZHANG
-- Create date: 2017-10-11
-- Description:	INSERT OUTBOUND
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_OUTBOUND_INSERT]
	@FID uniqueidentifier,
	@TRANSNO nvarchar(64),
	@METHORDNAME nvarchar(64),
	@KEYVALUE nvarchar(256),
	@CREATEUSER nvarchar(32)
AS
BEGIN
	
INSERT INTO [LES].[TI_SYS_OUTBOUND]
           ([FID]
           ,[TRANS_NO]
           ,[METHORD_NAME]
           ,[EXECUTE_RESULT]
           ,[EXECUTE_START_TIME]
           ,[EXECUTE_END_TIME]
           ,[KEY_VALUE]
           ,[SOURCE_XML]
           ,[ERROR_CODE]
           ,[ERROR_DESC]
           ,[VALID_FLAG]
           ,[CREATE_USER]
           ,[CREATE_DATE]
           ,[MODIFY_USER]
           ,[MODIFY_DATE])
     VALUES
           (@FID
           ,@TRANSNO
           ,@METHORDNAME
           ,0
           ,NULL
           ,NULL
           ,@KEYVALUE
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,@CREATEUSER
           ,GETDATE()
           ,NULL
           ,NULL)
END