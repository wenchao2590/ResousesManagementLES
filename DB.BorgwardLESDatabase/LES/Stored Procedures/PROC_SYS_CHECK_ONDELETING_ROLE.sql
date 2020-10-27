
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-11
-- Description:	删除一个角色前所做的检测
-- 检查角色-用户关联表及设置角色-权限关联表中是否有该角色关联数据
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_CHECK_ONDELETING_ROLE]
	@RoleId int,
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	DECLARE @RoleName nvarchar(50)
	SELECT @RoleName=ROLE_NAME FROM LES.TS_SYS_ROLE WHERE ROLE_ID=@RoleId
	
	-- 1.检查设置角色-用户关联表中是否有该角色信息
	SELECT @Count=COUNT(1) FROM LES.TR_SYS_USER_ROLE WITH(NOLOCK) WHERE ROLE_ID=@RoleId
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '角色' + @RoleName + '(ID:' + cast(@RoleId as varchar(8)) + ')' + '下面存在角色-用户关联数据，不能删除。'
		RETURN
	END
	
	-- 2.检查设置角色-权限关联表中是否有该角色信息
	SELECT @Count=COUNT(1) FROM LES.TS_SYS_AUTH WITH(NOLOCK) WHERE ROLE_ID=@RoleId
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '角色' + @RoleName + '(ID:' + cast(@RoleId as varchar(8)) + ')' + '下面存在设置的角色-权限关联的权限数据，不能删除。'
		RETURN
	END
END