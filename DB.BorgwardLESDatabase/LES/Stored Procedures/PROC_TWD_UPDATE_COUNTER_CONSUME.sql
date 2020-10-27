

-- =============================================
-- Author:		吕小望
-- Create date: 2011-10-11
-- Description:	计算TWD零件消耗
-- =============================================
CREATE procedure [LES].[PROC_TWD_UPDATE_COUNTER_CONSUME] 
as
    set nocount on
    declare @rowCount int
	select @rowCount = count(1) from [LES].[TE_TWD_MATERIAL_CONSUME_TEMP]
	print @rowCount 
	declare @flag int
	set @flag = 1
	declare @updateCount int
	
	declare @plant nvarchar(5)
	declare @assemblyLine nvarchar(10)
	declare @partNo nvarchar(20)
	declare @packCount int
	while( @flag <= @rowCount )
	begin
		select @plant = PLANT , @assemblyLine = ASSEMBLY_LINE, @partNo = PART_NO ,@packCount = PACK_COUNT
			from [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] 
			where ID = @flag
		--print @plant
		--print @assemblyLine
		--print @partNo
		--print @packCount
		
		update [LES].[TT_TWD_CONSUME_COUNTER]
			set CURRENT_PART_COUNT = CURRENT_PART_COUNT + @packCount
			where PLANT = @plant and ASSEMBLY_LINE = @assemblyLine and PART_NO = @partNo 
		set @updateCount = @@rowCount
		if(@updateCount <> 1)
		begin
			print 'error'
			--updatecount 为0 的时候说明计数表中缺少数据
			
			--updateCount 大于1的时候说明计数表中有重复数据		
		end

		set @flag = @flag +1
	end
  
	--SELECT * FROM [LES].[TT_TWD_CONSUME_COUNTER] WHERE CURRENT_PART_COUNT > 0 
	
	--第二部分完成消耗生成
    CREATE TABLE #TEMP_TWD_CONSUME_COUNTER(
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[COUNTER_ID] [INT] NOT NULL,
	[PLANT] [nvarchar](5) NOT NULL,
	[ASSEMBLY_LINE] [nvarchar](10) NULL,
	[PLANT_ZONE] [nvarchar](5) NULL,
	[WORKSHOP] [nvarchar](4) NULL,	
	[PART_NO] [nvarchar](20) NOT NULL,
	[INDENTIFY_PART_NO] [nvarchar](20) NULL,
	[INBOUND_PART_CLASS] [nvarchar](10) NULL,
	[PART_CNAME] [nvarchar](100) NULL,
	[PART_ENAME] [nvarchar](100) NULL,
	[DOCK] [nvarchar](10) NULL,
	[INHOUSE_PACKAGE_MODEL] [nvarchar](30) NULL,
	[INHOUSE_PACKAGE] [int] NULL,
	[INBOUND_PACKAGE_MODEL] [nvarchar](30) NULL,
	[INBOUND_PACKAGE] [int] NULL,
	[CURRENT_PART_COUNT] [int] NULL,
	[PACKAGE_COUNT] [int] NULL,
	[MEASURING_UNIT_NO] [nvarchar](1) NULL
	) 
	
	insert into #TEMP_TWD_CONSUME_COUNTER
	( 	
	[COUNTER_ID],
	[PLANT] ,
	[ASSEMBLY_LINE] ,
	[PLANT_ZONE] ,
	[WORKSHOP] ,
	[PART_NO] ,
	[INDENTIFY_PART_NO] ,
	[INBOUND_PART_CLASS] ,
	[PART_CNAME] ,
	[PART_ENAME] ,
	[DOCK] ,
	[INHOUSE_PACKAGE_MODEL] ,
	[INHOUSE_PACKAGE] ,
	[INBOUND_PACKAGE_MODEL] ,
	[INBOUND_PACKAGE],
	[CURRENT_PART_COUNT] ,
	[PACKAGE_COUNT],
	[MEASURING_UNIT_NO]
	)
	select 
	[ID],
	[PLANT] ,
	[ASSEMBLY_LINE] ,
	[PLANT_ZONE] ,
	[WORKSHOP] ,
	[PART_NO] ,
	[INDENTIFY_PART_NO] ,
	[INBOUND_PART_CLASS] ,
	[PART_CNAME] ,
	[PART_ENAME] ,
	[DOCK] ,
	[INHOUSE_PACKAGE_MODEL] ,
	[INHOUSE_PACKAGE] ,
	[INBOUND_PACKAGE_MODEL] ,
	[INBOUND_PACKAGE],
	[CURRENT_PART_COUNT],
	0,
	[MEASURING_UNIT_NO]
	from [LES].[TT_TWD_CONSUME_COUNTER] where CURRENT_PART_COUNT > 0 
	
	declare @inboundPackage int
	declare @counterId int
	declare @currentCount int
	declare @needPackageCount int --需求包装数
	declare @needPartCount int --零件数
	select @rowCount = count(1) from #TEMP_TWD_CONSUME_COUNTER
	set @flag = 1
	while @flag <= @rowCount
	begin
		--print 'loop'
		select @counterId = [COUNTER_ID], @inboundPackage = [INBOUND_PACKAGE] , @currentCount = [CURRENT_PART_COUNT]
			from #TEMP_TWD_CONSUME_COUNTER
			where [ID] = @flag
		if(@inboundPackage<=0)
		begin
			--这里除数错误
			print '除数错误'
		end
		set @needPackageCount = ceiling( convert(numeric(18,3),@currentCount)/@inboundPackage)
		set @needPartCount = @needPackageCount * @inboundPackage
		--print @needPackageCount
		--print @needPartCount
		update [LES].[TT_TWD_CONSUME_COUNTER]
			set [CURRENT_PART_COUNT] = [CURRENT_PART_COUNT] - @needPartCount
			where ID = @counterId
			
		update #TEMP_TWD_CONSUME_COUNTER
			set [PACKAGE_COUNT] = @needPartCount where ID = @flag
		set @flag = @flag +1
		
		--insert consume

	end 
	
	INSERT INTO [LES].[TI_TWD_MATERIAL_CONSUME]
           ([PLANT_ZONE]
           ,[WORKSHOP]
           ,[ASSEMBLY_LINE]
           ,[PLANT]
           ,[LOCATION]
           ,[REQUEST_TIME]
           ,[INTERFACE_STATUS]
           ,[PROCESS_TIME]
           ,[PART_NO]
           ,[INDENTIFY_PART_NO]
           ,[PART_CNAME]
           ,[PART_ENAME]
           ,[SUPPLIER_NUM]
           ,[DOCK]
           ,[BOX_PARTS]
           ,[INTERFACE_TYPE]
           ,[PACK_COUNT]
           ,[INBOUND_PACKAGE_MODEL]
           ,[INBOUND_PACKAGE]
           ,[MEASURING_UNIT_NO]
           --,[EXPECTED_ARRIVAL_TIME]
           --,[RDC_DLOC]
           --,[PICKUP_SEQ_NO]
           --,[SEQUENCE_NO]
           ,[IS_ORGANIZE_SHEET]
           --,[SEND_STATUS]
           --,[SEND_TIME]
           --,[IS_CANCEL]
           --,[UPDATE_DATE]
           --,[UPDATE_USER]
           ,[CREATE_DATE]
           ,[CREATE_USER]
           --,[COMMENTS]
           )
	SELECT 
		[PLANT_ZONE] ,
		[WORKSHOP] ,
		[ASSEMBLY_LINE] ,
		[PLANT] ,
		null,
		getdate(),
		6,
		null,
		[PART_NO] ,
		[INDENTIFY_PART_NO] ,
		[PART_CNAME] ,
		[PART_ENAME] ,
		-1 ,--SUPPLIER NUM
		'DOCK', --DOCK
		[INBOUND_PART_CLASS] ,
		0,--[INTERFACE_TYPE]
		[PACKAGE_COUNT],
		[INBOUND_PACKAGE_MODEL] ,
		[INBOUND_PACKAGE] ,
		[MEASURING_UNIT_NO],
		--null,
		--null,
		--null,
		--null,
		1,
		--0,
		--null,
		--0,
		--null,
		--null,
		getdate(),
		'twd consume'
		--''
		from #TEMP_TWD_CONSUME_COUNTER
	
	drop table #TEMP_TWD_CONSUME_COUNTER