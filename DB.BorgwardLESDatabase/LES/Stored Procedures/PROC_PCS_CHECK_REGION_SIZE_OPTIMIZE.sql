
/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS						                        */
/*   Program Name:  [PROC_PCS_CHECK_REGION_SIZE]                     */
/*   Called By:     ProcessVehicleMovement                          */
/*   Purpose:       检查扫描点设定的否有大小的变化，如增加位移，减小位移 */
/*    Author:       xuehaijun                                       */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_CHECK_REGION_SIZE_OPTIMIZE]
@Knr				Varchar(20),
@RegionNameIdentity	Integer,
@RegionSize			Integer,
@RegionName			Varchar(16),
@RegionOrder		Integer

As

Set Nocount on

Declare @RollBack					Integer
Declare @CurrentTimeStamp			Datetime
Declare @VEHICLE_MOVEMEN_TIDENTITY	Integer
Declare @LatestRegionSize			Integer
Declare @ErrorString				VarChar(80)
Declare @RollFlag					Integer
Declare @TmpKnr						Varchar(20)
Declare @TmpPermanentKnrIndex		Integer
Declare @plant					varchar(5)
Declare @AssemblyLine				varchar(10)

Select @RollBack = 0
Select @CurrentTimestamp = GetDate()

/*  MFG_PPS_CR22 - Modified to select oldes PVI. (Changed from max to min) */
select @VEHICLE_MOVEMEN_TIDENTITY = min(VEHICLE_MOVEMEN_TIDENTITY)
from [LES].[TT_PCS_VECHICLE_MOVEMENT] (nolock)
where Region_Identity = @RegionNameIdentity

select @TmpKnr = knr, @TmpPermanentKnrIndex = permanent_knr_index
from [LES].[TT_PCS_VECHICLE_MOVEMENT] (nolock)
where (VEHICLE_MOVEMEN_TIDENTITY = @VEHICLE_MOVEMEN_TIDENTITY) 

select @LatestRegionSize = count(*)
from [LES].[TT_PCS_VECHICLE_MOVEMENT] (nolock)
where ((Region_Identity = @RegionNameIdentity) and
	(knr = @TmpKnr) and permanent_knr_index = @TmpPermanentKnrIndex )


If (@LatestRegionSize > @RegionSize)
	begin
		Select @ErrorString = 'Region ' + @RegionName + ' has shrunk in size.'
		
		exec @RollFlag = [LES].[PROC_PCS_SUBTRACT_RECORDS] @LatestRegionSize, @RegionSize, @RegionNameIdentity, @RegionName, @RegionOrder
		if (@RollBack = 0)
			exec @RollFlag = [LES].[PROC_PCS_CREATE_RECORD_SET_TEMPLATE]

	End

If (@LatestRegionSize < @RegionSize)
	Begin
		Select @ErrorString = 'Region ' + @RegionName + ' has grown in size.'

		exec @RollFlag = [LES].[PROC_PCS_ADD_RECORDS]  @LatestRegionSize, @RegionSize, @RegionNameIdentity, @RegionName, @RegionOrder
		if (@RollBack = 0)
			exec @RollFlag = [LES].[PROC_PCS_CREATE_RECORD_SET_TEMPLATE]
	End

return (@RollBack)