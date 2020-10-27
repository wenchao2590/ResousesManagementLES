


/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS                          */
/*   Program Name:  [PROC_PCS_CONSUME_BACKWARDS]                   */
/*   Called By:     ProcessVehicleMovement                          */
/*    Author:       xuehaijun                                    */
/********************************************************************/

CREATE Procedure [LES].[PROC_PCS_CONSUME_BACKWARDS]
@Knr					Varchar(20),
@RegionIdentity			Integer
As

Set Nocount on

Declare @RollBack					Integer
Declare @LastRegionCheckTime		DateTime
Declare @DifferenceInMinutes		Integer
Declare @LastRegionName				VarChar(20)
Declare @LastRegionSize				Integer
Declare @LastRegionIdentity			Integer
Declare @RegionCheckTime			Integer
Declare @NumberOfVehiclesBack		Integer
Declare @Cmd						Varchar(1000)
Declare @Previous25Identity			Integer	
Declare @LastFootPrintIdentity		Integer	
Declare @NumberBackConsumed			Integer
Declare @Message					Varchar(150)
Declare @CountOfGoodReads 		Integer

Select @RollBack = 0

Update [LES].[TT_PCS_VECHICLE_MOVEMENT] set Arrival_Time_Stamp = GetDate() 
	from(Select [LES].TM_PCS_Region.Region_Identity from [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS] 
	inner join [LES].TM_PCS_Region on [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS].PREDESSOR_REGION = [LES].TM_PCS_Region.Region_Name
	where [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS].Region_Identity = @RegionIdentity)as NotConsumedRecords
	where 
	((NotConsumedRecords.Region_Identity = [LES].[TT_PCS_VECHICLE_MOVEMENT].Region_Identity) and
	(Arrival_Time_Stamp is NULL) 
	and (knr = @Knr))


If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Update VehicleMovement failed in ConsumeBackwards'
	End

Select @LastRegionCheckTime = NULL

Select top 1 @LastRegionCheckTime = [LAST_REGION_CHECKTIME]
	from [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS] (nolock)

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Select from RegionPredessorForConsumeBackwards failed in ConsumeBackwards'
	End


If (@LastRegionCheckTime is NULL)
	Begin
		Update [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS] set [LAST_REGION_CHECKTIME] = GetDate()
		If (@@error != 0)
			Begin 
				exec master.dbo.xp_logevent 75000,
					'Production Pull - Update RegionPredessorForConsumeBackwards failed in ConsumeBackwards'
			End

		Select @LastRegionCheckTime = GetDate()
	End


	Select @RegionCheckTime = 10


Select @DifferenceInMinutes = datediff(mi, @LastRegionCheckTime, GetDate())
If (@DifferenceInMinutes > @RegionCheckTime)
	Begin
		Select @NumberOfVehiclesBack = 25
		Select top 1 @LastRegionIdentity = [Region_Identity] from
			(Select top 2 [Region_Identity], count([Region_Identity])as RegionCount
				from [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS] (nolock)
				group by [Region_Identity]
				order by count([Region_Identity]) desc) as RegionRecords
			order by  RegionRecords.RegionCount

		If (@@error != 0)
			Begin 
				exec master.dbo.xp_logevent 75000,
				'Production Pull - Select LastRegionName from RegionPredessorForConsumeBackwards failed in ConsumeBackwards'
			End


		Select @LastRegionSize = Region_Size, @LastRegionIdentity = Region_Identity, @LastRegionName = Region_Name
		from [LES].TM_PCS_Region  (nolock)
		where Region_Identity = @RegionIdentity --Region_Name = @LastRegionName

		If (@@error != 0)
		Begin 
			exec master.dbo.xp_logevent 75000,
			'Production Pull - Select from Region failed in ConsumeBackwards'
		End

		Select @LastFootPrintIdentity = NULL
		Select top 1 @LastFootPrintIdentity = [VEHICLE_MOVEMEN_TIDENTITY] 
			from [LES].[TT_PCS_VECHICLE_MOVEMENT] (nolock)
			where (([Region_Identity] = @LastRegionIdentity) and
				(FootPrint = 1) and
				(Arrival_Time_Stamp is not NULL))
				order by [VEHICLE_MOVEMEN_TIDENTITY] Desc

		If (@@error != 0)
		Begin 
			exec master.dbo.xp_logevent 75000,
			'Production Pull - Select LastFootPrint from VehicleMovement failed in ConsumeBackwards'
		End
	
		if (@LastFootPrintIdentity is not NULL)
			Begin
				Delete from [LES].TM_PCS_CONSUME_BACKWARDS_WORKTABLE
	
				If (@@error != 0)
					Begin 
						exec master.dbo.xp_logevent 75000,
							'Production Pull - Delete from ConsumeBackwardsWorkTable failed in ConsumeBackwards'
					End


				Select @CountOfGoodReads = count(*) from
					(select top 5 [VEHICLE_MOVEMEN_TIDENTITY], Arrival_Time_Stamp from [LES].[TT_PCS_VECHICLE_MOVEMENT]  (nolock)
					where (([VEHICLE_MOVEMEN_TIDENTITY] < @LastFootPrintIdentity) and
						(FootPrint = 1) and
						([Region_Identity] = @LastRegionIdentity))
						order by [VEHICLE_MOVEMEN_TIDENTITY] desc) as Previous5Values
				where Previous5Values.Arrival_Time_Stamp is not NULL

				If (@CountOfGoodReads = 5)
					Begin

						Select @Cmd = 'Insert into [LES].TM_PCS_CONSUME_BACKWARDS_WORKTABLE (TMP_PREVIOUS_IDENTITY) Select top 1 [VEHICLE_MOVEMEN_TIDENTITY] from ' +
							'(Select top ' + Rtrim(convert(character(5),@NumberOfVehiclesBack)) + ' [VEHICLE_MOVEMEN_TIDENTITY] from [LES].[TT_PCS_VECHICLE_MOVEMENT]  (nolock)' +
							'where (([VEHICLE_MOVEMEN_TIDENTITY] < ' + Rtrim(convert(Character(30),@LastFootPrintIdentity)) + ') and ' + 
							'([Region_Identity] = ' + Rtrim(convert(Character(10),@LastRegionIdentity)) + ') and ' +
							'(FootPrint = 1 ))' +
							'Order by [VEHICLE_MOVEMEN_TIDENTITY] Desc)as PreviousRecords ' + 
							'order by PreviousRecords.[VEHICLE_MOVEMEN_TIDENTITY]  ' 
		
						If (@@error != 0)
							Begin 
								exec master.dbo.xp_logevent 75000,
									'Production Pull - Create @Cmd string failed in ConsumeBackwards'
							End
				
						exec (@Cmd)

						If (@@error != 0)
							Begin 
								exec master.dbo.xp_logevent 75000,
									'Production Pull - Insert into ConsumeBackwardsWorkTable failed in ConsumeBackwards'
							End

						Select top 1 @Previous25Identity = [TMP_PREVIOUS_IDENTITY] from [LES].TM_PCS_CONSUME_BACKWARDS_WORKTABLE
		
						If (@@error != 0)
							Begin 
								exec master.dbo.xp_logevent 75000,
									'Production Pull - Select from ConsumeBackwardsWorkTable failed in ConsumeBackwards'
							End

						Select @NumberBackConsumed = count(distinct (Knr)) from [LES].[TT_PCS_VECHICLE_MOVEMENT] (Nolock)
							where (([VEHICLE_MOVEMEN_TIDENTITY] < @Previous25Identity) and 
								([Region_Identity] = @LastRegionIdentity) and
								(Arrival_Time_Stamp is NULL))

						If (@NumberBackConsumed > 0)
							Begin 
								Select @Message = 'Production Pull Warning - back consumed ' + Rtrim(convert(character(10),@NumberBackConsumed)) + ' vehicles in region ' + Rtrim(@LastRegionName)
									exec master.dbo.xp_logevent 75000, @Message
								Select @Message = 'Production Pull Warning - back consumed  PVI located at identity =  ' + Rtrim(convert(character(30),@LastFootPrintIdentity))
									exec master.dbo.xp_logevent 75000, @Message
								End

						Update [LES].[TT_PCS_VECHICLE_MOVEMENT]  set Arrival_Time_Stamp = GetDate()
							where (([VEHICLE_MOVEMEN_TIDENTITY] < @Previous25Identity) and 
								([Region_Identity] = @LastRegionIdentity) and
								(Arrival_Time_Stamp is NULL))
		
						If (@@error != 0)
							Begin 
								exec master.dbo.xp_logevent 75000,
									'Production Pull - Update VehicleMovement failed in ConsumeBackwards'
							End
					End

			End

			Update [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS] set [LAST_REGION_CHECKTIME] = GetDate()
			
			If (@@error != 0)
				Begin 
					exec master.dbo.xp_logevent 75000,
						'Production Pull - Update table RegionPredessorForConsumeBackwards failed in ConsumeBackwards'

				End

	End

Return (@RollBack)