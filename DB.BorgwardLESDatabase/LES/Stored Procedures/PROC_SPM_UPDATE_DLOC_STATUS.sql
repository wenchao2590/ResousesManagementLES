/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_SPM_UPDATE_DLOC_STATUS]							 */
/*   Called By:     by the PDA													 */
/*   Purpose:       更新库位状态												 */
/*   author:        孙述霄	2017-08-09   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_UPDATE_DLOC_STATUS]
(
    @Plant NVARCHAR(8),						--工厂
	@WmNo NVARCHAR(16),						--仓库
	@ZoneNo NVARCHAR(32),					--存储区
	@Dloc NVARCHAR(32),						--库位
	@TrayNo NVARCHAR(20),					--托号
	@DlocStatus INT,						--库位状态
	@LoginName NVARCHAR(50),				--处理人
	@result INT OUTPUT,
	@ResultMessage NVARCHAR(4000) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRAN

		DECLARE @NUM INT
		DECLARE @BOXNUM INT
		DECLARE @BATCHNO NVARCHAR(100)
		DECLARE @PARTNO NVARCHAR(20)
		DECLARE @ISDYNAMICDLOC INT
		DECLARE @OVERFLOWDLOC NVARCHAR(32)

		SET @Result = 0
		SET @ResultMessage = ''

		SELECT @ISDYNAMICDLOC = ISNULL([IS_DYNAMIC_DLOC], 0), @OVERFLOWDLOC = ISNULL([OVERFLOW_DLOC], '') FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK) WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo

		IF @ISDYNAMICDLOC = 1
			BEGIN
				IF @Dloc <> @OVERFLOWDLOC
					BEGIN
						IF NOT EXISTS (SELECT 1 FROM [LES].[TR_BAS_PART_TRAY_STOCK] WITH (NOLOCK) WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc)
							BEGIN
								SET @Result = 1
								SET @ResultMessage = '工厂[' + @Plant + ']仓库[' + @WmNo + ']存储区[' + @ZoneNo + ']库位[' + @Dloc + ']不存在！'
							END
					END

				IF @Result = 0
					BEGIN
						IF @DlocStatus <> 0 AND @DlocStatus <> 1 AND @DlocStatus <> 2
							BEGIN
								SET @Result = 1
								SET @ResultMessage = '库位状态[' + @DlocStatus + ']错误！库位状态：0-空闲，1-预占用，2-占用！'
							END
					END

				IF @Result = 0
					BEGIN
						IF @DlocStatus = 0
							BEGIN
								IF @OVERFLOWDLOC = @Dloc
									BEGIN
										--溢库库位，则删除
										DELETE FROM [LES].[TR_BAS_PART_TRAY_STOCK] WITH (ROWLOCK)
										WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc AND [TRAY_NO] = @TrayNo
									END
								ELSE
									BEGIN
										--非溢库库位，设置为空闲
										UPDATE [LES].[TR_BAS_PART_TRAY_STOCK] WITH (ROWLOCK)
										SET [PART_NO] = '',
											[TRAY_NO] = '',
											[BATCH_NO] = '',
											[NUM] = NULL,
											[BOX_NUM] = NULL,
											[DLOC_STATUS] = 0,
											[VALID_FLAG] = 1,
											[MODIFY_USER] = @LoginName,
											[MODIFY_DATE] = GETDATE()
										WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc
									END
							END
						IF @DlocStatus = 1
							BEGIN
								IF @OVERFLOWDLOC <> @Dloc
									BEGIN
										--非溢库库位，设置为预占用
										UPDATE [LES].[TR_BAS_PART_TRAY_STOCK] WITH (ROWLOCK)
										SET [PART_NO] = '',
											[TRAY_NO] = '',
											[BATCH_NO] = '',
											[NUM] = NULL,
											[BOX_NUM] = NULL,
											[DLOC_STATUS] = 1,
											[VALID_FLAG] = 1,
											[MODIFY_USER] = @LoginName,
											[MODIFY_DATE] = GETDATE()
										WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc
									END
							END
						IF @DlocStatus = 2
							BEGIN
								IF ISNULL(@TrayNo, '') = ''
									BEGIN
										SET @Result = 1
										SET @ResultMessage = '托盘号不能为空！'
									END

								IF NOT EXISTS (SELECT 1 FROM [LES].[TT_SPM_TRAY_INFO] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo)
									BEGIN
										SET @Result = 1
										SET @ResultMessage = '托盘号[' + @TrayNo + ']不存在！'
									END

								IF @Result = 0
									BEGIN
										SELECT @PARTNO = [PART_NO], @BATCHNO = [BATCH_NO], @NUM = [NUM], @BOXNUM = [BOX_NUM] FROM [LES].[TT_SPM_TRAY_INFO] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo

										UPDATE [LES].[TT_SPM_TRAY_INFO] WITH (ROWLOCK) SET [DLOC] = @Dloc, [TRAY_STATUS] = 4 WHERE [TRAY_NO] = @TrayNo
										UPDATE [LES].[TT_SPM_TRAY_DETAIL] WITH (ROWLOCK) SET [DLOC] = @Dloc WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1

										IF @OVERFLOWDLOC = @Dloc
											BEGIN
												--溢库库位，生成记录
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
													@Plant,
													@WmNo,
													@ZoneNo,
													@Dloc,
													@PARTNO,
													@TrayNo,
													@BATCHNO,
													@NUM,
													@BOXNUM,
													2,
													1,
													@LoginName,
													GETDATE(),
													@LoginName,
													GETDATE()
											END
										ELSE
											BEGIN
												--非溢库库位，更新记录
												UPDATE [LES].[TR_BAS_PART_TRAY_STOCK] WITH (ROWLOCK)
												SET [PART_NO] = @PARTNO,
													[TRAY_NO] = @TrayNo,
													[BATCH_NO] = @BATCHNO,
													[NUM] = @NUM,
													[BOX_NUM] = @BOXNUM,
													[DLOC_STATUS] = 2,
													[VALID_FLAG] = 1,
													[MODIFY_USER] = @LoginName,
													[MODIFY_DATE] = GETDATE()
												WHERE [PLANT] = @Plant AND [WM_NO] = @WmNo AND [ZONE_NO] = @ZoneNo AND [DLOC] = @Dloc
											END
									END
							END
					END
			END
		COMMIT TRAN
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION

        SET @Result = 1
		SET @ResultMessage = ERROR_MESSAGE()
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS],  [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_SPM_UPDATE_DLOC_STATUS]', 'Procedure', @ResultMessage, ERROR_LINE()
    END CATCH
END