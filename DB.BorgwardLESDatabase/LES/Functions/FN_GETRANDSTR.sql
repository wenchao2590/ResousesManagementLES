--创建函数
CREATE function [LES].[FN_GETRANDSTR](@n int)
returns varchar(max)
as
begin
	
	IF (@n = 19)
	BEGIN
		SET @n=32
	END

    declare @i int
    set @i=ceiling(@n/32.00)
    declare @j int
    set @j=0
    declare @k varchar(max)
    set @k=''
    while @j<@i
    begin
    select @k=@k+replace(cast(MacoId as varchar(36)),'-','') from LES.V_NEWID
    set @j=@j+1
    end
    set @k=substring(@k,1,@n)
return @k
end