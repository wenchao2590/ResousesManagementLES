-- =============================================
-- Author:		luchao
-- Create date: 2011-11-23
-- Description:	删除一个供应商前所做的检测
-- 检查JIS,PCS,TWD零件类和SPS配载线下是否有该供应商关联
-- =============================================
CREATE PROCEDURE [LES].[PROC_BAS_CHECK_ONDELETING_SUPPLIER]
	@SupplierNum varchar(20),
	@Result int output,--检查结果，0表示成功，-1表示失败
	@ResultMessage varchar(1000) output 	--结果消息
AS
BEGIN
	DECLARE @Count int 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
 
 -- 1.检查JIS零件类下是否有该供应商关联
	SELECT @Count=COUNT(1) FROM LES.TM_JIS_RACK WITH(NOLOCK) WHERE SUPPLIER_NUM=@SupplierNum 
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '供应商' + @SupplierNum + '下面存在JIS零件类信息，不能删除。'
		RETURN
	END
	
 --2.检查PCS零件类下是否有该供应商关联
	SELECT @Count=COUNT(1) FROM LES.TM_PCS_ROUTE_BOX_PARTS WITH(NOLOCK) WHERE SUPPLIER_NUM=@SupplierNum 
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '供应商' + @SupplierNum + '下面存在PCS零件类信息，不能删除。'
		RETURN
	END
	
  --3.检查TWD零件类下是否有该供应商关联
	SELECT @Count=COUNT(1) FROM LES.TM_TWD_BOX_PARTS WITH(NOLOCK) WHERE SUPPLIER_NUM=@SupplierNum 
	IF @Count>0
	BEGIN
		SET @Result = -1
		SET @ResultMessage = '供应商' + @SupplierNum + '下面存在TWD零件类信息，不能删除。'
		RETURN
	END
	
 -- --4.检查SPS配载线下是否有该供应商关联
	--SELECT @Count=COUNT(1) FROM LES.TM_SPS_PICKING_LINE WITH(NOLOCK) WHERE SUPPLIER_NUM=@SupplierNum 
	--IF @Count>0
	--BEGIN
	--	SET @Result = -1
	--	SET @ResultMessage = '供应商' + @SupplierNum + '下面存在SPS配载线信息，不能删除。'
	--	RETURN
	--END
END