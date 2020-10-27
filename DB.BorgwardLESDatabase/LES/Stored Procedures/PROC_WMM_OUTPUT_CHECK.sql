




-- =============================================
-- Author:		Andy Liu
-- Create date: 2015-10-15
-- Description:	检查零件库存是否足够出库
-- =============================================
CREATE PROC [LES].[PROC_WMM_OUTPUT_CHECK] 
	@OUTPUT_ID int
AS

--select stocks_num,B.ZONE_NO,B.PART_NO,B.ACTUAL_QTY from
--(
--	select sum(stocks_num) stocks_num,SUM(FROZEN_STOCKS) FROZEN_STOCKS,PART_NO,ZONE_NO
--	from LES.TT_WMS_STOCKS 
--	group by ZONE_NO,PART_NO
--) A INNER JOIN LES.TT_WMM_OUTPUT_DETAIL B ON B.PART_NO = A.PART_NO AND B.ZONE_NO = A.ZONE_NO
--where (A.stocks_num-A.FROZEN_STOCKS) < B.ACTUAL_QTY and B.output_id = @OUTPUT_ID

select stocks_num,B.ZONE_NO,B.PART_NO,B.ACTUAL_QTY from
(
	select sum(stocks_num) stocks_num,SUM(FROZEN_STOCKS) FROZEN_STOCKS,PART_NO,ZONE_NO
	from LES.TT_WMS_STOCKS
	group by ZONE_NO,PART_NO
) A 
INNER JOIN 
(
	select sum(ACTUAL_QTY) ACTUAL_QTY,ZONE_NO,PART_NO,output_id
	from LES.TT_WMM_OUTPUT_DETAIL
	group by ZONE_NO,PART_NO,output_id
) B  ON B.PART_NO = A.PART_NO AND B.ZONE_NO = A.ZONE_NO
where (A.stocks_num-A.FROZEN_STOCKS) < B.ACTUAL_QTY and B.output_id = @OUTPUT_ID
and A.ZONE_NO NOT IN(select ZONE_NO from [LES].[TM_WMM_ZONES] where IS_NEGATIVE=1)