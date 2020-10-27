
/**********************************************************************************
* Purpose		: 检查某个以指定符号分隔的字符串中是否至少有一条数据被另一指定字符串包含  
* Author		: Yinxuefeng
* CreateDate	: 2013-01-23
***********************************************************************************/
CREATE FUNCTION [LES].[Func_CheckAnyIsInField]
(
	 @FieldValue	nvarchar(2000),
     @SearchValue	nvarchar(2000),
     @Separator	nvarchar(5) = ','
)
RETURNS	INT
AS
BEGIN
	IF (LEN(ISNULL(@SearchValue,'')))=0
	  RETURN 0
	DECLARE @TempSplit TABLE
	(
		RID		INT,
		FIELD	NVARCHAR(20)
	)
	
	INSERT	INTO @TempSplit
	SELECT	*	
	FROM	LES.FUN_SYS_SPLIT(@FieldValue, @Separator)
	
	DECLARE @i INT, @maxRID INT
	DECLARE	@field	NVARCHAR(20)
	SELECT	@i = MIN(RID), @maxRID = MAX(RID) FROM @TempSplit
	
	WHILE @i <= @maxRID
	BEGIN
		SELECT TOP 1 @field = FIELD
		FROM	@TempSplit
		WHERE	RID = @i
		ORDER	BY RID
		
		IF CHARINDEX(@field, @SearchValue) >0
			RETURN 1
		
		SET @i = @i + 1
	END
	
	RETURN 0
END