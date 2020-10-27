-- =============================================
-- Author:		Peng HOU
-- Create date: 2016-10-14
-- Description:	求2个整数的最小公倍数
-- =============================================
CREATE FUNCTION [LES].[Func_LCM]
(
	@num1 BIGINT,  
    @num2 BIGINT  
)
RETURNS BIGINT
AS
BEGIN
	IF(@num1 IS NULL OR @num2 IS NULL)
	BEGIN
		RETURN NULL
	END

    DECLARE @mod BIGINT;
	DECLARE @numBigger BIGINT = @num1
	DECLARE @numLesser BIGINT = @num2		--最大公约数

	IF ( @num1 < @num2 )
	BEGIN   
		SET @numBigger = @num2;
		SET @numLesser = @num1;
	END
	
	SET @mod = @numBigger % @numLesser;  
	WHILE (@mod > 0)
	BEGIN   
		SET @numBigger = @numLesser
		SET @numLesser = @mod
		SET @mod = @numBigger % @numLesser
	END

    RETURN @num1 * @num2 / @numLesser;   
END