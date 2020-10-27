

/********************************************************************/
/*                                                                  */
/*   Project Name:  LES                                            */
/*                                                                  */
/*   Program Name:  [PROC_PCS_DO_RECRODS_EXITS]                         */
/*                                                                  */
/*   Called By:     ProcessVehicleMovement                          */
/*                                                                  */
/*   Purpose:       Checks to make sure the CreatePVIRecordSet was  */
/*                  executed for this KNR.                          */
/********************************************************************/
CREATE Procedure [LES].[PROC_PCS_DO_RECRODS_EXITS]
@KNR				Varchar(20),
@NodeName			Varchar(50),
@Plant				Varchar(5),
@AssemblyLine				varchar(10)
As

Declare @RecordCount		Integer
Declare @RollBack			Integer
Declare @ReturnedRollBack	Integer
Declare @ErrorString		Varchar(135)
Declare @CurrentTimeStamp	dateTime

Select @CurrentTimeStamp = Getdate()
Select @RollBack = 0
Select @ReturnedRollBack = 0

Set Nocount on

Select @RollBack = 0

Select @RecordCount = count(*) from LES.TT_PCS_VECHICLE_MOVEMENT (nolock)
	where(  ([KNR] = @KNR) and ([PERMANENT_KNR_INDEX] = 0)  )

If (@@error != 0) 
	Begin
		exec master.dbo.xp_logevent 75000,
		'Production Pull - Select from table VehicleMovement failed in DoPVIRecordsExist'
		Select @RollBack = 1
	End


If (@RecordCount = 0)
	exec @ReturnedRollBack = LES.PROC_PCS_CREATE_RECORDSET @KNR, @Plant, @AssemblyLine

Select @RecordCount = 0
/*select @RecordCount = count(*) from TT_PPS_Vechicle (nolock)
	where PVI = @KNR

if (@RecordCount < 1)
	begin
		If (@NodeName is NULL)
			Begin
				If ( (select isnumeric(@KNR) ) = 1 and (select len(@KNR)) = 9  )
					begin
						If ( (Select count(*) from TM_PPS_MissingPVI (nolock) where PVI = @KNR) < 1)
							Begin 
								Insert  TM_PPS_MissingPVI with (rowlock) (PVI) values (@KNR) 
		
								If (@@error != 0) 
									Begin
										exec master.dbo.xp_logevent 75000,
											'Production Pull - Insert into MissingPVIs failed in DoPVIRecordsExist'
										Select @RollBack = 1
									End
							End
			
						Select @ErrorString = 'PVI ' + @KNR + ' is not in the vehicle table. An attempt will be made to get data from Flex'
						
						/* 03/06/2006 - John Williams - MFG_PPS_CR_43  SQL Reserved Keyword Function Column Name Change to FunctionName   */
--						Insert InformationAndErrors with (rowlock) (TimeStamp, Application,
--							FunctionName, Class, InformationOrError, Text)
--						Values (@CurrentTimeStamp, 'VehicleMoveEngine','DoPVIRecordsExist',NULL,'E',
--							@ErrorString)
				
						If (@@error != 0) 
							Begin
								exec master.dbo.xp_logevent 75000,
									'Production Pull - Insert into InformationAndErrors vehicle failed in DoPVIRecordsExist'
								Select @RollBack = 1
							End
					end
				else
					Begin
						Select @ErrorString = 'PVI ' + @KNR + ' is not in the vehicle table.  This may be a new permanent PVI. The PVI is not in the correct format to get from Flex.'
						
				                /* 03/06/2006 - John Williams - MFG_PPS_CR_43  SQL 2000 InformationAndErrors Column Name Change    */
--						Insert InformationAndErrors with (rowlock) (TimeStamp, Application,
--							FunctionName, Class, InformationOrError, Text)
--						Values (@CurrentTimeStamp, 'VehicleMoveEngine','DoPVIRecordsExist',NULL,'E',
--							@ErrorString)
--
						If (@@error != 0) 
							Begin
								exec master.dbo.xp_logevent 75000,
									'Production Pull - Insert into InformationAndErrors vehicle failed in DoPVIRecordsExist'
								Select @RollBack = 1
							End
					End
			end
		else
			Begin
				Insert  TM_PPS_LoadThesePVI (PVI) values (@KNR) 

				If (@@error != 0) 
					Begin
						exec master.dbo.xp_logevent 75000,
							'Production Pull - Insert into LoadThesePVIs vehicle failed in DoPVIRecordsExist'
						Select @RollBack = 1
					End
			End
	End

*/
If (@ReturnedRollBack = 1)
	select @RollBack = 1


Return (@RollBack)