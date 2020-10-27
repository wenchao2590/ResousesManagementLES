

/********************************************************************/
/*   Project Name:  PCS						                         */
/*   Program Name:  [PROC_PCS_ADD_RECORDS]                           */
/*   Called By:     CheckRegionSize                                 */
/*    Author:       xuehaijun                                       */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_ADD_RECORDS] 
@LatestRegionSize			Integer,
@RegionSize					Integer,
@RegionNameIdentity			Integer,
@RegionName					Varchar(16),
@RegionOrder				Integer

As

Declare @RollBack			Integer
Declare @NextRecord			Integer
Declare @KnrCount			Integer
Declare @Counter			Integer
Declare @Knr				Varchar(20)
Declare @CurrentTimestamp	Datetime
Declare @PermanentKnrIndex		Integer

Set Nocount On

Select @RollBack = 0

Create table #TmpAddRecords
(	
	TmpIdentity				Integer Identity,
	TmpVehicleMovementKnr	Varchar(20),
	TmpPermanentKnrIndex	Integer,
	KnrCount				Integer

)

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Create able TmpVehicleMovementPVI failed in AddRecords'
		Select @RollBack = 1
	End


Create table #TmpAddRecords2
(	
	TmpVehicleMovementKnr	Varchar(20),
	TmpPermanentKnrIndex	integer
)

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Delete from TmpAddRecords2 failed in AddRecords'
		Select @RollBack = 1
	End

/* The first select elminates all PVI's that have gone through all the footprints for this region */

Insert into #TmpAddRecords2
	Select Distinct Knr, permanent_knr_index from [LES].[TT_PCS_VECHICLE_MOVEMENT] (nolock)
		where (Arrival_Time_Stamp is NULL and Region_Identity = @RegionNameIdentity)
		group by knr, permanent_knr_index

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Insert into TmpAddRecords2 failed in AddRecords'
		Select @RollBack = 1
	End

/* The second select elminates all PVI's that have not reach this region */

Insert into #TmpAddRecords
	Select  Knr,  Permanent_Knr_Index, count(*) 
	from [LES].[TT_PCS_VECHICLE_MOVEMENT] (nolock) inner join #TmpAddRecords2 
	on ((#TmpAddRecords2.TmpVehicleMovementKnr = [LES].[TT_PCS_VECHICLE_MOVEMENT].knr)  and
		(#TmpAddRecords2.TmpPermanentKnrIndex = [LES].[TT_PCS_VECHICLE_MOVEMENT].permanent_knr_index))
	where (Region_Identity = @RegionNameIdentity)
	group by knr, permanent_knr_index


If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Insert into TmpAddRecords failed in AddRecords'
		Select @RollBack = 1
	End


Create table #TmpAddConsumedRecords
(	
	TmpIdentity				Integer Identity,
	TmpVehicleMovementKnr	Varchar(20),
	TmpPermanentKnrIndex	Integer,
	KnrCount				Integer

)

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Create Table TmpVehicleMovementKnr failed in AddRecords'
		Select @RollBack = 1
	End


/* MFG_PPS_CR22 Set timestammp to null  */
Select @CurrentTimeStamp = NULL
Select @NextRecord = 0

While (@NextRecord is not NULL)
begin
	select @NextRecord = min(TmpIdentity)
		from #TmpAddRecords where TmpIdentity  > @NextRecord
	
	If (@@error != 0)

		Begin 
			exec master.dbo.xp_logevent 75000,
			'Production Pull - Select @NextRecord failed in AddRecords'
			Select @RollBack = 1
		End

	If (@NextRecord is not NULL)
	begin
		select @Knr = TmpVehicleMovementKnr, @PermanentKnrIndex = TmpPermanentKnrIndex, @KnrCount = KnrCount
		from #TmpAddRecords 
		where (TmpIdentity = @NextRecord) 

		Select @Counter = @KnrCount + 1
		While(@Counter <= @RegionSize)
		begin
			insert [LES].[TT_PCS_VECHICLE_MOVEMENT] 
				(knr, Region_Name, Region_Identity, FootPrint, Arrival_Time_Stamp, Consumed_Time_Stamp, permanent_knr_index)
				values (@Knr, @RegionName, @RegionNameIdentity, @Counter, @CurrentTimeStamp, @CurrentTimeStamp, @PermanentKnrIndex)
								

				If (@@error != 0)
				Begin 
					exec master.dbo.xp_logevent 75000,
						'Production Pull - Insert into VehicleMovement failed in AddRecords'
					Select @RollBack = 1
				End

			Select @Counter = @Counter + 1
		End
	End

End

Insert into #TmpAddConsumedRecords
	Select Distinct knr, permanent_knr_index,count(*) from [LES].[TT_PCS_VECHICLE_MOVEMENT]  (nolock)
		where (Arrival_Time_Stamp is NOT NULL and Region_Identity = @RegionNameIdentity )
		group by knr, permanent_knr_index
	        having count(*) = @LatestRegionSize


If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Insert into TmpAddConsumedRecords failed in AddRecords'
		Select @RollBack = 1
	End

/* MFG_PPS_CR22  Added following while block to update all pvis that have already tracked and popullate arrival and consumed timestamp */

Select @CurrentTimeStamp = GetDate()
Select @NextRecord = 0

While (@NextRecord is not NULL)
begin
	select @NextRecord = min(TmpIdentity)
		from #TmpAddConsumedRecords where TmpIdentity  > @NextRecord
	
	If (@@error != 0)

		Begin 
			exec master.dbo.xp_logevent 75000,
			'Production Pull - Select @NextRecord failed in AddRecords'
			Select @RollBack = 1
		End

	If (@NextRecord is not NULL)
	begin
		select @Knr = TmpVehicleMovementKnr, @PermanentKnrIndex = TmpPermanentKnrIndex, @KnrCount = KnrCount
		from #TmpAddConsumedRecords 
		where (TmpIdentity = @NextRecord) 

		Select @Counter = @KnrCount + 1
		While(@Counter <= @RegionSize)
		begin
			insert [LES].[TT_PCS_VECHICLE_MOVEMENT]  
				(Knr, Region_Name, Region_Identity, FootPrint, Arrival_Time_Stamp, Consumed_Time_Stamp, Permanent_Knr_Index)
				values (@Knr, @RegionName, @RegionNameIdentity, @Counter, @CurrentTimeStamp, @CurrentTimeStamp, @PermanentKnrIndex)
								

				If (@@error != 0)
				Begin 
					exec master.dbo.xp_logevent 75000,
						'Production Pull - Insert into VehicleMovement failed in AddConsumedRecords'
					Select @RollBack = 1
				End

			Select @Counter = @Counter + 1
		End
	End

End


return (@RollBack)