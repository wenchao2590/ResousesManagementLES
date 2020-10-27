
-- =============================================
-- Author:		luchao
-- Create date: 2011-11-26
-- Description:	删除一个PCS_DCP扫描点前所做的检测
-- 检查给PCS_DCP扫描点设置的关联工位列表中是否存在该PCS_DCP扫描点的关联
-- =============================================
CREATE PROCEDURE [LES].[PROC_PCS_CHECK_ONDELETING_REGION]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@RegionIdentity int,
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	DECLARE @RegionName nvarchar(50) 
	SELECT @RegionName = REGION_NAME FROM LES.TM_PCS_REGION WHERE REGION_IDENTITY=@RegionIdentity and PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine 
 
 -- 检查给PCS_DCP扫描点设置的关联工位列表中是否存在该PCS_DCP扫描点的关联
	SELECT @Count=COUNT(1) FROM LES.TR_PCS_REGION_LOCATION WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and REGION_NAME=@RegionName
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + '  PCS_DCP扫描点' + @RegionName + '(序号:' + @RegionIdentity + ')下面存在设置的关联工位信息，不能删除。'
		RETURN
	END
END