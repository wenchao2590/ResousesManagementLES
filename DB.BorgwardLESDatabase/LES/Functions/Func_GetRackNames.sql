


CREATE Function [LES].[Func_GetRackNames](@ASSEMBLY_LINE varchar(10),@SUPPLIER_NUM varchar(10),@VEHICLE_STATUS	NVARCHAR(10))   
  returns   varchar(2000)   
  as   
  begin   
   
	declare   @sql   varchar(2000)   
    set   @sql=''   
    select  @sql=@sql+[RACK_CNAME]+','
	from	[LES].[TM_JIS_RACK] where [ASSEMBLY_LINE]=@ASSEMBLY_LINE and [SUPPLIER_NUM]=@SUPPLIER_NUM
    and CHARINDEX(@VEHICLE_STATUS,[PREVIEW_POINT]) > 0
    return ltrim(rtrim(@sql))
  end