﻿CREATE PROC [LES].[PROC_WMM_RETURN_TEMP_CHECK]
AS
	BEGIN TRANSACTION;

	
	BEGIN TRY

	UPDATE [LES].TE_WMM_RETURN_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '退货单号为空;'
	WHERE RETURN_NO IS NULL or LEN([RETURN_NO]) < 1

	UPDATE [LES].TE_WMM_RETURN_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '退货单已被确认不可再修改;'
	WHERE EXISTS
	(
		SELECT 1 FROM LES.TT_WMM_RETURN R 
		WHERE R.RETURN_NO = [LES].TE_WMM_RETURN_TEMP.RETURN_NO 
		AND R.CONFIRM_FLAG IN (1,2)
	)

	--还需添加供应商数据合法性的检查
	UPDATE [LES].TE_WMM_RETURN_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '退货类型不正确;'
	WHERE [RETURN_TYPE_Text] NOT IN (SELECT DETAIL_VALUE FROM les.TC_SYS_CODE_DETAIL
	WHERE  CODE_NAME = 'wms_return_type')

	UPDATE [LES].TE_WMM_RETURN_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '供应商不正确;'
	WHERE NOT EXISTS 
	(
		SELECT 1 FROM LES.TM_BAS_SUPPLIER S (nolock) WHERE S.SUPPLIER_NUM = TE_WMM_RETURN_TEMP.SUPPLIER_NUM
	)
	
	
	UPDATE [LES].TE_WMM_RETURN_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '箱数格式错误;'
	WHERE ISNUMERIC(PART_COUNT_TEXT)=0
	
	UPDATE [LES].TE_WMM_RETURN_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '件数格式错误;'
	WHERE  ISNUMERIC(PACK_COUNT_TEXT)=0

	UPDATE t_Temp
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '存在重复记录;'
	FROM [LES].TE_WMM_RETURN_TEMP t_Temp
	WHERE EXISTS(SELECT 1 FROM LES.TT_WMM_RETURN t_Receive WHERE
	 t_Temp.PLANT=t_Receive.PLANT 
	 AND t_Receive.WM_NO=t_Temp.WM_NO
	 AND t_Receive.ZONE_NO=t_Temp.ZONE_NO
	 AND t_Receive.SUPPLIER_NUM=t_Temp.SUPPLIER_NUM
	 AND t_Receive.RETURN_TYPE=t_Temp.RETURN_TYPE
	 AND t_Receive.COMMENTS=t_Temp.COMMENTS
	 AND t_Receive.RETURN_REASON=t_Temp.RETURN_REASON)
	 
	UPDATE t_Temp
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '工厂、仓库、存储区没有关联;'
	FROM [LES].TE_WMM_RETURN_TEMP t_Temp
	WHERE not EXISTS(SELECT 1 FROM [LES].[TM_WMM_ZONES] t_ZONES WHERE
	 t_Temp.PLANT=t_ZONES.PLANT 
	 AND t_ZONES.WM_NO=t_Temp.WM_NO
	 AND t_ZONES.ZONE_NO=t_Temp.ZONE_NO)



	--DECLARE @Count INT 
	--SELECT DISTINCT RETURN_TYPE, PLANT, ZONE_NO,WM_NO,SUPPLIER_NUM
	--INTO #tmp FROM LES.TE_WMM_RETURN_TEMP T (NOLOCK)
 --	SELECT @Count = COUNT(*) FROM #tmp
	--DROP TABLE #tmp
	--IF(@Count > 1)
	--BEGIN 
	--	UPDATE [LES].TE_WMM_RETURN_TEMP 
	--	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '检测到工厂，仓库，存储区或退货类型等基础数据不一致;'
	--END

	--UPDATE [LES].TE_WMM_RETURN_TEMP
	--SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号不正确或与该存储区域不匹配;'
	--WHERE [PART_NO] NOT IN 
	--(
	--	SELECT S.[PART_NO]  FROM 
	--	LES.TM_BAS_PARTS_STOCK S(NOLOCK), 
	--	LES.TT_WMM_RETURN R(NOLOCK), 
	--	LES.TE_WMM_RETURN_TEMP T(NOLOCK) 
	--	WHERE R.RETURN_NO = T.RETURN_NO AND
	--	 R.PLANT = S.PLANT AND R.WM_NO = S.WM_NO AND R.ZONE_NO = S.ZONE_NO
	--)

	UPDATE [LES].TE_WMM_RETURN_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号不正确或与该存储区域不匹配;'
	WHERE NOT EXISTS 
	(
		SELECT 1 FROM 
		LES.TM_BAS_PARTS_STOCK S(NOLOCK), 
		LES.TE_WMM_RETURN_TEMP T(NOLOCK) 
		WHERE T.PLANT = T.PLANT AND T.WM_NO = S.WM_NO AND T.ZONE_NO = S.ZONE_NO
		 AND S.[PART_NO] = TE_WMM_RETURN_TEMP.PART_NO
	)

	UPDATE [LES].TE_WMM_RETURN_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号重复;'
	WHERE PART_NO IN (
		SELECT tmp.PART_NO FROM 
			(SELECT RETURN_NO, PART_NO, COUNT(*) AS NUM FROM [LES].[TE_WMM_RETURN_TEMP] WHERE PART_NO is not NULL GROUP BY RETURN_NO, PART_NO) tmp 
		WHERE tmp.NUM > 1)
		
	DECLARE @ErrCount int
	
	SELECT @ErrCount=COUNT(1) FROM [LES].TE_WMM_RETURN_TEMP WHERE VALID_FLAG = 0
	
	IF(@ErrCount=0)
	BEGIN
	
	UPDATE [LES].TE_WMM_RETURN_TEMP SET PART_COUNT=PART_COUNT_TEXT
	UPDATE [LES].TE_WMM_RETURN_TEMP SET PACK_COUNT=PACK_COUNT_TEXT

	UPDATE t_temp SET t_temp.RETURN_TYPE=t_Detail.DETAIL_CODE
	  FROM [LES].TE_WMM_RETURN_TEMP  t_temp
	LEFT JOIN [LES].[TC_SYS_CODE_DETAIL] t_Detail 
	ON t_Detail.[CODE_NAME] = 'wms_return_type'
	AND t_temp.RETURN_TYPE_Text=t_Detail.DETAIL_VALUE

	END;

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