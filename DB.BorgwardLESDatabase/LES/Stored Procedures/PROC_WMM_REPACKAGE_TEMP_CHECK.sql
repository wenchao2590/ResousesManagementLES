﻿
/********************************************************************/
/*                                                                  */
/*   Project Name:  LES System                          */
/*   Program Name:  [PROC_WMM_REPACKAGE_TEMP_CHECK]                       */
/*   Called By:     by the Page							*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       andy	2015-06-09   				       */
/********************************************************************/
CREATE PROC [LES].[PROC_WMM_REPACKAGE_TEMP_CHECK]
AS
	BEGIN TRANSACTION;

	
	BEGIN TRY

	--UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	--SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '翻包计划单号为空;'
	--WHERE REPACKAGE_NO IS NULL or LEN([REPACKAGE_NO]) < 1

	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '翻包单已被确认不可再修改;'
	WHERE REPACKAGE_TEMP_ID  IN
	(
		SELECT TE.REPACKAGE_TEMP_ID FROM LES.TT_WMM_REPACKAGE_HEAD R (NOLOCK)
		INNER JOIN [LES].TE_WMM_REPACKAGE_TEMP TE ON R.REPACKAGE_NO = TE.REPACKAGE_NO 
		 WHERE R.COUNT_STATUS IN (1,2)
	)

	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '工厂不正确;'
	WHERE [PLANT] NOT IN (SELECT [PLANT] FROM [LES].[TM_BAS_PLANT])

	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号不正确或与该存储区域不匹配;'
	WHERE REPACKAGE_TEMP_ID NOT IN
	(
		SELECT T.REPACKAGE_TEMP_ID FROM 
		LES.TM_BAS_PARTS_STOCK S(NOLOCK), 
		LES.TE_WMM_REPACKAGE_TEMP T(NOLOCK) 
		WHERE 
		 T.PLANT = S.PLANT AND T.WM_NO = T.WM_NO AND T.ZONE_NO = S.ZONE_NO
		 AND S.[PART_NO] = T.PART_NO
	)

	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '零件号重复;'
	WHERE PART_NO IN (
		SELECT tmp.PART_NO FROM 
			(SELECT REPACKAGE_NO, PART_NO, COUNT(*) AS NUM FROM [LES].[TE_WMM_REPACKAGE_TEMP] WHERE PART_NO is not NULL GROUP BY REPACKAGE_NO, PART_NO) tmp 
		WHERE tmp.NUM > 1)

	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '仓库编码不正确;'
	WHERE [WM_NO] NOT IN (SELECT [WAREHOUSE] FROM [LES].[TM_BAS_WAREHOUSE])

	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '存储区编码不正确;'
	WHERE [ZONE_NO] NOT IN (SELECT [ZONE_NO] FROM [LES].[TM_WMM_ZONES])

	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '仓库与存储区关联不正确;'
	WHERE REPACKAGE_TEMP_ID NOT IN (SELECT  REPACKAGE_TEMP_ID FROM [LES].TE_WMM_REPACKAGE_TEMP M JOIN 
    LES.TM_WMM_ZONES Z ON M.WM_NO=Z.WM_NO AND M.ZONE_NO=Z.ZONE_NO)

	UPDATE A
	SET A.VALID_FLAG = 0, A.ERROR_MSG = ERROR_MSG + '翻包路径不正确;'
	FROM [LES].TE_WMM_REPACKAGE_TEMP A
	WHERE A.REPACKAGE_ROUTE NOT IN (select B.[ROUTE] from les.TM_BAS_ROUTE B where B.route_type = 4 and B.PLANT = A.PLANT)
	
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '工厂与第一行不同;'
	WHERE [PLANT]  <>
	(
		select top 1 [PLANT] from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '仓库与第一行不同;'
	WHERE WM_NO  <>
	(
		select top 1 WM_NO from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '存储区与第一行不同;'
	WHERE ZONE_NO  <>
	(
		select top 1 ZONE_NO from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '翻包人员与第一行不同;'
	WHERE BOOK_KEEPER  <>
	(
		select top 1 BOOK_KEEPER from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '计划时间与第一行不同;'
	WHERE REPACKAGE_TIME  <>
	(
		select top 1 REPACKAGE_TIME from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '翻包路径与第一行不同;'
	WHERE REPACKAGE_ROUTE  <>
	(
		select top 1 REPACKAGE_ROUTE from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '拣选开始时间与第一行不同;'
	WHERE REPACKAGE_PICKUP_TIME  <>
	(
		select top 1 REPACKAGE_PICKUP_TIME from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '投棚结束时间与第一行不同;'
	WHERE REPACKAGE_PICKUP_ETIME  <>
	(
		select top 1 REPACKAGE_PICKUP_ETIME from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '计划翻包开始时间与第一行不同;'
	WHERE REPACKAGE_BTIME  <>
	(
		select top 1 REPACKAGE_BTIME from [LES].TE_WMM_REPACKAGE_TEMP
	)
	UPDATE [LES].TE_WMM_REPACKAGE_TEMP
	SET VALID_FLAG = 0, ERROR_MSG = ERROR_MSG + '计划翻包结束时间与第一行不同;'
	WHERE REPACKAGE_ETIME  <>
	(
		select top 1 REPACKAGE_ETIME from [LES].TE_WMM_REPACKAGE_TEMP
	)

SELECT *FROM 
	[LES].[TE_WMM_REPACKAGE_TEMP] TE,
	[LES].[TT_WMM_REPACKAGE_HEAD] TT
WHERE 
	TE.VALID_FLAG = 1 
	AND TE.REPACKAGE_NO = TT.REPACKAGE_NO 
	AND (TT.COUNT_STATUS = 0 OR TT.COUNT_STATUS IS NULL)

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