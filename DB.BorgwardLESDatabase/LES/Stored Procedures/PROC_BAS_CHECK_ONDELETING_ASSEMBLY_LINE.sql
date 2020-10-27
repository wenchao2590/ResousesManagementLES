
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-11
-- Description:	删除一个流水线前所做的检测
-- 检查工位和DCP扫描点下是否有该流水线
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_ASSEMBLY_LINE]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 1.检查工位下是否有该流水线
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_LOCATION WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '流水线' + @AssemblyLine + '下面存在工位信息，不能删除。'
		RETURN
	END
	
 -- 2.检查DCP扫描点下是否有该流水线
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_DCP_POINT WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '流水线' + @AssemblyLine + '下面存在DCP扫描点信息，不能删除。'
		RETURN
	END
 -- 3.检查用户-设置流水线权限里是否有该流水线信息(权限),需先删除关联
	SELECT @Count=COUNT(1) FROM LES.TS_SYS_SEARCH_FILTER WITH(NOLOCK) WHERE FILTER_CODE='Plant' and FILTER_VALUE=@Plant and CHILD_FILTER_CODE='Assembly_Line' and CHILD_FILTER_VALUE=@AssemblyLine
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '流水线' + @AssemblyLine + '下面存在用户-设置流水线的权限关联信息，不能删除。请先去删除权限关联。'
		RETURN
	END
END