
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-11
-- Description:	删除一个DCP扫描点前所做的检测
-- 检查PCS_DCP扫描点下是否有该DCP扫描点
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_DCP_POINT]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@DcpPoint varchar(15),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 检查PCS模块的DCP扫描点下是否有该DCP扫描点关联
	SELECT @Count=COUNT(1) FROM LES.TM_PCS_REGION WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and REGION_NAME=@DcpPoint
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '流水线' + @AssemblyLine + 'DCP扫描点' + @DcpPoint + '下面存在PCS模块的DCP扫描点关联信息，不能删除。'
		RETURN
	END
END