/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_SPM_GET_DLOC_STATUS]							 */
/*   Called By:     by the PDA													 */
/*   Purpose:       获取库位状态												 */
/*   author:        孙述霄	2017-08-09   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_GET_DLOC_STATUS]
(
    @Plant NVARCHAR(8),						--工厂
	@WmNo NVARCHAR(16),						--仓库
	@ZoneNo NVARCHAR(32),					--存储区
	@Dloc NVARCHAR(32),						--库位
	@LoginName NVARCHAR(50),				--处理人
	@DlocStatus INT OUTPUT					--库位状态
)
AS
BEGIN
	IF EXISTS (SELECT 1 FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH (NOLOCK) WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc)
		BEGIN
			IF EXISTS (SELECT 1 FROM [LES].[TR_BAS_PART_TRAY_STOCK] WITH (NOLOCK) WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc)
				BEGIN
					SELECT
						@DlocStatus = [DLOC_STATUS]
					FROM [LES].[TR_BAS_PART_TRAY_STOCK] WITH (NOLOCK)
					WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc
				END
			ELSE
				BEGIN
					SET @DlocStatus = ISNULL(@DlocStatus, 0)
					INSERT INTO [LES].[TR_BAS_PART_TRAY_STOCK]
					(
						[PLANT],
						[WM_NO],
						[ZONE_NO],
						[DLOC],
						[PART_NO],
						[TRAY_NO],
						[BATCH_NO],
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
						'',
						'',
						'',
						0,
						1,
						@LoginName,
						GETDATE(),
						@LoginName,
						GETDATE()
					FROM [LES].[TM_BAS_WAREHOUSE_LOCATION] WITH (NOLOCK) WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc
				END
		END
END