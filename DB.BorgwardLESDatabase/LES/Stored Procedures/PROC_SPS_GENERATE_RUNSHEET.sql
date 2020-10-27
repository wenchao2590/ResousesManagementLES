/********************************************************************/
/*   Project Name:  SPS												*/
/*   Program Name:  [LES].[PROC_SPS_GENERATE_RUNSHEET]	 			*/
/*   Called By:     window service									*/
/*   Author:        孙述霄											*/
/*   Create date:	2017-10-13										*/
/*   Note:			SPS 生成拉动单									*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_SPS_GENERATE_RUNSHEET]
(
	@PLANT NVARCHAR(5),						--工厂
	@ASSEMBLY_LINE NVARCHAR(10),			--流水线
	@ORDER_ID NVARCHAR(50),					--订单号
	@DCP_POINT NVARCHAR(15),				--MES扫描点
	@SUPPLIER_NUM NVARCHAR(12),				--供应商
	@BOX_PARTS NVARCHAR(10)					--零件类
)
AS
BEGIN
	DECLARE @VIN NVARCHAR(30)				--VIN号
	DECLARE @DOCKNAME NVARCHAR(10)			--DOCK名称
	DECLARE @BOX_PARTS_STATE INT			--零件类状态
	DECLARE @PLANT_ZONE NVARCHAR(10)		--目标存储区
	DECLARE @SUGGESTTIME DATETIME			--建议发货时间
	DECLARE @EXPECTTIME DATETIME			--期望到达时间
	DECLARE @WMSSTATE INT					--WMS发送状态
	DECLARE @NOW DATETIME					--当前时间
    DECLARE @DOCK NVARCHAR(10)				--DOCK
    DECLARE @TRANS_SUPPLIER_NUM NVARCHAR(20)--运输供应商
    DECLARE @WAREHOUSE NVARCHAR(20)			--目标仓库
    DECLARE @TRANSPORT_TIME INT				--运输时间
	DECLARE @UNLOAD_TIME INT				--卸货时间
	DECLARE @LOAD_TIME INT					--装货时间
	DECLARE @DELAY_TIME INT					--延迟时间
	DECLARE @RUNSHEETNO NVARCHAR(30)		--拉动单号
	DECLARE @BLANBILL NVARCHAR(10)			--是否生成空白拉动单
	DECLARE @RUNSHEETID INT					--拉动单ID号
	DECLARE @RUNSHEETTYPE INT				--拉动单类型
	DECLARE @ALIAS NVARCHAR(10)				--别名
	DECLARE @CONSUMECOUNT INT				--明细条数
	SET @NOW = GETDATE()

	BEGIN TRY
		BEGIN TRANSACTION
    		--获取VIN号
			SELECT @VIN = [VIN] FROM [LES].[TT_BAS_PULL_ORDERS] WITH (NOLOCK) WHERE [WERK] = @PLANT AND [ORDER_NO] = @ORDER_ID

			--找到供应商对应的零件类
    		SELECT
				@DOCK = [DOCK],										--DOCK
				@TRANS_SUPPLIER_NUM = [TRANS_SUPPLIER_NUM],			--运输供应商
				@WAREHOUSE = [WM_NO],								--目标仓库
				@TRANSPORT_TIME = [TRANSPORT_TIME],					--运输时间
				@UNLOAD_TIME = [UNLOAD_TIME],						--卸货时间
				@LOAD_TIME = [LOAD_TIME],							--装货时间
				@DELAY_TIME = [DELAY_TIME],							--延迟时间
				@BOX_PARTS_STATE = [BOX_PARTS_STATE]				--零件类状态
			FROM [LES].[TM_SPS_BOX_PARTS] WITH (NOLOCK)
    		WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @ASSEMBLY_LINE AND [SUPPLIER_NUM] = @SUPPLIER_NUM AND [BOX_PARTS] = @BOX_PARTS
			AND [BOX_PARTS_STATE] <> 3  --1-活动，2-测试，3-停用

			--获取DOCK名称
			SELECT @DOCKNAME = [DOCK_NAME] FROM [LES].[TM_BAS_DOCK] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @ASSEMBLY_LINE AND [DOCK] = @DOCK

			SET @CONSUMECOUNT = 0
    		SELECT
				@CONSUMECOUNT = COUNT(1)
			FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] A WITH (NOLOCK)
			INNER JOIN [LES].[TT_BAS_ORDER_PART_RESULTS] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[PART_NO] = B.[PART_NO] AND A.[LOCATION] = B.[LOCATION]
			LEFT JOIN [LES].[TM_BAS_MAINTAIN_PARTS] C WITH (NOLOCK) ON A.[PLANT] = C.[PLANT] AND A.[PART_NO] = C.[PART_NO]
			WHERE A.[PLANT] = @PLANT AND A.[ASSEMBLY_LINE] = @ASSEMBLY_LINE AND A.[SUPPLIER_NUM] = @SUPPLIER_NUM AND A.[INHOUSE_PART_CLASS] = @BOX_PARTS
			AND A.[INHOUSE_SYSTEM_MODE] = 'SPS' AND A.[DELETE_FLAG] = 0 AND LEN(ISNULL(A.[LOCATION], '')) > 0
			AND B.[ORDER_NO] = @ORDER_ID AND C.[DELETE_FLAG] = 0

			--plant_zone借用存放目的存储区。
			SELECT TOP 1 @PLANT_ZONE=[PLANT_ZONE] FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] WITH (NOLOCK) WHERE [INHOUSE_PART_CLASS] = @BOX_PARTS AND [SUPPLIER_NUM] = @SUPPLIER_NUM AND [INHOUSE_SYSTEM_MODE] = 'SPS'
			--获取是否生成空白拉动单
			SELECT @BLANBILL = [PARAMETER_VALUE] FROM [LES].[TS_SYS_CONFIG] WITH (NOLOCK) WHERE PARAMETER_NAME = 'IsGenerateSPSBlankBill'

			SET @WMSSTATE = @BOX_PARTS_STATE
			IF(@CONSUMECOUNT > 0)
				BEGIN
					SET @RUNSHEETTYPE = 1		--正常拉动

					--获取拉动单号
					SET @ALIAS = 'SPS'
					EXEC [LES].[PROC_SPS_GET_RUNSHEET_NO] @PLANT, @ASSEMBLY_LINE, @SUPPLIER_NUM, @RUNSHEETNO OUTPUT, @ALIAS
					SELECT @SUGGESTTIME = [LES].[Func_Get_BAS_Work_Datetime](@PLANT, @ASSEMBLY_LINE, @NOW, ISNULL(@TRANSPORT_TIME, 0) + ISNULL(@UNLOAD_TIME, 0))
					SELECT @EXPECTTIME = [LES].[Func_Get_BAS_Work_Datetime](@PLANT, @ASSEMBLY_LINE, @SUGGESTTIME, ISNULL(@LOAD_TIME, 0) + ISNULL(@DELAY_TIME, 0))

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
    					@WAREHOUSE,				--<DELIVERY_LOCATION, nvarchar(50),>
    					@BOX_PARTS,				--<BOX_PARTS, nvarchar(10),>
    					0,						--<PART_TYPE, int,>
    					@EXPECTTIME,			--<EXPECTED_ARRIVAL_TIME, datetime,>
    					@SUGGESTTIME,			--<SUGGEST_DELIVERY_TIME, datetime,>
    					NULL,					--<ACTUAL_ARRIVAL_TIME, datetime,>
    					NULL,					--<VERIFY_TIME, datetime,>
    					NULL,					--<REJECT_REASON, nvarchar(200),>
    					@TRANS_SUPPLIER_NUM,	--<TRANS_SUPPLIER_NUM, nvarchar(20),>
    					NULL,					--<FEEDBACK, nvarchar(100),>
    					0,						--<SHEET_STATUS, int,>
    					@NOW,					--<SEND_TIME, datetime,>
    					@BOX_PARTS_STATE,		--<SEND_STATUS, int,>
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
    					'SPS RUNSHEET ENGINE'	--<CREATE_USER, nvarchar(50),>
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
    					A.[PART_NO],					--<PART_NO, nvarchar(20),>
    					A.[PART_NO],					--<IDENTIFY_PART_NO, nvarchar(20),>
    					A.[PART_CNAME],					--<PART_CNAME, nvarchar(300),>
    					A.[PART_ENAME],					--<PART_ENAME, nvarchar(300),>
    					ISNULL(@DOCK,''),				--<DOCK, nvarchar(10),>
    					@BOX_PARTS,						--<BOX_PARTS, nvarchar(10),>
    					0,								--<SEQUENCE_NO, int,>
    					0,								--<PICKUP_SEQ_NO, int,>
    					NULL,							--<RDC_DLOC, varchar(20),>
    					A.[INBOUND_PACKAGE],			--<INBOUND_PACKAGE, int,>
    					ISNULL(C.[PART_UNITS], 1),		--<MEASURING_UNIT_NO, nvarchar(50),>
    					A.[INBOUND_PACKAGE_MODEL],		--<INBOUND_PACKAGE_MODEL, nvarchar(30),>
    					CAST(B.[QUANTITY] AS INT),		--<PACK_COUNT, int,>
    					0,								--<REQUIRED_INBOUND_PACKAGE, int,>
    					CAST(B.[QUANTITY] AS INT),		--<REQUIRED_INBOUND_PACKAGE_QTY, int,>
    					0,								--<ACTUAL_INBOUND_PACKAGE, int,>
    					0,								--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
  						A.[LOCATION]					--<MANUAL_LOCATION, nvarchar(20),>
    				FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] A WITH (NOLOCK)
					INNER JOIN [LES].[TT_BAS_ORDER_PART_RESULTS] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[PART_NO] = B.[PART_NO] AND A.[LOCATION] = B.[LOCATION]
					LEFT JOIN [LES].[TM_BAS_MAINTAIN_PARTS] C WITH (NOLOCK) ON A.[PLANT] = C.[PLANT] AND A.[PART_NO] = C.[PART_NO]
					WHERE A.[PLANT] = @PLANT AND A.[ASSEMBLY_LINE] = @ASSEMBLY_LINE AND A.[SUPPLIER_NUM] = @SUPPLIER_NUM AND A.[INHOUSE_PART_CLASS] = @BOX_PARTS
					AND A.[INHOUSE_SYSTEM_MODE] = 'SPS' AND A.[DELETE_FLAG] = 0 AND LEN(ISNULL(A.[LOCATION], '')) > 0
					AND B.[ORDER_NO] = @ORDER_ID AND C.[DELETE_FLAG] = 0

					--创建仓库出库单
					EXEC [LES].[PROC_INSERT_OUTPUT] @RUNSHEETID, 'SPS'

					--投递二级拉动
					EXEC [LES].[PROC_TWD_GEN_TWICEPULL_DATA] @RUNSHEETID, 'SPS'
				END
			ELSE
				BEGIN
					IF @BLANBILL <> '0'
						BEGIN
							--生成空白拉动单
							SET @WMSSTATE = 4			--发送失败
							SET @RUNSHEETTYPE = 3		--空白拉动

							--获取拉动单号
							EXEC [LES].[PROC_SPS_GET_RUNSHEET_NO] @PLANT, @ASSEMBLY_LINE ,@SUPPLIER_NUM, @RUNSHEETNO OUTPUT,'T'
							SELECT @SUGGESTTIME = [LES].[Func_Get_BAS_Work_Datetime](@PLANT, @ASSEMBLY_LINE, @NOW, ISNULL(@TRANSPORT_TIME, 0) + ISNULL(@UNLOAD_TIME, 0))
							SELECT @EXPECTTIME = [LES].[Func_Get_BAS_Work_Datetime](@PLANT, @ASSEMBLY_LINE, @SUGGESTTIME, ISNULL(@LOAD_TIME, 0) + ISNULL(@DELAY_TIME, 0))

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
    							@WAREHOUSE,				--<DELIVERY_LOCATION, nvarchar(50),>
    							@BOX_PARTS,				--<BOX_PARTS, nvarchar(10),>
    							0,						--<PART_TYPE, int,>
    							@EXPECTTIME,			--<EXPECTED_ARRIVAL_TIME, datetime,>
    							@SUGGESTTIME,			--<SUGGEST_DELIVERY_TIME, datetime,>
    							NULL,					--<ACTUAL_ARRIVAL_TIME, datetime,>
    							NULL,					--<VERIFY_TIME, datetime,>
    							NULL,					--<REJECT_REASON, nvarchar(200),>
    							@TRANS_SUPPLIER_NUM,	--<TRANS_SUPPLIER_NUM, nvarchar(20),>
    							NULL,					--<FEEDBACK, nvarchar(100),>
    							0,						--<SHEET_STATUS, int,>
    							@NOW,					--<SEND_TIME, datetime,>
    							@BOX_PARTS_STATE,		--<SEND_STATUS, int,>
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
    							'SPS RUNSHEET ENGINE'	--<CREATE_USER, nvarchar(50),>
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
    						VALUES
							(
    							@RUNSHEETID,				--<SPS_RUNSHEET_SN, int,>
    							@PLANT,						--<PLANT, nvarchar(5),>
    							@ASSEMBLY_LINE,				--<ASSEMBLY_LINE, nvarchar(10),>
    							@SUPPLIER_NUM,				--<SUPPLIER_NUM, nvarchar(12),>
    							'******',					--<PART_NO, nvarchar(20),>
    							'******',					--<IDENTIFY_PART_NO, nvarchar(20),>
    							'******',					--<PART_CNAME, nvarchar(300),>
    							'******',					--<PART_ENAME, nvarchar(300),>
    							ISNULL(@DOCK, ''),			--<DOCK, nvarchar(10),>
    							@BOX_PARTS,					--<BOX_PARTS, nvarchar(10),>
    							0,							--<SEQUENCE_NO, int,>
    							0,							--<PICKUP_SEQ_NO, int,>
    							NULL,						--<RDC_DLOC, varchar(20),>
    							0,							--<INBOUND_PACKAGE, int,>
    							'1',						--<MEASURING_UNIT_NO, nvarchar(50),>
    							'',							--<INBOUND_PACKAGE_MODEL, nvarchar(30),>
    							0,							--<PACK_COUNT, int,>
    							0,							--<REQUIRED_INBOUND_PACKAGE, int,>
    							0,							--<REQUIRED_INBOUND_PACKAGE_QTY, int,>
    							0,							--<ACTUAL_INBOUND_PACKAGE, int,>
    							0,							--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
    							NULL						--<MANUAL_LOCATION, nvarchar(20),>
    						)
						END
				END
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT @NOW, 'SPS', '[LES].[PROC_SPS_GENERATE_RUNSHEET]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END