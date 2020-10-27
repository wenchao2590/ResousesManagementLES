

CREATE Function [LES].[Func_Get_Preivew_Racks](@ASSEMBLY_LINE varchar(10),@SUPPLIER_NUM varchar(10),@VEHICLE_STATUS	NVARCHAR(10))   
  returns   varchar(2000)   
  as   
  begin   
   
	declare   @sql   varchar(4000)   
    set   @sql=''   
    select  @sql=@sql+ [RACK]+','
	from	[LES].[TM_JIS_SUPPLY_RACK]  where [ASSEMBLY_LINE]=@ASSEMBLY_LINE and SUPPLIER_NUM=@SUPPLIER_NUM
	and [PROCESS_STATUS]=1
	
    return ltrim(rtrim(@sql))
  end