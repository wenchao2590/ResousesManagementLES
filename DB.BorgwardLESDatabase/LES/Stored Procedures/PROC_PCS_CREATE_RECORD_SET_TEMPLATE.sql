



/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS												 */
/*   Program Name:  [PROC_PCS_CREATE_RECORD_SET_TEMPLATE]            */
/*   Called By:     ProcessVehicleMovement and CheckRegionSize      */
/*                                                                  */
/*    Author:       xuehaijun                                      */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_CREATE_RECORD_SET_TEMPLATE]

As

Declare @NextRecord				Integer
Declare @RegionName				Varchar(16)
Declare @RegionSize				Integer
Declare @NumberOfRecords		Integer
Declare @RollBack				Integer
Declare @RegionNameIdentity		Integer
Declare @Knr					Varchar(20)
Declare @Plant					Varchar(5)
Declare	@AssemblyLine				varchar(10)

Set Nocount on

Select @Knr = '123456789'
Select @RollBack = 0
truncate table  LES.TM_PCS_VECHICLE_MOVEMENT_TEMPLATE
truncate table LES.TE_PCS_RECORDSET_TEMPLATE_WORKTABLE

Insert into LES.TE_PCS_RECORDSET_TEMPLATE_WORKTABLE(TMP_REGION_IDENTITY)
	Select Region_Identity as TmpRegionIdentity
	from [LES].[TM_PCS_REGION] (nolock)  where ([PERMANENT_REGION]  != 1) 


If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Insert into RecordSetTemplate WorkTable table failed in CreateKnrRecordSet'
		Select @RollBack = 1
	end
				
select @NextRecord = 0

While (@NextRecord is not NULL)
	begin
		select @NextRecord = min(TMP_REGION_IDENTITY)
			from LES.TE_PCS_RECORDSET_TEMPLATE_WORKTABLE (nolock) where TMP_REGION_IDENTITY  > @NextRecord
	    print @NextRecord
		If (@@error != 0)
			Begin 
				exec master.dbo.xp_logevent 75000,
				'Production Pull - Select @NextRecord failed in CreatePVIRecordSet'
				Select @RollBack = 1
			End

		If (@NextRecord is not NULL)
			begin
				Select @RegionSize = Region_Size, 
					@RegionName = Region_Name, @RegionNameIdentity = Region_Identity,
					@Plant = Plant, @AssemblyLine = [assembly_line]
				from [LES].[TM_PCS_REGION] (nolock)
				where Region_Identity = @NextRecord

				If (@@error != 0)
				Begin 
					exec master.dbo.xp_logevent 75000,
					'Production Pull - Select @RegionSize failed in CreatePVIRecordSet'
					Select @RollBack = 1
				End

				If ((@RegionName != 'Production_Start') and (@RegionName != 'Production_Stop') and (@RegionName != 'NONE'))
					Begin
						Select @NumberOfRecords = 1
						While (@NumberOfRecords <=@RegionSize )
							begin
								insert [LES].TM_PCS_VECHICLE_MOVEMENT_TEMPLATE 
									(Knr, Region_Name,  Region_Identity, FootPrint, Arrival_Time_Stamp, Consumed_Time_Stamp, [permanent_knr_index], Plant, [assembly_line],[plant_zone],[workshop])
									values (@Knr, @RegionName, @RegionNameIdentity, @NumberOfRecords, NULL, NULL, 0, @Plant, @AssemblyLine,null,null)
								-- print @Knr
								If (@@error != 0)
									Begin 
										exec master.dbo.xp_logevent 75000,
											'Production Pull - Insert into VehicleMovement failed in CreatePVIRecordSet'
										Select @RollBack = 1
									End

								Select @NumberOfRecords = @NumberOfRecords + 1
							End
					End
			End
	End	

return (@RollBack)