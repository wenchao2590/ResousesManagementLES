

-- =============================================
-- Author:		fengyoujun
-- Create date: 10/10/2008
-- Description:	<Get Expect Arrival Time>
-- =============================================
CREATE function   [LES].[Func_Get_PCS_Expect_Arrival_Time]
(
	@plant varchar(5),
	@assemblyLine nvarchar(20),
	@route varchar(10),
	@supplier varchar(10),
	@current_time datetime
)   
  returns   datetime
as
begin
	declare @expect_time datetime
	declare @online_time int
	
	set @online_time =
	(
		select top 1 [ONLINE_TIME]
		from LES.TM_PCS_ROUTE_BOX_PARTS
		where plant=@plant and [ASSEMBLY_LINE]=@assemblyLine
		and [SUPPLIER_NUM]=@supplier and [BOX_PARTS]= @route
		
	)
	if(@online_time=0)
	return @current_time
	
	if (@online_time is null)
		select @online_time=parameter_value from [LES].[TS_SYS_CONFIG] where parameter_name='PCSDefaultOnlineTime';
	if (isnull(@online_time,0)<= 0)
		set @online_time=45;
	    set @expect_time = [LES].[Func_Get_BAS_Work_Datetime](@plant, @assemblyLine, @current_time, @online_time)
	if(	@expect_time is null)
	return @current_time
	return @expect_time;
end;