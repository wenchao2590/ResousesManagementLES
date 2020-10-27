
-- =============================================
-- Author:		wangzhen 
-- Create date: 2007-9-28
-- Description:	获取期望到达时间。
--				对于看板扫描：期望到达时间等于当前时间 + 响应时间
--				对于暗灯:期望到达时间等于窗口时间
-- =============================================
CREATE FUNCTION [dbo].[Func_GetExpectedArrivalTime]
(
	-- Add the parameters for the function here
	@Plant varchar(2),
	@Workshop varchar(2),
	@PartType int,
	@Supplier varchar(3) 
)
RETURNS datetime
AS
BEGIN
	DECLARE @ReturnValue datetime
	SET @ReturnValue = GetDate()
	DECLARE @OnlineTime int
	SET @OnlineTime = 0
	SELECT @OnlineTime=online_time FROM TM_SUPPLIER_OnlineTime
	WHERE plant=@Plant and workshop=@Workshop and supplier=@Supplier
			and part_type=@PartType
	
	IF @OnlineTime<>0
	BEGIN
		SET @ReturnValue = Dateadd(minute,@OnlineTime,@ReturnValue)
	END
	RETURN @ReturnValue
END