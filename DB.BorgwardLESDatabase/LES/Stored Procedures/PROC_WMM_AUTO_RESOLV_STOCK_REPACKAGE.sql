

CREATE PROCEDURE [LES].[PROC_WMM_AUTO_RESOLV_STOCK_REPACKAGE]
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
			@TargetStockId INT,
			@TranState INT,
			@sIsBatch INT--来源存储区是否批次管理
		
		--先补全TM_WMM_TRAN_DETAILS表里应有的数据
		IF EXISTS(SELECT 1 FROM LES.TM_WMM_TRAN_DETAILS(NOLOCK) T
                     INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON (S.PART_NO=T.PART_NO AND S.ZONE_NO=T.ZONE_NO AND S.PLANT=T.PLANT)
					      WHERE UPDATE_FLAG=0 AND TRAN_STATE in (20,30))
		BEGIN
			UPDATE LES.TM_WMM_TRAN_DETAILS 
			  SET PACKAGE = (CASE WHEN ISNULL(T.PACKAGE, 0) = 0 THEN ISNULL(S.INBOUND_PACKAGE,1) ELSE T.PACKAGE END),
				   PACKAGE_MODEL = S.INBOUND_PACKAGE_MODEL,
				   MAX = S.MAX,
				   MIN= S.MIN,
				   PART_CNAME = S.PART_CNAME,
				   PART_NICKNAME = S.PART_NICKNAME,
				   PART_UNITS = CASE WHEN ISNULL(T.MEASURING_UNIT_NO,'')='' Then MP.PART_UNITS Else T.MEASURING_UNIT_NO END,
				   IS_BATCH = S.IS_BATCH,
				   PART_CLS = S.PART_CLS,
				   UPDATE_FLAG=1
			  FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
			 INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON S.PART_NO=T.PART_NO AND S.ZONE_NO=T.ZONE_NO AND T.PLANT=S.PLANT
			 INNER JOIN LES.TM_BAS_MAINTAIN_PARTS (NOLOCK) MP ON T.PART_NO=MP.PART_NO AND T.PLANT=MP.PLANT			
			 WHERE T.UPDATE_FLAG=0 AND TRAN_STATE =30

			UPDATE LES.TM_WMM_TRAN_DETAILS 
			  SET PACKAGE = (CASE WHEN ISNULL(T.PACKAGE, 0) = 0 THEN ISNULL(S.INBOUND_PACKAGE,1) ELSE T.PACKAGE END),
				   PACKAGE_MODEL = S.INBOUND_PACKAGE_MODEL,
				   MAX = S.MAX,
				   MIN= S.MIN,
				   PART_CNAME = S.PART_CNAME,
				   PART_NICKNAME = S.PART_NICKNAME,
				   PART_UNITS = CASE WHEN ISNULL(T.MEASURING_UNIT_NO,'')='' Then MP.PART_UNITS Else T.MEASURING_UNIT_NO END,
				   IS_BATCH = S.IS_BATCH,
				   PART_CLS = S.PART_CLS,
				   UPDATE_FLAG=1
			  FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) T 
			 INNER JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON S.PART_NO=T.PART_NO AND S.ZONE_NO=T.TARGET_ZONE AND T.PLANT=S.PLANT
			 INNER JOIN LES.TM_BAS_MAINTAIN_PARTS (NOLOCK) MP ON T.PART_NO=MP.PART_NO AND T.PLANT=MP.PLANT			
			 WHERE T.UPDATE_FLAG=0 AND TRAN_STATE =20
		END
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
												   ,TRAN_STATE
											   FROM LES.TM_WMM_TRAN_DETAILS (NOLOCK) 
				                              WHERE TRAN_STATE in (20,30) AND UPDATE_FLAG = 1 
				                              ORDER BY TRAN_ID ASC
		
		OPEN STOCK_CUR
		FETCH  NEXT  FROM STOCK_CUR INTO @TranId,@TranType,@BatchNo,@PartNo,@Plant,@WmNo,@ZoneNo,@DLOC,@TargetWm,@TargetZone,@TargetDloc,@BarCode,@IsBatch,@TranState
		WHILE( @@fetch_status = 0 )
		BEGIN
			
			DELETE FROM @TranSuccess

			set @StockId= null
			set @TargetStockId= null

			--SotckId只对非批次管理的情况才会用到
			SELECT @StockId=STOCK_IDENTITY 
			,@sIsBatch = IS_BATCH 
			  FROM LES.TT_WMS_STOCKS S ( NOLOCK ) 
			 WHERE PART_NO=@PartNo 
			   AND S.ZONE_NO=@ZoneNo
        
			SELECT  @TargetStockId=STOCK_IDENTITY 
			  FROM LES.TT_WMS_STOCKS S ( NOLOCK ) 
			 WHERE PART_NO=@PartNo 
			   AND S.ZONE_NO=@TargetZone
			
			BEGIN TRAN
			BEGIN TRY
				IF (@TranType = 2) 
				BEGIN
					IF(@TranState = 20)---翻包后
						BEGIN
							IF ( @IsBatch = 1)
								BEGIN
									INSERT  INTO [LES].[TT_WMS_STOCKS]
									([PLANT] ,
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
										SELECT  
										PLANT ,
										TARGET_WM ,
										TARGET_ZONE ,
										TARGET_DLOC ,
										SUPPLIER_NUM ,
										PART_NO ,
										PART_CNAME ,
										D.PART_NICKNAME,
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
										1 AS IS_BATCH ,
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
									--LOG处理
									INSERT INTO @TranSuccess
									SELECT @TranId
								END   
							ELSE
								BEGIN
									--零件存在，往存在的记录上添加库存
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
																		TARGET_WM ,
																		TARGET_ZONE ,
																		TARGET_DLOC ,
																		SUPPLIER_NUM ,
																		PART_NO ,
																		PART_CNAME ,
																		PART_NICKNAME,
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
																		'' ,
																		[BARCODE_TYPE] ,
																		[COMMENTS] ,
																		'' AS [CREATE_USER] ,
																		GETDATE() AS [CREATE_DATE]
																	FROM LES.TM_WMM_TRAN_DETAILS D (NOLOCK)
																	WHERE TRAN_ID = @TranId
									END
									--LOG处理
									INSERT INTO @TranSuccess (TRAN_ID) VALUES (@TranId)
								END	
						END
					ELSE IF(@TranState = 30)--翻包前
						BEGIN
							--批次管理的情况
							IF ( @sIsBatch = 1)
							BEGIN
								DELETE FROM LES.TT_WMS_STOCKS WHERE BARCODE_DATA = @BarCode
								--LOG处理
								INSERT INTO @TranSuccess
								SELECT @TranId
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
																		PART_NICKNAME,
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
							--LOG处理
							INSERT INTO @TranSuccess (TRAN_ID) VALUES (@TranId)
						END	
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
				IF (@TranType = 2 and @TranState = 20)
				BEGIN
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