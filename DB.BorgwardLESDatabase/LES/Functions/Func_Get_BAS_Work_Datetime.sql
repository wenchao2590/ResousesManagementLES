/************************************************/
/* Author:		xuehaijun						*/
/* Create date: 10/10/2011						*/
/* Description:	<Get Work Datetime>				*/
/* reference from SP_BAS_Get_Work_Datetime		*/
/************************************************/
CREATE FUNCTION [LES].[Func_Get_BAS_Work_Datetime]
(
	@plant NVARCHAR(5),
	@assemblyLine NVARCHAR(20),
	@starttime DATETIME,
	@diftime INT
)
RETURNS DATETIME
AS
BEGIN
	DECLARE @worktime DATETIME
	DECLARE @default_not_found_time DATETIME
	DECLARE @mi_count INT 
	SET @default_not_found_time = '1900-01-1'

	--设置返回值	
	SET @worktime = DATEADD(MI, @diftime, @starttime)
    --如果开始时间不在工作时间内，则返回1900-01-01
	IF EXISTS
	(
		SELECT 1 FROM [LES].[TM_BAS_WORK_SCHEDULE] WITH (NOLOCK)
		WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine AND [DAY_TYPE] = 1
		AND [BEGIN_TIME] <= @starttime AND [END_TIME] > @starttime
	)
		BEGIN
			--需求时间与在途时间的差>0时，实际需求时间在下一个工作时间内
			SELECT @worktime = MIN([END_TIME]) 
			FROM (SELECT DISTINCT [PLANT], [ASSEMBLY_LINE], [BEGIN_TIME], [END_TIME] FROM [LES].[TM_BAS_WORK_SCHEDULE] WITH (NOLOCK) WHERE [DAY_TYPE] = 1) AS a 
			WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine
			AND [BEGIN_TIME] <= @starttime AND [END_TIME] > @starttime
		
			SET @mi_count = DATEDIFF(MI, @starttime, @worktime)
		END
	ELSE
		BEGIN
			SELECT @worktime = @starttime
			SET @mi_count = 0
		END
   
	--如果需求时间在工作时间内，则从开始时间累加计算到需求时间，遇到非工作时间跳过不计算
	WHILE(@diftime - @mi_count > 0)
		BEGIN
			SET @diftime = @diftime - @mi_count
			IF EXISTS
			(
				SELECT 1
				FROM (SELECT DISTINCT [PLANT], [ASSEMBLY_LINE], [BEGIN_TIME], [END_TIME] FROM [LES].[TM_BAS_WORK_SCHEDULE] WITH (NOLOCK) WHERE [DAY_TYPE] = 1) AS a  
				WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine AND [BEGIN_TIME] >= @worktime
			)
				BEGIN
					--找出下一个工作时间的开始
					SELECT @starttime = MIN([BEGIN_TIME])
					FROM (SELECT DISTINCT [PLANT], [ASSEMBLY_LINE], [BEGIN_TIME], [END_TIME] FROM [LES].[TM_BAS_WORK_SCHEDULE] WHERE [DAY_TYPE] = 1) a  
					WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine AND [BEGIN_TIME] >= @worktime

					--找出下一个工作时间的结束
					SELECT @worktime = MIN([END_TIME])
					FROM (SELECT DISTINCT [PLANT], [ASSEMBLY_LINE], [BEGIN_TIME], [END_TIME] FROM [LES].[TM_BAS_WORK_SCHEDULE] WHERE [DAY_TYPE] = 1) a  
					WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine
					AND [BEGIN_TIME] <= @starttime AND [END_TIME] > @starttime			
			
					--计算这个工作时间的跨度
					SET @mi_count = DATEDIFF(MI, @starttime, @worktime)
				END
			ELSE
				BEGIN
					--计算的时间不在工作时间内，则返回1900-01-01
					SET @worktime = @default_not_found_time
					BREAK
				END
		END
	    
	IF (@worktime <> @default_not_found_time)
		BEGIN
			SET @worktime = DATEADD(MI, @diftime, @starttime)
		END
	ELSE
		BEGIN
			SET @worktime = DATEADD(MI, @diftime, @starttime)
		END

	RETURN @worktime
END