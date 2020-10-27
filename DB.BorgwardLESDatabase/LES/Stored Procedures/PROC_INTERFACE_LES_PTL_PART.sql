/*************************************************************/
/*   Project Name:  PTL									     */
/*   Program Name:  [LES].[PROC_INTERFACE_LES_PTL_PART]  	 */
/*   Called By:     window service							 */
/*   Author:        孙述霄									 */
/*   Date:			2017-11-15								 */
/*   Note:			同步PTL零件基础信息						 */
/*************************************************************/
CREATE PROCEDURE [LES].[PROC_INTERFACE_LES_PTL_PART]
AS
BEGIN
	SET XACT_ABORT ON
	SET NOCOUNT ON
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @FID UNIQUEIDENTIFIER

			--定义LES零件基础信息表
			DECLARE @PARTLES TABLE
			(
				[ID] INT IDENTITY,
				[PART_NO] NVARCHAR(20),
				[PART_CNAME] NVARCHAR(100),
				[INHOUSE_PART_CLASS] NVARCHAR(16)
			)

			--定义PTL零件基础信息表
			DECLARE @PARTPTL TABLE
			(
				[ID] INT IDENTITY,
				[PART_NO] NVARCHAR(20),
				[PART_CNAME] NVARCHAR(100),
				[INHOUSE_PART_CLASS] NVARCHAR(16)
			)

			--定义零件维护信息表
			DECLARE @PARTMAIN TABLE
			(
				[ID] INT IDENTITY,
				[FID] UNIQUEIDENTIFIER,
				[PART_NO] NVARCHAR(20),
				[PART_CNAME] NVARCHAR(100),
				[INHOUSE_PART_CLASS] NVARCHAR(16),
				[OPTYPE] INT
			)

			SET @FID = NEWID()

			--生成LES零件基础数据
			INSERT INTO @PARTLES
			(
				[PART_NO],
				[PART_CNAME],
				[INHOUSE_PART_CLASS]
			)
			SELECT
				[PART_NO],
				MAX([PART_CNAME]) AS [PART_CNAME],
				--目标存储区
				PLANT_ZONE
			FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] WITH (NOLOCK)
			WHERE [INHOUSE_SYSTEM_MODE] = 'SPS' AND [DELETE_FLAG] = 0 
			GROUP BY [PART_NO], PLANT_ZONE
			ORDER BY PLANT_ZONE, [PART_NO]

			--生成PTL零件基础数据
			INSERT INTO @PARTPTL
			(
				[PART_NO],
				[PART_CNAME],
				[INHOUSE_PART_CLASS]
			)
			SELECT
				[PartNo],
				[PartName],
				[BoxPart]
			FROM [LES].[TI_MID_PTL_PART] WITH (NOLOCK)
			WHERE [OpType] <> 3 AND [ID] IN
			(
				SELECT
					MAX([ID])
				FROM [LES].[TI_MID_PTL_PART] WITH (NOLOCK)
				GROUP BY [PartNo], [BoxPart]
			)

			--增加零件信息
			INSERT INTO @PARTMAIN
			(
				[FID],
				[PART_NO],
				[PART_CNAME],
				[INHOUSE_PART_CLASS],
				[OPTYPE]
			)
			SELECT
				@FID,
				A.[PART_NO],
				A.[PART_CNAME],
				A.[INHOUSE_PART_CLASS],
				1 AS [OPTYPE]
			FROM @PARTLES A
			LEFT JOIN @PARTPTL B ON A.[PART_NO] = B.[PART_NO] AND A.[INHOUSE_PART_CLASS] = B.[INHOUSE_PART_CLASS]
			WHERE B.[ID] IS NULL

			--修改零件信息
			INSERT INTO @PARTMAIN
			(
				[FID],
				[PART_NO],
				[PART_CNAME],
				[INHOUSE_PART_CLASS],
				[OPTYPE]
			)
			SELECT
				@FID,
				A.[PART_NO],
				A.[PART_CNAME],
				A.[INHOUSE_PART_CLASS],
				2 AS [OPTYPE]
			FROM @PARTLES A
			INNER JOIN @PARTPTL B ON A.[PART_NO] = B.[PART_NO] AND A.[INHOUSE_PART_CLASS] = B.[INHOUSE_PART_CLASS]
			WHERE A.[PART_CNAME] <> B.[PART_CNAME]

			--删除零件信息
			INSERT INTO @PARTMAIN
			(
				[FID],
				[PART_NO],
				[PART_CNAME],
				[INHOUSE_PART_CLASS],
				[OPTYPE]
			)
			SELECT
				@FID,
				A.[PART_NO],
				A.[PART_CNAME],
				A.[INHOUSE_PART_CLASS],
				3 AS [OPTYPE]
			FROM @PARTPTL A
			LEFT JOIN @PARTLES B ON A.[PART_NO] = B.[PART_NO] AND A.[INHOUSE_PART_CLASS] = B.[INHOUSE_PART_CLASS]
			WHERE B.[ID] IS NULL

			IF EXISTS (SELECT 1 FROM @PARTMAIN)
				BEGIN
					--插入零件基础信息中间表
					INSERT INTO [LES].[TI_MID_PTL_PART]
					(
						[FID],
						[PartNo],
						[PartName],
						[OpType],
						[BoxPart]
					)
					SELECT
						[FID],
						[PART_NO],
						[PART_CNAME],
						[OPTYPE],
						[INHOUSE_PART_CLASS]
					FROM @PARTMAIN
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
						'010',
						'TI_MID_PTL_PART',
						0,
						'',
						1,
						'admin',
						GETDATE()
				END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'PTL', '[LES].[PROC_INTERFACE_LES_PTL_PART]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END