
/********************************************************************/
/*   Project Name:  SPS                                               */
/*   Program Name:  [V_SPS_MODELQUERY]*/
/*   Called By:     sps按车辆查询                                 */
/*    Author:       mengli                                      */
/********************************************************************/
CREATE VIEW [LES].[V_SPS_MODELQUERY]
--WITH ENCRYPTION, SCHEMABINDING, VIEW_METADATA
AS
	SELECT  c.KNR ,
        a.VIN ,
        a.SPS_RUNSHEET_NO ,
        a.PLANT ,
        a.ASSEMBLY_LINE ,
        a.SUPPLIER_NUM ,
        a.BOX_PARTS ,
        MODEL_NO ,
        PART_NO ,
        PART_CNAME ,
        ACTUAL_INBOUND_PACKAGE_QTY ,
        a.DOCK ,
        PRINT_STATE ,
		SEND_STATUS ,
        SHEET_STATUS ,
        a.COMMENTS ,
        ACTUAL_ARRIVAL_TIME ,
        a.RUNSHEET_TYPE,
        TRIGGER_TYPE,
		REQUIRED_INBOUND_PACKAGE_QTY
    FROM    [LES].[TT_SPS_RUNSHEET] a WITH ( NOLOCK )
        INNER JOIN [LES].[TT_SPS_RUNSHEET_DETAIL] b WITH ( NOLOCK ) ON b.SPS_RUNSHEET_SN = a.SPS_RUNSHEET_SN
        INNER JOIN [LES].[TT_SPS_CALCULATE_POINT] c WITH ( NOLOCK ) ON c.VIN = a.VIN