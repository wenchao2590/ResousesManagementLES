

/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*                                                                  */
/*   Program Name:  SubtractRecords                                 */
/*                                                                  */
/*   Called By:     CheckRegionSize                                 */    
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_SUBTRACT_RECORDS]
@LatestRegionSize			Integer,
@RegionSize					Integer,
@RegionNameIdentity			Integer,
@RegionName					Varchar(16),
@RegionOrder				Integer


As

Declare @RollBack			Integer
Declare @NextRecord			Integer
Declare @KNR				Varchar(20)
Declare @CurrentTimeStamp		Datetime
Declare @PermanentPVIIndex		Integer
Declare @Footprint			Integer

Set Nocount On

Select @RollBack = 0
select @CurrentTimeStamp = GetDate()


/* MFG_PPS_CR22 Added field (TmpFootprint) to hold [KNR]'s current footprint for jobs that are in or before the region */
Create table #TmpSubtractRecords
(	
	TmpIdentity				Integer identity,
	TmpVehicleMovementPVI		Varchar(20),
	TmpFootprint				Integer,
	TmpPermanentPVIIndex			Integer
)

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Create able TmpSubtractRecords failed in AddRecords'
		Select @RollBack = 1
	End

/* MFG_PPS_CR22 Added field to hold [KNR]'s current footprint. The first select elminates all [KNR]'s that have gone through all the footprints for this region */
Insert into #TmpSubtractRecords
	Select Distinct [KNR], max(Footprint), [PERMANENT_KNR_INDEX] from TT_PCS_VECHICLE_MOVEMENT (nolock) 
		where (Arrival_Time_Stamp is NULL and [REGION_IDENTITY] = @RegionNameIdentity) group by [KNR], [PERMANENT_KNR_INDEX]


If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Insert into TmpSubtractRecords failed in SubtractRecords'
		Select @RollBack = 1
	End

/* MFG_PPS_CR22 Delete footprints for jobs that are in, out, and before the shrunk region. DELETE statement moved out of the  While loop */
Delete from TT_PCS_VECHICLE_MOVEMENT where
	(([REGION_IDENTITY] = @RegionNameIdentity) and (FootPrint > @RegionSize))
										
If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
			'Production Pull - Delete from VehicleMovement failed in SubtractRecords'
		Select @RollBack = 1
	End

Select @NextRecord = 0

While (@NextRecord is not NULL)
begin
	select @NextRecord = min(TmpIdentity)
		from #TmpSubtractRecords where TmpIdentity  > @NextRecord
	
	If (@@error != 0)
		Begin 
			exec master.dbo.xp_logevent 75000,
			'Production Pull - Select @NextRecord failed in SubtractRecords'
			Select @RollBack = 1
		End

	If (@NextRecord is not NULL)
		Begin

			Select @KNR = TmpVehicleMovementPVI, @Footprint = TmpFootprint, @PermanentPVIIndex = TmpPermanentPVIIndex
				from #TmpSubtractRecords
				where TmpIdentity = @NextRecord

			If (@@error != 0)
				Begin 
					exec master.dbo.xp_logevent 75000,
						'Production Pull - Select from #TmpSubtractRecords failed in SubtractRecords'
					Select @RollBack = 1
				End


			/* MFG_PPS_CR22 Update ArrivalTimeStamp for [KNR]'s that are in shrunk region and current footprint is between OLD and new region size */
			If ((@Footprint < @LatestRegionSize) and (@Footprint >= @RegionSize))
				Begin
					Update TT_PCS_VECHICLE_MOVEMENT set Arrival_Time_Stamp = @CurrentTimestamp
					Where(([REGION_IDENTITY] = @RegionNameIdentity) 
					And (FootPrint = @RegionSize) and ([KNR] = @KNR) and ([PERMANENT_KNR_INDEX] = @PermanentPVIIndex))
				End

			If (@@error != 0)
				Begin 

					exec master.dbo.xp_logevent 75000,
						'Production Pull - Update VehicleMovement failed in SubtractRecords'
					Select @RollBack = 1
				End
		End

End

return (@RollBack)