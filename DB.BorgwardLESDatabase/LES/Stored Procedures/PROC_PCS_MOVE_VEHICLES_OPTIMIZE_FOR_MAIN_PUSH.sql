
/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  MoveVehicles                                    */
/*   Called By:     ProcessVehicleMovement                          */
/*   Purpose:       PCS主推点车辆位移推动计算逻辑，把需要消耗的数据放在消耗表中 */
/*    Author:       yinxuefeng                                      */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_MOVE_VEHICLES_OPTIMIZE_FOR_MAIN_PUSH]
      @Knr VARCHAR(20) ,
      @RegionNameIdentity INTEGER ,
      @RegionSize INTEGER ,
      @Plant VARCHAR(5) ,
      @AssemblyLine VARCHAR(10)
AS 
SET Nocount ON

DECLARE @RollBack INTEGER
DECLARE @Region_Order INTEGER
DECLARE @NextRecord INT
DECLARE @FOOTPRINT INT
DECLARE @CarSequence INT
DECLARE @CurrentTakt INT
DECLARE @TMPCurrentTakt INT
DECLARE @CurrentKNR VARCHAR(20)
DECLARE @PreviewVechileTakt INT
DECLARE @SequenceNumber INT
DECLARE @MainRegionSize INT
SELECT  @RollBack = 0

SELECT  @NextRecord = 0
SELECT  @CarSequence = 1
SET @PreviewVechileTakt=0
SET @TMPCurrentTakt = 0

--从序列里找到当前车的移位信息
SELECT  @NextRecord = SEQUENCE_ASSEMBLY_LINE,@MainRegionSize=ISNULL(TOTAL_TAKT,0)
FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
WHERE   PLANT = @Plant
AND ASSEMBLY_LINE = @AssemblyLine
AND KNR = @Knr
		
WHILE ( @NextRecord IS NOT NULL ) 
      BEGIN		
            PRINT @NextRecord
            SELECT TOP 1
                    @CurrentKNR = KNR ,
                    @CarSequence = CURRENT_TAKT + 1 ,
                    @SequenceNumber = [SEQUENCE_NUMBER]
            FROM    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
            WHERE   [SEQUENCE_ASSEMBLY_LINE] = @NextRecord
            AND PLANT = @Plant
            AND ASSEMBLY_LINE = @AssemblyLine
            ORDER BY TIMESTAMP DESC
            PRINT @CurrentKNR
		
            SELECT  @CurrentTakt = ISNULL(MAX(ISNULL(FOOTPRINT, 0)), 0)
            FROM    [LES].[TT_PCS_VECHICLE_MOVEMENT]
            WHERE   [KNR] = @CurrentKNR
            AND PLANT = @Plant
            AND ASSEMBLY_LINE = @AssemblyLine
            
            SET @TMPCurrentTakt = @CurrentTakt

            IF ( @CarSequence > @TMPCurrentTakt ) 
               BEGIN
					IF(@TMPCurrentTakt <= @PreviewVechileTakt)
						BEGIN
							WHILE (@TMPCurrentTakt <= @PreviewVechileTakt)
								BEGIN
									IF (@TMPCurrentTakt < @MainRegionSize)
										BEGIN
											INSERT INTO [LES].[TT_PCS_VECHICLE_MOVEMENT]
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
											SELECT TOP 1
													@CurrentKNR ,
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
											AND PLANT = @Plant
											AND ASSEMBLY_LINE = @AssemblyLine
											ORDER BY TIMESTAMP DESC
										END
										
										UPDATE [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
										SET    CURRENT_TAKT = CURRENT_TAKT + 1
										WHERE  [SEQUENCE_NUMBER] = @SequenceNumber	
										
										--更新当前Takt
										SELECT    @TMPCurrentTakt = CURRENT_TAKT
                                        FROM      LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
                                        WHERE     SEQUENCE_NUMBER = @SequenceNumber
								END

							 SELECT @PreviewVechileTakt= CURRENT_TAKT FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
							 WHERE  SEQUENCE_NUMBER = @SequenceNumber
						END
					ELSE
						BREAK
               END

            SELECT  @NextRecord = MAX([SEQUENCE_ASSEMBLY_LINE])
            FROM    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
            WHERE   [SEQUENCE_ASSEMBLY_LINE] < @NextRecord
            AND PLANT = @Plant
            AND ASSEMBLY_LINE = @AssemblyLine
      END
      --清除已下线的车的计算信息
	  DELETE FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
	  WHERE CURRENT_TAKT>TOTAL_TAKT

IF ( @@error != 0 ) 
   BEGIN
         EXEC master.dbo.xp_logevent 75000,
            'Production Pull - Update of table VehicleMoveEngine for single Knr failed in MoveVehicles'
         SELECT @RollBack = 1
   END
	
RETURN (@RollBack)