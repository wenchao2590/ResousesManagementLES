
-- =============================================
-- Author:		luchao
-- Create date: 2011-11-28
-- Description:	删除一个JIS零件类前所做的检测
-- 检查JIS成对料架表,JIS拉动单管理(主表)和Inhouse拉动数据中是否存在该JIS零件类
-- 2012/10/31 添加装配规则检查 BUG:5153 yinxuefeng
-- 2013/08/13 添加组合零件类检查 BUG:5452 yinxuefeng
-- =============================================
CREATE PROCEDURE [LES].[PROC_JIS_CHECK_ONDELETING_RACK]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@Rack varchar(20),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count INT
	DECLARE @TmpRackName NVARCHAR(20)
	SET @TmpRackName='' 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	-- 1.检查JIS成对料架表中是否存在该JIS零件类数据
	SELECT @Count=COUNT(1) FROM LES.TM_JIS_PAIRRACK WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and RACK=@Rack
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' JIS零件类' + @Rack + '下面存在JIS成对料架数据，不能删除。'
		RETURN
	END
	
	 -- 2.检查JIS拉动单管理(主表)中是否存在该JIS零件类数据
	SELECT @Count=COUNT(1) FROM LES.TT_JIS_RUNSHEET WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and RACK=@Rack
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' JIS零件类' + @Rack + '下面存在JIS拉动单数据，不能删除。'
		RETURN
	END
	
	-- 3.检查Inhouse拉动数据表中是否存在该JIS零件类数据
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine 
			and (IN_PLANT_LOGISTIC_PART_CLASS=@Rack or INHOUSE_PART_CLASS=@Rack)
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' JIS零件类' + @Rack + '下面存在Inhouse拉动数据，不能删除。'
		RETURN
	END
	
	-- 4.检查JIS装配规则中是否存在该JIS零件类数据
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_ASSEMBLY_RULE WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and RACK=@Rack
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' JIS零件类' + @Rack + '下面存在JIS装配规则数据 ，不能删除。'
		RETURN
	END
	
	-- 5.检查该零件类是否是组合零件类
	SELECT @Count=COUNT(1),@TmpRackName=MIN(ISNULL(RACK,'')) FROM LES.TM_JIS_RACK WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine AND LEN(ISNULL(PLANT_ZONE,''))>0 and PLANT_ZONE=@Rack
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' JIS零件类' + @Rack + '下面存在JIS零件类'+@TmpRackName+' ，不能删除。'
		RETURN
	END
END