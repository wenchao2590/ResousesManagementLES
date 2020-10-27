/**********************************************************************/
/*   Project Name:  TWD                                               */
/*   Program Name:  [LES].[PROC_TWD_CHECK_ONDELETING_TWD_BOX_PARTS]   */
/*   Called By:     by web page					   					  */
/*   Purpose:       删除一个TWD零件类前所做的检测					  */
/*   Author:        luchao                                            */
/**********************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_CHECK_ONDELETING_TWD_BOX_PARTS]
(
	@Plant NVARCHAR(5),
	@AssemblyLine NVARCHAR(10),
	@BoxParts NVARCHAR(10),
	@SupplierNum NVARCHAR(12),
	@Result INT OUTPUT,--检查结果，0表示成功，-1表示失败
	@ResultMessage NVARCHAR(1000) OUTPUT 	--结果消息
)
AS
BEGIN
	DECLARE @Count INT 
	SET @Count = 0
	SET @Result = 0
	SET NOCOUNT ON;
	
	-- 1.检查TWD窗口时间表中是否存在该TWD零件类数据
	SELECT @Count = COUNT(1) FROM [LES].[TT_TWD_SUPPLIER_SENDTIME] WITH(NOLOCK) WHERE [PLANT] = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [BOX_PARTS] = @BoxParts AND [SUPPLIER_NUM] = @SupplierNum
	IF @Count>0
		BEGIN
			SET @Result = -1
			SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' TWD零件类' + @BoxParts + ' 供应商' + @SupplierNum + '下面存在TWD窗口时间数据，不能删除。'
			RETURN
		END
	
	 -- 2.检查TWD拉动单管理(主表)中是否存在该TWD零件类数据
	SELECT @Count = COUNT(1) FROM [LES].[TT_TWD_RUNSHEET] WITH(NOLOCK) WHERE PLANT = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [BOX_PARTS] = @BoxParts AND [SUPPLIER_NUM] = @SupplierNum
	IF @Count > 0
		BEGIN
			SET @Result = -1
			SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' TWD零件类' + @BoxParts + ' 供应商' + @SupplierNum + '下面存在TWD拉动单数据，不能删除。'
			RETURN
		END
	
	-- 3.检查Inbound拉动数据表中是否存在该TWD零件类数据
	SELECT @Count = COUNT(1) FROM [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD] WITH(NOLOCK) WHERE [PLANT] = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [INBOUND_PART_CLASS] = @BoxParts AND [SUPPLIER_NUM] = @SupplierNum
	IF @Count>0
		BEGIN
			SET @Result = -1
			SET @ResultMessage = '工厂' + @Plant + ' 流水线' + @AssemblyLine + ' TWD零件类' + @BoxParts + ' 供应商' + @SupplierNum + '下面存在Inbound拉动数据，不能删除。'
			RETURN
		END
END