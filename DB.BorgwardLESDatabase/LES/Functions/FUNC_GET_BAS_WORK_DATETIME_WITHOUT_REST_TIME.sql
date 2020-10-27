
-- =============================================
-- AUTHOR:		Yinxuefeng
-- CREATE DATE: 2013-10-29
-- DESCRIPTION:	获取指定时间后N分钟后的工作时间，排除非工作时间和休息时间
-- =============================================
CREATE FUNCTION [LES].[FUNC_GET_BAS_WORK_DATETIME_WITHOUT_REST_TIME]
(
	@PLANT VARCHAR(5),
	@ASSEMBLYLINE NVARCHAR(20),
	@STARTTIME DATETIME,
	@DIFTIME INT	 
)
RETURNS DATETIME
AS
BEGIN
	DECLARE @TmpDate DATETIME
	DECLARE @TmpBeginTime DATETIME
	DECLARE @TmpEndTime DATETIME
	DECLARE @CurBeginTime DATETIME
	DECLARE @CurEndTime DATETIME
	DECLARE @TmpTime DATETIME
	--工作时间临时表
	DECLARE @TmpWorkDateId TABLE
	(
		ID	INT IDENTITY(1,1) NOT NULL,
		DateId INT NOT NULL	
	)
	--休息时间临时表
	DECLARE @TmpRestDateId TABLE
	(
		ID	INT IDENTITY(1,1) NOT NULL,
		DateId INT NOT NULL		
	)
	
	--找出所有指定时间以后的工作时间
	INSERT INTO @TmpWorkDateId(DateId)
	SELECT ID FROM LES.TM_BAS_WORK_SCHEDULE
	WHERE PLANT=@PLANT
	AND ASSEMBLY_LINE=@ASSEMBLYLINE
	AND DAY_TYPE=1--工作时间类型 0:休息时间 1:工作时间
	AND END_TIME>@STARTTIME
	ORDER BY BEGIN_TIME ASC, END_TIME ASC
	
	
	--创建临时表存放分解后的工作时间
	DECLARE @TEMP_WORK_SCHEDULE TABLE
	(
		PLANT NVARCHAR(5) NOT NULL,
		ASSEMBLY_LINE NVARCHAR(10) NOT NULL,
		DATE DATETIME NOT NULL,
		BEGIN_TIME DATETIME NOT NULL,
		END_TIME DATETIME NOT NULL
	)
	
	DECLARE @NextRecord INT
	SET @NextRecord=0
	WHILE @NextRecord IS NOT NULL
		BEGIN
			SELECT @NextRecord=MIN(ID) FROM @TmpWorkDateId
			WHERE ID > @NextRecord
			IF @NextRecord IS NOT NULL
				BEGIN
					--找出第一个工作时间
					SELECT @TmpBeginTime=BEGIN_TIME,@TmpEndTime=END_TIME,@TmpDate=DATE 
					FROM LES.TM_BAS_WORK_SCHEDULE
					WHERE ID=(SELECT DateId FROM @TmpWorkDateId WHERE ID=@NextRecord)
					
					--找出此工作时间内的休息时间
					DELETE FROM @TmpRestDateId
					INSERT INTO @TmpRestDateId(DateId)
					SELECT ID FROM LES.TM_BAS_WORK_SCHEDULE
					WHERE PLANT=@PLANT
					AND ASSEMBLY_LINE=@ASSEMBLYLINE
					AND DAY_TYPE=0--工作时间类型 0:休息时间 1:工作时间
					AND BEGIN_TIME>=@TmpBeginTime
					AND END_TIME<=@TmpEndTime
					ORDER BY BEGIN_TIME ASC,END_TIME ASC
					
					DECLARE @RestCount INT
					SELECT @RestCount=COUNT(*) FROM @TmpRestDateId
					IF(@RestCount>0)
						BEGIN
							DECLARE @NextRestRecord INT
							SET @NextRestRecord=0
							WHILE @NextRestRecord IS NOT NULL
								BEGIN
									SELECT @NextRestRecord=MIN(ID) FROM @TmpRestDateId
									WHERE ID> @NextRestRecord
									IF(	@NextRestRecord IS NOT NULL)
										BEGIN
											SET @TmpTime=@TmpBeginTime
											SELECT @CurEndTime=BEGIN_TIME,@CurBeginTime=END_TIME  FROM LES.TM_BAS_WORK_SCHEDULE
											WHERE ID = (SELECT DateId FROM @TmpRestDateId WHERE ID=@NextRestRecord)
											
											UPDATE @TEMP_WORK_SCHEDULE SET END_TIME=@CurEndTime 
											WHERE END_TIME='1900-01-01'
											
											DECLARE @LastEndTime DATETIME
											SELECT @LastEndTime=ISNULL(MAX(END_TIME),'9999-01-01') FROM @TEMP_WORK_SCHEDULE
											WHERE DATE=@TmpDate
											
											IF(@CurEndTime<@LastEndTime)
												BEGIN
													INSERT INTO @TEMP_WORK_SCHEDULE
													(
													  PLANT ,
													  ASSEMBLY_LINE ,
													  DATE ,
													  BEGIN_TIME ,
													  END_TIME
													)
													VALUES  (
														  @PLANT , -- PLANT - nvarchar(5)
														  @ASSEMBLYLINE , -- ASSEMBLY_LINE - nvarchar(10)
														  @TmpDate , -- DATE - datetime
														  @TmpTime , -- BEGIN_TIME - datetime
														  @CurEndTime -- END_TIME - datetime
														)
												END
											
												
												INSERT INTO @TEMP_WORK_SCHEDULE
												(
												  PLANT ,
												  ASSEMBLY_LINE ,
												  DATE ,
												  BEGIN_TIME ,
												  END_TIME
												)
												VALUES  (
													  @PLANT , -- PLANT - nvarchar(5)
													  @ASSEMBLYLINE , -- ASSEMBLY_LINE - nvarchar(10)
													  @TmpDate , -- DATE - datetime
													  @CurBeginTime , -- BEGIN_TIME - datetime
													  '1900-01-01' -- END_TIME - datetime
													)
											
										END
									ELSE
										BEGIN
											UPDATE @TEMP_WORK_SCHEDULE SET END_TIME=@TmpEndTime
											WHERE END_TIME='1900-01-01'
										END	
								END	
						END
					ELSE
						BEGIN
							INSERT INTO @TEMP_WORK_SCHEDULE
										(
										  PLANT ,
										  ASSEMBLY_LINE ,
										  DATE ,
										  BEGIN_TIME ,
										  END_TIME
										)
										VALUES  (
											  @PLANT , -- PLANT - nvarchar(5)
											  @ASSEMBLYLINE , -- ASSEMBLY_LINE - nvarchar(10)
											  @TmpDate , -- DATE - datetime
											  @TmpBeginTime , -- BEGIN_TIME - datetime
											  @TmpEndTime -- END_TIME - datetime
											)
						END
				END
		END
	
	DECLARE @WORKTIME DATETIME
	DECLARE @DEFAULT_NOT_FOUND_TIME DATETIME
	DECLARE @MI_COUNT INT 
	SET @DEFAULT_NOT_FOUND_TIME = '1900-01-01'
	SET @WORKTIME = DATEADD(MI,@DIFTIME,@STARTTIME)

	IF EXISTS
	(
		SELECT * FROM @TEMP_WORK_SCHEDULE
		WHERE PLANT = @PLANT 
		AND [ASSEMBLY_LINE] = @ASSEMBLYLINE 
		AND [BEGIN_TIME] <= @STARTTIME AND END_TIME > @STARTTIME
	)
		BEGIN
			--需求时间与在途时间的差>0时，实际需求时间在下一个工作时间内
			SELECT @WORKTIME = MIN(END_TIME) 
			FROM  (
				SELECT DISTINCT PLANT,[ASSEMBLY_LINE],[BEGIN_TIME],END_TIME 
				FROM @TEMP_WORK_SCHEDULE
			) A 
			WHERE PLANT = @PLANT 
			AND [ASSEMBLY_LINE] = @ASSEMBLYLINE
			AND [BEGIN_TIME] <= @STARTTIME AND END_TIME > @STARTTIME
			
			SET @MI_COUNT = DATEDIFF(MI,@STARTTIME,@WORKTIME)
		END
	ELSE
		BEGIN
			SELECT @WORKTIME = @STARTTIME
			SET @MI_COUNT = 0
		END	
   
	--如果需求时间在工作时间内，则从开始时间累加计算到需求时间，遇到非工作时间跳过不计算
	WHILE(@DIFTIME - @MI_COUNT > 0)
	BEGIN
		SET @DIFTIME = @DIFTIME - @MI_COUNT
		IF EXISTS
		(
			SELECT *
			FROM (
				SELECT DISTINCT PLANT,[ASSEMBLY_LINE],[BEGIN_TIME],END_TIME 
				FROM @TEMP_WORK_SCHEDULE 
			) A  
			WHERE PLANT = @PLANT 
			AND [ASSEMBLY_LINE] = @ASSEMBLYLINE
			AND [BEGIN_TIME] >= @WORKTIME
		)
			BEGIN
				--找出下一个工作时间的开始
				SELECT @STARTTIME = MIN([BEGIN_TIME])
				FROM (
					SELECT DISTINCT PLANT,[ASSEMBLY_LINE],[BEGIN_TIME],END_TIME 
					FROM @TEMP_WORK_SCHEDULE 
				) A  
				WHERE PLANT = @PLANT 
				AND [ASSEMBLY_LINE] = @ASSEMBLYLINE
				AND [BEGIN_TIME] >= @WORKTIME
				
				--找出下一个工作时间的结束
				SELECT @WORKTIME = MIN(END_TIME)
				FROM (
					SELECT DISTINCT PLANT,[ASSEMBLY_LINE],[BEGIN_TIME],END_TIME 
					FROM @TEMP_WORK_SCHEDULE
				) A  
				WHERE PLANT = @PLANT 
				AND [ASSEMBLY_LINE] = @ASSEMBLYLINE
				AND [BEGIN_TIME] <= @STARTTIME AND END_TIME > @STARTTIME			
				
				--计算这个工作时间的跨度
				SET @MI_COUNT = DATEDIFF(MI,@STARTTIME,@WORKTIME)
			END
		ELSE
			BEGIN
				--计算的时间不在工作时间内，则返回1900-01-01
				SET @WORKTIME = @DEFAULT_NOT_FOUND_TIME
				BREAK
			END
	END
	    
	IF(@WORKTIME <> @DEFAULT_NOT_FOUND_TIME)
		BEGIN
			SET @WORKTIME = DATEADD(MI,@DIFTIME,@STARTTIME)
		END
	ELSE
		BEGIN
			SET @WORKTIME = NULL
		END
	DELETE FROM @TEMP_WORK_SCHEDULE
	RETURN @WORKTIME
END