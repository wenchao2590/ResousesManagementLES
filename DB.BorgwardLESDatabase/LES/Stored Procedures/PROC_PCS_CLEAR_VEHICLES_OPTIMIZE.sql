
/********************************************************************/
/*                                                                  */
/*   Project Name:  PCS					                            */
/*   Program Name:  PROC_PCS_CLEAR_VEHICLES_OPTIMIZE                */
/*   Called By:     ProcessVehicleMovement                          */
/*   Purpose:       删除已下线的车的消耗信息                        */
/*    Author:       yinxuefeng                                      */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_CLEAR_VEHICLES_OPTIMIZE]
       @PLANT NVARCHAR(5) ,
       @ASSEMBLY_LINE NVARCHAR(10)
AS 
SET NOCOUNT ON

DECLARE @RegionSize INT

SELECT  @RegionSize=REGION_SIZE
FROM    LES.TM_PCS_REGION
WHERE   PLANT = @PLANT
AND ASSEMBLY_LINE = @ASSEMBLY_LINE
AND RECALCULATE_FLAG = 1
        
IF(@RegionSize IS NULL)
	BEGIN
		SELECT  @RegionSize=REGION_SIZE
		FROM    LES.TM_PCS_REGION
		WHERE   PLANT = @PLANT
        AND ASSEMBLY_LINE = @ASSEMBLY_LINE
        AND RECALCULATE_FLAG = 2	
	END

IF(@RegionSize IS NULL)
	BEGIN
		RETURN
	END
 
BEGIN TRY
      DELETE FROM LES.TT_PCS_VECHICLE_MOVEMENT
      WHERE KNR IN ( 
		SELECT DISTINCT KNR
        FROM     [LES].[TT_PCS_VECHICLE_MOVEMENT](NOLOCK)
        WHERE    (
			CONSUMED_TIME_STAMP IS NOT NULL
            AND [REGION_IDENTITY] IN(
										SELECT REGION_IDENTITY FROM LES.TM_PCS_REGION
										WHERE   PLANT = @PLANT
										AND ASSEMBLY_LINE = @ASSEMBLY_LINE
                                      )
            AND [FOOTPRINT] = @RegionSize
        )
      )
END TRY
BEGIN CATCH
  
--记录错误信息
      INSERT    INTO [LES].[TS_SYS_EXCEPTION]
                (
                  time_stamp ,
                  [application] ,
                  [METHOD] ,
                  class ,
                  exception_message ,
                  error_code
                )
                SELECT  GETDATE() ,
                        'PCS' ,
                        'PROC_PCS_MOVE_VEHICLES' ,
                        'Procedure' ,
                        ERROR_MESSAGE() ,
                        ERROR_LINE()
END CATCH