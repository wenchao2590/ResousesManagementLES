
/********************************************************************/
/*                                                                  */
/*   Project Name:  LES                                            */
/*                                                                  */
/*   Program Name:  [PROC_PCS_DELIVERY_TIMEREFRESH]					 */
/*                                                                  */
/*   Called By:     ProcessVehicleMovement                          */
/*                                                                  */
/*   Purpose:       Checks to make sure the  delivery time is right  */
/*											                          */
/********************************************************************/
CREATE PROCEDURE  [LES].[PROC_PCS_DELIVERY_TIMEREFRESH]

@Plant varchar(2),
@AssemblyLine varchar(2)

AS

IF(NOT EXISTS(SELECT * FROM TT_PCS_Delivery_Schedule 
      WHERE delivery_date='1900-1-1' and plant =@plant and assembly_line=@AssemblyLine))

RETURN

DECLARE @CurrentDate DATETIME
SET @CurrentDate=DATEADD(dd ,DATEDIFF(dd,0,GETDATE ()),0)

BEGIN TRANSACTION

--删除当天及以后（若存在）按模板生成的投递时间

DELETE FROM TT_PCS_Delivery_Schedule WHERE delivery_date >=@CurrentDate and plant=@plant and assembly_line=@AssemblyLine

IF @@ERROR<>0
BEGIN
      ROLLBACK TRANSACTION
         RETRUN
END

--按最近模板重新生成当天的投递时间

INSERT INTO TT_PCS_Delivery_Schedule(plant, assembly_line,delivery_time,shift,delivery_date,is_deliveried) 
SELECT plant,assembly_line,delivery_time,shift, @CurrentDate,0
FROM TT_PCS_Delivery_Schedule 
WHERE delivery_date='1900-1-1' and plant=@plant and assembly_line=@AssemblyLine
 

IF @@ERROR<>0
BEGIN
      ROLLBACK TRANSACTION
         RETRUN
 END
 
COMMIT TRANSACTION