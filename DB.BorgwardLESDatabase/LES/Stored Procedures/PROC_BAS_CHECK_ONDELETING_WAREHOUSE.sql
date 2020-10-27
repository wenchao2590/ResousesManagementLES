
-- =============================================
-- Author:		xiekeng
-- Create date: 2015-06-09
-- Description:	删除一个仓库前所做的检测
-- 检查该仓库是否存在存储区
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_WAREHOUSE]
	@Warehouse nvarchar(10),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage nvarchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 检查该仓库是否存在存储区
	SELECT @Count=COUNT(1) FROM LES.TM_WMM_ZONES WITH(NOLOCK) WHERE WM_NO=@Warehouse 
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '仓库' + @warehouse + '下面存在存储区信息，不能删除。'
		RETURN
	END
END