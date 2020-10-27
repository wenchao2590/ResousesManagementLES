
Create Function [LES].[Func_GetTWDPageNumberByID](@twd_runsheet_sn int)   
  returns   int
  as
begin   
    declare   @PageNumber int
	declare   @RunsheetNo varchar(15)
	select @RunsheetNo=(substring(twd_runsheet_no,1,len(twd_runsheet_no)-1)) from LES.TT_TWD_Runsheet
	where twd_runsheet_sn=@twd_runsheet_sn
	select @PageNumber=max(page_order) from LES.TT_DD_Runsheet where substring(twd_runsheet_no,1,len(twd_runsheet_no)-1)= @RunsheetNo
	return   @PageNumber
  end