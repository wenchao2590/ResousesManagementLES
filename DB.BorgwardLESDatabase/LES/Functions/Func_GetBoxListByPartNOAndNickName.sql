
--modify by 【运维】whk 2014/8/12 【CMDB编号：CR-LES-20140807】 start
--根据“零件号，零件昵称，拉动单序号”获取单元格序号
--ALTER  Function   [LES].[Func_GetBoxListByPartNO](@part_no varchar(20),@jis_runsheet_sn int) 
CREATE  Function   [LES].[Func_GetBoxListByPartNOAndNickName](@part_no varchar(20),@jis_runsheet_sn int, @part_nick_name varchar(30))   
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
     --where p.part_no=@part_no and d.jis_runsheet_sn=@jis_runsheet_sn) a
     where p.part_no=@part_no and d.jis_runsheet_sn=@jis_runsheet_sn and p.PART_NICK_NAME=@part_nick_name) a
    return   ltrim(rtrim(@sql))
  end
  --modify by 【运维】whk 2014/8/12 end