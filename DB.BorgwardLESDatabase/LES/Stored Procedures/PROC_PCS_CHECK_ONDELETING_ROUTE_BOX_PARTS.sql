
-- =============================================
-- Author:		luchao
-- Create date: 2011-11-28
-- Description:	删除一个PCS零件类前所做的检测
-- 检查PCS实时消耗表,PCS窗口时间表和Inhouse拉动数据中是否存在该PCS零件类
-- =============================================
CREATE PROCEDURE [LES].[PROC_PCS_CHECK_ONDELETING_ROUTE_BOX_PARTS]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@BoxParts varchar(10),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	-- 1.检查PCS实时消耗表中是否存在该PCS零件类数据
	SELECT @Count=COUNT(1) FROM LES.TI_PCS_MATERIAL_REQUESTS WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and BOX_PARTS=@BoxParts
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' PCS零件类' + @BoxParts + '下面存在PCS实时消耗数据，不能删除。'
		RETURN
	END
	
	 -- 2.检查PCS窗口时间表中是否存在该PCS零件类数据
	SELECT @Count=COUNT(1) FROM LES.TT_PCS_DELIVERY_SCHEDULE WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and BOX_PARTS=@BoxParts
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' PCS零件类' + @BoxParts + '下面存在PCS窗口时间数据，不能删除。'
		RETURN
	END
	
	-- 3.检查Inhouse拉动数据表中是否存在该PCS零件类数据
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and IN_PLANT_LOGISTIC_PART_CLASS=@BoxParts
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' PCS零件类' + @BoxParts + '下面存在Inhouse拉动数据，不能删除。'
		RETURN
	END
END