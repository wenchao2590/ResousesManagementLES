-- =============================================
-- Author:		Scott
-- Create date: 2017/9/21
-- Description:	RDC仓库收货后，更新寄售订单收货数据和状态，更新VMI出库数据
-- =============================================
CREATE PROCEDURE [LES].[PROC_SPM_DELIVERY_RUNSHEET_UPDATE]
	@RECEIVE_ID INT,
	@LOGIN_USERNAME NVARCHAR(50)
AS
BEGIN
	begin tran
	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] with(tablock,xlock) set [CREATE_DATE]=[CREATE_DATE]
	where 1<>1

	declare @RECEIVE_NO nvarchar(50);
	
	select @RECEIVE_NO=RECEIVE_NO from [LES].[TT_WMM_RECEIVE] with(nolock) where RECEIVE_ID=@RECEIVE_ID

	truncate table [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP];

	--如果VMI出库寄售订单临时表中有遗留的脏数据就先删除
	delete from [LES].[TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP] where [RECEIVE_ID]=@RECEIVE_ID

	--按照RDC收货单中的零件号，统计收货件数到临时表
	insert into [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]([PART_NO],[STOCKS_NUM],[VALID_FLAG],[UPDATE_FLAG],[CREATE_DATE],[CREATE_USER])
	select [PART_NO],ISNULL(sum(ACTUAL_QTY),0),1,0,getdate(),@LOGIN_USERNAME from [LES].[TT_WMM_RECEIVE_DETAIL] with(nolock)
	where [RECEIVE_ID]=@RECEIVE_ID
	group by [PART_NO]
	
	--循环检查临时表中对应寄售订单的信息
	while 1=1
	begin
		update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
		set UPDATE_FLAG=0;

		--匹配寄售订单明细信息
		update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
		set [RUNSHEET_DETAIL_ID]=PD.DETAIL_ID
		from
		[LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
		inner join
		(
			select 
				SDRD.PART_NO,
				min(SDRD.RUNSHEET_DETAIL_ID) as DETAIL_ID
			from
				[LES].[TT_SPM_DELIVERY_RUNSHEET] as SDR with(nolock)
				inner join 
				[LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL] as SDRD with(nolock)
				on SDR.[PLAN_RUNSHEET_SN]=SDRD.[PLAN_RUNSHEET_SN]
					and
				   SDR.RUNSHEET_TYPE=-50 --寄售订单
					and
				   SDR.[SHEET_STATUS]<>10 --寄售订单未关单
					and
				   ISNULL(SDRD.REQUIRED_INHOUSE_PACKAGE,0)>ISNULL(SDRD.ACTUAL_RDC_INHOUSE_PACKAGE_QTY,0)--该零件没有全部入库RDC仓库
				    and
				   ISNULL(SDRD.ACTUAL_INHOUSE_PACKAGE_QTY,0)>ISNULL(SDRD.ACTUAL_RDC_INHOUSE_PACKAGE_QTY,0)--该零件的VMI仓库入库数大于RDC仓库入库数
			group by 
				SDRD.PART_NO
		) as PD 
		on TEMP.PART_NO=PD.PART_NO
		where STOCKS_NUM>0
		
		update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
		set 
			ERROR_MSG=N'零件号【'+PART_NO+N'】无法找到对应的寄售订单信息'
			,VALID_FLAG=0
		where [RUNSHEET_DETAIL_ID] is null and STOCKS_NUM>0

		--检查是否有零件没有检查到寄售订单信息，如果有就返回错误信息，存储过程结束
		if exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0)
		begin
			select PART_NO,ERROR_MSG
			from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
			where VALID_FLAG=0

			rollback;
			return
		end

		--根据寄售订单中已入库RDC的零件件数，计算出即将更新到寄售订单的零件件数
		update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
		set 
		DEAL_NUM=case when 
					ISNULL(DETAIL.REQUIRED_INHOUSE_PACKAGE,0)-ISNULL(DETAIL.ACTUAL_RDC_INHOUSE_PACKAGE_QTY,0)<=ISNULL(TEMP.STOCKS_NUM,0)
				 then
					ISNULL(DETAIL.REQUIRED_INHOUSE_PACKAGE,0)-ISNULL(DETAIL.ACTUAL_RDC_INHOUSE_PACKAGE_QTY,0)
				 else
					ISNULL(TEMP.STOCKS_NUM,0)
				 end
		from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
		inner join [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL] as DETAIL with(nolock)
		on TEMP.RUNSHEET_DETAIL_ID=DETAIL.RUNSHEET_DETAIL_ID
		where STOCKS_NUM>0

		
		--插入寄售订单更新信息到VMI出库寄售订单临时表，以便后面同步到SAP接口表
		insert into [LES].[TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP]
		(PART_NO,ITEM_NO,RUNSHEET_NO,RECEIVE_ID,STOCK_NUM,CREATE_USER,CREATE_DATE,LFART)
		select
		TEMP.PART_NO,DETAIL.ITEM_NO,SHEET.PLAN_RUNSHEET_NO,@RECEIVE_ID,ISNULL(TEMP.DEAL_NUM,0),@LOGIN_USERNAME,GETDATE(),SHEETIN.LFART
		from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
		inner join [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL] as DETAIL with(nolock) on TEMP.RUNSHEET_DETAIL_ID=DETAIL.RUNSHEET_DETAIL_ID and TEMP.STOCKS_NUM>0
		inner join [LES].[TT_SPM_DELIVERY_RUNSHEET] as SHEET with(nolock) on DETAIL.PLAN_RUNSHEET_SN=SHEET.PLAN_RUNSHEET_SN
		INNER JOIN LES.TI_SPM_DELIVERY_RUNSHEET_IN AS SHEETIN with(nolock) ON SHEET.PLAN_RUNSHEET_NO = SHEETIN.VBELN


		--更新寄售订单已入库RDC件数
		update [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL]
		set 
			ACTUAL_RDC_INHOUSE_PACKAGE_QTY=ISNULL(ACTUAL_RDC_INHOUSE_PACKAGE_QTY,0)+ISNULL(TEMP.DEAL_NUM,0)
		from [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL] as DETAIL
			inner join 
			[LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
			on DETAIL.RUNSHEET_DETAIL_ID=TEMP.RUNSHEET_DETAIL_ID and TEMP.STOCKS_NUM>0
		
		--将已经更新到寄售订单的零件件数从临时表STOCKS_NUM列中扣除
		update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
		set STOCKS_NUM=ISNULL(STOCKS_NUM,0)-ISNULL(DEAL_NUM,0)
		,UPDATE_FLAG=1
		where STOCKS_NUM>0
		
		--检查是否所有收货单的信息都更新到了寄售订单，如果是就退出循环
		if not exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where STOCKS_NUM>0)
		begin
			break;
		end

		--将RUNSHEET_DETAIL_ID重新置为null，继续循环匹配STOCKS_NUM>0的数据到寄售订单
		update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
		set RUNSHEET_DETAIL_ID=null;

		
	end

	--关闭已经全部入库RDC的寄售订单
	update [LES].[TT_SPM_DELIVERY_RUNSHEET]
	set SHEET_STATUS=10 
	where 
	not exists ( select 1 from [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL] where [TT_SPM_DELIVERY_RUNSHEET_DETAIL].PLAN_RUNSHEET_SN=[TT_SPM_DELIVERY_RUNSHEET].PLAN_RUNSHEET_SN and ISNULL([TT_SPM_DELIVERY_RUNSHEET_DETAIL].REQUIRED_INHOUSE_PACKAGE,0)<>ISNULL([TT_SPM_DELIVERY_RUNSHEET_DETAIL].ACTUAL_RDC_INHOUSE_PACKAGE_QTY,0))
	and
	SHEET_STATUS<>10

	truncate table [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]

	--重新插入收货数据到TEMP表，准备做VMI仓库库存扣减
	insert into [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	(PART_NO,SUPPLIER_NUM,STOCKS_NUM, VALID_FLAG, CREATE_USER, CREATE_DATE)
	select PART_NO,REC.SUPPLIER_NUM,ISNULL(sum(DETAIL.ACTUAL_QTY),0) as stock_num,1,@LOGIN_USERNAME,GETDATE() 
	from [LES].[TT_WMM_RECEIVE_DETAIL] as DETAIL with(nolock)
	inner join 
	[LES].[TT_WMM_RECEIVE] as REC with(nolock)
	on DETAIL.RECEIVE_ID=REC.RECEIVE_ID
	where
	DETAIL.RECEIVE_ID=@RECEIVE_ID
	group by PART_NO,REC.SUPPLIER_NUM


	--检查收货单中的零件号对应的供应商是否是外部供应商
	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	set VALID_FLAG=0
	,ERROR_MSG=N'零件号【'+PART_NO+N'】对应的供应商【'+ISNULL(TEMP.SUPPLIER_NUM,N'')+N'】在供应商主数据表中找不到对应数据，或该供应商不是外部供应商'
	from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
	left join
	[LES].[TM_BAS_SUPPLIER] as SP
	on TEMP.SUPPLIER_NUM=SP.SUPPLIER_NUM and SP.[SUPPLIER_TYPE]=1--SP.[SUPPLIER_TYPE]=1表示外部供应商
	where SP.SUPPLIER_NUM is null--SP.SUPPLIER_NUM为null表示有零件号对应的供应商不是外部供应商

	--如果有零件号对应的供应商不是外部供应商，那么返回错误信息，存储过程结束
	if exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0)
	begin
		select PART_NO,ERROR_MSG from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0
		
		rollback;
		return;
	end


	--更新VMI出库库存存储区信息
	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	set D_ZONE_NO=VS.ZONE_NO
		,ZONE_NO=VS.ZONE_NO
		,D_WM_NO=VS.WM_NO
		,WM_NO=VS.WM_NO
		,D_PLANT=VS.PLANT
		,PLANT=VS.PLANT
	from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
	inner join 
	(
		select 
		VS.*
		from
		[LES].[TM_BAS_VMI_SUPPLIER] as VS with(nolock)
		inner join 
		[LES].[TM_WMM_ZONES] as ZONES with(nolock)
		on VS.ZONE_NO=ZONES.ZONE_NO AND ZONES.IS_MANAGE=10--VMI出库，只能从合格品存储区出库
	) as VS
	on TEMP.SUPPLIER_NUM=VS.SUPPLIER_NUM

	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	set VALID_FLAG=0
	,ERROR_MSG=N'零件号【'+PART_NO+N'】找不到对应的VMI仓库合格品存储区信息'
	where D_ZONE_NO is null

	--如果有零件找不到VMI存储区信息那么返回错误信息，存储过程结束
	if exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0)
	begin
		select PART_NO,ERROR_MSG from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0
		
		rollback;
		return;
	end

	--检查VMI出库仓库是否启用了VMI库存管理
	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	set VALID_FLAG=0
	,ERROR_MSG=N'零件号【'+PART_NO+N'】对应的VMI仓库【'+WM_NO+N'】，没有启用VMI库存管理'
	from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
	inner join 
	LES.TM_BAS_WAREHOUSE as WS
	on TEMP.WM_NO=WS.WAREHOUSE and WS.VMI_ENABLE<>1

	--如果有零件对应的VMI仓库没有启用VMI库存管理，那么返回错误信息，存储过程结束
	if exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0)
	begin
		select PART_NO,ERROR_MSG from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0
		
		rollback;
		return;
	end
	
	--更新VMI出库库位信息
	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	set D_DLOC=PS.DLOC
		,DLOC=PS.DLOC
		,STOCKS=case when PS.PACKAGE is null or PS.PACKAGE=0
				then 
					0
				else
					case when ISNULL(STOCKS_NUM,0)>0
					then 
						CEILING(CAST(STOCKS_NUM as float)/CAST(PS.PACKAGE as float))
					else
						FLOOR(CAST(STOCKS_NUM as float)/CAST(PS.PACKAGE as float))
					end
				end
	from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
	inner join [LES].[TM_BAS_PARTS_STOCK] as PS
	on TEMP.PART_NO=PS.PART_NO 
	   and
	   TEMP.D_PLANT=PS.PLANT
	   and
	   TEMP.D_WM_NO=PS.WM_NO
	   and
	   TEMP.D_ZONE_NO=PS.ZONE_NO
	   and
	   TEMP.SUPPLIER_NUM=PS.SUPPLIER_NUM

	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	set VALID_FLAG=0
	,ERROR_MSG=N'零件号【'+PART_NO+N'】找不到对应的VMI仓库合格品库位信息'
	where D_DLOC is null

	--如果有零件找不到VMI库位信息那么返回错误信息，存储过程结束
	if exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0)
	begin
		select PART_NO,ERROR_MSG from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0
		
		rollback;
		return;
	end

	--检查时候所有数据能够匹配到VMI库存信息
	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	set VALID_FLAG=0
		,ERROR_MSG=N'零件【'+TEMP.PART_NO+N'】找不到对应的VMI库存数据'
	from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
		 left join 
	     [LES].[TT_WMS_STOCKS] as WS with(nolock)
		 on WS.[PART_NO]=TEMP.[PART_NO]
		    and
			WS.[PLANT]=TEMP.D_PLANT
			and
			WS.WM_NO=TEMP.D_WM_NO
			and
			WS.ZONE_NO=TEMP.D_ZONE_NO
			and
			WS.DLOC=TEMP.D_DLOC
			and
			TEMP.SUPPLIER_NUM=WS.SUPPLIER_NUM
    where WS.PART_NO is null

	

	--如果有零件找不到VMI库存数据那么返回错误信息，存储过程结束
	if exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0)
	begin
		select PART_NO,ERROR_MSG from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0
		
		rollback;
		return;
	end

	--检查VMI出库数据是否会把VMI库存更新为负数
	update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP]
	set VALID_FLAG=0 
		,ERROR_MSG=N'零件【'+TEMP.PART_NO+N'】从VMI出库后，对应的VMI库存会变成负数，但是该存储区不允许有负库存'
	from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
		 inner join 
	     [LES].[TT_WMS_STOCKS] as WS with(nolock)
		 on WS.[PART_NO]=TEMP.[PART_NO]
		    and
			WS.[PLANT]=TEMP.D_PLANT
			and
			WS.WM_NO=TEMP.D_WM_NO
			and
			WS.ZONE_NO=TEMP.D_ZONE_NO
			and
			WS.DLOC=TEMP.D_DLOC
			and
			TEMP.SUPPLIER_NUM=WS.SUPPLIER_NUM
			and
			ISNULL(WS.STOCKS_NUM,0)-ISNULL(TEMP.STOCKS_NUM,0)-ISNULL(TEMP.FRAGMENT_NUM,0)<0--VMI出库后库存变为了负数
		inner join
		[LES].[TM_WMM_ZONES] as ZONES with(nolock)
		on TEMP.ZONE_NO=ZONES.ZONE_NO and ZONES.IS_NEGATIVE=0 --该存储区不允许有负库存

		--如果有VMI出库数据会把VMI库存更新为负数，而该库存的存储区不允许为负数，那么返回错误信息，存储过程结束
		if exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0)
		begin
			select PART_NO,ERROR_MSG from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0
			
			rollback;
			return;
		end

		--扣减VMI库存数据
		update [LES].[TT_WMS_STOCKS]
		set
			STOCKS_NUM=ISNULL(WS.STOCKS_NUM,0)-ISNULL(TEMP.STOCKS_NUM,0)
			,AVAILBLE_STOCKS=ISNULL(WS.STOCKS_NUM,0)-ISNULL(TEMP.STOCKS_NUM,0)-ISNULL(WS.FRAGMENT_NUM,0)
			,STOCKS=case when WS.PACKAGE is null or WS.PACKAGE=0
					then 
						0
					else
						case when ISNULL(WS.STOCKS_NUM,0)-ISNULL(TEMP.STOCKS_NUM,0)-ISNULL(WS.FRAGMENT_NUM,0)>0
						then 
							CEILING(CAST(ISNULL(WS.STOCKS_NUM,0)-ISNULL(TEMP.STOCKS_NUM,0)-ISNULL(WS.FRAGMENT_NUM,0) as float)/CAST(WS.PACKAGE as float))
						else
							FLOOR(CAST(ISNULL(WS.STOCKS_NUM,0)-ISNULL(TEMP.STOCKS_NUM,0)-ISNULL(WS.FRAGMENT_NUM,0) as float)/CAST(WS.PACKAGE as float))
						end
					end
		from [LES].[TT_WMS_STOCKS] as WS with(nolock)
		inner join
		[LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
		on
			WS.[PART_NO]=TEMP.[PART_NO]
		    and
			WS.[PLANT]=TEMP.D_PLANT
			and
			WS.WM_NO=TEMP.D_WM_NO
			and
			WS.ZONE_NO=TEMP.D_ZONE_NO
			and
			WS.DLOC=TEMP.D_DLOC
			and
			TEMP.SUPPLIER_NUM=WS.SUPPLIER_NUM

		--将RDC入库单的入库位置信息更新到临时表的目的仓库相关列，以便后面插入VMI仓库交易明细表
		update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] 
		set D_PLANT=RD.PLANT
			,D_WM_NO=RD.WM_NO
			,D_ZONE_NO=RD.ZONE_NO
			,D_DLOC=RD.DLOC
		from
		[LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TE
		inner join 
		[LES].[TT_WMM_RECEIVE_DETAIL] as RD with(nolock)
		on TE.PART_NO=RD.PART_NO
		and TE.SUPPLIER_NUM=RD.SUPPLIER_NUM
		and RD.RECEIVE_ID=@RECEIVE_ID

		--检查收货单的目的RDC存储区是否能找到零件的零件仓库关系
		update [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] 
		set VALID_FLAG=0 
		,ERROR_MSG=N'零件【'+ISNULL(TEMP.PART_NO,N'')+N'】在RDC存储区【'+ISNULL(TEMP.D_ZONE_NO,N'')+N'】找不到零件仓库关系'
		from
		[LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
		left join [LES].[TM_BAS_PARTS_STOCK] as PS with(nolock)
				on TEMP.PART_NO=PS.PART_NO 
				   and
				   TEMP.D_PLANT=PS.PLANT
				   and
				   TEMP.D_WM_NO=PS.WM_NO
				   and
				   TEMP.D_ZONE_NO=PS.ZONE_NO
				   and
				   TEMP.SUPPLIER_NUM=PS.SUPPLIER_NUM
		where PS.PART_NO is null

		--如果收货单的目的RDC存储区不能找到零件的零件仓库关系，那么返回错误信息，存储过程结束
		if exists (select 1 from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0)
		begin
			select PART_NO,ERROR_MSG from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] where VALID_FLAG=0
			
			rollback;
			return;
		end
		
		--插入VMI出库数据到VMI仓库交易明细表
		insert into [LES].[TT_SPM_VMI_TRAN_DETAIL]
		(
			[BILL_NO],
			[PLANT],
			[WM_NO],
			[ZONE_NO],
			[D_PLANT],
			[D_WM_NO],
			[D_ZONE_NO],
			[SUPPLIER_NUM],
			[PART_NO],
			[PART_CNAME],
			[PACKAGE_MODEL],
			[DLOC],
			[D_DLOC],
			[PACKAGE],
			[NUM],
			[BOX_NUM],
			[MODIFICATION_CODE],
			[CREATE_USER],
			[CREATE_DATE]
		)
		select 
			@RECEIVE_NO,
			TEMP.PLANT,
			TEMP.WM_NO,
			TEMP.ZONE_NO,
			TEMP.D_PLANT,
			TEMP.D_WM_NO,
			TEMP.D_ZONE_NO,
			TEMP.SUPPLIER_NUM,
			TEMP.PART_NO,
			PS.PART_CNAME,
			PS.PACKAGE_MODEL,
			TEMP.DLOC,
			TEMP.D_DLOC,
			PS.PACKAGE,
			TEMP.STOCKS_NUM,
			TEMP.STOCKS,
			N'VMIOutput',
			@LOGIN_USERNAME,
			GETDATE()
		from [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] as TEMP
			 inner join [LES].[TM_BAS_PARTS_STOCK] as PS with(nolock)
				on TEMP.PART_NO=PS.PART_NO 
				   and
				   TEMP.D_PLANT=PS.PLANT
				   and
				   TEMP.D_WM_NO=PS.WM_NO
				   and
				   TEMP.D_ZONE_NO=PS.ZONE_NO
				   and
				   TEMP.SUPPLIER_NUM=PS.SUPPLIER_NUM

	commit
END