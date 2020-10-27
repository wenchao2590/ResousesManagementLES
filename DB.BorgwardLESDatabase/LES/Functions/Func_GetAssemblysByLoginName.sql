


CREATE Function [LES].[Func_GetAssemblysByLoginName](@LOGIN_USER varchar(10),@SUPPLIER_NUM varchar(10))   
  returns   varchar(2000)   
  as   
  begin   
   
	declare   @sql   varchar(4000)   
    set   @sql=''   
    select  @sql=@sql+''''+ASSEMBLY_LINE+''','
	from LES.TR_SYS_SUPPLY_ASSEMBLY_LINE where [USER_ID] IN ( SELECT [USER_ID]
	FROM [LES].[TS_SYS_USER] where USER_LOGIN_NAME=@LOGIN_USER)
    select @sql=substring(@sql,1,len(@sql)-1)
    return ltrim(rtrim(@sql))
  end