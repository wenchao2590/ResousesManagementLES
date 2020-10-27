
-- =============================================
-- Author:		yinxuefeng
-- Create date: 2012-06-08
-- Description:	删除用户及相关数据
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_USER_DELETEUSER]
	@USER_ID INT,
	@RESULT INT OUTPUT,						--删除结果，0,1表示成功，-1表示失败
	@RESULT_MESSAGE NVARCHAR(4000) OUTPUT 	--结果消息
AS
BEGIN
	SET @RESULT=0
	SET @RESULT_MESSAGE=''
	BEGIN TRY
		--用户角色   
		DELETE FROM [LES].[TR_SYS_USER_ROLE] WHERE [USER_ID] = @USER_ID
		--用户工厂/流水线/产品
		DELETE FROM [LES].[TS_SYS_SEARCH_FILTER] WHERE [USER_ID] = @USER_ID
		--用户供应商
		DELETE FROM [LES].[TR_SYS_USER_SUPPLIER] WHERE [USER_ID] = @USER_ID
		--用户JIS/PCS/TWD零件
		DELETE FROM [LES].[TR_SYS_USER_BOX_PARTS] WHERE [User_ID] = @USER_ID
		--用户
		DELETE FROM [LES].[TS_SYS_USER] WHERE [User_ID] = @USER_ID
		
		SET @RESULT=1
	END TRY   
	BEGIN CATCH  
			SET @RESULT=-1
			SET @RESULT_MESSAGE='出现错误的存储过程或触发器的名称: ['+ISNULL(ERROR_PROCEDURE(),'未知.]')+';导致错误的例程中的行号: [Line '+ CAST(ERROR_LINE() AS NVARCHAR(10)) +'];错误消息的完整文本: ['+ISNULL(ERROR_MESSAGE(),'未知错误.')+']'   
	END CATCH 
END