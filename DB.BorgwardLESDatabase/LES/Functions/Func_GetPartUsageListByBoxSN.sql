
CREATE Function [LES].[Func_GetPartUsageListByBoxSN](@jis_box_sn int,@jis_runsheet_sn int)   
  returns varchar(1000)   
  as   
  begin   
    declare @sql varchar(1000)   
    set   @sql=''   
    select 
    @sql=CASE WHEN CHARINDEX('*',p.PART_NO)=0 --*号零件用量为0
	THEN @sql+' '+ cast(p.usage as varchar) +'<br/>'
    ELSE   
		 @sql+' 0'+'<br/>'
    END
	from  LES.TT_JIS_Runsheet_Detail p 
		  inner join LES.TT_JIS_Runsheet_Flex c on p.jis_runsheet_flex_sn=c.jis_runsheet_flex_sn
	where  c.jis_runsheet_flex_sn=@jis_runsheet_sn

	return ltrim(rtrim(@sql))

  end