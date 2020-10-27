

/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  PROCESS_VEHICLES_MOVEMENT                          */
/*   Called By:     by the WindowService							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       xuehaijun	2011-06-20   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_PROCESS_VEHICLES_MOVEMENT] AS

Declare @NextRecord				Integer
Declare @SequenceNumber			Integer
Declare @Knr					Varchar(20)
Declare @Region					Varchar(16)
Declare @RegionOrder			Integer
Declare @RegionSize				Integer
Declare @RegionNameIdentity		Integer
Declare @RollBackFlag			Integer
Declare @ReturnedRollBack		Integer

Declare @GapCarrier				Integer
Declare @EmptyCarrier			Integer
Declare @GreatestSequenceNumber	Integer
Declare @cmd					Varchar(500)
Declare @CreatePVIRecordSetFlag Int
Declare @StartTime 				Datetime
Declare @EndTime 				Datetime
Declare @Duration_in_char		Char(20)
Declare @FailedWrites			Integer
Declare @SelectPrtItemCmd		Varchar(300)
Declare @PreviousRegionCount	Integer
Declare @CurrentRegionCount		Integer
Declare @PermanentKnrRegion		Integer
Declare @NodeName               varchar(50)

Declare @plant					varchar(5)
Declare @AssemblyLine				varchar(10)
Declare @recalculateFlag         varchar(1)
Declare @ItemType				 varchar(1)

Set Nocount On

Set xact_abort on

Set @NodeName = NULL

Select @CreatePVIRecordSetFlag = 0
Exec @CreatePVIRecordSetFlag = LES.PROC_PCS_CREATE_RECORD_SET_TEMPLATE

/*
update [ATLES].[LES].[TT_PCS_VECHICLE_MOVEMENT]  set [CONSUMED_TIME_STAMP]=getdate() where [ASSEMBLY_LINE]!='TFC3A2' and
 [ARRIVAL_TIME_STAMP] is not null and [CONSUMED_TIME_STAMP] is null
*/ 

Select @PreviousRegionCount = Count(*) from LES.TM_PCS_REGION(nolock)

If (@@error != 0) 
	Exec master.dbo.xp_logevent 75000,
	'Production Pull - PROC_PCS_CREATE_RECORD_SET_TEMPLATE failed in PROCESS_VEHICLES_MOVEMENT'

Select @SelectPrtItemCmd = 'Insert into LES.TE_PCS_PROCESS_VEHICLE_WORKTABLE Select top 30 sequence_number as TmpSequenceNumber, item_id as TmpKNR , [DCP_NAME] as TmpRegion, plant as TmpPlant, ASSEMBLY_LINE as TmpASSEMBLY_LINE from LES.TT_PCS_VECHILE_ITEM (nolock) where Processed_Flag = 0 order by sequence_number'

select @FailedWrites = 0

truncate table  LES.TE_PCS_PROCESS_VEHICLE_WORKTABLE

exec (@SelectPrtItemCmd)

If (@@error != 0) 
exec master.dbo.xp_logevent 75000,
'Production Pull - Insert into TmpProcessVehicle failed in PROCESS_VEHICLES_MOVEMENT'

Select @NextRecord = 0

While (@NextRecord is not NULL)
begin
	select @NextRecord = min(Tmp_Sequence_Number)
		from LES.TE_PCS_PROCESS_VEHICLE_WORKTABLE where Tmp_Sequence_Number  > @NextRecord

	If (@@error != 0) 
			exec master.dbo.xp_logevent 75000,
			'Production Pull - Select @NextRecord failed in PROCESS_VEHICLES_MOVEMENT'

	If (@NextRecord is not NULL)
	begin
		Select @SequenceNumber = Tmp_Sequence_Number, @Knr = [Tmp_Knr], 
					@Region = Tmp_Region, @plant = Tmp_Plant, @AssemblyLine = [TMP_ASSEMBLYLINE]
					from LES.TE_PCS_PROCESS_VEHICLE_WORKTABLE (nolock)
					where Tmp_Sequence_Number = @NextRecord

		If (@@error != 0) 
					exec master.dbo.xp_logevent 75000,
					'Production Pull - Select @SequenceNumber failed in PROCESS_VEHICLES_MOVEMENT'

		Select @Region = min(Region_Name),@ItemType=min(isnull(ORDER_TYPE,2))
					from LES.TM_PCS_REGION (nolock) where Region_Name = @Region and Plant = @Plant and [assembly_line] = @AssemblyLine

		Select @RollBackFlag = 0
		if(@ItemType=1) --使用传统单点推动算法
		Begin
		Begin  Transaction

		If (@Region is not NULL)
		Begin
				
			Select @RegionOrder = Region_Order, @RegionSize = Region_Size , @recalculateFlag=isnull([RECALCULATE_FLAG],1),
						@RegionNameIdentity = Region_Identity, @PermanentKnrRegion = [PERMANENT_REGION]
						from LES.TM_PCS_REGION (nolock) where Region_Name = @Region and plant = @plant and [assembly_line] = @AssemblyLine

			If (@@error != 0)
					Begin 
						exec master.dbo.xp_logevent 75000,
						'Production Pull - Select @RegionOrder failed in PROCESS_VEHICLES_MOVEMENT'
						Select @RollBackFlag = 1
					End
			if(@recalculateFlag='1')
					begin							
							if ((@RollBackFlag = 0) And (@PermanentKnrRegion != 1))
							Begin
								
								exec @RollBackFlag = [LES].[PROC_PCS_CHECK_REGION_SIZE] @Knr, @RegionNameIdentity, @RegionSize, @Region, @RegionOrder
								
								if (@RollBackFlag = 0)
									exec @RollBackFlag = [LES].[PROC_PCS_DO_RECRODS_EXITS] @Knr, @NodeName, @Plant, @AssemblyLine
		
								if (@RollBackFlag = 0)
									exec @RollBackFlag = [LES].PROC_PCS_MOVE_VEHICLES @Knr, @RegionNameIdentity, @RegionSize

								if (@RollBackFlag = 0)
									exec @RollBackFlag = [LES].PROC_PCS_CONSUME_BACKWARDS @Knr, @RegionNameIdentity
								
								if (@RollBackFlag = 0)
									exec  [LES].[PROC_PCS_CLEAR_VEHICLES] @Knr, @RegionNameIdentity, @RegionSize
		
							End

							if ((@RollBackFlag = 0) And (@PermanentKnrRegion= 1))
							Begin

								exec @RollBackFlag = [LES].PROC_PCS_CHECK_REGION_SIZE @Knr, @RegionNameIdentity, @RegionSize, @Region, @RegionOrder

								if (@RollBackFlag = 0)
									exec @RollBackFlag = [LES].PROC_PCS_CREATE_RECORD_SETFOR_SINGLE_REGION @RegionNameIdentity, @Knr
		
								if (@RollBackFlag = 0)
									exec @RollBackFlag = [LES].PROC_PCS_MOVE_VEHICLES @Knr, @RegionNameIdentity, @RegionSize
									
								if (@RollBackFlag = 0)
									exec  [LES].[PROC_PCS_CLEAR_VEHICLES] @Knr, @RegionNameIdentity, @RegionSize
							End
					end
					else  --校验点
					begin
								
								if (@RollBackFlag = 0)
									exec  [LES].[PROC_PCS_AMENDMENT_MOVE_VEHICLES] @Knr, @RegionNameIdentity, @RegionSize
					end		

		End

		If (@RollBackFlag = 1)
					RollBack Transaction
				else
					Commit Transaction

		Select @cmd = 'update [LES].[TT_PCS_VECHILE_ITEM] with (rowlock) set Processed_Flag = 1 where sequence_number = '  + convert(char(15), @NextRecord) 
				
		If (@@error != 0)
				Begin 
					exec master.dbo.xp_logevent 75000,
						'Production Pull - Update of ProcessedFlag in TT_PCS_VechileITEM  failed in PROCESS_VEHICLES_MOVEMENT'
					exec master.dbo.xp_logevent 75000,
						'Production Pull - Fix table TT_PCS_VechileITEM because this will severly slow down ProductionPull'
					Select @RollBackFlag = 1
				End

		if (@RollBackFlag = 0 )
					exec (@cmd)

		If (@@error != 0)
			Begin 
				exec master.dbo.xp_logevent 75000,
				'Production Pull - Update TT_PCS_VechileITEM failed in PROCESS_VEHICLES_MOVEMENT'						
			End
		End
		else  --使用优化算法
		Begin
					
					--使用优化算法
					BEGIN TRANSACTION;
					BEGIN TRY
						IF(@Region IS NOT NULL)
							BEGIN
								Select @RegionOrder = Region_Order, @RegionSize = Region_Size , @recalculateFlag=isnull([RECALCULATE_FLAG],1),
								@RegionNameIdentity = Region_Identity, @PermanentKnrRegion = [PERMANENT_REGION]
								from LES.TM_PCS_REGION (nolock) where Region_Name = @Region and plant = @plant and [assembly_line] = @AssemblyLine

								IF(@recalculateFlag='1')--扫描点类型 1:生成序列并主推点 2:生成序列点 3:主推点 4:校验点
									BEGIN
										IF ((@RollBackFlag = 0) And (@PermanentKnrRegion= 2))
											BEGIN
												IF (@RollBackFlag = 0)
													EXEC @RollBackFlag = [LES].PROC_PCS_CREATE_RECORD_SETFOR_SINGLE_REGION_OPTIMIZE @RegionNameIdentity, @Knr

												IF (@RollBackFlag = 0)
													EXEC @RollBackFlag = [LES].PROC_PCS_MOVE_VEHICLES_OPTIMIZE @Knr, @RegionNameIdentity, @RegionSize,@plant,@AssemblyLine

												IF (@RollBackFlag = 0)
													--EXEC  [LES].[PROC_PCS_CLEAR_VEHICLES] @Knr, @RegionNameIdentity, @RegionSize
													EXEC [LES].[PROC_PCS_CLEAR_VEHICLES_OPTIMIZE] @plant,@AssemblyLine
									End
									END
								ELSE IF(@recalculateFlag='2')--生成序列点
									BEGIN
										IF ((@RollBackFlag = 0) And (@PermanentKnrRegion= 2))
											BEGIN
												IF (@RollBackFlag = 0)
													EXEC @RollBackFlag = [LES].PROC_PCS_CREATE_RECORD_SETFOR_SINGLE_REGION_OPTIMIZE @RegionNameIdentity, @Knr
											END
									END
								ELSE IF(@recalculateFlag='3')--主推点
									BEGIN
										IF ((@RollBackFlag = 0) And (@PermanentKnrRegion= 2))
											BEGIN
												IF (@RollBackFlag = 0)
													EXEC @RollBackFlag = [LES].PROC_PCS_MOVE_VEHICLES_OPTIMIZE_FOR_MAIN_PUSH @Knr, @RegionNameIdentity, @RegionSize,@plant,@AssemblyLine

												IF (@RollBackFlag = 0)
													--EXEC  [LES].[PROC_PCS_CLEAR_VEHICLES] @Knr, @RegionNameIdentity, @RegionSize
													EXEC [LES].[PROC_PCS_CLEAR_VEHICLES_OPTIMIZE] @plant,@AssemblyLine
											END
									END
								ELSE IF(@recalculateFlag='4')--校验点
									BEGIN
										IF (@RollBackFlag = 0)
											EXEC  @RollBackFlag = [LES].[PROC_PCS_AMENDMENT_MOVE_VEHICLES_OPTIMIZE] @Knr, @RegionNameIdentity, @RegionSize
										IF (@RollBackFlag = 0)
											EXEC [LES].[PROC_PCS_CLEAR_VEHICLES_OPTIMIZE] @plant,@AssemblyLine
											
									END	
							END
							
					Select @cmd = 'update [LES].[TT_PCS_VECHILE_ITEM] with (rowlock) set Processed_Flag = 1 where sequence_number = '  + convert(char(15), @NextRecord) 
				
					
					exec (@cmd)

					
					END TRY
					BEGIN CATCH
					  INSERT  INTO [LES].[TS_SYS_EXCEPTION] (time_stamp,[application],[METHOD],class,exception_message,error_code)
						SELECT  GETDATE(),'使用优化算法','PROC_PCS_PROCESS_VEHICLES_MOVEMENT','Procedure',ERROR_MESSAGE(),ERROR_LINE()
						ROLLBACK TRANSACTION;
					END CATCH;

		
					COMMIT TRANSACTION;
				
		end
	End	

End