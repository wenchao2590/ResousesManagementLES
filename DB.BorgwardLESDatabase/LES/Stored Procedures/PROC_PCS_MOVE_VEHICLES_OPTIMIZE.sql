
/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  MoveVehicles                                    */
/*   Called By:     ProcessVehicleMovement                          */
/*   Purpose:       PCS车辆位移推动计算逻辑，把需要消耗的数据放在消耗表中 */
/*    Author:       xuehaijun                                       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_MOVE_VEHICLES_OPTIMIZE]
      @Knr VARCHAR(20) ,
      @RegionNameIdentity INTEGER ,
      @RegionSize INTEGER ,
      @Plant VARCHAR(5) ,
      @AssemblyLine VARCHAR(10)
AS 
SET Nocount ON

DECLARE @RollBack INTEGER
DECLARE @PassTime DATE
DECLARE @Diff INTEGER

DECLARE @Region_Order INTEGER
DECLARE @NextRecord INT
DECLARE @FOOTPRINT INT
DECLARE @CarSequence INT
DECLARE @CurrentTakt INT
DECLARE @CurrentKNR VARCHAR(20)
DECLARE @PreviewVechileTakt INT
DECLARE @SequenceNumber INT
SELECT  @RollBack = 0

SELECT  @NextRecord = 0
SELECT  @CarSequence = 1
SET @PreviewVechileTakt=0

SELECT  @NextRecord = MAX([SEQUENCE_ASSEMBLY_LINE])
FROM    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
WHERE   PLANT = @Plant
AND ASSEMBLY_LINE = @AssemblyLine
		
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

            IF ( @CarSequence > @CurrentTakt AND @CarSequence<=@RegionSize) 
               BEGIN
					IF(@CurrentTakt<=@PreviewVechileTakt)
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

							 UPDATE [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
							 SET    CURRENT_TAKT = CURRENT_TAKT + 1
							 WHERE  [SEQUENCE_NUMBER] = @SequenceNumber
							 
							 SELECT @PreviewVechileTakt= CURRENT_TAKT FROM LES.TT_PCS_VECHILE_ITEM_BY_SEQUENCE
							 WHERE  SEQUENCE_NUMBER = @SequenceNumber
						END
               END

            IF ( @CarSequence > @RegionSize ) 
				BEGIN
					DELETE   FROM [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
					WHERE    [SEQUENCE_NUMBER] = @SequenceNumber
				END
            SELECT  @NextRecord = MAX([SEQUENCE_ASSEMBLY_LINE])
            FROM    [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE]
            WHERE   [SEQUENCE_ASSEMBLY_LINE] < @NextRecord
            AND PLANT = @Plant
            AND ASSEMBLY_LINE = @AssemblyLine
      END

IF ( @@error != 0 ) 
   BEGIN
         EXEC master.dbo.xp_logevent 75000,
            'Production Pull - Update of table VehicleMoveEngine for single Knr failed in MoveVehicles'
         SELECT @RollBack = 1
   END
	
RETURN (@RollBack)