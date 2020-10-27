/********************************************************************/
/*                                                                  */
/*   Project Name:  [PROC_PCS_GET_ORGANIZE_DATA]					*/
/*   Program Name:													*/
/*   Called By:     by the 	generate runsheet						*/
/*   Purpose:       get GET_ORGANIZE_DATA							*/
/*   author:       xuehaijun	2011-08-2   				       */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_PCS_GET_ORGANIZE_DATA]
AS
SELECT
	   C.[PLANT]
      ,C.[ASSEMBLY_LINE]
      ,B.[SUPPLIER_NUM]
      ,B.[DOCK]
      ,C.[BOX_PARTS]
  FROM [LES].[TT_PCS_DELIVERY_SCHEDULE] A with (nolock)
  inner join LES.TR_PCS_SCHEDULE_BOX_PART C with (nolock) on A.Schedule_identity=C.ScheduleID
  left join les.[TI_PCS_MATERIAL_REQUESTS] B with (nolock) on B.PLANT=C.PLANT AND B.ASSEMBLY_LINE=C.ASSEMBLY_LINE AND B.BOX_PARTS=C.BOX_PARTS
  AND  B.[IS_ORGANIZE_SHEET]=1  and B.[INTERFACE_STATUS]=0 
where A.[IS_DELIVERIED]=0
group by  C.[PLANT]
      ,C.[ASSEMBLY_LINE]
      ,B.[SUPPLIER_NUM]
      ,B.[DOCK]
      ,C.[BOX_PARTS]