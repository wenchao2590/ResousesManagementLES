--====================================================
--Author:       <xuehaijun>
--Create date:  <2011-08-15>
--Description:  <生成PCS拉动单>
--====================================================
CREATE PROCEDURE [LES].[PROC_PCS_CREATE_PULL_SHEET]
(
	@Plant varchar(5),				--工厂编号
	@AssemblyLine  varchar(12),		--流水线编号
	@supplier  varchar(5),			--供应商
	@Route	   varchar(10),			--送货路径
	@PARTBOX	   varchar(10)		--PCS零件类
	--@result int output			--返回执行结果
)
as
set xact_abort on
declare @result int
set @result = 0

DECLARE @CurrentTime DATETIME, @CurrentDate DATETIME;
SET @CurrentTime=GETDATE();
SET @CurrentDate=DATEADD(dd,DATEDIFF(dd,0,@CurrentTime),0)

if(@supplier is null or @supplier='')--由于上一个存储过程查询 如果该字段为空则 该零件类肯定没有零件消耗
begin
	UPDATE [LES].[TT_PCS_DELIVERY_SCHEDULE] WITH (ROWLOCK) SET [IS_DELIVERIED] = 1
	WHERE [IS_DELIVERIED]=0 and [PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine  and [BOX_PARTS]=@PARTBOX
	AND @CurrentTime>=CONVERT(DATETIME,CONVERT(NVARCHAR(10),delivery_date,120)+ ' ' +delivery_time)
return
end

declare @SuggestDeliveryTime datetime
declare @expected_arrival_time datetime
--declare @post_time varchar(5)

IF(NOT EXISTS(SELECT * FROM [LES].[TT_PCS_DELIVERY_SCHEDULE] WITH (NOLOCK)
	WHERE delivery_date='1900-1-1' and plant=@plant and ASSEMBLY_LINE=@AssemblyLine))
RETURN

Declare	@RollBack Integer
SET @RollBack = 0


declare @startTime datetime
declare @endtime datetime
declare @termdiff int
declare @scan_group_id int
declare @delay_time   int
declare @transport_time  int
declare @online_timme  int
declare @total_time int
declare @tempdate datetime
declare @plant_zone nvarchar(10)
SET @scan_group_id = 0
SET @termdiff=0

--如监测到无可用的delivery_date
--则根据delivery_date='1900-1-1'的模板重新生成一天的deliverySchedule
begin try

INSERT INTO LES.TT_PCS_DELIVERY_SCHEDULE(plant,[ASSEMBLY_LINE],delivery_time,delivery_date,[IS_DELIVERIED],[BOX_PARTS],[WINDOWS_TIME],schedule_type, template_id,[CREATE_USER],[CREATE_DATE])
SELECT a.plant,a.[ASSEMBLY_LINE],delivery_time,@CurrentDate,0,b.[BOX_PARTS],[WINDOWS_TIME],schedule_type, schedule_identity,'1',getdate()
FROM LES.TT_PCS_DELIVERY_SCHEDULE A WITH (NOLOCK),LES.TR_PCS_SCHEDULE_BOX_PART B WITH (NOLOCK)
WHERE A.Schedule_identity=B.ScheduleID and
 delivery_date='1900-1-1' and a.plant=@plant and a.[ASSEMBLY_LINE]=@AssemblyLine and schedule_type<>2 
	--and @CurrentTime < CONVERT(DATETIME,CONVERT(NVARCHAR(10),@CurrentTime,120)+ ' ' +delivery_time)  --仅生成当前时间时候的窗口时间，已禁用
	and schedule_identity not in 
	(select template_id from LES.TT_PCS_DELIVERY_SCHEDULE where delivery_date=@CurrentDate)
end try
begin catch
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'PCS','PROC_PCS_CREATE_PULL_SHEET','Procedure',error_message(),ERROR_LINE()

end catch

--按时间间隔生成窗口时间
begin try
if(not exists (select * from LES.TM_BAS_WORK_SCHEDULE WITH (NOLOCK) where  plant=@plant and ASSEMBLY_LINE=@AssemblyLine and getdate() between BEGIN_TIME and [end_time]))
return


if( exists(SELECT a.plant,a.[ASSEMBLY_LINE],delivery_time,getdate(),0,b.[BOX_PARTS],[WINDOWS_TIME],schedule_type, schedule_identity,'1',getdate()
FROM LES.TT_PCS_DELIVERY_SCHEDULE A WITH (NOLOCK),LES.TR_PCS_SCHEDULE_BOX_PART B WITH (NOLOCK)
WHERE A.Schedule_identity=B.ScheduleID and b.[BOX_PARTS]=@PARTBOX and datediff(day,a.delivery_date,'1900-01-01 00:00:00.000')=0 and schedule_type=2  and 
schedule_identity not in 
	(select template_id from LES.TT_PCS_DELIVERY_SCHEDULE WITH (NOLOCK) where datediff(day,getdate(),delivery_date)=0)
))
begin	
	
	select @startTime=	min(begin_time),@endtime=max(end_time) from LES.TM_BAS_WORK_SCHEDULE WITH (NOLOCK) where  plant=@plant and ASSEMBLY_LINE=@AssemblyLine and datediff(day,getdate(),[date])=0
	if(@startTime=null)
		return

	if(	@startTime<getdate())
		set @startTime=getdate()
	select 	@termdiff=windows_time from LES.TT_PCS_DELIVERY_SCHEDULE WITH (NOLOCK) where  CharIndex(@PARTBOX,box_parts)>0 and schedule_type=2  and datediff(day,delivery_date,'1900-01-01 00:00:00.000')=0
	if(@termdiff<=0)
		return
	select 	@delay_time=isnull(delay_time,0),@transport_time=isnull(transport_time,0),@online_timme=isnull(online_time,0) from [LES].[TM_PCS_ROUTE_BOX_PARTS] WITH (NOLOCK) where   plant=@plant and [ASSEMBLY_LINE]=@AssemblyLine and [BOX_PARTS]=@PARTBOX

	select 	@startTime=DATEADD(mi,@termdiff,@startTime)
	while(	@startTime<@endtime)
	begin
		
		select @tempdate=dateadd(mi,-@delay_time,@startTime)
		if(@startTime>@tempdate)
		select @tempdate=@startTime
		INSERT INTO LES.TT_PCS_DELIVERY_SCHEDULE(plant,[ASSEMBLY_LINE],delivery_time,delivery_date,[IS_DELIVERIED],[BOX_PARTS],[WINDOWS_TIME],schedule_type, template_id,[CREATE_USER],[CREATE_DATE])
		SELECT a.plant,a.[ASSEMBLY_LINE],convert(char(5),@tempdate,108)  ,@CurrentDate,0,b.[BOX_PARTS],convert(char(5),@startTime,108),schedule_type, schedule_identity,'1',getdate()
		FROM LES.TT_PCS_DELIVERY_SCHEDULE A WITH (NOLOCK),LES.TR_PCS_SCHEDULE_BOX_PART B WITH (NOLOCK)
		WHERE A.Schedule_identity=B.ScheduleID and
		 delivery_date='1900-1-1' and a.plant=@plant and a.[ASSEMBLY_LINE]=@AssemblyLine and schedule_type=2 
		select 	@startTime=DATEADD(mi,@termdiff,@startTime)
	end	
end
end try

begin catch
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'PCS','PROC_PCS_CREATE_PULL_SHEET','Procedure',error_message(),ERROR_LINE()

end catch
--print '1'
if(not exists( SELECT * FROM [LES].[TT_PCS_DELIVERY_SCHEDULE] WITH (NOLOCK)
					WHERE [IS_DELIVERIED]=0 and [PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine  and
		 [BOX_PARTS]=@PARTBOX AND @CurrentTime>=CONVERT(DATETIME,CONVERT(NVARCHAR(10),delivery_date,120)+ ' ' +delivery_time)
))
return	
--增加判断如果当前窗口时间  该零件类没有消耗零件信息   则直接更新对应窗口时间状态
--if((select count(1) FROM [LES].[TI_PCS_MATERIAL_REQUESTS] where 
	   --[PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine and  [IS_ORGANIZE_SHEET]=1  and [INTERFACE_STATUS]=0  and
		 --[BOX_PARTS]=@PARTBOX)=0)

	--print '2'
	
	declare @pcs_pull_runsheet_id int
	declare @supply_code varchar(20)
	declare @runsheetNo varchar(22)
	declare @WmNo varchar(20)
	declare @interfaceid int
	declare @runsheet_type int
	declare @SEND_STATUS int
	--declare @count int
	select @supply_code=[PARAMETER1] from  [LES].TM_SYS_APPLICATION_CONFIGURATION WITH (NOLOCK) where [APPLICATION]='PCS_WMS_SUPPLY'
	--select @plant_zone=[PLANT_ZONE] from [LES].[TM_BAS_ASSEMBLY_LINE] where [ASSEMBLY_LINE]=@AssemblyLine
	--plant_zone借用存放目的存储区。
	select top 1 @plant_zone=[PLANT_ZONE] from LES.[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] WITH (NOLOCK) where INHOUSE_SYSTEM_MODE = 'PCS' and INHOUSE_PART_CLASS = @PARTBOX


	--生成PCS拉动单

		EXEC	@pcs_pull_runsheet_id=[LES].[PROC_PCS_GET_NEXT_SEQUENCE] 'pcs_pull_runsheet_id'
		EXEC	LES.PROC_PCS_GET_RUNSHEET_NO @Plant,@AssemblyLine,@supplier,@runsheetNo output
		
		--print @runsheetNo;

		set @runsheet_type=1
		--select @expected_arrival_time=GETDATE()
		--select @expected_arrival_time=max([expected_arrival_time]) from [LES].[TI_PCS_MATERIAL_REQUESTS] where
		--[PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine and  [IS_ORGANIZE_SHEET]=1  and [INTERFACE_STATUS]=0 and
		-- [BOX_PARTS]=@PARTBOX  
		--if(@expected_arrival_time=null)
		--select @expected_arrival_time=GETDATE()
		
		--获取上线时间
		declare @online_time int
		set @online_time =(
				select top 1 [ONLINE_TIME]
				from LES.TM_PCS_ROUTE_BOX_PARTS WITH (NOLOCK)
				where plant=@Plant and [ASSEMBLY_LINE]=@AssemblyLine
				and [SUPPLIER_NUM]=@supplier and [BOX_PARTS]= @Route)
		if (@online_time is null)
			select @online_time=parameter_value from [LES].[TS_SYS_CONFIG] WITH (NOLOCK) where parameter_name='PCSDefaultOnlineTime';
		if (isnull(@online_time,0)<= 0)
		begin
			set @online_time=45;
		end
		set @expected_arrival_time = dateadd(MI,@online_time,@CurrentTime)	

		set @SuggestDeliveryTime = dateadd(mi,-(@delay_time+@transport_time),@expected_arrival_time);  --出发时间=期望到达时间-延误时间-运输时间

		SELECT @interfaceid=max([INTERFACE_ID]) 
		FROM [LES].[TI_PCS_MATERIAL_REQUESTS] WITH (NOLOCK) where  [PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine and  [IS_ORGANIZE_SHEET]=1  and [INTERFACE_STATUS]=0 and
		 [BOX_PARTS]=@PARTBOX  
		 
		 select @SEND_STATUS=case when [BOX_PARTS_STATE]=1 then 1 else 3 end FROM LES.TM_PCS_ROUTE_BOX_PARTS WITH (NOLOCK) WHERE [PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine  and
		 [BOX_PARTS]=@PARTBOX  
		--print '2'
begin try
begin transaction
		INSERT INTO [LES].[TT_PCS_RUNSHEET]
           ([PCS_RUNSHEET_SN]
           ,[PLANT]
           ,[ASSEMBLY_LINE]
           ,[PCS_RUNSHEET_NO]
           ,[PUBLISH_TIME]
           ,[RUNSHEET_TYPE]
           ,[SUPPLIER_NUM]
           ,[BOX_PARTS]
           ,[UNLOADING_TIME]
           ,[EXPECTED_ARRIVAL_TIME]
           ,[SHEET_STATUS]
           ,[SEND_STATUS]
           ,[WMS_SEND_STATUS]
           ,[SAP_FLAG]
           ,[PLANT_ZONE]
		   ,[VERIFY_TIME]
		   ,[CREATE_DATE])
			select 
			@pcs_pull_runsheet_id,
			@Plant,
			@AssemblyLine,
			@runsheetNo,
			getdate(),
			@runsheet_type,				--PCS 拉动单
			@supplier,
			@PARTBOX,
			null,            --[UNLOADING_TIME]
			@expected_arrival_time,			 --[EXPECTED_ARRIVAL_TIME]
			10,				 --[SHEET_STATUS]
            @SEND_STATUS ,			--[SEND_STATUS]
			@SEND_STATUS ,			--[WMS_SEND_STATUS]
			0,
			@plant_zone,
			@SuggestDeliveryTime,
			GETDATE()

			--print '3'
		INSERT INTO [LES].[TT_PCS_RUNSHEET_DETAIL]
           ([PCS_RUNSHEET_SN]
           ,[PLANT]
           ,[ASSEMBLY_LINE]
           ,[LOCATION]
           ,[SUPPLIER_NUM]
           ,[PART_NO]
           ,[PART_CNAME]
           ,[PART_ENAME]
           ,[DOCK]
           ,[BOX_PARTS]
        --   ,[SEQUENCE_NO]
         --  ,[PICKUP_SEQ_NO]
           ,[INHOUSE_PACKAGE]
           ,[MEASURING_UNIT_NO]
           ,[INHOUSE_PACKAGE_MODEL]
           ,[PACK_COUNT]
           ,[REQUIRED_INHOUSE_PACKAGE]
           ,[REQUIRED_INHOUSE_PACKAGE_QTY]
           )
        
		SELECT  @pcs_pull_runsheet_id
      ,[PLANT]
      ,[ASSEMBLY_LINE]
      ,[LOCATION]
      ,[SUPPLIER_NUM]
      ,[PART_NO]
      ,[PART_CNAME]
      ,[PART_ENAME]
      ,[DOCK]
      ,[BOX_PARTS]
     -- ,[PICKUP_SEQ_NO]
     -- ,[SEQUENCE_NO]
      ,[INHOUSE_PACKAGE]
      ,[MEASURING_UNIT_NO]
      ,[INHOUSE_PACKAGE_MODEL]
      ,sum([PACK_COUNT]) [PACK_COUNT]
      ,sum([PACK_COUNT]) [PACK_COUNT]
      ,sum([PACK_COUNT])*[INHOUSE_PACKAGE]
  FROM [LES].[TI_PCS_MATERIAL_REQUESTS] with (nolock) where 
	   [PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine and  [IS_ORGANIZE_SHEET]=1  and [INTERFACE_STATUS]=0  and
		 [BOX_PARTS]=@PARTBOX and [INTERFACE_ID]<=@interfaceid
	  group by [PLANT]
		,[ASSEMBLY_LINE]
		,[LOCATION]
		,[SUPPLIER_NUM]
		,[PART_NO]
		,[PART_CNAME]
		,[PART_ENAME]
		,[DOCK]
		,[BOX_PARTS]
		,[INHOUSE_PACKAGE]
		,[MEASURING_UNIT_NO]
		,[INHOUSE_PACKAGE_MODEL]
		
	  --order by detailorder,book_keeper  --book_keeper借用来排序	
		
	--print '21'
	--更改LES.TT_PCS_DELIVERY_SCHEDULE状态
	update [LES].[TI_PCS_MATERIAL_REQUESTS]
	set [INTERFACE_STATUS] = 1 where [PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine and  [IS_ORGANIZE_SHEET]=1  and [INTERFACE_STATUS]=0  and
		 [BOX_PARTS]=@PARTBOX and [INTERFACE_ID]<=@interfaceid

    UPDATE [LES].[TT_PCS_DELIVERY_SCHEDULE] SET [IS_DELIVERIED] = 1
		WHERE [IS_DELIVERIED]=0 and TEMPLATE_ID is not null and [PLANT]=@Plant and [ASSEMBLY_LINE]= @AssemblyLine  and [BOX_PARTS]=@PARTBOX
		  AND @CurrentTime>=CONVERT(DATETIME,CONVERT(NVARCHAR(10),delivery_date,120)+ ' ' +delivery_time)

	--创建仓库出库单
	exec LES.PROC_INSERT_OUTPUT @pcs_pull_runsheet_id,'PCS'

	--投递二级拉动 Andy Liu 2015-12-14
	exec LES.[PROC_TWD_GEN_TWICEPULL_DATA] @pcs_pull_runsheet_id,'PCS'

--执行成功，提交事务，返回执行成功
commit transaction
set @result = 1
return
end try

begin catch
--出错，则返回执行不成功，回滚事务
rollback transaction

--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'PCS','PROC_PCS_CREATE_PULL_SHEET','Procedure',error_message(),ERROR_LINE()

set @result = 0
return
end catch