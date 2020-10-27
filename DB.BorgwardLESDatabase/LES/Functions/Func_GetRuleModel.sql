
CREATE Function [LES].[Func_GetRuleModel](@ASSEMBLY_RULE int)   
  returns   varchar(200)   
  as   
  begin   
   
	declare   @sql   varchar(1000)   
    set   @sql=''   
    select  @sql=@sql+','+[MODEL]
	from	[LES].[TR_BAS_RULE_MODEL] where [ASSEMBLY_RULE]=@ASSEMBLY_RULE
   
    return ltrim(rtrim(@sql))
  end