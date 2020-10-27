
CREATE function   [dbo].[fn_GetNextRegion](@region_Id varchar(20))   
  returns   varchar(20)
  as
BEGIN
    declare @nextRegion varchar(20)
	Declare @region_order int
	SELECT @Region_order = region_Order FROM TM_PPS_REGION WHERE region_name = @region_id
	
	select top 1 @nextRegion = region_name from TM_PPS_REGION WHERE region_order > @region_order order by region_order
	
	return @nextRegion
	
END