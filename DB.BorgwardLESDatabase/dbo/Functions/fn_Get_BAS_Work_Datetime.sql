
-- =============================================
-- Author:		fengyoujun
-- Create date: 10/10/2008
-- Description:	<Get Work Datetime> reference from SP_BAS_Get_Work_Datetime
--              updated not found return time: from 1900-01-1 to null
--              update when start time not in worktime then still get expect datetime in worktime(2009/05/04)
-- =============================================
CREATE function [dbo].[fn_Get_BAS_Work_Datetime]
(
	 @plant varchar(10),
	 @workshop varchar(10),
     @starttime datetime,
     @diftime int	 
)
returns datetime
as
begin
	declare @worktime datetime
	declare @default_not_found_time datetime
	declare @mi_count int 
	set @default_not_found_time = '1900-01-1'

	--设置返回值	
	set @worktime = dateadd(MI,@diftime,@starttime)
    --如果开始时间不在工作时间内，则返回1900-01-01
	if exists
	(
		select * from TS_BAS_WORKSCHEDULE
		where plant = @plant and workshop = @workshop and work_schedule_type = 1
		and start_time <= @starttime and end_time >= @starttime
	)
	begin
		--需求时间与在途时间的差>0时，实际需求时间在下一个工作时间内
		select @worktime = min(end_time) 
		from  (select distinct plant,workshop,start_time,end_time from TS_BAS_WORKSCHEDULE where work_schedule_type = 1) a 
		where plant = @plant and workshop = @workshop
		and start_time <= @starttime and end_time >= @starttime

		
		set @mi_count = datediff(MI,@starttime,@worktime)
	end
	else
	begin
		select @worktime = @starttime
		set @mi_count = 0
	end	
   
	--如果需求时间在工作时间内，则从开始时间累加计算到需求时间，遇到非工作时间跳过不计算

	

	while(@diftime - @mi_count > 0)
	begin
		set @diftime = @diftime - @mi_count
		if exists
		(
			select *
			from (select distinct plant,workshop,start_time,end_time from TS_BAS_WORKSCHEDULE where work_schedule_type = 1) a  
			where plant = @plant and workshop = @workshop 
			and start_time >= @worktime
		)
		begin
			--找出下一个工作时间的开始
			select @starttime = min(start_time)
			from (select distinct plant,workshop,start_time,end_time from TS_BAS_WORKSCHEDULE where work_schedule_type=1) a  
			where plant = @plant and workshop = @workshop 
			and start_time >= @worktime

			--找出下一个工作时间的结束
			select @worktime = min(end_time)
			from (select distinct plant,workshop,start_time,end_time from TS_BAS_WORKSCHEDULE where work_schedule_type=1) a  
			where plant = @plant and workshop = @workshop 
			and start_time <= @starttime and end_time >= @starttime			
			
			--计算这个工作时间的跨度
            set @mi_count = datediff(MI,@starttime,@worktime)
		end
		else
		begin
			--计算的时间不在工作时间内，则返回1900-01-01
			set @worktime = @default_not_found_time
			break
		end
	end
	    
	if(@worktime <> @default_not_found_time)
	begin
		set @worktime = dateadd(MI,@diftime,@starttime)
	end
	else
	begin
		set @worktime = null
	end
		

	return @worktime
end