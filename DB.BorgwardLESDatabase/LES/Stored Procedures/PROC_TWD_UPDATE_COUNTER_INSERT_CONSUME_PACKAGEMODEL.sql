
-- =============================================
-- Author:		Caodaowei
-- Create date: 2014-10-10
-- Description:	更新计数器(组单零件类)
-- =============================================
create PROC [LES].[PROC_TWD_UPDATE_COUNTER_INSERT_CONSUME_PACKAGEMODEL]
(

 @plant nvarchar(20)
 ,@assemblyLine nvarchar(20)
 ,@boxPart nvarchar(20)
 ,@supplier nvarchar(20)
 ,@dock nvarchar(20)
)
as

	set nocount on
	--exec LES.PROC_TWD_PREPARE_QUOTA --准备QUOTA信息
	--插入消耗-针对没有托盘的数据
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
		   ,INHOUSE_PACKAGE_MODEL
           )
	SELECT 
		c.[PLANT_ZONE] ,
		c.[WORKSHOP] ,
		c.[ASSEMBLY_LINE] ,
		c.[PLANT] ,
		null,
		getdate(),
		6,
		null,
		[PART_NO] ,
		[INDENTIFY_PART_NO] ,
		[PART_CNAME] ,
		[PART_ENAME] ,
		B.SUPPLIER_NUM,
		@dock, --DOCK
		[INBOUND_PART_CLASS] ,
		0,--[INTERFACE_TYPE]
		ceiling( convert(numeric(18,2),[CURRENT_PART_COUNT])/isnull([INBOUND_PACKAGE],99999)) * isnull([INBOUND_PACKAGE],99999),
		[INBOUND_PACKAGE_MODEL] ,
		isnull([INBOUND_PACKAGE],99999) ,
		[MEASURING_UNIT_NO],
		--null,
		--null,
		--null,
		--null,
		2,
		--0,
		--null,
		--0,
		--null,
		--null,
		getdate(),
		'twd consume'
		--''
		,C.INHOUSE_PACKAGE_MODEL
	from [LES].[TT_TWD_CONSUME_COUNTER] c inner join LES.TM_TWD_BOX_PARTS b
	on c.PLANT = b.PLANT and c.ASSEMBLY_LINE = b.ASSEMBLY_LINE and c.INBOUND_PART_CLASS = b.BOX_PARTS
 	 where c.PLANT = @plant 
		 and c.ASSEMBLY_LINE  = @assemblyLine 
		 and CURRENT_PART_COUNT >0
		 and INHOUSE_PACKAGE_MODEL = @boxPart --and C.SUPPLIER_NUM=@supplier
		 and isnull(c.PALLET_PACKAGE,0)=0

	 
		 
	--更新计数器--针对没有托盘的数据
	update [LES].[TT_TWD_CONSUME_COUNTER] 
		set CURRENT_PART_COUNT = CURRENT_PART_COUNT -convert(numeric(18,2), ceiling( convert(numeric(18,2),[CURRENT_PART_COUNT])/[INBOUND_PACKAGE]) * [INBOUND_PACKAGE])
		from [LES].[TT_TWD_CONSUME_COUNTER] c inner join LES.TM_TWD_BOX_PARTS b
		on c.PLANT = b.PLANT and c.ASSEMBLY_LINE = b.ASSEMBLY_LINE and c.INBOUND_PART_CLASS = b.BOX_PARTS
 		 where c.PLANT = @plant 
			 and c.ASSEMBLY_LINE  = @assemblyLine 
			 and CURRENT_PART_COUNT >0
			 and INHOUSE_PACKAGE_MODEL = @boxPart   --and C.SUPPLIER_NUM=@supplier
			  and isnull(c.PALLET_PACKAGE,0)=0

--插入消耗-针对有托盘的数据
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
           ,INHOUSE_PACKAGE_MODEL
           )
	SELECT 
		c.[PLANT_ZONE] ,
		c.[WORKSHOP] ,
		c.[ASSEMBLY_LINE] ,
		c.[PLANT] ,
		null,
		getdate(),
		6,
		null,
		[PART_NO] ,
		[INDENTIFY_PART_NO] ,
		[PART_CNAME] ,
		[PART_ENAME] ,
		B.SUPPLIER_NUM,
		@dock, --DOCK
		[INBOUND_PART_CLASS] ,
		0,--[INTERFACE_TYPE]
		ceiling( convert(numeric(18,3),[CURRENT_PART_COUNT])/isnull([PALLET_COUNT],99999)) * isnull([PALLET_COUNT],99999),
		[INBOUND_PACKAGE_MODEL] ,
		isnull([INBOUND_PACKAGE],99999) ,
		[MEASURING_UNIT_NO],
		--null,
		--null,
		--null,
		--null,
		2,
		--0,
		--null,
		--0,
		--null,
		--null,
		getdate(),
		'twd consume',
		--''
		C.INHOUSE_PACKAGE_MODEL
	from [LES].[TT_TWD_CONSUME_COUNTER] c inner join LES.TM_TWD_BOX_PARTS b
	on c.PLANT = b.PLANT and c.ASSEMBLY_LINE = b.ASSEMBLY_LINE and c.INBOUND_PART_CLASS = b.BOX_PARTS
 	 where c.PLANT = @plant 
		 and c.ASSEMBLY_LINE  = @assemblyLine 
		 and CURRENT_PART_COUNT >0
		 and INHOUSE_PACKAGE_MODEL = @boxPart   --and C.SUPPLIER_NUM=@supplier
		  and isnull(c.PALLET_PACKAGE,0)>0

	--更新计数器--针对有托盘的数据
	update [LES].[TT_TWD_CONSUME_COUNTER] 
		set CURRENT_PART_COUNT = CURRENT_PART_COUNT - convert(numeric(18,2),ceiling( convert(numeric(18,3),[CURRENT_PART_COUNT])/isnull(c.[PALLET_COUNT],99999)) * isnull(c.[PALLET_COUNT],99999))
		from [LES].[TT_TWD_CONSUME_COUNTER] c inner join LES.TM_TWD_BOX_PARTS b
		on c.PLANT = b.PLANT and c.ASSEMBLY_LINE = b.ASSEMBLY_LINE and c.INBOUND_PART_CLASS = b.BOX_PARTS
 		 where c.PLANT = @plant 
			 and c.ASSEMBLY_LINE  = @assemblyLine 
			 and CURRENT_PART_COUNT>0
			 and INHOUSE_PACKAGE_MODEL = @boxPart   --and C.SUPPLIER_NUM=@supplier
			 and isnull(c.PALLET_PACKAGE,0)>0