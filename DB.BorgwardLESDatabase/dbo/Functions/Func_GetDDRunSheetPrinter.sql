﻿
Create Function [dbo].[Func_GetDDRunSheetPrinter] 
(	@RunSheetID int)
returns varchar(50)
AS
begin
   declare @Supplier Varchar(2)
   declare @Plant Varchar(2)	
   declare @WorkShop Varchar(2)	
   declare @Dock Varchar(3)
   declare @Route Varchar(3)	
   declare @Part_type int		
   declare @DDPrinter Varchar(50)
   set  @DDPrinter=''
   select @Supplier=supplier_code,@Plant=plant_code,@WorkShop=workshop_code, 
    @Part_type=part_type,@Dock=dock_code,@Route=route_code
	from TT_DD_Runsheet where runsheet_id=@RunSheetID
   Select @DDPrinter=printer_name from TR_BAS_RunsheetPrinter where 
   supplier=@supplier and part_type=@part_type and plant=@plant and      
   workshop=@workshop and (dock=@dock or dock='**') and (route=@route or route='**')           
   if @DDPrinter=''
       select top 1 @DDPrinter=printer_name  from TM_BAS_DefaultPrinter
	   where supplier=@Supplier and part_type=@Part_type
   return @DDPrinter
end