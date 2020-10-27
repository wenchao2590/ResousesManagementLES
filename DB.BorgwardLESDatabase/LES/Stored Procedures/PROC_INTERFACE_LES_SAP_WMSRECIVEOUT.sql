/********************************************************************/
/*   Program Name:  [LES].[PROC_INTERFACE_LES_SAP_WMSRECIVEOUT]		*/
/*   Called By:     window service									*/
/*   Modifier:      孙述霄											*/
/*   Modify date:	2017-11-22										*/
/*   Note:			更新RDC入库单同步到SAP接口表的存储过程			*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_INTERFACE_LES_SAP_WMSRECIVEOUT]
AS
BEGIN
	IF (NOT EXISTS(SELECT 1 FROM [LES].[TI_WMS_RECIVE_OUT] WITH (NOLOCK) WHERE ISNULL([DEAL_FLAG], 0) = 0))
		BEGIN
			--判断接口表是否有未处理数据  如果有则累计在 log表中不进行处理
			IF (EXISTS (SELECT 1 FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK) WHERE ISNULL([ERP_FLAG], 0) = 2 AND (ISNUMERIC([RECEIVE_NO]) = 1 OR [RUNSHEET_CODE] = 'JIS' OR [RUNSHEET_CODE] = 'TWD' OR [RUNSHEET_CODE] = 'CTD' OR [RUNSHEET_CODE] = 'ASN')) OR EXISTS (SELECT 1 FROM [LES].[TE_SPM_SAP_RUNSHEET] WITH (NOLOCK) WHERE ISNULL([PROCESS_STATUS], 0) = 0))
				BEGIN
					BEGIN TRANSACTION TA_RECEIVE
					BEGIN TRY

						--取出待处理的ID
						DECLARE @IDListReceive TABLE ([ID] BIGINT)
						DECLARE @IDListReceiveLog TABLE ([ID] BIGINT)
						DECLARE @IDRunsheet TABLE ([PLAN_RUNSHEET_SN] INT)

						INSERT INTO @IDListReceive
						SELECT TOP 50 [RECEIVE_ID] FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK)
						WHERE ISNULL([ERP_FLAG], 0) = 2 
						AND (ISNUMERIC([RECEIVE_NO]) = 1 OR [RUNSHEET_CODE] = 'JIS' OR [RUNSHEET_CODE] = 'TWD' OR [RUNSHEET_CODE] = 'CTD' OR [RUNSHEET_CODE] = 'ASN')
						ORDER BY [RECEIVE_ID] ASC

						--更新要操作的数据对应的状态
						UPDATE [LES].[TT_WMM_RECEIVE] WITH (ROWLOCK)
						SET [ERP_FLAG] = 3 
						WHERE [RECEIVE_ID] IN (SELECT [ID] FROM @IDListReceive)

						UPDATE [LES].[TT_WMM_RECEIVE_DETAIL_LOG] WITH (ROWLOCK)
						SET [ERP_FLAG] = 3 
						WHERE ISNULL([ERP_FLAG], 0) = 2 
						AND [RECEIVE_ID] IN (SELECT [ID] FROM @IDListReceive)
			
						INSERT INTO @IDListReceiveLog
						SELECT [RECEIVE_DETAIL_LOG_ID] FROM LES.[TT_WMM_RECEIVE_DETAIL_LOG] WITH (NOLOCK)
						WHERE ISNULL([ERP_FLAG], 0) = 3

						--获取关单数据
						INSERT INTO @IDRunsheet
						SELECT [PLAN_RUNSHEET_SN] FROM [LES].[TE_SPM_SAP_RUNSHEET] WITH (NOLOCK)
						WHERE ISNULL([PROCESS_STATUS], 0) = 0

						--插入普通收货数据（SAP下发的订单）
						INSERT INTO [LES].[TI_WMS_RECIVE_OUT]
						(
							[WERKS],
							[MATNR],
							[LGMNG],
							[LGORT],
							[BUDAT],
							[OPTIM],
							[OPRTR],
							[DEAL_FLAG],
							[PROCESS_FLAG],
							[VBELN],
							[LFART],
							[CREATE_DATE],
							[CREATE_USER],
							[LIFNR],
							[MEINS],
							[POSNR],
							[ZSERIAL],
							[DONE],
							[ASN]
						)
						SELECT
							d.[PLANT],
							d.[PART_NO],
							ISNULL(
							(
								SELECT
									SUM([NUM]) AS [NUM]
								FROM [LES].[TT_WMM_RECEIVE_DETAIL_LOG] l WITH (NOLOCK)
								WHERE l.[ERP_FLAG] = 3
								AND l.[RECEIVE_ID] = d.[RECEIVE_ID]
								AND l.[PART_NO] = d.[PART_NO]
							), 0),
							d.[ZONE_NO],
							w.[TRAN_TIME],
							w.[TRAN_TIME],
							w.[BOOK_KEEPER],
							0,
							0,
							w.[RUNSHEET_NO],
							s.[LFART],
							GETDATE(),
							'admin',
							d.[SUPPLIER_NUM],
							d.[MEASURING_UNIT_NO],
							s.[POSNR],
							[LES].[FN_GETRANDSTR](19),							
							CASE w.[CONFIRM_FLAG] WHEN 2 THEN 1 ELSE 0 END,--2017-12-20 jinmiao
							w.[RUNSHEET_NO]
						FROM [LES].[TT_WMM_RECEIVE_DETAIL] d WITH (NOLOCK)
						INNER JOIN [LES].[TT_WMM_RECEIVE] w WITH (NOLOCK) ON w.[RECEIVE_ID] = d.[RECEIVE_ID]
						INNER JOIN [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] s WITH (NOLOCK) ON s.[VBELN] = w.[RECEIVE_NO] AND s.[MATNR] = d.[PART_NO]
						LEFT JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET] r WITH (NOLOCK) ON w.[RECEIVE_NO] = r.[PLAN_RUNSHEET_NO]       
						WHERE w.[RECEIVE_ID] IN (SELECT [ID] FROM @IDListReceive)
						AND ISNUMERIC(w.[RECEIVE_NO]) = 1 
						AND ISNULL(w.[RUNSHEET_CODE], '') NOT IN ('JIS','TWD', 'CTD','ASN')

						--ASN收货同步（SAP下发的订单）
						INSERT INTO [LES].[TI_WMS_RECIVE_OUT]
						(
							[WERKS],
							[MATNR],
							[LGMNG],
							[LGORT],
							[BUDAT],
							[OPTIM],
							[OPRTR],
							[DEAL_FLAG],
							[PROCESS_FLAG],
							[VBELN],
							[LFART],
							[CREATE_DATE],
							[CREATE_USER],
							[LIFNR],
							[MEINS],
							[POSNR],
							[ZSERIAL],
							[DONE],
							[ASN]
						)
						SELECT
							d.[PLANT],
							d.[PART_NO],
							ISNULL(d.[ACTUAL_QTY], 0),
							d.[ZONE_NO],
							w.[TRAN_TIME],
							w.[TRAN_TIME],
							w.[BOOK_KEEPER],
							0,
							0,
							d.[TWD_RUNSHEET_NO],
							s.[LFART],
							GETDATE(),
							'admin',
							d.[SUPPLIER_NUM],
							d.[MEASURING_UNIT_NO],
							s.[POSNR],
							[LES].[FN_GETRANDSTR](19),
							CASE r.[SHEET_STATUS] WHEN 10 THEN 1 ELSE 0 END,
							w.[RUNSHEET_NO]
						FROM [LES].[TT_WMM_RECEIVE_DETAIL] d WITH (NOLOCK)
						INNER JOIN [LES].[TT_WMM_RECEIVE] w WITH (NOLOCK) ON w.[RECEIVE_ID] = d.[RECEIVE_ID]
						INNER JOIN [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] s WITH (NOLOCK) ON s.[VBELN] = d.[TWD_RUNSHEET_NO] AND s.[MATNR] = d.[PART_NO]
						LEFT JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET] r WITH (NOLOCK) ON d.[TWD_RUNSHEET_NO] = r.[PLAN_RUNSHEET_NO]
						WHERE w.[RECEIVE_ID] IN (SELECT [ID] FROM @IDListReceive)
						AND ISNUMERIC(d.[TWD_RUNSHEET_NO]) = 1
						AND w.[RUNSHEET_CODE] = 'ASN'
        
						--插入JIS、TWD、CTD、ASN供应商收货信息中，不涉及到寄售订单的数据——————提交默认 关单DONE=1
						INSERT INTO [LES].[TI_WMS_RECIVE_OUT]
						(
							[WERKS],
							[MATNR],
							[LGMNG],
							[LGORT],
							[BUDAT],
							[OPTIM],
							[OPRTR],
							[DEAL_FLAG],
							[PROCESS_FLAG],
							[VBELN],
							[LFART],
							[CREATE_DATE],
							[CREATE_USER],
							[LIFNR],
							[MEINS],
							[POSNR],
							[ZSERIAL],
							[DONE],
							[ASN]
						)
						SELECT
							d.[PLANT],
							d.[PART_NO],
							ISNULL(d.[ACTUAL_QTY], 0),
							d.[ZONE_NO],
							w.[TRAN_TIME],
							w.[TRAN_TIME],
							w.[BOOK_KEEPER],
							0,
							0,
							NULL AS RUNSHEET_NO,
							CASE w.[RUNSHEET_CODE]
							WHEN 'JIS' THEN 'ZJIT'
							WHEN 'TWD' THEN 'ZHIG'
							WHEN 'CTD' THEN 'ZHIG'
							WHEN 'ASN' THEN 'ZHIG'
							END AS [RUNSHEET_TYPE],
							GETDATE(),
							'admin',
							d.[SUPPLIER_NUM],
							d.[MEASURING_UNIT_NO],
							NULL AS [POSNR],
							[LES].[FN_GETRANDSTR](19),
							1,
							w.[RUNSHEET_NO]
						FROM [LES].[TT_WMM_RECEIVE_DETAIL] d WITH (NOLOCK)
						INNER JOIN [LES].[TT_WMM_RECEIVE] w WITH (NOLOCK) ON w.[RECEIVE_ID] = d.[RECEIVE_ID]
						LEFT JOIN [LES].[TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP] e WITH (NOLOCK) ON w.[RECEIVE_ID] = e.[RECEIVE_ID] AND d.[PART_NO] = e.[PART_NO]
						WHERE w.[RECEIVE_ID] IN (SELECT [ID] FROM @IDListReceive)
						AND ISNUMERIC(d.[TWD_RUNSHEET_NO]) <> 1 
						AND w.[RUNSHEET_CODE] IN ('JIS', 'TWD', 'CTD', 'ASN')
						AND e.[ID] IS NULL

						--插入JIS、TWD、CTD、ASN供应商收货信息中，涉及到寄售订单的数据——————提交默认 关单DONE=1
						INSERT INTO [LES].[TI_WMS_RECIVE_OUT]
						(
							[WERKS],
							[MATNR],
							[LGMNG],
							[LGORT],
							[BUDAT],
							[OPTIM],
							[OPRTR],
							[DEAL_FLAG],
							[PROCESS_FLAG],
							[VBELN],
							[LFART],
							[CREATE_DATE],
							[CREATE_USER],
							[LIFNR],
							[MEINS],
							[POSNR],
							[ZSERIAL],
							[DONE],
							[ASN]
						)
						SELECT
							d.[PLANT],
							d.[PART_NO],
							ISNULL(e.[STOCK_NUM], 0),
							d.[ZONE_NO],
							w.[TRAN_TIME],
							w.[TRAN_TIME],
							w.[BOOK_KEEPER],
							0,
							0,
							e.[RUNSHEET_NO],
							e.[LFART],
							GETDATE(),
							'admin',
							d.[SUPPLIER_NUM],
							d.[MEASURING_UNIT_NO],
							e.[ITEM_NO] AS [POSNR],
							[LES].[FN_GETRANDSTR](19),
							1,
							w.[RUNSHEET_NO]
						FROM [LES].[TT_WMM_RECEIVE_DETAIL] d WITH (NOLOCK)
						INNER JOIN [LES].[TT_WMM_RECEIVE] w WITH (NOLOCK) ON (w.[RECEIVE_ID] = d.[RECEIVE_ID]) 
						INNER JOIN [LES].[TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP] e WITH (NOLOCK) ON w.[RECEIVE_ID] = e.[RECEIVE_ID] AND d.[PART_NO] = e.[PART_NO]
						WHERE w.[RECEIVE_ID] IN (SELECT [ID] FROM @IDListReceive)
						AND ISNUMERIC(d.[TWD_RUNSHEET_NO]) <> 1
						AND w.[RUNSHEET_CODE] IN ('JIS', 'TWD', 'CTD', 'ASN')

						--插入关单数据（数量为0，DONE为1）
						INSERT INTO [LES].[TI_WMS_RECIVE_OUT]
						(
							[WERKS],
							[MATNR],
							[LGMNG],
							[LGORT],
							[BUDAT],
							[OPTIM],
							[OPRTR],
							[DEAL_FLAG],
							[PROCESS_FLAG],
							[VBELN],
							[LFART],
							[CREATE_DATE],
							[CREATE_USER],
							[LIFNR],
							[MEINS],
							[POSNR],
							[ZSERIAL],
							[DONE],
							[ASN]
						)
						SELECT
							d.[PLANT],
							d.[PART_NO],
							0,
							d.[ZONE_NO],
							w.[UPDATE_DATE],
							w.[UPDATE_DATE],
							'',
							0,
							0,
							w.[PLAN_RUNSHEET_NO],
							s.[LFART],
							GETDATE(),
							'admin',
							d.[SUPPLIER_NUM],
							d.[MEASURING_UNIT_NO],
							s.[POSNR],
							[LES].[FN_GETRANDSTR](19),
							1,
							w.[PLAN_RUNSHEET_NO]
						FROM [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL] d WITH (NOLOCK)
						INNER JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET] w WITH (NOLOCK) ON w.[PLAN_RUNSHEET_SN] = d.[PLAN_RUNSHEET_SN]
						INNER JOIN [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] s WITH (NOLOCK) ON s.[VBELN] = w.[PLAN_RUNSHEET_NO] AND s.[MATNR] = d.[PART_NO]
						WHERE w.[PLAN_RUNSHEET_SN] IN (SELECT [PLAN_RUNSHEET_SN] FROM @IDRunsheet)

						--从VMI出库寄售订单临时表中删除已经同步到SAP接口表的收货单数据
						DELETE FROM [LES].[TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP]
						WHERE [RECEIVE_ID] IN (SELECT [ID] FROM @IDListReceive)

						--更新收货数据状态
						UPDATE [LES].[TT_WMM_RECEIVE] WITH (ROWLOCK)
						SET [ERP_FLAG] = 1,
							[UPDATE_DATE] = GETDATE()
						WHERE [RECEIVE_ID] IN (SELECT [ID] FROM @IDListReceive) AND [ERP_FLAG] = 3

						UPDATE [LES].[TT_WMM_RECEIVE_DETAIL_LOG] WITH (ROWLOCK)
						SET [ERP_FLAG] = 1,
							[UPDATE_DATE] = GETDATE()
						WHERE [RECEIVE_DETAIL_LOG_ID] IN (SELECT [ID] FROM @IDListReceiveLog)

						--更新关单数据状态
						UPDATE [LES].[TE_SPM_SAP_RUNSHEET] WITH (ROWLOCK)
						SET [PROCESS_STATUS] = 1,
							[PROCESS_TIME] = GETDATE()
						WHERE [PLAN_RUNSHEET_SN] IN (SELECT [PLAN_RUNSHEET_SN] FROM @IDRunsheet)					

						COMMIT TRANSACTION TA_RECEIVE
					END TRY
					BEGIN CATCH
						--出错，则返回执行不成功，回滚事务
						ROLLBACK TRANSACTION TA_RECEIVE

						--记录错误信息
						DECLARE @ERROR_RECEIVE_ID NVARCHAR(MAX)
						SET @ERROR_RECEIVE_ID = (SELECT CAST([ID] AS NVARCHAR(50)) + ',' FROM @IDListReceive FOR XML PATH(''))
						INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
						SELECT GETDATE(), 'INTERFACE', 'PROC_INTERFACE_LES_SAP_WMSRECIVEOUT', 'Procedure', 'TT_WMM_RECEIVE:' + ISNULL(ERROR_MESSAGE(), '') + ';RECEIVE_ID:' + ISNULL(@ERROR_RECEIVE_ID, ''), ERROR_LINE()
			
						--标记错误的数据状态为9
						UPDATE [LES].[TT_WMM_RECEIVE] WITH (ROWLOCK)
						SET [ERP_FLAG] = 9,
							[UPDATE_DATE] = GETDATE()
						WHERE RECEIVE_ID IN (SELECT [ID] FROM @IDListReceive) AND [ERP_FLAG] = 3

						UPDATE [LES].[TT_WMM_RECEIVE_DETAIL_LOG] WITH (ROWLOCK)
						SET [ERP_FLAG] = 9,
							[UPDATE_DATE] = GETDATE()
						WHERE [RECEIVE_DETAIL_LOG_ID] IN (SELECT [ID] FROM @IDListReceiveLog)
					END CATCH
				END

			--插入退货数据
			BEGIN TRANSACTION TA_RETURN
			BEGIN TRY
			
				--取出待处理的ID
				DECLARE @IDListReturn TABLE([ID] BIGINT)
				INSERT INTO @IDListReturn
				SELECT TOP 50 [RETURN_ID] FROM [LES].[TT_WMM_RETURN] WITH (NOLOCK)
				WHERE ISNULL([ERP_FLAG], 0) = 0
				AND ISNULL([CONFIRM_FLAG], 0) = 2
				AND [RUNSHEET_CODE] IS NOT NULL

				INSERT INTO [LES].[TI_WMS_RECIVE_OUT]
				(
					[WERKS],
					[MATNR],
					[LGMNG],
					[LGORT],
					[BUDAT],
					[OPTIM],
					[OPRTR],
					[DEAL_FLAG],
					[PROCESS_FLAG],
					[VBELN],
					[LFART],
					[CREATE_DATE],
					[CREATE_USER],
					[LIFNR],
					[MEINS],
					[POSNR],
					[ZSERIAL],
					[DONE]
				)
				SELECT
					d.[PLANT],
					d.[PART_NO],
					d.[PACK_SIZE],
					d.[ZONE_NO],
					w.[TRAN_TIME],
					w.[TRAN_TIME],
					w.[BOOK_KEEPER],
					0,
					0,
					w.[RETURN_NO],
					CASE r.[RUNSHEET_TYPE]
					WHEN -3 THEN 'YRTN' 
					WHEN -22 THEN 'RL' 
					WHEN -32 THEN 'RB' 
					END AS [RUNSHEET_TYPE],
					GETDATE(),
					'admin',
					d.[SUPPLIER_NUM],
					S.[MEINS],
					s.[POSNR],
					[LES].[FN_GETRANDSTR](19),
					1
				FROM [LES].[TT_WMM_RETURN_DETAIL] d WITH (NOLOCK)
				INNER JOIN LES.TT_WMM_RETURN w WITH (NOLOCK) ON w.[RETURN_ID] = d.[RETURN_ID]
				INNER JOIN (SELECT DISTINCT [MEINS], [POSNR], [VBELN], [MATNR] FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (NOLOCK)) s ON s.[VBELN] = w.[RETURN_NO] AND s.[MATNR] = d.[PART_NO]
				LEFT JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET] r WITH (NOLOCK) ON w.[RETURN_NO] = r.[PLAN_RUNSHEET_NO]
				WHERE ISNULL(w.[ERP_FLAG], 0) = 0 
				AND ISNULL(w.[CONFIRM_FLAG],0) = 2 
				AND w.[RUNSHEET_CODE] IS NOT NULL
				AND w.[RETURN_ID] IN (SELECT [ID] FROM @IDListReturn)

				--更新退货数据状态
				UPDATE [LES].[TT_WMM_RETURN] WITH (ROWLOCK)
				SET [ERP_FLAG] = 1,
					[UPDATE_DATE] = GETDATE()
				WHERE ISNULL([ERP_FLAG], 0) = 0  
				AND ISNULL([CONFIRM_FLAG], 0) = 2 
				AND [RUNSHEET_CODE] IS NOT NULL
				AND [RETURN_ID] IN (SELECT [ID] FROM @IDListReturn)

				COMMIT TRANSACTION TA_RETURN
			END TRY
			BEGIN CATCH
				ROLLBACK TRANSACTION TA_RETURN

				--记录错误信息
				DECLARE @ERROR_RETURN_ID NVARCHAR(MAX)
				SET @ERROR_RETURN_ID = (SELECT CAST([ID] AS NVARCHAR(50)) + ',' FROM @IDListReturn FOR XML PATH(''))
				INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
				SELECT GETDATE(), 'INTERFACE', 'PROC_INTERFACE_LES_SAP_WMSRECIVEOUT', 'Procedure', 'TT_WMM_RETURN:' + ISNULL(ERROR_MESSAGE(), '') + ';RETURN_ID:' + ISNULL(@ERROR_RETURN_ID,''), ERROR_LINE()
			
				UPDATE [LES].[TT_WMM_RETURN] WITH (ROWLOCK)
				SET [ERP_FLAG] = 9,
					[UPDATE_DATE] = GETDATE()
				WHERE ISNULL([ERP_FLAG], 0) = 0
				AND ISNULL([CONFIRM_FLAG], 0) = 2
				AND [RUNSHEET_CODE] IS NOT NULL
				AND [RETURN_ID] IN (SELECT [ID] FROM @IDListReturn)
			END CATCH
		END
END