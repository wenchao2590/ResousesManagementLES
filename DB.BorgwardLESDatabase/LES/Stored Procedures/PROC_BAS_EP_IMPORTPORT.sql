
-- =============================================
-- Author:		<袁世安,,>
-- Create date: <2014-12-09,,>
-- Description:	<更新发动机零件基础数据,,>
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_EP_IMPORTPORT]
	@SEQ_ID		INT,
	@BATCH_ID	VARCHAR(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @ResultCode	INT,
			@ResultMsg NVARCHAR(1024)

	BEGIN TRANSACTION
	BEGIN TRY
		-- 其它更新零件基础信息语句......

		-- 更新本次处理的零件号
		UPDATE [LES].[TM_BAS_EP_IMPORTPORT]
		SET UPDATE_DATE = GETDATE(), 
			UPDATE_USER = 'SAPTOLESEPBOMService'
		WHERE SEQ_ID = @SEQ_ID

		-- 清空临时表数据
		TRUNCATE TABLE [LES].[TE_EP_SAP_BOM]

		-- 提交事务
		COMMIT TRANSACTION
		
		-- 返回处理成功的信息
		SET @ResultCode = 0
		SET @ResultMsg = '完成发动机PBOM基础数据更新操作'
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION

		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] (TIME_STAMP, [APPLICATION], [METHOD], CLASS,  EXCEPTION_MESSAGE, ERROR_CODE)
		SELECT GETDATE(),'EP','[LES].[PROC_BAS_EP_IMPORTPORT]','PROCEDURE',ERROR_MESSAGE() + ' Error Line:' + CONVERT(NVARCHAR(10),ERROR_LINE()),ERROR_NUMBER()
		
		SET @ResultCode = ERROR_NUMBER()
		SET @ResultMsg = ERROR_MESSAGE() + 'Error Line:' + CONVERT(NVARCHAR(10),ERROR_LINE())
	END CATCH

	SELECT @ResultCode AS Code, @ResultMsg AS Msg
END