
create function [dbo].[Func_GetRDCWMNO] 
(	@runsheetno varchar(50),
	@part_type int,
	@pageindex varchar(10),
	@publishtimestr datetime,
	@simpleplantname varchar(20)
)
returns varchar(50)
AS
begin
	declare @wmno varchar(20)
	set @wmno = '';
	set @runsheetno = ltrim(rtrim(@runsheetno));
	set @pageindex = ltrim(rtrim(@pageindex));
	set @simpleplantname = ltrim(rtrim(@simpleplantname));

	if @publishtimestr is not null and @publishtimestr <> ''
	begin	
		declare @date varchar(100),@parttype varchar(50)
		set @date = 'A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,1,2,3,4,5';
		set @parttype = 'A,D,J';

		declare @I int
		declare @year varchar(2)
		declare @month varchar(2)
		declare @day varchar(2)
		declare @ptype varchar(2)
		declare @wmSerail varchar(10)
		declare @datetime varchar(10)
		set @datetime = convert(varchar(10),@publishtimestr,120)

		--年
		set @I = convert(int,substring(@datetime,4,1))
		select top 1 @year = item from (select top(@I) item from split(@date,',') order by item) a order by item desc

		--月
		set @I = convert(int,substring(@datetime,6,2))
		select top 1 @month = item from (select top(@I) item from split(@date,',') order by item) a order by item desc

		--日
		set @I = convert(int,substring(@datetime,9,2))
		select top 1 @day = item from (select top(@I) item from split(@date,',') order by item) a order by item desc
		
		--PartType
		select top 1 @ptype = item from (select top(@part_type) item from split(@parttype,',') order by item) a order by item desc

		set @wmSerail = substring(@runsheetno, len(@runsheetno) - 3, 4);

		set @wmno = @year + @month + @day + @ptype + @wmSerail + substring(@pageindex, len(@pageindex), 1) + substring(@simpleplantname, len(@simpleplantname), 1)
	end
	return @wmno
end