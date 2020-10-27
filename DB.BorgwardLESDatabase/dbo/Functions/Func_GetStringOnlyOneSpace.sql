
-- =============================================
-- Author:		yinxuefeng
-- Create date: 2013-07-16
-- Description:	去除多个空格，只保留一个
-- =============================================
CREATE FUNCTION [dbo].[Func_GetStringOnlyOneSpace]
(
	@OriginalString NVARCHAR(max)
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @CleanString NVARCHAR(MAX)

	-- Add the T-SQL statements to compute the return value here
	SELECT @CleanString=REPLACE(
		REPLACE(  
                REPLACE(LTRIM(RTRIM(@OriginalString))  ,'  ',' '+CHAR(7))  --Changes 2 spaces to the OX model  
                ,CHAR(7)+' ','')       --Changes the XO model to nothing  
        ,CHAR(7),'')

	-- Return the result of the function
	RETURN @CleanString

END