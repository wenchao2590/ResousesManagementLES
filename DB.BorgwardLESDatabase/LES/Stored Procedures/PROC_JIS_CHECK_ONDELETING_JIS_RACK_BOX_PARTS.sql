
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-11
-- Description:	删除一个JIS零件大类前所做的检测
-- 检查JIS零件类下是否有该JIS零件大类
-- =============================================
CREATE PROCEDURE [LES].[PROC_JIS_CHECK_ONDELETING_JIS_RACK_BOX_PARTS]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@JisRackBoxParts varchar(20),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 检查JIS零件类下是否有该JIS零件大类
	SELECT @Count=COUNT(1) FROM LES.TM_JIS_RACK WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and BOX_PARTS=@JisRackBoxParts
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '流水线' + @AssemblyLine + ' JIS零件大类' + @JisRackBoxParts + '下面存在JIS零件类关联数据，不能删除。'
		RETURN
	END
END