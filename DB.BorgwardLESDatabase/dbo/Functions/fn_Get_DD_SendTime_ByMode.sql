
-- =============================================
-- Author:		Ben Wang
-- Create date: March 12,2008
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_Get_DD_SendTime_ByMode]
(
	 @PLANT VARCHAR(10),
	 @WORKSHOP VARCHAR(10),
     @WINDOWDATE DATETIME,
     @ONLINE_TIME INT
)
RETURNS DATETIME
AS
BEGIN

    --如果窗口时间不在工作时间内，则返回NULL
	IF(NOT EXISTS(SELECT * FROM TS_BAS_WORKSCHEDULE 
           WHERE plant=@PLANT AND workshop=@WORKSHOP AND work_schedule_type=1 
                AND start_time<=@WINDOWDATE AND end_time>=@WINDOWDATE
          )
    )
	RETURN NULL
	
   
	--如果窗口时间在工作时间内，则从窗口时间点回溯在途时间，遇到非工作时间跳过不计算

	--工作时间段的开始时间
	DECLARE @START_TIME DATETIME 

    SELECT @START_TIME=MIN(start_time) 
	FROM  (SELECT DISTINCT plant,workshop,start_time,end_time FROM TS_BAS_WORKSCHEDULE WHERE work_schedule_type=1) a 
	WHERE plant=@PLANT AND workshop=@WORKSHOP
	AND start_time<=@WINDOWDATE AND end_time>=@WINDOWDATE

	--窗口时间与 该窗口时间属于的工作时间段的开始时间 之差，>=0
	DECLARE @MI_COUNT INT 
    SET @MI_COUNT=DATEDIFF(MI,@START_TIME,@WINDOWDATE)

	WHILE(@ONLINE_TIME-@MI_COUNT>0)
	BEGIN
		SET @ONLINE_TIME=@ONLINE_TIME-@MI_COUNT
		IF(EXISTS(
				   SELECT *  
				   FROM   (SELECT DISTINCT plant,workshop,start_time,end_time FROM TS_BAS_WORKSCHEDULE WHERE work_schedule_type=1) a  
				   WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP 
						 AND END_TIME<=@START_TIME
				  )
		   )
		BEGIN
			
			SELECT @WINDOWDATE=MAX(END_TIME)
			FROM   (SELECT DISTINCT plant,workshop,start_time,end_time FROM TS_BAS_WORKSCHEDULE WHERE work_schedule_type=1) a  
			WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP 
			AND END_TIME<=@START_TIME

			SELECT @START_TIME=MIN(START_TIME)
			FROM   (SELECT DISTINCT plant,workshop,start_time,end_time FROM TS_BAS_WORKSCHEDULE WHERE work_schedule_type=1) a  
			WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP
			AND START_TIME<=@WINDOWDATE AND END_TIME>=@WINDOWDATE
          
            SET @MI_COUNT=DATEDIFF(MI,@START_TIME,@WINDOWDATE)
		END
		ELSE
		BEGIN
			SET @WINDOWDATE = NULL
			BREAK
		END
	END
	    
	IF(@WINDOWDATE IS NOT NULL)
		SET @WINDOWDATE = DATEADD(MI,-@ONLINE_TIME,@WINDOWDATE)
	
	RETURN @WINDOWDATE

END