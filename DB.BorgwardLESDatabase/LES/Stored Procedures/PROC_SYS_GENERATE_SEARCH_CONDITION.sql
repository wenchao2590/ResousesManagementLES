CREATE PROCEDURE [LES].[PROC_SYS_GENERATE_SEARCH_CONDITION]
	@modelName nvarchar(32),---检索模型名称
	@controlId nvarchar(32),--属性名
	@controlType nvarchar(32),--控件类型 Text 文本，CodeList 系统代码 ，DateTimeRange 时间范围，LinkageDropDownList 下拉
	@labelText nvarchar(32),--显示信息
	@columnName nvarchar(64),--字段名
	@columnType nvarchar(32),--程序属性类型 string datetime...
	@datasearchType nvarchar(32),--匹配类型 NULL allmatch...
	@codeName nvarchar(32),--CodeList控件类型时对应的系统代码
	@extend1 nvarchar(512),--LinkageDropDownList配置1
	@extend2 nvarchar(512),--LinkageDropDownList配置2
	@extend3 nvarchar(512),--LinkageDropDownList配置3
	@extend4 nvarchar(512),--LinkageDropDownList配置4
	@extend5 nvarchar(512),--LinkageDropDownList配置5
	@extend6 nvarchar(512),--LinkageDropDownList配置6
	@extend7 nvarchar(512),--LinkageDropDownList配置7
	@extend8 nvarchar(512),--LinkageDropDownList配置8
	@extend9 nvarchar(512),--LinkageDropDownList配置9
	@extend10 nvarchar(512)--LinkageDropDownList配置10
AS
BEGIN
	declare @displayOrder int---排序
	declare @modelId int---检索条件模型ID
	select @modelId = MODEL_ID from [LES].[TS_SYS_SEARCH_MODEL] with(nolock)
	where MODEL_NAME = @modelName
	IF(@modelId is not NULL)
	BEGIN 
		IF((select COUNT(1) from [LES].[TS_SYS_SEARCH_MODEL_CONDITION] with(nolock)
		where MODEL_ID = @modelId and CONTROL_ID = @controlId) = 0)
		BEGIN
			select @displayOrder = DISPLAY_ORDER from [LES].[TS_SYS_SEARCH_MODEL_CONDITION] with(nolock)
			where MODEL_ID = @modelId
			set @displayOrder = @displayOrder +10
			INSERT INTO [LES].[TS_SYS_SEARCH_MODEL_CONDITION]
					   ([MODEL_ID]
					   ,[CONTROL_ID]
					   ,[CONTROL_TYPE]
					   ,[DEFAULT_VALUE]
					   ,[DISPLAY_ORDER]
					   ,[MAX_LENGTH]
					   ,[LABEL_TEXT]
					   ,[TABLE_NAME]
					   ,[COLUMN_NAME]
					   ,[COLUMN_TYPE]
					   ,[REGEX_EXPRESSION]
					   ,[DATASEARCH_TYPE]
					   ,[CODE_NAME]
					   ,[EXTEND_FIELD_1]
					   ,[EXTEND_FIELD_2]
					   ,[EXTEND_FIELD_3]
					   ,[EXTEND_FIELD_4]
					   ,[EXTEND_FIELD_5]
					   ,[EXTEND_FIELD_6]
					   ,[EXTEND_FIELD_7]
					   ,[EXTEND_FIELD_8]
					   ,[EXTEND_FIELD_9]
					   ,[EXTEND_FIELD_10]
					   ,[CREATE_USER]
					   ,[CREATE_DATE]
					   ,[UPDATE_USER]
					   ,[UPDATE_DATE])
				 VALUES
					   (@modelId
					   ,@controlId
					   ,@controlType
					   ,NULL--DEFAULT_VALUE
					   ,@displayOrder
					   ,NULL--MAX_LENGTH
					   ,@labelText
					   ,NULL--TABLE_NAME
					   ,@columnName
					   ,@columnType
					   ,NULL--REGEX_EXPRESSION
					   ,@datasearchType
					   ,@codeName
					   ,@extend1
					   ,@extend2
					   ,@extend3
					   ,@extend4
					   ,@extend5
					   ,@extend6
					   ,@extend7
					   ,@extend8
					   ,@extend9
					   ,@extend10
					   ,'sqlscript'
					   ,GETDATE()
					   ,NULL
					   ,NULL)
			print 'Success'
		END
		ELSE
		BEGIN
			print 'Existed'
		END
	END
	ELSE
	BEGIN
		print 'Fail'
	END
END