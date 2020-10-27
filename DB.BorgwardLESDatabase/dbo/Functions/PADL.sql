
CREATE FUNCTION [dbo].[PADL](@str varchar(20),@length int)  
RETURNS varchar(20)
AS  
BEGIN 
  while len(@str)<@length set @str='0'+@str
  return @str
END