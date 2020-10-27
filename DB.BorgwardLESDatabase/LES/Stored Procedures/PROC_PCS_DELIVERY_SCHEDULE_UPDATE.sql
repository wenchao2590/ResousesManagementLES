


CREATE PROCEDURE [LES].[PROC_PCS_DELIVERY_SCHEDULE_UPDATE]
@schedule_identity int,
@windowsTime varchar(5),
@routeBoxParts varchar(1000),
@scheduleType int,
@plant varchar(10),
@assemblyline varchar(10),
@comments varchar(200)

AS

BEGIN TRAN

declare @deliveryTime varchar(5);
declare @time datetime;
declare @onlinetime int;
DECLARE @CurrentDate DATETIME;

SET @CurrentDate=DATEADD(dd,DATEDIFF(dd,0,GETDATE()),0);

select @onlineTime=max(online_time) from LES.TM_PCS_Route_Box_Parts where plant=@plant and assembly_line=@assemblyline and box_parts in (select item from split(@routeBoxParts, ','));

--若PCS零件类中取不到OnlineTime,则从系统配置表中取默认值
if (isnull(@onlineTime,0)<= 0)
	select @onlineTime=parameter_value from LES.TS_SYS_CONFIG where parameter_name='PCS_Post_time';

--若系统配置表中取不到默认值,设定默认值
if (isnull(@onlineTime,0)<= 0)
	set @onlineTime=45;

-- 获得DeliveryTime
if(ISNUMERIC(@windowsTime)= 0)
	set @time = DATEADD(mi,@onlineTime,@currentDate + @windowsTime);
else
	set @time = DATEADD(mi,@onlineTime + @windowsTime,@currentDate);


set @deliveryTime=@windowsTime
--@deliveryTime直接改为 窗口时间 不进行其他计算
--set @deliveryTime=right('0' + cast(datepart(hh,@time) as varchar(5)),2) + ':' + right('0' + cast(datepart(mi,@time) as varchar(5)),2);

--更新窗口时间记录
UPDATE LES.TT_PCS_DELIVERY_SCHEDULE
SET delivery_time=@deliveryTime,
windows_time=@windowsTime,
schedule_type=@scheduleType,
box_parts=@routeBoxParts,
comments=@comments
WHERE schedule_identity=@schedule_identity

-- 更新Route_Box_Parts映射表数据
DELETE FROM LES.TR_PCS_SCHEDULE_BOX_PART WHERE scheduleid=@schedule_identity;

INSERT INTO LES.TR_PCS_SCHEDULE_BOX_PART(ScheduleId,Plant,Assembly_Line,Box_Parts) 
SELECT @schedule_identity as scheduleid,@plant as plant,@assemblyline as assembly_line,item from split(@routeBoxParts, ',');

--模板变更, 则对当天的时间进行更新
if (@scheduleType = 1)
begin
	-- 模板为允许, 可能时间变动
	UPDATE LES.TT_PCS_DELIVERY_SCHEDULE set delivery_time=@deliveryTime,windows_time=@windowsTime,comments=@comments where template_id=@schedule_identity and is_deliveried=0
end
ELSE if  (@scheduleType = 2)
	-- 模板为禁止, 设置状态为跳过
	UPDATE LES.TT_PCS_DELIVERY_SCHEDULE set delivery_time=@deliveryTime,windows_time=@windowsTime,schedule_type=@scheduleType,is_deliveried=2,comments=@comments where template_id=@schedule_identity and is_deliveried=0
else
begin
	-- 模板为run once, 可能时间变动
	if((@currentdate + @deliveryTime)<getdate())
		set @CurrentDate=DATEADD(dd,DATEDIFF(dd,0,GETDATE()),1)

	if(exists(select * from LES.TT_PCS_DELIVERY_SCHEDULE where template_id=@schedule_identity and DATEDIFF(dd,@CurrentDate,GETDATE())=0))
		UPDATE LES.TT_PCS_DELIVERY_SCHEDULE set delivery_time=@deliveryTime,windows_time=@windowsTime,comments=@comments where template_id=@schedule_identity and DATEDIFF(dd,@CurrentDate,GETDATE())=0
	else
		INSERT INTO LES.TT_PCS_DELIVERY_SCHEDULE(plant,assembly_line,delivery_time,delivery_date,is_deliveried,box_parts,windows_time,schedule_type, template_id,comments)
		SELECT plant,assembly_line,delivery_time,@CurrentDate,0,box_parts,windows_time,schedule_type,@schedule_identity,@comments from LES.TT_PCS_DELIVERY_SCHEDULE where schedule_identity=@schedule_identity;
end
COMMIT TRAN