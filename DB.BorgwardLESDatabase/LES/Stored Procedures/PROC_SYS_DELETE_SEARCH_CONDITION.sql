CREATE PROCEDURE [LES].[PROC_SYS_DELETE_SEARCH_CONDITION]
	@modelName nvarchar(32),---检索模型名称
	@controlId nvarchar(32)--属性名
AS
BEGIN
	declare @displayOrder int---排序
	declare @modelId int---检索条件模型ID
	select @modelId = MODEL_ID from [LES].[TS_SYS_SEARCH_MODEL] with(nolock)
	where MODEL_NAME = @modelName
	IF(@modelId is not NULL)
	BEGIN 
		IF((select COUNT(1) from [LES].[TS_SYS_SEARCH_MODEL_CONDITION] with(nolock)
		where MODEL_ID = @modelId and CONTROL_ID = @controlId) > 0)
		BEGIN
			delete from [LES].[TS_SYS_SEARCH_MODEL_CONDITION] with(rowlock)
			where MODEL_ID = @modelId and CONTROL_ID = @controlId
			print 'Success'
		END
		ELSE
		BEGIN
			print 'Not Exist'
		END
	END
	ELSE
	BEGIN
		print 'Fail'
	END
END