
-- =============================================
-- Author:		luchao
-- Create date: 2011-11-28
-- Description:	删除一个交货DOCK前所做的检测
-- 检查TWD零件类,Inhouse拉动数据及Inbound拉动数据中是否存在该交货DOCK
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_DOCK]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@Dock varchar(20),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	-- 1.检查TWD零件类表中是否存在该交货DOCK数据
	SELECT @Count=COUNT(1) FROM LES.TM_TWD_BOX_PARTS WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and DOCK=@Dock
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' 交货DOCK【' + @Dock + '】下面存在TWD零件类数据，不能删除。'
		RETURN
	END
	
	 -- 2.检查Inhouse拉动数据表中是否存在该交货DOCK数据
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine 
			and DOCK=@Dock
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' 交货DOCK【' + @Dock + '】下面存在Inhouse拉动数据，不能删除。'
		RETURN
	END
	
	-- 3.检查Inbound拉动数据表中是否存在该交货DOCK数据
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine 
			and DOCK=@Dock
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' 交货DOCK【' + @Dock + '】下面存在Inbound拉动数据，不能删除。'
		RETURN
	END
END