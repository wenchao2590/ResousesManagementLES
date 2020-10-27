
-- =============================================
-- Author:		luchao
-- Create date: 2011-11-26
-- Description:	删除一个PCS_DCP扫描点前所做的检测
-- 检查给PCS_描点的关联
-- =============================================
CREATE PROCEDURE [LES].[PROC_PCS_CHECK_DATA]

(
	@PART_NO NVARCHAR(MAX)	
)

AS
BEGIN

if( not exists(select *from LES.TM_BAS_INHOUSE_PULL_LOGISTIC_STANDARD where part_no=@PART_NO and assembly_line='TFC3A2'))
begin
select '在拉动数据库不存在'
end

if( not exists(select *from LES.TT_PCS_COUNTER where part_no=@PART_NO and assembly_line='TFC3A2'))
select '在计数器中不存在'

if( not exists(select *from LES.TL_PCS_VECHILE_CONSUME_LOG where part_no=@PART_NO and assembly_line='TFC3A2'))
select '在拉动数据库不存在'
else
begin
select sum(dosage) from LES.TL_PCS_VECHILE_CONSUME_LOG where part_no=@PART_NO and assembly_line='TFC3A2'
end

if( not exists(select *from LES.TI_PCS_MATERIAL_REQUESTS where part_no=@PART_NO and assembly_line='TFC3A2'))
select '在传安吉请求中数据库不存在'
else
select sum(inhouse_package) from LES.TI_PCS_MATERIAL_REQUESTS where part_no=@PART_NO and assembly_line='TFC3A2'
END