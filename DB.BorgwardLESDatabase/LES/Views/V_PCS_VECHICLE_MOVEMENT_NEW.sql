
/********************************************************************/
/*   Project Name:  PCS                                               */
/*   Program Name:  [V_PCS_VECHICLE_MOVEMENT_NEW]*/
/*   Called By:                                       */
/*    Author:       yingxuefeng                                      */
/********************************************************************/
CREATE VIEW [LES].[V_PCS_VECHICLE_MOVEMENT_NEW]
AS
	SELECT	A.* , C.RUNNING_NO
	FROM	LES.TT_PCS_VECHICLE_MOVEMENT A,
	(select footprint, max(vehicle_movemen_tidentity) as vehicle_movemen_tidentity from LES.TT_PCS_VECHICLE_MOVEMENT
	group by footprint) B,
	LES.TT_PCS_VECHILE_ITEM C
	WHERE A.vehicle_movemen_tidentity = B.vehicle_movemen_tidentity AND A.KNR = C.KNR