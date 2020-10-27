/********************************************************************/
/*   Program Name:  [LES].[PROC_SPM_SAP_LES_RUNSHEET]				*/
/*   Called By:     window service									*/
/*   Modifier:      孙述霄											*/
/*   Modify date:	2017-11-22										*/
/*   Note:			生成交货单										*/
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_SPM_SAP_LES_RUNSHEET]
AS
BEGIN
	SET NOCOUNT ON
	SET XACT_ABORT ON

	DECLARE @VBELN NVARCHAR(12)
	DECLARE @POSNR NUMERIC(18,3)
	DECLARE @LIFNR NVARCHAR(10)
	DECLARE @LFDAT NVARCHAR(10)
	DECLARE @LFUHR NVARCHAR(10)
	DECLARE @WERKS NVARCHAR(5)
	DECLARE @EMLIF NVARCHAR(10)
	DECLARE @ZDOCK NVARCHAR(10)
	DECLARE @DISPO NVARCHAR(20)
	DECLARE @MATNR NVARCHAR(18)
	DECLARE @STRAS NVARCHAR(100)
	DECLARE @LGMNG NUMERIC(18,3)
	DECLARE @MEINS NVARCHAR(8)
	DECLARE @LGORT NVARCHAR(18)
	DECLARE @MAKTX_ZH NVARCHAR(40)
	DECLARE @LFART NVARCHAR(4)
	DECLARE @BSTRF NUMERIC(18,3)
	DECLARE @LESTYPE NVARCHAR(1)
	DECLARE @ZVERSION NVARCHAR(4)
	DECLARE @EXPECTED_ARRIVAL_TIME DATETIME
	DECLARE @SHEET_STATUS INT
	DECLARE @IS_ASN INT
	DECLARE @IS_CJS INT

	DECLARE @ProcessVBELNTable TABLE
	(
		[MAX_SEQ_ID] BIGINT,
		[MIN_SEQ_ID] BIGINT,
		[VBELN] NVARCHAR(20)
	)
	DECLARE @MAX_SEQ_ID AS BIGINT
	DECLARE @MIN_SEQ_ID AS BIGINT

	BEGIN TRAN
	BEGIN TRY
		--由于订单批量下发,处理时间较长3000条数据约30分钟,当此时接口继续接收新数据时会发生死锁
		--经过优化，处理3000条数据约8分钟,
		--再次优化，处理3000条数据约2分30秒
		--目前调整为每次处理约500个订单
		INSERT INTO @ProcessVBELNTable
		(
			[MAX_SEQ_ID],
			[MIN_SEQ_ID],
			[VBELN]
		)
		SELECT TOP 500
			[MAX_SEQ_ID],
			[MIN_SEQ_ID],
			[VBELN]
		FROM
		(
			SELECT
				[MAX_SEQ_ID] = MAX([SEQ_ID]),
				[MIN_SEQ_ID] = MIN([SEQ_ID]),
				[VBELN]
			FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (NOLOCK)
			WHERE ISNULL([PROCESS_FLAG], 0) = 0 --未处理的数据
			AND (ISNULL([LESTYPE], '') = '' OR [LESTYPE] = '1' OR [LESTYPE] = '2' OR [LESTYPE] = '5' OR [LESTYPE] = '6')
			GROUP BY [VBELN], ISNULL([LIFNR], ''), ISNULL([ZDOCK], '')
		) A
		ORDER BY [MAX_SEQ_ID] ASC

		--如果存在待处理数据,则进行处理,否则提交事务,退出
		IF (EXISTS(SELECT TOP 1 * FROM @ProcessVBELNTable))
			BEGIN
				--找到要处理数据的ID区间
				SET @MIN_SEQ_ID=(SELECT MIN([MIN_SEQ_ID]) FROM @ProcessVBELNTable)
				SET @MAX_SEQ_ID=(SELECT MAX([MAX_SEQ_ID]) FROM @ProcessVBELNTable)

				--更新处理状态
				UPDATE [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (ROWLOCK)
				SET [PROCESS_FLAG] = 0 
				WHERE [VBELN] IN (SELECT [VBELN] FROM @ProcessVBELNTable)
				AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID

				--更新包装数
				UPDATE [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (ROWLOCK)
				SET [BSTRF] = 0
				WHERE [BSTRF] IS NULL
				AND [VBELN] IN (SELECT [VBELN] FROM @ProcessVBELNTable)
				AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID

				--处理器具报警信息
				DECLARE @event_detail NVARCHAR(4000)
				SET @event_detail = '';
				SELECT
					@event_detail = @event_detail + '订单号：' + [VBELN] + ',器具号：' + [ZYSQJC] + '在器具库存信息中不存在;' 
				FROM
				(
					SELECT DISTINCT TOP 50
						[VBELN],
						[ZYSQJC]
					FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (NOLOCK) 
					WHERE [VBELN] IN (SELECT [VBELN] FROM @ProcessVBELNTable)
					AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
					AND [ZYSQJC] NOT IN (SELECT [PACKAGE_NO] from [LES].[TM_RPM_PACKAGE_STOCKS])
				) A

				IF @event_detail <> ''
					BEGIN
						INSERT INTO [LES].[TL_SYS_EVENT_LOG]
						(
							[EVENT_TIME],
							[EVENT_ID],
							[EVENT_SOURCE],
							[EVENT_STATE],
							[EVENT_TYPE],
							[EVENT_LEVEL],
							[EVENT_DETAIL]
						)
						VALUES
						(
							GETDATE(),
							301,
							'SAP下发订单数据错误',
							0,
							200,
							2,
							@event_detail
						)
					END

				--处理供应商报警
				SET @event_detail = '';
				SELECT
					@event_detail = @event_detail + '订单号：' + [VBELN] + ',供应商：' + [LIFNR] + '在用户信息中不存在;'
				FROM
				(
					SELECT DISTINCT TOP 50
						[VBELN],
						[LIFNR]
					FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (NOLOCK) 
					WHERE [VBELN] IN (SELECT [VBELN] FROM @ProcessVBELNTable)
					AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
					AND [LIFNR] NOT IN (SELECT [USER_LOGIN_NAME] FROM [LES].[TS_SYS_USER] WITH (NOLOCK))
				) A

				IF @event_detail <> ''
					BEGIN
						INSERT INTO [LES].[TL_SYS_EVENT_LOG]
						(
							[EVENT_TIME],
							[EVENT_ID],
							[EVENT_SOURCE],
							[EVENT_STATE],
							[EVENT_TYPE],
							[EVENT_LEVEL],
							[EVENT_DETAIL]
						)
						VALUES
						(
							GETDATE(),
							301,
							'SAP下发订单数据错误',
							0,
							200,
							2,
							@event_detail
						)
					END

				--处理零件报警
				SET @event_detail = '';
				SELECT
					@event_detail = @event_detail + '订单号：' + [VBELN] + ',零件号：' + [MATNR] + ',存储区：' + [LGORT] + '在零件仓库信息中不存在;' 
				FROM
				(
					SELECT DISTINCT TOP 50
						[VBELN],
						[MATNR],
						[LGORT]
					FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] B WITH (NOLOCK)
					WHERE [VBELN] IN (SELECT VBELN FROM @ProcessVBELNTable)
					AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
					AND B.[MATNR] NOT IN
					(
						SELECT C.[PART_NO] FROM [LES].[TM_BAS_PARTS_STOCK] C WITH (NOLOCK) WHERE C.[PLANT] = B.[WERKS] AND C.[ZONE_NO] = B.[LGORT]
					)
				) AS A

				IF @event_detail <> ''
					BEGIN
						INSERT INTO [LES].[TL_SYS_EVENT_LOG]
						(
							[EVENT_TIME],
							[EVENT_ID],
							[EVENT_SOURCE],
							[EVENT_STATE],
							[EVENT_TYPE],
							[EVENT_LEVEL],
							[EVENT_DETAIL]
						)
						VALUES
						(
							GETDATE(),
							301,
							'SAP下发订单数据错误',
							0,
							200,
							2,
							@event_detail
						)
					END

				--处理包装数报警
				SET @event_detail = ''
				SELECT
					@event_detail = @event_detail + '订单号：' + [VBELN] + ',零件号：' + [MATNR] + '包装数为0，不能正常组单;'
				FROM
				(
					SELECT DISTINCT TOP 50
						[VBELN],
						[MATNR]
					FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (NOLOCK)
					WHERE [VBELN] IN (SELECT [VBELN] FROM @ProcessVBELNTable)
					AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
					AND [BSTRF] = 0
				) AS A

				IF @event_detail <> ''
					BEGIN
						--针对包装数为0的订单，整个订单都不处理
						UPDATE [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (ROWLOCK)
						SET [PROCESS_FLAG] = 1,
							[PROCESS_TIME] = GETDATE()
						WHERE [BSTRF] = 0
						AND [VBELN] IN (SELECT [VBELN] FROM @ProcessVBELNTable)
						AND [LESTYPE] IN( '1','6') --只适用订单，不适用非共用件--jinmiao20180101
						AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID

						INSERT INTO [LES].[TL_SYS_EVENT_LOG]
						(
							[EVENT_TIME],
							[EVENT_ID],
							[EVENT_SOURCE],
							[EVENT_STATE],
							[EVENT_TYPE],
							[EVENT_LEVEL],
							[EVENT_DETAIL]
						)
						VALUES
						(
							GETDATE(),
							301,
							'SAP下发订单数据错误',
							0,
							200,
							2,
							@event_detail
						)
					END

				--定义游标
				DECLARE RUNSHEET_CUR CURSOR FOR
				SELECT
					[SEQ_ID],
					[VBELN],
					CASE WHEN [POSNR] = '' THEN NULL ELSE POSNR END POSNR,
					[LIFNR],
					[LFUHR],
					[WERKS],
					[EMLIF],
					[ZDOCK],
					[DISPO],
					[LFART],
					[ZVERSION],
					[STRAS],
					[LFDAT],
					[LFUHR],
					[MEINS],
					[LGORT],
					[LESTYPE]
				FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (NOLOCK)
				WHERE [SEQ_ID] IN (SELECT [MAX_SEQ_ID] FROM @ProcessVBELNTable)
				ORDER BY SEQ_ID ASC

				OPEN RUNSHEET_CUR
				FETCH NEXT FROM RUNSHEET_CUR INTO @MAX_SEQ_ID, @VBELN, @POSNR, @LIFNR, @LFUHR, @WERKS, @EMLIF, @ZDOCK, @DISPO, @LFART, @ZVERSION, @STRAS, @LFDAT, @LFUHR, @MEINS, @LGORT, @LESTYPE
				WHILE( @@FETCH_STATUS = 0 )
					BEGIN
						--PRINT('开始处理订单' + @VBELN)
						--得到本次游标处理的最小ID
						SELECT @MIN_SEQ_ID = [MIN_SEQ_ID] FROM @ProcessVBELNTable WHERE [MAX_SEQ_ID] = @MAX_SEQ_ID

						DECLARE @recordenum INT--记录数
						DECLARE @InsertId BIGINT
						SELECT @recordenum = COUNT(*) FROM [LES].[TT_SPM_DELIVERY_RUNSHEET] WITH (NOLOCK) WHERE [PLAN_RUNSHEET_NO] = @VBELN AND ISNULL([SUPPLIER_NUM], '') = ISNULL(@LIFNR, '') AND ISNULL(DOCK, '') = ISNULL(@ZDOCK, '')
						DECLARE @s VARCHAR(30)
						SET @s = @LFDAT + @LFUHR
						DECLARE @Status INT
						SET @EXPECTED_ARRIVAL_TIME = LEFT(@s, 4) + '-' + SUBSTRING(@s,5,2) + '-' + SUBSTRING(@s,7,2) + ' ' + SUBSTRING(@s,9,2) + ':' + SUBSTRING(@s,11,2) + ':' + SUBSTRING(@s,13,2)
						--PRINT CONVERT(varchar(20),@EXPECTED_ARRIVAL_TIME,120)

						SET @IS_CJS = 0
						SET @SHEET_STATUS = 1
						IF(@LFART = 'EL')
							SET @Status = -1
						ELSE IF(@LFART = 'ZCRS')
							SET @Status = -2
						ELSE IF(@LFART = 'YRTN')
							SET @Status = -3
						ELSE IF(@LFART = 'YURG')
							SET @Status = -4
						ELSE IF(@LFART = 'ZOVS')
							SET @Status = -5
						ELSE IF(@LFART = 'SRTN')
								SET  @Status = -11
						ELSE IF(@LFART = 'SURG')
								SET  @Status = -12
						ELSE IF(@LFART = 'ZSPS')
								SET  @Status = -13
						ELSE IF(@LFART = 'ZPJ1')
								SET  @Status = -14
						ELSE IF(@LFART = 'ZPJR') --以上废弃中
								SET  @Status = -15
						ELSE IF(@LFART = 'EL')--交货单收货
								SET  @Status = -21
						ELSE IF(@LFART = 'RL')--交货单退货
								SET  @Status = -22
						ELSE IF(@LFART = 'NB' AND @LESTYPE <> 5 AND @LESTYPE <> 6)--采购订单收货
								SET  @Status = -31
						ELSE IF(@LFART = 'RB')--采购订单退货
								SET  @Status = -32
						ELSE IF(@LFART = 'NB' AND @LESTYPE = 5)--寄售订单，初始状态为已确认
							BEGIN
								SET @Status = -50
								SET @SHEET_STATUS = 15
								IF EXISTS
								(
									SELECT TOP 1
										1
									FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] A WITH (NOLOCK)
									INNER JOIN [LES].[TI_SPM_DELIVERY_EXCESS_OUT] D WITH (NOLOCK) ON D.[Z_SERIAL] = A.[Z_SERIAL]
									WHERE A.[VBELN] = @VBELN
									AND ISNULL(A.[LIFNR], '') = ISNULL(@LIFNR, '')
									AND ISNULL(A.[ZDOCK], '') = ISNULL(@ZDOCK, '')
									AND A.[PROCESS_FLAG] = 0
									AND A.[SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
								)
									BEGIN
										--超量寄售
										SET @IS_CJS = 1
									END

								
								--处理对应存储区与供应商关系在VMI供应商中存在并标记危机。
								SET @event_detail = '';
								SELECT
									@event_detail = @event_detail + '订单号：' + [VBELN] + '，供应商：' + [LIFNR] + '，存储区：' + @LGORT + '在VMI供应商中不存在或未启用寄售;'
								FROM
								(
									SELECT DISTINCT TOP 1
										[VBELN],
										[LIFNR]
									FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] A WITH (NOLOCK) 
									WHERE [VBELN] = @VBELN
									AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
									AND NOT EXISTS (SELECT 1 FROM [LES].[TM_BAS_VMI_SUPPLIER] B WITH (NOLOCK) WHERE B.[SUPPLIER_NUM] = A.[LIFNR] AND B.[ZONE_NO] = @LGORT AND B.[VMI_FLAG] = 1)
								) A

								IF @event_detail <> ''
									BEGIN
										INSERT INTO [LES].[TL_SYS_EVENT_LOG]
										(
											[EVENT_TIME],
											[EVENT_ID],
											[EVENT_SOURCE],
											[EVENT_STATE],
											[EVENT_TYPE],
											[EVENT_LEVEL],
											[EVENT_DETAIL]
										)
										VALUES
										(
											GETDATE(),
											301,
											'SAP下发订单数据错误',
											0,
											200,
											2,
											@event_detail
										)
									END
							END
						ELSE IF(@LFART ='NB' AND @LESTYPE = 6)--试制订单
							SET @Status = -51
						ELSE 
							SET @Status = -6

						IF @recordenum > 0
							BEGIN
								UPDATE [LES].[TT_SPM_DELIVERY_RUNSHEET] WITH (ROWLOCK)
								SET [PLAN_NO] = @POSNR,
									[EXPECTED_ARRIVAL_TIME] = @EXPECTED_ARRIVAL_TIME,
									[PLANT] = @WERKS,
									[TRANS_SUPPLIER_NUM] = @EMLIF,
									[DOCK] = @ZDOCK,
									[WHAREHOUSE] = @STRAS,
									[RUNSHEET_TYPE] = @Status,
									[SHEET_Done_STATUS] = ISNULL(SHEET_Done_STATUS, 1)
								WHERE [PLAN_RUNSHEET_NO] = @VBELN 
								AND ISNULL([SUPPLIER_NUM], '') = ISNULL(@LIFNR, '')
								AND ISNULL([DOCK], '') = ISNULL(@ZDOCK, '')

								--更新处理状态
								UPDATE [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (ROWLOCK) 
								SET [PROCESS_FLAG] = 1,
									[PROCESS_TIME] = GETDATE()
								WHERE [VBELN] = @VBELN
								AND ISNULL([LIFNR], '') = ISNULL(@LIFNR, '')
								AND ISNULL([ZDOCK], '') = ISNULL(@ZDOCK, '')
								AND [PROCESS_FLAG] = 0
								AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
							END
						ELSE
							BEGIN
								--设置创建ASN单据标识
								SET @IS_ASN = 0
								IF NOT EXISTS (SELECT 1 FROM [LES].[TM_BAS_PLANT] WITH (NOLOCK) WHERE [PLANT] = @WERKS)
									BEGIN
										--非共用件，需要创建ASN
										SET @IS_ASN = 1
									END
								ELSE IF @Status <> -50
									BEGIN
										--不是寄售
										IF EXISTS (SELECT 1 FROM [LES].[TM_BAS_SUPPLIER] WITH (NOLOCK) WHERE [SUPPLIER_NUM] = @LIFNR AND [ASN_FLAG] = 1
										AND @LESTYPE <> 6)--jinmiao20180101
											BEGIN
												SET @IS_ASN = 1
											END
									END
								ELSE
									BEGIN
										--寄售订单
										IF EXISTS (SELECT 1 FROM [LES].[TM_BAS_VMI_SUPPLIER] WITH (NOLOCK) WHERE [SUPPLIER_NUM] = @LIFNR AND [ZONE_NO] = @LGORT AND [ASN_FLAG] = 1)
											BEGIN
												SET @IS_ASN = 1
											END
									END

								--添加主表信息
								INSERT INTO [LES].[TT_SPM_DELIVERY_RUNSHEET]
								(
									[PLAN_RUNSHEET_NO],
									[PLAN_NO],
									[SUPPLIER_NUM],
									[EXPECTED_ARRIVAL_TIME],
									[PLANT],
									[TRANS_SUPPLIER_NUM],
									[DOCK],
									[WHAREHOUSE],
									[PUBLISH_TIME],
									[RUNSHEET_TYPE],
									[SHEET_STATUS],
									[CREATE_DATE],
									[CREATE_USER],
									[PLANT_ZONE],
									[SHEET_Done_STATUS],
									[IS_ASN]
								)
								VALUES
								(
									@VBELN,
									@POSNR,
									@LIFNR,
									@EXPECTED_ARRIVAL_TIME,
									@WERKS,
									@EMLIF,
									@ZDOCK,
									@STRAS,
									GETDATE(),
									@Status,
									@SHEET_STATUS,
									GETDATE(),
									'admin',
									@LGORT,
									1,
									@IS_ASN
								)

								SELECT @InsertId= SCOPE_IDENTITY()
								--添加明细信息		 

								--不是寄售订单
								IF @Status <> -50
									BEGIN
										INSERT INTO [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL]
										(
											[PLAN_RUNSHEET_SN],
											[PLANT],
											[SUPPLIER_NUM],
											[PART_NO],
											[PART_CNAME],
											[DOCK],
											[MEASURING_UNIT_NO],
											[REQUIRED_INHOUSE_PACKAGE],
											[REQUIRED_INHOUSE_PACKAGE_QTY],
											[IDOC],
											[WM_NO],
											[ZONE_NO],
											[DLOC],
											[INHOUSE_PACKAGE_MODEL],
											[INHOUSE_PACKAGE],
											[DISPO],
											[COMMENTS],
											[ITEM_NO]
										)
										SELECT
											@InsertId,
											A.[WERKS],
											A.[LIFNR],
											A.[MATNR],
											A.[MAKTX_ZH],
											A.[ZDOCK],
											A.[MEINS],
											A.[LGMNG],
											CASE WHEN ISNULL(A.[BSTRF], 0) = 0 THEN NULL ELSE CEILING(A.[LGMNG] / (A.[BSTRF] * 1.0)) END,
											'IDOC',
											C.[WM_NO],	--B.WM_NO
											A.[LGORT],	--B.ZONE_NO
											B.[DLOC],
											A.[ZYSQJC],	--B.PACKAGE_MODEL
											A.[BSTRF],
											A.[DISPO],
											A.[COMMENTS],
											A.[POSNR]
										FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] A WITH (NOLOCK)
										LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON B.[PLANT] = A.[WERKS] AND B.[PART_NO] = A.[MATNR] AND B.[ZONE_NO] = A.[LGORT]
										LEFT JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON A.[WERKS] = C.[PLANT] AND A.[LGORT] = C.[ZONE_NO]	--AND C.IS_MANAGE IN (10,60) --= @ZONE_TYPE  收货仓库为10，退货仓库为60
										WHERE A.[VBELN] = @VBELN
										AND ISNULL(A.[LIFNR],'') = ISNULL(@LIFNR,'')
										AND ISNULL(A.[ZDOCK],'') = ISNULL(@ZDOCK,'')
										AND A.[PROCESS_FLAG] = 0
										AND A.[SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
									END
								ELSE
									BEGIN
										IF @IS_CJS = 1
											BEGIN
												--超量寄售订单
												INSERT INTO [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL]
												(
													[PLAN_RUNSHEET_SN],
													[PLANT],
													[SUPPLIER_NUM],
													[PART_NO],
													[PART_CNAME],
													[DOCK],
													[MEASURING_UNIT_NO],
													[REQUIRED_INHOUSE_PACKAGE],
													[REQUIRED_INHOUSE_PACKAGE_QTY],
													[ACTUAL_INHOUSE_PACKAGE],
													[ACTUAL_INHOUSE_PACKAGE_QTY],
													[IDOC],
													[WM_NO],
													[ZONE_NO],
													[DLOC],
													[INHOUSE_PACKAGE_MODEL],
													[INHOUSE_PACKAGE],
													[DISPO],
													[COMMENTS],
													[ITEM_NO],
													[ASN_CONFIRM_QTY]
												)
												SELECT
													@InsertId,
													A.[WERKS],
													A.[LIFNR],
													A.[MATNR],
													A.[MAKTX_ZH],
													A.[ZDOCK],
													A.[MEINS],
													A.[LGMNG],
													CASE WHEN ISNULL(A.[BSTRF],0) = 0 THEN NULL ELSE CEILING(A.[LGMNG] / (A.[BSTRF] * 1.0)) END,
													CASE WHEN ISNULL(A.[BSTRF],0) = 0 THEN NULL ELSE CEILING(D.[MENGE] / (A.[BSTRF] * 1.0)) END,
													D.[MENGE],
													'IDOC',
													C.[WM_NO],	--B.WM_NO
													A.[LGORT],	--B.ZONE_NO
													B.[DLOC],
													A.[ZYSQJC],	--B.PACKAGE_MODEL
													A.[BSTRF],
													A.[DISPO],
													A.[COMMENTS],
													A.[POSNR],
													A.[LGMNG]
												FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] A WITH (NOLOCK)
												LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON B.[PLANT] = A.[WERKS] AND B.[PART_NO] = A.[MATNR] AND B.[ZONE_NO] = A.[LGORT]
												LEFT JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON A.[WERKS] = C.[PLANT] AND A.[LGORT] = C.[ZONE_NO] --AND C.IS_MANAGE IN (10,60) --= @ZONE_TYPE  收货仓库为10，退货仓库为60
												LEFT JOIN [LES].[TI_SPM_DELIVERY_EXCESS_OUT] D WITH (NOLOCK) ON D.[Z_SERIAL] = A.[Z_SERIAL]
												WHERE A.[VBELN] = @VBELN
												AND ISNULL(A.[LIFNR], '') = ISNULL(@LIFNR, '')
												AND ISNULL(A.[ZDOCK], '') = ISNULL(@ZDOCK, '')
												AND A.[PROCESS_FLAG] = 0
												AND A.[SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID

												---回写单据号到ASN单据明细
												UPDATE E
												SET E.[TWD_RUNSHEET_NO] = A.[VBELN]
												FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] A WITH (NOLOCK)
												INNER JOIN [LES].[TT_WMM_VMI_RECEIVE_DETAIL] B WITH (NOLOCK) ON B.[Z_SERIAL] = A.[Z_SERIAL]
												INNER JOIN [LES].[TT_WMM_VMI_RECEIVE] C WITH (NOLOCK) ON C.[RECEIVE_ID] = B.[RECEIVE_ID]
												INNER JOIN [LES].[TT_SPM_RUNSHEET_ASN] D WITH (NOLOCK) ON D.[ASN_NO] = C.[RECEIVE_NO]
												INNER JOIN [LES].[TT_SPM_RUNSHEET_ASN_DETAIL] E WITH (ROWLOCK) ON e.[ASN_ID] = D.[ID] AND E.[PART_NO] = B.[PART_NO]
												WHERE A.[VBELN] = @VBELN
												AND ISNULL(A.[LIFNR], '') = ISNULL(@LIFNR, '')
												AND ISNULL(A.[ZDOCK], '') = ISNULL(@ZDOCK, '')
												AND A.[PROCESS_FLAG] = 0
												AND A.[SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
											END
										ELSE
											BEGIN
												--普通寄售订单
												INSERT INTO [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL]
												(
													[PLAN_RUNSHEET_SN],
													[PLANT],
													[SUPPLIER_NUM],
													[PART_NO],
													[PART_CNAME],
													[DOCK],
													[MEASURING_UNIT_NO],
													[REQUIRED_INHOUSE_PACKAGE],
													[REQUIRED_INHOUSE_PACKAGE_QTY],
													[IDOC],
													[WM_NO],
													[ZONE_NO],
													[DLOC],
													[INHOUSE_PACKAGE_MODEL],
													[INHOUSE_PACKAGE],
													[DISPO],
													[COMMENTS],
													[ITEM_NO]
												)
												SELECT
													@InsertId,
													A.[WERKS],
													A.[LIFNR],
													A.[MATNR],
													A.[MAKTX_ZH],
													A.[ZDOCK],
													A.[MEINS],
													A.[LGMNG],
													CASE WHEN ISNULL(A.[BSTRF],0) = 0 THEN NULL ELSE CEILING(A.[LGMNG] / (A.[BSTRF] * 1.0)) END,
													'IDOC',
													C.[WM_NO],	--B.WM_NO
													A.[LGORT],	--B.ZONE_NO
													B.[DLOC],
													A.[ZYSQJC],	--B.PACKAGE_MODEL
													A.[BSTRF],
													A.[DISPO],
													A.[COMMENTS],
													A.[POSNR]
												FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] A WITH (NOLOCK)
												LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON B.[PLANT] = A.[WERKS] AND B.[PART_NO] = A.[MATNR] AND B.[ZONE_NO] = A.[LGORT]
												LEFT JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON A.[WERKS] = C.[PLANT] AND A.[LGORT] = C.[ZONE_NO]
												WHERE A.[VBELN] = @VBELN
												AND ISNULL(A.[LIFNR], '') = ISNULL(@LIFNR, '')
												AND ISNULL(A.[ZDOCK], '') = ISNULL(@ZDOCK, '')
												AND A.[PROCESS_FLAG] = 0
												AND A.[SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
											END
									END

								--寄售订单判断且没有ASN标识及不是超量寄售订单,直接创建VMI入库单
								IF @Status = -50 AND @IS_ASN = 0 AND @IS_CJS = 0
									BEGIN
										INSERT INTO [LES].[TT_WMM_VMI_RECEIVE]
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
										SELECT TOP 1
											@VBELN,		--[RECEIVE_NO]	
											@WERKS,		--[PLANT]
											@LIFNR,		--[SUPPLIER_NUM]
											C.WM_NO,	--[WM_NO]
											C.ZONE_NO,	--[ZONE_NO]
											NULL,		--[DELIVERY_LOCATION_NO]
											ZDOCK,		--[DOCK]
											GETDATE(),	--[SEND_TIME]
											1,			--[RECEIVE_TYPE]
											NULL,		--[TRAN_TIME]
											NULL,		--[RECEIVE_REASON]
											1,			--[CONFIRM_FLAG]
											NULL,		--[TRANS_SUPPLIER_NUM]
											NULL,		--[PLAN_NO]
											NULL,		--[ASN_NO]
											@VBELN,		--[RUNSHEET_NO]
											NULL,		--[ASSEMBLY_LINE]
											C.ZONE_NO,	--[PLANT_ZONE]
											NULL,		--[WORKSHOP]
											1,			--[SUPPLIER_TYPE]
											0,			--[ERP_FLAG]
											N'CON',		--[RUNSHEET_CODE], CON表示寄售订单
											NULL,		--[DELIVERY_LOCATION_NAME]
											NULL,		--[COMMENTS]
											N'admin',	--[CREATE_USER]
											GETDATE()	--[CREATE_DATE]
										FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] A WITH (NOLOCK)
										LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON B.[PLANT] = A.[WERKS] AND B.[PART_NO] = A.[MATNR] AND B.[ZONE_NO] = A.[LGORT]
										LEFT JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON A.[WERKS] = C.[PLANT] AND A.[LGORT] = C.[ZONE_NO] --AND C.IS_MANAGE IN (10,60) --= @ZONE_TYPE  收货仓库为10，退货仓库为60
										WHERE A.VBELN = @VBELN
										AND ISNULL(A.[LIFNR], '') = ISNULL(@LIFNR,'')
										AND ISNULL(A.[ZDOCK], '') = ISNULL(@ZDOCK,'')
										AND A.[PROCESS_FLAG] = 0
										AND A.[SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID

										DECLARE @VMI_RECEIVE_ID INT = SCOPE_IDENTITY()
						
										INSERT INTO [LES].[TT_WMM_VMI_RECEIVE_DETAIL]
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
											@VMI_RECEIVE_ID,	--[RECEIVE_ID]
											@WERKS,				--[PLANT]
											@LIFNR,				--[SUPPLIER_NUM]
											C.[WM_NO],			--[WM_NO]
											C.[ZONE_NO],		--[ZONE_NO]
											B.[DLOC],			--[DLOC]
											C.[WM_NO],			--[TARGET_WM]
											C.[ZONE_NO],		--[TARGET_ZONE]
											NULL,				--[TARGET_DLOC]
											A.[MEINS],			--[MEASURING_UNIT_NO]
											A.[BSTRF],			--[PACKAGE]
											CASE WHEN ISNULL(A.[BSTRF], 0) = 0
											THEN 
												0
											ELSE
												CASE WHEN ISNULL(A.[LGMNG], 0) >= 0
													THEN	
														CEILING(CAST(ISNULL(A.[LGMNG], 0) AS FLOAT)/CAST(ISNULL(A.[BSTRF], 0) AS FLOAT))
													ELSE
														FLOOR(CAST(ISNULL(A.[LGMNG], 0) AS FLOAT)/CAST(ISNULL(A.[BSTRF], 0) AS FLOAT))
													END
											END,				--[NUM]
											CASE WHEN ISNULL(A.[BSTRF], 0) = 0
											THEN 
												0
											ELSE
												CASE WHEN ISNULL(A.[LGMNG], 0) >= 0
													THEN	
														CEILING(CAST(ISNULL(A.[LGMNG], 0) AS FLOAT)/CAST(ISNULL(A.[BSTRF], 0) AS FLOAT))
													ELSE
														FLOOR(CAST(ISNULL(A.[LGMNG], 0) AS FLOAT)/CAST(ISNULL(A.[BSTRF], 0) AS FLOAT))
													END
											END,				--[BOX_NUM]
											A.[MATNR],			--[PART_NO]
											A.[MAKTX_ZH],		--[PART_CNAME]
											NULL,				--[PART_TYPE]
											CASE WHEN ISNULL(A.[BSTRF], 0) = 0
											THEN 
												0
											ELSE
												CASE WHEN ISNULL(A.[LGMNG], 0) >= 0
													THEN	
														CEILING(CAST(ISNULL(A.[LGMNG], 0) AS FLOAT)/CAST(ISNULL(A.[BSTRF], 0) AS FLOAT))
													ELSE
														FLOOR(CAST(ISNULL(A.[LGMNG], 0) AS FLOAT)/CAST(ISNULL(A.[BSTRF], 0) AS FLOAT))
													END
											END,				--[REQUIRED_BOX_NUM]
											A.[LGMNG],			--[REQUIRED_QTY]
											NULL,				--[ACTUAL_BOX_NUM]
											NULL,				--[ACTUAL_QTY]
											NULL,				--[Current_BOX_NUM]
											NULL,				--[Current_QTY]
											A.[ZYSQJC],			--[PACKAGE_MODEL]
											NULL,				--[BARCODE_DATA]
											NULL,				--[TRAN_NO]
											NULL,				--[IDENTIFY_PART_NO]
											B.[PART_ENAME],		--[PART_ENAME]
											A.[ZDOCK],			--[DOCK]
											NULL,				--[ASSEMBLY_LINE]
											NULL,				--[BOX_PARTS]
											NULL,				--[SEQUENCE_NO]
											NULL,				--[PICKUP_SEQ_NO]
											NULL,				--[RDC_DLOC]
											A.[BSTRF],			--[INHOUSE_PACKAGE]
											A.[ZYSQJC],			--[INBOUND_PACKAGE_MODEL]
											CASE WHEN ISNULL(A.[BSTRF], 0) = 0
											THEN 
												0
											ELSE
												CASE WHEN ISNULL(A.[LGMNG],0) >= 0
													THEN	
														CEILING(CAST(ISNULL(A.[LGMNG], 0) AS FLOAT)/CAST(ISNULL(A.[BSTRF], 0) AS FLOAT))
													ELSE
														FLOOR(CAST(ISNULL(A.[LGMNG], 0) AS FLOAT)/CAST(ISNULL(A.[BSTRF], 0) AS FLOAT))
													END
											END,				--[PACK_COUNT]
											@VBELN,				--[TWD_RUNSHEET_NO]
											NULL,				--[SUPPLIER_NUM_SHEET]
											NULL,				--[BOX_PARTS_SHEET]
											NULL,				--[RETURN_REPORT_FLAG]
											NULL,				--[ORDER_NO]
											NULL,				--[ITEM_NO]
											NULL,				--[COMMENTS]
											N'admin',			--[CREATE_USER]
											GETDATE()			--[CREATE_DATE]
										FROM [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] A WITH (NOLOCK)
										LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON B.[PLANT] = A.[WERKS] AND B.[PART_NO] = A.[MATNR] AND B.[ZONE_NO] = A.[LGORT]
										LEFT JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON A.[WERKS] = C.[PLANT] AND A.[LGORT] = C.[ZONE_NO]	--AND C.IS_MANAGE IN (10,60) --= @ZONE_TYPE  收货仓库为10，退货仓库为60
										WHERE A.[VBELN] = @VBELN
										AND ISNULL(A.[LIFNR], '') = ISNULL(@LIFNR,'')
										AND ISNULL(A.[ZDOCK], '') = ISNULL(@ZDOCK,'')
										AND A.[PROCESS_FLAG] = 0
										AND A.[SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
									END

								--生产件,售后配件(共用件,非共用件)
								--对于售后配件中的非共用件，客户有特殊业务需求，只做订单接收、展示、关单功能，不做收货，退货，打印条码等操作
								--售后配件中的非共用件,SAP通过非基础数据的工厂代码传过来，所以此处通过工厂代码是否在工厂基础数据中判断是否为售后配件中的非共用件
								--如果不是售后配件中的非共用件，代码走IF中语句，否则直接更新处理状态
								IF @Status <> -50
									BEGIN
											IF (@Status = -3 OR @Status = -22 OR @Status = -32)   --退货单
												BEGIN
													--创建退货单
													EXEC [LES].[PROC_WMM_CREATERETURN] @InsertId
												END
											ELSE
												BEGIN
												--正常单据，创建入库单和箱条码；对于委外加工的单据，不产生箱条码；对于售后配件4100工厂的不产生箱条码
												IF @LGORT IS NOT NULL AND @IS_ASN = 0
													BEGIN
															--生成条码表信息
															EXEC [LES].[PROC_SPM_SAP_LES_BARCODE] @InsertId

															--创建收货入库单
															EXEC [LES].[PROC_WMM_CREATERECEIVE] @InsertId
													END
												END
									END

								--更新处理状态
								UPDATE [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (ROWLOCK)
								SET [PROCESS_FLAG] = 1,
									[PROCESS_TIME] = GETDATE() 
								WHERE [VBELN] = @VBELN
								AND ISNULL([LIFNR], '') = ISNULL(@LIFNR, '')
								AND ISNULL([ZDOCK], '') = ISNULL(@ZDOCK, '')
								AND [PROCESS_FLAG] = 0
								AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
							END
						FETCH NEXT FROM RUNSHEET_CUR INTO @MAX_SEQ_ID, @VBELN, @POSNR, @LIFNR, @LFUHR, @WERKS, @EMLIF, @ZDOCK, @DISPO, @LFART, @ZVERSION, @STRAS, @LFDAT, @LFUHR, @MEINS, @LGORT, @LESTYPE
					END
				CLOSE  RUNSHEET_CUR
				DEALLOCATE RUNSHEET_CUR
			END
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN

		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS], [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', 'PROC_SPM_SAP_LES_RUNSHEET', 'Procedure', ISNULL(ERROR_MESSAGE(), '') + ';SEQ_ID:' + ISNULL(@MIN_SEQ_ID, '') + '-' + ISNULL(@MAX_SEQ_ID, ''), ERROR_LINE()

		--更新处理状态
		UPDATE [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] WITH (ROWLOCK)
		SET [PROCESS_FLAG] = 9,
			[PROCESS_TIME] = GETDATE()
		WHERE [VBELN] = @VBELN
		AND ISNULL([LIFNR], '') = ISNULL(@LIFNR, '')
		AND ISNULL([ZDOCK], '') = ISNULL(@ZDOCK, '')
		AND [PROCESS_FLAG] = 0
		AND [SEQ_ID] BETWEEN @MIN_SEQ_ID AND @MAX_SEQ_ID
	END CATCH
END