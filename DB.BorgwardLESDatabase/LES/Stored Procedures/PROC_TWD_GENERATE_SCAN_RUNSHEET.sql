-- =============================================
-- Author:		Andy Liu
-- Create date: 2015-06-25
-- Description:	看板扫描组单
-- =============================================

CREATE PROC [LES].[PROC_TWD_GENERATE_SCAN_RUNSHEET]

(
	@groupID	   varchar(20),		--组号	
	@pulltype	   varchar(2),		--拉动单类型
	@remark	   varchar(200),		--备注
	@CreateUser	NVARCHAR(50)		-- 操作用户
)
AS 
    declare @sendTime datetime
	declare @dock varchar(40)
	declare @now datetime
	set @now = getdate()
	declare @suggestTime datetime
	declare @isorganizesheet int
    --找到所有时间小于当前时间未处理的窗口时间、按照供应商分组

	declare @Plant		varchar(5)					--工厂编号
	declare @AssemblyLine  varchar(12)				--流水线编号
	declare @supplier	varchar(5)					--供应商
	declare @PARTBOX	varchar(10)					--TWD零件类
	declare @expectedarrivaltime	datetime		--期望到达时间
	
	----根据供应商、TWD零件类分组组单
 --print '0'
  begin
		begin try	
		begin transaction	
			declare consume_crsr cursor  fast_forward  for 
				select  plant, Assembly_Line, SUPPLIER_NUM, BOX_PARTS, max(EXPECTED_ARRIVAL_TIME)
					from LES.TI_TWD_MATERIAL_CONSUME where scan_group_id = @groupID
					group by plant,Assembly_Line,SUPPLIER_NUM,BOX_PARTS
					
			open consume_crsr
			fetch next from consume_crsr into @plant,@AssemblyLine,@supplier,@PARTBOX,@expectedarrivaltime
			
			while( @@fetch_status = 0 )
			begin
				--print '1'
    			declare @boxPartsName nvarchar(100)
    			declare @transSupplierNum nvarchar(20)
				declare @dockName nvarchar(10)
				declare @DELIVERY_LOCATION nvarchar(20)
				declare @transportTime int
				declare @BoxState int
				declare @plant_zone nvarchar(10)

    			--找到供应商对应的零件类
				declare @runSheetType int 
				--print @groupID
				set @runSheetType = 1  --改成普通单据 
				if(@expectedarrivaltime='')
				begin
					select @expectedarrivaltime=getdate()
				end
				declare @runsheetNo nvarchar(30)

				--获取拉动单号			
				exec LES.PROC_TWD_GET_RUNSHEET_NO @plant,@assemblyLine,@supplier,@runsheetNo output
				select @suggestTime=dateadd(mi,-isnull(@transportTime,0),@expectedarrivaltime)
				--select  max( isnull([EXPECTED_ARRIVAL_TIME],getdate())),max(isnull(dock,'')) FROM [LES].[TI_TWD_MATERIAL_CONSUME] where [SEQUENCE_NO] =@groupID
				select @dock=max(isnull(dock,'')),@plant_zone=max(isnull(PLANT_ZONE,''))
					FROM [LES].[TI_TWD_MATERIAL_CONSUME] where scan_group_id =@groupID and PLANT=@plant and ASSEMBLY_LINE=@AssemblyLine and SUPPLIER_NUM=@supplier and BOX_PARTS=@PARTBOX
				SELECT @dockName=[DOCK_NAME] FROM [LES].[TM_BAS_DOCK] where PLANT = @plant and ASSEMBLY_LINE = @assemblyLine and [DOCK] = @dock
				select @DELIVERY_LOCATION=WAREHOUSE,@transSupplierNum=[TRANS_SUPPLIER_NUM],@transportTime=[TRANSPORT_TIME],@BoxState=isnull([BOX_PARTS_STATE],1),@isorganizesheet=isnull([IS_ORGANIZE_SHEET],0) from LES.TM_TWD_BOX_PARTS 
    				where PLANT = @plant and ASSEMBLY_LINE = @assemblyLine and SUPPLIER_NUM = @supplier and BOX_PARTS = @PARTBOX
			
				--select @plant_zone=[PLANT_ZONE] from [LES].[TM_BAS_ASSEMBLY_LINE] where [ASSEMBLY_LINE]=@assemblyLine
			
			--print '2'
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
    				,@plant --<PLANT, nvarchar(5),>
    				,@assemblyLine --<ASSEMBLY_LINE, nvarchar(10),>
    				,null --<WORKSHOP, nvarchar(4),>
    				,@plant_zone --<PLANT_ZONE, nvarchar(5),>
    				,getdate() --<PUBLISH_TIME, datetime,>
    				,@runSheetType  --<RUNSHEET_TYPE, int,>
    				,@supplier  --<SUPPLIER_NUM, nvarchar(12),>
    				,0 --尚不明确含义<SUPPLIER_SN, int,>
    				,@dockName--<DOCK, nvarchar(10),>
    				,@DELIVERY_LOCATION --<DELIVERY_LOCATION, nvarchar(50),>
    				,@PARTBOX --<BOX_PARTS, nvarchar(10),>
    				,0 --尚不明确含义<PART_TYPE, int,>
    				,null --<UNLOADING_TIME, int,>
    				,@expectedarrivaltime --<EXPECTED_ARRIVAL_TIME, datetime,>
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
    				,@BoxState --<WMS_SEND_STATUS, int,>
    				,@remark --<COMMENTS, nvarchar(200),>
    				,null --<UPDATE_DATE, datetime,>
    				,null --<UPDATE_USER, nvarchar(50),>
    				,getdate() --<CREATE_DATE, datetime,>
    				,@CreateUser--<CREATE_USER, nvarchar(50),>
    				)
    			--   print @dock
    			declare @runsheetId int
    			select @runsheetId = scope_identity()
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
							,MANUAL_LOCATION)
    					select
    						@runsheetId--(<TWD_RUNSHEET_SN, int,>
    						,@plant --<PLANT, nvarchar(5),>
    						,@assemblyLine --<ASSEMBLY_LINE, nvarchar(10),>
    						,@supplier --<SUPPLIER_NUM, nvarchar(12),>
    						,PART_NO --, nvarchar(20),>
    						,INDENTIFY_PART_NO --, nvarchar(20),>
    						,PART_CNAME --, nvarchar(100),>
    						,PART_ENAME --, nvarchar(100),>
    						,@dock--<DOCK, nvarchar(10),>
    						,@PARTBOX --<BOX_PARTS, nvarchar(10),>
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
							,LOCATION
    					from LES.TI_TWD_MATERIAL_CONSUME 
    					where [IS_ORGANIZE_SHEET] =2 and
    						scan_group_id =@groupID
							and PLANT=@plant and ASSEMBLY_LINE=@AssemblyLine and SUPPLIER_NUM=@supplier and BOX_PARTS=@PARTBOX

    					group by PART_NO,INDENTIFY_PART_NO,PART_CNAME,PART_ENAME,INBOUND_PACKAGE,MEASURING_UNIT_NO,INBOUND_PACKAGE_MODEL,LOCATION--相同零件多个需求合并到一个明细中
				
    					--设置需求表中状态
    					update LES.TI_TWD_MATERIAL_CONSUME 
    					set [IS_ORGANIZE_SHEET]=1
    					where [IS_ORGANIZE_SHEET] =2 and 
    					scan_group_id =@groupID					
						and PLANT=@plant and ASSEMBLY_LINE=@AssemblyLine and SUPPLIER_NUM=@supplier and BOX_PARTS=@PARTBOX
					--生成BARCODE
					exec LES.PROC_TWD_GENERATE_BARCODE @runsheetId
					--创建仓库出库单
					exec LES.PROC_INSERT_OUTPUT @runsheetId,'TWD'

				fetch next from consume_crsr into @plant,@AssemblyLine,@supplier,@PARTBOX,@expectedarrivaltime
			end
			close consume_crsr
			deallocate consume_crsr

		--add by【运维】xhm 2014/08/04【CMDB编号：CR-LES-20140807】start
		--TWD拉动单编码的序号问题,使用事务并捕捉异常 
		commit transaction
		end try
		begin catch
		--出错，则返回执行不成功，回滚事务
		rollback transaction
		--记录错误信息
		insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, [application], [METHOD], class,  exception_message, error_code)
		select getdate(),'PCS','PROC_TWD_GENERATE_MANUAL_RUNSHEET','Procedure',error_message(),ERROR_LINE()

		end catch	
		--add by【运维】xhm 2014/08/04 end	
end