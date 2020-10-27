/****************************************************************/
/*   Project Name:  TWD											*/
/*   Program Name:  [LES].[PROC_WMM_RECEIVE_GENERATE_TRAYNO]	*/
/*   Called By:     web page									*/
/*   Author:        孙述霄										*/
/*   Create date:	2017-09-15									*/
/*   Note:			根据入库单号生成托标签						*/
/****************************************************************/
CREATE PROCEDURE [LES].[PROC_WMM_RECEIVE_GENERATE_TRAYNO]
(
	@ReceiveId INT
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @RECEIVE_NO NVARCHAR(20)
	DECLARE @PLANT NVARCHAR(5)
	DECLARE @WM_NO NVARCHAR(10)
	DECLARE @ZONE_NO NVARCHAR(20)
	DECLARE @DLOC NVARCHAR(20)
	DECLARE @ASSEMBLYLINE NVARCHAR(10)
	DECLARE @DMSNO NVARCHAR(50)
	DECLARE @FARBAU NVARCHAR(30)
	DECLARE @FARBIN NVARCHAR(30)
	DECLARE @MODEL_YEAR NVARCHAR(30)
	DECLARE @MODEL NVARCHAR(30)
	DECLARE @ZCOLORI NVARCHAR(30)
	DECLARE @ZCOLORI_D NVARCHAR(30)
	DECLARE @TWD_RUNSHEET_NO NVARCHAR(36)
	DECLARE @TRAY_NO NVARCHAR(20)
	DECLARE @TRAY_ID BIGINT
	DECLARE @RUNSHEET_TYPE INT
	DECLARE @rowCount INT
	DECLARE @flag INT
	DECLARE @now DATETIME
	SET @now = GETDATE()

	--定义本次拉动车辆表
	DECLARE @DmsnoTable TABLE
	(
		[ID] INT IDENTITY,
		[DMSNO] NVARCHAR(50)		--车号
	)

	--获取入库单信息
	SELECT
		@RECEIVE_NO = [RECEIVE_NO],
		@PLANT = [PLANT],
		@WM_NO = [WM_NO],
		@ZONE_NO = [ZONE_NO]
	FROM [LES].[TT_WMM_RECEIVE]WITH (NOLOCK)
	WHERE [RECEIVE_ID] = @ReceiveId

	--获取拉动类型
	SET @RUNSHEET_TYPE = 1
	SELECT TOP 1
		@TWD_RUNSHEET_NO = [TWD_RUNSHEET_NO]
	FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
	WHERE [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 2 AND [ASN_NO] = @RECEIVE_NO
	IF EXISTS (SELECT 1 FROM [LES].[TT_TWD_RUNSHEET] WITH (NOLOCK) WHERE [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO AND [RUNSHEET_TYPE] = 2)
		BEGIN
			SET @RUNSHEET_TYPE = 2
		END

	IF @RUNSHEET_TYPE = 1
		BEGIN
			--生成拉动车辆表
			INSERT INTO @DmsnoTable
			SELECT DISTINCT
				[DMSNO]
			FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
			WHERE [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 2 AND [ASN_NO] = @RECEIVE_NO

			--循环拉动车辆表，生成托信息
			SET @rowCount = @@ROWCOUNT
			SET @flag = 1
			WHILE(@flag <= @rowCount)
				BEGIN
					--获取车号
					SELECT
						@DMSNO = [DMSNO]
					FROM @DmsnoTable
					WHERE [ID] = @flag

					--获取车特征信息
					SELECT TOP 1
						@FARBAU = [FARBAU],
						@FARBIN = [FARBIN],
						@MODEL_YEAR = [MODEL_YEAR],
						@MODEL = [MODEL],
						@ZCOLORI = [ZCOLORI],
						@ZCOLORI_D = [ZCOLORI_D],
						@ASSEMBLYLINE = [ASSEMBLY_LINE],
						@TWD_RUNSHEET_NO = [TWD_RUNSHEET_NO]
					FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
					WHERE [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 2 AND [DMSNO] = @DMSNO AND [ASN_NO] = @RECEIVE_NO
					ORDER BY [ID] DESC

					--获取托号
					EXEC @TRAY_NO = [LES].[PROC_SYS_GET_NEXT_SEQUENCE] 'TrayNo'
					SET @TRAY_NO = 'S' + RIGHT(REPLACE(CONVERT(NVARCHAR(10), GETDATE(), 120), '-', ''), 6) + RIGHT('0000' + CAST(@TRAY_NO AS NVARCHAR), 4)
					--生成托基本信息
					INSERT INTO [LES].[TM_SPM_TRAY]
					(
						[TRAY_NO],
						[TRAY_BARCODE_TYPE],
						[TRAY_USE_TYPE],
						[TRAY_OWNER],
						[VALID_FLAG],
						[COMMENTS],
						[CREATE_USER],
						[CREATE_DATE]
					)
					VALUES
					(
						@TRAY_NO,
						1,
						1,
						'',
						1,
						'',
						'admin',
						@now
					)
					--生成托状态信息
					INSERT INTO [LES].[TT_SPM_TRAY_INFO]
					(
						[TRAY_NO],
						[PLANT],
						[WM_NO],
						[ZONE_NO],
						[DLOC],
						[BILL_NO],
						[PART_NO],
						[PART_CNAME],
						[BIND_TIME],
						[TRAY_STATUS],
						[BATCH_NO],
						[VALID_FLAG],
						[COMMENTS],
						[CREATE_USER],
						[CREATE_DATE],
						[UPDATE_USER],
						[UPDATE_DATE],
						[FARBAU],
						[FARBIN],
						[MODEL_YEAR],
						[MODEL],
						[ZCOLORI],
						[ZCOLORI_D],
						[TWD_RUNSHEET_NO],
						[ASSEMBLY_LINE],
						[DMSNO]
					)
					VALUES
					(
						@TRAY_NO,
						@PLANT,
						@WM_NO,
						@ZONE_NO,
						'',
						@RECEIVE_NO,
						'',
						'',
						@now,
						2,
						'',
						1,
						'',
						'admin',
						@now,
						'admin',
						@now,
						@FARBAU,
						@FARBIN,
						@MODEL_YEAR,
						@MODEL,
						@ZCOLORI,
						@ZCOLORI_D,
						@TWD_RUNSHEET_NO,
						@ASSEMBLYLINE,
						@DMSNO
					)
					SELECT @TRAY_ID = SCOPE_IDENTITY()
					--生成托明细信息
					INSERT INTO [LES].[TT_SPM_TRAY_DETAIL]
					(
						[TRAY_NO],
						[BARCODE_DATA],
						[PLANT],
						[WM_NO],
						[ZONE_NO],
						[DLOC],
						[PART_NO],
						[PART_CNAME],
						[NUM],
						[BIND_TIME],
						[BATCH_NO],
						[TRAY_ID],
						[BIND_FLAG],
						[VALID_FLAG],
						[COMMENTS],
						[CREATE_USER],
						[CREATE_DATE],
						[UPDATE_USER],
						[UPDATE_DATE]
					)
					SELECT
						@TRAY_NO,
						'',
						@PLANT,
						@WM_NO,
						@ZONE_NO,
						'',
						[PART_NO],
						[PART_CNAME],
						[PACK_COUNT],
						@now,
						'',
						@TRAY_ID,
						1,
						1,
						'',
						'admin',
						@now,
						'admin',
						@now
					FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
					WHERE [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 2 AND [DMSNO] = @DMSNO AND [ASN_NO] = @RECEIVE_NO
					ORDER BY [ID]

					SET @flag = @flag + 1
				END
		END
	IF @RUNSHEET_TYPE = 2
		BEGIN
			--获取车特征信息
			SELECT TOP 1
				@FARBAU = [FARBAU],
				@FARBIN = [FARBIN],
				@MODEL_YEAR = [MODEL_YEAR],
				@MODEL = [MODEL],
				@ZCOLORI = [ZCOLORI],
				@ZCOLORI_D = [ZCOLORI_D],
				@ASSEMBLYLINE = [ASSEMBLY_LINE],
				@TWD_RUNSHEET_NO = [TWD_RUNSHEET_NO],
				@DMSNO = [DMSNO]
			FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
			WHERE [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 2 AND [ASN_NO] = @RECEIVE_NO
			ORDER BY [ID] DESC

			--获取套数
			SELECT
				@rowCount = [SET_NUM]
			FROM [LES].[TT_SPS_MANUAL_PULL] WITH (NOLOCK)
			WHERE [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO
			SET @rowCount = ISNULL(@rowCount, 1)

			--循环拉动车辆表，生成托信息
			SET @flag = 1
			WHILE(@flag <= @rowCount)
				BEGIN
					--获取托号
					EXEC @TRAY_NO = [LES].[PROC_SYS_GET_NEXT_SEQUENCE] 'TrayNo'
					SET @TRAY_NO = 'S' + RIGHT(REPLACE(CONVERT(NVARCHAR(10), GETDATE(), 120), '-', ''), 6) + RIGHT('0000' + CAST(@TRAY_NO AS NVARCHAR), 4)
					--生成托基本信息
					INSERT INTO [LES].[TM_SPM_TRAY]
					(
						[TRAY_NO],
						[TRAY_BARCODE_TYPE],
						[TRAY_USE_TYPE],
						[TRAY_OWNER],
						[VALID_FLAG],
						[COMMENTS],
						[CREATE_USER],
						[CREATE_DATE]
					)
					VALUES
					(
						@TRAY_NO,
						1,
						1,
						'',
						1,
						'',
						'admin',
						@now
					)
					--生成托状态信息
					INSERT INTO [LES].[TT_SPM_TRAY_INFO]
					(
						[TRAY_NO],
						[PLANT],
						[WM_NO],
						[ZONE_NO],
						[DLOC],
						[BILL_NO],
						[PART_NO],
						[PART_CNAME],
						[BIND_TIME],
						[TRAY_STATUS],
						[BATCH_NO],
						[VALID_FLAG],
						[COMMENTS],
						[CREATE_USER],
						[CREATE_DATE],
						[UPDATE_USER],
						[UPDATE_DATE],
						[FARBAU],
						[FARBIN],
						[MODEL_YEAR],
						[MODEL],
						[ZCOLORI],
						[ZCOLORI_D],
						[TWD_RUNSHEET_NO],
						[ASSEMBLY_LINE],
						[DMSNO]
					)
					VALUES
					(
						@TRAY_NO,
						@PLANT,
						@WM_NO,
						@ZONE_NO,
						'',
						@RECEIVE_NO,
						'',
						'',
						@now,
						2,
						'',
						1,
						'',
						'admin',
						@now,
						'admin',
						@now,
						@FARBAU,
						@FARBIN,
						@MODEL_YEAR,
						@MODEL,
						@ZCOLORI,
						@ZCOLORI_D,
						@TWD_RUNSHEET_NO,
						@ASSEMBLYLINE,
						@DMSNO
					)
					SELECT @TRAY_ID = SCOPE_IDENTITY()
					--生成托明细信息
					INSERT INTO [LES].[TT_SPM_TRAY_DETAIL]
					(
						[TRAY_NO],
						[BARCODE_DATA],
						[PLANT],
						[WM_NO],
						[ZONE_NO],
						[DLOC],
						[PART_NO],
						[PART_CNAME],
						[NUM],
						[BIND_TIME],
						[BATCH_NO],
						[TRAY_ID],
						[BIND_FLAG],
						[VALID_FLAG],
						[COMMENTS],
						[CREATE_USER],
						[CREATE_DATE],
						[UPDATE_USER],
						[UPDATE_DATE]
					)
					SELECT
						@TRAY_NO,
						'',
						@PLANT,
						@WM_NO,
						@ZONE_NO,
						'',
						[PART_NO],
						[PART_CNAME],
						[PACK_COUNT],
						@now,
						'',
						@TRAY_ID,
						1,
						1,
						'',
						'admin',
						@now,
						'admin',
						@now
					FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
					WHERE [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 2 AND [ASN_NO] = @RECEIVE_NO
					ORDER BY [ID]

					SET @flag = @flag + 1
				END
		END

	--更新托日志表
	UPDATE [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (ROWLOCK)
	SET [IS_GENERATE] = 1,
		[UPDATE_DATE] = GETDATE(),
		[UPDATE_USER] = 'admin'
	WHERE [IS_ASN] = 1 AND [PROCESS_FLAG] = 1 AND [IS_GENERATE] = 2 AND [ASN_NO] = @RECEIVE_NO

	SET NOCOUNT OFF
END