
CREATE  Function   [LES].[Func_GetBoxListByPartNO](@part_no varchar(20),@jis_runsheet_sn int)   
  returns   varchar(200)   
  as   
  begin   
    declare   @sql   varchar(1000)   
    set   @sql=''   
    select    @sql=@sql+' '+ cast(a.jis_box_sn as varchar) from 
    (select distinct b.jis_box_sn 
     from LES.TT_JIS_RUNSHEET_FLEX b
			inner join LES.TT_JIS_RUNSHEET_DETAIL p 
			on  p.jis_runsheet_flex_sn=b.jis_runsheet_flex_sn
			inner join LES.TT_JIS_RUNSHEET d on b.JIS_RUNSHEET_SN = d.JIS_RUNSHEET_SN 
     where p.part_no=@part_no and d.jis_runsheet_sn=@jis_runsheet_sn) a
    return   ltrim(rtrim(@sql))
  end