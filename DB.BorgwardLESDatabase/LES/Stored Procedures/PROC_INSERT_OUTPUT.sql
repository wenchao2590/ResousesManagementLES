/****************************************************************************************/
/*   Program Name:  [LES].[PROC_INSERT_OUTPUT]											*/
/*   Called By:     windows service														*/
/*   Description:	<添加出入库信息与出入库明细信息-针对TWD,PCS,JIS,EPS>				*/
/*   Modify:		Author: 孙述霄 Note: 按照拉动供应商类型判断出入库  DATE 2017-06-07	*/
/****************************************************************************************/
CREATE PROCEDURE [LES].[PROC_INSERT_OUTPUT]
(
	@RunsheetSn NVARCHAR(100),
	@Type NVARCHAR(10)
)
AS
BEGIN
	DECLARE	@OutPutId BIGINT
	DECLARE @identity BIGINT
	DECLARE @PLANT_ZONE NVARCHAR(10)
	DECLARE @RUNSHEET_TYPE INT
	DECLARE @SUPPLIER_NUM NVARCHAR(12)
	DECLARE @SUPPLIER_TYPE INT
	DECLARE @PLANT NVARCHAR(20)
	DECLARE @Assmbley_Line NVARCHAR(20)
	DECLARE @Box_Part NVARCHAR(50)
	DECLARE @IS_CREATE_TASK INT

	SET @IS_CREATE_TASK = 0
	IF @Type = 'TWD'
		BEGIN
			------------------------------------------TWD--START-----------------------------------------------------------------
			DECLARE @TWD_RUNSHEET_NO NVARCHAR(100)	--拉动单号
			DECLARE @PULL_TYPE NVARCHAR(50)			--拉动类型
			DECLARE @ROUTE NVARCHAR(50)				--路径
			DECLARE @RUNSHEET_CODE NVARCHAR(12)		--入库单类型
			DECLARE @IS_ASN INT						--是否ASN
			DECLARE @IS_TRAY INT					--是否按套组托

			SELECT @TWD_RUNSHEET_NO = [TWD_RUNSHEET_NO], @SUPPLIER_NUM = [SUPPLIER_NUM], @PLANT_ZONE = [PLANT_ZONE], @RUNSHEET_TYPE = [RUNSHEET_TYPE], @Box_Part = [BOX_PARTS], @PLANT = [PLANT], @Assmbley_Line = [ASSEMBLY_LINE], @IS_ASN = [IS_ASN], @IS_TRAY = [IS_TRAY] FROM [LES].[TT_TWD_RUNSHEET] WITH (NOLOCK) WHERE [TWD_RUNSHEET_SN] = @RunsheetSn
			SELECT TOP 1 @PULL_TYPE = [INHOUSE_SYSTEM_MODE] FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] WITH (NOLOCK) WHERE [INHOUSE_PART_CLASS] = @Box_Part AND [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @Assmbley_Line AND [SUPPLIER_NUM] = @SUPPLIER_NUM
			SELECT @SUPPLIER_TYPE = [SUPPLIER_TYPE] FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK) WHERE SUPPLIER_NUM = @SUPPLIER_NUM

			IF @PULL_TYPE = 'TWD'
				BEGIN
					SELECT @ROUTE = [ROUTE] FROM [LES].[TM_TWD_BOX_PARTS] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @Assmbley_Line AND [BOX_PARTS] = @Box_Part AND [SUPPLIER_NUM] = @SUPPLIER_NUM
				END
			ELSE IF @PULL_TYPE = 'PCS'
				BEGIN
					SELECT @ROUTE = [ROUTE] FROM [LES].[TM_PCS_ROUTE_BOX_PARTS] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @Assmbley_Line AND [BOX_PARTS] = @Box_Part
				END
			ELSE IF @PULL_TYPE = 'JIS'
				BEGIN
					SELECT @ROUTE = [ROUTE] FROM [LES].[TM_JIS_RACK] WITH (NOLOCK) WHERE [PLANT] = @PLANT AND [ASSEMBLY_LINE] = @Assmbley_Line AND [RACK] = @Box_Part
				END

			--只针对正常物料单或紧急物料单才创建出入库单据
			IF @RUNSHEET_TYPE = 1 OR @RUNSHEET_TYPE = 2
				BEGIN
					IF @SUPPLIER_TYPE = 2
						BEGIN
							--对于内部拉动，创建出库单
							INSERT INTO [LES].[TT_WMM_OUTPUT]
							( 
								[OUTPUT_NO],
								[PLANT],
								[SUPPLIER_NUM],
								[WM_NO],
								[ZONE_NO],
								[SEND_TIME],
								[OUTPUT_TYPE],
								[TRAN_TIME],
								[OUTPUT_REASON],
								[BOOK_KEEPER],
								[CONFIRM_FLAG],
								[PLAN_NO],
								[ASN_NO],
								[RUNSHEET_NO],
								[ASSEMBLY_LINE],
								[PLANT_ZONE],
								[WORKSHOP],
								[TRANS_SUPPLIER_NUM],
								[PART_TYPE],
								[SUPPLIER_TYPE],
								[RUNSHEET_CODE],
								[ERP_FLAG],
								[LOGICAL_PK],
								[BUSINESS_PK],
								[COMMENTS],
								[CREATE_DATE],
								[CREATE_USER],
								[ROUTE],
								[REQUEST_TIME]
							)
							SELECT 
								A.[TWD_RUNSHEET_NO] AS [OUTPUT_NO],
								A.[PLANT],
								A.[SUPPLIER_NUM],
								B.[WM_NO],
								A.[SUPPLIER_NUM] AS [ZONE_NO],
								GETDATE() AS [SEND_TIME],
								1 AS [OUTPUT_TYPE],
								NULL AS [TRAN_TIME],
								NULL AS [OUTPUT_REASON],
								NULL AS [BOOK_KEEPER],
								1 AS [CONFIRM_FLAG],
								NULL AS [PLAN_NO],
								NULL AS [ASN_NO],
								A.[TWD_RUNSHEET_NO] AS [RUNSHEET_NO],
								A.[ASSEMBLY_LINE],
								A.[PLANT_ZONE],
								A.[WORKSHOP],
								A.[TRANS_SUPPLIER_NUM],
								A.[PART_TYPE],
								NULL AS [SUPPLIER_TYPE],
								'TWD' AS [RUNSHEET_CODE],
								NULL AS [ERP_FLAG],
								NULL AS [LOGICAL_PK],
								NULL AS [BUSINESS_PK],
								A.[COMMENTS],
								GETDATE() AS [CREATE_DATE],
								A.[CREATE_USER],
								@ROUTE,
								A.[EXPECTED_ARRIVAL_TIME]
							FROM [LES].[TT_TWD_RUNSHEET] A WITH (NOLOCK)
							INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
							WHERE A.[TWD_RUNSHEET_SN] = @RunsheetSn
	
							SELECT @OutPutId = SCOPE_IDENTITY()

							---判断出库主表插入数据后的最新自增id号是否大于0
							IF @OutPutId > 0
								BEGIN		
									-------插入数据到出库明细表--------
									INSERT INTO [LES].[TT_WMM_OUTPUT_DETAIL]
									(
										[OUTPUT_ID],
										[PLANT],
										[SUPPLIER_NUM],
										[WM_NO],
										[ZONE_NO],
										[DLOC],
										[TRAN_NO],
										[TARGET_WM],
										[TARGET_ZONE],
										[TARGET_DLOC],
										[PART_NO],
										[PART_CNAME],
										[PACK_COUNT],
										[REQUIRED_BOX_NUM],
										[REQUIRED_QTY],
										[ACTUAL_BOX_NUM],
										[ACTUAL_QTY],
										[PACKAGE_MODEL],
										[BARCODE_DATA],
										[MEASURING_UNIT_NO],
										[PACKAGE],
										[NUM],
										[BOX_NUM],
										[IDENTIFY_PART_NO],
										[PART_ENAME],
										[DOCK],
										[ASSEMBLY_LINE],
										[BOX_PARTS],
										[SEQUENCE_NO],
										[PICKUP_SEQ_NO],
										[RDC_DLOC],
										[INHOUSE_PACKAGE],
										[INBOUND_PACKAGE_MODEL],
										[SUPPLIER_NUM_SHEET],
										[BOX_PARTS_SHEET],
										[ORDER_NO],
										[ITEM_NO],
										[TWD_RUNSHEET_NO],
										[COMMENTS],
										[CREATE_DATE],
										[CREATE_USER]
									)
									SELECT 
										@OutPutId AS [OUTPUT_ID],
										A.[PLANT],
										A.[SUPPLIER_NUM],
										B.[WM_NO],
										A.[SUPPLIER_NUM] AS	[ZONE_NO],
										(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] P WITH (NOLOCK) WHERE P.[PART_NO] = A.[PART_NO]
										AND P.[PLANT] = A.[PLANT] AND P.[ZONE_NO] = A.[SUPPLIER_NUM]) AS [DLOC],
										@RunsheetSn AS [TRAN_NO],
										C.[WM_NO] AS [TARGET_WM],
										C.[ZONE_NO] AS [TARGET_ZONE],
										(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] TARGET_P WITH (NOLOCK) WHERE TARGET_P.[PART_NO] = A.[PART_NO]
										AND TARGET_P.[PLANT] = A.[PLANT] AND TARGET_P.[ZONE_NO] = @PLANT_ZONE) AS [TARGET_DLOC],
										A.[PART_NO],
										A.[PART_CNAME],
										A.[PACK_COUNT],
										A.[REQUIRED_INBOUND_PACKAGE] AS	[REQUIRED_BOX_NUM],
										A.[REQUIRED_INBOUND_PACKAGE_QTY] AS	[REQUIRED_QTY],
										NULL,
										NULL,
										A.[INBOUND_PACKAGE_MODEL] AS [PACKAGE_MODEL],
										A.[BARCODE_DATA],
										A.[MEASURING_UNIT_NO],
										A.[INBOUND_PACKAGE] AS [PACKAGE],
										NULL AS	[NUM],
										NULL AS	[BOX_NUM],
										A.[IDENTIFY_PART_NO],
										A.[PART_ENAME],
										A.[DOCK],
										A.[ASSEMBLY_LINE],
										A.[BOX_PARTS],
										A.[SEQUENCE_NO],
										A.[PICKUP_SEQ_NO],
										A.[RDC_DLOC],
										A.[INBOUND_PACKAGE]	AS [INHOUSE_PACKAGE],
										A.[INBOUND_PACKAGE_MODEL],
										A.[SUPPLIER_NUM_SHEET],
										A.[BOX_PARTS_SHEET],
										A.[ORDER_NO],
										A.[ITEM_NO],
										@TWD_RUNSHEET_NO AS	[TWD_RUNSHEET_NO],
										A.[COMMENTS],
										GETDATE() AS [CREATE_DATE],
										'admin' AS	[CREATE_USER]
									FROM [LES].[TT_TWD_RUNSHEET_DETAIL] A WITH (NOLOCK)
									INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
									INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[ZONE_NO] = @PLANT_ZONE AND C.[PLANT] = A.[PLANT]
									WHERE A.[TWD_RUNSHEET_SN] = @RunsheetSn
									ORDER BY A.[PART_NO]
								END
							
							IF EXISTS
							(
								SELECT
									1
								FROM [LES].[TT_WMM_OUTPUT_DETAIL] A WITH (NOLOCK)
								INNER JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[TARGET_WM] = B.[WM_NO] AND A.[TARGET_ZONE] = B.[ZONE_NO] AND A.[PART_NO] = B.[PART_NO]
								WHERE A.[OUTPUT_ID] = @OutPutId AND B.[IS_CREATE_TASK] = 1
							)
								BEGIN
									SET @IS_CREATE_TASK = 1
								END
							IF @IS_CREATE_TASK = 1
								BEGIN
									EXEC [LES].[PROC_INSERT_OUTPUT_ONBOARDTASK] @OutPutId, @Type
								END
						END
					ELSE
						BEGIN
							IF (@IS_ASN = 0)
								BEGIN
									SET @RUNSHEET_CODE = 'TWD'
									IF @IS_TRAY = 1
										BEGIN
											SET @RUNSHEET_CODE = 'CTD'
										END
									--对于外部供应商，创建入库单
									INSERT INTO [LES].[TT_WMM_RECEIVE]
									(
										[RECEIVE_NO],
										[PLANT],
										[SUPPLIER_NUM],
										[WM_NO],
										[ZONE_NO],
										[DELIVERY_LOCATION_NO],
										[DOCK],
										[SEND_TIME],
										[RECEIVE_TYPE],
										[TRAN_TIME],
										[RECEIVE_REASON],
										[CONFIRM_FLAG],
										[TRANS_SUPPLIER_NUM],
										[PLAN_NO],
										[ASN_NO],
										[RUNSHEET_NO],
										[ASSEMBLY_LINE],
										[PLANT_ZONE],
										[WORKSHOP],
										[SUPPLIER_TYPE],
										[ERP_FLAG],
										[RUNSHEET_CODE],
										[DELIVERY_LOCATION_NAME],
										[COMMENTS],
										[CREATE_USER],
										[CREATE_DATE]
									)
									SELECT 
										A.[TWD_RUNSHEET_NO],
										A.[PLANT],
										A.[SUPPLIER_NUM],
										B.[WM_NO],
										B.[ZONE_NO],
										NULL,
										A.[DOCK],
										A.[SEND_TIME],
										1,
										NULL,
										'TWD PULL',
										1,
										A.[TRANS_SUPPLIER_NUM],
										NULL,
										NULL,
										A.[TWD_RUNSHEET_NO],
										A.[ASSEMBLY_LINE],
										A.[PLANT_ZONE],
										A.[WORKSHOP],
										1,
										0,
										@RUNSHEET_CODE,
										NULL,
										A.[COMMENTS],
										'admin',
										GETDATE()
									FROM [LES].[TT_TWD_RUNSHEET] AS A WITH (NOLOCK)
									LEFT JOIN [LES].[TM_WMM_ZONES] AS B WITH (NOLOCK) ON B.PLANT = A.PLANT AND B.ZONE_NO = A.[PLANT_ZONE]
									WHERE A.[TWD_RUNSHEET_SN] = @RunsheetSn

									SELECT @identity = SCOPE_IDENTITY()
									--判断入库主表插入数据后的最新自增id号是否大于0
									IF @identity > 0
										BEGIN
											-------插入数据到入库明细表--------
											INSERT INTO [LES].[TT_WMM_RECEIVE_DETAIL]
											(
												[RECEIVE_ID],
												[PLANT],
												[SUPPLIER_NUM],
												[WM_NO],
												[ZONE_NO],
												[DLOC],
												[TARGET_WM],
												[TARGET_ZONE],
												[TARGET_DLOC],
												[MEASURING_UNIT_NO],
												[PACKAGE],
												[NUM],
												[BOX_NUM],
												[PART_NO],
												[PART_CNAME],
												[PART_TYPE],
												[REQUIRED_BOX_NUM],
												[REQUIRED_QTY],
												[ACTUAL_BOX_NUM],
												[ACTUAL_QTY],
												[Current_BOX_NUM],
												[Current_QTY],
												[PACKAGE_MODEL],
												[BARCODE_DATA],
												[TRAN_NO],
												[IDENTIFY_PART_NO],
												[PART_ENAME],
												[DOCK],
												[ASSEMBLY_LINE],
												[BOX_PARTS],
												[SEQUENCE_NO],
												[PICKUP_SEQ_NO],
												[RDC_DLOC],
												[INHOUSE_PACKAGE],
												[INBOUND_PACKAGE_MODEL],
												[PACK_COUNT],
												[TWD_RUNSHEET_NO],
												[SUPPLIER_NUM_SHEET],
												[BOX_PARTS_SHEET],
												[RETURN_REPORT_FLAG],
												[ORDER_NO],
												[ITEM_NO],
												[COMMENTS],
												[CREATE_USER],
												[CREATE_DATE]
											)
											SELECT 
												@identity,							--[RECEIVE_ID]
												A.[PLANT],							--[PLANT]
												A.[SUPPLIER_NUM],					--[SUPPLIER_NUM]
												C.[WM_NO],							--[WM_NO]
												C.[ZONE_NO],						--[ZONE_NO]
												(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] P WITH (NOLOCK) WHERE P.[PART_NO] = A.[PART_NO] AND P.[PLANT] = A.[PLANT] AND P.[ZONE_NO] = @PLANT_ZONE)
												AS [DLOC],							--[DLOC]
												C.[WM_NO],							--[TARGET_WM]
												C.[ZONE_NO],						--[TARGET_ZONE]
												A.[RDC_DLOC],						--[TARGET_DLOC]
												(SELECT TOP 1 [PART_UNITS] FROM [LES].[TM_BAS_MAINTAIN_PARTS] T WITH (NOLOCK) WHERE T.[PART_NO] = A.[PART_NO] AND T.[PLANT] = A.[PLANT])
												AS [MEASURING_UNIT_NO],				--[MEASURING_UNIT_NO]
												A.[INBOUND_PACKAGE],				--[PACKAGE]
												A.[PACK_COUNT],						--[NUM]
												A.[REQUIRED_INBOUND_PACKAGE],		--[BOX_NUM]
												A.[PART_NO],						--[PART_NO]
												A.[PART_CNAME],						--[PART_CNAME]
												NULL,								--[PART_TYPE]
												A.[REQUIRED_INBOUND_PACKAGE],		--[REQUIRED_BOX_NUM]
												A.[REQUIRED_INBOUND_PACKAGE_QTY],	--[REQUIRED_QTY]
												NULL,								--[ACTUAL_BOX_NUM]
												NULL,								--[ACTUAL_QTY]
												NULL,								--[Current_BOX_NUM]
												NULL,								--[Current_QTY]
												A.[INBOUND_PACKAGE_MODEL],			--[PACKAGE_MODEL]
												NULL,								--[BARCODE_DATA]
												NULL,								--[TRAN_NO]
												NULL,								--[IDENTIFY_PART_NO]
												A.[PART_ENAME],						--[PART_ENAME]
												A.[DOCK],							--[DOCK]
												A.[ASSEMBLY_LINE],					--[ASSEMBLY_LINE]
												A.[BOX_PARTS],						--[BOX_PARTS]
												NULL,								--[SEQUENCE_NO]
												NULL,								--[PICKUP_SEQ_NO]
												A.[RDC_DLOC],						--[RDC_DLOC]
												A.[INBOUND_PACKAGE],				--[INHOUSE_PACKAGE]
												A.[INBOUND_PACKAGE_MODEL],			--[INBOUND_PACKAGE_MODEL]
												A.[PACK_COUNT],						--[PACK_COUNT]
												B.[TWD_RUNSHEET_NO],				--[TWD_RUNSHEET_NO]
												NULL,								--[SUPPLIER_NUM_SHEET]
												NULL,								--[BOX_PARTS_SHEET]
												NULL,								--[RETURN_REPORT_FLAG]
												A.[ORDER_NO],						--[ORDER_NO]
												A.[ITEM_NO],						--[ITEM_NO]
												A.[COMMENTS],						--[COMMENTS]
												'admin',							--[CREATE_USER]
												GETDATE()							--[CREATE_DATE]
											FROM [LES].[TT_TWD_RUNSHEET_DETAIL] AS A WITH (NOLOCK)
											INNER JOIN [LES].[TT_TWD_RUNSHEET] AS B WITH (NOLOCK) ON B.[TWD_RUNSHEET_SN] = A.[TWD_RUNSHEET_SN]
											LEFT JOIN [LES].[TM_WMM_ZONES] AS C WITH (NOLOCK) ON C.PLANT = A.PLANT AND C.ZONE_NO = @PLANT_ZONE
											WHERE A.[TWD_RUNSHEET_SN] = @RunsheetSn
											ORDER BY A.[PART_NO]
										END
								END
						END
				END
			------------------------------------------TWD--end-----------------------------------------------------------------
		END
	ELSE IF  @Type = 'PCS'
		BEGIN
			------------------------------------------PCS--start---------------------------------------------------------------
			DECLARE @PCS_RUNSHEET_NO NVARCHAR(100)
			SELECT  @PCS_RUNSHEET_NO = [PCS_RUNSHEET_NO], @PLANT_ZONE = [PLANT_ZONE], @PLANT = [PLANT], @Assmbley_Line = [ASSEMBLY_LINE], @Box_Part = [BOX_PARTS] FROM [LES].[TT_PCS_RUNSHEET] WITH (NOLOCK) WHERE [PCS_RUNSHEET_SN] = @RunsheetSn

			INSERT INTO [LES].[TT_WMM_OUTPUT]
			( 
				[OUTPUT_NO],			--出库流水号
				[PLANT],				--工厂
				[SUPPLIER_NUM],			--供应商
				[WM_NO],				--仓库编码
				[ZONE_NO],				--存储区编码
				[SEND_TIME],			--发送时间
				[OUTPUT_TYPE],			--出库类型
				[TRAN_TIME],			--入库时间
				[OUTPUT_REASON],		--出库原因
				[BOOK_KEEPER],			--收货员
				[CONFIRM_FLAG],			--确认标志
				[PLAN_NO],				--零件号
				[ASN_NO],				--ASN编号
				[RUNSHEET_NO],			--拉动单号
				[ASSEMBLY_LINE],		--流水线
				[PLANT_ZONE],			--厂区
				[WORKSHOP],				--车间
				[TRANS_SUPPLIER_NUM],	--运输供应商
				[PART_TYPE],			--零件类型
				[SUPPLIER_TYPE],		--供应商类型
				[RUNSHEET_CODE],		--单据号码
				[ERP_FLAG],				--标志
				[LOGICAL_PK],			--本地主键
				[BUSINESS_PK],			--业务主键
				[COMMENTS],				--备注
				[CREATE_DATE],
				[CREATE_USER],
				[ROUTE],
				[REQUEST_TIME]
			)
			SELECT 
				A.[PCS_RUNSHEET_NO] AS [OUTPUT_NO],
				A.[PLANT],
				A.[SUPPLIER_NUM],
				B.[WM_NO],
				A.[SUPPLIER_NUM] AS [ZONE_NO],
				GETDATE() AS [SEND_TIME],
				1 AS [OUTPUT_TYPE],
				NULL AS [TRAN_TIME],
				NULL AS [OUTPUT_REASON],
				NULL AS [BOOK_KEEPER],
				1 AS [CONFIRM_FLAG],
				NULL AS [PLAN_NO],
				NULL AS [ASN_NO],
				A.[PCS_RUNSHEET_NO] AS [RUNSHEET_NO],
				A.[ASSEMBLY_LINE],
				A.[PLANT_ZONE],
				A.[WORKSHOP],
				A.[TRANS_SUPPLIER_NUM],
				NULL AS	[PART_TYPE],
				NULL AS [SUPPLIER_TYPE],
				'PCS' AS [RUNSHEET_CODE],
				NULL AS [ERP_FLAG],
				NULL AS [LOGICAL_PK],
				NULL AS [BUSINESS_PK],
				A.[COMMENTS],
				GETDATE() AS [CREATE_DATE],
				A.[CREATE_USER],
				C.[ROUTE],
				A.[EXPECTED_ARRIVAL_TIME]
			FROM [LES].[TT_PCS_RUNSHEET] A WITH (NOLOCK)
			INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
			INNER JOIN [LES].[TM_PCS_ROUTE_BOX_PARTS] C WITH (NOLOCK) ON C.[PLANT] = A.[PLANT] AND C.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND C.[BOX_PARTS] = A.[BOX_PARTS]
			WHERE A.[PCS_RUNSHEET_SN] = @RunsheetSn

			SELECT @OutPutId = SCOPE_IDENTITY()

			--判断出库主表插入数据后的最新自增id号是否大于0
			IF @OutPutId > 0
				BEGIN		
					-------插入数据到出库明细表--------
					INSERT INTO [LES].[TT_WMM_OUTPUT_DETAIL]
					( 
						[OUTPUT_ID],				--出库主表ID
						[PLANT],					--工厂
						[SUPPLIER_NUM],				--供应商
						[WM_NO],					--仓库编码
						[ZONE_NO],					--存储区编码
						[DLOC],						--库位
						[TRAN_NO],					--交易编码
						[TARGET_WM],				--目的仓库
						[TARGET_ZONE],				--目的存储区
						[TARGET_DLOC],				--目的库位
						[PART_NO],					--零件号
						[PART_CNAME],				--零件中文名
						[PACK_COUNT],				--包装数
						[REQUIRED_BOX_NUM],			--需求箱数
						[REQUIRED_QTY],				--需求数量
						[ACTUAL_BOX_NUM],			--实际箱数
						[ACTUAL_QTY],				--实际数量
						[PACKAGE_MODEL],			--包装型号
						[BARCODE_DATA],				--条码
						[MEASURING_UNIT_NO],		--单位
						[PACKAGE],					--包装
						[NUM],						--数量
						[BOX_NUM],					--箱数
						[IDENTIFY_PART_NO],			--标识零件号
						[PART_ENAME],				--零件德文名
						[DOCK],						--工厂模型DOCK
						[ASSEMBLY_LINE],			--流水线
						[BOX_PARTS],				--零件类
						[SEQUENCE_NO],				--排序号
						[PICKUP_SEQ_NO],			--捡料顺序号
						[RDC_DLOC],					--供应商库位
						[INHOUSE_PACKAGE],			--上线包装数量
						[INBOUND_PACKAGE_MODEL],	--上线包装型号
						[SUPPLIER_NUM_SHEET],		--基础数据组单_供应商
						[BOX_PARTS_SHEET],			--零件类组单
						[ORDER_NO],					--订单号
						[ITEM_NO],
						[TWD_RUNSHEET_NO],			--拉动单号
						[COMMENTS],					--备注
						[CREATE_DATE],
						[CREATE_USER]
					)
					SELECT 
						@OutPutId AS [OUTPUT_ID],
						A.[PLANT],
						A.[SUPPLIER_NUM],
						B.[WM_NO],
						A.[SUPPLIER_NUM] AS [ZONE_NO],
						(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] P WITH (NOLOCK) WHERE P.[PART_NO] = A.[PART_NO] 
						AND P.[PLANT] = A.[PLANT] AND P.[ZONE_NO] = A.[SUPPLIER_NUM]) AS [DLOC],
						@RunsheetSn AS [TRAN_NO],
						C.[WM_NO] AS [TARGET_WM],
						C.[ZONE_NO] AS [TARGET_ZONE],
						(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] TARGET_P WITH (NOLOCK) WHERE TARGET_P.[PART_NO] = A.[PART_NO]
						AND TARGET_P.[PLANT] = A.[PLANT] AND TARGET_P.[ZONE_NO] = @PLANT_ZONE) AS [TARGET_DLOC],
						A.[PART_NO],
						A.[PART_CNAME],
						A.[PACK_COUNT],
						A.[REQUIRED_INHOUSE_PACKAGE] AS	[REQUIRED_BOX_NUM],
						A.[REQUIRED_INHOUSE_PACKAGE_QTY] AS	[REQUIRED_QTY],
						NULL AS	[ACTUAL_BOX_NUM],
						NULL AS [ACTUAL_QTY],
						A.[INHOUSE_PACKAGE_MODEL] AS [PACKAGE_MODEL],
						NULL AS	[BARCODE_DATA],
						A.[MEASURING_UNIT_NO],
						A.[INHOUSE_PACKAGE] AS [PACKAGE],
						NULL AS	[NUM],
						NULL AS	[BOX_NUM],
						NULL AS	[IDENTIFY_PART_NO],
						A.[PART_ENAME],
						A.[DOCK],
						A.[ASSEMBLY_LINE],
						A.[BOX_PARTS],
						A.[SEQUENCE_NO],
						A.[PICKUP_SEQ_NO],
						A.[RDC_DLOC],
						A.[INHOUSE_PACKAGE]	AS [INHOUSE_PACKAGE],
						A.[INHOUSE_PACKAGE_MODEL] AS [INBOUND_PACKAGE_MODEL],
						NULL AS	[SUPPLIER_NUM_SHEET],
						NULL AS	[BOX_PARTS_SHEET],
						NULL AS	[ORDER_NO],
						NULL AS	[ITEM_NO],
						@PCS_RUNSHEET_NO AS [TWD_RUNSHEET_NO],
						A.[COMMENTS],
						GETDATE() AS [CREATE_DATE],
						'admin' AS [CREATE_USER]
					FROM [LES].[TT_PCS_RUNSHEET_DETAIL] A WITH (NOLOCK)
					INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
					INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[PLANT] = A.[PLANT] AND C.[ZONE_NO] = @PLANT_ZONE
					WHERE A.[PCS_RUNSHEET_SN] = @RunsheetSn
					ORDER BY A.[RUNSHEET_DETAIL_ID]
				END

			IF EXISTS
			(
				SELECT
					1
				FROM [LES].[TT_WMM_OUTPUT_DETAIL] A WITH (NOLOCK)
				INNER JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[TARGET_WM] = B.[WM_NO] AND A.[TARGET_ZONE] = B.[ZONE_NO] AND A.[PART_NO] = B.[PART_NO]
				WHERE A.[OUTPUT_ID] = @OutPutId AND B.[IS_CREATE_TASK] = 1
			)
				BEGIN
					SET @IS_CREATE_TASK = 1
				END
			IF @IS_CREATE_TASK = 1
				BEGIN
					EXEC [LES].[PROC_INSERT_OUTPUT_ONBOARDTASK] @OutPutId, @Type
				END
			------------------------------------------PCS--end-----------------------------------------------------------------
		END
	ELSE IF  @Type = 'JIS'
		BEGIN
			------------------------------------------JIS--start-----------------------------------------------------------------
			DECLARE @JIS_RUNSHEET_NO NVARCHAR(100)	--拉动单号
			SELECT @JIS_RUNSHEET_NO = [JIS_RUNSHEET_NO], @SUPPLIER_NUM = [SUPPLIER_NUM], @PLANT_ZONE = [PLANT_ZONE], @PLANT = [PLANT], @Assmbley_Line = [ASSEMBLY_LINE], @Box_Part = [RACK] FROM [LES].[TT_JIS_RUNSHEET] WITH (NOLOCK) WHERE [JIS_RUNSHEET_SN] = @RunsheetSn
			SELECT @SUPPLIER_TYPE = [SUPPLIER_TYPE] FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK) WHERE SUPPLIER_NUM = @SUPPLIER_NUM

			IF @SUPPLIER_TYPE = 2
				BEGIN
					--对于内部拉动，创建出库单
					INSERT INTO [LES].[TT_WMM_OUTPUT]
					( 
						[OUTPUT_NO],			--出库流水号
						[PLANT],				--工厂
						[SUPPLIER_NUM],			--供应商
						[WM_NO],				--仓库编码
						[ZONE_NO],				--存储区编码
						[SEND_TIME],			--发送时间
						[OUTPUT_TYPE],			--出库类型
						[TRAN_TIME],			--入库时间
						[OUTPUT_REASON],		--出库原因
						[BOOK_KEEPER],			--收货员
						[CONFIRM_FLAG],			--确认标志
						[PLAN_NO],				--零件号
						[ASN_NO],				--ASN编号
						[RUNSHEET_NO],			--拉动单号
						[ASSEMBLY_LINE],		--流水线
						[PLANT_ZONE],			--厂区
						[WORKSHOP],				--车间
						[TRANS_SUPPLIER_NUM],	--运输供应商
						[PART_TYPE],			--零件类型
						[SUPPLIER_TYPE],		--供应商类型
						[RUNSHEET_CODE],		--单据号码
						[ERP_FLAG],				--标志
						[LOGICAL_PK],			--本地主键
						[BUSINESS_PK],			--业务主键
						[COMMENTS],				--备注
						[CREATE_DATE],
						[CREATE_USER],
						[ROUTE],
						[REQUEST_TIME]
					)
					SELECT 
						A.[JIS_RUNSHEET_NO] AS [OUTPUT_NO],
						A.[PLANT],
						A.[SUPPLIER_NUM],
						B.[WM_NO],
						A.[SUPPLIER_NUM] AS [ZONE_NO],
						GETDATE() AS [SEND_TIME],
						1 AS [OUTPUT_TYPE],
						NULL AS [TRAN_TIME],
						NULL AS [OUTPUT_REASON],
						NULL AS [BOOK_KEEPER],
						1 AS [CONFIRM_FLAG],
						NULL AS [PLAN_NO],
						NULL AS [ASN_NO],
						A.[JIS_RUNSHEET_NO] AS [RUNSHEET_NO],
						A.[ASSEMBLY_LINE],
						A.[PLANT_ZONE],
						A.[WORKSHOP],
						A.[TRANS_SUPPLIER_NUM],
						NULL AS	[PART_TYPE],
						NULL AS [SUPPLIER_TYPE],
						'JIS' AS [RUNSHEET_CODE],
						NULL AS [ERP_FLAG],
						NULL AS [LOGICAL_PK],
						NULL AS [BUSINESS_PK],
						A.[COMMENTS],
						GETDATE() AS [CREATE_DATE],
						A.[CREATE_USER],
						C.[ROUTE],
						A.[EXPECTED_ARRIVAL_TIME]
					FROM [LES].[TT_JIS_RUNSHEET] A WITH (NOLOCK)
					INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
					INNER JOIN [LES].[TM_JIS_RACK] C WITH (NOLOCK) ON C.[RACK] = A.[RACK] and C.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE]
					WHERE A.[JIS_RUNSHEET_SN] = @RunsheetSn

					SELECT @OutPutId = SCOPE_IDENTITY()

					--判断出库主表插入数据后的最新自增id号是否大于0
					IF @OutPutId > 0
						BEGIN		
							-------插入数据到出库明细表--------
							INSERT INTO [LES].[TT_WMM_OUTPUT_DETAIL]
							(
								[OUTPUT_ID],				--出库主表ID
								[PLANT],					--工厂
								[SUPPLIER_NUM],				--供应商
								[WM_NO],					--仓库编码
								[ZONE_NO],					--存储区编码
								[DLOC],						--库位
								[TRAN_NO],					--交易编码
								[TARGET_WM],				--目的仓库
								[TARGET_ZONE],				--目的存储区
								[TARGET_DLOC],				--目的库位
								[PART_NO],					--零件号
								[PART_CNAME],				--零件中文名
								[PACK_COUNT],				--包装数
								[REQUIRED_BOX_NUM],			--需求箱数
								[REQUIRED_QTY],				--需求数量
								[ACTUAL_BOX_NUM],			--实际箱数
								[ACTUAL_QTY],				--实际数量
								[PACKAGE_MODEL],			--包装型号
								[BARCODE_DATA],				--条码
								[MEASURING_UNIT_NO],		--单位
								[PACKAGE],					--单位
								[NUM],						--数量
								[BOX_NUM],					--箱数
								[IDENTIFY_PART_NO],			--标识零件号
								[PART_ENAME],				--零件德文名
								[DOCK],						--工厂模型DOCK
								[ASSEMBLY_LINE],			--流水线
								[BOX_PARTS],				--零件类
								[SEQUENCE_NO],				--排序号
								[PICKUP_SEQ_NO],			--捡料顺序号
								[RDC_DLOC],					--供应商库位
								[INHOUSE_PACKAGE],			--上线包装数量
								[INBOUND_PACKAGE_MODEL],	--上线包装型号
								[SUPPLIER_NUM_SHEET],		--基础数据组单_供应商
								[BOX_PARTS_SHEET],			--零件类组单
								[ORDER_NO],					--订单号
								[ITEM_NO],
								[TWD_RUNSHEET_NO],			--拉动单号
								[COMMENTS],					--备注
								[CREATE_DATE],
								[CREATE_USER]
							)
							SELECT 
								@OutPutId AS [OUTPUT_ID],
								D.[PLANT] AS [PLANT],
								F.[SUPPLIER_NUM],
								B.[WM_NO],
								F.[SUPPLIER_NUM] AS [ZONE_NO],
								(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] P WITH (NOLOCK) WHERE P.[PART_NO] = D.[PART_NO] 
								AND P.[PLANT] = F.[PLANT] AND P.[ZONE_NO] = F.[SUPPLIER_NUM]) AS [DLOC],
								@RunsheetSn AS [TRAN_NO],
								C.[WM_NO] AS [TARGET_WM],
								C.[ZONE_NO] AS [TARGET_ZONE],
								(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] TARGET_P WITH (NOLOCK) WHERE TARGET_P.[PART_NO] = D.[PART_NO] 
								AND TARGET_P.[PLANT] = F.[PLANT] AND TARGET_P.[ZONE_NO] = @PLANT_ZONE) AS [TARGET_DLOC],
								D.[PART_NO],
								D.[PART_CNAME],
								E.[INBOUND_PACKAGE] AS [PACK_COUNT],
								1 AS [REQUIRED_BOX_NUM],						--需求箱数
								D.[USAGE] AS [REQUIRED_QTY],					--需求数量
								1 AS [ACTUAL_BOX_NUM],							--实际箱数
								D.[USAGE] AS [ACTUAL_QTY],						--实际数量
								E.[INBOUND_PACKAGE_MODEL] AS [PACKAGE_MODEL],	--包装型号
								NULL AS	[BARCODE_DATA],
								(SELECT TOP 1 [PART_UNITS] FROM [LES].[TM_BAS_MAINTAIN_PARTS] S WITH (NOLOCK) WHERE S.[PLANT] = D.[PLANT] AND S.[PART_NO] = D.[PART_NO]) AS	[MEASURING_UNIT_NO],--单位
								E.[INBOUND_PACKAGE] AS [PACKAGE],
								NULL AS	[NUM],
								NULL AS	[BOX_NUM],
								D.[PART_NO] AS [IDENTIFY_PART_NO],
								E.[PART_ENAME] AS [PART_ENAME],
								E.[DOCK],
								D.[ASSEMBLY_LINE],
								F.[RACK] AS [BOX_PARTS],
								NULL AS	[SEQUENCE_NO],
								NULL AS	[PICKUP_SEQ_NO],
								NULL AS	[RDC_DLOC],
								E.[INBOUND_PACKAGE]	AS [INHOUSE_PACKAGE],
								E.[INBOUND_PACKAGE_MODEL] AS [INBOUND_PACKAGE_MODEL],
								NULL AS	[SUPPLIER_NUM_SHEET],
								NULL AS	[BOX_PARTS_SHEET],
								NULL AS	[ORDER_NO],
								NULL AS	[ITEM_NO],
								@JIS_RUNSHEET_NO AS	[TWD_RUNSHEET_NO],
								D.[COMMENTS],
								GETDATE() AS [CREATE_DATE],
								'admin' AS [CREATE_USER]
							FROM [LES].[TT_JIS_RUNSHEET_FLEX] F WITH (NOLOCK)
							INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[ZONE_NO] = F.[SUPPLIER_NUM]
							INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[ZONE_NO] = @PLANT_ZONE
							INNER JOIN [LES].[TT_JIS_RUNSHEET_DETAIL] D WITH (NOLOCK) ON F.[JIS_RUNSHEET_FLEX_SN] = D.[JIS_RUNSHEET_FLEX_SN]
							INNER JOIN [LES].[TM_JIS_RACK] R WITH (NOLOCK) ON F.[RACK] = R.[RACK] AND R.[ASSEMBLY_LINE] = F.[ASSEMBLY_LINE]
							INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] E WITH (NOLOCK) ON E.[LOCATION] = R.[LOCATION] AND E.[PLANT] = F.[PLANT] AND E.[PART_NO] = D.[PART_NO] AND E.[INHOUSE_PART_CLASS] = F.[RACK] AND E.[INHOUSE_SYSTEM_MODE] = 'JIS'
							WHERE F.[JIS_RUNSHEET_SN] = @RunsheetSn AND F.[PLANT] = '9110'
							ORDER BY D.[JIS_RUNSHEET_FLEX_SN], D.[JIS_RUNSHEET_PART_SN]
						END

					IF EXISTS
					(
						SELECT
							1
						FROM [LES].[TT_WMM_OUTPUT_DETAIL] A WITH (NOLOCK)
						INNER JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON A.[PLANT] = B.[PLANT] AND A.[TARGET_WM] = B.[WM_NO] AND A.[TARGET_ZONE] = B.[ZONE_NO] AND A.[PART_NO] = B.[PART_NO]
						WHERE A.[OUTPUT_ID] = @OutPutId AND B.[IS_CREATE_TASK] = 1
					)
						BEGIN
							SET @IS_CREATE_TASK = 1
						END
					IF @IS_CREATE_TASK = 1
						BEGIN
							EXEC [LES].[PROC_INSERT_OUTPUT_ONBOARDTASK] @OutPutId, @Type
						END
				END
			ELSE
				BEGIN
					--对于外部供应商，创建入库单
					INSERT INTO [LES].[TT_WMM_RECEIVE]
					(
						[RECEIVE_NO],
						[PLANT],
						[SUPPLIER_NUM],
						[WM_NO],
						[ZONE_NO],
						[DELIVERY_LOCATION_NO],
						[DOCK],
						[SEND_TIME],
						[RECEIVE_TYPE],
						[TRAN_TIME],
						[RECEIVE_REASON],
						[CONFIRM_FLAG],
						[TRANS_SUPPLIER_NUM],
						[PLAN_NO],
						[ASN_NO],
						[RUNSHEET_NO],
						[ASSEMBLY_LINE],
						[PLANT_ZONE],
						[WORKSHOP],
						[SUPPLIER_TYPE],
						[ERP_FLAG],
						[RUNSHEET_CODE],
						[DELIVERY_LOCATION_NAME],
						[COMMENTS],
						[CREATE_USER],
						[CREATE_DATE]
					)
					SELECT 
						A.[JIS_RUNSHEET_NO],
						A.[PLANT],
						A.[SUPPLIER_NUM],
						B.[WM_NO],
						B.[ZONE_NO],
						NULL,
						A.[DOCK],
						A.[SEND_TIME],
						1,
						NULL,
						'JIS PULL',
						1,
						A.[TRANS_SUPPLIER_NUM],
						NULL,
						NULL,
						A.[JIS_RUNSHEET_NO],
						A.[ASSEMBLY_LINE],
						A.[PLANT_ZONE],
						A.[WORKSHOP],
						1,
						0,
						'JIS',
						NULL,
						A.[COMMENTS],
						'admin',
						GETDATE()
					FROM [LES].[TT_JIS_RUNSHEET] A WITH (NOLOCK)
					INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[PLANT_ZONE]
					INNER JOIN [LES].[TM_JIS_RACK] C WITH (NOLOCK) ON C.[RACK] = A.[RACK] AND C.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE]
					WHERE A.[JIS_RUNSHEET_SN] = @RunsheetSn

					SELECT @identity = SCOPE_IDENTITY()

					--判断入库主表插入数据后的最新自增id号是否大于0
					IF @identity > 0
						BEGIN
							-------插入数据到入库明细表--------
							INSERT INTO [LES].[TT_WMM_RECEIVE_DETAIL]
							(
								[RECEIVE_ID],
								[PLANT],
								[SUPPLIER_NUM],
								[WM_NO],
								[ZONE_NO],
								[DLOC],
								[TARGET_WM],
								[TARGET_ZONE],
								[TARGET_DLOC],
								[MEASURING_UNIT_NO],
								[PACKAGE],
								[NUM],
								[BOX_NUM],
								[PART_NO],
								[PART_CNAME],
								[PART_TYPE],
								[REQUIRED_BOX_NUM],
								[REQUIRED_QTY],
								[ACTUAL_BOX_NUM],
								[ACTUAL_QTY],
								[Current_BOX_NUM],
								[Current_QTY],
								[PACKAGE_MODEL],
								[BARCODE_DATA],
								[TRAN_NO],
								[IDENTIFY_PART_NO],
								[PART_ENAME],
								[DOCK],
								[ASSEMBLY_LINE],
								[BOX_PARTS],
								[SEQUENCE_NO],
								[PICKUP_SEQ_NO],
								[RDC_DLOC],
								[INHOUSE_PACKAGE],
								[INBOUND_PACKAGE_MODEL],
								[PACK_COUNT],
								[TWD_RUNSHEET_NO],
								[SUPPLIER_NUM_SHEET],
								[BOX_PARTS_SHEET],
								[RETURN_REPORT_FLAG],
								[ORDER_NO],
								[ITEM_NO],
								[COMMENTS],
								[CREATE_USER],
								[CREATE_DATE]
							)
							SELECT 
								@identity,					--[RECEIVE_ID]
								F.[PLANT],					--[PLANT]
								F.[SUPPLIER_NUM],			--[SUPPLIER_NUM]
								C.[WM_NO],					--[WM_NO]
								C.[ZONE_NO],				--[ZONE_NO]
								E.[DLOC],					--[DLOC]
								C.[WM_NO],					--[TARGET_WM]
								C.[ZONE_NO],				--[TARGET_ZONE]
								E.[DLOC],					--[TARGET_DLOC]
								T.[PART_UNITS],				--[MEASURING_UNIT_NO]
								E.[INBOUND_PACKAGE],		--[PACKAGE]
								D.[USAGE],					--[NUM]
								1,							--[BOX_NUM]
								D.[PART_NO],				--[PART_NO]
								D.[PART_CNAME],				--[PART_CNAME]
								NULL,						--[PART_TYPE]
								1,							--[REQUIRED_BOX_NUM]
								D.[USAGE],					--[REQUIRED_QTY]
								NULL,						--[ACTUAL_BOX_NUM]
								NULL,						--[ACTUAL_QTY]
								1,							--[Current_BOX_NUM]
								D.[USAGE],					--[Current_QTY]
								E.[INBOUND_PACKAGE_MODEL],	--[PACKAGE_MODEL]
								NULL,						--[BARCODE_DATA]
								NULL,						--[TRAN_NO]
								NULL,						--[IDENTIFY_PART_NO]
								--生成时实际箱数实际件数应为空
								--D.PART_NO,D.PART_CNAME,NULL,1,D.USAGE,null,null,E.INBOUND_PACKAGE_MODEL,NULL,NULL,NULL, 
								E.[PART_ENAME],				--[PART_ENAME]
								E.[DOCK],					--[DOCK]
								NULL,						--[ASSEMBLY_LINE]
								F.[RACK] AS [BOX_PARTS],	--[BOX_PARTS]
								NULL,						--[SEQUENCE_NO]
								NULL,						--[PICKUP_SEQ_NO]
								NULL,						--[RDC_DLOC]
								1,							--[INHOUSE_PACKAGE]
								E.[INBOUND_PACKAGE_MODEL],	--[INBOUND_PACKAGE_MODEL]
								NULL,						--[PACK_COUNT]
								NULL,						--[TWD_RUNSHEET_NO]
								NULL,						--[SUPPLIER_NUM_SHEET]
								NULL,						--[BOX_PARTS_SHEET]
								NULL,						--[RETURN_REPORT_FLAG]
								D.[ORDER_NO],				--[ORDER_NO]
								D.[ITEM_NO],				--[ITEM_NO]
								D.[COMMENTS],				--[COMMENTS]
								'admin',					--[CREATE_USER]
								GETDATE()					--[CREATE_DATE]
							FROM [LES].[TT_JIS_RUNSHEET_FLEX] F WITH (NOLOCK)
							INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[ZONE_NO] = @PLANT_ZONE
							INNER JOIN [LES].[TT_JIS_RUNSHEET_DETAIL] D WITH (NOLOCK) ON F.[JIS_RUNSHEET_FLEX_SN] = D.[JIS_RUNSHEET_FLEX_SN]
							INNER JOIN [LES].[TM_JIS_RACK] R WITH (NOLOCK) ON F.[RACK] = R.[RACK] AND R.[ASSEMBLY_LINE] = F.[ASSEMBLY_LINE]
							INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] E WITH (NOLOCK) ON E.[LOCATION] = R.[LOCATION] AND E.[PLANT] = F.[PLANT] AND E.[PART_NO] = D.[PART_NO] AND E.[INHOUSE_PART_CLASS] = F.[RACK] AND E.[INHOUSE_SYSTEM_MODE] = 'JIS'
							LEFT JOIN [LES].[TM_BAS_MAINTAIN_PARTS] T WITH (NOLOCK) ON D.[PLANT] = T.[PLANT] AND D.[PART_NO] = T.[PART_NO]
							WHERE F.[JIS_RUNSHEET_SN] = @RunsheetSn
							ORDER BY D.[JIS_RUNSHEET_FLEX_SN], D.[JIS_RUNSHEET_PART_SN]
						END
				END
			------------------------------------------JIS--end-----------------------------------------------------------------
		END
	ELSE IF  @Type = 'EPS'
		BEGIN
			------------------------------------------EPS--START-----------------------------------------------------------------
			SELECT @PLANT_ZONE = PLANT_ZONE FROM [LES].[TT_EPS_TASK] WITH (NOLOCK) WHERE [TASK_SN] = @RunsheetSn

			INSERT INTO [LES].[TT_WMM_OUTPUT]
			(
				[OUTPUT_NO],			--出库流水号
				[PLANT],				--工厂
				[SUPPLIER_NUM],			--供应商
				[WM_NO],				--仓库编码
				[ZONE_NO],				--存储区编码
				[SEND_TIME],			--发送时间
				[OUTPUT_TYPE],			--出库类型
				[TRAN_TIME],			--入库时间
				[OUTPUT_REASON],		--出库原因
				[BOOK_KEEPER],			--收货员
				[CONFIRM_FLAG],			--确认标志
				[PLAN_NO],				--零件号
				[ASN_NO],				--ASN编号
				[RUNSHEET_NO],			--拉动单号
				[ASSEMBLY_LINE],		--流水线
				[PLANT_ZONE],			--厂区
				[WORKSHOP],				--车间
				[TRANS_SUPPLIER_NUM],	--运输供应商
				[PART_TYPE],			--零件类型
				[SUPPLIER_TYPE],		--供应商类型
				[RUNSHEET_CODE],		--单据号码
				[ERP_FLAG],				--标志
				[LOGICAL_PK],			--本地主键
				[BUSINESS_PK],			--业务主键
				[COMMENTS],				--备注
				[CREATE_DATE],
				[CREATE_USER],
				[ROUTE],
				[REQUEST_TIME]
			)
			SELECT 
				A.[RUNSHEET_NO] AS [OUTPUT_NO],
				A.[PLANT],
				A.[SUPPLIER_NUM],
				B.[WM_NO],
				A.[SUPPLIER_NUM] AS [ZONE_NO],
				GETDATE() AS [SEND_TIME],
				1 AS [OUTPUT_TYPE],
				NULL AS [TRAN_TIME],
				NULL AS [OUTPUT_REASON],
				NULL AS [BOOK_KEEPER],
				1 AS [CONFIRM_FLAG],
				NULL AS [PLAN_NO],
				NULL AS [ASN_NO],
				A.[RUNSHEET_NO],
				A.[ASSEMBLY_LINE],
				A.[PLANT_ZONE],
				NULL AS [WORKSHOP],
				A.[TRANS_SUPPLIER_NUM],
				NULL AS	[PART_TYPE],
				NULL AS [SUPPLIER_TYPE],
				'EPS' AS [RUNSHEET_CODE],
				NULL AS [ERP_FLAG],
				NULL AS [LOGICAL_PK],
				NULL AS [BUSINESS_PK],
				A.[COMMENTS],
				GETDATE() AS [CREATE_DATE],
				A.[CREATE_USER],
				A.[ROUTE],
				A.[REQUEST_TIME]
			FROM [LES].[TT_EPS_TASK] A WITH (NOLOCK)
			INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
			WHERE A.[TASK_SN] = @RunsheetSn

			SELECT @OutPutId = SCOPE_IDENTITY()

			--判断出库主表插入数据后的最新自增id号是否大于0
			IF @OutPutId > 0
				BEGIN
					-------插入数据到出库明细表--------
					INSERT INTO [LES].[TT_WMM_OUTPUT_DETAIL]
					(
						[OUTPUT_ID],				--出库主表ID
						[PLANT],					--工厂
						[SUPPLIER_NUM],				--供应商
						[WM_NO],					--仓库编码
						[ZONE_NO],					--存储区编码
						[DLOC],						---库位
						[TRAN_NO],					--交易编码
						[TARGET_WM],				--目的仓库
						[TARGET_ZONE],				--目的存储区
						[TARGET_DLOC],				--目的库位
						[PART_NO],					--零件号
						[PART_CNAME],				--零件中文名
						[PACK_COUNT],				--包装数
						[REQUIRED_BOX_NUM],			--需求箱数
						[REQUIRED_QTY],				--需求数量
						[ACTUAL_BOX_NUM],			--实际箱数
						[ACTUAL_QTY],				--实际数量
						[PACKAGE_MODEL],			--包装型号
						[BARCODE_DATA],				--条码
						[MEASURING_UNIT_NO],		--单位
						[PACKAGE],					--单位
						[NUM],						--数量
						[BOX_NUM],					--箱数
						[IDENTIFY_PART_NO],			--标识零件号
						[PART_ENAME],				--零件德文名
						[DOCK],						--工厂模型DOCK
						[ASSEMBLY_LINE],			--流水线
						[BOX_PARTS],				--零件类
						[SEQUENCE_NO],				--排序号
						[PICKUP_SEQ_NO],			--捡料顺序号
						[RDC_DLOC],					--供应商库位
						[INHOUSE_PACKAGE],			--上线包装数量
						[INBOUND_PACKAGE_MODEL],	--上线包装型号
						[SUPPLIER_NUM_SHEET],		--基础数据组单_供应商
						[BOX_PARTS_SHEET],			--零件类组单
						[ORDER_NO],					--订单号
						[ITEM_NO],
						[TWD_RUNSHEET_NO],			--拉动单号
						[COMMENTS],					--备注
						[CREATE_DATE],
						[CREATE_USER]
					)
					SELECT 
						@OutPutId AS [OUTPUT_ID],
						A.[PLANT],
						A.[SUPPLIER_NUM],
						B.[WM_NO],
						A.[SUPPLIER_NUM] AS [ZONE_NO],
						(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] P WITH (NOLOCK) WHERE P.[PART_NO] = A.[PART_NO] 
						AND P.[PLANT] = A.[PLANT] AND P.[ZONE_NO] = A.[SUPPLIER_NUM]) AS [DLOC],
						@RunsheetSn AS [TRAN_NO],
						C.[WM_NO] AS [TARGET_WM],
						C.[ZONE_NO] AS [TARGET_ZONE],
						(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] TARGET_P WITH (NOLOCK) WHERE TARGET_P.[PART_NO] = A.[PART_NO] 
						AND TARGET_P.[PLANT] = A.[PLANT] AND TARGET_P.[ZONE_NO] = @PLANT_ZONE) AS [TARGET_DLOC],
						A.[PART_NO],
						D.[PART_CNAME],
						D.[INHOUSE_PACKAGE] AS [PACK_COUNT],  --包装数
						A.[USAGE] AS [REQUIRED_BOX_NUM],--需求箱数
						ISNULL(D.[INHOUSE_PACKAGE], 0) * ISNULL(A.[USAGE], 0) AS [REQUIRED_QTY],--需求数量
						NULL AS	[ACTUAL_BOX_NUM],--实际箱数
						NULL AS [ACTUAL_QTY],--实际数量
						D.[INHOUSE_PACKAGE_MODEL] AS [PACKAGE_MODEL],--包装型号
						NULL AS	[BARCODE_DATA],
						NULL AS [MEASURING_UNIT_NO],--单位
						D.[INHOUSE_PACKAGE] AS [PACKAGE],
						NULL AS	[NUM],
						NULL AS	[BOX_NUM],
						A.[PART_NO] AS [IDENTIFY_PART_NO],
						D.[PART_ENAME] AS [PART_ENAME],
						NULL AS	[DOCK],
						A.[ASSEMBLY_LINE],
						A.[ROUTE] AS [BOX_PARTS],
						NULL AS	[SEQUENCE_NO],
						NULL AS	[PICKUP_SEQ_NO],
						NULL AS	[RDC_DLOC],
						D.[INHOUSE_PACKAGE],
						D.[INHOUSE_PACKAGE_MODEL] AS [INBOUND_PACKAGE_MODEL],
						NULL AS	[SUPPLIER_NUM_SHEET],
						NULL AS	[BOX_PARTS_SHEET],
						NULL AS	[ORDER_NO],
						NULL AS	[ITEM_NO],
						@RunsheetSn AS [TWD_RUNSHEET_NO],
						A.[COMMENTS],
						GETDATE() AS [CREATE_DATE],
						'admin' AS	[CREATE_USER]
					FROM [LES].[TT_EPS_TASK] A WITH (NOLOCK)
					INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
					INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[ZONE_NO] = @PLANT_ZONE
					INNER JOIN [LES].[TM_BAS_MAINTAIN_PARTS] D WITH (NOLOCK) ON D.[PLANT] = A.[PLANT] AND D.[PART_NO] = A.[PART_NO]  --从零件信息中获取，零件拉动关系中不维护EPS拉动关系
					WHERE A.[TASK_SN] = @RunsheetSn
				END
			------------------------------------------EPS--end-----------------------------------------------------------------
		END
	ELSE IF  @Type = 'SPS'
		BEGIN
			------------------------------------------SPS--start---------------------------------------------------------------
			DECLARE @SPS_RUNSHEET_NO NVARCHAR(100)
			SELECT  @SPS_RUNSHEET_NO = [SPS_RUNSHEET_NO], @PLANT_ZONE = [PLANT_ZONE], @PLANT = [PLANT], @Assmbley_Line = [ASSEMBLY_LINE], @Box_Part = [BOX_PARTS] FROM [LES].[TT_SPS_RUNSHEET] WITH (NOLOCK) WHERE [SPS_RUNSHEET_SN] = @RunsheetSn

			INSERT INTO [LES].[TT_WMM_OUTPUT]
			( 
				[OUTPUT_NO],			--出库流水号
				[PLANT],				--工厂
				[SUPPLIER_NUM],			--供应商
				[WM_NO],				--仓库编码
				[ZONE_NO],				--存储区编码
				[SEND_TIME],			--发送时间
				[OUTPUT_TYPE],			--出库类型
				[TRAN_TIME],			--入库时间
				[OUTPUT_REASON],		--出库原因
				[BOOK_KEEPER],			--收货员
				[CONFIRM_FLAG],			--确认标志
				[PLAN_NO],				--零件号
				[ASN_NO],				--ASN编号
				[RUNSHEET_NO],			--拉动单号
				[ASSEMBLY_LINE],		--流水线
				[PLANT_ZONE],			--厂区
				[WORKSHOP],				--车间
				[TRANS_SUPPLIER_NUM],	--运输供应商
				[PART_TYPE],			--零件类型
				[SUPPLIER_TYPE],		--供应商类型
				[RUNSHEET_CODE],		--单据号码
				[ERP_FLAG],				--标志
				[LOGICAL_PK],			--本地主键
				[BUSINESS_PK],			--业务主键
				[COMMENTS],				--备注
				[CREATE_DATE],
				[CREATE_USER],
				[ROUTE],
				[REQUEST_TIME]
			)
			SELECT 
				A.[SPS_RUNSHEET_NO] AS [OUTPUT_NO],
				A.[PLANT],
				A.[SUPPLIER_NUM],
				B.[WM_NO],
				A.[SUPPLIER_NUM] AS [ZONE_NO],
				GETDATE() AS [SEND_TIME],
				1 AS [OUTPUT_TYPE],
				NULL AS [TRAN_TIME],
				NULL AS [OUTPUT_REASON],
				NULL AS [BOOK_KEEPER],
				1 AS [CONFIRM_FLAG],
				NULL AS [PLAN_NO],
				NULL AS [ASN_NO],
				A.[SPS_RUNSHEET_NO] AS [RUNSHEET_NO],
				A.[ASSEMBLY_LINE],
				A.[PLANT_ZONE],
				A.[WORKSHOP],
				A.[TRANS_SUPPLIER_NUM],
				NULL AS	[PART_TYPE],
				NULL AS [SUPPLIER_TYPE],
				'SPS' AS [RUNSHEET_CODE],
				NULL AS [ERP_FLAG],
				NULL AS [LOGICAL_PK],
				NULL AS [BUSINESS_PK],
				A.[COMMENTS],
				GETDATE() AS [CREATE_DATE],
				A.[CREATE_USER],
				'' AS [ROUTE],
				A.[EXPECTED_ARRIVAL_TIME]
			FROM [LES].[TT_SPS_RUNSHEET] A WITH (NOLOCK)
			INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
			INNER JOIN [LES].[TM_SPS_BOX_PARTS] C WITH (NOLOCK) ON C.[PLANT] = A.[PLANT] AND C.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND C.[BOX_PARTS] = A.[BOX_PARTS]
			WHERE A.[SPS_RUNSHEET_SN] = @RunsheetSn

			SELECT @OutPutId = SCOPE_IDENTITY()

			--判断出库主表插入数据后的最新自增id号是否大于0
			IF @OutPutId > 0
				BEGIN		
					-------插入数据到出库明细表--------
					INSERT INTO [LES].[TT_WMM_OUTPUT_DETAIL]
					( 
						[OUTPUT_ID],				--出库主表ID
						[PLANT],					--工厂
						[SUPPLIER_NUM],				--供应商
						[WM_NO],					--仓库编码
						[ZONE_NO],					--存储区编码
						[DLOC],						--库位
						[TRAN_NO],					--交易编码
						[TARGET_WM],				--目的仓库
						[TARGET_ZONE],				--目的存储区
						[TARGET_DLOC],				--目的库位
						[PART_NO],					--零件号
						[PART_CNAME],				--零件中文名
						[PACK_COUNT],				--包装数
						[REQUIRED_BOX_NUM],			--需求箱数
						[REQUIRED_QTY],				--需求数量
						[ACTUAL_BOX_NUM],			--实际箱数
						[ACTUAL_QTY],				--实际数量
						[PACKAGE_MODEL],			--包装型号
						[BARCODE_DATA],				--条码
						[MEASURING_UNIT_NO],		--单位
						[PACKAGE],					--包装
						[NUM],						--数量
						[BOX_NUM],					--箱数
						[IDENTIFY_PART_NO],			--标识零件号
						[PART_ENAME],				--零件德文名
						[DOCK],						--工厂模型DOCK
						[ASSEMBLY_LINE],			--流水线
						[BOX_PARTS],				--零件类
						[SEQUENCE_NO],				--排序号
						[PICKUP_SEQ_NO],			--捡料顺序号
						[RDC_DLOC],					--供应商库位
						[INHOUSE_PACKAGE],			--上线包装数量
						[INBOUND_PACKAGE_MODEL],	--上线包装型号
						[SUPPLIER_NUM_SHEET],		--基础数据组单_供应商
						[BOX_PARTS_SHEET],			--零件类组单
						[ORDER_NO],					--订单号
						[ITEM_NO],
						[TWD_RUNSHEET_NO],			--拉动单号
						[COMMENTS],					--备注
						[CREATE_DATE],
						[CREATE_USER]
					)
					SELECT 
						@OutPutId AS [OUTPUT_ID],
						A.[PLANT],
						A.[SUPPLIER_NUM],
						B.[WM_NO],
						A.[SUPPLIER_NUM] AS	[ZONE_NO],
						(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] P WITH (NOLOCK) WHERE P.[PART_NO] = A.[PART_NO]
						AND P.[PLANT] = A.[PLANT] AND P.[ZONE_NO] = A.[SUPPLIER_NUM]) AS [DLOC],
						@RunsheetSn AS [TRAN_NO],
						C.[WM_NO] AS [TARGET_WM],
						C.[ZONE_NO] AS [TARGET_ZONE],
						(SELECT TOP 1 [DLOC] FROM [LES].[TM_BAS_PARTS_STOCK] TARGET_P WITH (NOLOCK) WHERE TARGET_P.[PART_NO] = A.[PART_NO]
						AND TARGET_P.[PLANT] = A.[PLANT] AND TARGET_P.[ZONE_NO] = @PLANT_ZONE) AS [TARGET_DLOC],
						A.[PART_NO],
						A.[PART_CNAME],
						A.[PACK_COUNT],
						A.[REQUIRED_INBOUND_PACKAGE] AS	[REQUIRED_BOX_NUM],			--需求箱数
						A.[REQUIRED_INBOUND_PACKAGE_QTY] AS	[REQUIRED_QTY],			--需求数量
						NULL AS	[ACTUAL_BOX_NUM],									--实际箱数
						NULL AS	[ACTUAL_QTY],										--实际数量
						A.[INBOUND_PACKAGE_MODEL] AS [PACKAGE_MODEL],
						'' AS [BARCODE_DATA],
						A.[MEASURING_UNIT_NO],
						A.[INBOUND_PACKAGE] AS [PACKAGE],
						NULL AS	[NUM],
						NULL AS	[BOX_NUM],
						A.[IDENTIFY_PART_NO],
						A.[PART_ENAME],
						A.[DOCK],
						A.[ASSEMBLY_LINE],
						A.[BOX_PARTS],
						A.[SEQUENCE_NO],
						A.[PICKUP_SEQ_NO],
						A.[RDC_DLOC],
						A.[INBOUND_PACKAGE]	AS [INHOUSE_PACKAGE],
						A.[INBOUND_PACKAGE_MODEL],
						'' AS [SUPPLIER_NUM_SHEET],
						'' AS [BOX_PARTS_SHEET],
						A.[ORDER_NO],
						'' AS [ITEM_NO],
						@SPS_RUNSHEET_NO AS	[TWD_RUNSHEET_NO],
						'' AS [COMMENTS],
						GETDATE() AS [CREATE_DATE],
						'admin' AS	[CREATE_USER]
					FROM
					(
						SELECT
							A.[PLANT],
							A.[SUPPLIER_NUM],
							A.[PART_NO],
							MAX(A.[PART_CNAME]) AS [PART_CNAME],
							MAX(A.[PART_ENAME]) AS [PART_ENAME],
							MAX(A.[PACK_COUNT]) AS [PACK_COUNT],
							SUM(A.[REQUIRED_INBOUND_PACKAGE]) AS [REQUIRED_INBOUND_PACKAGE],
							SUM(A.[REQUIRED_INBOUND_PACKAGE_QTY]) AS [REQUIRED_INBOUND_PACKAGE_QTY],
							MAX(A.[INBOUND_PACKAGE_MODEL]) AS [INBOUND_PACKAGE_MODEL],
							MAX(A.[MEASURING_UNIT_NO]) AS [MEASURING_UNIT_NO],
							MAX(A.[INBOUND_PACKAGE]) AS [INBOUND_PACKAGE],
							A.[IDENTIFY_PART_NO],
							A.[DOCK],
							A.[ASSEMBLY_LINE],
							A.[BOX_PARTS],
							A.[SEQUENCE_NO],
							A.[PICKUP_SEQ_NO],
							A.[RDC_DLOC],
							B.[ORDER_NO]
						FROM [LES].[TT_SPS_RUNSHEET_DETAIL] A WITH (NOLOCK)
						INNER JOIN [LES].[TT_SPS_RUNSHEET] B WITH (NOLOCK) ON A.[SPS_RUNSHEET_SN] = B.[SPS_RUNSHEET_SN]
						WHERE A.[SPS_RUNSHEET_SN] = @RunsheetSn
						GROUP BY A.[PLANT], A.[SUPPLIER_NUM], A.[PART_NO], A.[IDENTIFY_PART_NO], A.[DOCK], A.[ASSEMBLY_LINE], A.[BOX_PARTS], A.[SEQUENCE_NO], A.[PICKUP_SEQ_NO], A.[RDC_DLOC], B.[ORDER_NO]
					) AS A
					INNER JOIN [LES].[TM_WMM_ZONES] B WITH (NOLOCK) ON B.[PLANT] = A.[PLANT] AND B.[ZONE_NO] = A.[SUPPLIER_NUM]
					INNER JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON C.[ZONE_NO] = @PLANT_ZONE AND C.[PLANT] = A.[PLANT]
					ORDER BY A.[PART_NO]
				END
			------------------------------------------SPS--end-----------------------------------------------------------------
		END
END