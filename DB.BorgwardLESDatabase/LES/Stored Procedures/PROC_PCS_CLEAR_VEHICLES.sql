


/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS					                          */
/*   Program Name:  MoveVehicles                                    */
/*   Called By:     ProcessVehicleMovement                          */
/*   Purpose:       This stored procedure moves vehicles from their */
/*    Author:       xuehaijun                                       */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_CLEAR_VEHICLES]
@Knr					Varchar(20),
@RegionNameIdentity		Integer,
@RegionSize				Integer

As

Set Nocount on

 
 Begin Try
	delete from LES.TT_PCS_VECHICLE_MOVEMENT where knr 

in (
	(Select distinct Knr from [LES].

[TT_PCS_VECHICLE_MOVEMENT](nolock) 
	where (consumed_time_stamp is not null and 
[REGION_IDENTITY] = @RegionNameIdentity and  
	[FOOTPRINT] = @RegionSize)) )
	end try
 begin catch
  
--记录错误信息
insert into [LES].[TS_SYS_EXCEPTION] (time_stamp, 

[application], [METHOD], class,  exception_message, 

error_code)
select getdate

(),'PCS','PROC_PCS_MOVE_VEHICLES','Procedure',error_message

(),ERROR_LINE()
 end catch