﻿/********************************************************************/
/*                                                                  */
/*   Project Name:  Production Pull System                          */
/*   Program Name:  [PROC_BAS_CHECK_TE_BAS_TMP_PARTS_STOCKS]        */
/*   Called By:     by the Page										*/
/*   Purpose:       This is the main stored procedure for the       */
/*   author:       Andy Liu	2015-06-24   							*/
/********************************************************************/
CREATE PROC [LES].[PROC_BAS_CHECK_TE_BAS_TMP_PARTS_STOCKS]
AS
BEGIN
	BEGIN TRANSACTION

	BEGIN TRY
		DECLARE @IsCheckInboundPackage NVARCHAR(10)
		SELECT @IsCheckInboundPackage = [PARAMETER_VALUE] FROM [LES].[TS_SYS_CONFIG] WITH (NOLOCK) WHERE [PARAMETER_NAME] = 'IsCheckInboundPackage'

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '主键重复;'
		WHERE [LOGICAL_PK] IN (SELECT [LOGICAL_PK] FROM [LES].[TE_BAS_TMP_PARTS_STOCK] WITH (NOLOCK) GROUP BY [LOGICAL_PK] HAVING(COUNT(*)) > 1)	

		UPDATE A
		SET A.[VALID_FLAG] = 0, A.[ERROR_MSG] = [ERROR_MSG] + '对应工厂的零件编号不存在;'
		FROM [LES].[TE_BAS_TMP_PARTS_STOCK] A
		WHERE NOT EXISTS(SELECT * FROM [LES].[TM_BAS_MAINTAIN_PARTS] WITH (NOLOCK) WHERE A.[PLANT] = [PLANT] AND A.[PART_NO] = [PART_NO])

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '删除标记不正确;'
		WHERE [DELETE_FLAG] NOT IN (SELECT [DETAIL_VALUE] FROM [LES].[TC_SYS_CODE_DETAIL] WITH (NOLOCK) WHERE [CODE_NAME] = 'delete_flag')

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '仓库不正确;'
		WHERE [WM_NO] NOT IN (SELECT [WAREHOUSE] FROM [LES].[TM_BAS_WAREHOUSE] WITH (NOLOCK))

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '存储区不正确;'
		WHERE [ZONE_NO] NOT IN (SELECT [ZONE_NO] FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK))

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '库位不正确;'
		WHERE [DLOC] NOT IN (SELECT [DLOC] FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH (NOLOCK))

		UPDATE A 
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '工厂、仓库、存储区、库位不对应;'
		FROM [LES].[TE_BAS_TMP_PARTS_STOCK] A
		LEFT JOIN [LES].[TM_BAS_WAREHOUSE_LOCATION] B WITH (NOLOCK)
		on A.[PLANT] = B.[PLANT] AND A.[WM_NO] = B.[WM_NO] AND A.[ZONE_NO] = B.[ZONE_NO] AND A.[DLOC] = B.[DLOC]
		WHERE B.[DLOC] IS NULL

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '送货路径不正确;'
		WHERE ISNULL([ROUTE], '') <> '' AND [ROUTE] NOT IN (SELECT [ROUTE] FROM [LES].[TM_BAS_ROUTE] WITH (NOLOCK) WHERE [ROUTE_TYPE] = 2)

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '是否翻包不正确;'
		WHERE [IS_REPACK] NOT IN (SELECT [DETAIL_VALUE] FROM [LES].[TC_SYS_CODE_DETAIL] WITH (NOLOCK) WHERE [CODE_NAME] = 'is_repack')

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '翻包路径不正确;'
		WHERE [IS_REPACK] = '是' AND [REPACK_ROUTE] NOT IN (SELECT [ROUTE] FROM [LES].[TM_BAS_ROUTE] WITH (NOLOCK) WHERE [ROUTE_TYPE] = 4)

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '是否触发层级拉动不正确;'
		WHERE [IS_TRIGGER_PULL] NOT IN (SELECT [DETAIL_VALUE] FROM [LES].[TC_SYS_CODE_DETAIL] WITH (NOLOCK) WHERE [CODE_NAME] = 'is_trigger_pull')

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '层级拉动仓库不正确;'
		WHERE [IS_TRIGGER_PULL] = '是' AND [TRIGGER_WM_NO] NOT IN (SELECT [WAREHOUSE] FROM [LES].[TM_BAS_WAREHOUSE] WITH (NOLOCK))

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '层级拉动存储区不正确;'
		WHERE [IS_TRIGGER_PULL] = '是' AND [TRIGGER_ZONE_NO] NOT IN (SELECT [ZONE_NO] FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK))	

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '零件类别不正确;'
		WHERE [PART_CLS] NOT IN (SELECT [DETAIL_VALUE] FROM [LES].[TC_SYS_CODE_DETAIL] WITH (NOLOCK) WHERE [CODE_NAME] = 'part_category')

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '标准包装数不正确;'
		WHERE [PACKAGE] <= 0

		UPDATE A
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '存储区为收货库区时，入库包装器具编号必须为标准包装器具编号;'
		FROM [LES].[TE_BAS_TMP_PARTS_STOCK] A
		INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[WM_NO] = B.[WM_NO] AND A.[ZONE_NO] = B.[ZONE_NO]
		WHERE B.[IS_MANAGE] = 10 AND A.[INBOUND_PACKAGE_MODEL] <> A.[PACKAGE_MODEL]

		UPDATE A
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '存储区为收货库区时，入库包装数必须为标准包装数;'
		FROM [LES].[TE_BAS_TMP_PARTS_STOCK] A
		INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[WM_NO] = B.[WM_NO] AND A.[ZONE_NO] = B.[ZONE_NO]
		WHERE B.[IS_MANAGE] = 10 AND A.[INBOUND_PACKAGE] <> A.[PACKAGE]

		UPDATE A
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '存储区为线边库区或线边配料区时，入库包装器具编号必须为上线包装器具编号;'
		FROM [LES].[TE_BAS_TMP_PARTS_STOCK] A
		INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[WM_NO] = B.[WM_NO] AND A.[ZONE_NO] = B.[ZONE_NO]
		WHERE (B.[IS_MANAGE] = 40 OR B.[IS_MANAGE] = 70) AND A.[INBOUND_PACKAGE_MODEL] <> A.[INHOUSE_PACKAGE_MODEL]

		UPDATE A
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '存储区为线边库区或线边配料区时，入库包装数必须为上线包装数;'
		FROM [LES].[TE_BAS_TMP_PARTS_STOCK] A
		INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[WM_NO] = B.[WM_NO] AND A.[ZONE_NO] = B.[ZONE_NO]
		WHERE (B.[IS_MANAGE] = 40 OR B.[IS_MANAGE] = 70) AND A.[INBOUND_PACKAGE] <> A.[INHOUSE_PACKAGE]
		------------------------------------------------------
		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '价值分类不正确;'
		WHERE ISNULL([VALUE_SORT],'')<> ''--JINMIAO20171228
		AND [VALUE_SORT] NOT IN (SELECT [DETAIL_VALUE] FROM [LES].[TC_SYS_CODE_DETAIL] WITH (NOLOCK) WHERE [CODE_NAME] = 'value_sort')
		
		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '零件属性不正确;'
		WHERE ISNULL([PARTS_ATTRIBUTE],'')<> ''--JINMIAO20171228
		AND [PARTS_ATTRIBUTE] NOT IN (SELECT [DETAIL_VALUE] FROM [LES].[TC_SYS_CODE_DETAIL] WITH (NOLOCK) WHERE [CODE_NAME] = 'parts_attribute')
		--------------------------------------------------------------------

		UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
		SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '供应商编号不正确;'
		WHERE ISNULL([SUPPLIER_NUM],'') <> ''
		AND [SUPPLIER_NUM] NOT IN (SELECT [SUPPLIER_NUM] FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK) WHERE [SUPPLIER_TYPE] = '1')

		IF (ISNULL(@IsCheckInboundPackage, '0') <> '0')
			BEGIN
				UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
				SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '入库包装器具编号必须为标准包装器具编号或者上线包装器具编号;'
				WHERE [INBOUND_PACKAGE_MODEL] <> [PACKAGE_MODEL] AND [INBOUND_PACKAGE_MODEL] <> [INHOUSE_PACKAGE_MODEL]

				UPDATE [LES].[TE_BAS_TMP_PARTS_STOCK]
				SET [VALID_FLAG] = 0, [ERROR_MSG] = [ERROR_MSG] + '入库包装器数必须为标准包装数或者上线包装数;'
				WHERE [INBOUND_PACKAGE] <> [PACKAGE] AND [INBOUND_PACKAGE] <> [INHOUSE_PACKAGE]
			END
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION
	END CATCH

	IF @@TRANCOUNT > 0
		COMMIT TRANSACTION
END