

/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS												*/
/*   Program Name:  [PROC_PCS_CREATE_RECORDSET]                     */
/*   Called By:     ProcessVehicleMovement                          */
/*    Author:       xuehaijun                                       */
/********************************************************************/

CREATE Procedure [LES].[PROC_PCS_CREATE_RECORDSET]
@Knr							Varchar(20),
@plant							Varchar(5),
@AssemblyLine					Varchar(10)
As

Declare @NextRecord				Integer
Declare @RegionName				Varchar(16)
Declare @RegionSize				Integer
Declare @NumberOfRecords		Integer
Declare @RollBack				Integer
Declare @RegionNameIdentity		Integer


Set Nocount on

Select @RollBack = 0

Update [LES].[TM_PCS_VECHICLE_MOVEMENT_TEMPLATE] set knr=@Knr where PLANT=@plant and ASSEMBLY_LINE=@AssemblyLine

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Update VehicleMovementTemplate table failed in CreatePVIRecordSet'
		Select @RollBack = 1
	end

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
SELECT [knr]
      ,[Region_Identity]
      ,[region_name]
      ,[footprint]
      ,[arrival_time_stamp]
      ,[consumed_time_stamp]
      ,[permanent_knr_index]
      ,[plant]
      ,[assembly_line]
      ,[plant_zone]
      ,[workshop]
  FROM [LES].[TM_PCS_VECHICLE_MOVEMENT_TEMPLATE]
WHERE Plant = @Plant AND [assembly_line] = @AssemblyLine
order by Template_Identity

If (@@error != 0)
	Begin 
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Update VehicleMovementTemplate table failed in CreateKnrRecordSet'
		Select @RollBack = 1
	end

				
return (@RollBack)