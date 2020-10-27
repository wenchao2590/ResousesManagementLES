
CREATE function   [dbo].[Func_GetDDPageNumberByID_Tmp](@runsheet_id int)   
  returns   int
  as   
  begin   
    declare   @PageNumber int
	declare   @runsheetcode varchar(15)
	select @runsheetcode=substring(runsheet_code,1,len(runsheet_code)-1) from TT_DD_Runsheet
	where runsheet_id=@runsheet_id
	select @PageNumber=max(page_order) from  TT_DD_Runsheet where substring(runsheet_code,1,len(runsheet_code)-1) =@runsheetcode
	return   @PageNumber
  end