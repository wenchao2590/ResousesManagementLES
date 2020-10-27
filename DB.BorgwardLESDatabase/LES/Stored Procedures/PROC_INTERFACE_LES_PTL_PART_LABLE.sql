/****************************************************************/
/*   Project Name:  PTL											*/
/*   Program Name:  [LES].[PROC_INTERFACE_LES_PTL_PART_LABLE]	*/
/*   Called By:     window service								*/
/*   Author:        孙述霄										*/
/*   Date:			2017-11-15									*/
/*   Note:			同步PTL零件标签信息							*/
/****************************************************************/
CREATE PROCEDURE [LES].[PROC_INTERFACE_LES_PTL_PART_LABLE]
AS
BEGIN
	SET XACT_ABORT ON
	SET NOCOUNT ON
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @FID UNIQUEIDENTIFIER

			--定义入库单号临时表
			DECLARE @RECEIVETABLE TABLE
			(
				[RECEIVE_ID] INT,
				[RECEIVE_NO] NVARCHAR(50)
			)

			--定义出库单号临时表
			DECLARE @OUTPUTTABLE TABLE
			(
				[OUTPUT_ID] INT,
				[OUTPUT_NO] NVARCHAR(50)
			)

			--定义零件标签临时表
			DECLARE @PARTLABEL TABLE
			(
				[ID] INT IDENTITY,
				[FID] UNIQUEIDENTIFIER,
				[BARCODE] NVARCHAR(64),
				[PARTNO] NVARCHAR(20),
				[PARTNAME] NVARCHAR(100),
				[BOXPARTS] NVARCHAR(10),
				[ORDERNO] NVARCHAR(16),
				[QTY] DECIMAL(18, 2)
			)

			--生成入库单号临时表数据
			INSERT INTO @RECEIVETABLE
			(
				[RECEIVE_ID],
				[RECEIVE_NO]
			)
			SELECT TOP 100
				[RECEIVE_ID],
				[RECEIVE_NO]
			FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK)
			WHERE [CONFIRM_FLAG] = 2 AND DATEADD(MINUTE, 3, [TRAN_TIME]) < GETDATE()
			AND [PTL_FLAG] IS NULL OR [PTL_FLAG] = 0
			ORDER BY [RECEIVE_ID]

			IF EXISTS (SELECT TOP 1 [RECEIVE_ID] FROM @RECEIVETABLE)
				BEGIN
					SET @FID = NEWID()

					--更新标识
					UPDATE [LES].[TT_WMM_RECEIVE] WITH (ROWLOCK)
					SET [PTL_FLAG] = 1,
						[PTL_TIME] = GETDATE()
					WHERE [RECEIVE_ID] IN (SELECT [RECEIVE_ID] FROM @RECEIVETABLE)

					--删除零时零件标签数据
					DELETE FROM @PARTLABEL
					--生成临时零件标签数据
					INSERT INTO @PARTLABEL
					(
						[FID],
						[BARCODE],
						[PARTNO],
						[PARTNAME],
						[BOXPARTS],
						[ORDERNO],
						[QTY]
					)
					SELECT
						@FID,
						ISNULL([BARCODE_DATA], ''),
						[PART_NO],
						[PART_CNAME],
						--目标存储区：入库使用ZONE_NO
						[ZONE_NO],
						[TRAN_NO],
						[NUM]
					FROM [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH (NOLOCK)
					WHERE [TRAN_STATE] = 1 AND [TRAN_TYPE] = 1 AND [TRAN_NO] IN
					(
						SELECT
							[RECEIVE_NO]
						FROM @RECEIVETABLE
						WHERE [RECEIVE_ID] IN
						(
							SELECT DISTINCT
								[RECEIVE_ID]
							FROM [LES].[TT_WMM_RECEIVE_DETAIL] WITH (NOLOCK)
							WHERE [RECEIVE_ID] IN (SELECT [RECEIVE_ID] FROM @RECEIVETABLE)
							AND [TWD_RUNSHEET_NO] IN
							(
								SELECT
									A.[TWD_RUNSHEET_NO]
								FROM [LES].[TT_TWD_RUNSHEET] A WITH (NOLOCK)
								INNER JOIN [LES].[TM_TWD_BOX_PARTS] B WITH (NOLOCK)
								ON A.[PLANT] = B.[PLANT] AND A.[ASSEMBLY_LINE] = B.[ASSEMBLY_LINE]
								AND A.[SUPPLIER_NUM] = B.[SUPPLIER_NUM] AND A.[BOX_PARTS] = B.[BOX_PARTS]
								WHERE B.[IS_SPS] = 1
							)
						)
					)

					IF EXISTS (SELECT 1 FROM @PARTLABEL)
						BEGIN
							--插入零件标签中间表
							INSERT INTO [LES].[TI_MID_PTL_PART_LABLE]
							(
								[FID],
								[BarCode],
								[PartNo],
								[PartName],
								[BoxPart],
								[OrderNo],
								[Qty],
								[CreateTime]
							)
							SELECT
								[FID],
								[BARCODE],
								[PARTNO],
								[PARTNAME],
								[BOXPARTS],
								[ORDERNO],
								[QTY],
								GETDATE()
							FROM @PARTLABEL
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
								@FID,
								'008',
								'TI_MID_PTL_PART_LABLE',
								0,
								'',
								1,
								'admin',
								GETDATE()
						END
				END

			--生成出库单号临时表数据
			INSERT INTO @OUTPUTTABLE
			(
				[OUTPUT_ID],
				[OUTPUT_NO]
			)
			SELECT TOP 100
				[OUTPUT_ID],
				[OUTPUT_NO]
			FROM [LES].[TT_WMM_OUTPUT] WITH (NOLOCK)
			WHERE [CONFIRM_FLAG] = 2 AND DATEADD(MINUTE, 3, [TRAN_TIME]) < GETDATE()
			AND [PTL_FLAG] IS NULL OR [PTL_FLAG] = 0
			ORDER BY [OUTPUT_ID]

			IF EXISTS (SELECT TOP 1 [OUTPUT_ID] FROM @OUTPUTTABLE)
				BEGIN
					SET @FID = NEWID()

					--更新标识
					UPDATE [LES].[TT_WMM_OUTPUT] WITH (ROWLOCK)
					SET [PTL_FLAG] = 1,
						[PTL_TIME] = GETDATE()
					WHERE [OUTPUT_ID] IN (SELECT [OUTPUT_ID] FROM @OUTPUTTABLE)

					--删除零时零件标签数据
					DELETE FROM @PARTLABEL
					--生成临时零件标签数据
					INSERT INTO @PARTLABEL
					(
						[FID],
						[BARCODE],
						[PARTNO],
						[PARTNAME],
						[BOXPARTS],
						[ORDERNO],
						[QTY]
					)
					SELECT
						@FID,
						ISNULL([BARCODE_DATA], ''),
						[PART_NO],
						[PART_CNAME],
						--目标存储区：出库使用TARGET_ZONE
						[TARGET_ZONE],
						[TRAN_NO],
						[NUM]
					FROM [LES].[TM_WMM_TRAN_DETAILS_LOG] WITH (NOLOCK)
					WHERE [TRAN_STATE] = 1 AND [TRAN_TYPE] = 2 AND [TRAN_NO] IN
					(
						SELECT
							[OUTPUT_NO]
						FROM @OUTPUTTABLE
						WHERE [OUTPUT_NO] IN
						(
							SELECT
								A.[TWD_RUNSHEET_NO]
							FROM [LES].[TT_TWD_RUNSHEET] A WITH (NOLOCK)
							INNER JOIN [LES].[TM_TWD_BOX_PARTS] B WITH (NOLOCK)
							ON A.[PLANT] = B.[PLANT] AND A.[ASSEMBLY_LINE] = B.[ASSEMBLY_LINE]
							AND A.[SUPPLIER_NUM] = B.[SUPPLIER_NUM] AND A.[BOX_PARTS] = B.[BOX_PARTS]
							WHERE B.[IS_SPS] = 1
						)
					)

					IF EXISTS (SELECT 1 FROM @PARTLABEL)
						BEGIN
							--插入零件标签中间表
							INSERT INTO [LES].[TI_MID_PTL_PART_LABLE]
							(
								[FID],
								[BarCode],
								[PartNo],
								[PartName],
								[BoxPart],
								[OrderNo],
								[Qty],
								[CreateTime]
							)
							SELECT
								[FID],
								[BARCODE],
								[PARTNO],
								[PARTNAME],
								[BOXPARTS],
								[ORDERNO],
								[QTY],
								GETDATE()
							FROM @PARTLABEL
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
								@FID,
								'008',
								'TI_MID_PTL_PART_LABLE',
								0,
								'',
								1,
								'admin',
								GETDATE()
						END
				END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'PTL', '[LES].[PROC_INTERFACE_LES_PTL_PART_LABLE]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END