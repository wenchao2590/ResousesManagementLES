﻿CREATE PROCEDURE [LES].[PROC_WMM_RECEIVE_DETAIL_IMPORT]
AS
BEGIN
	DELETE FROM [LES].[TT_WMM_RECEIVE_DETAIL] WITH (ROWLOCK)
	WHERE [RECEIVE_DETAIL_ID] IN
	(
		SELECT DISTINCT
			[RECEIVE_DETAIL_ID]
		FROM [LES].[TT_WMM_RECEIVE_DETAIL] TD WITH (NOLOCK)
		LEFT JOIN [LES].[TT_WMM_RECEIVE] TM WITH (NOLOCK) ON TM.[RECEIVE_ID] = TD.[RECEIVE_ID]
		LEFT JOIN [LES].[TE_WMM_RECEIVE_DETAIL_TEMP] (NOLOCK) T ON T.[RECEIVE_NO] = TM.[RECEIVE_NO]
		WHERE T.[PART_NO] = TD.[PART_NO] AND TM.[CONFIRM_FLAG] = 0
	)

	INSERT INTO [LES].[TT_WMM_RECEIVE_DETAIL]
	(
		[RECEIVE_ID],
		[PLANT],
		[WM_NO],
		[ZONE_NO],
		[SUPPLIER_NUM],
		[DLOC],
		[PACKAGE],
		[PACKAGE_MODEL],
		[BOX_PARTS],
		[PART_NO],
		[PART_CNAME],
		[PART_ENAME],
		--,PART_TYPE
		[REQUIRED_BOX_NUM],
		[REQUIRED_QTY],
		--,[ACTUAL_BOX_NUM]
		--,[ACTUAL_QTY]
		--,BOX_NUM
		--,NUM
		[Current_BOX_NUM],
		[Current_QTY],
		[CREATE_USER],
		[CREATE_DATE],
		[COMMENTS]
	)
	SELECT 
		TT.[RECEIVE_ID],
		TT.[PLANT],
		TT.[WM_NO],
		TT.[ZONE_NO],
		TT.[SUPPLIER_NUM],
		ST.[DLOC],
		ST.[INBOUND_PACKAGE],
		ST.[INBOUND_PACKAGE_MODEL],
		ST.[PART_CLS],
		TE.[PART_NO],
		ST.[PART_CNAME],
		ST.[PART_ENAME],
		--,TM.PART_CLS
		TE.[REQUIRED_BOX_NUM],
		TE.[REQUIRED_QTY],
		--,TE.[ACTUAL_BOX_NUM]
		--,TE.[ACTUAL_QTY]
		--,TE.[ACTUAL_BOX_NUM] AS BOX_NUM
		--,TE.[ACTUAL_QTY] AS NUM
		TE.[Current_BOX_NUM],
		TE.[Current_QTY],
		TE.[CREATE_USER],
		TE.[CREATE_DATE],
		TE.COMMENTS
	FROM [LES].[TE_WMM_RECEIVE_DETAIL_TEMP] TE WITH (NOLOCK) 
	JOIN [LES].[TT_WMM_RECEIVE] TT WITH (NOLOCK) ON TE.[RECEIVE_NO] = TT.[RECEIVE_NO]
	LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] ST WITH (NOLOCK) ON TT.[PLANT] = ST.[PLANT] AND TT.[WM_NO] = ST.[WM_NO] AND TT.[ZONE_NO] = ST.[ZONE_NO] AND TE.[PART_NO] = ST.[PART_NO]
END