CREATE PROC [LES].[PROC_WMM_OUTPUT_SUBMITBOX_NoSheet]
(
	@OUTPUT_ID INT,
	@PART_NO NVARCHAR(50),
	@BARCODE_DATA NVARCHAR(50),
	@ActualQty INT,
	@Create_User NVARCHAR(50)
)
AS
BEGIN
	BEGIN TRAN
	BEGIN TRY
		SET @ActualQty = ISNULL(@ActualQty, 0)
		IF EXISTS(SELECT * FROM [LES].[TT_WMM_OUTPUT_DETAIL] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OUTPUT_ID AND [PART_NO] = @PART_NO)
			BEGIN
				IF EXISTS(SELECT 1 FROM [LES].[TM_WMM_TRAN_DETAILS] WITH (NOLOCK) WHERE [BARCODE_DATA] = @BARCODE_DATA AND [TRAN_TYPE] = 2 AND [TRAN_STATE] = 0)
					BEGIN
						DECLARE @LASTNUM DECIMAL(18,2)
						SELECT @LASTNUM=NUM FROM [LES].[TM_WMM_TRAN_DETAILS] WITH (NOLOCK) WHERE [BARCODE_DATA] = @BARCODE_DATA AND [TRAN_TYPE] = 2 AND [TRAN_STATE] = 0
			
						UPDATE [LES].[TT_WMM_OUTPUT_DETAIL] WITH (ROWLOCK)
						SET [REQUIRED_QTY] = ISNULL([ACTUAL_QTY], 0) - @LASTNUM + @ActualQty,
							[ACTUAL_QTY] = ISNULL([ACTUAL_QTY], 0) - @LASTNUM + @ActualQty
						WHERE [OUTPUT_ID] = @OUTPUT_ID AND [PART_NO] = @PART_NO
			
						DECLARE @OUTPUT_NO NVARCHAR(50)
						SELECT @OUTPUT_NO = [OUTPUT_NO] FROM [LES].[TT_WMM_OUTPUT] WITH (NOLOCK) WHERE [OUTPUT_ID] = @OUTPUT_ID

						UPDATE [LES].[TM_WMM_TRAN_DETAILS] WITH (ROWLOCK)
						SET [NUM] = @ActualQty
						WHERE [TRAN_NO] = @OUTPUT_NO AND [BARCODE_DATA] = @BARCODE_DATA

						INSERT INTO [LES].[OutPutTrace]([OUTPUT_No], [OUTPUT_ID], [PART_NO], [BARCODE_DATA], [ActualQty], [Create_User], [Create_Time], [Operation]) 
						VALUES ('', @OUTPUT_ID, @PART_NO, @BARCODE_DATA, @ActualQty, @Create_User, GETDATE(), 'UpdateNum')
					END
				ELSE
					BEGIN
						UPDATE [LES].[TT_WMM_OUTPUT_DETAIL] WITH (ROWLOCK)
						SET [REQUIRED_QTY] = ISNULL([ACTUAL_QTY], 0) + @ActualQty,
							[ACTUAL_QTY] = ISNULL([ACTUAL_QTY], 0) + @ActualQty,
							[REQUIRED_BOX_NUM] = ISNULL([ACTUAL_BOX_NUM], 0) + 1,
							[ACTUAL_BOX_NUM] = ISNULL([ACTUAL_BOX_NUM], 0) + 1
						WHERE [OUTPUT_ID] = @OUTPUT_ID AND [PART_NO] = @PART_NO

						INSERT INTO [LES].[TM_WMM_TRAN_DETAILS]
						(
							[BARCODE_DATA],
							[PART_NO],
							[BOX_NUM],
							[NUM],
							[PLANT],
							[WM_NO],
							[ZONE_NO],
							[DLOC],
							[PACKAGE_MODEL],
							[PACKAGE],
							[TRAN_STATE],
							[SUPPLIER_NUM],
							[BOX_PARTS],
							[PART_CNAME],
							[MEASURING_UNIT_NO],
							[TRAN_TYPE],
							[TRAN_NO],
							[CREATE_DATE],
							[CREATE_USER],
							[TRAN_DATE],
							[TARGET_WM],
							[TARGET_ZONE],
							[TARGET_DLOC]
						)
						SELECT
							@BARCODE_DATA,			--BARCODE_DATA
							@PART_NO,				--PART_NO
							1,						--BOX_NUM
							@ActualQty,				--Num
							A.[PLANT],				--Plant
							A.[WM_NO],				--WM_NO
							A.[ZONE_NO],			--ZONE_NO
							B.[DLOC],				--Dloc
							B.[PACKAGE_MODEL],		--PACKAGE_MODEL
							B.[PACKAGE],			--PACKAGE
							0,						--Tran_State
							A.[SUPPLIER_NUM],		--Supplier_Num
							B.[BOX_PARTS],			--BOX_PARTS
							B.[PART_CNAME],			--Part_Cname
							B.[MEASURING_UNIT_NO],	--MEASURING_UNIT_NO
							2,						--Tran_Type
							A.[OUTPUT_NO],			--Tran_No
							GETDATE(),				--Create_Date
							@Create_User,			--Create_User
							GETDATE(),				--Tran_Date
							C.[WM_NO],
							C.[ZONE_NO],
							C.[DLOC]
						FROM [LES].[TT_WMM_OUTPUT] A WITH (NOLOCK)
						INNER JOIN [LES].[TT_WMM_OUTPUT_DETAIL] B WITH (NOLOCK) ON A.[OUTPUT_ID] = B.[OUTPUT_ID] AND B.[PART_NO] = @PART_NO
						LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] C WITH (NOLOCK) ON C.[ZONE_NO] = A.[PLANT_ZONE] AND C.[PART_NO] = @PART_NO
						WHERE A.[OUTPUT_ID] = @OUTPUT_ID

						INSERT INTO [LES].[OutPutTrace] ([OUTPUT_No], [OUTPUT_ID], [PART_NO], [BARCODE_DATA], [ActualQty], [Create_User], [Create_Time], [Operation]) 
						VALUES ('', @OUTPUT_ID, @PART_NO, @BARCODE_DATA, @ActualQty, @Create_User, GETDATE(), 'Add')
					END
			END
		ELSE
			BEGIN
				INSERT INTO [LES].[TT_WMM_OUTPUT_DETAIL]
				(
					[OUTPUT_ID],
					[REQUIRED_BOX_NUM],
					[REQUIRED_QTY],
					[ACTUAL_BOX_NUM],
					[ACTUAL_QTY],
					[CREATE_DATE],
					[PART_CNAME],
					[PART_NO],
					[MEASURING_UNIT_NO],
					[PLANT],
					[WM_NO],
					[ZONE_NO],
					[DLOC], 
					[CREATE_USER],
					[PACKAGE],
					[PACKAGE_MODEL],
					[SUPPLIER_NUM],
					[TARGET_WM],
					[TARGET_ZONE],
					[TARGET_DLOC]
				)
				SELECT
					@OUTPUT_ID,					--OUTPUT_ID
					1,							--REQUIRED_BOX_NUM
					@ActualQty,					--REQUIRED_QTY
					1,							--Actual_Box_Num
					@ActualQty,					--Actual_Qty
					GETDATE(),					--Create_Date
					B.[PART_CNAME],				--Part_Cname
					@PART_NO,					--Part_No
					B.[PART_UNITS],				--MEASURING_UNIT_NO
					A.[PLANT],					--Plant
					A.[WM_NO],					--Wm_No
					A.[ZONE_NO],				--Zone_No
					B.[DLOC],					--DLOC
					@Create_User,				--Create_User
					B.[INBOUND_PACKAGE],		--PACKAGE
					B.[INBOUND_PACKAGE_MODEL],	--PACKAGE_MODEL
					B.[SUPPLIER_NUM],			--SUPPLIER_NUM
					C.[WM_NO],					--TARGET_WM
					C.[ZONE_NO],				--TARGET_ZONE
					C.[DLOC]					--TARGET_DLOC
				FROM [LES].[TT_WMM_OUTPUT] A WITH (NOLOCK)
				INNER JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON B.[ZONE_NO] = A.[ZONE_NO] AND B.[PART_NO] = @PART_NO
				LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] C WITH (NOLOCK) ON C.[ZONE_NO] = A.[PLANT_ZONE] AND C.[PART_NO] = @PART_NO
				WHERE A.[OUTPUT_ID] = @OUTPUT_ID
		
				INSERT INTO [LES].[TM_WMM_TRAN_DETAILS]
				(
					[BARCODE_DATA],
					[PART_NO],
					[BOX_NUM],
					[NUM],
					[PLANT],
					[WM_NO],
					[ZONE_NO],
					[DLOC],
					[PACKAGE_MODEL],
					[PACKAGE],
					[TRAN_STATE],
					[SUPPLIER_NUM],
					[PART_CNAME],
					[MEASURING_UNIT_NO],
					[TRAN_TYPE],
					[TRAN_NO],
					[CREATE_DATE],
					[CREATE_USER],
					[TRAN_DATE],
					[TARGET_WM],
					[TARGET_ZONE],
					[TARGET_DLOC]
				)
				SELECT
					@BARCODE_DATA,				--BARCODE_DATA
					@PART_NO,					--PART_NO
					1,							--BOX_NUM
					@ActualQty,					--Num
					A.[PLANT],					--Plant
					A.[WM_NO],					--WM_NO
					A.[ZONE_NO],				--ZONE_NO
					B.[DLOC],					--Dloc
					B.[INBOUND_PACKAGE_MODEL],	--PACKAGE_MODEL
					B.[INBOUND_PACKAGE],		--PACKAGE
					0,							--Tran_State
					A.[SUPPLIER_NUM],			--Supplier_Num
					B.[PART_CNAME],				--Part_Cname
					B.[PART_UNITS],				--MEASURING_UNIT_NO
					2,							--Tran_Type
					A.[OUTPUT_NO],				--Tran_No
					GETDATE(),					--Create_Date
					@Create_User,				--Create_User
					GETDATE(),					--Tran_Date
					C.[WM_NO],					--TARGET_WM
					C.[ZONE_NO],				--TARGET_ZONE
					C.[DLOC]					--TARGET_DLOC
				FROM [LES].[TT_WMM_OUTPUT] A WITH (NOLOCK)
				INNER JOIN [LES].[TM_BAS_PARTS_STOCK] B WITH (NOLOCK) ON B.[ZONE_NO] = A.[ZONE_NO] AND B.[PART_NO] = @PART_NO
				LEFT JOIN [LES].[TM_BAS_PARTS_STOCK] C WITH (NOLOCK) ON C.[ZONE_NO] = A.[PLANT_ZONE] AND C.[PART_NO] = @PART_NO
				WHERE A.[OUTPUT_ID] = @OUTPUT_ID

				INSERT INTO [LES].[OutPutTrace] ([OUTPUT_No], [OUTPUT_ID], [PART_NO], [BARCODE_DATA], [ActualQty], [Create_User], [Create_Time], [Operation]) 
				VALUES ('', @OUTPUT_ID, @PART_NO, @BARCODE_DATA, @ActualQty, @Create_User, GETDATE(), 'AddFirst')
			END
	END TRY   
	BEGIN CATCH   
		IF @@trancount > 0  
			BEGIN 
				ROLLBACK TRAN  
				INSERT INTO [LES].[OutPutTrace] ([OUTPUT_No], [OUTPUT_ID], [PART_NO], [BARCODE_DATA], [ActualQty], [Create_User], [Create_Time], [Operation]) 
				VALUES ('', @OUTPUT_ID, @PART_NO, @BARCODE_DATA, @ActualQty, @Create_User, GETDATE(), 'RollBack')
				EXEC [LES].[PROC_WMM_OUTPUT_SUBMITBOX_NoSheet] @OUTPUT_ID, @PART_NO, @BARCODE_DATA, @ActualQty, @Create_User
			END
	END CATCH

	IF @@trancount > 0   
		COMMIT TRAN  
END