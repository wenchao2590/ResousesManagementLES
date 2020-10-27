
-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	TWD 手工拉动
-- Modified date: 2013-07-09
-- Description:	增加零件工位器具号
-- =============================================
CREATE PROC [LES].[PROC_TWD_MANUAL_GENERATE_BEIS3_RUNSHEET]
(
	@PLANT varchar(5),				--工厂编号
	@SUPPLIER_NUM  varchar(5),		--供应商
	@BOX_PARTS	   varchar(10),		--PCS零件类
	@REQUEST_DATE	   datetime		--请求时间
)
AS
	set nocount on
	declare @SUPPLIER_BESI_TIME_SN int
	declare @ASSEMBLY_LINE nvarchar(10)
	declare @DELIVERY_TIME datetime
	declare @WINDOW_TIME datetime
	declare @SEQ_ID int
	declare @suggestTime datetime
	declare @BoxState int
	declare @plant_zone nvarchar(10)
	--找到所有未处理的BEIS3请求
	declare @isorganizesheet int

	create table #REQUEST(
		PART_NO nvarchar(20) null
		,PART_CNAME nvarchar(100) null
		,INBOUND_PACKAGE int null
		,TIME_REQ decimal null)

	declare beis_request_crsr cursor for 
		select SUPPLIER_BESI_TIME_SN,ASSEMBLY_LINE,PLANT,SUPPLIER_NUM,BOX_PARTS,DELIVERY_TIME,CONVERT(varchar(12),@REQUEST_DATE,111) +' '+CONVERT(varchar(12) , WINDOW_TIME,114) WINDOW_TIME,SEQ_ID from [LES].[TT_TWD_BESI3TIME_TEMPLATE] 
			where [PLANT]=@PLANT AND [SUPPLIER_NUM]= @SUPPLIER_NUM AND [BOX_PARTS]=@BOX_PARTS
--add by【运维】xhm 2014/08/04【CMDB编号：CR-LES-20140807】start
--TWD拉动单编码的序号问题,使用事务并捕捉异常			
begin try	
begin transaction	
--add by【运维】xhm 2014/08/04 end		
	open beis_request_crsr

	fetch next from beis_request_crsr into 
		@SUPPLIER_BESI_TIME_SN
		,@ASSEMBLY_LINE
		,@PLANT
		,@SUPPLIER_NUM
		,@BOX_PARTS
		,@DELIVERY_TIME
		,@WINDOW_TIME
		,@SEQ_ID

	while(@@fetch_status =0)
	begin
		--print  @SUPPLIER_BESI_TIME_SN
		

			
		
		--找到零件类对应的dock , transport_supplier
		declare @BOX_PARTS_NAME nvarchar(100)
		declare @DOCK nvarchar(10)
		declare @TRANS_SUPPLIER_NUM nvarchar(20)
		declare @DELIVERY_LOCATION nvarchar(20)
		declare @transportTime int
		select @BOX_PARTS_NAME = BOX_PARTS_NAME
			,@DOCK = b.DOCK_NAME
			,@TRANS_SUPPLIER_NUM = TRANS_SUPPLIER_NUM ,@DELIVERY_LOCATION=WAREHOUSE,@transportTime=[TRANSPORT_TIME],@BoxState=isnull([BOX_PARTS_STATE],1),@isorganizesheet=isnull([IS_ORGANIZE_SHEET],0)
				from LES.TM_TWD_BOX_PARTS a
				LEFT JOIN LES.TM_BAS_DOCK b ON a.PLANT=b.PLANT AND a.ASSEMBLY_LINE=b.ASSEMBLY_LINE  AND a.DOCK=b.DOCK
    			where a.PLANT = @PLANT and a.ASSEMBLY_LINE = @ASSEMBLY_LINE and SUPPLIER_NUM = @SUPPLIER_NUM and BOX_PARTS = @BOX_PARTS
			--print @TRANS_SUPPLIER_NUM
		declare @today datetime
		set @today = convert(datetime , convert(date, @DELIVERY_TIME))

		delete from #REQUEST
		insert into #REQUEST exec LES.PROC_TWD_GET_BEIS3_REQUEST @PLANT,@SUPPLIER_NUM,@SEQ_ID,@REQUEST_DATE
		declare @count int 
		PRINT @SEQ_ID
		select @count = count(1) from #REQUEST where TIME_REQ >0
		--print 'count:' + convert(nvarchar(10),@count)
		if( @count > 0)
		begin
			
			declare @runsheetNo nvarchar(30)
					--获取拉动单号
			exec LES.PROC_TWD_GET_RUNSHEET_NO @PLANT,@ASSEMBLY_LINE,@SUPPLIER_NUM,@runsheetNo output
			select @suggestTime=dateadd(mi,-isnull(@transportTime,0),@WINDOW_TIME)
			--print @runsheetNo
			select @plant_zone=[PLANT_ZONE] from [LES].[TM_BAS_ASSEMBLY_LINE] where [ASSEMBLY_LINE]=@ASSEMBLY_LINE
			
			INSERT INTO [LES].[TT_TWD_RUNSHEET]
				   ([TWD_RUNSHEET_NO]
				   ,[PLANT]
				   ,[ASSEMBLY_LINE]
				   ,[WORKSHOP]
				   ,[PLANT_ZONE]
				   ,[PUBLISH_TIME]
				   ,[RUNSHEET_TYPE]
				   ,[SUPPLIER_NUM]
				   ,[SUPPLIER_SN]
				   ,[DOCK]
				   ,[DELIVERY_LOCATION]
				   ,[BOX_PARTS]
				   ,[PART_TYPE]
				   ,[UNLOADING_TIME]
				   ,[EXPECTED_ARRIVAL_TIME]
				   ,[SUGGEST_DELIVERY_TIME]
				   ,[ACTUAL_ARRIVAL_TIME]
				   ,[VERIFY_TIME]
				   ,[REJECT_REASON]
				   ,[TRANS_SUPPLIER_NUM]
				   ,[FEEDBACK]
				   ,[SHEET_STATUS]
				   ,[SEND_TIME]
				   ,[SEND_STATUS]
				   ,[OPERATON_USER]
				   ,[CHECK_USER]
				   ,[RETRY_TIMES]
				   ,[SUPPLY_TIME]
				   ,[SUPPLY_STATUS]
				   ,[FAX_TIME]
				   ,[FAX_STATUS]
				   ,[SAP_FLAG]
				   ,[SAP_FLAG2]
				   ,[RECKONING_NO]
				   ,[WMS_SEND_TIME]
				   ,[WMS_SEND_STATUS]
				   ,[COMMENTS]
				   ,[UPDATE_DATE]
				   ,[UPDATE_USER]
				   ,[CREATE_DATE]
				   ,[CREATE_USER])
				VALUES
				   (
				   @runsheetNo----<TWD_RUNSHEET_NO, varchar(18),>
				   ,@PLANT --<PLANT, nvarchar(5),>
				   ,@ASSEMBLY_LINE --<ASSEMBLY_LINE, nvarchar(10),>
				   ,null --<WORKSHOP, nvarchar(4),>
				   ,@plant_zone --<PLANT_ZONE, nvarchar(5),>
				   ,getdate() --<PUBLISH_TIME, datetime,>
				   ,4  --<RUNSHEET_TYPE, int,> BEIS3
				   ,@SUPPLIER_NUM  --<SUPPLIER_NUM, nvarchar(12),>
				   ,0 --尚不明确含义<SUPPLIER_SN, int,>
				   ,@DOCK--<DOCK, nvarchar(10),>
				   ,@DELIVERY_LOCATION --<DELIVERY_LOCATION, nvarchar(50),>
				   ,@BOX_PARTS --<BOX_PARTS, nvarchar(10),>
				   ,0 --尚不明确含义<PART_TYPE, int,>
				   ,null --<UNLOADING_TIME, int,>
				   ,@WINDOW_TIME --<EXPECTED_ARRIVAL_TIME, datetime,>
				   ,@suggestTime --需求尚未确定<SUGGEST_DELIVERY_TIME, datetime,>
				   ,null --<ACTUAL_ARRIVAL_TIME, datetime,>
				   ,null --<VERIFY_TIME, datetime,>
				   ,null --<REJECT_REASON, nvarchar(200),>
				   ,@TRANS_SUPPLIER_NUM --<TRANS_SUPPLIER_NUM, nvarchar(8),>
				   ,null --<FEEDBACK, nvarchar(100),>
				   ,0 ---<SHEET_STATUS, int,>
				   ,getdate() --<SEND_TIME, datetime,>
				   ,@BoxState --<SEND_STATUS, int,>
				   ,null --<OPERATON_USER, nvarchar(10),>
				   ,null --<CHECK_USER, nvarchar(10),>
				   ,0 --<RETRY_TIMES, int,>
				   ,null --<SUPPLY_TIME, datetime,>
				   ,0 --<SUPPLY_STATUS, int,>
				   ,null --<FAX_TIME, datetime,>
				   ,0 --<FAX_STATUS, int,>
				   ,0 --<SAP_FLAG, int,>
				   ,0 ---<SAP_FLAG2, int,>
				   ,null --<RECKONING_NO, nvarchar(30),>
				   ,null --<WMS_SEND_TIME, datetime,>
				   ,@BoxState --<WMS_SEND_STATUS, int,>
				   ,'' --<COMMENTS, nvarchar(200),>
				   ,null --<UPDATE_DATE, datetime,>
				   ,null --<UPDATE_USER, nvarchar(50),>
				   ,getdate() --<CREATE_DATE, datetime,>
				   ,'Manual BEIS3'--<CREATE_USER, nvarchar(50),>
				   )
				declare @runsheetId int
    			select @runsheetId = scope_identity()
    					INSERT INTO [LES].[TT_TWD_RUNSHEET_DETAIL]
    						   ([TWD_RUNSHEET_SN]
    						   ,[PLANT]
    						   ,[ASSEMBLY_LINE]
    						   ,[SUPPLIER_NUM]
    						   ,[PART_NO]
    						   ,[IDENTIFY_PART_NO]
    						   ,[PART_CNAME]
    						   ,[PART_ENAME]
    						   ,[DOCK]
    						   ,[BOX_PARTS]
    						   ,[SEQUENCE_NO]
    						   ,[PICKUP_SEQ_NO]
    						   ,[RDC_DLOC]
    						   ,[INBOUND_PACKAGE]
    						   ,[MEASURING_UNIT_NO]
    						   ,[INBOUND_PACKAGE_MODEL]
    						   ,[PACK_COUNT]
    						   ,[REQUIRED_INBOUND_PACKAGE]
    						   ,[REQUIRED_INBOUND_PACKAGE_QTY]
    						   ,[ACTUAL_INBOUND_PACKAGE]
    						   ,[ACTUAL_INBOUND_PACKAGE_QTY]
    						   ,[BARCODE_DATA]
    						   ,[COMMENTS])
    					 select
    							@runsheetId
    						   ,@PLANT 
    						   ,@ASSEMBLY_LINE 
    						   ,@SUPPLIER_NUM 
    						   ,PART_NO 
    						   ,PART_NO 
    						   ,PART_CNAME 
    						   ,'' 
    						   ,@DOCK
    						   ,@BOX_PARTS 
    						   ,0 
    						   ,0 
    						   ,null 
    						   ,INBOUND_PACKAGE
    						   ,'' 
    						   ,LES.Func_Get_Inbound_Package_Model(PART_NO,@PLANT,@ASSEMBLY_LINE,@BOX_PARTS,@SUPPLIER_NUM)
    						  ,ceiling(TIME_REQ/INBOUND_PACKAGE)		--[PACK_COUNT]
    						   ,ceiling(TIME_REQ/INBOUND_PACKAGE)
    						   ,ceiling(TIME_REQ/INBOUND_PACKAGE)* INBOUND_PACKAGE
    						   ,0
    						   ,0
    						   ,null 
    						   ,null 
							from #REQUEST where TIME_REQ >0 
					INSERT INTO [LES].[TT_TWD_RUNSHEET_SEND_SUPPLY]
						   ([TWD_RUNSHEET_SN]
						   ,[TWD_RUNSHEET_NO]
						   ,[PUBLISH_TIME]
						   ,[SUPPLIER_NUM]
						   ,[BOX_PARTS]
						   ,[SEND_TIME]
						   ,[SEND_STATUS]
						   ,[COMMENTS]
						   ,[CREATE_DATE]
						   ,[CREATE_USER])
						SELECT [TWD_RUNSHEET_SN]
						 
							  ,A.[TWD_RUNSHEET_NO]
							  ,A.[PUBLISH_TIME]
							  ,B.[SUPPLIER_NUM]
							  ,A.[BOX_PARTS]
							  ,null
							  ,1
							  ,null
							  ,getdate()
							  ,'TWD BESI'
	    
						  FROM [LES].[TT_TWD_RUNSHEET] A
						  inner join [LES].[TM_TWD_SUPPLY_BOX_PARTS] B
						  on A.[PLANT]=b.[PLANT] And A.[ASSEMBLY_LINE]=b.[ASSEMBLY_LINE] and A.[BOX_PARTS]=B.[BOX_PARTS]
						  where A.[TWD_RUNSHEET_SN]=@runsheetId
			UPDATE A SET A.[ORDER_NO]=B.[AGREEMENT_NO]     ,A.[ITEM_NO]=B.[PROJECT]
					FROM [LES].[TT_TWD_RUNSHEET_DETAIL] A,[LES].[TI_BAS_SUPPLIER_SOURCE_LIST] B
					WHERE A.[PLANT]=B.[PLANT] AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND A.[PART_NO]=B.[PART_NO] AND A.[TWD_RUNSHEET_SN]=@runsheetId 
					--modify by【运维】xhm 2014/08/04【CMDB编号：CR-LES-20140807】start
					--零件货源匹配使用期望到达时间
					--modify by【运维】hx 2014/04/01【CMDB编号：CR-LES-20140402】start
					--修改起始有效期和结束有效期只比较日期
					--AND	CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),23)) BETWEEN START_EFFECTIVE_DATE AND END_EFFECTIVE_DATE
					AND	CONVERT(DATETIME,CONVERT(VARCHAR(10),@WINDOW_TIME,23)) BETWEEN START_EFFECTIVE_DATE AND END_EFFECTIVE_DATE
					--modify by【运维】hx 2014/04/01 end
					--modify by【运维】xhm 2014/08/04 end

				--生成送货单,只有生成送货单，对于延迟生成送货单和不生成送货单，不操作xuehaijun20140926
					if(@isorganizesheet=0)		
			exec LES.PROC_TWD_GENERATE_BARCODE @runsheetId

			--生成送货单
					exec [LES].[PROC_TWD_MANUAL_GEN_RECKONING_SHEET] @runsheetId
		end
		
		
		
			
		
		fetch next from beis_request_crsr into 
		@SUPPLIER_BESI_TIME_SN
		,@ASSEMBLY_LINE
		,@PLANT
		,@SUPPLIER_NUM
		,@BOX_PARTS
		,@DELIVERY_TIME
		,@WINDOW_TIME
		,@SEQ_ID
	end
	
	close beis_request_crsr
	deallocate beis_request_crsr
	
	update LES.TT_TWD_BESI3_REQUEST set FLAG = 1 WHERE [PLANT]=@PLANT AND [SUPPLIER_NUM]= @SUPPLIER_NUM 
	drop table #REQUEST
--add by【运维】xhm 2014/08/04【CMDB编号：CR-LES-20140807】start
--TWD拉动单编码的序号问题,使用事务并捕捉异常
commit transaction
end try
begin catch
--出错，则返回执行不成功，回滚事务
rollback transaction
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'PCS','PROC_TWD_MANUAL_GENERATE_BEIS3_RUNSHEET','Procedure',error_message(),ERROR_LINE()

end catch	
	set nocount off
--add by【运维】xhm 2014/08/04 end