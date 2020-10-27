
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-11
-- Description:	删除一个菜单前所做的检测
-- 检查设置动作-菜单关联表中是否有该菜单关联数据
-- =============================================
CREATE PROCEDURE [LES].[PROC_SYS_CHECK_ONDELETING_ACTION]
	@ActionId int,
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	DECLARE @ActionNameCn nvarchar(100)
	DECLARE @ActionUrl nvarchar(100)
	SELECT @ActionNameCn=ACTION_NAME_CN,@ActionUrl=ACTION_URL FROM LES.TS_SYS_ACTION WHERE ACTION_ID=@ActionId
	
	-- 检查设置动作-菜单关联表中是否有该角色信息
	SELECT @Count=COUNT(1) FROM LES.TS_SYS_MENU_ACTION WITH(NOLOCK) WHERE ACTION_ID=@ActionId 
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '页面(' + @ActionUrl + ')中的动作<' + @ActionNameCn + ',(ID:' + cast(@ActionId as varchar(8)) + ')>' + '下面存在动作-菜单关联数据，不能删除。'
		RETURN
	END

END