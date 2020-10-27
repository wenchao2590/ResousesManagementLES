


/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  MoveVehicles                                    */
/*   Called By:     ProcessVehicleMovement                          */
/*   Purpose:       This stored procedure moves vehicles from their */
/*    Author:       xuehaijun                                       */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_MOVE_VEHICLES]
@Knr					Varchar(20),
@RegionNameIdentity		Integer,
@RegionSize				Integer

As

Set Nocount on

Declare @RollBack					Integer
Declare @PassTime					date
Declare @Diff						Integer
Declare @PLANT						Varchar(2)
Declare @ASSEMBLY_LINE              Varchar(5)
Declare @DCP_NAME                   Varchar(50)
Declare @Region_Order				Integer
Select @RollBack = 0

Update [LES].[TT_PCS_VECHICLE_MOVEMENT]  with (rowlock) set Arrival_Time_Stamp = GetDate() from 

(Select Knr, min(VEHICLE_MOVEMEN_TIDENTITY) as VMIdentity from [LES].[TT_PCS_VECHICLE_MOVEMENT](nolock) 
where (Arrival_Time_Stamp is null and [Region_Identity] = @RegionNameIdentity) 
group by Knr, Permanent_Knr_Index having (count(Knr) < @RegionSize)) as KnrMovement 

where ([LES].[TT_PCS_VECHICLE_MOVEMENT].VEHICLE_MOVEMEN_TIDENTITY = KnrMovement.VMIdentity)

If (@@error != 0) 
	Begin
	exec master.dbo.xp_logevent 75000,
		'Production Pull - Update of table VehicleMoveEngine failed in MoveVehicles'
		Select @RollBack = 1
	End

	Update [LES].[TT_PCS_VECHICLE_MOVEMENT] with(rowlock) set Arrival_Time_Stamp = GetDate() 
	where (( Knr = @Knr ) and 
	( FootPrint = 1 ) and 
	( [Region_Identity] = @RegionNameIdentity ) and
	( Arrival_Time_Stamp is NULL ))

If (@@error != 0) 
	Begin
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Update of table VehicleMoveEngine for single Knr failed in MoveVehicles'
		Select @RollBack = 1
	End
	
 
return (@RollBack)