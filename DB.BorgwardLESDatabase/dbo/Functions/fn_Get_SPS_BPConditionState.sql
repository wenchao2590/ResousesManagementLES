
-- =============================================
-- Author:		Andy Liu
-- Create date: Nov 11,2009
-- Description:	<Description, ,>
-- =============================================
create function [dbo].[fn_Get_SPS_BPConditionState]
(
	@part_spec_id int,
	@status int
)
returns int
as
begin	
	declare @returnValue int
	if exists
	(
		select top 1 *
		from dbo.TM_SPS_Part
		where part_spec_id = @part_spec_id
		and [status] = @status
	)
	begin
		set @returnValue = 1;
	end
	else
	begin
		set @returnValue = 0;
	end

	return @returnValue;
end