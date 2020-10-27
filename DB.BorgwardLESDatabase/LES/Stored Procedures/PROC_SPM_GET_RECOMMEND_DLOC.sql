/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_SPM_GET_RECOMMEND_DLOC]							 */
/*   Called By:     by the PDA													 */
/*   Purpose:       获取推荐库位												 */
/*   author:        孙述霄	2017-08-09   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_GET_RECOMMEND_DLOC]
(
    @Plant NVARCHAR(8),						--工厂
	@WmNo NVARCHAR(16),						--仓库
	@ZoneNo NVARCHAR(32),					--存储区
	@PartNo NVARCHAR(32),					--零件号
	@LoginName NVARCHAR(50),				--处理人
	@IsOverFlow BIT OUTPUT,					--是否溢库库位
	@Dloc NVARCHAR(32) OUTPUT				--推荐库位
)
AS
BEGIN
	DECLARE @OverflowDloc NVARCHAR(32)
	SELECT @OverflowDloc = ISNULL([OVERFLOW_DLOC], '') FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK)
	WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo
	SET @OverflowDloc = ISNULL(@OverflowDloc, '')

	SELECT TOP 1
		@Dloc = A.[DLOC]
	FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] A WITH (NOLOCK)
	INNER JOIN [LES].[TR_BAS_PART_DYNAMIC_STOCK] B WITH (NOLOCK)
	ON A.[PLANT] = B.[PLANT] AND A.[WM_NO] = B.[WM_NO] AND A.[ZONE_NO] = B.[ZONE_NO] AND A.[DLOC] = B.[DLOC]
	LEFT JOIN [LES].[TR_BAS_PART_TRAY_STOCK] C WITH (NOLOCK)
	ON A.[PLANT] = C.[PLANT] AND A.[WM_NO] = C.[WM_NO] AND A.[ZONE_NO] = C.[ZONE_NO] AND A.[DLOC] = C.[DLOC]
	WHERE A.[PLANT] = @Plant AND A.[WM_NO] = @WmNo AND A.[ZONE_NO] = @ZoneNo AND A.[DLOC] <> @OverflowDloc
	AND B.[PART_NO] = @PartNo AND ISNULL(C.[DLOC_STATUS], 0) = 0
	ORDER BY ISNULL(A.[LANE_NO], 0), ISNULL(A.[SHELVES_NO], 0), ISNULL(A.[LAYER_NO], 0), ISNULL(A.[GRID_NO], 0)

	SET @Dloc = ISNULL(@Dloc, '')

	IF @Dloc = ''
		BEGIN
			SET @Dloc = @OverflowDloc
		END

	SET @IsOverFlow = 0
	IF @Dloc <> ''
		BEGIN
			IF @Dloc = @OverflowDloc
				BEGIN
					SET @IsOverFlow = 1
				END
			ELSE
				BEGIN
					IF EXISTS (SELECT 1 FROM [LES].[TR_BAS_PART_TRAY_STOCK] WITH (NOLOCK) WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc)
						BEGIN
							UPDATE [LES].[TR_BAS_PART_TRAY_STOCK] WITH (ROWLOCK)
							SET [DLOC_STATUS] = 1,
								[PART_NO] = @PartNo,
								[MODIFY_USER] = @LoginName,
								[MODIFY_DATE] = GETDATE()
							WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc
						END
					ELSE
						BEGIN
							INSERT INTO [LES].[TR_BAS_PART_TRAY_STOCK]
							(
								[PLANT],
								[WM_NO],
								[ZONE_NO],
								[DLOC],
								[PART_NO],
								[TRAY_NO],
								[BATCH_NO],
								[NUM],
								[BOX_NUM],
								[DLOC_STATUS],
								[VALID_FLAG],
								[CREATE_USER],
								[CREATE_DATE],
								[MODIFY_USER],
								[MODIFY_DATE]
							)
							SELECT
								[PLANT],
								[WM_NO],
								[ZONE_NO],
								[DLOC],
								@PartNo,
								'',
								'',
								NULL,
								NULL,
								1,
								1,
								@LoginName,
								GETDATE(),
								@LoginName,
								GETDATE()
							FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH (NOLOCK) WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc
						END
				END
		END
END