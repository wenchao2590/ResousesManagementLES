/********************************************************************/
/*   Project Name:  SAP												*/
/*   Program Name:  [LES].[PROC_INTERFACE_SAP_LES_QUALITY_CLAIM]	*/
/*   Called By:     window service									*/
/*   Author:        孙述霄											*/
/*   Date:			2018-01-16										*/
/*   Note:			SAP下发质量索赔数据本地化						*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_INTERFACE_SAP_LES_QUALITY_CLAIM]
AS
BEGIN
	SET XACT_ABORT ON
	SET NOCOUNT ON
    BEGIN TRY
        BEGIN TRANSACTION

		--定义单号临时表
		DECLARE @IDTABLE TABLE
		(
			[ID] INT
		)

		INSERT INTO @IDTABLE
		(
			[ID]
		)
		SELECT
			[ID]
		FROM [LES].[TI_SPM_QUALITY_CLAIM_SAP] WITH (NOLOCK)
		WHERE ISNULL([PROCESS_STATUS], 0) = 0 AND DATEADD(MINUTE, 3, [CREATE_DATE]) <= GETDATE()

		IF EXISTS (SELECT TOP 1 [ID] FROM @IDTABLE)
			BEGIN
				--更新标识
				UPDATE [LES].[TI_SPM_QUALITY_CLAIM_SAP] WITH (ROWLOCK)
				SET [PROCESS_STATUS] = 1,
					[PROCESS_DATE] = GETDATE()
				WHERE [ID] IN (SELECT [ID] FROM @IDTABLE)

				--清空正式表
				TRUNCATE TABLE [LES].[TT_SPM_QUALITY_CLAIM]

				--插入数据
				INSERT INTO [LES].[TT_SPM_QUALITY_CLAIM]
				(
					[NAME1_D],
					[CLANO],
					[VHVIN],
					[CMILE],
					[CLAMT],
					[DUCODE],
					[DUCODE_NAME],
					[MONAM],
					[PRODAT],
					[REPDAT],
					[TRDESC],
					[CUDESC],
					[LAAMT],
					[PAAMT],
					[MAAMT],
					[FRAMT],
					[OTAMT],
					[MSDESC],
					[PANUM],
					[LIFNR],
					[NAME1_K],
					[BW_PERSON],
					[BW_PHONE],
					[BW_EMAIL],
					[CLAIM_BATCH],
					[CREATE_USER],
					[CREATE_DATE]
				)
				SELECT
					[NAME1_D],
					[CLANO],
					[VHVIN],
					[CMILE],
					[CLAMT],
					[DUCODE],
					[DUCODE_NAME],
					[MONAM],
					[PRODAT],
					[REPDAT],
					[TRDESC],
					[CUDESC],
					[LAAMT],
					[PAAMT],
					[MAAMT],
					[FRAMT],
					[OTAMT],
					[MSDESC],
					[PANUM],
					[LIFNR],
					[NAME1_K],
					[BW_PERSON],
					[BW_PHONE],
					[BW_EMAIL],
					[CLAIM_BATCH],
					'admin',
					GETDATE()
				FROM [LES].[TI_SPM_QUALITY_CLAIM_SAP] WITH (NOLOCK)
				WHERE [ID] IN (SELECT [ID] FROM @IDTABLE)
			END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_INTERFACE_SAP_LES_QUALITY_CLAIM]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END