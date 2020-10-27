
-- =============================================
-- Author:		luchao
-- Create date: 2011-10-12
-- Description:	删除一个工位前所做的检测
-- 检查Inhouse拉动数据和Inhouse本地物流标准下是否有该工位
-- =============================================
create PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_LOCATION]
	@Plant varchar(5),
	@AssemblyLine varchar(10),
	@Location varchar(20),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 1.检查Inhouse拉动数据下是否有该工位
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and LOCATION=@Location
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '流水线' + @AssemblyLine + '工位' + @Location +'下面存在Inhouse拉动数据，不能删除。'
		RETURN
	END
	
 -- 2.检查Inhouse本地物流标准下是否有该工位
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD WITH(NOLOCK) WHERE PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and LOCATION=@Location
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '流水线' + @AssemblyLine + '工位' + @Location + '下面存在Inhouse本地物流标准信息，不能删除。'
		RETURN
	END
END