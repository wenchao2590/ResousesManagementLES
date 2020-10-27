
-- =============================================
-- Author:		luchao
-- Create date: 2011-12-12
-- Description:	删除一个工段前所做的检测
-- 检查工位下是否有该工段
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_WORKSHOP_SECTION]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@WorkshopSection varchar(20),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 检查工位下是否有该工段
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_LOCATION WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and REGION=@WorkshopSection
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '流水线' + @AssemblyLine + '工段' + @WorkshopSection +'下面存在工位数据，不能删除。'
		RETURN
	END
END