







/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS												*/
/*   Program Name:  [PROC_PCS_AMENDMENT_MOVE_VEHICLES]               */
/*   Called By:     ProcessVehicleMovement                          */
/*   Purpose:       This stored procedure moves vehicles from their */
/*    Author:       xuehaijun                                       */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_AMENDMENT_MOVE_VEHICLES]
@Knr					Varchar(20),
@RegionNameIdentity		Integer,
@RegionSize				Integer

As

Set Nocount on

Declare @RollBack					Integer
--Declare @PassTime					date
--Declare @offset						Integer
--Declare @Region_Order				Integer
Declare @checkSize					int
Declare @MainRegionIdentity			int
Declare @TotalSize					int
Declare @RegionOrder				int
Declare @MainRegionSize				int
declare @NextRegionSize				int
declare @MainMovementIdentity		int
declare @PcsDelay					int
Declare @PLANT						Varchar(5)
Declare @ASSEMBLY_LINE              Varchar(5)
Declare @RegionName					Varchar(20)

Declare @MainMovementArrivalTimeStamp	datetime
Select @RollBack = 0


Select  @checkSize = Region_Size,@PLANT=plant,@ASSEMBLY_LINE=ASSEMBLY_LINE,@RegionOrder=REGION_ORDER  from LES.TM_PCS_REGION (nolock) where Region_Identity = @RegionNameIdentity 
select @MainRegionIdentity=[Region_Identity],@MainRegionSize=Region_Size,@RegionName=[REGION_NAME] from  LES.TM_PCS_REGION where (isnull([RECALCULATE_FLAG],1)=1) and PLANT=@PLANT and ASSEMBLY_LINE=@ASSEMBLY_LINE

If (@@error != 0) 
Begin
	exec master.dbo.xp_logevent 75000,
	'Production Pull - Update of table VehicleMoveEngine failed in MoveVehicles'
	Select @RollBack = 1
End

select @MainMovementIdentity=[VEHICLE_MOVEMEN_TIDENTITY],@MainMovementArrivalTimeStamp=Arrival_Time_Stamp from  [LES].[TT_PCS_VECHICLE_MOVEMENT] 
	where Knr=@Knr and FootPrint=@checkSize and PLANT=@PLANT and ASSEMBLY_LINE=@ASSEMBLY_LINE AND [Region_Identity] = @MainRegionIdentity

if @MainMovementArrivalTimeStamp is not null
BEGIN
return (@RollBack)
END

Update [LES].[TT_PCS_VECHICLE_MOVEMENT] with(rowlock) set Arrival_Time_Stamp = GetDate() 
	where [VEHICLE_MOVEMEN_TIDENTITY]<= @MainMovementIdentity and  footprint<=@checkSize and 
	[Region_Identity] = @MainRegionIdentity and
	Arrival_Time_Stamp is NULL and PLANT=@PLANT and ASSEMBLY_LINE=@ASSEMBLY_LINE
					
select @PcsDelay=isnull([PARAMETER_VALUE],5) from [LES].[TS_SYS_CONFIG] where [PARAMETER_NAME]='PCS_DELAY'
if(not exists(select [SEQUENCE_NUMBER] from [LES].[TT_PCS_VECHILE_ITEM] where [DCP_NAME]=@RegionName and DATEDIFF(mi,[TIMESTAMP],GETDATE())<@PcsDelay))
begin
	--Select  @TotalSize=min(Region_Size) from LES.TM_PCS_REGION where (isnull([RECALCULATE_FLAG],1)!=1)  and PLANT=@PLANT and ASSEMBLY_LINE=@ASSEMBLY_LINE and REGION_ORDER>@RegionOrder
	--select @NextRegionSize=@TotalSize
	select @TotalSize=	@MainRegionSize-isnull(@checkSize,0)

	Update [LES].[TT_PCS_VECHICLE_MOVEMENT]  with (rowlock) set Arrival_Time_Stamp = GetDate() from 
	(Select Knr, min(VEHICLE_MOVEMEN_TIDENTITY) as VMIdentity from [LES].[TT_PCS_VECHICLE_MOVEMENT](nolock) 
	where (Arrival_Time_Stamp is null and [Region_Identity] = @MainRegionIdentity and footprint>=@checkSize
	AND VEHICLE_MOVEMEN_TIDENTITY< @MainMovementIdentity) 
	group by Knr, Permanent_Knr_Index having (count(Knr) <= @TotalSize)) as KnrMovement 
	where ([LES].[TT_PCS_VECHICLE_MOVEMENT].VEHICLE_MOVEMEN_TIDENTITY = KnrMovement.VMIdentity)	
end

If (@@error != 0)
Begin
	exec master.dbo.xp_logevent 75000,
	'Production Pull - Update of table VehicleMoveEngine for single Knr failed in MoveVehicles'
	Select @RollBack = 1
End
	
return (@RollBack)