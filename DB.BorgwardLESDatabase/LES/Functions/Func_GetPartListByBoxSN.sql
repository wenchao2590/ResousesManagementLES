
CREATE Function [LES].[Func_GetPartListByBoxSN](@jis_box_sn int,@jis_runsheet_sn int)   
  returns   varchar(100)   
  as   
  begin   
    declare   @sql   varchar(1000)   
    set   @sql=''   
    select  @sql=@sql+' '+cast(p.part_no as varchar)
	from	LES.TT_JIS_Runsheet_Detail p inner join LES.TT_JIS_Runsheet_Box b 
				on p.jis_runsheet_box_sn=b.jis_runsheet_box_sn 
				and p.jis_runsheet_flex_sn=b.jis_runsheet_flex_sn
			inner join LES.TT_JIS_Runsheet_Flex c on b.jis_runsheet_flex_sn=c.jis_runsheet_flex_sn
where b.jis_box_sn=@jis_box_sn and c.jis_runsheet_sn=@jis_runsheet_sn
    return ltrim(rtrim(@sql))
  end