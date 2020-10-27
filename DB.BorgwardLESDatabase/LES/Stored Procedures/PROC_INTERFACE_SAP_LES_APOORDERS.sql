/********************************************************************/
/*   Program Name:  [LES].[PROC_INTERFACE_SAP_LES_APOORDERS]		*/
/*   Called By:     window service									*/
/*   Modifier:      孙述霄											*/
/*   Modify date:	2017-11-24										*/
/*   Note:			SAP接口_APO_整车订单表 业务_车辆订单表			*/
/*  				（存在更新、不存在新增，不删除）				*/
/*  				主子表必须一起添加和更新						*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_INTERFACE_SAP_LES_APOORDERS]
AS
BEGIN
	DECLARE @ORDERS_IN_ID BIGINT	--接口自增id
	DECLARE @ZST NVARCHAR(1)		--接口状态:A,正常;D,删除;C,变更
	DECLARE @ZORDNO NVARCHAR(12)	--订单号
	DECLARE @ZVIN NVARCHAR(17)		--VIN号
	DECLARE @BATCHNO NVARCHAR(50)	--批次号码
	DECLARE @ROWCOUNT INT
	DECLARE @FLAG INT

	--定义订单临时表
	DECLARE @ODS TABLE
	(
		[ID] INT IDENTITY,
		[SEQ_ID] BIGINT,
		[ZST] NVARCHAR(1),
		[ZORDNO] NVARCHAR(12),
		[ZVIN] NVARCHAR(17),
		[BATCHNO] NVARCHAR(50)
	)

	--定义计划接收临时表
	DECLARE @PLANRECEIVE TABLE
	(
		[ID] INT IDENTITY,
		[FID] UNIQUEIDENTIFIER,
		[VIN] NVARCHAR(32),
		[SEQNO] NVARCHAR(16),
		[PARTNO] NVARCHAR(20),
		[PARTNAME] NVARCHAR(100),
		[QTY] DECIMAL(18, 2),
		[BOXPART] NVARCHAR(16),
		[ORDERTYPE] NVARCHAR(2),
		[ORDERNO] NVARCHAR(32)
	)

	BEGIN TRANSACTION ProcessOrders
	BEGIN TRY
		--生成订单临时数据
		--只是处理5个订单
		INSERT INTO @ODS
		SELECT TOP 5
			[SEQ_ID],
			ISNULL([ZST], 'A') AS [ZST],
			[ZORDNO],
			[ZVIN],
			[BATCHNO]
		FROM [LES].[TI_ODS_ORDERS_IN] WITH (NOLOCK)
		WHERE ISNULL([DEAL_FLAG], 0) = 0
		ORDER BY [SEQ_ID]

		SET @FLAG = 1
		SELECT @ROWCOUNT = COUNT(1) FROM @ODS
		--循环订单临时数据
		WHILE (@FLAG <= @ROWCOUNT)
			BEGIN
				--获取变量值
				SELECT
					@ORDERS_IN_ID = [SEQ_ID],
					@ZST = [ZST],
					@ZORDNO = [ZORDNO],
					@ZVIN = [ZVIN],
					@BATCHNO = [BATCHNO]
				FROM @ODS
				WHERE [ID] = @FLAG

				IF @ZST = 'D'
					BEGIN
						--直接删除
						DELETE FROM [LES].[TT_BAS_ORDER_PART_RESULTS] WITH (ROWLOCK) WHERE [ORDER_NO] = @ZORDNO
						DELETE FROM [LES].[TT_BAS_PULL_ORDERS] WITH (ROWLOCK) WHERE [ORDER_NO] = @ZORDNO

						DELETE FROM @PLANRECEIVE
						--插入计划接收临时表
						INSERT INTO @PLANRECEIVE
						(
							[FID],
							[VIN],
							[SEQNO],
							[PARTNO],
							[PARTNAME],
							[QTY],
							[BOXPART],
							[ORDERTYPE],
							[ORDERNO]
						)
						SELECT
							NEWID(),
							@ZVIN,
							'',
							'',
							'',
							0,
							'',
							'D',
							''

						--插入计划接收中间表
						INSERT INTO [LES].[TI_MID_PTL_PLAN_RECEVIE]
						(
							[FID],
							[VIN],
							[SeqNo],
							[PartNo],
							[PartName],
							[Qty],
							[BoxPart],
							[CreateTime],
							[OrderType],
							[OrderNo]
						)
						SELECT
							[FID],
							[VIN],
							[SEQNO],
							[PARTNO],
							[PARTNAME],
							[QTY],
							[BOXPART],
							GETDATE(),
							[ORDERTYPE],
							[ORDERNO]
						FROM @PLANRECEIVE
						ORDER BY [ID]

						--插入服务中间表
						INSERT INTO [LES].[TI_SYS_OUTBOUND]
						(
							[FID],
							[TRANS_NO],
							[METHORD_NAME],
							[EXECUTE_RESULT],
							[KEY_VALUE],
							[VALID_FLAG],
							[CREATE_USER],
							[CREATE_DATE]
						)
						SELECT
							[FID],
							'007',
							'TI_MID_PTL_PLAN_RECEVIE',
							0,
							[VIN] + '|' + [PARTNO] + '|' + [ORDERTYPE],
							1,
							'admin',
							GETDATE()
						FROM @PLANRECEIVE
						ORDER BY [ID]

						--状态更改
						UPDATE [LES].[TI_ODS_ORDER_BOM_IN] WITH (ROWLOCK)
						SET [DEAL_FLAG] = 1,
							[UPDATE_DATE] = GETDATE()
						WHERE ISNULL([ZST], 'A') = 'D'
						AND [ZORDNO] = @ZORDNO
						AND [BATCHNO] = @BATCHNO
						AND ISNULL([DEAL_FLAG], 0) = 0

						UPDATE [LES].[TI_ODS_ORDERS_IN] WITH (ROWLOCK)
						SET [DEAL_FLAG] = 1,
							[UPDATE_DATE] = GETDATE()
						WHERE [SEQ_ID] = @ORDERS_IN_ID
					END

				IF @ZST = 'C'
					BEGIN
						--修改
						--1先删除
						DELETE FROM [LES].[TT_BAS_ORDER_PART_RESULTS] WHERE [ORDER_NO] = @ZORDNO
						DELETE FROM [LES].[TT_BAS_PULL_ORDERS] WHERE [ORDER_NO] = @ZORDNO
						DELETE FROM @PLANRECEIVE

						--2再插入
						INSERT INTO [LES].[TT_BAS_ORDER_PART_RESULTS]
						(
							[ORDER_NO],
							[KNR],
							[SIGNATURE],
							[PLANT],
							[MODEL],
							[PART_NO],
							[PART_NAME],
							[PART_NAME_CN],
							[UNIT_ID],
							[QUANTITY],
							[WORKSHOP_SECTION],
							[LOCATION],
							[IN_PLANT_LOGISTIC_MODE],
							[IN_PLANT_SYSTEM_MODE],
							[IN_PLANT_LOGISTIC_PART_CLASS],
							[INHOUSE_MODE],
							[INHOUSE_SYSTEM_MODE],
							[EFFECTIVE_STATUS],
							[START_EFFECTIVE_DATE],
							[INHOUSE_PART_CLASS],
							[DOCK],
							[INHOUSE_PACKAGE_MODEL],
							[INHOUSE_PACKAGE],
							[VIN],
							[MBOM_ITEMID],
							[MEASURING_UNIT_NO]
						)
						SELECT DISTINCT
							[ZORDNO],						-- ORDER_NO - nvarchar(20)
							[ZBOMID],						-- KNR - nvarchar(8)
							[ZMEMO],						-- SIGNATURE - nvarchar(256)
							[ZKWERK],						-- PLANT - nvarchar(5)
							N'',							-- MODEL - nvarchar(10)
							[ZCOMNO],						-- PART_NO - nvarchar(20)
							[ZCOMDS],						-- PART_NAME - nvarchar(40)
							[ZCOMDS],						-- PART_NAME_CN - nvarchar(40)
							N'',							-- UNIT_ID - nvarchar(5)
							[ZQTY],							-- QUANTITY - numeric
							N'',							-- WORKSHOP_SECTION - nvarchar(20)
							[ZLOC],							-- LOCATION - nvarchar(20)
							N'',							-- IN_PLANT_LOGISTIC_MODE - nvarchar(50)
							N'',							-- IN_PLANT_SYSTEM_MODE - nvarchar(10)
							N'',							-- IN_PLANT_LOGISTIC_PART_CLASS - nvarchar(10)
							N'',							-- INHOUSE_MODE - nvarchar(50)
							N'',							-- INHOUSE_SYSTEM_MODE - nvarchar(10)
							NULL,							-- EFFECTIVE_STATUS - bit
							CAST([ZDATE] AS DATETIME),		-- START_EFFECTIVE_DATE - datetime
							N'',							-- INHOUSE_PART_CLASS - nvarchar(10)
							[ZLOC],							-- DOCK - nvarchar(10)
							N'',							-- INHOUSE_PACKAGE_MODEL - nvarchar(30)
							0,								-- INHOUSE_PACKAGE - int
							N'',							-- VIN - nvarchar(20)
							[ZBOMID],						-- MBOM_ITEMID - nvarchar(20)
							[ZMEINS]						-- MEASURING_UNIT_NO - varchar(8)
						FROM [LES].[TI_ODS_ORDER_BOM_IN] WITH (NOLOCK)
						WHERE ISNULL([ZST], 'A') = 'C'
						AND [ZORDNO] = @ZORDNO
						AND [BATCHNO] = @BATCHNO
						AND ISNULL([DEAL_FLAG], 0) = 0
		
						INSERT INTO [LES].[TT_BAS_PULL_ORDERS]
						(
							[ORDER_NO],
							[WERK],
							[SPJ],
							[KNR],
							[MODEL_YEAR],
							[MODEL],
							[FARBAU],
							[FARBIN],
							[PNR_STRING],
							[PNR_STRING_COMPUTE],
							[VEHICLE_ORDER],
							[ORDER_DATE],
							[DEAL_FLAG],
							[STATUS_FLAG],
							[VORSERIE],
							[SIGNATURE],
							[ORDER_FILE_NAME],
							[ORDER_TYPE],
							[RECALCULATE_FLAG],
							[CHANGE_FLAG],
							[ASSEMBLY_LINE],
							[PROCESS_LINE_SN],
							[INIT_STSTUS],
							[VIN],
							[PART_NO],
							[QTY],
							[MEASURING_UNIT],
							[PLAN_FLAG],
							[COMMENTS],
							[CREATE_USER],
							[CREATE_DATE],  
							[ZCOLORI],
							[ZCOLORI_D]
						)
						SELECT
							[ZORDNO],										-- ORDER_NO - nvarchar(20)
							[ZKWERK],										-- WERK - nvarchar(4)
							N'',											-- SPJ - nvarchar(8)
							N'',											-- KNR - nvarchar(8)
							ISNULL([ZCOLORE], ''),							-- MODEL_YEAR - nvarchar(20)
							ISNULL([ZCOLORE_D], ''),						-- MODEL - nvarchar(30)
							ISNULL([ZMODEL], ''),							-- FARBAU - nvarchar(30)
							ISNULL([ZMODEL_D], ''),							-- FARBIN - nvarchar(30)
							ISNULL([ZPACK], ''),							-- PNR_STRING - nvarchar(200)
							ISNULL([ZPACK_D], ''),							-- PNR_STRING_COMPUTE - nvarchar(200)
							N'',											-- VEHICLE_ORDER - nvarchar(8)
							ISNULL(CAST([ZDATE] AS DATETIME), GETDATE()),	-- ORDER_DATE - datetime
							0,												-- DEAL_FLAG - int
							'',												-- STATUS_FLAG - nvarchar(8)
							1,												-- VORSERIE - bit
							N'',											-- SIGNATURE - nvarchar(256)
							N'',											-- ORDER_FILE_NAME - nvarchar(256)
							'',												-- ORDER_TYPE - varchar(2)
							0,												-- RECALCULATE_FLAG - int
							0,												-- CHANGE_FLAG - int
							N'',											-- ASSEMBLY_LINE - nvarchar(10)
							0,												-- PROCESS_LINE_SN - int
							0,												-- INIT_STSTUS - int
							ISNULL([ZVIN], ''),								-- VIN - nvarchar(30)
							ISNULL([ZMATNR], ''),							-- PART_NO - nvarchar(18)
							ISNULL([ZQTY], 0),								-- QTY - numeric
							ISNULL([ZMEINS], ''),							-- MEASURING_UNIT - nvarchar(8)
							ISNULL([ZDISPO], ''),							-- PLAN_FLAG - nvarchar(3)
							ISNULL([ZMEMO], ''),							-- COMMENTS - nvarchar(200)
							N'admin',										-- CREATE_USER - nvarchar(50)
							GETDATE(),										-- CREATE_DATE - datetime
							[ZCOLORI],
							[ZCOLORI_D]
						FROM [LES].[TI_ODS_ORDERS_IN] WITH (NOLOCK)
						WHERE [SEQ_ID] = @ORDERS_IN_ID

						--插入计划接收临时表
						INSERT INTO @PLANRECEIVE
						(
							[FID],
							[VIN],
							[SEQNO],
							[PARTNO],
							[PARTNAME],
							[QTY],
							[BOXPART],
							[ORDERTYPE],
							[ORDERNO]
						)
						SELECT
							NEWID() AS [FID],
							A.[ZVIN] AS [VIN],
							'' AS [SEQNO],
							A.[ZCOMNO] AS [PARTNO],
							A.[ZCOMDS] AS [PARTNAME],
							A.[ZQTY] AS [QTY],
							C.[BOX_PARTS] AS [BOXPART],
							'N' AS [ORDERTYPE],
							'' AS [ORDERNO]
						FROM [LES].[TI_ODS_ORDER_BOM_IN] A WITH (NOLOCK)
						INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] B WITH (NOLOCK) ON A.[ZKWERK] = B.[PLANT] AND A.[ZCOMNO] = B.[PART_NO] AND A.[ZLOC] = B.[LOCATION] AND B.[INHOUSE_SYSTEM_MODE] = 'SPS' AND B.[DELETE_FLAG] = 0
						INNER JOIN [LES].[TM_SPS_BOX_PARTS] C WITH (NOLOCK) ON C.[PLANT] = B.[PLANT] AND C.[ASSEMBLY_LINE] = B.[ASSEMBLY_LINE] AND C.[SUPPLIER_NUM] = B.[SUPPLIER_NUM] AND C.[BOX_PARTS] = B.[INHOUSE_PART_CLASS] AND C.[BOX_PARTS_STATE] <> 3
						WHERE ISNULL(A.[ZST], 'A') = 'C'
						AND A.[ZORDNO] = @ZORDNO
						AND A.[BATCHNO] = @BATCHNO
						AND ISNULL(A.[DEAL_FLAG], 0) = 0

						--插入计划接收中间表
						INSERT INTO [LES].[TI_MID_PTL_PLAN_RECEVIE]
						(
							[FID],
							[VIN],
							[SeqNo],
							[PartNo],
							[PartName],
							[Qty],
							[BoxPart],
							[CreateTime],
							[OrderType],
							[OrderNo]
						)
						SELECT
							[FID],
							[VIN],
							[SEQNO],
							[PARTNO],
							[PARTNAME],
							[QTY],
							[BOXPART],
							GETDATE(),
							[ORDERTYPE],
							[ORDERNO]
						FROM @PLANRECEIVE
						ORDER BY [ID]

						--插入服务中间表
						INSERT INTO [LES].[TI_SYS_OUTBOUND]
						(
							[FID],
							[TRANS_NO],
							[METHORD_NAME],
							[EXECUTE_RESULT],
							[KEY_VALUE],
							[VALID_FLAG],
							[CREATE_USER],
							[CREATE_DATE]
						)
						SELECT
							[FID],
							'007',
							'TI_MID_PTL_PLAN_RECEVIE',
							0,
							[VIN] + '|' + [PARTNO] + '|' + CAST([QTY] AS NVARCHAR),
							1,
							'admin',
							GETDATE()
						FROM @PLANRECEIVE
						ORDER BY [ID]

						--状态更改
						UPDATE [LES].[TI_ODS_ORDER_BOM_IN] WITH (ROWLOCK)
						SET [DEAL_FLAG] = 1,
							[UPDATE_DATE] = GETDATE()
						WHERE ISNULL([ZST], 'A') = 'C' 
						AND [ZORDNO] = @ZORDNO 
						AND [BATCHNO] = @BATCHNO
						AND ISNULL([DEAL_FLAG], 0) = 0

						UPDATE [LES].[TI_ODS_ORDERS_IN] WITH (ROWLOCK)
						SET [DEAL_FLAG] = 1,
							[UPDATE_DATE] = GETDATE()
						WHERE [SEQ_ID] = @ORDERS_IN_ID
					END

				IF @ZST = 'A'
					BEGIN
						--增加
						DECLARE @recordCount INT
						SELECT @recordCount = COUNT(1) FROM [LES].[TT_BAS_PULL_ORDERS] WITH (NOLOCK) WHERE [ORDER_NO] = @ZORDNO
		
						IF (@recordCount = 0)
							BEGIN
								--2再插入
								INSERT INTO [LES].[TT_BAS_ORDER_PART_RESULTS]
								(
									[ORDER_NO],
									[KNR],
									[SIGNATURE],
									[PLANT],
									[MODEL],
									[PART_NO],
									[PART_NAME],
									[PART_NAME_CN],
									[UNIT_ID],
									[QUANTITY],
									[WORKSHOP_SECTION],
									[LOCATION],
									[IN_PLANT_LOGISTIC_MODE],
									[IN_PLANT_SYSTEM_MODE],
									[IN_PLANT_LOGISTIC_PART_CLASS],
									[INHOUSE_MODE],
									[INHOUSE_SYSTEM_MODE],
									[EFFECTIVE_STATUS],
									[START_EFFECTIVE_DATE],
									[INHOUSE_PART_CLASS],
									[DOCK],
									[INHOUSE_PACKAGE_MODEL],
									[INHOUSE_PACKAGE],
									[VIN],
									[MBOM_ITEMID],
									[MEASURING_UNIT_NO]
								)
								SELECT DISTINCT
									[ZORDNO],					-- ORDER_NO - nvarchar(20)
									[ZBOMID],					-- KNR - nvarchar(8)
									[ZMEMO],					-- SIGNATURE - nvarchar(256)
									[ZKWERK],					-- PLANT - nvarchar(5)
									N'',						-- MODEL - nvarchar(10)
									[ZCOMNO],					-- PART_NO - nvarchar(20)
									[ZCOMDS],					-- PART_NAME - nvarchar(40)
									[ZCOMDS],					-- PART_NAME_CN - nvarchar(40)
									N'',						-- UNIT_ID - nvarchar(5)
									[ZQTY],						-- QUANTITY - numeric
									N'',						-- WORKSHOP_SECTION - nvarchar(20)
									[ZLOC],						-- LOCATION - nvarchar(20)
									N'',						-- IN_PLANT_LOGISTIC_MODE - nvarchar(50)
									N'',						-- IN_PLANT_SYSTEM_MODE - nvarchar(10)
									N'',						-- IN_PLANT_LOGISTIC_PART_CLASS - nvarchar(10)
									N'',						-- INHOUSE_MODE - nvarchar(50)
									N'',						-- INHOUSE_SYSTEM_MODE - nvarchar(10)
									NULL,						-- EFFECTIVE_STATUS - bit
									CAST([ZDATE] AS DATETIME),	-- START_EFFECTIVE_DATE - datetime
									N'',						-- INHOUSE_PART_CLASS - nvarchar(10)
									[ZLOC],						-- DOCK - nvarchar(10)
									N'',						-- INHOUSE_PACKAGE_MODEL - nvarchar(30)
									0,							-- INHOUSE_PACKAGE - int
									N'',						-- VIN - nvarchar(20)
									[ZBOMID],					-- MBOM_ITEMID - nvarchar(20)
									[ZMEINS]					-- MEASURING_UNIT_NO - varchar(8)
								FROM [LES].[TI_ODS_ORDER_BOM_IN] WITH (NOLOCK)
								WHERE ISNULL([ZST], 'A') = 'A' 
								AND [ZORDNO] = @ZORDNO 
								AND [BATCHNO] = @BATCHNO
								AND ISNULL([DEAL_FLAG], 0) = 0
		
								INSERT INTO LES.TT_BAS_PULL_ORDERS
								(
									[ORDER_NO],
									[WERK],
									[SPJ],
									[KNR],
									[MODEL_YEAR],
									[MODEL],
									[FARBAU],
									[FARBIN],
									[PNR_STRING],
									[PNR_STRING_COMPUTE],
									[VEHICLE_ORDER],
									[ORDER_DATE],
									[DEAL_FLAG],
									[STATUS_FLAG],
									[VORSERIE],
									[SIGNATURE],
									[ORDER_FILE_NAME],
									[ORDER_TYPE],
									[RECALCULATE_FLAG],
									[CHANGE_FLAG],
									[ASSEMBLY_LINE],
									[PROCESS_LINE_SN],
									[INIT_STSTUS],
									[VIN],
									[PART_NO],
									[QTY],
									[MEASURING_UNIT],
									[PLAN_FLAG],
									[COMMENTS],
									[CREATE_USER],
									[CREATE_DATE],  
									[ZCOLORI],
									[ZCOLORI_D]
								)
								SELECT
									[ZORDNO],										-- ORDER_NO - nvarchar(20)
									[ZKWERK],										-- WERK - nvarchar(4)
									N'',											-- SPJ - nvarchar(8)
									N'',											-- KNR - nvarchar(8)
									ISNULL([ZCOLORE], ''),							-- MODEL_YEAR - nvarchar(20)
									ISNULL([ZCOLORE_D], ''),						-- MODEL - nvarchar(30)
									ISNULL([ZMODEL], ''),							-- FARBAU - nvarchar(30)
									ISNULL([ZMODEL_D], ''),							-- FARBIN - nvarchar(30)
									ISNULL([ZPACK], ''),							-- PNR_STRING - nvarchar(200)
									ISNULL([ZPACK_D], ''),							-- PNR_STRING_COMPUTE - nvarchar(200)
									N'',											-- VEHICLE_ORDER - nvarchar(8)
									ISNULL(CAST([ZDATE] AS DATETIME), GETDATE()),	-- ORDER_DATE - datetime
									0,												-- DEAL_FLAG - int
									'',												-- STATUS_FLAG - nvarchar(8)
									1,												-- VORSERIE - bit
									N'',											-- SIGNATURE - nvarchar(256)
									N'',											-- ORDER_FILE_NAME - nvarchar(256)
									'',												-- ORDER_TYPE - varchar(2)
									0,												-- RECALCULATE_FLAG - int
									0,												-- CHANGE_FLAG - int
									N'',											-- ASSEMBLY_LINE - nvarchar(10)
									0,												-- PROCESS_LINE_SN - int
									0,												-- INIT_STSTUS - int
									ISNULL([ZVIN], ''),								-- VIN - nvarchar(30)
									ISNULL([ZMATNR], ''),							-- PART_NO - nvarchar(18)
									ISNULL([ZQTY], 0),								-- QTY - numeric
									ISNULL([ZMEINS], ''),							-- MEASURING_UNIT - nvarchar(8)
									ISNULL([ZDISPO], ''),							-- PLAN_FLAG - nvarchar(3)
									ISNULL([ZMEMO], ''),							-- COMMENTS - nvarchar(200)
									N'admin',										-- CREATE_USER - nvarchar(50)
									GETDATE(),										-- CREATE_DATE - datetime
									[ZCOLORI],
									[ZCOLORI_D]
								FROM [LES].[TI_ODS_ORDERS_IN] WITH (NOLOCK)
								WHERE [SEQ_ID] = @ORDERS_IN_ID

								--删除计划接收临时表
								DELETE FROM @PLANRECEIVE
								--插入计划接收临时表
								INSERT INTO @PLANRECEIVE
								(
									[FID],
									[VIN],
									[SEQNO],
									[PARTNO],
									[PARTNAME],
									[QTY],
									[BOXPART],
									[ORDERTYPE],
									[ORDERNO]
								)
								SELECT
									NEWID() AS [FID],
									A.[ZVIN] AS [VIN],
									'' AS [SEQNO],
									A.[ZCOMNO] AS [PARTNO],
									A.[ZCOMDS] AS [PARTNAME],
									A.[ZQTY] AS [QTY],
									C.[BOX_PARTS] AS [BOXPART],
									'N' AS [ORDERTYPE],
									'' AS [ORDERNO]
								FROM [LES].[TI_ODS_ORDER_BOM_IN] A WITH (NOLOCK)
								INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] B WITH (NOLOCK) ON A.[ZKWERK] = B.[PLANT] AND A.[ZCOMNO] = B.[PART_NO] AND A.[ZLOC] = B.[LOCATION] AND B.[INHOUSE_SYSTEM_MODE] = 'SPS' AND B.[DELETE_FLAG] = 0
								INNER JOIN [LES].[TM_SPS_BOX_PARTS] C WITH (NOLOCK) ON C.[PLANT] = B.[PLANT] AND C.[ASSEMBLY_LINE] = B.[ASSEMBLY_LINE] AND C.[SUPPLIER_NUM] = B.[SUPPLIER_NUM] AND C.[BOX_PARTS] = B.[INHOUSE_PART_CLASS] AND C.[BOX_PARTS_STATE] <> 3
								WHERE ISNULL(A.[ZST], 'A') = 'A'
								AND A.[ZORDNO] = @ZORDNO
								AND A.[BATCHNO] = @BATCHNO
								AND ISNULL(A.[DEAL_FLAG], 0) = 0

								--插入计划接收中间表
								INSERT INTO [LES].[TI_MID_PTL_PLAN_RECEVIE]
								(
									[FID],
									[VIN],
									[SeqNo],
									[PartNo],
									[PartName],
									[Qty],
									[BoxPart],
									[CreateTime],
									[OrderType],
									[OrderNo]
								)
								SELECT
									[FID],
									[VIN],
									[SEQNO],
									[PARTNO],
									[PARTNAME],
									[QTY],
									[BOXPART],
									GETDATE(),
									[ORDERTYPE],
									[ORDERNO]
								FROM @PLANRECEIVE
								ORDER BY [ID]

								--插入服务中间表
								INSERT INTO [LES].[TI_SYS_OUTBOUND]
								(
									[FID],
									[TRANS_NO],
									[METHORD_NAME],
									[EXECUTE_RESULT],
									[KEY_VALUE],
									[VALID_FLAG],
									[CREATE_USER],
									[CREATE_DATE]
								)
								SELECT
									[FID],
									'007',
									'TI_MID_PTL_PLAN_RECEVIE',
									0,
									[VIN] + '|' + [PARTNO] + '|' + CAST([QTY] AS NVARCHAR),
									1,
									'admin',
									GETDATE()
								FROM @PLANRECEIVE
								ORDER BY [ID]
							END

						--状态更改
						UPDATE [LES].[TI_ODS_ORDER_BOM_IN] WITH (ROWLOCK)
						SET [DEAL_FLAG] = 1,
							[UPDATE_DATE] = GETDATE()
						WHERE ISNULL([ZST], 'A') = 'A'
						AND [ZORDNO] = @ZORDNO
						AND [BATCHNO] = @BATCHNO
						AND ISNULL([DEAL_FLAG], 0) = 0

						UPDATE [LES].[TI_ODS_ORDERS_IN] WITH (ROWLOCK)
						SET [DEAL_FLAG] = 1,
							[UPDATE_DATE] = GETDATE()
						WHERE [SEQ_ID] = @ORDERS_IN_ID
					END

				SET @FLAG = @FLAG + 1
			END
		COMMIT TRANSACTION ProcessOrders
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION ProcessOrders

		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', 'PROC_INTERFACE_SAP_LES_APOORDERS', 'Procedure', ISNULL(ERROR_MESSAGE(), '') + ';SEQ_ID:' + ISNULL(@ORDERS_IN_ID, ''), ERROR_LINE()
		
		--标记错误的数据状态为9
		UPDATE [LES].[TI_ODS_ORDERS_IN] WITH (ROWLOCK)
		SET [DEAL_FLAG] = 9,
			[UPDATE_DATE] = GETDATE() 
		WHERE [SEQ_ID] = @ORDERS_IN_ID
	END CATCH
END