
--获取JIT物料单的默许打印机
--参数1：物料单ID
CREATE Function [dbo].[Func_GetJitRunSheetPrinter] 
(	@RunSheetID int)
returns varchar(50)
AS
begin
   declare @Supplier Varchar(2)
   declare @Plant Varchar(2)	
   declare @WorkShop Varchar(2)	
   declare @Dock Varchar(3)
   declare @Rack Varchar(3)	
   declare @Part_type int		
   declare @JitPrinter Varchar(50)
   set @Part_type=3  -- Added by fengyoujun, set JIT Part Type default value
   set @JitPrinter=''
   select @Supplier=supplier,@Plant=plant,@WorkShop=workshop, @Dock=dock,@Rack=rack
	from TT_JIT_Runsheet where jit_runsheet_sn=@RunSheetID
   Select @JitPrinter=printer_name from TR_BAS_RunsheetPrinter 
   where supplier=@supplier and plant=@plant and workshop=@workshop and 
   (dock=@dock or dock='**')  and (rack=@rack or rack='**') 

	if @JitPrinter=''
       select top 1 @JitPrinter=printer_name  from TM_BAS_DefaultPrinter
	   where supplier=@Supplier and part_type=@Part_type
   return @JitPrinter
end