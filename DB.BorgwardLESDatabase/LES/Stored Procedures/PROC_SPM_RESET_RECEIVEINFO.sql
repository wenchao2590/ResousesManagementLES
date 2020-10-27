/*********************************************************************************/
/*   Project Name:  Foton LES System											 */
/*   Program Name:  [LES].[PROC_SPM_RESET_RECEIVEINFO]							 */
/*   Called By:     by the PDA													 */
/*   Purpose:       重置PDA收货信息												 */
/*   author:        孙述霄	2017-08-18   										 */
/*********************************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_RESET_RECEIVEINFO]
(
    @ReceiveNo NVARCHAR(50),				--收货单号
	@TrayNo NVARCHAR(20),					--托号
	@LoginName NVARCHAR(50),				--处理人
	@result INT OUTPUT,
	@ResultMessage NVARCHAR(4000) OUTPUT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

		DECLARE @TRAY_NO NVARCHAR(20)
		DECLARE @ROWCOUNT INT
		DECLARE @FLAG INT

		--定义托号零时表
		DECLARE @TrayNoTable TABLE
		(
			[ID] INT IDENTITY,
			[TRAY_NO] NVARCHAR(20) --托号
		)

		--定义箱号零时表
		DECLARE @BarCodeTable TABLE
		(
			[ID] INT IDENTITY,
			[BARCODE_DATA] NVARCHAR(20) --箱号
		)

		SET @Result = 0
		SET @ResultMessage = ''

		IF ISNULL(@TrayNo, '') = ''
			BEGIN
				--获取本次已提交的托号
				INSERT INTO @TrayNoTable
				SELECT DISTINCT [TRAY_NO] FROM [LES].[TT_SPM_TRAY_DETAIL] WITH (NOLOCK) WHERE [BIND_FLAG] = 1 AND [VALID_FLAG] = 1 AND [BARCODE_DATA] IN (SELECT [BARCODE_DATA] FROM [LES].[TM_WMM_TRAN_DETAILS] WITH (NOLOCK) WHERE [TRAN_NO] = @ReceiveNo AND [TRAN_TYPE] = 1 AND [TRAN_STATE] = 0)

				--设置受影响的行数
				SET @ROWCOUNT = @@ROWCOUNT
				SET @FLAG = 1

				--获取已经组完托的托号，并解绑
				WHILE (@FLAG <= @ROWCOUNT)
					BEGIN
						--获取托号
						SELECT @TRAY_NO = [TRAY_NO] FROM @TrayNoTable WHERE [ID] = @FLAG

						--解绑
						EXEC [LES].[PROC_SPM_TRAYDETAIL_UNBIND] @TRAY_NO, @LoginName, @Result OUTPUT, @ResultMessage OUTPUT
						IF @Result = 1
							BREAK

						SET @FLAG = @FLAG + 1
					END

				--更新入库单收货明细信息
				IF @Result = 0
					BEGIN
						UPDATE A
						SET A.[Current_BOX_NUM] = ISNULL(A.[Current_BOX_NUM], 0) - B.[BOX_NUM],
							A.[Current_QTY] = ISNULL(A.[Current_QTY], 0) - B.[NUM]
						FROM [LES].[TT_WMM_RECEIVE_DETAIL] A WITH (ROWLOCK)
						INNER JOIN
						(
							SELECT
								A.[PART_NO],
								ISNULL(B.[TWD_RUNSHEET_NO], '') AS [TWD_RUNSHEET_NO],
								CAST(SUM(A.[NUM]) AS INT) AS [NUM],
								COUNT(1) AS [BOX_NUM]
							FROM [LES].[TM_WMM_TRAN_DETAILS] A WITH (NOLOCK)
							INNER JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] B WITH (NOLOCK) ON A.[BARCODE_DATA] = B.[BARCODE_DATA]
							WHERE A.[TRAN_NO] = @ReceiveNo AND A.[TRAN_TYPE] = 1 AND A.[TRAN_STATE] = 0
							GROUP BY A.[PART_NO], ISNULL(B.[TWD_RUNSHEET_NO], '')
						) AS B
						ON A.[PART_NO] = B.[PART_NO] AND ISNULL(A.[TWD_RUNSHEET_NO], '') = B.[TWD_RUNSHEET_NO]
						WHERE A.[RECEIVE_ID] IN (SELECT [RECEIVE_ID] FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK) WHERE [RECEIVE_NO] = @ReceiveNo)
					END

				--删除本次提交的交易明细
				IF @Result = 0
					BEGIN
						DELETE FROM [LES].[TM_WMM_TRAN_DETAILS] WITH (ROWLOCK) WHERE [TRAN_NO] = @ReceiveNo AND [TRAN_TYPE] = 1 AND [TRAN_STATE] = 0
					END
			END
		ELSE
			BEGIN
				--拆托
				--获取本次提交托号对应的箱号
				INSERT INTO @BarCodeTable
				SELECT DISTINCT [BARCODE_DATA] FROM [LES].[TT_SPM_TRAY_DETAIL] WITH (NOLOCK) WHERE [TRAY_NO] = @TrayNo AND [BIND_FLAG] = 1 AND [VALID_FLAG] = 1 AND [BARCODE_DATA] IN (SELECT [BARCODE_DATA] FROM [LES].[TM_WMM_TRAN_DETAILS] WITH (NOLOCK) WHERE [TRAN_NO] = @ReceiveNo AND [TRAN_TYPE] = 1 AND [TRAN_STATE] = 0)

				--设置受影响的行数
				SET @ROWCOUNT = @@ROWCOUNT
				IF @ROWCOUNT = 0
					BEGIN
						SET @Result = 1
						SET @ResultMessage = '此托[' + @TrayNo + ']对应的箱已经完成收货，不能拆托！'
					END

				--解绑
				IF @Result = 0
					BEGIN
						EXEC [LES].[PROC_SPM_TRAYDETAIL_UNBIND] @TrayNo, @LoginName, @Result OUTPUT, @ResultMessage OUTPUT
					END

				--更新入库单收货明细信息
				IF @Result = 0
					BEGIN
						UPDATE A
						SET A.[Current_BOX_NUM] = ISNULL(A.[Current_BOX_NUM], 0) - B.[BOX_NUM],
							A.[Current_QTY] = ISNULL(A.[Current_QTY], 0) - B.[NUM]
						FROM [LES].[TT_WMM_RECEIVE_DETAIL] A WITH (ROWLOCK)
						INNER JOIN
						(
							SELECT
								A.[PART_NO],
								ISNULL(B.[TWD_RUNSHEET_NO], '') AS [TWD_RUNSHEET_NO],
								CAST(SUM(A.[NUM]) AS INT) AS [NUM],
								COUNT(1) AS [BOX_NUM]
							FROM [LES].[TM_WMM_TRAN_DETAILS] A WITH (NOLOCK)
							INNER JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET_BARCODE] B WITH (NOLOCK) ON A.[BARCODE_DATA] = B.[BARCODE_DATA]
							WHERE A.[TRAN_NO] = @ReceiveNo AND A.[TRAN_TYPE] = 1 AND A.[TRAN_STATE] = 0 AND A.[BARCODE_DATA] IN (SELECT [BARCODE_DATA] FROM @BarCodeTable)
							GROUP BY A.[PART_NO], ISNULL(B.[TWD_RUNSHEET_NO], '')
						) AS B
						ON A.[PART_NO] = B.[PART_NO] AND ISNULL(A.[TWD_RUNSHEET_NO], '') = B.[TWD_RUNSHEET_NO]
						WHERE A.[RECEIVE_ID] IN (SELECT [RECEIVE_ID] FROM [LES].[TT_WMM_RECEIVE] WITH (NOLOCK) WHERE [RECEIVE_NO] = @ReceiveNo)
					END

				--删除本次提交的交易明细
				IF @Result = 0
					BEGIN
						DELETE FROM [LES].[TM_WMM_TRAN_DETAILS] WITH (ROWLOCK) WHERE [TRAN_NO] = @ReceiveNo AND [TRAN_TYPE] = 1 AND [TRAN_STATE] = 0 AND [BARCODE_DATA] IN (SELECT [BARCODE_DATA] FROM @BarCodeTable)
					END
			END

		IF @Result = 0
			BEGIN
				COMMIT TRANSACTION
			END
		ELSE
			BEGIN
				ROLLBACK TRANSACTION
			END
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION

        SET @Result = 1
		SET @ResultMessage = ERROR_MESSAGE()
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS],  [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_SPM_RESET_RECEIVEINFO]', 'Procedure', @ResultMessage, ERROR_LINE()
    END CATCH
END