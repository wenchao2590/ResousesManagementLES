


/****** Object:  Stored Procedure dbo.CreatePVIRecordSetForSingleRegion    Script Date: 2/2/2004 8:53:05 AM ******/
/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS                          */
/*   Program Name:  [PROC_PCS_CREATE_RECORD_SETFOR_SINGLE_REGION]	*/
/*   Called By:     ProcessVehicleMovement							*/
/*    Author:       xuehaijun                                      */
/*                                                                  */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_CREATE_RECORD_SETFOR_SINGLE_REGION]
@RegionNameIdentity 	Integer,
@Knr					Varchar(20)

As

Declare @RegionOrder			Integer
Declare @RegionSize			Integer
Declare @NumberOfRecords		Integer
Declare @RollBack			Integer
Declare @SubIndexValue		Integer
Declare @RegionName			Varchar(16)
Declare @RecordCount			Integer
Declare @ErrorString			Varchar(135)
Declare @CurrentTimeStamp		dateTime
Declare @NodeName			Varchar(50)
Declare @Plant varchar(5)
Declare @AssemblyLine varchar(30)


Set Nocount on


Select @CurrentTimeStamp = GetDate()

Select @RollBack = 0
	
Select  @RegionSize = Region_Size,
	@RegionName = Region_Name,@Plant=PLANT,@AssemblyLine=ASSEMBLY_LINE
	from [LES].[TM_PCS_Region]   (nolock)
	where Region_Identity = @RegionNameIdentity

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
			'Production Pull - Select @RegionSize failed in CreatePVIRecordSetForBodyShop'
		Select @RollBack = 1
	End

Select @SubIndexValue = max(permanent_knr_index) from [LES].[TT_PCS_VECHICLE_MOVEMENT] (nolock)

If (@SubIndexValue is NULL)
	select @SubIndexValue = 0

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
			'Production Pull - Select @SubIndexValue failed in CreatePVIRecordSet'
		Select @RollBack = 1
	End

Select @SubIndexValue = @SubIndexValue + 1

If ((@RegionName != 'Start') and (@RegionName != 'Stop') and (@RegionName != 'NONE'))
	Begin
		Select @NumberOfRecords = 1
		While (@NumberOfRecords <= @RegionSize)
			begin
				INSERT INTO [LES].[TT_PCS_VECHICLE_MOVEMENT]
           ([knr]
           ,[Region_Identity]
           ,[region_name]
           ,[footprint]
           ,[arrival_time_stamp]
           ,[consumed_time_stamp]
           ,[permanent_knr_index]
           ,[plant]
           ,[assembly_line]
           ,[plant_zone]
           ,[workshop])
			values (@Knr,  @RegionNameIdentity,@RegionName,  @NumberOfRecords, NULL, NULL,@SubIndexValue,@Plant,@AssemblyLine,null,null)
								
				If (@@error != 0)
					Begin 
						exec master.dbo.xp_logevent 75000,
							'Production Pull - Insert into VehicleMovement failed in CreateKnrRecordSetforBodyShop'
						Select @RollBack = 1
					End

					Select @NumberOfRecords = @NumberOfRecords +1
			End
	End


return (@RollBack)