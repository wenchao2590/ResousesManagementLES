/********************************************************************/
/*   Project Name:  TWD												*/
/*   Program Name:  [LES].[PROC_TWD_GENERATE_MANUAL_KIT_RUNSHEET]	*/
/*   Called By:     web page										*/
/*   Author:        孙述霄											*/
/*   Create date:	2017-10-18										*/
/*   Note:			成套紧急拉动单									*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_GENERATE_MANUAL_KIT_RUNSHEET]
(
	@SEQUENCE_NO			BIGINT,							--拉动序号
	@EXPECTED_ARRIVAL_TIME	DATETIME,						--期望到达时间
	@REMARK					NVARCHAR(200),					--备注
	@LOGINNAME				NVARCHAR(50),					--处理人
	@RESULT					INT OUTPUT,						--执行结果，0表示成功，-1表示失败
	@RESULTMESSAGE			NVARCHAR(4000) OUTPUT 			--结果消息
)
AS
BEGIN
	DECLARE @PULL_FLAG NVARCHAR(10)							--拉动标记
	DECLARE @PLANT NVARCHAR(5)								--工厂编号
	DECLARE @ASSEMBLY_LINE NVARCHAR(10)						--流水线编号
	DECLARE @BOX_PARTS NVARCHAR(10)							--零件类
	DECLARE @SUPPLIER_NUM NVARCHAR(12)						--供应商
	DECLARE @VIN NVARCHAR(30)								--VIN号
	DECLARE @ORDER_ID NVARCHAR(50)							--订单号
	DECLARE @PROCESS_FLAG INT								--处理状态
	DECLARE @DELIVERY_LOCATION NVARCHAR(20)					--目标仓库
	DECLARE @PLANT_ZONE NVARCHAR(10)						--目标存储区
	DECLARE @SUPPLIER_TYPE INT								--供应商类型
	DECLARE @ASNFLAG INT									--ASN标识
	DECLARE @DOCK NVARCHAR(10)								--DOCK
    DECLARE @TRANS_SUPPLIER_NUM NVARCHAR(20)				--承运商
	DECLARE @TRANSPORT_TIME INT								--运输时间
	DECLARE @UNLOAD_TIME INT								--卸货时间
	DECLARE @BOXSTATE INT									--零件类状态
	DECLARE @IS_ORGANIZE_SHEET INT							--是否生成拉动单
	DECLARE @IS_TRAY INT									--是否按套组托
	DECLARE @SUGGESTTIME DATETIME							--建议发货时间
	DECLARE @RUNSHEETTYPE INT								--拉动单类型
	DECLARE @RUNSHEETID INT									--拉动单序号
	DECLARE @RUNSHEETNO NVARCHAR(30)						--拉动单号

    DECLARE @sendTime DATETIME
	DECLARE @sendstatus INT
	DECLARE @flexSn INT
	DECLARE @boxSn INT
	DECLARE @interfaceId INT
	DECLARE @SUPPLIER_NUMSn INT
	DECLARE @Format NVARCHAR(6)
	DECLARE @StartRunnNo NVARCHAR(8)
	DECLARE @EndRunnNo NVARCHAR(8)
	DECLARE @NOW DATETIME									--当前时间

	SET @RESULT = 0
	SET @RESULTMESSAGE = ''

	--TWD拉动单编码的序号问题,使用事务	并捕捉异常
	BEGIN TRY
		BEGIN TRANSACTION
			SET @NOW = GETDATE()
			SET @RUNSHEETTYPE = 2
			IF @EXPECTED_ARRIVAL_TIME IS NULL
				BEGIN
					SELECT @EXPECTED_ARRIVAL_TIME = @NOW
				END

			--获取拉动信息
			SELECT
				@PULL_FLAG = [PULL_FLAG],
				@PLANT = [PLANT],
				@ASSEMBLY_LINE = [ASSEMBLY_LINE],
				@BOX_PARTS = [BOX_PARTS],
				@SUPPLIER_NUM = [SUPPLIER_NUM],
				@VIN = [VIN],
				@PROCESS_FLAG = [PROCESS_FLAG]
			FROM [LES].[TT_SPS_MANUAL_PULL] WITH (NOLOCK) WHERE [SEQUENCE_NO] = @SEQUENCE_NO

			--获取订单号
			SELECT * FROM [LES].[TT_BAS_PULL_ORDERS] WITH (NOLOCK)
			SELECT @ORDER_ID = [ORDER_NO] FROM [LES].[TT_BAS_PULL_ORDERS] WITH (NOLOCK) WHERE [WERK] = @PLANT AND [VIN] = @VIN

			IF @PROCESS_FLAG IS NULL
				BEGIN
					SET @RESULT = 1
					SET @RESULTMESSAGE = '拉动序号不存在'
				END

			IF @RESULT = 0
				BEGIN
					IF @PROCESS_FLAG = 1
						BEGIN
							SET @RESULT = 1
							SET @RESULTMESSAGE = '此拉动信息已经生成拉动单'
						END
				END

			IF @RESULT = 0
				BEGIN
					--目标仓库、目标存储区
					SELECT TOP 1 @DELIVERY_LOCATION = [WAREHOUSE], @PLANT_ZONE = [PLANT_ZONE] FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @ASSEMBLY_LINE AND [INHOUSE_PART_CLASS] = @BOX_PARTS AND [SUPPLIER_NUM] = @SUPPLIER_NUM AND [INHOUSE_SYSTEM_MODE] = @PULL_FLAG
					--供应商类型及是否ASN
					SELECT @SUPPLIER_TYPE = [SUPPLIER_TYPE], @ASNFLAG=(CASE WHEN [ASN_FLAG] = 0 OR [ASN_FLAG] IS NULL THEN 0 ELSE 1 END) FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK) WHERE [SUPPLIER_NUM] = @SUPPLIER_NUM
			
					IF UPPER(@PULL_FLAG) = 'TWD'
						BEGIN
							SELECT
								@DOCK = [DOCK],
								@TRANS_SUPPLIER_NUM = [TRANS_SUPPLIER_NUM],
								@TRANSPORT_TIME = [TRANSPORT_TIME],
								@UNLOAD_TIME = [UNLOAD_TIME],
								@BOXSTATE = ISNULL([BOX_PARTS_STATE],1),
								@IS_ORGANIZE_SHEET = ISNULL([IS_ORGANIZE_SHEET],0),
								@IS_TRAY = [IS_TRAY]
							FROM [LES].[TM_TWD_BOX_PARTS] WITH (NOLOCK)
    						WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @ASSEMBLY_LINE AND [SUPPLIER_NUM] = @SUPPLIER_NUM AND [BOX_PARTS] = @BOX_PARTS

							--获取建议发货时间
							SELECT @SUGGESTTIME = [LES].[FN_GET_TWD_SENDTIME](@PLANT, @ASSEMBLY_LINE, @EXPECTED_ARRIVAL_TIME, ISNULL(@TRANSPORT_TIME, 0) + ISNULL(@UNLOAD_TIME, 0))
							--获取拉动单号
							EXEC [LES].[PROC_TWD_GET_RUNSHEET_NO] @PLANT, @ASSEMBLY_LINE, @SUPPLIER_NUM, @RUNSHEETNO OUTPUT, 'S'

    						INSERT INTO [LES].[TT_TWD_RUNSHEET]
    						(
								[TWD_RUNSHEET_NO],
    							[PLANT],
    							[ASSEMBLY_LINE],
    							[WORKSHOP],
    							[PLANT_ZONE],
    							[PUBLISH_TIME],
    							[RUNSHEET_TYPE],
    							[SUPPLIER_NUM],
    							[SUPPLIER_SN],
    							[DOCK],
    							[DELIVERY_LOCATION],
    							[BOX_PARTS],
    							[PART_TYPE],
    							[UNLOADING_TIME],
    							[EXPECTED_ARRIVAL_TIME],
    							[SUGGEST_DELIVERY_TIME],
    							[ACTUAL_ARRIVAL_TIME],
    							[VERIFY_TIME],
    							[REJECT_REASON],
    							[TRANS_SUPPLIER_NUM],
    							[FEEDBACK],
    							[SHEET_STATUS],
    							[SEND_TIME],
    							[SEND_STATUS],
    							[OPERATON_USER],
    							[CHECK_USER],
    							[RETRY_TIMES],
    							[SUPPLY_TIME],
    							[SUPPLY_STATUS],
    							[FAX_TIME],
    							[FAX_STATUS],
    							[SAP_FLAG],
    							[SAP_FLAG2],
    							[RECKONING_NO],
    							[WMS_SEND_TIME],
    							[WMS_SEND_STATUS],
    							[COMMENTS],
    							[UPDATE_DATE],
    							[UPDATE_USER],
    							[CREATE_DATE],
    							[CREATE_USER],
								[INHOUSE_SYSTEM_MODE],
								[GENERATE_TYPE],
								[IS_ASN],
								[IS_TRAY]
							)
    						VALUES
    						(
								@RUNSHEETNO,			--<TWD_RUNSHEET_NO, varchar(18),>
    							@PLANT,					--<PLANT, nvarchar(5),>
    							@ASSEMBLY_LINE,			--<ASSEMBLY_LINE, nvarchar(10),>
    							NULL,					--<WORKSHOP, nvarchar(4),>
    							@PLANT_ZONE,			--<PLANT_ZONE, nvarchar(5),>
    							@NOW,					--<PUBLISH_TIME, datetime,>
    							@RUNSHEETTYPE,			--<RUNSHEET_TYPE, int,>
    							@SUPPLIER_NUM,			--<SUPPLIER_NUM, nvarchar(12),>
    							0,						--<SUPPLIER_SN, int,>
    							@DOCK,					--<DOCK, nvarchar(10),>
    							@DELIVERY_LOCATION,		--<DELIVERY_LOCATION, nvarchar(50),>
    							@BOX_PARTS,				--<BOX_PARTS, nvarchar(10),>
    							0,						--<PART_TYPE, int,>
    							NULL,					--<UNLOADING_TIME, int,>
    							@EXPECTED_ARRIVAL_TIME,	--<EXPECTED_ARRIVAL_TIME, datetime,>
    							@SUGGESTTIME,			--<SUGGEST_DELIVERY_TIME, datetime,>
    							NULL,					--<ACTUAL_ARRIVAL_TIME, datetime,>
    							NULL,					--<VERIFY_TIME, datetime,>
    							NULL,					--<REJECT_REASON, nvarchar(200),>
    							@TRANS_SUPPLIER_NUM,	--<TRANS_SUPPLIER_NUM, nvarchar(8),>
    							NULL,					--<FEEDBACK, nvarchar(100),>
    							0,						--<SHEET_STATUS, int,>
    							@NOW,					--<SEND_TIME, datetime,>
    							@BOXSTATE,				--<SEND_STATUS, int,>
    							NULL,					--<OPERATON_USER, nvarchar(10),>
    							NULL,					--<CHECK_USER, nvarchar(10),>
    							0,						--<RETRY_TIMES, int,>
    							NULL,					--<SUPPLY_TIME, datetime,>
    							0,						--<SUPPLY_STATUS, int,>
    							NULL,					--<FAX_TIME, datetime,>
    							0,						--<FAX_STATUS, int,>
    							0,						--<SAP_FLAG, int,>
    							0,						--<SAP_FLAG2, int,>
    							NULL,					--<RECKONING_NO, nvarchar(30),>
    							NULL,					--<WMS_SEND_TIME, datetime,>
    							@BOXSTATE,				--<WMS_SEND_STATUS, int,>
    							@REMARK,				--<COMMENTS, nvarchar(200),>
    							NULL,					--<UPDATE_DATE, datetime,>
    							NULL,					--<UPDATE_USER, nvarchar(50),>
    							@NOW,					--<CREATE_DATE, datetime,>
    							@LOGINNAME,				--<CREATE_USER, nvarchar(50),>
    							@PULL_FLAG,				--<INHOUSE_SYSTEM_MODE, nvarchar(10),>
								@IS_ORGANIZE_SHEET,		--<GENERATE_TYPE, int,>
								@ASNFLAG,				--<IS_ASN, int,>
								@IS_TRAY				--<IS_TRAY, int,>
							)

    						SELECT @RUNSHEETID = SCOPE_IDENTITY()

    						INSERT INTO [LES].[TT_TWD_RUNSHEET_DETAIL]
    						(
								[TWD_RUNSHEET_SN],
    							[PLANT],
    							[ASSEMBLY_LINE],
    							[SUPPLIER_NUM],
    							[PART_NO],
    							[IDENTIFY_PART_NO],
    							[PART_CNAME],
    							[PART_ENAME],
    							[DOCK],
    							[BOX_PARTS],
    							[SEQUENCE_NO],
    							[PICKUP_SEQ_NO],
    							[RDC_DLOC],
    							[INBOUND_PACKAGE],
    							[MEASURING_UNIT_NO],
    							[INBOUND_PACKAGE_MODEL],
    							[PACK_COUNT],
    							[REQUIRED_INBOUND_PACKAGE],
    							[REQUIRED_INBOUND_PACKAGE_QTY],
    							[ACTUAL_INBOUND_PACKAGE],
    							[ACTUAL_INBOUND_PACKAGE_QTY],
    							[BARCODE_DATA],
    							[COMMENTS],
								[PRINT_TIMES],
								[PRINT_STATE],
								[PRINT_SUPPLEMENT]
							)
							SELECT
    							@RUNSHEETID,												--<TWD_RUNSHEET_SN, int,>
    							A.[PLANT],													--<PLANT, nvarchar(5),>
    							A.[ASSEMBLY_LINE],											--<ASSEMBLY_LINE, nvarchar(10),>
    							A.[SUPPLIER_NUM],											--<SUPPLIER_NUM, nvarchar(12),>
    							B.[PART_NO],												--<PART_NO, nvarchar(20),>
    							B.[PART_NO] AS [INDENTIFY_PART_NO],							--<INDENTIFY_PART_NO, nvarchar(20),>
    							MAX(B.[PART_CNAME]) AS [PART_CNAME],						--<PART_CNAME, nvarchar(100),>
    							MAX(B.[PART_ENAME]) AS [PART_ENAME],						--<PART_ENAME, nvarchar(100),>
    							@DOCK,														--<DOCK, nvarchar(10),>
    							A.[BOX_PARTS],												--<BOX_PARTS, nvarchar(10),>
    							0,															--<SEQUENCE_NO, int,>
    							0,															--<PICKUP_SEQ_NO, int,>
    							NULL,														--<RDC_DLOC, varchar(20),>
    							MAX(B.[INBOUND_PACKAGE]) AS [INBOUND_PACKAGE],				--<INBOUND_PACKAGE, int,>
    							MAX(ISNULL(F.[PART_UNITS], 1)) AS [MEASURING_UNIT_NO],		--<MEASURING_UNIT_NO, nvarchar(8),>
    							MAX(B.[INBOUND_PACKAGE_MODEL]) AS [INBOUND_PACKAGE_MODEL],	--<INBOUND_PACKAGE_MODEL, nvarchar(30),>
    							SUM(CAST(D.[QUANTITY] AS INT)) * MAX(A.[SET_NUM]),			--<PACK_COUNT, int,>
    							SUM(CAST(D.[QUANTITY] AS INT)) * MAX(A.[SET_NUM]) / MAX(ISNULL(B.[INBOUND_PACKAGE], 1)),
																							--<REQUIRED_INBOUND_PACKAGE, int,>
    							SUM(CAST(D.[QUANTITY] AS INT)) * MAX(A.[SET_NUM]),				--<REQUIRED_INBOUND_PACKAGE_QTY, int,>
    							0,															--<ACTUAL_INBOUND_PACKAGE, int,>
    							0,															--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
    							NULL,														--<BARCODE_DATA, nvarchar(50),>
    							NULL,														--<COMMENTS, nvarchar(200),>
								0,															--<[PRINT_TIMES], INT>
								0,															--<[PRINT_STATE], INT>
								0															--<[PRINT_SUPPLEMENT], INT>
							FROM [LES].[TT_SPS_MANUAL_PULL] A WITH (NOLOCK)
    						INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND B.[SUPPLIER_NUM] = A.[SUPPLIER_NUM] AND B.[INHOUSE_PART_CLASS] = A.[BOX_PARTS] AND B.[INHOUSE_SYSTEM_MODE] = A.[PULL_FLAG] AND B.[DELETE_FLAG] = 0
							INNER JOIN [LES].[TT_BAS_PULL_ORDERS] C WITH (NOLOCK) ON C.[WERK] = A.[PLANT] AND C.[VIN] = A.[VIN]
							INNER JOIN [LES].[TT_BAS_ORDER_PART_RESULTS] D WITH (NOLOCK) ON D.[PLANT] = C.[WERK] AND D.[ORDER_NO] = C.[ORDER_NO] AND D.[PART_NO] = B.[PART_NO]
							INNER JOIN [LES].[TM_TWD_BOX_PARTS] E WITH (NOLOCK) ON E.[PLANT] = A.[PLANT] AND E.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND E.[BOX_PARTS] = A.[BOX_PARTS] AND E.[SUPPLIER_NUM] = A.[SUPPLIER_NUM]
							LEFT JOIN [LES].[TM_BAS_MAINTAIN_PARTS] F WITH (NOLOCK) ON F.[PLANT] = B.[PLANT] AND F.[PART_NO] = B.[PART_NO]
							WHERE A.[SEQUENCE_NO] = @SEQUENCE_NO
							GROUP BY A.[PLANT], A.[ASSEMBLY_LINE], A.[BOX_PARTS], A.[SUPPLIER_NUM], B.[PART_NO]

							INSERT INTO [LES].[TL_TWD_MATERIAL_TRAY_LOG]
							(
								[PLANT],
								[ASSEMBLY_LINE],
								[SUPPLIER_NUM],
								[BOX_PARTS],
								[DMSNO],
								[PART_NO],
								[PART_CNAME],
								[PACK_COUNT],
								[TWD_RUNSHEET_NO],
								[FARBAU],
								[FARBIN],
								[MODEL_YEAR],
								[MODEL],
								[ZCOLORI],
								[ZCOLORI_D],
								[IS_ASN],
								[PROCESS_FLAG],
								[PROCESS_DATE],
								[IS_GENERATE],
								[ASN_NO],
								[UPDATE_DATE],
								[UPDATE_USER],
								[CREATE_DATE],
								[CREATE_USER],
								[PUBLISH_TIME],
								[EXPECTED_ARRIVAL_TIME],
								[SUGGEST_DELIVERY_TIME]
							)
							SELECT
								A.[PLANT],
								A.[ASSEMBLY_LINE],
								A.[SUPPLIER_NUM],
								A.[BOX_PARTS],
								C.[ORDER_NO],
								B.[PART_NO],
								B.[PART_CNAME],
								CAST(D.[QUANTITY] AS INT),
								@RUNSHEETNO,
								C.[FARBAU],
								C.[FARBIN],
								C.[MODEL_YEAR],
								C.[MODEL],
								C.[ZCOLORI],
								C.[ZCOLORI_D],
								@ASNFLAG AS [IS_ASN],
								1 AS [PROCESS_FLAG],
								@NOW AS [PROCESS_DATE],
								0 AS [IS_GENERATE],
								NULL AS [ASN_NO],
								NULL AS [UPDATE_DATE],
								NULL AS [UPDATE_USER],
								@NOW AS [CREATE_DATE],
								@LOGINNAME AS [CREATE_USER],
								@NOW AS [PUBLISH_TIME],
								@EXPECTED_ARRIVAL_TIME AS [EXPECTED_ARRIVAL_TIME],
								@SUGGESTTIME AS [SUGGEST_DELIVERY_TIME]
							FROM [LES].[TT_SPS_MANUAL_PULL] A WITH (NOLOCK)
    						INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND B.[SUPPLIER_NUM] = A.[SUPPLIER_NUM] AND B.[INHOUSE_PART_CLASS] = A.[BOX_PARTS] AND B.[INHOUSE_SYSTEM_MODE] = A.[PULL_FLAG] AND B.[DELETE_FLAG] = 0
							INNER JOIN [LES].[TT_BAS_PULL_ORDERS] C WITH (NOLOCK) ON C.[WERK] = A.[PLANT] AND C.[VIN] = A.[VIN]
							INNER JOIN [LES].[TT_BAS_ORDER_PART_RESULTS] D WITH (NOLOCK) ON D.[PLANT] = C.[WERK] AND D.[ORDER_NO] = C.[ORDER_NO] AND D.[PART_NO] = B.[PART_NO]
							INNER JOIN [LES].[TM_TWD_BOX_PARTS] E WITH (NOLOCK) ON E.[PLANT] = A.[PLANT] AND E.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND E.[BOX_PARTS] = A.[BOX_PARTS] AND E.[SUPPLIER_NUM] = A.[SUPPLIER_NUM]
							WHERE A.[SEQUENCE_NO] = @SEQUENCE_NO
					
							IF @SUPPLIER_TYPE = 1 AND @ASNFLAG = 0
								BEGIN
									--生成托标签
									EXEC [LES].[PROC_TWD_GENERATE_TRAYNO] @RUNSHEETID
								END

							--创建仓库出库单
							EXEC [LES].[PROC_INSERT_OUTPUT] @RUNSHEETID, 'TWD'

							--投递二级拉动
							EXEC [LES].[PROC_TWD_GEN_TWICEPULL_DATA] @RUNSHEETID, 'TWD'
						END

					IF UPPER(@PULL_FLAG) = 'SPS'
						BEGIN
							SELECT
								@DOCK = [DOCK],
								@TRANS_SUPPLIER_NUM = [TRANS_SUPPLIER_NUM],
								@TRANSPORT_TIME = [TRANSPORT_TIME],
								@UNLOAD_TIME = [UNLOAD_TIME],
								@BOXSTATE = ISNULL([BOX_PARTS_STATE],1)
							FROM [LES].[TM_SPS_BOX_PARTS] WITH (NOLOCK)
    						WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @ASSEMBLY_LINE AND [SUPPLIER_NUM] = @SUPPLIER_NUM AND [BOX_PARTS] = @BOX_PARTS

							--获取建议发货时间
							SELECT @SUGGESTTIME = [LES].[FN_GET_TWD_SENDTIME](@PLANT, @ASSEMBLY_LINE, @EXPECTED_ARRIVAL_TIME, ISNULL(@TRANSPORT_TIME, 0) + ISNULL(@UNLOAD_TIME, 0))
							--获取拉动单号
							EXEC [LES].[PROC_SPS_GET_RUNSHEET_NO] @PLANT, @ASSEMBLY_LINE, @SUPPLIER_NUM, @RUNSHEETNO OUTPUT, 'SPS'

    						INSERT INTO [LES].[TT_SPS_RUNSHEET]
    						(
								[SPS_RUNSHEET_NO],
    							[PLANT],
    							[ASSEMBLY_LINE],
    							[WORKSHOP],
    							[PLANT_ZONE],
    							[PUBLISH_TIME],
    							[RUNSHEET_TYPE],
    							[SUPPLIER_NUM],
    							[SUPPLIER_SN],
    							[DOCK],
    							[DELIVERY_LOCATION],
    							[BOX_PARTS],
    							[PART_TYPE],
    							[EXPECTED_ARRIVAL_TIME],
    							[SUGGEST_DELIVERY_TIME],
    							[ACTUAL_ARRIVAL_TIME],
    							[VERIFY_TIME],
    							[REJECT_REASON],
    							[TRANS_SUPPLIER_NUM],
    							[FEEDBACK],
    							[SHEET_STATUS],
    							[SEND_TIME],
    							[SEND_STATUS],
    							[OPERATON_USER],
    							[CHECK_USER],
    							[RETRY_TIMES],
    							[SUPPLY_TIME],
    							[SUPPLY_STATUS],
    							[FAX_TIME],
    							[FAX_STATUS],
								[ORDER_NO],
								[VIN],
    							[PTL_SEND_TIME],
    							[PTL_SEND_STATUS],
    							[PRINT_TIMES],
								[PRINT_STATE],
    							[COMMENTS],
    							[UPDATE_DATE],
    							[UPDATE_USER],
    							[CREATE_DATE],
    							[CREATE_USER]
							)
    						VALUES
    						(
								@RUNSHEETNO,			--<SPS_RUNSHEET_NO, nvarchar(22),>
    							@PLANT,					--<PLANT, nvarchar(5),>
    							@ASSEMBLY_LINE,			--<ASSEMBLY_LINE, nvarchar(10),>
    							NULL,					--<WORKSHOP, nvarchar(4),>
    							@PLANT_ZONE,			--<PLANT_ZONE, nvarchar(10),>
    							@NOW,					--<PUBLISH_TIME, datetime,>
    							@RUNSHEETTYPE,			--<RUNSHEET_TYPE, int,>
    							@SUPPLIER_NUM,			--<SUPPLIER_NUM, nvarchar(12),>
    							0,						--<SUPPLIER_SN, int,>
    							@DOCK,					--<DOCK, nvarchar(10),>
    							@DELIVERY_LOCATION,		--<DELIVERY_LOCATION, nvarchar(50),>
    							@BOX_PARTS,				--<BOX_PARTS, nvarchar(10),>
    							0,						--<PART_TYPE, int,>
    							@EXPECTED_ARRIVAL_TIME,	--<EXPECTED_ARRIVAL_TIME, datetime,>
    							@SUGGESTTIME,			--<SUGGEST_DELIVERY_TIME, datetime,>
    							NULL,					--<ACTUAL_ARRIVAL_TIME, datetime,>
    							NULL,					--<VERIFY_TIME, datetime,>
    							NULL,					--<REJECT_REASON, nvarchar(200),>
    							@TRANS_SUPPLIER_NUM,	--<TRANS_SUPPLIER_NUM, nvarchar(20),>
    							NULL,					--<FEEDBACK, nvarchar(100),>
    							0,						--<SHEET_STATUS, int,>
    							@NOW,					--<SEND_TIME, datetime,>
    							@BOXSTATE,				--<SEND_STATUS, int,>
    							NULL,					--<OPERATON_USER, nvarchar(10),>
    							NULL,					--<CHECK_USER, nvarchar(10),>
    							0,						--<RETRY_TIMES, int,>
    							NULL,					--<SUPPLY_TIME, datetime,>
    							0,						--<SUPPLY_STATUS, int,>
    							NULL,					--<FAX_TIME, datetime,>
    							0,						--<FAX_STATUS, int,>
    							@ORDER_ID,				--<ORDER_NO, nvarchar(36),>
    							@VIN,					--<VIN, nvarchar(20),>
    							NULL,					--<PTL_SEND_TIME, datetime,>
    							0,						--<PTL_SEND_STATUS, int,>
								0,						--<PRINT_TIMES, int,>
								0,						--<PRINT_STATE, int,>
    							'',						--<COMMENTS, nvarchar(200),>
    							NULL,					--<UPDATE_DATE, datetime,>
    							NULL,					--<UPDATE_USER, nvarchar(50),>
    							@NOW,					--<CREATE_DATE, datetime,>
    							@LOGINNAME				--<CREATE_USER, nvarchar(50),>
							)

    						SELECT @RUNSHEETID = SCOPE_IDENTITY()

    						INSERT INTO [LES].[TT_SPS_RUNSHEET_DETAIL]
    						(
								[SPS_RUNSHEET_SN],
    							[PLANT],
    							[ASSEMBLY_LINE],
    							[SUPPLIER_NUM],
    							[PART_NO],
    							[IDENTIFY_PART_NO],
    							[PART_CNAME],
    							[PART_ENAME],
    							[DOCK],
    							[BOX_PARTS],
    							[SEQUENCE_NO],
    							[PICKUP_SEQ_NO],
    							[RDC_DLOC],
    							[INBOUND_PACKAGE],
    							[MEASURING_UNIT_NO],
    							[INBOUND_PACKAGE_MODEL],
    							[PACK_COUNT],
    							[REQUIRED_INBOUND_PACKAGE],
    							[REQUIRED_INBOUND_PACKAGE_QTY],
    							[ACTUAL_INBOUND_PACKAGE],
    							[ACTUAL_INBOUND_PACKAGE_QTY],
    							[MANUAL_LOCATION]
							)
    						SELECT
    							@RUNSHEETID,					--(<SPS_RUNSHEET_SN, int,>
    							@PLANT,							--<PLANT, nvarchar(5),>
    							@ASSEMBLY_LINE,					--<ASSEMBLY_LINE, nvarchar(10),>
    							@SUPPLIER_NUM,					--<SUPPLIER_NUM, nvarchar(12),>
    							B.[PART_NO],					--<PART_NO, nvarchar(20),>
    							B.[PART_NO],					--<IDENTIFY_PART_NO, nvarchar(20),>
    							B.[PART_CNAME],					--<PART_CNAME, nvarchar(300),>
    							B.[PART_ENAME],					--<PART_ENAME, nvarchar(300),>
    							ISNULL(@DOCK,''),				--<DOCK, nvarchar(10),>
    							@BOX_PARTS,						--<BOX_PARTS, nvarchar(10),>
    							0,								--<SEQUENCE_NO, int,>
    							0,								--<PICKUP_SEQ_NO, int,>
    							NULL,							--<RDC_DLOC, varchar(20),>
    							B.[INBOUND_PACKAGE],			--<INBOUND_PACKAGE, int,>
    							ISNULL(F.[PART_UNITS], 1),		--<MEASURING_UNIT_NO, nvarchar(50),>
    							B.[INBOUND_PACKAGE_MODEL],		--<INBOUND_PACKAGE_MODEL, nvarchar(30),>
    							CAST(D.[QUANTITY] AS INT),		--<PACK_COUNT, int,>
    							0,								--<REQUIRED_INBOUND_PACKAGE, int,>
    							CAST(D.[QUANTITY] AS INT),		--<REQUIRED_INBOUND_PACKAGE_QTY, int,>
    							0,								--<ACTUAL_INBOUND_PACKAGE, int,>
    							0,								--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
  								B.[LOCATION]					--<MANUAL_LOCATION, nvarchar(20),>
							FROM [LES].[TT_SPS_MANUAL_PULL] A WITH (NOLOCK)
    						INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND B.[SUPPLIER_NUM] = A.[SUPPLIER_NUM] AND B.[INHOUSE_PART_CLASS] = A.[BOX_PARTS] AND B.[INHOUSE_SYSTEM_MODE] = A.[PULL_FLAG] AND B.[DELETE_FLAG] = 0 AND LEN(ISNULL(B.[LOCATION], '')) > 0
							INNER JOIN [LES].[TT_BAS_PULL_ORDERS] C WITH (NOLOCK) ON C.[WERK] = A.[PLANT] AND C.[VIN] = A.[VIN]
							INNER JOIN [LES].[TT_BAS_ORDER_PART_RESULTS] D WITH (NOLOCK) ON D.[PLANT] = C.[WERK] AND D.[ORDER_NO] = C.[ORDER_NO] AND D.[PART_NO] = B.[PART_NO] AND D.[LOCATION] = B.[LOCATION]
							INNER JOIN [LES].[TM_SPS_BOX_PARTS] E WITH (NOLOCK) ON E.[PLANT] = A.[PLANT] AND E.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND E.[BOX_PARTS] = A.[BOX_PARTS] AND E.[SUPPLIER_NUM] = A.[SUPPLIER_NUM]
							LEFT JOIN [LES].[TM_BAS_MAINTAIN_PARTS] F WITH (NOLOCK) ON F.[PLANT] = B.[PLANT] AND F.[PART_NO] = B.[PART_NO]
							WHERE A.[SEQUENCE_NO] = @SEQUENCE_NO

							--创建仓库出库单
							EXEC [LES].[PROC_INSERT_OUTPUT] @RUNSHEETID, 'SPS'

							--投递二级拉动
							EXEC [LES].[PROC_TWD_GEN_TWICEPULL_DATA] @RUNSHEETID, 'SPS'
						END

					--更新拉动信息状态
					UPDATE [LES].[TT_SPS_MANUAL_PULL] WITH (ROWLOCK)
					SET [PROCESS_FLAG] = 1,
						[PROCESS_TIME] = @NOW,
						[EXPECTED_ARRIVAL_TIME] = @EXPECTED_ARRIVAL_TIME,
						[TWD_RUNSHEET_NO] = @RUNSHEETNO,
						[COMMENTS] = @REMARK
					WHERE [SEQUENCE_NO] = @SEQUENCE_NO
    			END			
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		SET @RESULT = 1
		SET @RESULTMESSAGE = ERROR_MESSAGE()

		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'TWD', '[LES].[PROC_TWD_GENERATE_MANUAL_KIT_RUNSHEET]', 'Procedure', @RESULTMESSAGE, ERROR_LINE()
	END CATCH
END