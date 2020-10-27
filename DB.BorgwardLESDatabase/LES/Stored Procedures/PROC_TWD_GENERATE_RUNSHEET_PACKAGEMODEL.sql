
-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-13
-- Description:	TWD 生成拉动单
-- Modified: by Caodaowei on 20141013 for 通过组单零件类组单 
-- =============================================
CREATE PROC [LES].[PROC_TWD_GENERATE_RUNSHEET_PACKAGEMODEL]
AS

    declare @plant nvarchar(5)
    declare @assemblyLine nvarchar(10)
    declare @supplierNum nvarchar(12)
    declare @sendTime datetime
    declare @windowTime datetime
    declare @boxParts nvarchar(10)
    declare @dockName nvarchar(10)
	declare @BoxState int
	declare @plant_zone nvarchar(10)
	declare @suggestTime DATETIME
	declare @wmsState int
	declare @now datetime
	declare @isorganizesheet int
	set @now = getdate()
    --找到所有时间小于当前时间未处理的窗口时间、按照供应商分组
    declare sendTime_cursor cursor fast_forward for 
    select t.PLANT,t.ASSEMBLY_LINE,t.SUPPLIER_NUM ,t.BOX_PARTS,max(t.SEND_TIME)as NEAREST_SEND_TIME ,max(t.WINDOW_TIME) as MAX_WINDOW_TIME
    	from LES.TT_TWD_SUPPLIER_SENDTIME t
    	LEFT JOIN LES.TM_TWD_BOX_PARTS b
    	ON t.BOX_PARTS=b.BOX_PARTS 
    	where SEND_TIME < @now and (SEND_TIME_STATUS is null or SEND_TIME_STATUS = 0 ) AND b.CACULATE_TYPE='4'
    	group by t.PLANT,t.ASSEMBLY_LINE,t.SUPPLIER_NUM,t.BOX_PARTS
	
    open sendTime_cursor
begin try	
begin transaction	
    fetch next from sendTime_cursor into @plant , @assemblyLine , @supplierNum ,@boxParts, @sendTime,@windowTime
    while( @@fetch_status = 0 )
    begin
    	--print @plant
    	--print @assemblyLine
    	--print @supplierNum
    	--print @sendTime
    	--print @windowTime
	
    	declare @boxPartsName nvarchar(100)
    	declare @dock nvarchar(10)
    	declare @transSupplierNum nvarchar(20)
    	declare @WAREHOUSE nvarchar(20)
    	declare @transportTime int
    	--找到供应商对应的零件类
    	--declare boxparts_cursor cursor fast_forward for
    	select @boxPartsName=BOX_PARTS_NAME,@dock=DOCK,@transSupplierNum=TRANS_SUPPLIER_NUM,@WAREHOUSE=WAREHOUSE,@transportTime=[TRANSPORT_TIME],@BoxState=[BOX_PARTS_STATE],@isorganizesheet=isnull([IS_ORGANIZE_SHEET],0)  from LES.TM_TWD_BOX_PARTS 
    		where PLANT = @plant and ASSEMBLY_LINE = @assemblyLine and SUPPLIER_NUM = @supplierNum and BOX_PARTS = @boxParts
			and BOX_PARTS_STATE <> 3  -- 3 mean that Rack is invalid
    	--open boxparts_cursor
    	--fetch next from boxparts_cursor into @boxPartsName,@dock,@transSupplierNum,@WAREHOUSE,@transportTime,@BoxState
    	--while( @@fetch_status = 0 )

			exec [LES].[PROC_TWD_UPDATE_COUNTER_INSERT_CONSUME_PACKAGEMODEL] @plant,@assemblyLine,@boxParts,@supplierNum,@dock

			SELECT @dockName=[DOCK_NAME] FROM [LES].[TM_BAS_DOCK] where PLANT = @plant and ASSEMBLY_LINE = @assemblyLine and [DOCK] = @dock

			declare @consumeCount int 
		    SET @consumeCount=0
			--薛海军 修改2014-09-25 增加组单零件类进行组单
			select @consumeCount = count(1) from LES.TI_TWD_MATERIAL_CONSUME
    			where [IS_ORGANIZE_SHEET] =2 
    			and ([SEND_STATUS] =0 or [SEND_STATUS] is null) 
    			and [INHOUSE_PACKAGE_MODEL]= @boxParts 
    			and PLANT = @plant
    			and ASSEMBLY_LINE = @assemblyLine
    			AND INTERFACE_TYPE<>3 --3 为手工拉动，不用处理
			--end 组单零件类完成组单
 
			--print '@consumeCount:' + convert(nvarchar(10), @consumeCount)
			set @wmsState=@BoxState
			declare @runSheetType int 
			if(@consumeCount>0)
			begin
				set @runSheetType = 1
			end
			else
			BEGIN
				set @wmsState=4
				set @runSheetType = 3
			end
			declare @runsheetNo nvarchar(30)
			select @plant_zone=[PLANT_ZONE] from [LES].[TM_BAS_ASSEMBLY_LINE] where [ASSEMBLY_LINE]=@assemblyLine
			--获取拉动单号
			exec LES.PROC_TWD_GET_RUNSHEET_NO @plant,@assemblyLine,@supplierNum,@runsheetNo output
			select @suggestTime=dateadd(mi,-isnull(@transportTime,0),@windowTime)
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
    			   ,[CREATE_USER]
    			   ,GENERATE_TYPE)
    			VALUES
    			   (
    			   @runsheetNo----<TWD_RUNSHEET_NO, varchar(18),>
    			   ,@plant --<PLANT, nvarchar(5),>
    			   ,@assemblyLine --<ASSEMBLY_LINE, nvarchar(10),>
    			   ,null --<WORKSHOP, nvarchar(4),>
    			   ,@plant_zone --<PLANT_ZONE, nvarchar(10),>
    			   ,getdate() --<PUBLISH_TIME, datetime,>
    			   ,@runSheetType  --<RUNSHEET_TYPE, int,>
    			   ,@supplierNum  --<SUPPLIER_NUM, nvarchar(12),>
    			   ,0 --尚不明确含义<SUPPLIER_SN, int,>
    			   ,@dockName--<DOCK, nvarchar(10),>
    			   ,@WAREHOUSE --<DELIVERY_LOCATION, nvarchar(50),>
    			   ,@boxParts --<BOX_PARTS, nvarchar(10),>
    			   ,0 --尚不明确含义<PART_TYPE, int,>
    			   ,null --<UNLOADING_TIME, int,>
    			   ,@windowTime --<EXPECTED_ARRIVAL_TIME, datetime,>
    			   ,@suggestTime --需求尚未确定<SUGGEST_DELIVERY_TIME, datetime,>
    			   ,null --<ACTUAL_ARRIVAL_TIME, datetime,>
    			   ,null --<VERIFY_TIME, datetime,>
    			   ,null --<REJECT_REASON, nvarchar(200),>
    			   ,@transSupplierNum --<TRANS_SUPPLIER_NUM, nvarchar(8),>
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
    			   ,@wmsState --<WMS_SEND_STATUS, int,>
    			   ,'' --<COMMENTS, nvarchar(200),>
    			   ,null --<UPDATE_DATE, datetime,>
    			   ,null --<UPDATE_USER, nvarchar(50),>
    			   ,getdate() --<CREATE_DATE, datetime,>
    			   ,'TWD RUNSHEET ENGINE'--<CREATE_USER, nvarchar(50),>
    			   ,@isorganizesheet)
    			declare @runsheetId int
    			select @runsheetId = scope_identity()
			    if(  @consumeCount > 0 )
			    begin
    				--print '插入明细'
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
    					   ,[COMMENTS]
    					   ,SUPPLIER_NUM_SHEET
    					   ,BOX_PARTS_SHEET)
    				 select
    						@runsheetId--(<TWD_RUNSHEET_SN, int,>
    					   ,@plant --<PLANT, nvarchar(5),>
    					   ,@assemblyLine --<ASSEMBLY_LINE, nvarchar(10),>
    					   ,@supplierNum --<SUPPLIER_NUM, nvarchar(12),>
    					   ,PART_NO --, nvarchar(20),>
    					   ,INDENTIFY_PART_NO --, nvarchar(20),>
    					   ,PART_CNAME --, nvarchar(100),>
    					   ,PART_ENAME --, nvarchar(100),>
    					   ,isnull(@dock,'')--<DOCK, nvarchar(10),>
    					   ,@boxParts --<BOX_PARTS, nvarchar(10),>
    					   ,0 --<SEQUENCE_NO, int,>
    					   ,0 --<PICKUP_SEQ_NO, int,>
    					   ,null --<RDC_DLOC, varchar(20),>
    					   ,INBOUND_PACKAGE--, int,>
    					   ,MEASURING_UNIT_NO --, nvarchar(1),>
    					   ,INBOUND_PACKAGE_MODEL--, nvarchar(30),>
    					   ,sum(PACK_COUNT)--, int,>
    					   ,sum(PACK_COUNT/INBOUND_PACKAGE) --<REQUIRED_INBOUND_PACKAGE, int,>
    					   ,sum(PACK_COUNT) --<REQUIRED_INBOUND_PACKAGE_QTY, int,>
    					   ,0--<ACTUAL_INBOUND_PACKAGE, int,>
    					   ,0--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
    					   ,null --<BARCODE_DATA, nvarchar(50),>
    					   ,null --<COMMENTS, nvarchar(200),>)
    					   ,SUPPLIER_NUM
    					   ,BOX_PARTS
    					from LES.TI_TWD_MATERIAL_CONSUME 
    					where [IS_ORGANIZE_SHEET] =2 AND INTERFACE_TYPE<>3 --3为手工拉动，不用处理
    						and [INHOUSE_PACKAGE_MODEL]= @boxParts
    						and PLANT = @plant
    						and ASSEMBLY_LINE = @assemblyLine
    					group by PART_NO,INDENTIFY_PART_NO,PART_CNAME,PART_ENAME,INBOUND_PACKAGE,MEASURING_UNIT_NO,INBOUND_PACKAGE_MODEL,SUPPLIER_NUM,BOX_PARTS --相同零件多个需求合并到一个明细中
					
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
						  ,'TWD AUTO'
    
					  FROM [LES].[TT_TWD_RUNSHEET] A
					  inner join [LES].[TM_TWD_SUPPLY_BOX_PARTS] B
					  on A.[PLANT]=b.[PLANT] And A.[ASSEMBLY_LINE]=b.[ASSEMBLY_LINE] and A.[BOX_PARTS]=B.[BOX_PARTS]
					  where A.[TWD_RUNSHEET_SN]=@runsheetId
				
						UPDATE A SET A.[ORDER_NO]=B.[AGREEMENT_NO]     ,A.[ITEM_NO]=B.[PROJECT]
					FROM [LES].[TT_TWD_RUNSHEET_DETAIL] A,[LES].[TI_BAS_SUPPLIER_SOURCE_LIST] B
					WHERE A.[PLANT]=B.[PLANT] AND A.[SUPPLIER_NUM]=B.[SUPPLIER_NUM] AND A.[PART_NO]=B.[PART_NO] AND A.[TWD_RUNSHEET_SN]=@runsheetId  
						--modify by【运维】xhm 2014/08/04【CMDB编号：CR-LES-20140807】start
						--零件货源匹配使用期望到达时间
						--modify by【运维】hx 2014/08/04【CMDB编号：CR-LES-20140402】start
						--修改起始有效期和结束有效期只比较日期
						--AND	CONVERT(DATETIME,CONVERT(VARCHAR(10),GETDATE(),23)) BETWEEN START_EFFECTIVE_DATE AND END_EFFECTIVE_DATE
						AND	CONVERT(DATETIME,CONVERT(VARCHAR(10),@windowTime,23)) BETWEEN START_EFFECTIVE_DATE AND END_EFFECTIVE_DATE
						--modify by【运维】hx 2014/04/01 end
						--modify by【运维】xhm 2014/08/04 end
					
					
    					--设置需求表中状态
    					update LES.TI_TWD_MATERIAL_CONSUME 
    					set [IS_ORGANIZE_SHEET]=1
    					where [IS_ORGANIZE_SHEET] =2
    					 and [INHOUSE_PACKAGE_MODEL]= @boxParts
    					 and PLANT = @plant
    					 and ASSEMBLY_LINE = @assemblyLine
    					 AND INTERFACE_TYPE<>3 --3为手工拉动，不用处理

    				end
    				else
    				begin
    					--print 'null'
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
    					  values(
    					    @runsheetId--(<TWD_RUNSHEET_SN, int,>
    					   ,@plant --<PLANT, nvarchar(5),>
    					   ,@assemblyLine --<ASSEMBLY_LINE, nvarchar(10),>
    					   ,@supplierNum --<SUPPLIER_NUM, nvarchar(12),>
    					   ,'******' --, nvarchar(20),>
    					   ,'******' --, nvarchar(20),>
    					   ,'******' --, nvarchar(100),>
    					   ,'******' --, nvarchar(100),>
    					   ,Isnull(@dockName,'') --<DOCK, nvarchar(10),>
    					   ,@boxParts --<BOX_PARTS, nvarchar(10),>
    					   ,0 --<SEQUENCE_NO, int,>
    					   ,0 --<PICKUP_SEQ_NO, int,>
    					   ,null --<RDC_DLOC, varchar(20),>
    					   ,0--INBOUND_PACKAGE--, int,>
    					   ,'1' ---MEASURING_UNIT_NO --, nvarchar(1),>
    					   ,''--INBOUND_PACKAGE_MODEL--, nvarchar(30),>
    					   ,0--sum(PACK_COUNT)--, int,>
    					   ,0--sum(PACK_COUNT/INBOUND_PACKAGE) --<REQUIRED_INBOUND_PACKAGE, int,>
    					   ,0 --sum(PACK_COUNT) --<REQUIRED_INBOUND_PACKAGE_QTY, int,>
    					   ,0--<ACTUAL_INBOUND_PACKAGE, int,>
    					   ,0--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
    					   ,null --<BARCODE_DATA, nvarchar(50),>
    					   ,null --<COMMENTS, nvarchar(200),>)
    					  )
    				end

					--生成BARCODE
					exec [LES].[PROC_TWD_GENERATE_BARCODE_PACKAGEMODEL] @runsheetId

					--生成送货单
					if(@runSheetType=1) and (@isorganizesheet=0)
					exec [LES].[PROC_TWD_MANUAL_GEN_RECKONING_SHEET_PACKAGEMODEL] @runsheetId
	

    		--设置窗口时间中状态
    	update LES.TT_TWD_SUPPLIER_SENDTIME 
    		set LAST_SEND_TIME = getdate() , SEND_TIME_STATUS = 1
    		where SEND_TIME < @now and (SEND_TIME_STATUS = 0 or SEND_TIME_STATUS is null)and BOX_PARTS = @boxParts
		fetch next from sendTime_cursor into @plant , @assemblyLine , @supplierNum ,@boxParts, @sendTime,@windowTime
    end
    close sendTime_cursor
    deallocate sendTime_cursor
    
commit transaction
end try
begin catch
--出错，则返回执行不成功，回滚事务
rollback transaction
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
select getdate(),'TWD','PROC_TWD_GENERATE_RUNSHEET_PACKAGEMODEL','Procedure',error_message(),ERROR_LINE()

end catch