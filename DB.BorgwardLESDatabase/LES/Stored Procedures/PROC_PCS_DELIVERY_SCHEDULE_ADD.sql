

/********************************************************************/
/*                                                                  */
/*   Project Name:  LES                                            */
/*                                                                  */
/*   Program Name:  [PROC_PCS_DELIVERY_SCHEDULE_ADD]				 */
/*                                                                  */
/*   Called By:     web page .add the new timewindow              */
/*                                                                  */
/*   Purpose:       Checks to make sure the  delivery time is right  */
/*											                          */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_DELIVERY_SCHEDULE_ADD]
@plant varchar(10),
@assemblyline varchar(10),
@windowsTime varchar(5),
@routeBoxParts varchar(1000),
@scheduleType int,
@comments varchar(200),
@createUser varchar(50)

AS

-- 根据工厂,流水线,路线,投递时间四个字段判断是否重复
IF(EXISTS(SELECT * FROM LES.TT_PCS_DELIVERY_SCHEDULE WHERE plant=@plant and assembly_line=@assemblyline and  box_parts=@routeBoxParts and windows_time=@windowsTime))
	RETURN -1


BEGIN TRAN

declare @id int;
declare @deliveryTime varchar(5);
declare @time datetime;
declare @onlineTime int;
DECLARE @currentDate DATETIME;

-- 获得DeliveryTime
SET @currentDate=DATEADD(dd,DATEDIFF(dd,0,GETDATE()),0);

select @onlineTime=max(online_time) from LES.TM_PCS_Route_Box_Parts where plant=@plant and assembly_line=@assemblyline and box_parts in (select item from split(@routeBoxParts, ','));

--若PCS零件类中取不到OnlineTime,则从系统配置表中取默认值
if (isnull(@onlineTime,0)<= 0)
	select @onlineTime=parameter_value from LES.TS_SYS_CONFIG where parameter_name='PCS_Post_time';
	
--若系统配置表中取不到默认值,设定默认值
if (isnull(@onlineTime,0)<= 0)
	set @onlineTime=45;
	
if(ISNUMERIC(@windowsTime)= 0)
	set @time = DATEADD(mi,@onlineTime,@currentDate + @windowsTime);
else
	set @time = DATEADD(mi,@onlineTime + @windowsTime,@currentDate);
	
	
set @deliveryTime=@windowsTime
--@deliveryTime直接改为 窗口时间 不进行其他计算
--set @deliveryTime=right('0' + cast(datepart(hh,@time) as varchar(5)),2) + ':' + right('0' + cast(datepart(mi,@time) as varchar(5)),2);

-- 插入模板时间
INSERT INTO LES.TT_PCS_DELIVERY_SCHEDULE(plant,assembly_line,delivery_time,delivery_date,is_deliveried,box_parts,windows_time,schedule_type,comments,create_user,create_date)
VALUES(@plant,@assemblyline,@deliveryTime,'1900-1-1',0,@routeBoxParts,@windowsTime,@scheduleType,@comments,@createUser,getdate());
set @id=scope_identity();

SELECT @id 
-- 插入Route_Box_Parts映射
INSERT INTO LES.TR_PCS_SCHEDULE_BOX_PART(ScheduleId,Plant,Assembly_Line,Box_Parts) 
SELECT @id as scheduleid,@plant as plant,@assemblyline as assembly_line,item from split(@routeBoxParts, ',');

COMMIT TRAN

RETURN 1