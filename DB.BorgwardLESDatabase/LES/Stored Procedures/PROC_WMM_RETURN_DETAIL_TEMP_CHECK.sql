﻿
CREATE PROC [LES].[PROC_WMM_RETURN_DETAIL_TEMP_CHECK]
AS
	BEGIN TRANSACTION;

	
	BEGIN TRY

	UPDATE [LES].TE_WMM_RETURN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '退货单号为空;'
	WHERE RETURN_NO IS NULL or LEN([RETURN_NO]) < 1

	UPDATE [LES].TE_WMM_RETURN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '退货单已被确认不可再修改;'
	WHERE EXISTS
	(
		SELECT 1 FROM LES.TT_WMM_RETURN R 
		WHERE R.RETURN_NO = [LES].TE_WMM_RETURN_DETAIL_TEMP.RETURN_NO 
		AND R.CONFIRM_FLAG <> 0
	)

	UPDATE [LES].TE_WMM_RETURN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号长度超出;'
	WHERE LEN([PART_NO]) > 20

	UPDATE [LES].TE_WMM_RETURN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号不正确或与该存储区域不匹配;'
	WHERE [PART_NO] NOT IN 
	(
		SELECT S.[PART_NO]  FROM 
		LES.TM_BAS_PARTS_STOCK S(NOLOCK), 
		LES.TT_WMM_RETURN R(NOLOCK), 
		LES.TE_WMM_RETURN_DETAIL_TEMP T(NOLOCK) 
		WHERE R.RETURN_NO = T.RETURN_NO AND
		 R.PLANT = S.PLANT AND R.WM_NO = S.WM_NO AND R.ZONE_NO = S.ZONE_NO
	)

	UPDATE [LES].TE_WMM_RETURN_DETAIL_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号重复;'
	WHERE PART_NO IN (
		SELECT tmp.PART_NO FROM 
			(SELECT RETURN_NO, PART_NO, COUNT(*) AS NUM FROM [LES].TE_WMM_RETURN_DETAIL_TEMP WHERE PART_NO is not NULL GROUP BY RETURN_NO, PART_NO) tmp 
		WHERE tmp.NUM > 1)

	 UPDATE [LES].TE_WMM_RETURN_TEMP
	 SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '箱数和件数应该为正整数;'
	 WHERE PART_COUNT <= 0 OR PACK_COUNT <= 0

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