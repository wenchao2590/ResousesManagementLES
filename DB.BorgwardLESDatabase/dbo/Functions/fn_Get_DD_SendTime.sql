﻿
-- =============================================
-- Author:		Ben Wang
-- Create date: Aug 08,2007
-- Description:	<Description, ,>
-- =============================================
CREATE FUNCTION [dbo].[fn_Get_DD_SendTime]
(
	 @PLANT VARCHAR(10),
	 @WORKSHOP VARCHAR(10),
     @WINDOWDATE DATETIME,
     @ONLINE_TIME INT
)
RETURNS DATETIME
AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM TS_BAS_WORKSCHEDULE 
           WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP AND WORK_SCHEDULE_TYPE=1 
                AND START_TIME<=@WINDOWDATE AND END_TIME>=@WINDOWDATE
          )
    )
	
	BEGIN
--	   IF(EXISTS(
--					SELECT * FROM TS_BAS_WORKSCHEDULE 
--					WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP AND WORK_SCHEDULE_TYPE=1 
--					AND END_TIME<@WINDOWDATE
--				)
--		  )
--		  SELECT @WINDOWDATE=MAX(END_TIME)
--          FROM TS_BAS_WORKSCHEDULE 
--		  WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP AND WORK_SCHEDULE_TYPE=1 
--				AND END_TIME<@WINDOWDATE
--       ELSE
			RETURN NULL
	END
   

	DECLARE @MI_COUNT INT
	DECLARE @START_TIME DATETIME

	SELECT @MI_COUNT = DATEDIFF(MI,START_TIME,@WINDOWDATE),@START_TIME=START_TIME 
	FROM TS_BAS_WORKSCHEDULE 
	WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP AND WORK_SCHEDULE_TYPE=1 
	AND START_TIME<=@WINDOWDATE AND END_TIME>=@WINDOWDATE

	WHILE(@ONLINE_TIME-@MI_COUNT>0)
	BEGIN
		SET @ONLINE_TIME=@ONLINE_TIME-@MI_COUNT
		IF(EXISTS(
				   SELECT *  
				   FROM TS_BAS_WORKSCHEDULE 
				   WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP AND WORK_SCHEDULE_TYPE=1 
				   AND END_TIME<=@START_TIME
				  )
		   )
		BEGIN
			SELECT @WINDOWDATE=MAX(END_TIME)
			FROM TS_BAS_WORKSCHEDULE 
			WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP AND WORK_SCHEDULE_TYPE=1 
			   AND END_TIME<=@START_TIME

			SELECT @MI_COUNT = DATEDIFF(MI,MIN(START_TIME),@WINDOWDATE),@START_TIME=MIN(START_TIME)
			FROM TS_BAS_WORKSCHEDULE 
			WHERE PLANT=@PLANT AND WORKSHOP=@WORKSHOP AND WORK_SCHEDULE_TYPE=1 
			AND START_TIME<=@WINDOWDATE AND END_TIME>=@WINDOWDATE
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