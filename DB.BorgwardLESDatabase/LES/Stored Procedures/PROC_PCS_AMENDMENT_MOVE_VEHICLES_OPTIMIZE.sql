
/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS												*/
/*   Program Name:  [PROC_PCS_AMENDMENT_MOVE_VEHICLES]               */
/*   Called By:     ProcessVehicleMovement                          */
/*   Purpose:       This stored procedure moves vehicles from their */
/*    Author:       xuehaijun                                       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_AMENDMENT_MOVE_VEHICLES_OPTIMIZE]
      @Knr VARCHAR(20) ,
      @RegionNameIdentity INTEGER ,
      @RegionSize INTEGER
AS 
SET Nocount ON

DECLARE @RollBack INTEGER
DECLARE @PassTime DATE
DECLARE @offset INTEGER
DECLARE @Region_Order INTEGER
DECLARE @checkSize INT
DECLARE @MainRegionIdentity INT
DECLARE @TotalSize INT
DECLARE @RegionOrder INT
DECLARE @MainRegionSize INT
DECLARE @NextRegionSize INT
DECLARE @MainMovementIdentity INT
DECLARE @PcsDelay INT
DECLARE @CurrentTakt INT
DECLARE @OldCurrentTakt INT
DECLARE @TMPCurrentTakt INT
DECLARE @i INT
DECLARE @j INT
DECLARE @k INT
DECLARE @NextRecord INT
DECLARE @PLANT VARCHAR(2)
DECLARE @ASSEMBLY_LINE VARCHAR(10)
DECLARE @MainRegionName VARCHAR(4)
DECLARE @CarSequence INT

DECLARE @CurrentKNR VARCHAR(20)
DECLARE @SequenceNumber INT

SELECT  @RollBack = 0
SELECT  @CurrentTakt = 0
SELECT  @OldCurrentTakt = 0
SELECT  @TMPCurrentTakt = 0

PRINT @RegionNameIdentity
SELECT  @checkSize = Region_Size ,
        @PLANT = LTRIM(RTRIM(plant)) ,
        @ASSEMBLY_LINE = LTRIM(RTRIM(ASSEMBLY_LINE)) ,
        @RegionOrder = REGION_ORDER
FROM    LES.TM_PCS_REGION (NOLOCK)
WHERE   Region_Identity = @RegionNameIdentity 

--检查当前工厂流水线下有无生成序列
IF NOT EXISTS(
	SELECT TOP 1 SEQUENCE_NUMBER FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
	WHERE PLANT = @PLANT
	AND ASSEMBLY_LINE = @ASSEMBLY_LINE
)
	BEGIN
		RETURN (@RollBack)
	END

SELECT  @OldCurrentTakt = ISNULL(CURRENT_TAKT, 1)
FROM    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
WHERE   PLANT = @Plant
AND ASSEMBLY_LINE = @ASSEMBLY_LINE
AND knr = @Knr
        
SET @CurrentTakt=@OldCurrentTakt
SET @TMPCurrentTakt=@CurrentTakt

IF ( @OldCurrentTakt < @checkSize )  --表示该车还没有推动到该检验点，需要系统推动
   BEGIN
	
         SELECT @NextRecord = 0
         SELECT @NextRecord = MAX([SEQUENCE_ASSEMBLY_LINE])
         FROM   [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
         WHERE  PLANT = @Plant
         AND ASSEMBLY_LINE = @ASSEMBLY_LINE
         AND KNR = @Knr
         
         --找出主推点信息
         SELECT @MainRegionIdentity = [Region_Identity] ,
                @MainRegionSize = Region_Size ,
                @MainRegionName = [REGION_NAME]
         FROM   LES.TM_PCS_REGION
         WHERE  RECALCULATE_FLAG = 1 
         AND PLANT = @PLANT
         AND ASSEMBLY_LINE = @ASSEMBLY_LINE
         
         IF(@MainRegionSize IS NULL)
			BEGIN
				SELECT @MainRegionIdentity = [Region_Identity] ,
                @MainRegionSize = Region_Size ,
                @MainRegionName = [REGION_NAME]
				FROM   LES.TM_PCS_REGION
				WHERE  RECALCULATE_FLAG = 2 
				AND PLANT = @PLANT
				AND ASSEMBLY_LINE = @ASSEMBLY_LINE
			END
			
		 IF(@MainRegionSize IS NULL)
			BEGIN
				RETURN (@RollBack)
			END	

         WHILE ( @NextRecord IS NOT NULL ) 
               BEGIN
					 DECLARE @NewKNR NVARCHAR(10)
					 DECLARE @NewSequenceNumber INT
					 DECLARE @NewTakt INT
					 SELECT @NewSequenceNumber=SEQUENCE_NUMBER,@NewKNR=KNR,@NewTakt=CURRENT_TAKT FROM   [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
                     WHERE  [SEQUENCE_ASSEMBLY_LINE] = @NextRecord
                     AND PLANT = @Plant
                     AND ASSEMBLY_LINE = @ASSEMBLY_LINE
                     IF (@NewKNR=@KNR)
						BEGIN
							SET @i = 0
							WHILE @i < @checkSize - @OldCurrentTakt 
								BEGIN
                                 SELECT @CurrentKNR = KNR ,
                                        @CarSequence = CURRENT_TAKT + 1 ,
                                        @CurrentTakt = CURRENT_TAKT ,
                                        @SequenceNumber = [SEQUENCE_NUMBER]
                                 FROM   [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
                                 WHERE  [SEQUENCE_ASSEMBLY_LINE] = @NextRecord
                                 AND PLANT=@PLANT
                                 AND ASSEMBLY_LINE=@ASSEMBLY_LINE
								 
								 SET @TMPCurrentTakt=@CurrentTakt
								 
                                 IF ( @CurrentTakt < @checkSize ) 
                                    BEGIN
                                          INSERT    INTO [LES].[TT_PCS_VECHICLE_MOVEMENT]
                                                    (
                                                      [KNR] ,
                                                      [REGION_IDENTITY] ,
                                                      [REGION_NAME] ,
                                                      [FOOTPRINT] ,
                                                      [ARRIVAL_TIME_STAMP] ,
                                                      [CONSUMED_TIME_STAMP] ,
                                                      [PERMANENT_KNR_INDEX] ,
                                                      [PLANT] ,
                                                      [ASSEMBLY_LINE]
                                                    )
                                                    SELECT  @CurrentKNR ,
                                                            @RegionNameIdentity ,
                                                            [REGION_NAME] ,
                                                            CURRENT_TAKT + 1 ,
                                                            GETDATE() ,
                                                            NULL ,
                                                            NULL ,
                                                            PLANT ,
                                                            ASSEMBLY_LINE
                                                    FROM    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
                                                    WHERE   SEQUENCE_ASSEMBLY_LINE = @NextRecord
                                                    AND PLANT=@PLANT
                                                    AND ASSEMBLY_LINE=@ASSEMBLY_LINE

                                          UPDATE    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
                                          SET       CURRENT_TAKT = CURRENT_TAKT + 1
                                          WHERE     [SEQUENCE_NUMBER] = @SequenceNumber
                                          
                                          SELECT @TMPCurrentTakt=CURRENT_TAKT FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
                                          WHERE SEQUENCE_NUMBER=@SequenceNumber
                                    END
                                 SET @i = @i + 1
								END
						END
                     ELSE
						BEGIN
							IF(@NewTakt<=@TMPCurrentTakt)
								BEGIN
									--找出当前车移位信息
									DECLARE @PreviewVechileTakt INT
									SELECT @PreviewVechileTakt=MAX(CURRENT_TAKT) FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
									WHERE KNR=@KNR
									AND PLANT=@PLANT
									AND ASSEMBLY_LINE=@ASSEMBLY_LINE
									SET @TMPCurrentTakt=@NewTakt
									--如果前车已经到了当前校验点的起始节拍
									IF(@NewTakt>=@checkSize)
										BEGIN
											SET @j=0
											DECLARE @TmpDiff INT =0
											SET @TmpDiff=@PreviewVechileTakt-@NewTakt
											WHILE @j <= @TmpDiff
												BEGIN
													IF (@TMPCurrentTakt<@MainRegionSize)
														BEGIN
															--插入消耗信息
															INSERT    INTO [LES].[TT_PCS_VECHICLE_MOVEMENT]
																(
																  [KNR] ,
																  [REGION_IDENTITY] ,
																  [REGION_NAME] ,
																  [FOOTPRINT] ,
																  [ARRIVAL_TIME_STAMP] ,
																  [CONSUMED_TIME_STAMP] ,
																  [PERMANENT_KNR_INDEX] ,
																  [PLANT] ,
																  [ASSEMBLY_LINE]
																)
															SELECT  KNR ,
																	@RegionNameIdentity ,
																	@MainRegionName,--@NewRegionName ,
																	CURRENT_TAKT + 1 ,
																	GETDATE() ,
																	NULL ,
																	NULL ,
																	PLANT ,
																	ASSEMBLY_LINE
															FROM    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
															WHERE   SEQUENCE_NUMBER=@NewSequenceNumber	
														END
													
													--更新当前位移信息
													UPDATE    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
													SET       CURRENT_TAKT = CURRENT_TAKT + 1
													WHERE     [SEQUENCE_NUMBER] = @NewSequenceNumber
							                        
													SELECT @KNR=KNR,@TMPCurrentTakt=CURRENT_TAKT FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
													WHERE SEQUENCE_NUMBER = @NewSequenceNumber
							                        
													SET @j=@j+1
												END
										END
									--前车还没到当前校验点的起始节拍，表示前车漏了
									--那么前车漏了的节拍也要处理
									ELSE
										BEGIN
											SET @k=0
											WHILE (@k < @checkSize - @OldCurrentTakt)
												BEGIN
													IF(@TMPCurrentTakt<@MainRegionSize)
														BEGIN
															--插入消耗信息
															INSERT    INTO [LES].[TT_PCS_VECHICLE_MOVEMENT]
																(
																  [KNR] ,
																  [REGION_IDENTITY] ,
																  [REGION_NAME] ,
																  [FOOTPRINT] ,
																  [ARRIVAL_TIME_STAMP] ,
																  [CONSUMED_TIME_STAMP] ,
																  [PERMANENT_KNR_INDEX] ,
																  [PLANT] ,
																  [ASSEMBLY_LINE]
																)
															SELECT  KNR ,
																	@RegionNameIdentity ,
																	@MainRegionName,--@NewRegionName ,
																	CURRENT_TAKT + 1 ,
																	GETDATE() ,
																	NULL ,
																	NULL ,
																	PLANT ,
																	ASSEMBLY_LINE
															FROM    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
															WHERE   SEQUENCE_NUMBER=@NewSequenceNumber	
														END
													
													--更新当前节拍信息
													UPDATE    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
													SET       CURRENT_TAKT = CURRENT_TAKT + 1
													WHERE     [SEQUENCE_NUMBER] = @NewSequenceNumber
							                        
													SELECT @KNR=KNR,@TMPCurrentTakt=CURRENT_TAKT FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
													WHERE SEQUENCE_NUMBER = @NewSequenceNumber
							                        
													SET @k=@k+1
												END
										END
								END
							ELSE
								RETURN
						END
                     SELECT @NextRecord = MAX([SEQUENCE_ASSEMBLY_LINE])
                     FROM   [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
                     WHERE  [SEQUENCE_ASSEMBLY_LINE] < @NextRecord
                     AND PLANT = @Plant
                     AND ASSEMBLY_LINE = @ASSEMBLY_LINE
               END
         --清除已下线的车的计算信息
		 DELETE FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
		 WHERE CURRENT_TAKT>TOTAL_TAKT
   END
IF ( @@error != 0 ) 
   BEGIN
         EXEC master.dbo.xp_logevent 75000,
            'Production Pull - Update of table VehicleMoveEngine for single Knr failed in MoveVehicles'
         SELECT @RollBack = 1
   END
	
RETURN (@RollBack)