
/********************************************************************/
/*   Project Name:  JIS						                         */
/*   Program Name:  [PROC_JIS_GET_SUPPLY_FILE_DATA]                           */
/*   Called By:     供应商A000结果查询                                 */
/*    Author:       陈刚                                      */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_JIS_GET_SUPPLY_FILE_DATA]
(
	@PLANT	NVARCHAR(5),
	@ASSEMBLY_LINE	NVARCHAR(10),
	@SUPPLY_NO	NVARCHAR(20)
)
AS
BEGIN
		SELECT a.[PREVIEW_DATA_SN]
		 ,b.KNR
      ,a.[PLANT]
      ,a.[ASSEMBLY_LINE]
      ,a.[SUPPLIER_NUM]
      ,a.[RACK]
      ,a.[ITEM_NUMBER_TYPE]
      ,a.[MODEL]
      ,a.[MODEL_NO]
	  ,a.[VEHICLE_STATUS]
      ,a.[PART_NO]
      ,a.[PART_CNAME]
      ,a.[USAGE]
      ,a.[VIN]
      ,a.[MEASURING_UNIT_NO]
      ,b.[ORDER_NO]
      ,a.[PREVIEW_DATA_TIME]
      ,a.[COMMENTS]
  FROM [LES].[TT_ODS_INIT_PREVIEW_DATA] A
  inner join [LES].[TT_BAS_PULL_ORDERS] B ON A.[SIGNATURE]=B.[SIGNATURE]
  where A.[PLANT]=@PLANT and A.[ASSEMBLY_LINE]=@ASSEMBLY_LINE and A.[SUPPLIER_NUM]=@SUPPLY_NO
  and not exists(select [ORDER_ID] from [LES].[TI_VEHICLE_STATUS] C where B.[ORDER_NO]=C.[ORDER_ID] and [VEHICLE_STATUS]='A500')
  and B.ORDER_NO in (
select ORDER_ID from [LES].[TI_VEHICLE_STATUS] A where VEHICLE_STATUS='A500' and ORDER_ID in 
(select ORDER_ID from [LES].[TI_VEHICLE_STATUS] where VEHICLE_STATUS='A000' and datediff(day,a.pass_time,pass_time)=0) and datediff(day,a.pass_time,getdate())=1
)
END