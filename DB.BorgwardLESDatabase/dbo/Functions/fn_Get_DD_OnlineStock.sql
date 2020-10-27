
/*******************************
--计算在途库存
--wangmeng 
********************************/
CREATE FUNCTION [dbo].[fn_Get_DD_OnlineStock]
(
@plant varchar(2),
@workshop varchar(2),
@part_code varchar(8),
@route varchar(3),
@supplier varchar(2)	
)
RETURNS int
AS
BEGIN
DECLARE @ONLINE_STOCK INT
SET @ONLINE_STOCK = 0

If(exists(SELECT * FROM tt_dd_runsheetdetail D 
INNER JOIN tt_dd_runsheet M  ON D.RunSheet_Id=M.RunSheet_Id 
 WHERE M.sheet_status<200  
 and D.plant_code =@plant
 and D.workshop_code =@workshop
 and D.part_code=@part_code
 and D.Route_Code=@route
 and M.supplier_code = @supplier))

SELECT @ONLINE_STOCK=sum(isnull(required_pack_count,0)) FROM tt_dd_runsheetdetail D 
INNER JOIN tt_dd_runsheet M  ON D.RunSheet_Id=M.RunSheet_Id 
 WHERE M.sheet_status<200  
 and D.plant_code =@plant
 and D.workshop_code =@workshop
 and D.part_code=@part_code
 and D.Route_Code=@route
 and M.supplier_code = @supplier
--这里要只计算可以记入常库存的物料单 wangzhen 2007-9-27
 and M.stock_flag=1

return Isnull(@ONLINE_STOCK,0)
END