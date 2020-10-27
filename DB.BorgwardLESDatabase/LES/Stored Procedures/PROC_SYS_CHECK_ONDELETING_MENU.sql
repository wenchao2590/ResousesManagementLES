
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-11
-- Description:	删除一个菜单前所做的检测
-- 检查设置动作-菜单关联表及设置角色-菜单关联权限表中是否有该菜单关联数据
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_CHECK_ONDELETING_MENU]
	@MenuId int,
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	DECLARE @MenuName nvarchar(50)
	SELECT @MenuName=MENU_NAME FROM LES.TM_SYS_MENU WHERE MENU_ID=@MenuId
	
	-- 1.检查设置动作-菜单关联表中是否有该角色信息
	SELECT @Count=COUNT(1) FROM LES.TS_SYS_MENU_ACTION WITH(NOLOCK) WHERE MENU_ID=@MenuId 
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '菜单' + @MenuName + '(ID:' + cast(@MenuId as varchar(8)) + ')' + '下面存在动作-菜单关联数据，不能删除。'
		RETURN
	END
	
	-- 2.检查设置角色-菜单权限关联表中是否有该角色信息
	SELECT @Count=COUNT(1) FROM LES.TS_SYS_AUTH WITH(NOLOCK) WHERE RESOURCE_TYPE='menu' and RESOURCE_ID=@MenuId
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '菜单' + @MenuName + '(ID:' + cast(@MenuId as varchar(8)) + ')' + '下面存在设置的角色-菜单关联权限数据，不能删除。'
		RETURN
	END
END