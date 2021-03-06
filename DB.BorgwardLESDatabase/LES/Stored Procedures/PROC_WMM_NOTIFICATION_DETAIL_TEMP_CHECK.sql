﻿

CREATE PROC [LES].[PROC_WMM_NOTIFICATION_DETAIL_TEMP_CHECK]
AS
	BEGIN TRANSACTION;

	BEGIN TRY

	UPDATE [LES].TE_WMM_NOTIFICATION_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '盘点通知单号为空;'
	WHERE NOTIFICATION_NO IS NULL or LEN(NOTIFICATION_NO) < 1

	UPDATE [LES].TE_WMM_NOTIFICATION_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '盘点通知单已被确认不可再修改;'
	WHERE EXISTS
	(
		SELECT 1 FROM LES.TT_WMM_NOTIFICATION_HEAD R 
		WHERE R.NOTIFICATION_NO = [LES].TE_WMM_NOTIFICATION_DETAIL_TEMP.NOTIFICATION_NO 
		AND R.CONFIRM_FLAG <> 0
	)

	UPDATE [LES].TE_WMM_NOTIFICATION_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号长度超出;'
	WHERE LEN([PART_NO]) > 20

	UPDATE [LES].TE_WMM_NOTIFICATION_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号不正确或与该存储区域不匹配;'
	WHERE [PART_NO] NOT IN 
	(
		SELECT S.[PART_NO]  FROM 
		LES.TM_BAS_PARTS_STOCK S(NOLOCK), 
		LES.TT_WMM_NOTIFICATION_HEAD R(NOLOCK), 
		LES.TE_WMM_NOTIFICATION_DETAIL_TEMP T(NOLOCK) 
		WHERE R.NOTIFICATION_NO = T.NOTIFICATION_NO AND
		 R.PLANT = S.PLANT AND R.WM_NO = S.WM_NO AND R.ZONE_NO = S.ZONE_NO
	)

	UPDATE [LES].TE_WMM_NOTIFICATION_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号重复;'
	WHERE PART_NO IN (
		SELECT tmp.PART_NO FROM 
			(SELECT NOTIFICATION_NO, PART_NO, COUNT(*) AS NUM FROM [LES].TE_WMM_NOTIFICATION_DETAIL_TEMP WHERE PART_NO is not NULL GROUP BY NOTIFICATION_NO, PART_NO) tmp 
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