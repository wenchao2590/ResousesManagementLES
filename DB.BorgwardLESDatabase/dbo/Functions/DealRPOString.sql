
Create FUNCTION [dbo].[DealRPOString] 
(
	@RPOString varchar(5000)
)
RETURNS varchar(5000)
AS
BEGIN	
	DECLARE @ResultVar varchar(5000)
	set @ResultVar='';
	while(len(@RPOString)>0)
	begin
	 
	select   @ResultVar=@ResultVar+left(@RPOString,3)+' '
	
	select @RPOString=right(@RPOString,len(@RPOString)-3)
	
	end
    
	-- Return the result of the function
	RETURN @ResultVar

END