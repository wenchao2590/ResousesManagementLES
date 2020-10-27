
Create Function [dbo].[Split](@Sql varchar(8000), @Splits varchar(10))  
	returns @temp Table (item varchar(100))  
As  
Begin  
	Declare @i Int;  
	Set @Sql = RTrim(LTrim(@Sql));
	Set @i = CharIndex(@Splits,@Sql);
	While @i >= 1  
	Begin
		Insert @temp Values(Left(@Sql,@i-1));
		Set @Sql = SubString(@Sql,@i+1,Len(@Sql)-@i);
		Set @i = CharIndex(@Splits,@Sql);
	End

	If @Sql <> ''  
		Insert @temp Values (@Sql);
	Return  
End