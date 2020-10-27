
CREATE Function [LES].[Func_GetPartNoCountByBoxSN](@jis_box_sn int,@jis_runsheet_sn int)   
  returns int
  as   
  begin   
    declare @PartNoCount int
    select  @PartNoCount=count(*) from LES.TT_JIS_Runsheet_Detail p
			inner join LES.TT_JIS_Runsheet_Flex c on p.jis_runsheet_flex_sn=c.jis_runsheet_flex_sn
	where  c.jis_runsheet_flex_sn=@jis_runsheet_sn
    
    return   @PartNoCount
  end