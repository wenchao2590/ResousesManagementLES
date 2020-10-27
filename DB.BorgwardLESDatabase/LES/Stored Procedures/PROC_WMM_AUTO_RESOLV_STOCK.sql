


CREATE PROCEDURE [LES].[PROC_WMM_AUTO_RESOLV_STOCK]
AS
    BEGIN
    SET NOCOUNT ON;

	DECLARE @TranSuccess TABLE(TRAN_ID INT)        

	DECLARE @insertNum INT =-1 ,@deleteNum INT =-1  --用于排查收货、出库数据异常丢失的数据标记

    DECLARE @TranId INT ,
            @TranType INT ,
            @BatchNo NVARCHAR(20) ,
            @PartNo NVARCHAR(20) ,
			@Plant NVARCHAR(20),
			@WmNo NVARCHAR(20),
            @ZoneNo NVARCHAR(20),
			@DLOC NVARCHAR(20),
			@TargetWm NVARCHAR(20),
			@TargetZone NVARCHAR(20),
			@TargetDloc NVARCHAR(20),
            @BarCode NVARCHAR(100),
			@IsBatch INT,
			@StockId INT,
			@TargetStockId INT
		
		--先补全TM_WMM_TRAN_DETAILS表里应有的数据
		IF EXISTS(SELECT 1 FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) T
                     INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON (S.PART_NO=T.PART_NO AND S.ZONE_NO=T.ZONE_NO AND S.PLANT=T.PLANT)
					      WHERE UPDATE_FLAG=0 AND TRAN_STATE=1)
		BEGIN
			--更新要进行数据处理的数据对应的状态
			--UPDATE LES.TM_WMM_TRAN_DETAILS --WITH(ROWLOCK)
			--   SET UPDATE_FLAG=3
			--  FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
			-- INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON (S.PART_NO=T.PART_NO AND S.ZONE_NO=T.ZONE_NO AND S.PLANT=T.PLANT)
			-- WHERE UPDATE_FLAG=0 AND TRAN_STATE =1

			UPDATE LES.TM_WMM_TRAN_DETAILS 
			   SET PACKAGE = (CASE WHEN ISNULL(T.PACKAGE, 0) = 0 THEN ISNULL(S.PACKAGE,1) ELSE T.PACKAGE END),
				   PACKAGE_MODEL = S.PACKAGE_MODEL, MAX = S.MAX, MIN= S.MIN, PLANT=S.PLANT, WM_NO = S.WM_NO,
				   DLOC = S.DLOC, 
				   SUPPLIER_NUM = S.SUPPLIER_NUM,
				   PART_CNAME = S.PART_CNAME,
				   PART_UNITS = CASE WHEN ISNULL(T.MEASURING_UNIT_NO,'')='' Then MP.PART_UNITS Else T.MEASURING_UNIT_NO END,
				   IS_BATCH = S.IS_BATCH,
				   PART_CLS = S.PART_CLS, 
				   NUM =ISNULL(CASE --批次管理的情况,入库和出库PDA会传递真实的件数,但Web会传NUM=0,只要是0则把包装数当成实际件数
								WHEN S.IS_BATCH = 1 AND T.NUM = 0 THEN ISNULL(S.PACKAGE,1)
								--翻包操作，NUM就取package
								--WHEN T.TRAN_TYPE IN (5, 6) THEN S.PACKAGE*T.BOX_NUM
								ELSE ISNULL(T.NUM,0) END,0),
				   BOX_NUM = ISNULL(CASE WHEN ISNULL(T.NUM,0) < ISNULL(S.PACKAGE,1) THEN 0 ELSE ISNULL(T.BOX_NUM,0) END,0),
				   UPDATE_FLAG=1
			  FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
			 INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON S.PART_NO=T.PART_NO AND S.ZONE_NO=T.ZONE_NO AND T.PLANT=S.PLANT
			 INNER JOIN LES.TM_BAS_MAINTAIN_PARTS (NOLOCK) MP ON T.PART_NO=MP.PART_NO AND T.PLANT=MP.PLANT			
			 WHERE T.UPDATE_FLAG=0 AND TRAN_STATE =1
				
			--张恒 提出在程序里处理
			--由于翻包实际件数允许修改，所以只能在程序里赋值
			--翻包前数据处理			
			--UPDATE  LES.TM_WMM_TRAN_DETAILS
			--SET NUM = S.PACKAGE*T.BOX_NUM
			--FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
			--INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S 
			--ON S.PART_NO = T.PART_NO AND S.ZONE_NO = T.ZONE_NO				
			--WHERE T.TRAN_ID IN (SELECT DISTINCT TRAN_ID FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE UPDATE_FLAG = 0 AND TRAN_STATE = 1)
			--AND T.TRAN_TYPE=5
			--翻包后数据处理
			--UPDATE  T_After 
			--	SET NUM=T_Before.NUM
			--	FROM LES.TM_WMM_TRAN_DETAILS T_After 
			--	INNER JOIN  LES.TM_WMM_TRAN_DETAILS T_Before ON (T_After.TRAN_NO=T_Before.TRAN_NO AND T_After.PART_NO=T_Before.PART_NO AND T_After.TRAN_TYPE=6 AND T_Before.TRAN_TYPE=5)	
			--	--INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON (S.PART_NO = T_After.PART_NO AND S.ZONE_NO = T_After.ZONE_NO)				
			--	WHERE T_After.UPDATE_FLAG=3
				--T_After.TRAN_ID IN (SELECT DISTINCT TRAN_ID FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE UPDATE_FLAG = 0 AND TRAN_STATE = 1)

			--标示修改
			--UPDATE LES.TM_WMM_TRAN_DETAILS 
			--   SET UPDATE_FLAG=1
			--  FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
			-- WHERE T.UPDATE_FLAG=3

				 --INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON (S.PART_NO=T.PART_NO AND S.ZONE_NO=T.ZONE_NO)				
				 --WHERE T.TRAN_ID IN (SELECT DISTINCT TRAN_ID 
					--				   FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) 
					--				  WHERE UPDATE_FLAG = 0 
					--					AND TRAN_STATE = 1)		
		END

		--SELECT  @TranId = TRAN_ID ,
		--              @TranType = TRAN_TYPE ,
		--              @BatchNo = BATCH_NO ,
		--              @PartNo = PART_NO ,
		--		@Plant = PLANT,
		--		@WmNo = WM_NO,
		--              @ZoneNo = ZONE_NO ,
		--		@DLOC = DLOC,
		--		@TargetWm = TARGET_WM,
		--		@TargetZone = TARGET_ZONE,
		--		@TargetDloc = TARGET_DLOC,
		--              @BarCode = BARCODE_DATA,
		--		@IsBatch = IS_BATCH
		--      FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK)
		--      WHERE TRAN_ID = (SELECT TOP 1 TRAN_ID 
		--                    FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) 
		--				WHERE TRAN_STATE = 1 
		--					AND UPDATE_FLAG = 1 
		--				ORDER BY TRAN_ID ASC )
		--以上逻辑已改为游标，单次处理500条

		DECLARE STOCK_CUR CURSOR FOR SELECT TOP 500 TRAN_ID
		                                           ,TRAN_TYPE
												   ,BATCH_NO
												   ,PART_NO
												   ,PLANT
												   ,WM_NO
												   ,ZONE_NO
												   ,DLOC
												   ,TARGET_WM
												   ,TARGET_ZONE
												   ,TARGET_DLOC
												   ,BARCODE_DATA
												   ,IS_BATCH 
											   FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) 
				                              WHERE TRAN_STATE = 1 AND UPDATE_FLAG = 1 
				                              ORDER BY TRAN_ID ASC
		
		OPEN STOCK_CUR
		FETCH  NEXT  FROM STOCK_CUR INTO @TranId,@TranType,@BatchNo,@PartNo,@Plant,@WmNo,@ZoneNo,@DLOC,@TargetWm,@TargetZone,@TargetDloc,@BarCode,@IsBatch
		WHILE( @@fetch_status = 0 )
		BEGIN
			
			DELETE FROM @TranSuccess

			set @StockId= null
			set @TargetStockId= null

			--SotckId只对非批次管理的情况才会用到
			SELECT @StockId=STOCK_IDENTITY 
			  FROM LES.TT_WMS_STOCKS S ( NOLOCK ) 
			 WHERE PART_NO=@PartNo 
			   AND S.ZONE_NO=@ZoneNo
        
			SELECT  @TargetStockId=STOCK_IDENTITY 
			  FROM LES.TT_WMS_STOCKS S ( NOLOCK ) 
			 WHERE PART_NO=@PartNo 
			   AND S.ZONE_NO=@TargetZone
			
			BEGIN TRAN
			BEGIN TRY
				--1.入库, 6.翻包后 ,8 销售退货
				IF (@TranType=1 OR @TranType=6 OR @TranType=8) 
				BEGIN
					--批次管理入库
					IF (@IsBatch = 1 ) 
					BEGIN
						INSERT  INTO [LES].[TT_WMS_STOCKS]
								(   [PLANT] ,
									[WM_NO] ,
									[ZONE_NO] ,
									[DLOC] ,
									[SUPPLIER_NUM] ,
									[PART_NO] ,
									[PART_CNAME] ,
									PART_NICKNAME,
									[PART_UNITS] ,
									[PACKAGE_MODEL] ,
									[PACKAGE] ,
									[MAX] ,
									[MIN] ,
									[SAFE_STOCK] ,
									[STOCKS] ,
									STOCKS_NUM,
									[FROZEN_STOCKS] ,
									[AVAILBLE_STOCKS] ,
									[IS_BATCH] ,
									[FRAGMENT_NUM] ,
									[PART_CLS] ,
									[IS_REPACK] ,
									[REPACK_ROUTE] ,
									[BATCH_NO] ,
									[BARCODE_DATA] ,
									[BARCODE_TYPE] ,
									[COMMENTS] ,
									[CREATE_USER] ,
									[CREATE_DATE])
								SELECT  PLANT ,
										WM_NO ,
										ZONE_NO ,
										DLOC ,
										SUPPLIER_NUM ,
										PART_NO ,
										PART_CNAME ,
										(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
										PART_UNITS ,
										PACKAGE_MODEL ,
										ISNULL(PACKAGE,1) ,
										MAX ,
										MIN ,
										'' AS SAFE_STOCK ,
										ISNULL(BOX_NUM,0) AS STOCKS ,
										ISNULL(NUM,0) AS STOCKS_NUM,
										0 AS FROZEN_STOCKS ,
										BOX_NUM AS AVAILBLE_STOCKS ,
										1 AS [IS_BATCH],
										[FRAGMENT_NUM] = ( 
												CASE 
												WHEN BOX_NUM = 1 THEN 0 
												ELSE NUM 
												END),
										PART_CLS ,
										NULL AS [IS_REPACK] ,
										NULL AS [REPACK_ROUTE] ,
										[BATCH_NO] ,
										[BARCODE_DATA] ,
										[BARCODE_TYPE] ,
										[COMMENTS] ,
										'' AS [CREATE_USER] ,
										GETDATE() AS [CREATE_DATE]
								FROM    LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
								WHERE   BATCH_NO = @BatchNo
							
						INSERT INTO @TranSuccess SELECT DISTINCT TRAN_ID FROM LES.TM_WMM_TRAN_DETAILS WHERE BATCH_NO=@BatchNo
					END   
					ELSE
					--非批次管理入库
					BEGIN
						--零件存在，往存在的记录上添加库存
						IF ( ISNULL(@StockId, '') <> '' )
						BEGIN
							UPDATE LES.TT_WMS_STOCKS
							   SET STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0),									
								   STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT),                                            
								   AVAILBLE_STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT),		 							
								   FRAGMENT_NUM = (ISNULL(S.FRAGMENT_NUM,0) + ISNULL(T.NUM, 0)- ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1),
								   UPDATE_DATE = GETDATE(),											
								   COMMENTS = T.COMMENTS
							  FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
							 INNER JOIN LES.TT_WMS_STOCKS S ON (S.STOCK_IDENTITY=@StockId)
							 Where T.TRAN_ID = @TranId		
							--张恒 取消大小判断，由于增加负库存，所以不用判断
							--UPDATE LES.TT_WMS_STOCKS
							--SET STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0),									
							--	STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
							--				ELSE 0 END),                                            
							--	AVAILBLE_STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT) 
							--						ELSE 0 END),		 							
							--	FRAGMENT_NUM = (ISNULL(S.FRAGMENT_NUM,0) + ISNULL(T.NUM, 0)- ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1),
							--	UPDATE_DATE = GETDATE(),											
							--	COMMENTS = T.COMMENTS
							--FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
							--	INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @StockId 
							--where T.TRAN_ID = @TranId						
						END
						ELSE
						--零件不存在，添加新的库存
						BEGIN
							INSERT  INTO [LES].[TT_WMS_STOCKS]([PLANT] ,
																[WM_NO] ,
																[ZONE_NO] ,
																[DLOC] ,
																[SUPPLIER_NUM] ,
																[PART_NO] ,
																[PART_CNAME] ,
																PART_NICKNAME,
																[PART_UNITS] ,
																[PACKAGE_MODEL] ,
																[PACKAGE] ,
																[MAX] ,
																[MIN] ,
																[SAFE_STOCK] ,
																[STOCKS] ,
																STOCKS_NUM,
																[FROZEN_STOCKS] ,
																[AVAILBLE_STOCKS] ,
																[IS_BATCH] ,
																[FRAGMENT_NUM] ,
																[PART_CLS] ,
																[IS_REPACK] ,
																[REPACK_ROUTE] ,
																[BATCH_NO] ,
																[BARCODE_DATA] ,
																[BARCODE_TYPE] ,
																[COMMENTS] ,
																[CREATE_USER] ,
																[CREATE_DATE])
													SELECT  PLANT ,
															WM_NO ,
															ZONE_NO ,
															DLOC ,
															SUPPLIER_NUM ,
															PART_NO ,
															PART_CNAME ,
															(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
															PART_UNITS ,
															PACKAGE_MODEL ,
															ISNULL(PACKAGE,1) ,
															MAX ,
															MIN ,
															0 AS SAFE_STOCK ,
															STOCKS =( CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT) ELSE 0 END),
															ISNULL(NUM,0) AS STOCKS_NUM,
															0 AS FROZEN_STOCKS ,
															AVAILBLE_STOCKS =( CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT) ELSE 0 END),
															0 AS IS_BATCH ,
															ISNULL(NUM,0) % ISNULL(PACKAGE,1) AS [FRAGMENT_NUM] ,
															PART_CLS ,
															'' AS [IS_REPACK] ,
															'' AS [REPACK_ROUTE] ,
															[BATCH_NO] ,
															[BARCODE_DATA] ,
															[BARCODE_TYPE] ,
															[COMMENTS] ,
															'' AS [CREATE_USER] ,
															GETDATE() AS [CREATE_DATE]
													   FROM LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
													  WHERE TRAN_ID = @TranId
						END

						INSERT INTO @TranSuccess (TRAN_ID) VALUES (@TranId)
					END		
				END

				--5翻包前，3供应商退货, 307其他出库, 55工废, 11研发领用,7销售出库,405 Z05-返修领用,407 Z07-部门领用,411 Z11-项目阶段生产物资领用,451 Z51-库内物料报废,455 Z55-生产物料报废,417 Z17-调试样件领用,457 Z57-工程报废
				ELSE IF (@TranType = 5 OR @TranType = 3 OR @TranType = 307 OR @TranType = 55 OR @TranType = 11 OR @TranType = 7  OR @TranType = 405 OR @TranType = 407 OR @TranType = 411 OR @TranType = 451 OR @TranType = 455  OR @TranType = 417 oR @TranType = 457) 
				BEGIN
					--批次管理出库
					IF ( @IsBatch = 1)
					BEGIN
						--有箱条码,批次管理出库
						SELECT TRAN_NO, PART_NO, TRAN_TYPE INTO #TempReceive FROM LES.TM_WMM_TRAN_DETAILS WHERE BARCODE_DATA = @BarCode
						SELECT D.BARCODE_DATA INTO #TempReceiveBarCode FROM LES.TM_WMM_TRAN_DETAILS D(NOLOCK) JOIN #TempReceive T ON D.TRAN_NO = T.TRAN_NO AND D.PART_NO = T.PART_NO AND D.TRAN_TYPE = T.TRAN_TYPE
						DROP TABLE #TempReceive

						UPDATE  LES.TT_WMS_STOCKS
						SET --批次管理的要出库，库存和散件直接全部置为0  
							STOCKS = 0 ,
							STOCKS_NUM = 0,
							AVAILBLE_STOCKS = 0 ,										                                        
							FRAGMENT_NUM = 0 ,
							BUSINESS_PK = NULL,
							UPDATE_DATE = GETDATE()
						WHERE   BARCODE_DATA IN (SELECT BARCODE_DATA FROM #TempReceiveBarCode)
							
						INSERT INTO @TranSuccess
						SELECT DISTINCT TRAN_ID FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE BARCODE_DATA IN (SELECT BARCODE_DATA FROM #TempReceiveBarCode) 
						DROP TABLE #TempReceiveBarCode                           
					END   
					ELSE		
					BEGIN
						--零件存在，往存在的记录上添加库存
						IF ( ISNULL(@StockId, '') <> '' )
						BEGIN
							UPDATE LES.TT_WMS_STOCKS
								SET STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0)
									,STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT)
									,AVAILBLE_STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) / ISNULL(S.PACKAGE,1) AS INT)
									,FRAGMENT_NUM = (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) % ISNULL(S.PACKAGE,1)
									,UPDATE_DATE = GETDATE()
									,COMMENTS = T.COMMENTS
								FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
									INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @StockId
								WHERE T.TRAN_ID = @TranId
							--张恒 取消大小判断，由于增加负库存，所以不用判断
							--UPDATE LES.TT_WMS_STOCKS
							--	SET STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0)
							--	   ,STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
							--				  --WHEN S.STOCKS_NUM - T.NUM = 0 THEN 0
							--				  ELSE 0 END)
							--	   ,AVAILBLE_STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) / ISNULL(S.PACKAGE,1) AS INT) 
							--						   --WHEN S.STOCKS_NUM - T.NUM - S.FROZEN_STOCKS * S.PACKAGE = 0 THEN 0
							--						   ELSE 0 END)
							--	   ,FRAGMENT_NUM = (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) % ISNULL(S.PACKAGE,1)
							--	   ,UPDATE_DATE = GETDATE()
							--	   ,COMMENTS = T.COMMENTS
							--	FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
							--		INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @StockId
							--	WHERE T.TRAN_ID = @TranId
						END
						ELSE
						--零件不存在，添加新的库存
						BEGIN
							INSERT  INTO [LES].[TT_WMS_STOCKS]([PLANT] ,
																[WM_NO] ,
																[ZONE_NO] ,
																[DLOC] ,
																[SUPPLIER_NUM] ,
																[PART_NO] ,
																[PART_CNAME] ,
																PART_NICKNAME,
																[PART_UNITS] ,
																[PACKAGE_MODEL] ,
																[PACKAGE] ,
																[MAX] ,
																[MIN] ,
																[SAFE_STOCK] ,
																[STOCKS] ,
																STOCKS_NUM,
																[FROZEN_STOCKS] ,
																[AVAILBLE_STOCKS] ,
																[IS_BATCH] ,
																[FRAGMENT_NUM] ,
																[PART_CLS] ,
																[IS_REPACK] ,
																[REPACK_ROUTE] ,
																[BATCH_NO] ,
																[BARCODE_DATA] ,
																[BARCODE_TYPE] ,
																[COMMENTS] ,
																[CREATE_USER] ,
																[CREATE_DATE])
													SELECT  PLANT ,
															WM_NO ,
															ZONE_NO ,
															DLOC ,
															SUPPLIER_NUM ,
															PART_NO ,
															PART_CNAME ,
															(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
															PART_UNITS ,
															PACKAGE_MODEL ,
															ISNULL(PACKAGE,1) ,
															MAX ,
															MIN ,
															0 AS SAFE_STOCK ,
															STOCKS =(0-CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT)),
															(0-ISNULL(NUM,0)) AS STOCKS_NUM,
															0 AS FROZEN_STOCKS ,
															AVAILBLE_STOCKS =(0-CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT)),
															0 AS IS_BATCH ,
															(0-ISNULL(NUM,0)) % ISNULL(PACKAGE,1) AS [FRAGMENT_NUM] ,
															PART_CLS ,
															'' AS [IS_REPACK] ,
															'' AS [REPACK_ROUTE] ,
															[BATCH_NO] ,
															[BARCODE_DATA] ,
															[BARCODE_TYPE] ,
															[COMMENTS] ,
															'' AS [CREATE_USER] ,
															GETDATE() AS [CREATE_DATE]
														FROM LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
														WHERE TRAN_ID = @TranId
						END
						INSERT INTO @TranSuccess (TRAN_ID) VALUES (@TranId)
					END	
				END

				--4.退料
				ELSE IF ( @TranType = 4) 
				BEGIN
					--批次管理退料
					IF (@IsBatch = 1 ) 
					BEGIN
						DELETE FROM [LES].[TT_WMS_STOCKS] WHERE BARCODE_DATA IN (SELECT BARCODE_DATA FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) WHERE BATCH_NO = @BatchNo)
						INSERT  INTO [LES].[TT_WMS_STOCKS]
								( [PLANT] ,
									[WM_NO] ,
									[ZONE_NO] ,
									[DLOC] ,
									[SUPPLIER_NUM] ,
									[PART_NO] ,
									[PART_CNAME] ,
									PART_NICKNAME,
									[PART_UNITS] ,
									[PACKAGE_MODEL] ,
									[PACKAGE] ,
									[MAX] ,
									[MIN] ,
									[SAFE_STOCK] ,
									[STOCKS] ,
									STOCKS_NUM,
									[FROZEN_STOCKS] ,
									[AVAILBLE_STOCKS] ,
									[IS_BATCH] ,
									[FRAGMENT_NUM] ,
									[PART_CLS] ,
									[IS_REPACK] ,
									[REPACK_ROUTE] ,
									[BATCH_NO] ,
									[BARCODE_DATA] ,
									[BARCODE_TYPE] ,
									[COMMENTS] ,
									[CREATE_USER] ,
									[CREATE_DATE]
								)
								SELECT  PLANT ,
										TARGET_WM AS WM_NO ,
										TARGET_ZONE AS ZONE_NO ,
										TARGET_DLOC AS DLOC ,
										SUPPLIER_NUM ,
										PART_NO ,
										PART_CNAME ,
										(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
										PART_UNITS ,
										PACKAGE_MODEL ,
										PACKAGE ,
										MAX ,
										MIN ,
										'' AS SAFE_STOCK ,
										ISNULL(BOX_NUM,0) AS STOCKS ,
										ISNULL(NUM,0) AS STOCKS_NUM,
										0 AS FROZEN_STOCKS ,
										BOX_NUM AS AVAILBLE_STOCKS ,
										1 AS [IS_BATCH],
										[FRAGMENT_NUM] = (CASE WHEN BOX_NUM = 1 THEN 0 ELSE NUM END),
										PART_CLS ,
										NULL AS [IS_REPACK] ,
										NULL AS [REPACK_ROUTE] ,
										[BATCH_NO] ,
										[BARCODE_DATA] ,
										[BARCODE_TYPE] ,
										[COMMENTS] ,
										'' AS [CREATE_USER] ,
										GETDATE() AS [CREATE_DATE]
									FROM   LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
								WHERE   BATCH_NO = @BatchNo
							
						INSERT INTO @TranSuccess SELECT DISTINCT TRAN_ID FROM LES.TM_WMM_TRAN_DETAILS WHERE BATCH_NO = @BatchNo	                 
					END   
					ELSE
					--非批次管理退料
					BEGIN
						--零件存在，往存在的记录上添加库存
						IF ( ISNULL(@TargetStockId, '') <> '' )
						BEGIN
							UPDATE  LES.TT_WMS_STOCKS
								SET  STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0)
									,STOCKS= CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT)
									,AVAILBLE_STOCKS= CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) / ISNULL(S.PACKAGE,1) AS INT)
									,FRAGMENT_NUM = (ISNULL(S.FRAGMENT_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) % ISNULL(S.PACKAGE,1)
									,UPDATE_DATE = GETDATE()
									,COMMENTS = T.COMMENTS
								FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
									INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @TargetStockId
								WHERE T.TRAN_ID = @TranId	
							--张恒 取消大小判断，由于增加负库存，所以不用判断
							--UPDATE  LES.TT_WMS_STOCKS
								--SET  STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0)
								--	,STOCKS=(CASE WHEN (ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
								--			ELSE 0 END)
								--	,AVAILBLE_STOCKS=(CASE WHEN (ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) / ISNULL(S.PACKAGE,1) AS INT) 
								--			ELSE 0 END)
								--	,FRAGMENT_NUM = (ISNULL(S.FRAGMENT_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS,0)) % ISNULL(S.PACKAGE,1)
								--	,UPDATE_DATE = GETDATE()
								--	,COMMENTS = T.COMMENTS
								--FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
								--	INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @TargetStockId
								--WHERE T.TRAN_ID = @TranId						
						END
						ELSE
						--零件不存在，添加新的库存
						BEGIN
							INSERT  INTO [LES].[TT_WMS_STOCKS]
									( [PLANT] ,
										[WM_NO] ,
										[ZONE_NO] ,
										[DLOC] ,
										[SUPPLIER_NUM] ,
										[PART_NO] ,
										[PART_CNAME] ,
										PART_NICKNAME,
										[PART_UNITS] ,
										[PACKAGE_MODEL] ,
										[PACKAGE] ,
										[MAX] ,
										[MIN] ,
										[SAFE_STOCK] ,
										[STOCKS] ,
										STOCKS_NUM,
										[FROZEN_STOCKS] ,
										[AVAILBLE_STOCKS] ,
										[IS_BATCH] ,
										[FRAGMENT_NUM] ,
										[PART_CLS] ,
										[IS_REPACK] ,
										[REPACK_ROUTE] ,
										[BATCH_NO] ,
										[BARCODE_DATA] ,
										[BARCODE_TYPE] ,
										[COMMENTS] ,
										[CREATE_USER] ,
										[CREATE_DATE]
									)
									SELECT  PLANT ,
											TARGET_WM AS WM_NO ,
											TARGET_ZONE AS ZONE_NO ,
											TARGET_DLOC AS DLOC ,
											SUPPLIER_NUM ,
											PART_NO ,
											PART_CNAME ,
											(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
											PART_UNITS ,
											PACKAGE_MODEL ,
											ISNULL(PACKAGE,1) ,
											MAX ,
											MIN ,
											0 AS SAFE_STOCK ,
											STOCKS =( CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(NUM / ISNULL(PACKAGE,1) AS INT) ELSE 0 END),
											ISNULL(NUM,0) AS STOCKS_NUM,
											0 AS FROZEN_STOCKS ,
											AVAILBLE_STOCKS =( CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(NUM / ISNULL(PACKAGE,1) AS INT) ELSE 0 END),
											0 AS IS_BATCH ,
											ISNULL(NUM,0) % ISNULL(PACKAGE,1) AS [FRAGMENT_NUM] ,
											PART_CLS ,
											'' AS [IS_REPACK] ,
											'' AS [REPACK_ROUTE] ,
											[BATCH_NO] ,
											[BARCODE_DATA] ,
											[BARCODE_TYPE] ,
											[COMMENTS] ,
											'' AS [CREATE_USER] ,
											GETDATE() AS [CREATE_DATE]
									FROM    LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
									WHERE   TRAN_ID = @TranId
						END

						INSERT INTO @TranSuccess (TRAN_ID) VALUES (@TranId)
					END		
				END

				--301 零件封存（冻结）
				ELSE IF ( @TranType = 301 ) 
				BEGIN
					--批次管理的情况
					IF ( @IsBatch = 1)
					BEGIN
						--找到该批次所有箱条码，根据箱条码来批量更新
						SELECT TRAN_NO, PART_NO, TRAN_TYPE INTO #TempFrozen FROM LES.TM_WMM_TRAN_DETAILS WHERE BARCODE_DATA = @BarCode
						SELECT D.BARCODE_DATA INTO #TempFrozenBarCode FROM LES.TM_WMM_TRAN_DETAILS D(NOLOCK) JOIN #TempFrozen T ON D.TRAN_NO = T.TRAN_NO AND D.PART_NO = T.PART_NO AND D.TRAN_TYPE = T.TRAN_TYPE
						DROP TABLE #TempFrozen

						--直接更换库位为目标库位，并更新冻结库存和可用库存
						UPDATE  LES.TT_WMS_STOCKS
						SET	WM_NO = T.TARGET_WM,
							ZONE_NO = T.TARGET_ZONE,
							DLOC = T.TARGET_DLOC,						
							FROZEN_STOCKS = S.STOCKS_NUM,		--冻结库存用总件数代替
							AVAILBLE_STOCKS = 0,				--可用库存为0，总件数不变
							BUSINESS_PK = NULL,
							UPDATE_DATE = GETDATE()
						FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
						INNER JOIN LES.TT_WMS_STOCKS S ON S.BARCODE_DATA = T.BARCODE_DATA	
						WHERE S.BARCODE_DATA IN (SELECT BARCODE_DATA FROM #TempFrozenBarCode)

						--LOG处理
						INSERT INTO @TranSuccess
						SELECT DISTINCT TRAN_ID FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE BARCODE_DATA IN (SELECT BARCODE_DATA FROM #TempFrozenBarCode) 
						DROP TABLE #TempFrozenBarCode
					END   
					ELSE		
					BEGIN
						--减原库位库存
						UPDATE LES.TT_WMS_STOCKS
							SET STOCKS_NUM = ISNULL(S.STOCKS_NUM, 0) - ISNULL(T.NUM, 0)
								,STOCKS= CAST((ISNULL(S.STOCKS_NUM, 0) - ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT)
								,FRAGMENT_NUM= (ISNULL(S.FRAGMENT_NUM, 0) - ISNULL(T.NUM, 0)- ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1)
								,AVAILBLE_STOCKS= CAST((ISNULL(S.STOCKS_NUM, 0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT)
								,UPDATE_DATE = GETDATE()
							FROM  LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
							INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY=@StockId
							WHERE T.TRAN_ID = @TranId
						--张恒 取消大小判断，由于增加负库存，所以不用判断	
						--UPDATE LES.TT_WMS_STOCKS
						--	SET STOCKS_NUM = ISNULL(S.STOCKS_NUM, 0) - ISNULL(T.NUM, 0)
						--		,STOCKS=(CASE WHEN (ISNULL(S.STOCKS_NUM, 0) - ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM, 0) - ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
						--				ELSE 0 END)
						--		,FRAGMENT_NUM=(ISNULL(S.FRAGMENT_NUM, 0) - ISNULL(T.NUM, 0)- ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1)
						--		,AVAILBLE_STOCKS=(CASE WHEN (ISNULL(S.STOCKS_NUM, 0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM, 0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT) 
						--						ELSE 0 END)
						--		,UPDATE_DATE = GETDATE()
						--	FROM  LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
						--	INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY=@StockId
						--	WHERE T.TRAN_ID = @TranId	
						--加目标库位库存
						IF ( ISNULL(@TargetStockId, '') <> '' )
						BEGIN
							UPDATE  LES.TT_WMS_STOCKS
								SET STOCKS_NUM = ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0),									
									STOCKS = CAST((ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT),  --库存箱数不变                            
									FROZEN_STOCKS = ISNULL(S.FROZEN_STOCKS, 0) + ISNULL(T.NUM, 0),   --直接加件数					
									--FRAGMENT_NUM = ( S.FRAGMENT_NUM + T.NUM ) % T.PACKAGE,  --散件数不变
									UPDATE_DATE = GETDATE(),											
									COMMENTS = T.COMMENTS
								FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
									INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @TargetStockId 
								WHERE T.TRAN_ID = @TranId	
							--张恒 取消大小判断，由于增加负库存，所以不用判断	
							--UPDATE  LES.TT_WMS_STOCKS
							--	SET STOCKS_NUM = ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0),									
							--		STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
							--					ELSE 0 END),                --库存箱数不变                            
							--		FROZEN_STOCKS = ISNULL(S.FROZEN_STOCKS, 0) + ISNULL(T.NUM, 0),   --直接加件数					
							--		--FRAGMENT_NUM = ( S.FRAGMENT_NUM + T.NUM ) % T.PACKAGE,  --散件数不变
							--		UPDATE_DATE = GETDATE(),											
							--		COMMENTS = T.COMMENTS
							--	FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
							--		INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @TargetStockId 
							--	WHERE T.TRAN_ID = @TranId									
						END
						ELSE
						--零件不存在，添加新的库存
						BEGIN
							INSERT  INTO [LES].[TT_WMS_STOCKS]
									( [PLANT] ,
										[WM_NO] ,
										[ZONE_NO] ,
										[DLOC] ,
										[SUPPLIER_NUM] ,
										[PART_NO] ,
										[PART_CNAME] ,
										PART_NICKNAME,
										[PART_UNITS] ,
										[PACKAGE_MODEL] ,
										[PACKAGE] ,
										[MAX] ,
										[MIN] ,
										[SAFE_STOCK] ,
										[STOCKS] ,
										STOCKS_NUM,
										[FROZEN_STOCKS] ,
										[AVAILBLE_STOCKS] ,
										[IS_BATCH] ,
										[FRAGMENT_NUM] ,
										[PART_CLS] ,
										[IS_REPACK] ,
										[REPACK_ROUTE] ,
										[BATCH_NO] ,
										[BARCODE_DATA] ,
										[BARCODE_TYPE] ,
										[COMMENTS] ,
										[CREATE_USER] ,
										[CREATE_DATE]
									)
									SELECT  PLANT ,
											TARGET_WM AS WM_NO ,
											TARGET_ZONE AS ZONE_NO ,
											TARGET_DLOC AS DLOC ,
											SUPPLIER_NUM ,
											PART_NO ,
											PART_CNAME ,
											(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
											PART_UNITS ,
											PACKAGE_MODEL ,
											ISNULL(PACKAGE,1) ,
											MAX ,
											MIN ,
											0 AS SAFE_STOCK ,
											STOCKS =( CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(NUM / ISNULL(PACKAGE,1) AS INT) ELSE 0 END), 
											ISNULL(NUM,0) AS STOCKS_NUM,
											ISNULL(NUM,0) AS FROZEN_STOCKS,
											0 AS AVAILBLE_STOCKS,
											0 AS IS_BATCH ,
											0 AS [FRAGMENT_NUM] ,
											PART_CLS ,
											'' AS [IS_REPACK] ,
											'' AS [REPACK_ROUTE] ,
											[BATCH_NO] ,
											[BARCODE_DATA] ,
											[BARCODE_TYPE] ,
											[COMMENTS] ,
											'' AS [CREATE_USER] ,
											GETDATE() AS [CREATE_DATE]
									FROM    LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
									WHERE   TRAN_ID = @TranId
						END

						--LOG处理
						INSERT INTO @TranSuccess (TRAN_ID) VALUES (@TranId)
					END	
				END
			
				--302 零件解封（解冻）
				ELSE IF ( @TranType = 302 ) 
				BEGIN
					--批次管理的情况
					IF ( @IsBatch = 1)
					BEGIN
						--找到该批次所有箱条码，根据箱条码来批量更新
						SELECT TRAN_NO, PART_NO, TRAN_TYPE INTO #TempUnfreeze FROM LES.TM_WMM_TRAN_DETAILS WHERE BARCODE_DATA = @BarCode
						SELECT D.BARCODE_DATA INTO #TempUnfreezeBarCode FROM LES.TM_WMM_TRAN_DETAILS D(NOLOCK) JOIN #TempUnfreeze T ON D.TRAN_NO = T.TRAN_NO AND D.PART_NO = T.PART_NO AND D.TRAN_TYPE = T.TRAN_TYPE
						DROP TABLE #TempUnfreeze

						--只更新冻结库存和可用库存
						UPDATE  LES.TT_WMS_STOCKS
						SET	WM_NO = T.TARGET_WM,
							ZONE_NO = T.TARGET_ZONE,
							DLOC = T.TARGET_DLOC,						
							FROZEN_STOCKS = 0,
							AVAILBLE_STOCKS = S.FROZEN_STOCKS,																		                                        
							BUSINESS_PK = NULL,
							UPDATE_DATE = GETDATE()
						FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
						INNER JOIN LES.TT_WMS_STOCKS S ON S.BARCODE_DATA = T.BARCODE_DATA	
						WHERE S.BARCODE_DATA IN (SELECT BARCODE_DATA FROM #TempUnfreezeBarCode)

						--LOG处理
						INSERT INTO @TranSuccess
						SELECT DISTINCT TRAN_ID FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE BARCODE_DATA IN (SELECT BARCODE_DATA FROM #TempUnfreezeBarCode) 
						DROP TABLE #TempUnfreezeBarCode
					END   
					ELSE		
					BEGIN
						--更新源库位库存
						UPDATE LES.TT_WMS_STOCKS
							SET STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0),  --总件数减少
								STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT), --源库存不变
								--FRAGMENT_NUM = ( S.STOCKS_NUM - T.NUM ) % S.PACKAGE, 散件数不变
								FROZEN_STOCKS = ISNULL(S.FROZEN_STOCKS,0) - ISNULL(T.NUM, 0),  	  --冻结件数减少							
								UPDATE_DATE = GETDATE()
							FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
								INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @StockId 
							WHERE T.TRAN_ID = @TranId	
						--张恒 取消大小判断，由于增加负库存，所以不用判断
						--UPDATE LES.TT_WMS_STOCKS
						--	SET STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0),  --总件数减少
						--		STOCKS = (CASE  WHEN (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
						--					ELSE 0 END), --源库存不变
						--		--FRAGMENT_NUM = ( S.STOCKS_NUM - T.NUM ) % S.PACKAGE, 散件数不变
						--		FROZEN_STOCKS = ISNULL(S.FROZEN_STOCKS,0) - ISNULL(T.NUM, 0),  	  --冻结件数减少							
						--		UPDATE_DATE = GETDATE()
						--	FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
						--		INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @StockId 
						--WHERE T.TRAN_ID = @TranId						
							
						--根据目标库位进行运输目标库位的库存
						IF ( ISNULL(@TargetStockId, '') <> '' )
						BEGIN
							UPDATE  LES.TT_WMS_STOCKS
							SET     STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0),									
									STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT),                                            
									AVAILBLE_STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT),									
									FRAGMENT_NUM = (ISNULL(S.FRAGMENT_NUM,0) + ISNULL(T.NUM, 0)- ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1),
									UPDATE_DATE = GETDATE(),											
									COMMENTS = T.COMMENTS
							FROM    LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
									INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @TargetStockId AND T.TRAN_ID = @TranId
							--张恒 取消大小判断，由于增加负库存，所以不用判断
							--UPDATE  LES.TT_WMS_STOCKS
							--SET     STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0),									
							--		STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
							--					ELSE 0 END),                                            
							--		AVAILBLE_STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT) 
							--							ELSE 0 END),									
							--		FRAGMENT_NUM = (ISNULL(S.FRAGMENT_NUM,0) + ISNULL(T.NUM, 0)- ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1),
							--		UPDATE_DATE = GETDATE(),											
							--		COMMENTS = T.COMMENTS
							--FROM    LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
							--		INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @TargetStockId AND T.TRAN_ID = @TranId						
						END
						ELSE
						--零件不存在，添加新的库存
						BEGIN
							INSERT  INTO [LES].[TT_WMS_STOCKS]
									( [PLANT] ,
										[WM_NO] ,
										[ZONE_NO] ,
										[DLOC] ,
										[SUPPLIER_NUM] ,
										[PART_NO] ,
										[PART_CNAME] ,
										PART_NICKNAME,
										[PART_UNITS] ,
										[PACKAGE_MODEL] ,
										[PACKAGE] ,
										[MAX] ,
										[MIN] ,
										[SAFE_STOCK] ,
										[STOCKS] ,
										STOCKS_NUM,
										[FROZEN_STOCKS] ,
										[AVAILBLE_STOCKS] ,
										[IS_BATCH] ,
										[FRAGMENT_NUM] ,
										[PART_CLS] ,
										[IS_REPACK] ,
										[REPACK_ROUTE] ,
										[BATCH_NO] ,
										[BARCODE_DATA] ,
										[BARCODE_TYPE] ,
										[COMMENTS] ,
										[CREATE_USER] ,
										[CREATE_DATE]
									)
									SELECT  PLANT ,
											TARGET_WM AS WM_NO ,
											TARGET_ZONE AS ZONE_NO ,
											TARGET_DLOC AS DLOC ,
											SUPPLIER_NUM ,
											PART_NO ,
											PART_CNAME ,
											(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
											PART_UNITS ,
											PACKAGE_MODEL ,
											ISNULL(PACKAGE,1) ,
											MAX ,
											MIN ,
											0 AS SAFE_STOCK ,
											STOCKS =(CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT) ELSE 0 END),
											ISNULL(NUM,0) AS STOCKS_NUM,
											0 AS [FROZEN_STOCKS],
											AVAILBLE_STOCKS = ( CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT) ELSE 0 END),
											0 AS IS_BATCH ,
											ISNULL(NUM,0) % ISNULL(PACKAGE,1) AS [FRAGMENT_NUM] ,
											PART_CLS ,
											'' AS [IS_REPACK] ,
											'' AS [REPACK_ROUTE] ,
											[BATCH_NO] ,
											[BARCODE_DATA] ,
											[BARCODE_TYPE] ,
											[COMMENTS] ,
											'' AS [CREATE_USER] ,
											GETDATE() AS [CREATE_DATE]
									FROM    LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
									WHERE   TRAN_ID = @TranId
						END

						--LOG处理
						INSERT INTO @TranSuccess (TRAN_ID) VALUES (@TranId)
					END	
				END

				--261 移库, 304零件不良品出库，2出库
				ELSE IF (@TranType = 261 OR @TranType = 304 OR @TranType = 2) 
				BEGIN
					--批次管理的情况
					IF ( @IsBatch = 1)
					BEGIN
						--找到该批次所有箱条码，根据箱条码来批量更新
						SELECT TRAN_NO, PART_NO, TRAN_TYPE INTO #TempMove FROM LES.TM_WMM_TRAN_DETAILS WHERE BARCODE_DATA = @BarCode
						SELECT D.BARCODE_DATA INTO #TempMoveBarCode FROM LES.TM_WMM_TRAN_DETAILS D(NOLOCK) JOIN #TempMove T ON D.TRAN_NO = T.TRAN_NO AND D.PART_NO = T.PART_NO AND D.TRAN_TYPE = T.TRAN_TYPE
						DROP TABLE #TempMove

						--只更新冻结库存和可用库存
						UPDATE  LES.TT_WMS_STOCKS
						SET WM_NO = T.TARGET_WM,
							ZONE_NO = T.TARGET_ZONE,
							DLOC = T.TARGET_DLOC,																		                                        
							BUSINESS_PK = NULL,
							UPDATE_DATE = GETDATE()
						FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
						INNER JOIN LES.TT_WMS_STOCKS S ON S.BARCODE_DATA = T.BARCODE_DATA	
						WHERE S.BARCODE_DATA IN (SELECT BARCODE_DATA FROM #TempMoveBarCode)

						--LOG处理
						INSERT INTO @TranSuccess
						SELECT DISTINCT TRAN_ID FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE BARCODE_DATA IN (SELECT BARCODE_DATA FROM #TempMoveBarCode) 
						DROP TABLE #TempMoveBarCode
					END   
					ELSE		
					BEGIN
						--零件存在，往存在的记录上添加库存
						IF ( ISNULL(@StockId, '') <> '' )
						BEGIN
							UPDATE  LES.TT_WMS_STOCKS
								SET STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0),
									STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT),
									FRAGMENT_NUM = (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1),
									AVAILBLE_STOCKS = CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT),  								
									UPDATE_DATE = GETDATE()
								FROM    LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
										INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @StockId
								WHERE T.TRAN_ID = @TranId	
							--张恒 取消大小判断，由于增加负库存，所以不用判断
							--UPDATE  LES.TT_WMS_STOCKS
							--SET STOCKS_NUM = ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0),
							--	STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) THEN CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
							--			  ELSE 0 END),
							--	FRAGMENT_NUM = (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1),
							--	AVAILBLE_STOCKS = (CASE 
							--						WHEN (ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) >= ISNULL(S.PACKAGE,1) 
							--						THEN CAST((ISNULL(S.STOCKS_NUM,0) - ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT)
							--						--WHEN S.STOCKS_NUM - T.NUM - S.FROZEN_STOCKS * S.PACKAGE = 0 THEN 0 
							--						ELSE 0 
							--						END),  								
							--	UPDATE_DATE = GETDATE()
							--FROM    LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
							--		INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @StockId
							--WHERE T.TRAN_ID = @TranId	
						END
						ELSE
						--零件不存在，添加新的库存
						BEGIN
							INSERT  INTO [LES].[TT_WMS_STOCKS]([PLANT] ,
																[WM_NO] ,
																[ZONE_NO] ,
																[DLOC] ,
																[SUPPLIER_NUM] ,
																[PART_NO] ,
																[PART_CNAME] ,
																PART_NICKNAME,
																[PART_UNITS] ,
																[PACKAGE_MODEL] ,
																[PACKAGE] ,
																[MAX] ,
																[MIN] ,
																[SAFE_STOCK] ,
																[STOCKS] ,
																STOCKS_NUM,
																[FROZEN_STOCKS] ,
																[AVAILBLE_STOCKS] ,
																[IS_BATCH] ,
																[FRAGMENT_NUM] ,
																[PART_CLS] ,
																[IS_REPACK] ,
																[REPACK_ROUTE] ,
																[BATCH_NO] ,
																[BARCODE_DATA] ,
																[BARCODE_TYPE] ,
																[COMMENTS] ,
																[CREATE_USER] ,
																[CREATE_DATE])
													SELECT  PLANT ,
															WM_NO ,
															ZONE_NO ,
															DLOC ,
															SUPPLIER_NUM ,
															PART_NO ,
															PART_CNAME ,
															(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
															PART_UNITS ,
															PACKAGE_MODEL ,
															ISNULL(PACKAGE,1) ,
															MAX ,
															MIN ,
															0 AS SAFE_STOCK ,
															STOCKS =(0-CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT)),
															(0-ISNULL(NUM,0)) AS STOCKS_NUM,
															0 AS FROZEN_STOCKS ,
															AVAILBLE_STOCKS =(0-CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT)),
															0 AS IS_BATCH ,
															(0-ISNULL(NUM,0)) % ISNULL(PACKAGE,1) AS [FRAGMENT_NUM] ,
															PART_CLS ,
															'' AS [IS_REPACK] ,
															'' AS [REPACK_ROUTE] ,
															[BATCH_NO] ,
															[BARCODE_DATA] ,
															[BARCODE_TYPE] ,
															[COMMENTS] ,
															'' AS [CREATE_USER] ,
															GETDATE() AS [CREATE_DATE]
														FROM LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
														WHERE TRAN_ID = @TranId
						END
						--根据目标库位进行运输目标库位的库存
						IF ( ISNULL(@TargetStockId, '') <> '' )
						BEGIN
							UPDATE  LES.TT_WMS_STOCKS
							SET     STOCKS_NUM = ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0),									
									STOCKS = CAST((ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT),                                            
									AVAILBLE_STOCKS =CAST((ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT),									
									FRAGMENT_NUM = (ISNULL(S.FRAGMENT_NUM, 0) + ISNULL(T.NUM, 0)- ISNULL(S.FROZEN_STOCKS, 0) ) % ISNULL(S.PACKAGE,1),
									UPDATE_DATE = GETDATE(),											
									COMMENTS = T.COMMENTS
							FROM    LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
									INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @TargetStockId 
							WHERE T.TRAN_ID = @TranId	
								--张恒 取消大小判断，由于增加负库存，所以不用判断
								--UPDATE  LES.TT_WMS_STOCKS
								--SET     STOCKS_NUM = ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0),									
								--		STOCKS = (CASE WHEN (ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0)) >= ISNULL(S.PACKAGE,1) 
								--					THEN CAST((ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0) ) / ISNULL(S.PACKAGE,1) AS INT) 
								--					ELSE 0 END),                                            
								--		AVAILBLE_STOCKS = ( 
								--				CASE 
								--				WHEN (ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) >= ISNULL(S.PACKAGE,1) 
								--				THEN CAST((ISNULL(S.STOCKS_NUM, 0) + ISNULL(T.NUM, 0) - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT) 
								--				ELSE 0 
								--				END),									
								--		FRAGMENT_NUM = (ISNULL(S.FRAGMENT_NUM, 0) + ISNULL(T.NUM, 0)- ISNULL(S.FROZEN_STOCKS, 0) ) % ISNULL(S.PACKAGE,1),
								--		UPDATE_DATE = GETDATE(),											
								--		COMMENTS = T.COMMENTS
								--FROM    LES.TM_WMM_TRAN_DETAILS (NOLOCK) T
								--		INNER JOIN LES.TT_WMS_STOCKS S ON S.STOCK_IDENTITY = @TargetStockId 
								--WHERE T.TRAN_ID = @TranId							
						END
						ELSE
						--零件不存在，添加新的库存
						BEGIN
							INSERT  INTO [LES].[TT_WMS_STOCKS]
									( [PLANT] ,
										[WM_NO] ,
										[ZONE_NO] ,
										[DLOC] ,
										[SUPPLIER_NUM] ,
										[PART_NO] ,
										[PART_CNAME] ,
										PART_NICKNAME,
										[PART_UNITS] ,
										[PACKAGE_MODEL] ,
										[PACKAGE] ,
										[MAX] ,
										[MIN] ,
										[SAFE_STOCK] ,
										[STOCKS] ,
										STOCKS_NUM,
										[FROZEN_STOCKS] ,
										[AVAILBLE_STOCKS] ,
										[IS_BATCH] ,
										[FRAGMENT_NUM] ,
										[PART_CLS] ,
										[IS_REPACK] ,
										[REPACK_ROUTE] ,
										[BATCH_NO] ,
										[BARCODE_DATA] ,
										[BARCODE_TYPE] ,
										[COMMENTS] ,
										[CREATE_USER] ,
										[CREATE_DATE]
									)
									SELECT  PLANT ,
											TARGET_WM AS WM_NO ,
											TARGET_ZONE AS ZONE_NO ,
											TARGET_DLOC AS DLOC ,
											SUPPLIER_NUM ,
											PART_NO ,
											PART_CNAME ,
											(SELECT TOP 1 PART_NICKNAME FROM LES.TM_BAS_MAINTAIN_PARTS T WHERE T.PLANT=D.PLANT AND T.PART_NO=D.PART_NO),
											PART_UNITS ,
											PACKAGE_MODEL ,
											ISNULL(PACKAGE,1) ,
											MAX ,
											MIN ,
											0 AS SAFE_STOCK ,
											STOCKS =( CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT) ELSE 0 END),
											ISNULL(NUM,0) AS STOCKS_NUM,
											0 AS [FROZEN_STOCKS],
											AVAILBLE_STOCKS = ( CASE  WHEN ISNULL(NUM,0) >= ISNULL(PACKAGE,1) THEN CAST(ISNULL(NUM,0) / ISNULL(PACKAGE,1) AS INT) ELSE 0 END),
											0 AS IS_BATCH ,
											ISNULL(NUM,0) % ISNULL(PACKAGE,1) AS [FRAGMENT_NUM] ,
											PART_CLS ,
											'' AS [IS_REPACK] ,
											'' AS [REPACK_ROUTE] ,
											[BATCH_NO] ,
											[BARCODE_DATA] ,
											[BARCODE_TYPE] ,
											[COMMENTS] ,
											'' AS [CREATE_USER] ,
											GETDATE() AS [CREATE_DATE]
									FROM    LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
									WHERE   TRAN_ID = @TranId
						END

						--LOG处理
						INSERT INTO @TranSuccess (TRAN_ID) VALUES (@TranId)
					END	
				END	
				ELSE
				BEGIN
					INSERT INTO LES.TM_WMM_TRAN_DETAILS_LOG 
						SELECT [TRAN_NO]      ,[PLANT]      ,[BATCH_NO]      ,[PART_NO]      ,[BARCODE_DATA]      ,[WM_NO]
							,[ZONE_NO]      ,[DLOC]      ,[TARGET_WM]      ,[TARGET_ZONE]      ,[TARGET_DLOC]      ,[MEASURING_UNIT_NO]
							,[PACKAGE]      ,[MAX]      ,[MIN]      ,[NUM]      ,[BOX_NUM]      ,[TRAN_STATE]      ,[TRAN_DATE]      ,[SUPPLIER_NUM]
							,[PART_CNAME]      ,[BOX_PARTS]      ,[PICKUP_SEQ_NO]      ,[RDC_DLOC]      ,[ACTUAL_PACKAGE_QTY]      ,[INNER_LOCATION]
							,[LOCATION]      ,[STORAGE_LOCATION]      ,[INHOUSE_PACKAGE_MODEL]      ,[REQUIRED_PACKAGE_QTY]      ,[BARCODE_TYPE]
							,[REQUIRED_DATE]      ,[PACHAGE_TYPE]      ,[LINE_POSITION]      ,[RUNSHEET_NO]      ,[PART_NICKNAME]      ,[SUPPLIER_NAME]
							,[DOCK]      ,[SUPPLIER_SNAME]      ,[PACKAGE_MODEL]      ,[PART_CLS]      ,[PART_UNITS]      ,[IS_BATCH]      ,[TRAN_TYPE]
							,[CREATE_USER]      ,[CREATE_DATE]      ,[COMMENTS]      ,[UPDATE_USER]      ,[UPDATE_DATE]      ,[UPDATE_FLAG]
							, -1 AS PROCESS_RESULT, '暂不能解析该类型的操作' AS PROCESS_MESSAGE 
						FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE TRAN_ID = @TranId
					DELETE FROM LES.TM_WMM_TRAN_DETAILS WHERE TRAN_ID = @TranId 
				END

				--将交易信息记录到SAP接口_MM_移库接口表，上传给SAP；
				----6.翻包后，5翻包前，2出库
				--IF (@TranType = 6 OR @TranType = 5 OR @TranType = 2)
				IF (@TranType = 5 OR @TranType = 2)
				BEGIN
					--对于入库数据，采购入库已经通过收货入库接口上传；对于冲压车间的无单收货，业务人员会在SAP中直接录入生产数据，所以入库数据都不需要上传SAP；
					--对于普通移库单，已经通过[LES].[PROC_INTERFACE_LES_SAP_WMSTRANOUT]存储过程处理，所以该处只需要处理出库单和翻包单信息即可;
					--插入数据SEQ_ID,d.TRAN_ID,
					INSERT INTO LES.TI_WMS_TRAN_OUT(MATNR
												   ,WERKS
												   ,LGMNG
												   ,MEINS
												   ,LGORT
												   ,UMLGO
												   ,BWART
												   ,KONTO
												   ,KOSTL
												   ,AUFNR
												   ,WBS
												   ,BUDAT
												   ,OPTIM
												   ,OPRTR
												   ,CREATE_DATE
												   ,CREATE_USER
												   ,ZSERIAL)
											 SELECT PART_NO
												   ,PLANT
												   ,ISNULL(NUM,0)
												   ,PART_UNITS
												   ,CASE WHEN TRAN_TYPE IN (2,5) THEN ZONE_NO ELSE NULL END S_ZONE_NO
												   ,CASE WHEN TRAN_TYPE IN (2,5) THEN TARGET_ZONE ELSE NULL END O_ZONE_NO
												   ,'311'
												   ,NULL
												   ,NULL
												   ,NULL
												   ,NULL
												   ,CREATE_DATE
												   ,CREATE_DATE
												   ,CREATE_USER
												   ,GETDATE()
												   ,'admin'
												   ,LES.FN_GETRANDSTR(19)
											   FROM LES.TM_WMM_TRAN_DETAILS
											  WHERE TRAN_ID IN (SELECT DISTINCT TRAN_ID FROM @TranSuccess)
				END

				IF(EXISTS (SELECT 1 FROM @TranSuccess))
				BEGIN
					INSERT INTO LES.TM_WMM_TRAN_DETAILS_LOG
						SELECT [TRAN_NO]      
						      ,[PLANT]      
							  ,[BATCH_NO]      
							  ,[PART_NO]      
							  ,[BARCODE_DATA]      
							  ,[WM_NO]
							  ,[ZONE_NO]      
							  ,[DLOC]      
							  ,[TARGET_WM]      
							  ,[TARGET_ZONE]      
							  ,[TARGET_DLOC]      
							  ,[MEASURING_UNIT_NO]
							  ,[PACKAGE]      
							  ,[MAX]      
							  ,[MIN]      
							  ,[NUM]      
							  ,[BOX_NUM]      
							  ,[TRAN_STATE]      
							  ,[TRAN_DATE]      
							  ,[SUPPLIER_NUM]
							  ,[PART_CNAME]      
							  ,[BOX_PARTS]      
							  ,[PICKUP_SEQ_NO]      
							  ,[RDC_DLOC]      
							  ,[ACTUAL_PACKAGE_QTY]      
							  ,[INNER_LOCATION]
							  ,[LOCATION]      
							  ,[STORAGE_LOCATION]      
							  ,[INHOUSE_PACKAGE_MODEL]      
							  ,[REQUIRED_PACKAGE_QTY]      
							  ,[BARCODE_TYPE]
							  ,[REQUIRED_DATE]      
							  ,[PACHAGE_TYPE]      
							  ,[LINE_POSITION]      
							  ,[RUNSHEET_NO]      
							  ,[PART_NICKNAME]      
							  ,[SUPPLIER_NAME]
							  ,[DOCK]      
							  ,[SUPPLIER_SNAME]      
							  ,[PACKAGE_MODEL]      
							  ,[PART_CLS]      
							  ,[PART_UNITS]      
							  ,[IS_BATCH]      
							  ,[TRAN_TYPE]
							  ,[CREATE_USER]      
							  ,[CREATE_DATE]      
							  ,[COMMENTS]      
							  ,[UPDATE_USER]      
							  ,[UPDATE_DATE]      
							  ,[UPDATE_FLAG]
							  ,0 AS PROCESS_RESULT
							  ,NULL AS PROCESS_MESSAGE 
						  FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE TRAN_ID IN (SELECT DISTINCT TRAN_ID FROM @TranSuccess)
					SET @insertNum = @@ROWCOUNT
						  	
					DELETE  FROM LES.TM_WMM_TRAN_DETAILS WHERE TRAN_ID IN (SELECT DISTINCT TRAN_ID FROM @TranSuccess)
					SET @deleteNum = @@ROWCOUNT
				END
				COMMIT TRAN
			END TRY
			BEGIN CATCH
				ROLLBACK TRAN;

				INSERT INTO LES.TM_WMM_TRAN_DETAILS_LOG 
					SELECT [TRAN_NO]      
					      ,[PLANT]      
						  ,[BATCH_NO]      
						  ,[PART_NO]      
						  ,[BARCODE_DATA]      
						  ,[WM_NO]
						  ,[ZONE_NO]      
						  ,[DLOC]      
						  ,[TARGET_WM]      
						  ,[TARGET_ZONE]      
						  ,[TARGET_DLOC]      
						  ,[MEASURING_UNIT_NO]
						  ,[PACKAGE]      
						  ,[MAX]      
						  ,[MIN]      
						  ,[NUM]      
						  ,[BOX_NUM]      
						  ,[TRAN_STATE]      
						  ,[TRAN_DATE]      
						  ,[SUPPLIER_NUM]
						  ,[PART_CNAME]      
						  ,[BOX_PARTS]      
						  ,[PICKUP_SEQ_NO]      
						  ,[RDC_DLOC]      
						  ,[ACTUAL_PACKAGE_QTY]      
						  ,[INNER_LOCATION]
						  ,[LOCATION]      
						  ,[STORAGE_LOCATION]      
						  ,[INHOUSE_PACKAGE_MODEL]      
						  ,[REQUIRED_PACKAGE_QTY]      
						  ,[BARCODE_TYPE]
						  ,[REQUIRED_DATE]      
						  ,[PACHAGE_TYPE]      
						  ,[LINE_POSITION]      
						  ,[RUNSHEET_NO]      
						  ,[PART_NICKNAME]      
						  ,[SUPPLIER_NAME]
						  ,[DOCK]      
						  ,[SUPPLIER_SNAME]      
						  ,[PACKAGE_MODEL]      
						  ,[PART_CLS]      
						  ,[PART_UNITS]      
						  ,[IS_BATCH]      
						  ,[TRAN_TYPE]
						  ,[CREATE_USER]      
						  ,[CREATE_DATE]      
						  ,[COMMENTS]      
						  ,[UPDATE_USER]      
						  ,[UPDATE_DATE]      
						  ,[UPDATE_FLAG]
						  ,-1 AS PROCESS_RESULT
						  ,ERROR_MESSAGE() AS PROCESS_MESSAGE 
					  FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) WHERE TRAN_ID=@TranId

				DELETE FROM LES.TM_WMM_TRAN_DETAILS WHERE TRAN_ID=@TranId

			END CATCH

			begin try
			if(@insertNum <> @deleteNum)	
			begin
				insert into [LES].[OutPutTrace](Operation) 
				values ('库存更新服务：同步数量不一样:插入TRAN_DETAILS_LOG:'+cast(@insertNum as nvarchar(50))+';删除TRAN_DETAILS:'+cast(@deleteNum as nvarchar(50)))
			end	
			end try
			begin catch
			end catch

			FETCH  NEXT  FROM STOCK_CUR INTO @TranId,@TranType,@BatchNo,@PartNo,@Plant,@WmNo,@ZoneNo,@DLOC,@TargetWm,@TargetZone,@TargetDloc,@BarCode,@IsBatch
		END

		CLOSE  STOCK_CUR
		DEALLOCATE STOCK_CUR

        SET NOCOUNT OFF;
    END