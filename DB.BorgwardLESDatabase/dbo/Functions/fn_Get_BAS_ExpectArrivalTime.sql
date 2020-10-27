
-- =============================================
-- Author:		fengyoujun
-- Create date: 10/10/2008
-- Description:	<Get Expect Arrival Time>
-- =============================================
CREATE function   [dbo].[fn_Get_BAS_ExpectArrivalTime]
(
	@plant varchar(2),
	@workshop varchar(2),
	@route varchar(3),
	@supplier varchar(2),
	@part_type int,
	@current_time datetime
)   
  returns   datetime
as
begin
	declare @expect_time datetime
	declare @online_time int

--	-- temp set value
--	set @plant = 'SS'
--	set @workshop = 'GA'
--	set @route = 'G01'
--	set @supplier = 'LC'
--	set @part_type = 2  --kanban
--	set @current_time = getdate()

	if (@part_type = 1) -- andon
		begin			
				set @expect_time =
				(
					select TOP 1 F.window_time 
					FROM tt_supplier_sendtime F 
					WHERE F.plant = @plant AND F.workshop = @workshop 
						AND F.route = @route AND F.supplier = @supplier 
						AND F.send_time > @current_time AND part_type=@part_type
					ORDER BY F.window_time
				)
		end
	else if (@part_type = 2) -- kanban
		begin
			set @online_time =
			(
				select top 1 online_time
				from TM_SUPPLIER_OnlineTime
				where plant=@plant and workshop=@workshop
					and supplier=@supplier and [route]= @route
					and part_type=@part_type
			)
			if (isnull(@online_time,0)<= 0)
				select @online_time=parameter_value from TS_BAS_SysConfig where parameter_name='OnlineTime';
			if (isnull(@online_time,0)<= 0)
				set @online_time=45;
			set @expect_time = dbo.fn_Get_BAS_Work_Datetime(@plant, @workshop, @current_time, @online_time)
		end
	
	return @expect_time;
end;