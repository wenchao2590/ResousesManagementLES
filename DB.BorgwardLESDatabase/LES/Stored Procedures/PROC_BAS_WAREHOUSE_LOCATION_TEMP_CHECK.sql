﻿
CREATE PROC [LES].[PROC_BAS_WAREHOUSE_LOCATION_TEMP_CHECK]
AS
BEGIN
	BEGIN TRANSACTION

	BEGIN TRY

		DECLARE @IsDlocRepeat AS NVARCHAR(10)
		SELECT @IsDlocRepeat = [PARAMETER_VALUE] FROM [LES].[TS_SYS_CONFIG] WITH (NOLOCK) WHERE [PARAMETER_NAME] = 'IsDlocRepeat'

		UPDATE [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '工厂不正确;'
		WHERE [PLANT] NOT IN (SELECT [PLANT] FROM [LES].[TM_BAS_PLANT] WITH (NOLOCK))

		UPDATE [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '仓库编码不正确;'
		WHERE [WM_NO] NOT IN (SELECT [WAREHOUSE] FROM [LES].[TM_BAS_WAREHOUSE] WITH (NOLOCK))

		UPDATE [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '存储区编码不正确;'
		WHERE [ZONE_NO] NOT IN (SELECT [ZONE_NO] FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK))

		UPDATE [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '仓库与存储区关联不正确;'
		WHERE [ID] NOT IN (SELECT [ID] FROM [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP] M JOIN 
		[LES].[TM_WMM_ZONES] Z ON M.[PLANT]=Z.[PLANT] AND M.[WM_NO]=Z.[WM_NO] AND M.[ZONE_NO]=Z.[ZONE_NO])

		UPDATE [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '库位编码为空;'
		WHERE [DLOC] IS NULL OR LEN([DLOC]) < 1

		--IF @IsDlocRepeat = 0--jinmiao20171228
		--	BEGIN
		--		UPDATE A
		--		SET A.[VALID_FLAG] = 0,
		--			A.[ERROR_MSG] = [ERROR_MSG] + '库位编码重复;'
		--		FROM [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP] A WITH (ROWLOCK)
		--		WHERE EXISTS
		--		(
		--			SELECT 1 FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] B WITH (NOLOCK)
		--			WHERE B.[DLOC] = A.[DLOC] AND (B.[PLANT] <> A.[PLANT] OR B.[WM_NO] <> A.[WM_NO] OR B.[ZONE_NO] <> A.[ZONE_NO])
		--		)

		--		UPDATE A
		--		SET A.[VALID_FLAG] = 0,
		--			A.[ERROR_MSG] = [ERROR_MSG] + '库位编码重复;'
		--		FROM [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP] A WITH (ROWLOCK)
		--		WHERE EXISTS
		--		(
		--			SELECT 1 FROM [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP] B WITH (NOLOCK)
		--			WHERE B.[DLOC] = A.[DLOC] AND (B.[PLANT] <> A.[PLANT] OR B.[WM_NO] <> A.[WM_NO] OR B.[ZONE_NO] <> A.[ZONE_NO])
		--		)
		--	END

	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION
END