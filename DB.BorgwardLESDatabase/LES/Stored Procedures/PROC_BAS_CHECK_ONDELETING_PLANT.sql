
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-11
-- Description:	删除一个工厂前所做的检测
-- 检查流水线下是否有该工厂
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_PLANT]
	@Plant varchar(5),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 检查流水线下是否有该工厂
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_ASSEMBLY_LINE WITH(NOLOCK) WHERE plant=@Plant 
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '下面存在流水线信息，不能删除。'
		RETURN
	END
END