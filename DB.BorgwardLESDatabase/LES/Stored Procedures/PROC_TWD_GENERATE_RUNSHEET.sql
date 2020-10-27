/********************************************************************/
/*   Project Name:  TWD												*/
/*   Program Name:  [LES].[PROC_TWD_GENERATE_RUNSHEET]	 			*/
/*   Called By:     window service									*/
/*   Author:        孙述霄											*/
/*   Create date:	2017-05-18										*/
/*   Note:			TWD 生成拉动单									*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_GENERATE_RUNSHEET]
(
	@plant NVARCHAR(5),
	@assemblyLine NVARCHAR(10),
	@supplierNum NVARCHAR(12),
	@boxParts NVARCHAR(10),
	@sendTime DATETIME,
	@windowTime DATETIME,
	@timeand NVARCHAR(16)              -----TIME_AND
	
)
AS
BEGIN
	DECLARE @dockName NVARCHAR(10)			--DOCK名称
	DECLARE @BoxState INT					--零件状态
	DECLARE @plant_zone NVARCHAR(10)		--厂区
	DECLARE @suggestTime DATETIME			--建议发货时间
	DECLARE @pickTime DATETIME				--拣选时间
	DECLARE @wmsState INT					--WMS发送状态
	DECLARE @now DATETIME					--当前时间
	DECLARE @isorganizesheet INT			--是否组织拉动单
	DECLARE @boxPartsName NVARCHAR(100)		--零件类名称
    DECLARE @dock NVARCHAR(10)				--DOCK
    DECLARE @transSupplierNum NVARCHAR(20)	--运输供应商
    DECLARE @WAREHOUSE NVARCHAR(20)			--仓库
    DECLARE @transportTime INT				--运输时间
	DECLARE @unloadtime INT					--卸货时间
	DECLARE @suppliertype INT				--供应商类型
	DECLARE @isasn INT						--是否ASN
	DECLARE @istray INT						--是否按套组托
	DECLARE @ALIAS NVARCHAR(10)				--别名
	DECLARE @dcppoint NVARCHAR(15)
	DECLARE @ispointsnumber BIT				---是否记录过点车辆数量
	SET @now = GETDATE()

	BEGIN TRY
		BEGIN TRANSACTION
    		--找到供应商对应的零件类
    		SELECT
				@boxPartsName = [BOX_PARTS_NAME],					--零件类名称
				@dock = [DOCK],										--DOCK
				@transSupplierNum = [TRANS_SUPPLIER_NUM],			--运输供应商
				@WAREHOUSE = [WAREHOUSE],							--仓库
				@transportTime = [TRANSPORT_TIME],					--运输时间
				@unloadtime = [UNLOAD_TIME],						--卸货时间
				@BoxState = [BOX_PARTS_STATE],						--零件状态
				@isorganizesheet = ISNULL([IS_ORGANIZE_SHEET], 0),	--是否组织拉动单
				@istray = [IS_TRAY]	,								--是否按套组托
				@dcppoint = [DCP_POINT]  --点
			    
			FROM [LES].[TM_TWD_BOX_PARTS] WITH (NOLOCK)

    		WHERE PLANT = @plant AND ASSEMBLY_LINE = @assemblyLine AND SUPPLIER_NUM = @supplierNum AND BOX_PARTS = @boxParts
			AND BOX_PARTS_STATE <> 3  --1-活动，2-测试，3-停用

			SELECT @suppliertype = [SUPPLIER_TYPE], @isasn=(CASE WHEN [ASN_FLAG] = 0 OR [ASN_FLAG] IS NULL THEN 0 ELSE 1 END) FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK) WHERE [SUPPLIER_NUM] = @supplierNum
			IF @suppliertype = 2
				BEGIN
					SET @isasn = 0
					SET @istray = 0
				END

			--插入消耗
			EXEC [LES].[PROC_TWD_UPDATE_COUNTER_INSERT_CONSUME] @plant, @assemblyLine, @boxParts, @supplierNum, @dock

			--获取DOCK名称
			SELECT @dockName=[DOCK_NAME] FROM [LES].[TM_BAS_DOCK] WITH (NOLOCK) WHERE PLANT = @plant AND ASSEMBLY_LINE = @assemblyLine AND [DOCK] = @dock

			DECLARE @consumeCount INT 
			SET @consumeCount = 0
    		SELECT
				@consumeCount = count(1)
			FROM [LES].[TI_TWD_MATERIAL_CONSUME] A WITH (NOLOCK)  ---业务_TWD 物料消耗接口表  
			INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] D WITH (NOLOCK)      --   基础数据_零件信息_拉动表 
			ON D.[PLANT] = A.[PLANT] AND D.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND D.[INHOUSE_SYSTEM_MODE] = 'TWD'
			AND D.[PART_NO] = A.[PART_NO] AND D.[INHOUSE_PART_CLASS] = A.[BOX_PARTS]    ---上线零件类 
    		WHERE A.[IS_ORGANIZE_SHEET] = 2 ---是否组织拉动单 
			AND A.[INTERFACE_TYPE] <> 3 --3为手工拉动，不用处理
			AND (A.[SEND_STATUS] = 0 OR A.[SEND_STATUS] IS NULL) ---发送状态是零或空
    		AND A.[BOX_PARTS] = @boxParts  ---零件类
    		AND A.[PLANT] = @plant
			AND D.[SUPPLIER_NUM] = @supplierNum
    		AND A.[ASSEMBLY_LINE] = @assemblyLine
			AND (A.[INHOUSE_PACKAGE_MODEL] IS NULL OR LEN(A.[INHOUSE_PACKAGE_MODEL]) < 1) --add by caodaowei for 排除组单零件类  ---上线包装型号 

			DECLARE @runsheetNo NVARCHAR(30)
			DECLARE @BlanBill NVARCHAR(10)   --
			--plant_zone借用存放目的存储区。
			SELECT TOP 1 @plant_zone=[PLANT_ZONE] FROM [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] WITH (NOLOCK) WHERE [INHOUSE_PART_CLASS] = @boxParts AND [SUPPLIER_NUM] = @supplierNum AND [INHOUSE_SYSTEM_MODE] = 'TWD'
			--获取是否生成空白拉动单数量
            SELECT  @BlanBill = [PARAMETER_VALUE]
            FROM    [LES].[TS_SYS_CONFIG] WITH ( NOLOCK )
            WHERE   PARAMETER_NAME = 'IsGenerateTWDBlankBill'
   ----系统_参数配置表 


			----Add 记录时区计数器数据
    
                    IF NOT EXISTS ( SELECT  *
                                    FROM    [TT_TWD_TIMEZONE_COUNTER]
                                    WHERE   [CREATE_DATE] = @windowTime
                                            AND [PLANT_ZONE] = @plant_zone AND DCP_POINT=@dcppoint AND  VALIDITY_FLAG = 1)
BEGIN
INSERT INTO [LES].[TT_TWD_TIMEZONE_COUNTER_TEMP1] SELECT [QUANTITY] dcppoint ,1 plant,1 assemblyline FROM [LES].[TT_TWD_TIMEZONE_COUNTER_TEMP]
IF	EXISTS(SELECT * FROM [LES].[TT_TWD_TIMEZONE_COUNTER_TEMP] WHERE   VALIDITY_FLAG = 1
                                        AND PLANT = @plant
                                        AND ASSEMBLY_LINE = @assemblyline AND DCP_POINT=@dcppoint AND [QUANTITY]<>0)
                        INSERT  INTO [LES].[TT_TWD_TIMEZONE_COUNTER]
                                ( [PLANT] ,
                                  [ASSEMBLY_LINE] ,
                                  [DCP_POINT] ,
                                  [PLANT_ZONE] ,
                                  [TIME_AND] ,
                                  [QUANTITY] ,
                                  [WINDOW_TIME] ,
                                  [VALIDITY_FLAG] ,
                                  [CREATE_DATE] 
                                  
								)
                                SELECT  [PLANT] ,
                                        [ASSEMBLY_LINE] ,
                                        [DCP_POINT] ,
                                        @plant_zone ,
                                        @timeand ,
                                        [QUANTITY] ,
                                        @windowTime,
										[VALIDITY_FLAG],
                                        GETDATE() 
                                        
                                FROM    [LES].[TT_TWD_TIMEZONE_COUNTER_TEMP]
                                WHERE   VALIDITY_FLAG = 1
                                        AND PLANT = @plant
                                        AND ASSEMBLY_LINE = @assemblyline AND DCP_POINT=@dcppoint AND [QUANTITY]<>0
			
         END      

            UPDATE  [LES].[TT_TWD_TIMEZONE_COUNTER_TEMP] WITH ( ROWLOCK )
            SET     QUANTITY = 0 ,
                    UPDATE_DATE = GETDATE()
            WHERE   PLANT = @plant
                    AND ASSEMBLY_LINE = @assemblyline
                    AND DCP_POINT = @dcppoint
                    AND VALIDITY_FLAG = 1
INSERT INTO [LES].[TT_TWD_TIMEZONE_COUNTER_TEMP1] SELECT @dcppoint dcppoint ,@plant plant,@assemblyline assemblyline   ---测试语句
			-------

            SET @wmsState = @BoxState
            DECLARE @runsheetId INT
			DECLARE @runSheetType INT 
			IF(@consumeCount > 0)
				BEGIN
					SET @runSheetType = 1		--正常拉动

					--获取拉动单号
					SET @ALIAS = 'T'
					IF @istray = 1
						BEGIN
							SET @ALIAS = 'S'
						END
					EXEC [LES].[PROC_TWD_GET_RUNSHEET_NO] @plant, @assemblyLine, @supplierNum, @runsheetNo OUTPUT, @ALIAS
					SELECT @suggestTime = [LES].[FN_GET_TWD_SENDTIME](@plant, @assemblyLine, @windowTime, ISNULL(@transportTime, 0) + ISNULL(@unloadtime, 0))  --建议发货时间和时区有没有关系？


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
    					[GENERATE_TYPE],
						[IS_ASN],
						[IS_TRAY],
						[TIME_AND]
						
					)
    				VALUES
    				(
    					@runsheetNo,			--<TWD_RUNSHEET_NO, varchar(18),>							
    					@plant,					--<PLANT, nvarchar(5),>
    					@assemblyLine,			--<ASSEMBLY_LINE, nvarchar(10),>
    					NULL,					--<WORKSHOP, nvarchar(4),>
    					@plant_zone,			--<PLANT_ZONE, nvarchar(10),>
    					GETDATE(),				--<PUBLISH_TIME, datetime,>
    					@runSheetType,			--<RUNSHEET_TYPE, int,>
    					@supplierNum,			--<SUPPLIER_NUM, nvarchar(12),>
    					0,						--尚不明确含义<SUPPLIER_SN, int,>
    					@dock,					--<DOCK, nvarchar(10),>
    					@WAREHOUSE,				--<DELIVERY_LOCATION, nvarchar(50),>  ----发货地点
    					@boxParts,				--<BOX_PARTS, nvarchar(10),>
    					0,						--尚不明确含义<PART_TYPE, int,>
    					NULL,					--<UNLOADING_TIME, int,>
    					@windowTime,			--<EXPECTED_ARRIVAL_TIME, datetime,>
    					@suggestTime,			--需求尚未确定<SUGGEST_DELIVERY_TIME, datetime,>
    					NULL,					--<ACTUAL_ARRIVAL_TIME, datetime,>
    					NULL,					--<VERIFY_TIME, datetime,>
    					NULL,					--<REJECT_REASON, nvarchar(200),>
    					@transSupplierNum,		--<TRANS_SUPPLIER_NUM, nvarchar(8),>
    					NULL,					--<FEEDBACK, nvarchar(100),>
    					0,						--<SHEET_STATUS, int,>
    					GETDATE(),				--<SEND_TIME, datetime,>
    					@BoxState,				--<SEND_STATUS, int,>
    					NULL,					--<OPERATON_USER, nvarchar(10),>
    					NULL,					--<CHECK_USER, nvarchar(10),>
    					0,						--<RETRY_TIMES, int,>
    					NULL,					--<SUPPLY_TIME, datetime,>
    					0,						--<SUPPLY_STATUS, int,>
    					NULL,					--<FAX_TIME, datetime,>
    					0,						--<FAX_STATUS, int,>
    					0,						--<SAP_FLAG, int,>
    					0,						---<SAP_FLAG2, int,>
    					NULL,					--<RECKONING_NO, nvarchar(30),>
    					NULL,					--<WMS_SEND_TIME, datetime,>
    					@wmsState,				--<WMS_SEND_STATUS, int,>
    					'',						--<COMMENTS, nvarchar(200),>
    					NULL,					--<UPDATE_DATE, datetime,>
    					NULL,					--<UPDATE_USER, nvarchar(50),>
    					GETDATE(),				--<CREATE_DATE, datetime,>
    					'TWD RUNSHEET ENGINE',	--<CREATE_USER, nvarchar(50),>
    					@isorganizesheet,
						@isasn,
						@istray,
						@timeand
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
    					[SUPPLIER_NUM_SHEET],
    					[BOX_PARTS_SHEET],
						[PRINT_TIMES],
						[PRINT_STATE],
						[PRINT_SUPPLEMENT]
					)
    				SELECT
    					@runsheetId,					--(<TWD_RUNSHEET_SN, int,>
    					@plant,							--<PLANT, nvarchar(5),>
    					@assemblyLine,					--<ASSEMBLY_LINE, nvarchar(10),>
    					@supplierNum,					--<SUPPLIER_NUM, nvarchar(12),>
    					A.[PART_NO],					--, nvarchar(20),>
    					A.[INDENTIFY_PART_NO],			--, nvarchar(20),>
    					D.[PART_CNAME],					--, nvarchar(100),>
    					D.[PART_ENAME],					--, nvarchar(100),>
    					ISNULL(@dock,''),				--<DOCK, nvarchar(10),>
    					@boxParts,						--<BOX_PARTS, nvarchar(10),>
    					0,								--<SEQUENCE_NO, int,>
    					0,								--<PICKUP_SEQ_NO, int,>
    					NULL,							--<RDC_DLOC, varchar(20),>
    					D.[INBOUND_PACKAGE],			--, int,>
    					A.[MEASURING_UNIT_NO],			--, nvarchar(1),>
    					D.[INBOUND_PACKAGE_MODEL],		--, nvarchar(30),>
    					SUM([PACK_COUNT]),																	--, int,> 向上取整 取整包装对应件数
    					(SUM([PACK_COUNT])+ISNULL(D.[INBOUND_PACKAGE],1)-1)/ISNULL(D.[INBOUND_PACKAGE],1),	--<REQUIRED_INBOUND_PACKAGE, int,>
    					SUM([PACK_COUNT]),																	--<REQUIRED_INBOUND_PACKAGE_QTY, int,> 向上取整 取整包装对应件数
    					0,								--<ACTUAL_INBOUND_PACKAGE, int,>
    					0,								--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
    					NULL,							--<BARCODE_DATA, nvarchar(50),>
    					NULL,							--<COMMENTS, nvarchar(200),>)
    					A.[SUPPLIER_NUM],
    					A.[BOX_PARTS],
						0,								--<[PRINT_TIMES], INT>
						0,								--<[PRINT_STATE], INT>
						0								--<[PRINT_SUPPLEMENT], INT>
    				FROM [LES].[TI_TWD_MATERIAL_CONSUME] A WITH (NOLOCK)
					INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] D WITH (NOLOCK)
					ON D.[PLANT] = A.[PLANT] AND D.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND D.[INHOUSE_SYSTEM_MODE] = 'TWD'
					AND D.[PART_NO] = A.[PART_NO] AND D.[INHOUSE_PART_CLASS] = A.[BOX_PARTS] AND ISNULL([DELETE_FLAG],0) = 0
    				WHERE A.[IS_ORGANIZE_SHEET] = 2 AND A.[INTERFACE_TYPE] <> 3 --3为手工拉动，不用处理
    				AND (A.[SEND_STATUS] = 0 OR A.[SEND_STATUS] IS NULL)
					AND A.[BOX_PARTS]= @boxParts
    				AND A.[PLANT] = @plant
					AND D.[SUPPLIER_NUM] = @supplierNum
    				AND A.[ASSEMBLY_LINE] = @assemblyLine
					AND (A.[INHOUSE_PACKAGE_MODEL] IS NULL OR LEN(A.[INHOUSE_PACKAGE_MODEL]) < 1)
    				GROUP BY A.PART_NO,A.INDENTIFY_PART_NO,D.PART_CNAME,D.PART_ENAME,D.INBOUND_PACKAGE,A.MEASURING_UNIT_NO,D.INBOUND_PACKAGE_MODEL,A.SUPPLIER_NUM,A.BOX_PARTS --相同零件多个需求合并到一个明细中

					--设置需求表中状态
    				UPDATE A 
    				SET A.[IS_ORGANIZE_SHEET] = 1
					FROM [LES].[TI_TWD_MATERIAL_CONSUME] A WITH (ROWLOCK)
					INNER JOIN [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] D WITH (NOLOCK)
					ON D.[PLANT] = A.[PLANT] AND D.[ASSEMBLY_LINE] = A.[ASSEMBLY_LINE] AND D.[INHOUSE_SYSTEM_MODE] = 'TWD'
					AND D.[PART_NO] = A.[PART_NO] AND D.[INHOUSE_PART_CLASS] = A.[BOX_PARTS]
    				WHERE A.[IS_ORGANIZE_SHEET] = 2 AND A.[INTERFACE_TYPE] <> 3 --3为手工拉动，不用处理
					AND (A.[SEND_STATUS] = 0 OR A.[SEND_STATUS] IS NULL)
    				AND A.[BOX_PARTS] = @boxParts
    				AND A.[PLANT] = @plant
					AND D.[SUPPLIER_NUM] = @supplierNum
    				AND A.[ASSEMBLY_LINE] = @assemblyLine
					AND (A.[INHOUSE_PACKAGE_MODEL] IS NULL OR LEN(A.[INHOUSE_PACKAGE_MODEL]) < 1) --add by caodaowei for 排除组单零件类

					IF @suppliertype = 1 AND @istray = 0 AND @isasn = 0
						BEGIN
							--生成箱条码
							EXEC [LES].[PROC_TWD_GENERATE_BARCODE] @runsheetId
						END

					IF @suppliertype = 1 AND @istray = 1
						BEGIN
							--生成托标签
							EXEC [LES].[PROC_TWD_GENERATE_TRAYNO] @runsheetId
						END

					--创建仓库出库单
					EXEC [LES].[PROC_INSERT_OUTPUT] @runsheetId, 'TWD'

					--投递二级拉动 Andy Liu 2015-12-14
					EXEC [LES].[PROC_TWD_GEN_TWICEPULL_DATA] @runsheetId, 'TWD'
				END
			ELSE
				BEGIN
					IF @BlanBill <> '0'
						BEGIN
							--生成空白拉动单
							SET @wmsState = 4			--发送失败
							SET @runSheetType = 3		--空白拉动

							--获取拉动单号
							EXEC [LES].[PROC_TWD_GET_RUNSHEET_NO] @plant, @assemblyLine ,@supplierNum, @runsheetNo OUTPUT,'T'
							SELECT @suggestTime = [LES].[FN_GET_TWD_SENDTIME](@plant, @assemblyLine, @windowTime, ISNULL(@transportTime, 0) + ISNULL(@unloadtime, 0))

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
    							[GENERATE_TYPE]
							)
    						VALUES
    						(
    							@runsheetNo,			--<TWD_RUNSHEET_NO, varchar(18),>
    							@plant,					--<PLANT, nvarchar(5),>
    							@assemblyLine,			--<ASSEMBLY_LINE, nvarchar(10),>
    							NULL,					--<WORKSHOP, nvarchar(4),>
    							@plant_zone,			--<PLANT_ZONE, nvarchar(10),>
    							GETDATE(),				--<PUBLISH_TIME, datetime,>
    							@runSheetType,			--<RUNSHEET_TYPE, int,>
    							@supplierNum,			--<SUPPLIER_NUM, nvarchar(12),>
    							0,						--尚不明确含义<SUPPLIER_SN, int,>
    							@dock,					--<DOCK, nvarchar(10),>
    							@WAREHOUSE,				--<DELIVERY_LOCATION, nvarchar(50),>
    							@boxParts,				--<BOX_PARTS, nvarchar(10),>
    							0,						--尚不明确含义<PART_TYPE, int,>
    							NULL,					--<UNLOADING_TIME, int,>
    							@windowTime,			--<EXPECTED_ARRIVAL_TIME, datetime,>
    							@suggestTime,			--需求尚未确定<SUGGEST_DELIVERY_TIME, datetime,>
    							NULL,					--<ACTUAL_ARRIVAL_TIME, datetime,>
    							NULL,					--<VERIFY_TIME, datetime,>
    							NULL,					--<REJECT_REASON, nvarchar(200),>
    							@transSupplierNum,		--<TRANS_SUPPLIER_NUM, nvarchar(8),>
    							NULL,					--<FEEDBACK, nvarchar(100),>
    							0,						--<SHEET_STATUS, int,>
    							GETDATE(),				--<SEND_TIME, datetime,>
    							@BoxState,				--<SEND_STATUS, int,>
    							NULL,					--<OPERATON_USER, nvarchar(10),>
    							NULL,					--<CHECK_USER, nvarchar(10),>
    							0,						--<RETRY_TIMES, int,>
    							NULL,					--<SUPPLY_TIME, datetime,>
    							0,						--<SUPPLY_STATUS, int,>
    							NULL,					--<FAX_TIME, datetime,>
    							0,						--<FAX_STATUS, int,>
    							0,						--<SAP_FLAG, int,>
    							0,						---<SAP_FLAG2, int,>
    							NULL,					--<RECKONING_NO, nvarchar(30),>
    							NULL,					--<WMS_SEND_TIME, datetime,>
    							@wmsState,				--<WMS_SEND_STATUS, int,>
    							'',						--<COMMENTS, nvarchar(200),>
    							NULL,					--<UPDATE_DATE, datetime,>
    							NULL,					--<UPDATE_USER, nvarchar(50),>
    							GETDATE(),				--<CREATE_DATE, datetime,>
    							'TWD RUNSHEET ENGINE',	--<CREATE_USER, nvarchar(50),>
    							@isorganizesheet
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
								[PRINT_TIMES],
								[PRINT_STATE],
								[PRINT_SUPPLEMENT]
							)
    						VALUES
							(
    							@runsheetId,				--(<TWD_RUNSHEET_SN, int,>
    							@plant,						--<PLANT, nvarchar(5),>
    							@assemblyLine,				--<ASSEMBLY_LINE, nvarchar(10),>
    							@supplierNum,				--<SUPPLIER_NUM, nvarchar(12),>
    							'******',					--, nvarchar(20),>
    							'******',					--, nvarchar(20),>
    							'******',					--, nvarchar(100),>
    							'******',					--, nvarchar(100),>
    							ISNULL(@dockName,''),		--<DOCK, nvarchar(10),>
    							@boxParts,					--<BOX_PARTS, nvarchar(10),>
    							0,							--<SEQUENCE_NO, int,>
    							0,							--<PICKUP_SEQ_NO, int,>
    							NULL,						--<RDC_DLOC, varchar(20),>
    							0,							--INBOUND_PACKAGE--, int,>
    							'1',						--MEASURING_UNIT_NO --, nvarchar(1),>
    							'',							--INBOUND_PACKAGE_MODEL--, nvarchar(30),>
    							0,							--sum(PACK_COUNT)--, int,>
    							0,							--sum(PACK_COUNT/INBOUND_PACKAGE) --<REQUIRED_INBOUND_PACKAGE, int,>
    							0,							--sum(PACK_COUNT) --<REQUIRED_INBOUND_PACKAGE_QTY, int,>
    							0,							--<ACTUAL_INBOUND_PACKAGE, int,>
    							0,							--<ACTUAL_INBOUND_PACKAGE_QTY, int,>
    							NULL,						--<BARCODE_DATA, nvarchar(50),>
    							NULL,						--<COMMENTS, nvarchar(200),>)
								0,							--<[PRINT_TIMES], INT>
								0,							--<[PRINT_STATE], INT>
								0							--<[PRINT_SUPPLEMENT], INT>
    						)
						END
				END

			--设置窗口时间中状态
    		UPDATE [LES].[TT_TWD_SUPPLIER_SENDTIME] WITH (ROWLOCK)
    		SET LAST_SEND_TIME = GETDATE() , SEND_TIME_STATUS = 1
    		WHERE SEND_TIME < @now AND (SEND_TIME_STATUS = 0 OR SEND_TIME_STATUS IS NULL) AND BOX_PARTS = @boxParts
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS],  [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'TWD', 'PROC_TWD_GENERATE_RUNSHEET', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END