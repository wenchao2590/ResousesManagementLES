-- =============================================
-- Author:		Scott
-- Create date: 2017-9-12
-- Description:	调整库存存储过程
-- =============================================
CREATE PROCEDURE [dbo].[PROC_WMM_STOCK_ADJUST_CREATE] 
	@partNo nvarchar(20),
	@plant nvarchar(5),
	@wmNo nvarchar(10),
	@zoneNo nvarchar(20),
	@dloc nvarchar(20),
	@num numeric(18,2),
	@supplierNum nvarchar(12),
	@batchNo nvarchar(100),
	@barCodeData nvarchar(50),
	@barCodeType nvarchar(10),
	@userName nvarchar(50)
AS
BEGIN
	if @dloc is null
	select @dloc=DLOC from [LES].[TM_BAS_PARTS_STOCK] where PART_NO=@partNo and PLANT=@plant and WM_NO=@wmNo and ZONE_NO=@zoneNo and SUPPLIER_NUM=@supplierNum;

	declare @stockIdentity int=null;

	select @stockIdentity=[STOCK_IDENTITY] from [LES].[TT_WMS_STOCKS] where  PART_NO=@partNo and PLANT=@plant and WM_NO=@wmNo and ZONE_NO=@zoneNo and DLOC=@dloc and SUPPLIER_NUM=@supplierNum;

	if @stockIdentity is not null
	begin
		update [LES].[TT_WMS_STOCKS] 
		set 
			[STOCKS_NUM]=isnull([STOCKS_NUM],0)+@num
			,[AVAILBLE_STOCKS] = isnull([STOCKS_NUM],0)+@num- ISNULL([FROZEN_STOCKS] , 0)
			,[STOCKS]=(case when [PACKAGE] is null or [PACKAGE]=0 then 0 else (case when [STOCKS_NUM]+@num>=0 then ceiling(cast(isnull([STOCKS_NUM],0)+@num as float)/cast([PACKAGE] as float)) else floor(cast(isnull([STOCKS_NUM],0)+@num as float)/cast([PACKAGE] as float)) end) end)
			,[FRAGMENT_NUM] =(isnull([STOCKS_NUM],0)+@num- ISNULL([FROZEN_STOCKS] , 0)) % ISNULL([PACKAGE] , 1)
			,[UPDATE_USER]=@userName
			,[UPDATE_DATE]=GETDATE()
		where
			[STOCK_IDENTITY]=@stockIdentity
	end
	else
	begin
		if @num>=0
		begin
			insert into [LES].[TT_WMS_STOCKS]
			(	
				[PLANT]
                ,[ASSEMBLY_LINE]
                ,[PLANT_ZONE]
                ,[WORKSHOP]
                ,[SUPPLIER_NUM]
                ,[PART_NO]
                ,[PART_CNAME]
                ,[PART_ENAME]
                ,[PART_NICKNAME]
                ,[PART_UNITS]
                ,[PACKAGE_MODEL]
                ,[PACKAGE]
                ,[LOGICAL_PK]
                ,[DELETE_FLAG]
                ,[ROUTE]
                ,[ZONE_NO]
                ,[WM_NO]
                ,[OCCUPY_AREA]
                ,[DLOC]
                ,[MAX]
                ,[MIN]
                ,[ROW_NUMBER]
                ,[LINE_NUMBER]
                ,[HIGH_NUMBER]
                ,[MATERIAL_GROUP]
                ,[KEEPER]
                ,[TRANSER]
                ,[INFORMATIONER]
                ,[ELOC]
                ,[SAFE_STOCK]
                ,[STOCKS]
                ,[FROZEN_STOCKS]
                ,[AVAILBLE_STOCKS]
                ,[IS_BATCH]
                ,[WMS_RULE]
                ,[COUNTER]
                ,[FRAGMENT_NUM]
                ,[STOCKS_NUM]
                ,[PART_WEIGHT]
                ,[PART_CLS]
                ,[IS_REPACK]
                ,[REPACK_ROUTE]
                ,[IS_TRIGGER_PULL]
                ,[TRIGGER_WM_NO]
                ,[TRIGGER_ZONE_NO]
                ,[TRIGGER_DLOC]
                ,[EMG_TIME]
                ,[SUPPER_ZONE_DLOC]
                ,[CHECK_TYPE]
                ,[BUSINESS_PK]
                ,[BATCH_NO]
                ,[BARCODE_DATA]
                ,[BARCODE_TYPE]
                ,[COMMENTS]
                ,[CREATE_USER]
                ,[CREATE_DATE]
			) 
			SELECT
			top 1
			@plant,
			NULL,--ASSEMBLY_LINE
			NULL,--PLANT_ZONE
			NULL,--WORKSHOP
			@supplierNum,
			@partNo,
			PART_CNAME,
			NULL,--PART_ENAME
			PART_NICKNAME,
			PART_UNITS,
			PACKAGE_MODEL,
			PACKAGE,
			NULL,--LOGICAL_PK
			NULL,--DELETE_FLAG
			NULL,--ROUTE
			@zoneNo,
			@wmNo,
			NULL,--OCCUPY_AREA
			@dloc,
			MAX,
			MIN,
			NULL,--ROW_NUMBER
			NULL,--LINE_NUMBER
			NULL,--HIGH_NUMBER
			NULL,--MATERIAL_GROUP
			NULL,--KEEPER
			NULL,--TRANSER
			NULL,--INFORMATIONER
			NULL,--ELOC
			0,--SAFE_STOCK
			(case when [PACKAGE] is null or [PACKAGE]=0 then 0 else (case when @num>=0 then ceiling(cast(@num as float)/cast([PACKAGE] as float)) else floor(cast(@num as float)/cast([PACKAGE] as float)) end) end),--STOCKS
			0,--FROZEN_STOCKS
			@num,--AVAILBLE_STOCKS
			IS_BATCH,
			NULL,--WMS_RULE
			NULL,--COUNTER
			@num%isnull(package,1),--FRAGMENT_NUM
			@num,--STOCKS_NUM
			NULL,--PART_WEIGHT
			PART_CLS,
			NULL,--IS_REPACK
			NULL,--REPACK_ROUTE
			NULL,--IS_TRIGGER_PULL
			NULL,--TRIGGER_WM_NO
			NULL,--TRIGGER_ZONE_NO
			NULL,--TRIGGER_DLOC
			NULL,--EMG_TIME
			NULL,--SUPPER_ZONE_DLOC
			NULL,--CHECK_TYPE
			NULL,--BUSINESS_PK
			@batchNo,
			@barCodeData,
			@barCodeType,
			NULL,--COMMENTS
			@userName,
			GETDATE()
			from  LES.TM_BAS_PARTS_STOCK
			where PART_NO=@partNo and PLANT=@plant and WM_NO=@wmNo and ZONE_NO=@zoneNo and DLOC=@dloc and SUPPLIER_NUM=@supplierNum
		end
	end
END