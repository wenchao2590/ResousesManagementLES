/****************************************************************/
/*   Project Name:  TWD											*/
/*   Program Name:  [LES].[PROC_TWD_GENERATE_MANUAL_RUNSHEET]	*/
/*   Called By:     window service								*/
/*   Author:        孙述霄										*/
/*   Create date:	2017-06-13									*/
/*   Note:			紧急拉动单									*/
/****************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_GENERATE_MANUAL_RUNSHEET]
(
	@Plant					NVARCHAR(5),					--工厂编号
	@AssemblyLine			NVARCHAR(10),					--流水线编号
	@supplier				NVARCHAR(12),					--供应商
	@inhouseSystemMode		NVARCHAR(10),					--拉动标记
	@groupID				NVARCHAR(20),					--组号
	@PARTBOX				NVARCHAR(10),					--TWD零件类
	@pulltype				NVARCHAR(2),					--拉动单类型
	@remark					NVARCHAR(200),					--备注
	@expectedarrivaltime	DATETIME,						--期望到达时间
	@CreateUser				NVARCHAR(50) = 'Manual ENGINE'	--操作用户
)
AS
BEGIN
    DECLARE @sendTime DATETIME
	DECLARE @dock NVARCHAR(40)
	DECLARE @now DATETIME
	DECLARE @suggestTime DATETIME
	DECLARE @isorganizesheet INT
    DECLARE @boxPartsName NVARCHAR(100)
    DECLARE @transSupplierNum NVARCHAR(20)
	DECLARE @dockName NVARCHAR(10)
	DECLARE @DELIVERY_LOCATION NVARCHAR(20)
	DECLARE @transportTime INT
	DECLARE @unloadtime INT
	DECLARE @delay_time INT
	DECLARE @BoxState INT
	DECLARE @sendstatus INT
	DECLARE @plant_zone NVARCHAR(10)
	DECLARE @runSheetType INT
	DECLARE @runsheetNo NVARCHAR(30)
	DECLARE @runsheetId INT
	DECLARE @flexSn INT
	DECLARE @boxSn INT
	DECLARE @interfaceId INT
	DECLARE @SupplierSn INT
	DECLARE @Format NVARCHAR(6)
	DECLARE @StartRunnNo NVARCHAR(8)
	DECLARE @EndRunnNo NVARCHAR(8)
	DECLARE @suppliertype INT
	DECLARE @isasn INT

	--TWD拉动单编码的序号问题,使用事务	并捕捉异常
	BEGIN TRY
		BEGIN TRANSACTION
			SET @now = GETDATE()
			SET @runSheetType = 2
			IF @expectedarrivaltime = ''
				BEGIN
					SELECT @expectedarrivaltime = @now
				END

			--目标存储区
			SELECT TOP 1 @plant_zone = [PLANT_ZONE] FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] WITH (NOLOCK) WHERE [INHOUSE_PART_CLASS] = @PARTBOX AND [SUPPLIER_NUM] = @supplier AND [INHOUSE_SYSTEM_MODE] = @inhouseSystemMode
			--目标仓库
			SELECT TOP 1 @DELIVERY_LOCATION = [WM_NO] FROM [LES].[TM_WMM_ZONES] WITH (NOLOCK) WHERE [ZONE_NO] = @plant_zone
			--DOCK
			SELECT @dock = MAX(ISNULL([DOCK], '')) FROM [LES].[TI_TWD_MATERIAL_CONSUME] WITH (NOLOCK) WHERE [SEQUENCE_NO] = @groupID
			--DOCK名称
			SELECT @dockName = [DOCK_NAME] FROM [LES].[TM_BAS_DOCK] WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine AND [DOCK] = @dock
			--供应商类型及是否ASN
			SELECT @suppliertype = [SUPPLIER_TYPE], @isasn=(CASE WHEN [ASN_FLAG] = 0 OR [ASN_FLAG] IS NULL THEN 0 ELSE 1 END) FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK) WHERE [SUPPLIER_NUM] = @supplier
			
			IF UPPER(@inhouseSystemMode) = 'TWD'
				BEGIN
					--获取拉动单号
					EXEC LES.PROC_TWD_GET_RUNSHEET_NO @Plant, @AssemblyLine, @supplier, @runsheetNo OUTPUT, 'Q'
					SELECT
						@transSupplierNum = [TRANS_SUPPLIER_NUM],
						@transportTime = [TRANSPORT_TIME],
						@unloadtime = [UNLOAD_TIME],
						@BoxState = ISNULL([BOX_PARTS_STATE],1),
						@isorganizesheet = ISNULL([IS_ORGANIZE_SHEET],0) 
					FROM [LES].[TM_TWD_BOX_PARTS] WITH (NOLOCK)
    				WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @assemblyLine AND [SUPPLIER_NUM] = @supplier AND [BOX_PARTS] = @PARTBOX
					SELECT @suggestTime = [LES].[FN_GET_TWD_SENDTIME](@plant, @assemblyLine, @expectedarrivaltime, ISNULL(@transportTime, 0) + ISNULL(@unloadtime, 0))

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
						[IS_ASN],
						[IS_TRAY]
					)
    				VALUES
    				(
						@runsheetNo,			--<TWD_RUNSHEET_NO, varchar(18),>
    					@plant,					--<PLANT, nvarchar(5),>
    					@assemblyLine,			--<ASSEMBLY_LINE, nvarchar(10),>
    					NULL,					--<WORKSHOP, nvarchar(4),>
    					@plant_zone,			--<PLANT_ZONE, nvarchar(5),>
    					@now,					--<PUBLISH_TIME, datetime,>
    					@runSheetType,			--<RUNSHEET_TYPE, int,>
    					@supplier,				--<SUPPLIER_NUM, nvarchar(12),>
    					0,						--尚不明确含义<SUPPLIER_SN, int,>
    					@dock,					--<DOCK, nvarchar(10),>
    					@DELIVERY_LOCATION,		--<DELIVERY_LOCATION, nvarchar(50),>
    					@PARTBOX,				--<BOX_PARTS, nvarchar(10),>
    					0,						--尚不明确含义<PART_TYPE, int,>
    					NULL,					--<UNLOADING_TIME, int,>
    					@expectedarrivaltime,	--<EXPECTED_ARRIVAL_TIME, datetime,>
    					@suggestTime,			--需求尚未确定<SUGGEST_DELIVERY_TIME, datetime,>
    					NULL,					--<ACTUAL_ARRIVAL_TIME, datetime,>
    					NULL,					--<VERIFY_TIME, datetime,>
    					NULL,					--<REJECT_REASON, nvarchar(200),>
    					@transSupplierNum,		--<TRANS_SUPPLIER_NUM, nvarchar(8),>
    					NULL,					--<FEEDBACK, nvarchar(100),>
    					0,						--<SHEET_STATUS, int,>
    					@now,					--<SEND_TIME, datetime,>
    					@BoxState,				--<SEND_STATUS, int,>
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
    					@BoxState,				--<WMS_SEND_STATUS, int,>
    					@remark,				--<COMMENTS, nvarchar(200),>
    					NULL,					--<UPDATE_DATE, datetime,>
    					NULL,					--<UPDATE_USER, nvarchar(50),>
    					@now,					--<CREATE_DATE, datetime,>
    					@CreateUser,			--<CREATE_USER, nvarchar(50),>
    					@inhouseSystemMode,
						@isasn,
						0
					)

    				SELECT @runsheetId = SCOPE_IDENTITY()
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
						[MANUAL_LOCATION],
						[PRINT_TIMES],
						[PRINT_STATE],
						[PRINT_SUPPLEMENT]
					)
					SELECT
    					@runsheetId,							--<TWD_RUNSHEET_SN, int,>
    					@plant,									--<PLANT, nvarchar(5),>
    					@assemblyLine,							--<ASSEMBLY_LINE, nvarchar(10),>
    					@supplier,								--<SUPPLIER_NUM, nvarchar(12),>
    					[PART_NO],								--<nvarchar(20),>
    					[INDENTIFY_PART_NO],					--<nvarchar(20),>
    					[PART_CNAME],							--<nvarchar(100),>
    					[PART_ENAME],							--<nvarchar(100),>
    					@dock,									--<DOCK, nvarchar(10),>
    					@PARTBOX,								--<BOX_PARTS, nvarchar(10),>
    					0,										--<SEQUENCE_NO, int,>
    					0,										--<PICKUP_SEQ_NO, int,>
    					NULL,									--<RDC_DLOC, varchar(20),>
    					[INBOUND_PACKAGE],						--<int,>
    					[MEASURING_UNIT_NO],					--<nvarchar(8),>
    					[INBOUND_PACKAGE_MODEL],				--<nvarchar(30),>
    					SUM([PACK_COUNT]),						--<int,>
    					SUM([PACK_COUNT]/[INBOUND_PACKAGE]),	--<REQUIRED_INBOUND_PACKAGE, int,>
    					SUM([PACK_COUNT]),						--<REQUIRED_INBOUND_PACKAGE_QTY, int,>
    					0,										--<ACTUAL_INBOUND_PACKAGE, int,>
    					0,										--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
    					NULL,									--<BARCODE_DATA, nvarchar(50),>
    					NULL,									--<COMMENTS, nvarchar(200),>
						[LOCATION],								--<Location, nvarchar(20),>
						0,										--<[PRINT_TIMES], INT>
						0,										--<[PRINT_STATE], INT>
						0										--<[PRINT_SUPPLEMENT], INT>
					FROM [LES].[TI_TWD_MATERIAL_CONSUME] WITH (NOLOCK)
    				WHERE [IS_ORGANIZE_SHEET] = 2
    				AND [SEQUENCE_NO] = @groupID
					AND [PLANT] = @plant
					AND [ASSEMBLY_LINE] = @AssemblyLine
					AND [SUPPLIER_NUM] = @supplier
					AND [BOX_PARTS] = @PARTBOX
    				GROUP BY [PART_NO], [INDENTIFY_PART_NO], [LOCATION], [PART_CNAME], [PART_ENAME], [INBOUND_PACKAGE], [MEASURING_UNIT_NO], [INBOUND_PACKAGE_MODEL]	--相同零件多个需求合并到一个明细中
					
					IF @suppliertype = 1 AND @isasn = 0
						BEGIN
							--生成箱条码
							EXEC [LES].[PROC_TWD_GENERATE_BARCODE] @runsheetId
						END

					--创建仓库出库单
					EXEC [LES].[PROC_INSERT_OUTPUT] @runsheetId, 'TWD'

					--投递二级拉动
					EXEC [LES].[PROC_TWD_GEN_TWICEPULL_DATA] @runsheetId, 'TWD'
				END

			IF UPPER(@inhouseSystemMode) = 'PCS'
				BEGIN
					--获取拉动单号
					EXEC @runsheetId = [LES].[PROC_PCS_GET_NEXT_SEQUENCE] 'pcs_pull_runsheet_id'
					EXEC [LES].[PROC_PCS_GET_RUNSHEET_NO] @Plant, @AssemblyLine, @supplier, @runsheetNo OUTPUT, 'Q'
					SELECT
						@delay_time = ISNULL([DELAY_TIME], 0),
						@transportTime = ISNULL([TRANSPORT_TIME], 0),
						@sendstatus = CASE WHEN [BOX_PARTS_STATE] = 1 THEN 1 ELSE 3 END
					FROM [LES].[TM_PCS_ROUTE_BOX_PARTS] WITH (NOLOCK)
					WHERE [PLANT] = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [BOX_PARTS] = @PARTBOX
					SELECT @suggestTime = [LES].[FN_GET_TWD_SENDTIME](@Plant, @AssemblyLine, @expectedarrivaltime, ISNULL(@delay_time, 0) + ISNULL(@transportTime, 0))

					INSERT INTO [LES].[TT_PCS_RUNSHEET]
					(
						[PCS_RUNSHEET_SN],
						[PLANT],
						[ASSEMBLY_LINE],
						[PCS_RUNSHEET_NO],
						[PUBLISH_TIME],
						[RUNSHEET_TYPE],
						[SUPPLIER_NUM],
						[BOX_PARTS],
						[UNLOADING_TIME],
						[EXPECTED_ARRIVAL_TIME],
						[SHEET_STATUS],
						[SEND_STATUS],
						[WMS_SEND_STATUS],
						[SAP_FLAG],
						[PLANT_ZONE],
						[VERIFY_TIME],
						[CREATE_DATE]
					)
					SELECT 
						@runsheetId,
						@Plant,
						@AssemblyLine,
						@runsheetNo,
						@now,
						@runSheetType,				--PCS拉动单类型
						@supplier,
						@PARTBOX,
						NULL,						--[UNLOADING_TIME]
						@expectedarrivaltime,		--[EXPECTED_ARRIVAL_TIME]
						10,							--[SHEET_STATUS]
						@sendstatus,				--[SEND_STATUS]
						@sendstatus,				--[WMS_SEND_STATUS]
						0,
						@plant_zone,
						@suggestTime,
						@now

					INSERT INTO [LES].[TT_PCS_RUNSHEET_DETAIL]
					(
						[PCS_RUNSHEET_SN],
						[PLANT],
						[ASSEMBLY_LINE],
						[LOCATION],
						[SUPPLIER_NUM],
						[PART_NO],
						[PART_CNAME],
						[PART_ENAME],
						[DOCK],
						[BOX_PARTS],
						[INHOUSE_PACKAGE],
						[MEASURING_UNIT_NO],
						[INHOUSE_PACKAGE_MODEL],
						[PACK_COUNT],
						[REQUIRED_INHOUSE_PACKAGE],
						[REQUIRED_INHOUSE_PACKAGE_QTY]
					)
					SELECT
    					@runsheetId,							--<PCS_RUNSHEET_SN, int,>
    					@plant,									--<PLANT, nvarchar(5),>
    					@assemblyLine,							--<ASSEMBLY_LINE, nvarchar(10),>
						[LOCATION],								--<LOCATION, nvarchar(20),>
    					@supplier,								--<SUPPLIER_NUM, nvarchar(12),>
    					[PART_NO],								--<PART_NO, nvarchar(20),>
    					[PART_CNAME],							--<PART_CNAME, nvarchar(300),>
    					[PART_ENAME],							--<PART_ENAME, nvarchar(300),>
    					@dock,									--<DOCK, nvarchar(10),>
    					@PARTBOX,								--<BOX_PARTS, nvarchar(10),>
    					[INBOUND_PACKAGE],						--<INHOUSE_PACKAGE, int,>
    					[MEASURING_UNIT_NO],					--<MEASURING_UNIT_NO, nvarchar(8),>
    					[INBOUND_PACKAGE_MODEL],				--<INHOUSE_PACKAGE_MODEL, nvarchar(30),>
    					SUM([PACK_COUNT]),						--<PACK_COUNT, int,>
    					SUM([PACK_COUNT]/[INBOUND_PACKAGE]),	--<REQUIRED_INHOUSE_PACKAGE, int,>
    					SUM([PACK_COUNT])						--<REQUIRED_INHOUSE_PACKAGE_QTY, int,>
					FROM [LES].[TI_TWD_MATERIAL_CONSUME] WITH (NOLOCK)
    				WHERE [IS_ORGANIZE_SHEET] = 2
    				AND [SEQUENCE_NO] = @groupID
					AND [PLANT] = @plant
					AND [ASSEMBLY_LINE] = @AssemblyLine
					AND [SUPPLIER_NUM] = @supplier
					AND [BOX_PARTS] = @PARTBOX
    				GROUP BY [PART_NO], [INDENTIFY_PART_NO], [LOCATION], [PART_CNAME], [PART_ENAME], [INBOUND_PACKAGE], [MEASURING_UNIT_NO], [INBOUND_PACKAGE_MODEL]	--相同零件多个需求合并到一个明细中
		
					--创建仓库出库单
					EXEC [LES].[PROC_INSERT_OUTPUT] @runsheetId, 'PCS'

					--投递二级拉动
					EXEC [LES].[PROC_TWD_GEN_TWICEPULL_DATA] @runsheetId, 'PCS'
				END

			IF UPPER(@inhouseSystemMode) = 'JIS'
				BEGIN
					--获取拉动单号
					EXEC @runsheetId = [LES].[PROC_JIS_GET_NEXT_SEQUENCE] 'JIS_RUNSHEET_SN'
					SET @runsheetNo = LEFT(@AssemblyLine + '00', 2) + REPLACE(CONVERT(NVARCHAR(10), @now, 120), '-', '') + 'Q' + RIGHT('0000' + CAST(@runsheetId AS NVARCHAR), 4)
					SELECT
						@dock = [DOCK],
						@transSupplierNum = [TRANSPORT_SUPPLIER],
						@delay_time = ISNULL([UNLOADING_TIME], 0)
					FROM [LES].[TM_JIS_RACK] WITH (NOLOCK)
					WHERE [PLANT] = @Plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [RACK] = @PARTBOX
					SELECT @suggestTime = [LES].[FN_GET_TWD_SENDTIME](@Plant, @AssemblyLine, @expectedarrivaltime, ISNULL(@delay_time, 0))

					SET @boxSn = 1
					SET @interfaceId = NULL
					SELECT
						@interfaceId = MIN([INTERFACE_ID])
					FROM [LES].[TI_TWD_MATERIAL_CONSUME] WITH (NOLOCK)
    				WHERE [IS_ORGANIZE_SHEET] = 2 AND [SEQUENCE_NO] = @groupID AND [PLANT] = @plant
					AND [ASSEMBLY_LINE] = @AssemblyLine AND [SUPPLIER_NUM] = @supplier AND [BOX_PARTS] = @PARTBOX
					WHILE @interfaceId IS NOT NULL
						BEGIN
							EXEC @flexSn = [LES].[PROC_JIS_GET_NEXT_SEQUENCE] 'JIS_RUNSHEET_FLEX_SN'
							INSERT INTO [LES].[TT_JIS_RUNSHEET_FLEX]
							(
								[JIS_RUNSHEET_FLEX_SN],
								[JIS_RUNSHEET_FLEX_TIME],
								[PLANT],
								[ASSEMBLY_LINE],
								[SUPPLIER_NUM],
								[RACK],
								[BOX_NUMBER],
								[FORMAT],
								[CAR_NO],
								[RUNNING_NUMBER],
								[JIS_RUNSHEET_SN],
								[JIS_RUNSHEET_NO],
								[CREATE_DATE],
								[SAP_FLAG],
								[VIN],
								[MODEL_NO],
								[JIS_BOX_SN]
							)
							SELECT
								@flexSn,													--<[JIS_RUNSHEET_FLEX_SN], INT>
								@now,														--<[JIS_RUNSHEET_FLEX_TIME], DATETIME>
								@plant,														--<[PLANT], NVARCHAR(5)>
								@AssemblyLine,												--<[ASSEMBLY_LINE], NVARCHAR(10)>
								@supplier,													--<[SUPPLIER_NUM], NVARCHAR(8)>
								@PARTBOX,													--<[RACK], NVARCHAR(20)>
								A.[REQURIED_PACK],											--<[BOX_NUMBER], INT>
								CASE WHEN B.[VORSERIE] = 1 THEN 'True' ELSE 'False' END,	--<[FORMAT], NVARCHAR(6)>
								RIGHT('00000000' + ISNULL(B.[ORDER_NO], ''), 8),			--<[CAR_NO], NVARCHAR(8)>
								RIGHT('0000' + ISNULL([RUNNING_NUMBER], ''), 4),			--<[RUNNING_NUMBER], NVARCHAR(5)>
								@runsheetId,												--<[JIS_RUNSHEET_SN], INT>
								@runsheetNo,												--<[JIS_RUNSHEET_NO], NVARCHAR(30)>
								@now,														--<[CREATE_DATE], DATETIME>
								1,															--<[SAP_FLAG], INT>
								A.[VIN],													--<[VIN], NVARCHAR(20)>
								RIGHT('00000000' + ISNULL(B.[FARBAU], ''), 8),				--<[MODEL_NO], NVARCHAR(8)>
								@boxSn														--<[JIS_BOX_SN], INT>
							FROM [LES].[TI_TWD_MATERIAL_CONSUME] A WITH (NOLOCK)
							INNER JOIN [LES].[TT_BAS_PULL_ORDERS] B WITH (NOLOCK) ON B.[WERK] = @plant AND B.[VIN] = A.[VIN]
							WHERE A.[INTERFACE_ID] = @interfaceId

							INSERT INTO [LES].[TT_JIS_RUNSHEET_DETAIL]
							(
								[JIS_RUNSHEET_FLEX_SN],
								[JIS_RUNSHEET_BOX_SN],
								[JIS_RUNSHEET_PART_SN],
								[PART_NO],
								[PLANT],
								[ASSEMBLY_LINE],
								[PART_CNAME],
								[USAGE],
								[PART_NICK_NAME]
							)
							SELECT
								@flexSn,				--<[JIS_RUNSHEET_FLEX_SN], INT>
								@flexSn,				--<[JIS_RUNSHEET_BOX_SN], INT>
								@boxSn,					--<[JIS_RUNSHEET_PART_SN], INT>
								A.[PART_NO],			--<[PART_NO], NVARCHAR(20)>
								@plant,					--<[PLANT], NVARCHAR(5)>
								@AssemblyLine,			--<[ASSEMBLY_LINE], NVARCHAR(10)>
								A.[PART_CNAME],			--<[PART_CNAME], NVARCHAR(300)>
								A.[PACK_COUNT],			--<[USAGE], NUMERIC(18,2)>
								(
									SELECT TOP 1
										ISNULL([PART_NICKNAME], '')
									FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] B WITH (NOLOCK)
									WHERE B.[PLANT] = @plant AND B.[ASSEMBLY_LINE] = @AssemblyLine AND B.[INHOUSE_PART_CLASS] = @PARTBOX AND B.[SUPPLIER_NUM] = @supplier AND B.[PART_NO] = A.[PART_NO] AND B.[INHOUSE_SYSTEM_MODE] = 'JIS'
								) AS [PART_NICK_NAME]	--<[PART_NICK_NAME], NVARCHAR(30)>
							FROM [LES].[TI_TWD_MATERIAL_CONSUME] A WITH (NOLOCK)
							WHERE A.[INTERFACE_ID] = @interfaceId

							SET @boxSn = @boxSn + 1
							SELECT
								@interfaceId = MIN([INTERFACE_ID])
							FROM [LES].[TI_TWD_MATERIAL_CONSUME] WITH (NOLOCK)
    						WHERE [IS_ORGANIZE_SHEET] = 2 AND [SEQUENCE_NO] = @groupID AND [PLANT] = @plant
							AND [ASSEMBLY_LINE] = @AssemblyLine AND [SUPPLIER_NUM] = @supplier
							AND [BOX_PARTS] = @PARTBOX AND [INTERFACE_ID] > @interfaceId
						END

					--获取供应商流水号
					SET @SupplierSn = NULL
					SELECT @SupplierSn = [JIS_SUPPLIER_SN] + 1 FROM [LES].[TM_JIS_SUPPLIER_SN] WITH (NOLOCK) WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [RACK] = @PARTBOX AND [SUPPLIER_NUM] = @supplier
					IF @SupplierSn IS NOT NULL
						BEGIN
							IF @SupplierSn > 9999
								SET @SupplierSn = 1
							UPDATE [LES].[TM_JIS_SUPPLIER_SN] SET [JIS_SUPPLIER_SN] = @SupplierSn WHERE [PLANT] = @plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [RACK] = @PARTBOX AND [SUPPLIER_NUM] = @supplier
						END
					ELSE
						BEGIN
							SET @SupplierSn = 1
							INSERT INTO [LES].[TM_JIS_SUPPLIER_SN]
							(
								[PLANT],
								[ASSEMBLY_LINE],
								[PLANT_ZONE],
								[WORKSHOP],
								[SUPPLIER_NUM],
								[RACK],
								[JIS_SUPPLIER_SN],
								[COMMENTS],
								[CREATE_USER],
								[CREATE_DATE],
								[UPDATE_USER],
								[UPDATE_DATE]
							)
							VALUES
							(
								@plant,
								@AssemblyLine,
								'',
								'',
								@supplier,
								@PARTBOX,
								@SupplierSn,
								'',
								@CreateUser,
								@now,
								'',
								@now
							)
						END
					SELECT TOP 1 @Format = [FORMAT] FROM [LES].[TT_JIS_RUNSHEET_FLEX] WITH (NOLOCK) WHERE [JIS_RUNSHEET_SN] = @runsheetId
					SELECT @StartRunnNo = MIN([RUNNING_NUMBER]), @EndRunnNo = MAX([RUNNING_NUMBER]) FROM [LES].[TT_JIS_RUNSHEET_FLEX] WITH (NOLOCK) WHERE [JIS_RUNSHEET_SN] = @runsheetId
					INSERT INTO [LES].[TT_JIS_RUNSHEET]
					(
						[JIS_RUNSHEET_SN],
						[JIS_RUNSHEET_NO],
						[JIS_RUNSHEET_TIME],
						[PLANT],
						[ASSEMBLY_LINE],
						[RACK],
						[SUPPLIER_NUM],
						[PLANT_ZONE],
						[LOCATION],
						[JIS_SUPPLIER_SN],
						[DOCK],
						[FIRST_TIME],
						[EXPECTED_ARRIVAL_TIME],
						[ESTIMATED_ARRIVAL_TIME],
						[PRINT_TYPE],
						[FORMAT],
						[CARS],
						[START_RUNNING_NO],
						[END_RUNNING_NO],
						[REDO_FLAG],
						[JIS_RUNSHEET_STATUS],
						[SEND_STATUS],
						[SUPPLY_STATUS],
						[SAP_FLAG],
						[TRANS_SUPPLIER_NUM],
						[WMS_SEND_STATUS],
						[COMMENTS],
						[CREATE_DATE],
						[CREATE_USER],
						[PRINT_TIMES],
						[PRINT_STATE],
						[RUNSHEET_TYPE]
					)
					VALUES
					(
						@runsheetId,			--<[JIS_RUNSHEET_SN], INT>
						@runsheetNo,			--<[JIS_RUNSHEET_NO], VARCHAR(30)>
						@now,					--<[JIS_RUNSHEET_TIME], DATETIME>
						@plant,					--<[PLANT], NVARCHAR(5)>
						@AssemblyLine,			--<[ASSEMBLY_LINE], NVARCHAR(10)>
						@PARTBOX,				--<[RACK], NVARCHAR(20)>
						@supplier,				--<[SUPPLIER_NUM], NVARCHAR(8)>
						@plant_zone,			--<[PLANT_ZONE], NVARCHAR(10)>
						'',						--<[LOCATION], NVARCHAR(20)>
						@SupplierSn,			--<[JIS_SUPPLIER_SN], INT>
						@dock,					--<[DOCK], NVARCHAR(10)>
						@now,					--<[FIRST_TIME], DATETIME>
						@expectedarrivaltime,	--<[EXPECTED_ARRIVAL_TIME], DATETIME>
						@suggestTime,			--<[ESTIMATED_ARRIVAL_TIME], DATETIME>
						'',						--<[PRINT_TYPE], VARCHAR(2)>
						@Format,				--<[FORMAT], VARCHAR(6)>
						'',						--<[CARS], VARCHAR(200)>
						@StartRunnNo,			--<[START_RUNNING_NO], VARCHAR(8)>
						@EndRunnNo,				--<[END_RUNNING_NO], VARCHAR(8)>
						0,						--<[REDO_FLAG], BIT>
						0,						--<[JIS_RUNSHEET_STATUS], INT>
						1,						--<[SEND_STATUS], INT>
						0,						--<[SUPPLY_STATUS], INT>
						4,						--<[SAP_FLAG], INT>
						@transSupplierNum,		--<[TRANS_SUPPLIER_NUM], NVARCHAR(20)>
						0,						--<[WMS_SEND_STATUS], INT>
						@remark,				--<[COMMENTS], NVARCHAR(200)>
						@now,				--<[CREATE_DATE], DATETIME>
						@CreateUser,			--<[CREATE_USER], NVARCHAR(50)>
						0,						--<[PRINT_TIMES], INT>
						0,						--<[PRINT_STATE], INT>
						@runSheetType			--<[RUNSHEET_TYPE], INT>
					)

					--创建仓库出库单
					EXEC [LES].[PROC_INSERT_OUTPUT] @runsheetId, 'JIS'

					--投递二级拉动
					EXEC [LES].[PROC_TWD_GEN_TWICEPULL_DATA] @runsheetId, 'JIS'
				END

    		--设置需求表中状态
    		UPDATE [LES].[TI_TWD_MATERIAL_CONSUME] 
    		SET [IS_ORGANIZE_SHEET] = 1
    		WHERE [IS_ORGANIZE_SHEET] = 2 AND [SEQUENCE_NO] = @groupID	
			AND [PLANT] = @plant AND [ASSEMBLY_LINE] = @AssemblyLine AND [SUPPLIER_NUM] = @supplier AND [BOX_PARTS] = @PARTBOX			
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'PCS', '[LES].[PROC_TWD_GENERATE_MANUAL_RUNSHEET]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END