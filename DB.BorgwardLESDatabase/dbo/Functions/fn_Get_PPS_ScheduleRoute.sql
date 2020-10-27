


 /************************************************************
*	Function Name: fn_Get_PPS_ScheduleRoute
*	Description:
*		To get the delivery Routes of a specified schedule
*	Modification:
*		2008-09-11		Hu jian		Inital Version
************************************************************/

CREATE function [dbo].[fn_Get_PPS_ScheduleRoute]
(
	@scheduleId int
)
returns varchar(2000)
AS
BEGIN
	declare @routes varchar(2000)
	declare @route varchar(3)

	SET @routes = ''
	SET @route = ''
	--SELECT @route = min([route]) from TR_PPS_ScheduleRoute
	while (@route is not null)
	BEGIN	
		SELECT @route = min([route]) from TR_PPS_ScheduleRoute
		where  [ScheduleId] = @scheduleId and route > @route
		
		if(@route is null)
			break
		
		SET @routes = @routes + (case when @routes = '' then '' else ',' end)+ @route
		
	End
	
	return @routes
	
	return @route
END