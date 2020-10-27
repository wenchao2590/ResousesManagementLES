/****************************************************************/
/*   Project Name:  TWD											*/
/*   Program Name:  [LES].[PROC_SPM_CREATE_ASN_UPDATE_ASNNO]	*/
/*   Called By:     web page									*/
/*   Author:        孙述霄										*/
/*   Create date:	2017-09-14									*/
/*   Note:			更新ASN临时单号								*/
/****************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_CREATE_ASN_UPDATE_ASNNO]
(
	@TWD_RUNSHEET_NO NVARCHAR(50),	--拉动单号
	@FARBAU NVARCHAR(30),			--特征代码
	@MODEL_YEAR NVARCHAR(30),		--内饰颜色
	@ZCOLORI NVARCHAR(30),			--外饰颜色
	@MODEL_NUM INT,					--车型数量
	@GENERATE_NUM INT,				--发运数量
	@ASNNO NVARCHAR(50)				--ASN单号
)
AS
BEGIN
	SET NOCOUNT ON

	IF @MODEL_NUM = @GENERATE_NUM
		BEGIN
			--全部发运
			UPDATE [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (ROWLOCK)
			SET [ASN_NO] = @ASNNO
			WHERE [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO AND [FARBAU] = @FARBAU AND [MODEL_YEAR] = @MODEL_YEAR AND [ZCOLORI] = @ZCOLORI
			AND [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 0
		END
	ELSE
		BEGIN
			--部分发运
			UPDATE [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (ROWLOCK)
			SET [ASN_NO] = @ASNNO
			WHERE [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO AND [FARBAU] = @FARBAU AND [MODEL_YEAR] = @MODEL_YEAR AND [ZCOLORI] = @ZCOLORI
			AND [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 0 AND [DMSNO] IN
			(
				SELECT
					[DMSNO]
				FROM
				(
					SELECT
						ROW_NUMBER() OVER(ORDER BY [ID]) AS [ID],
						[DMSNO]
					FROM
					(
						SELECT
							MIN([ID]) AS [ID],
							[DMSNO]
						FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
						WHERE [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO AND [FARBAU] = @FARBAU AND [MODEL_YEAR] = @MODEL_YEAR AND [ZCOLORI] = @ZCOLORI
						AND [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 0
						GROUP BY [DMSNO]
					) AS A
				) AS B
				WHERE [ID] <= @GENERATE_NUM
			)
		END

	SET NOCOUNT OFF
END