/********************************************************/
/*   Project Name:  TWD									*/
/*   Program Name:  [LES].[PROC_TWD_GENERATE_TRAYNO]	*/
/*   Called By:     window service						*/
/*   Author:        孙述霄								*/
/*   Create date:	2017-09-11							*/
/*   Note:			TWD 生成托标签						*/
/********************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_GENERATE_TRAYNO]
(
	@runsheetSN INT
)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @DETAILID BIGINT
	DECLARE @LOGID BIGINT
	DECLARE @PLANT NVARCHAR(5)
	DECLARE @ASSEMBLY_LINE NVARCHAR(10)
	DECLARE @SUPPLIER_NUM NVARCHAR(12)
	DECLARE @BOX_PARTS NVARCHAR(10)
	DECLARE @TWD_RUNSHEET_NO NVARCHAR(50)
	DECLARE @PUBLISH_TIME DATETIME
	DECLARE @EXPECTED_ARRIVAL_TIME DATETIME
	DECLARE @SUGGEST_DELIVERY_TIME DATETIME
	DECLARE @WM_NO NVARCHAR(10)
	DECLARE @ZONE_NO NVARCHAR(10)
	DECLARE @PART_NO NVARCHAR(20)
	DECLARE @PART_CNAME NVARCHAR(100)
	DECLARE @DMSNO NVARCHAR(50)
	DECLARE @TRAY_NO NVARCHAR(20)
	DECLARE @FARBAU NVARCHAR(30)
	DECLARE @FARBIN NVARCHAR(30)
	DECLARE @MODEL_YEAR NVARCHAR(30)
	DECLARE @MODEL NVARCHAR(30)
	DECLARE @ZCOLORI NVARCHAR(30)
	DECLARE @ZCOLORI_D NVARCHAR(30)
	DECLARE @TRAY_ID BIGINT
	DECLARE @REQUIRED_INBOUND_PACKAGE_QTY INT
	DECLARE @PACK_COUNT INT
	DECLARE @RUNSHEET_TYPE INT
	DECLARE @isASN INT
	DECLARE @rowCount INT
	DECLARE @flag INT
	DECLARE @now DATETIME
	SET @now = GETDATE()

	--定义本次拉动消耗零件表
	DECLARE @PartTable TABLE
	(
		[ID] INT IDENTITY,
		[PART_NO] NVARCHAR(20),		--零件号
		[PART_CNAME] NVARCHAR(100),	--零件名称
		[DMSNO] NVARCHAR(50),		--车号
		[PACK_COUNT] INT			--拉动数量
	)

	--定义本次拉动车辆表
	DECLARE @DmsnoTable TABLE
	(
		[ID] INT IDENTITY,
		[DMSNO] NVARCHAR(50)		--车号
	)
	
	--获取基本信息
	SELECT
		@PLANT = A.[PLANT],
		@ASSEMBLY_LINE = A.[ASSEMBLY_LINE],
		@SUPPLIER_NUM = A.[SUPPLIER_NUM],
		@BOX_PARTS = A.[BOX_PARTS],
		@TWD_RUNSHEET_NO = A.[TWD_RUNSHEET_NO],
		@PUBLISH_TIME = A.[PUBLISH_TIME],
		@EXPECTED_ARRIVAL_TIME = A.[EXPECTED_ARRIVAL_TIME],
		@SUGGEST_DELIVERY_TIME = A.[SUGGEST_DELIVERY_TIME],
		@WM_NO = B.[WM_NO],
		@ZONE_NO = B.[ZONE_NO],
		@RUNSHEET_TYPE = A.[RUNSHEET_TYPE],
		@isASN = A.[IS_ASN]
	FROM [LES].[TT_TWD_RUNSHEET] A WITH (NOLOCK)
	LEFT JOIN [LES].[TM_WMM_ZONES] AS B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[PLANT_ZONE]
	WHERE [TWD_RUNSHEET_SN] = @runsheetSN

	IF @RUNSHEET_TYPE = 1
		BEGIN
			--正常拉动单
			--循环拉动单明细
			SELECT @DETAILID = MIN([RUNSHEET_DETAIL_ID]) FROM [LES].[TT_TWD_RUNSHEET_DETAIL] WITH (NOLOCK) WHERE [TWD_RUNSHEET_SN] = @runsheetSN
			WHILE @DETAILID IS NOT NULL
				BEGIN
					SELECT
						@PART_NO = [PART_NO],
						@REQUIRED_INBOUND_PACKAGE_QTY = [REQUIRED_INBOUND_PACKAGE_QTY]
					FROM [LES].[TT_TWD_RUNSHEET_DETAIL] WITH (NOLOCK)
					WHERE [RUNSHEET_DETAIL_ID] =  @DETAILID

					--获取消耗明细
					SELECT @LOGID = MIN([ID]) FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @ASSEMBLY_LINE AND [SUPPLIER_NUM] = @SUPPLIER_NUM AND [BOX_PARTS] = @BOX_PARTS AND [PART_NO] = @PART_NO AND [PROCESS_FLAG] = 0
					WHILE @LOGID IS NOT NULL
						BEGIN
							SELECT @PART_CNAME = [PART_CNAME], @PACK_COUNT = [PACK_COUNT], @DMSNO = [DMSNO] FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK) WHERE [ID] = @LOGID
							SET @REQUIRED_INBOUND_PACKAGE_QTY = @REQUIRED_INBOUND_PACKAGE_QTY - @PACK_COUNT

							--插入消耗零件表
							INSERT INTO @PartTable
							(
								[PART_NO],
								[PART_CNAME],
								[DMSNO],
								[PACK_COUNT]
							)
							VALUES
							(
								@PART_NO,
								@PART_CNAME,
								@DMSNO,
								@PACK_COUNT
							)

							UPDATE [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (ROWLOCK) SET [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO, [IS_ASN] = @isASN, [PROCESS_FLAG] = 1, [PROCESS_DATE] = GETDATE(), [PUBLISH_TIME] = @PUBLISH_TIME, [EXPECTED_ARRIVAL_TIME] = @EXPECTED_ARRIVAL_TIME, [SUGGEST_DELIVERY_TIME] = @SUGGEST_DELIVERY_TIME WHERE [ID] = @LOGID
							IF @REQUIRED_INBOUND_PACKAGE_QTY <= 0
								BEGIN
									--退出循环
									BREAK
								END
							SELECT @LOGID = MIN([ID]) FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @ASSEMBLY_LINE AND [SUPPLIER_NUM] = @SUPPLIER_NUM AND [BOX_PARTS] = @BOX_PARTS AND [PART_NO] = @PART_NO AND [PROCESS_FLAG] = 0 AND [ID] > @LOGID
						END

					SELECT @DETAILID = MIN([RUNSHEET_DETAIL_ID]) FROM [LES].[TT_TWD_RUNSHEET_DETAIL] WITH (NOLOCK) WHERE [TWD_RUNSHEET_SN] = @runsheetSN AND [RUNSHEET_DETAIL_ID] > @DETAILID
				END

			IF @isASN = 0
				BEGIN
					--非ASN生成托标签
					--生成拉动车辆表
					INSERT INTO @DmsnoTable
					SELECT DISTINCT
						[DMSNO]
					FROM @PartTable

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
								@ZCOLORI_D = [ZCOLORI_D]
							FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
							WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @ASSEMBLY_LINE AND [DMSNO] = @DMSNO
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
								@TWD_RUNSHEET_NO,
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
								@ASSEMBLY_LINE,
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
							FROM @PartTable
							WHERE [DMSNO] = @DMSNO
							ORDER BY [ID]

							SET @flag = @flag + 1
						END
				END
		END

	IF @RUNSHEET_TYPE = 1
		BEGIN
			--紧急拉动
			IF @isASN = 0
				BEGIN
					--非ASN生成托标签
					--获取套数
					SELECT
						@rowCount = [SET_NUM]
					FROM [LES].[TT_SPS_MANUAL_PULL] WITH (NOLOCK)
					WHERE [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO
					SET @rowCount = ISNULL(@rowCount, 1)

					--获取车特征信息
					SELECT TOP 1
						@FARBAU = [FARBAU],
						@FARBIN = [FARBIN],
						@MODEL_YEAR = [MODEL_YEAR],
						@MODEL = [MODEL],
						@ZCOLORI = [ZCOLORI],
						@ZCOLORI_D = [ZCOLORI_D],
						@DMSNO = [DMSNO]
					FROM [LES].[TL_TWD_MATERIAL_TRAY_LOG] WITH (NOLOCK)
					WHERE [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO
					ORDER BY [ID] DESC

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
								@TWD_RUNSHEET_NO,
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
								@ASSEMBLY_LINE,
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
							WHERE [TWD_RUNSHEET_NO] = @TWD_RUNSHEET_NO
							ORDER BY [ID]

							SET @flag = @flag + 1
						END
				END
		END

	SET NOCOUNT OFF
END