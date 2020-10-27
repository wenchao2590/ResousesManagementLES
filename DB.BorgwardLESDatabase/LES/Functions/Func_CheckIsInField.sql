


CREATE FUNCTION [LES].[Func_CheckIsInField]
(
      @FieldValue	nvarchar(2000),
      @SearchValue	nvarchar(2000),
      @Separator	nvarchar(5) = ','
)
RETURNS INT
BEGIN
	DECLARE @Ret int
	SET @Ret = 0
	
	IF @SearchValue IS NULL OR @SearchValue = ''
	BEGIN
		SET @Ret = 0
	END
	ELSE
	BEGIN
	
		DECLARE @CurrentIndex int;
		DECLARE @NextIndex	int;
		DECLARE @PartText	nvarchar(100);
		
		SET	@CurrentIndex = 1;
		
		WHILE(@CurrentIndex <= len(@SearchValue))
        BEGIN
			SET @NextIndex = charindex(@Separator,@SearchValue,@CurrentIndex)
			
            IF(@NextIndex = 0 OR @NextIndex IS NULL)
				SET @NextIndex = len(@SearchValue) + 1
            
            SET @PartText = substring(@SearchValue, @CurrentIndex, @NextIndex - @CurrentIndex)
				
			IF charindex(@PartText, @FieldValue) > 0
			BEGIN
				SET @Ret = 1
				BREAK;
			END
				
			SET @CurrentIndex = @NextIndex + 1
		END    
	END
	
	return @Ret
END