
/****** Object:  Stored Procedure dbo.CreatePVIRecordSetForSingleRegion    Script Date: 2/2/2004 8:53:05 AM ******/
/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS                          */
/*   Program Name:  [PROC_PCS_CREATE_RECORD_SETFOR_SINGLE_REGION]	*/
/*   Called By:     ProcessVehicleMovement							*/
/*    Author:       xuehaijun                                      */
/*   Purpose:       主推点的计算逻辑，生成车辆顺序队列,并初始化消耗 */
/*                                                                  */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_CREATE_RECORD_SETFOR_SINGLE_REGION_OPTIMIZE]
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
DECLARE @SEQUENCE_ASSEMBLY_LINE int


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
 EXEC LES.PROC_PCS_GET_NEXT_ASSEMBLY_LINE_SEQUENCE @Plant,@AssemblyLine,@SEQUENCE_ASSEMBLY_LINE OUTPUT
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
		if(not exists (select SEQUENCE_NUMBER FROM [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE] where @RegionName = Region_Name and PLANT=@Plant and ASSEMBLY_LINE=@AssemblyLine and KNR=@Knr))
		Begin
				INSERT INTO [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
           ([PLANT]
           ,[ASSEMBLY_LINE]
           ,[ITEM_ID]
           ,[REGION_NAME]
           ,[TIMESTAMP]
           ,[ENTRY_TIME]
           ,[PROCESSED_FLAG]
           ,[KNR]
           ,[RUNNING_NO]
           ,[SEQUENCE_ASSEMBLY_LINE]
           ,[TOTAL_TAKT]
           ,[CURRENT_TAKT]
           ,[REGION_IDENTITY])
			values (@Plant,@AssemblyLine,@Knr,@RegionName, GETDATE() ,null,  0, @Knr, NULL,@SEQUENCE_ASSEMBLY_LINE,@RegionSize,0,@RegionNameIdentity)
				
		End
	End


return (@RollBack)