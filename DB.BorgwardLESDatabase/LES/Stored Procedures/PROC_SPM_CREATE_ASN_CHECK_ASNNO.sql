/********************************************************************/
/*   Project Name:  TWD												*/
/*   Program Name:  [LES].[PROC_SPM_CREATE_ASN_CHECK_ASNNO]			*/
/*   Called By:     web page										*/
/*   Author:        孙述霄											*/
/*   Create date:	2017-10-18										*/
/*   Note:			车型订单创建ASN时检查							*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_CREATE_ASN_CHECK_ASNNO]
(
	@ASNNO					NVARCHAR(50),					--ASN单号
	@RESULT					INT OUTPUT,						--执行结果，0表示成功，-1表示失败
	@RESULTMESSAGE			NVARCHAR(4000) OUTPUT 			--结果消息
)
AS
BEGIN
	DECLARE @ROWCOUNT INT

	SET @RESULT = 0
	SET @RESULTMESSAGE = ''

	IF EXISTS (SELECT 1 FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK) WHERE [ASN_NO] = @ASNNO AND [TWD_RUNSHEET_NO] IN (SELECT [TWD_RUNSHEET_NO] FROM [LES].[TT_TWD_RUNSHEET] WITH (NOLOCK) WHERE [RUNSHEET_TYPE] = 2))
		BEGIN
			SELECT
				@ROWCOUNT = COUNT(1)
			FROM
			(
				SELECT DISTINCT
					[TWD_RUNSHEET_NO]
				FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
				WHERE [ASN_NO] = @ASNNO
			) AS A
			IF @ROWCOUNT > 1
				BEGIN
					SET @RESULT = 1
					SET @RESULTMESSAGE = '紧急拉动单不允许合并处理'
				END

			IF @RESULT = 0
				BEGIN
					IF EXISTS
					(
						SELECT
							1
						FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
						WHERE [ASN_NO] <> @ASNNO AND [TWD_RUNSHEET_NO] IN
						(
							SELECT DISTINCT
								[TWD_RUNSHEET_NO]
							FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
							WHERE [ASN_NO] = @ASNNO
						)
					)
						BEGIN
							SET @RESULT = 1
							SET @RESULTMESSAGE = '紧急拉动单不允许拆分处理'
						END
				END

			IF @RESULT = 1
				BEGIN
					UPDATE [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (ROWLOCK)
					SET [ASN_NO] = NULL
					WHERE [ASN_NO] = @ASNNO
				END
		END
END