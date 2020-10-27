
-- =============================================
-- Author:		luchao
-- Create date: 2011-11-23
-- Description:	删除一个状态点前所做的检测
-- 检查JIS零件类下是否有该状态点关联
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_VEHICLE_STATUS]
	@Plant varchar(5),
	@VehicleStatus varchar(15),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 检查JIS零件类下是否有该状态点关联
	SELECT @Count=COUNT(1) FROM LES.TM_JIS_RACK WITH(NOLOCK) WHERE PLANT=@Plant 
			and (CACULATE_POINT=@VehicleStatus or ARRANGEMENT_POINT=@VehicleStatus or CACULATE_CHECK_POINT=@VehicleStatus)
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '工厂' + @Plant + '状态点' + @VehicleStatus + '下面存在JIS零件类关联字段信息，不能删除。'
		RETURN
	END
END