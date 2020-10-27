﻿CREATE PROC [LES].[PROC_WMM_NOTIFICATION_RESULT_TEMP_CHECK]
AS
BEGIN
	BEGIN TRANSACTION;

	BEGIN TRY

	UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '盘点通知单号为空;'
	WHERE NOTIFICATION_NO IS NULL or LEN([NOTIFICATION_NO]) < 1

	
	UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '盘点通知单单号不存在;'
	WHERE not EXISTS
	(
		SELECT 1 FROM LES.TT_WMM_NOTIFICATION_HEAD R (NOLOCK)
		WHERE R.NOTIFICATION_NO = [LES].TE_WMM_NOTIFICATION_RESULT_TEMP.NOTIFICATION_NO 
		AND R.COUNT_STATUS <=2
	)

	UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '盘点通知单已实盘完成;'
	WHERE EXISTS
	(
		SELECT 1 FROM LES.TT_WMM_NOTIFICATION_HEAD R (NOLOCK)
		WHERE R.NOTIFICATION_NO = [LES].TE_WMM_NOTIFICATION_RESULT_TEMP.NOTIFICATION_NO 
		AND R.COUNT_STATUS >= 3
	)
		
	UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '实盘数量为正整数;'
	WHERE ISNUMERIC(REAL_NUM_TEXT)!=1 
	UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	SET REAL_NUM=CAST(REAL_NUM_TEXT AS NUMERIC)
	WHERE ISNUMERIC(REAL_NUM_TEXT)=1 

	--UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	--SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '盘点通知类型不正确;'
	--WHERE [COUNT_TYPE] NOT IN ('1','2')

	--UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	--SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '盘点紧急程度不正确;'
	--WHERE [EMERGENCY_TYPE] NOT IN ('1','2')

	UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号及供应商与盘点单号不匹配;'
	WHERE NOT EXISTS 
	(
		SELECT 1 FROM 
		LES.TT_WMM_NOTIFICATION_HEAD M(NOLOCK)
		LEFT JOIN [LES].TE_WMM_NOTIFICATION_RESULT_TEMP T (NOLOCK) ON M.NOTIFICATION_NO = T.NOTIFICATION_NO
		LEFT JOIN LES.TT_WMM_NOTIFICATION_DETAIL D(NOLOCK) ON M.NOTIFICATION_ID = D.NOTIFICATIONID
		WHERE D.PART_NO = T.PART_NO AND ISNULL(D.[SUPPLIER_NUM], '') = ISNULL(T.[SUPPLIER_NUM], '')
	)

	UPDATE [LES].TE_WMM_NOTIFICATION_RESULT_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号及供应商重复;'
	WHERE PART_NO IN (
		SELECT tmp.PART_NO FROM 
			(SELECT NOTIFICATION_NO, PART_NO, COUNT(*) AS NUM FROM [LES].[TE_WMM_NOTIFICATION_RESULT_TEMP] WHERE PART_NO IS NOT NULL GROUP BY NOTIFICATION_NO, PART_NO, SUPPLIER_NUM) tmp 
		WHERE tmp.NUM > 1)

	END TRY
	BEGIN CATCH
		SELECT 
			ERROR_NUMBER() AS ErrorNumber
			,ERROR_SEVERITY() AS ErrorSeverity
			,ERROR_STATE() AS ErrorState
			,ERROR_PROCEDURE() AS ErrorProcedure
			,ERROR_LINE() AS ErrorLine
			,ERROR_MESSAGE() AS ErrorMessage;

		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
	END CATCH;

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION;
END