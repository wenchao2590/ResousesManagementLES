
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-11
-- Description:	删除一个用户前所做的检测
-- 检查角色-用户关联表,设置流水线-用户关联表及设置供应商-用户关联表中是否有该用户关联数据
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_CHECK_ONDELETING_USER]
	@UserId int,
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	DECLARE @UserLoginName nvarchar(50)
	SELECT @UserLoginName=USER_LOGIN_NAME FROM LES.TS_SYS_USER WHERE USER_ID=@UserId
	
	-- 1.检查设置角色-用户关联表中是否有该用户信息
	SELECT @Count=COUNT(1) FROM LES.TR_SYS_USER_ROLE WITH(NOLOCK) WHERE USER_ID=@UserId
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '用户' + @UserLoginName + '(ID:' + cast(@UserId as varchar(8)) + ')' + '下面存在角色-用户关联数据，不能删除。'
		RETURN
	END
	
	-- 2.检查设置流水线-用户关联表中是否有该用户信息
	SELECT @Count=COUNT(1) FROM LES.TS_SYS_SEARCH_FILTER WITH(NOLOCK) WHERE USER_ID=@UserId and FILTER_CODE='Assembly_Line'
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '用户' + @UserLoginName + '(ID:' + cast(@UserId as varchar(8)) + ')' + '下面存在流水线-用户关联的权限数据，不能删除。'
		RETURN
	END
	
	-- 3.检查设置供应商-用户关联表中是否有该用户信息
	SELECT @Count=COUNT(1) FROM LES.TR_SYS_USER_SUPPLIER WITH(NOLOCK) WHERE USER_ID=@UserId
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '用户' + @UserLoginName + '(ID:' + cast(@UserId as varchar(8)) + ')' + '下面存在供应商-用户关联数据，不能删除。'
		RETURN
	END
END