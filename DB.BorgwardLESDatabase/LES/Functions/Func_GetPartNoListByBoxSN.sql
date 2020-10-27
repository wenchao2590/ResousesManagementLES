
CREATE Function [LES].[Func_GetPartNoListByBoxSN](@jis_box_sn int,@jis_runsheet_sn int)   
  returns varchar(1000)   
  as   
  begin   
    declare @sql varchar(1000)   
    set   @sql=''   
    select @sql=@sql+' '+cast(p.part_no as varchar)+'<br/>'
	from  LES.TT_JIS_Runsheet_Detail p 
		  inner join LES.TT_JIS_Runsheet_Flex c on p.jis_runsheet_flex_sn=c.jis_runsheet_flex_sn
	where   c.jis_runsheet_flex_sn=@jis_runsheet_sn

	return ltrim(rtrim(@sql))
  end