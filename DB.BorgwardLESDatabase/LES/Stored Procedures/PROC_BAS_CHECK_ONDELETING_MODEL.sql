
-- =============================================
-- Author:		luchao
-- Create date: 2011-11-26
-- Description:	删除一个车型前所做的检测
-- 检查Inhouse和Inbound拉动数据下是否有该车型关联
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_MODEL]
	@ModelId int,
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	DECLARE @Model varchar(10)
	SELECT @Model=MODEL FROM LES.TM_BAS_MODEL WHERE MODEL_ID=@ModelId
	
 -- 1.检查Inhouse拉动数据下是否有该车型关联
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD WITH(NOLOCK) WHERE MODEL=@Model
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '车型' + @ModelId + '下面存在Inhouse拉动数据关联信息，不能删除。'
		RETURN
	END
	
 -- 2.检查Inbound拉动数据下是否有该车型关联
	SELECT @Count=COUNT(1) FROM LES.TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD WITH(NOLOCK) WHERE MODEL=@Model
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '车型' + @ModelId + '下面存在Inbound拉动数据关联信息，不能删除。'
		RETURN
	END
END