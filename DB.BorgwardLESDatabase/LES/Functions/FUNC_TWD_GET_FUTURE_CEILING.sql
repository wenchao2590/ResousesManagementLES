CREATE FUNCTION [LES].[FUNC_TWD_GET_FUTURE_CEILING]
(
	@current_part_count NUMERIC,
	@inbound_package INT,
	@plant NVARCHAR(100),
	@assembly_line NVARCHAR(100),
	@part_no NVARCHAR(100),
	@part_class NVARCHAR(100)
)
RETURNS INT
AS
BEGIN
	DECLARE @start_date DATETIME
	DECLARE @end_date DATETIME
	DECLARE @preview_days INT
	DECLARE @ceiling_count INT
	DECLARE @future_count INT
	DECLARE @result INT

	SET @ceiling_count = CEILING(CONVERT(NUMERIC(18, 2), @current_part_count) / ISNULL(@inbound_package, 99999)) * ISNULL(@inbound_package, 99999)
	SET @result = @ceiling_count

	SELECT @preview_days = CONVERT(INT, [PARAMETER_VALUE]) FROM [LES].[TS_SYS_CONFIG] WITH (NOLOCK) WHERE [PARAMETER_NAME] = 'TWDPreviewDays'
	SET @preview_days = ISNULL(@preview_days, 0)

	IF @preview_days <= 1
		BEGIN
			RETURN @result
		END

	--SET @start_date = GETDATE()
	--select @end_date = MAX(plan_date) from 
	--(
	--	select top (@preview_days) convert(date,up_time) plan_date, count(*) plant_count from les.TI_VEHICLE_PLAN
	--	where up_time > @start_date
	--	group by convert(date,up_time)
	--	order by  convert(date,up_time) asc
	--) a
	--SET @end_date = DateAdd(DD, 1, @end_date)

	--SELECT @future_count = isnull(sum(pr.QUANTITY), 0)
	--FROM [LES].[TT_BAS_ORDER_PART_RESULTS] pr WITH (NOLOCK)
	--LEFT JOIN [LES].[TM_BAS_MAINTAIN_PULL_PARTS] pp WITH (NOLOCK) ON pp.PART_NO = pr.PART_NO
	--	AND pp.LOCATION = pr.LOCATION
	--	AND pp.PLANT = pr.PLANT
	--	AND pp.ASSEMBLY_LINE = pr.ASSEMBLY_LINE
	--WHERE pp.PULL_MODE = 'TWD'
	--	AND pp.PLANT = @plant
	--	AND pp.ASSEMBLY_LINE = @assembly_line
	--	AND pp.part_no = @part_no
	--	AND pp.PART_CLASS = @part_class
	--	AND pr.UP_DATETIME BETWEEN @start_date AND @end_date

	--SET @result = IIF((@current_part_count + @future_count) > @ceiling_count, @ceiling_count, (@current_part_count + @future_count))

	RETURN @result
END