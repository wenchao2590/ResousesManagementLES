/************************************************/
/* Author:		Andy Liu						*/
/* Create date: 2015-07-03						*/
/* Description:	SAP零件信息下发					*/
/* Modify Date: 2017-09-21 孙述霄				*/
/************************************************/
CREATE PROCEDURE [LES].[PROC_INTERFACE_SAP_LES_PART]
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		--定义自增长序号临时表
		DECLARE @SelfNo TABLE
		(
			[SEQ_ID] BIGINT
		)

		INSERT INTO @SelfNo
		SELECT
			[SEQ_ID]
		FROM [LES].[TI_BAS_SAP_PARTS] WITH (NOLOCK)
		WHERE [PROCESS_FLAG] IS NULL OR [PROCESS_FLAG] <> 1

		IF EXISTS (SELECT TOP 1 [SEQ_ID] FROM @SelfNo)
			BEGIN
				--更新接口表标识
				UPDATE [LES].[TI_BAS_SAP_PARTS] WITH (ROWLOCK)
				SET [PROCESS_FLAG] = 1,
					[PROCESS_TIME] = GETDATE()
				WHERE [SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)

				--已同一零件最新的数据作为更新依据
				DELETE FROM @SelfNo
				WHERE [SEQ_ID] IN
				(
					SELECT
						[SEQ_ID]
					FROM [LES].[TI_BAS_SAP_PARTS] WITH (NOLOCK)
					WHERE [SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
					AND [MATNR] IN
					(
						SELECT
							[MATNR]
						FROM [LES].[TI_BAS_SAP_PARTS] WITH (NOLOCK)
						WHERE [SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
						GROUP BY [MATNR]
						HAVING COUNT(1) > 1
					)
					AND [SEQ_ID] NOT IN
					(
						SELECT
							MAX([SEQ_ID])
						FROM [LES].[TI_BAS_SAP_PARTS] WITH (NOLOCK)
						WHERE [SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
						GROUP BY [MATNR]
						HAVING COUNT(1) > 1
					)
				)

				--更新现有零件信息
				UPDATE A
				SET	A.[PLANT] = B.[WERKS],
					A.[PART_ENAME] = B.[MAKTX_EN],
					A.[PART_CNAME] = B.[MAKTX_ZH],
					A.[PART_UNITS] = B.[MEINS],
					A.[PART_GROUP] = B.[MTART],
					A.[PART_WEIGHT] = B.[BRGEW],
					A.[PART_PURCHASER] = B.[EKGRP],
					A.[INBOUND_PACKAGE_MODEL] = B.[ZYSQJC],
					A.[PACKAGE_MODEL] = B.[ZYSQJC],
					A.[PACKAGE] = B.[BSTRF],
					A.[INBOUND_PACKAGE] = B.[BSTRF],
					A.[STOCK_FEE] = B.[ZYSBZ],
					A.[INHOUSE_PACKAGE_MODEL] = B.[ZXBQJC],
					A.[INHOUSE_PACKAGE] = B.[ZXBQJQ],
					A.[PACKAGE_FEE] = B.[ZXBBZ],
					A.[MRP_TYPE] = B.[DISMM],
					A.[MRP_CONTROL] = B.[DISPO],
					A.[MRP_GROUP] = B.[DISGR],
					A.[SUPPLY_STYLE] = B.[BESKZ],
					A.[ORDER_BATCH] = B.[BSTMI],
					A.[ORDER_ADVANCE] = B.[PLIFZ],
					A.[DOCK] = B.[ZDOCK],
					A.[TRANS_FEE] = B.[ZYSFY],
					A.[REORDER_POINT] = B.[MINBE],
					A.[DELETE_FLAG] = (CASE WHEN B.[MSTAE] = 1 THEN 1 ELSE 0 END),
					A.[LOGICAL_PK] = (B.[WERKS] + B.[MATNR])
				FROM [LES].[TM_BAS_MAINTAIN_PARTS] A WITH (ROWLOCK)
				INNER JOIN [LES].[TI_BAS_SAP_PARTS] B WITH (NOLOCK) ON B.[MATNR] = A.[PART_NO]
				WHERE B.[SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)

				--新增不存在的零件信息
				INSERT INTO [LES].[TM_BAS_MAINTAIN_PARTS]
				(
					[PLANT],
					[PART_NO],
					[PART_ENAME],
					[PART_CNAME],
					[PART_UNITS],
					[PART_GROUP],
					[PART_WEIGHT],
					[PART_PURCHASER],
					[INBOUND_PACKAGE_MODEL],
					[PACKAGE_MODEL],
					[PACKAGE],
					[INBOUND_PACKAGE],
					[STOCK_FEE],
					[INHOUSE_PACKAGE_MODEL],
					[INHOUSE_PACKAGE],
					[PACKAGE_FEE],
					[MRP_TYPE],
					[MRP_CONTROL],
					[MRP_GROUP],
					[SUPPLY_STYLE],
					[ORDER_BATCH],
					[ORDER_ADVANCE],
					[DOCK],
					[TRANS_FEE],
					[REORDER_POINT],
					[DELETE_FLAG],
					[LOGICAL_PK],
					[PART_CLS],
					[PART_STATE],
					[CREATE_USER],
					[CREATE_DATE]
				)
				SELECT
					[WERKS],
					[MATNR],
					[MAKTX_EN],
					[MAKTX_ZH],
					[MEINS],
					[MTART],
					[BRGEW],
					[EKGRP],
					[ZYSQJC],
					[ZYSQJC],
					[BSTRF],
					[BSTRF],
					[ZYSBZ],
					[ZXBQJC],
					[ZXBQJQ],
					[ZXBBZ],
					[DISMM],
					[DISPO],
					[DISGR],
					[BESKZ],
					[BSTMI],
					[PLIFZ],
					[ZDOCK],
					[ZYSFY],
					[MINBE],
					(CASE WHEN [MSTAE] = 1 THEN 1 ELSE 0 END),
					([WERKS] + [MATNR]),--LOGICAL_PK,
					1,		--默认值：参与SAP计算
					1,		--默认值：已使用
					'admin',
					GETDATE()
				FROM [LES].[TI_BAS_SAP_PARTS] WITH (NOLOCK)
				WHERE [SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
				AND [MATNR] IS NOT NULL
				AND	[MATNR] NOT IN
				(
					SELECT
						[PART_NO] 
					FROM [LES].[TM_BAS_MAINTAIN_PARTS] WITH (NOLOCK)
				)

				--更新现有零件的仓库信息，对于收获库区，入库包装 = 标准包装
				UPDATE A
				SET	A.[PLANT] = B.[WERKS],
					A.[PART_ENAME] = B.[MAKTX_EN],
					A.[PART_CNAME] = B.[MAKTX_ZH],
					A.[PACKAGE_MODEL] = B.[ZYSQJC],				--标准包装器具编号
					A.[PACKAGE] = B.[BSTRF],					--标准包装数
					A.[INBOUND_PACKAGE_MODEL] = B.[ZYSQJC],		--入库包装器具编号
					A.[INBOUND_PACKAGE] = B.[BSTRF],			--入库包装数
					A.[INHOUSE_PACKAGE_MODEL] = B.[ZXBQJC],		--上线包装器具编号
					A.[INHOUSE_PACKAGE] = B.[ZXBQJQ],			--上线包装数
					A.[DELETE_FLAG] = (CASE WHEN B.[MSTAE] = 1 THEN 1 ELSE 0 END)
				FROM [LES].[TM_BAS_PARTS_STOCK] A WITH (ROWLOCK)
				INNER JOIN [LES].[TI_BAS_SAP_PARTS] B WITH (NOLOCK) ON B.[MATNR] = A.[PART_NO]
				INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[PLANT] = A.[PLANT] AND C.[WM_NO] = A.[WM_NO] AND C.[ZONE_NO] = A.[ZONE_NO]
				WHERE B.[SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo) AND C.[IS_MANAGE] = 10

				--更新现有零件的仓库信息，对于线边库区和线边配料区，入库包装 = 上线包装
				UPDATE A
				SET	A.[PLANT] = B.[WERKS],
					A.[PART_ENAME] = B.[MAKTX_EN],
					A.[PART_CNAME] = B.[MAKTX_ZH],
					A.[PACKAGE_MODEL] = B.[ZYSQJC],				--标准包装器具编号
					A.[PACKAGE] = B.[BSTRF],					--标准包装数
					A.[INBOUND_PACKAGE_MODEL] = B.[ZXBQJC],		--入库包装器具编号
					A.[INBOUND_PACKAGE] = B.[ZXBQJQ],			--入库包装数
					A.[INHOUSE_PACKAGE_MODEL] = B.[ZXBQJC],		--上线包装器具编号
					A.[INHOUSE_PACKAGE] = B.[ZXBQJQ],			--上线包装数
					A.[DELETE_FLAG] = (CASE WHEN B.[MSTAE] = 1 THEN 1 ELSE 0 END)
				FROM [LES].[TM_BAS_PARTS_STOCK] A WITH (ROWLOCK)
				INNER JOIN [LES].[TI_BAS_SAP_PARTS] B WITH (NOLOCK) ON B.[MATNR] = A.[PART_NO]
				INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[PLANT] = A.[PLANT] AND C.[WM_NO] = A.[WM_NO] AND C.[ZONE_NO] = A.[ZONE_NO]
				WHERE B.[SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
				AND (C.[IS_MANAGE] = 40 OR C.[IS_MANAGE] = 70)

				--更新现有零件的仓库信息，对于除收获库区、线边库区和线边配料区外的其它库区，如入库包装未维护则入库包装 = 标准包装，否则保持不变
				UPDATE A
				SET	A.[PLANT] = B.[WERKS],
					A.[PART_ENAME] = B.[MAKTX_EN],
					A.[PART_CNAME] = B.[MAKTX_ZH],
					A.[PACKAGE_MODEL] = B.[ZYSQJC],																												--标准包装器具编号
					A.[PACKAGE] = B.[BSTRF],																													--标准包装数
					A.[INBOUND_PACKAGE_MODEL] = (CASE WHEN ISNULL(A.[INBOUND_PACKAGE_MODEL], '') = '' THEN B.[ZYSQJC] ELSE A.[INBOUND_PACKAGE_MODEL] END),		--入库包装器具编号
					A.[INBOUND_PACKAGE] = (CASE WHEN ISNULL(A.[INBOUND_PACKAGE_MODEL], '') = '' THEN B.[BSTRF] ELSE A.[INBOUND_PACKAGE] END),					--入库包装数
					A.[INHOUSE_PACKAGE_MODEL] = B.[ZXBQJC],																										--上线包装器具编号
					A.[INHOUSE_PACKAGE] = B.[ZXBQJQ],																											--上线包装数
					A.[DELETE_FLAG] = (CASE WHEN B.[MSTAE] = 1 THEN 1 ELSE 0 END)
				FROM [LES].[TM_BAS_PARTS_STOCK] A WITH (ROWLOCK)
				INNER JOIN [LES].[TI_BAS_SAP_PARTS] B WITH (NOLOCK) ON B.[MATNR] = A.[PART_NO]
				INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[PLANT] = A.[PLANT] AND C.[WM_NO] = A.[WM_NO] AND C.[ZONE_NO] = A.[ZONE_NO]
				WHERE B.[SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo) AND C.[IS_MANAGE] <> 10 AND C.[IS_MANAGE] <> 40 AND C.[IS_MANAGE] <> 70

				--更新现有零件的仓库信息，对于超市料架区和线旁库，都更新成翻包后的包装数
				--UPDATE A
				--SET	A.[INBOUND_PACKAGE_MODEL] = B.[ZXBQJC],
				--	A.[PACKAGE_MODEL] = B.[ZXBQJC],
				--	A.[PACKAGE] = B.[ZXBQJQ],
				--	A.[INBOUND_PACKAGE] = B.[ZXBQJQ],
				--	A.[INHOUSE_PACKAGE_MODEL] = B.[ZXBQJC],
				--	A.[INHOUSE_PACKAGE] = B.[ZXBQJQ]
				--FROM [LES].[TM_BAS_PARTS_STOCK] A WITH (ROWLOCK)
				--INNER JOIN [LES].[TI_BAS_SAP_PARTS] B WITH (NOLOCK) ON B.[MATNR] = A.[PART_NO]
				--WHERE B.[SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
				--AND (A.[WM_NO] IN (8004,8006) OR A.[ZONE_NO] IN (2016,2026))
				
				--更新现有零件的拉动信息
				--UPDATE A
				--SET	A.[PLANT] = B.[WERKS],
				--	A.[PART_ENAME] = B.[MAKTX_EN],
				--	A.[PART_CNAME] = B.[MAKTX_ZH],
				--	A.[INBOUND_PACKAGE_MODEL] = B.[ZXBQJC],
				--	A.[DOCK] = B.[ZDOCK],
				--	A.[DELETE_FLAG] = (CASE WHEN B.[MSTAE] = 1 THEN 1 WHEN A.[DELETE_FLAG] = 0 THEN 0 ELSE 0 END)
				--FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] A WITH (ROWLOCK)
				--INNER JOIN [LES].[TI_BAS_SAP_PARTS] B WITH (NOLOCK) ON B.[MATNR] = A.[PART_NO]
				--WHERE B.[SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)

				--更新现有零件的拉动信息，拉动包装对应零件仓库信息中的入库包装
				UPDATE A
				SET	A.[PLANT] = B.[WERKS],
					A.[PART_ENAME] = B.[MAKTX_EN],
					A.[PART_CNAME] = B.[MAKTX_ZH],
					--A.[INBOUND_PACKAGE_MODEL] = C.[INBOUND_PACKAGE_MODEL],--jinmiao
					--A.[INBOUND_PACKAGE] = C.[INBOUND_PACKAGE],
					A.[DOCK] = B.[ZDOCK]
					--A.[DELETE_FLAG] = (CASE WHEN B.[MSTAE] = 1 THEN 1 WHEN A.[DELETE_FLAG] = 0 THEN 0 ELSE 0 END)
				FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] A WITH (ROWLOCK)
				INNER JOIN [LES].[TI_BAS_SAP_PARTS] B WITH (NOLOCK) ON B.[MATNR] = A.[PART_NO]
				INNER JOIN [LES].[TM_BAS_PARTS_STOCK] C WITH (NOLOCK) ON C.[PLANT] = A.[PLANT] AND C.[WM_NO] = A.[S_WM_NO] AND C.[ZONE_NO] = A.[S_ZONE_NO] AND C.[PART_NO] = A.[PART_NO]
				WHERE B.[SEQ_ID] IN (SELECT [SEQ_ID] FROM @SelfNo)
			END

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_INTERFACE_SAP_LES_PART]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END