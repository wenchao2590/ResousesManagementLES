﻿
/********************************************************************/
/*   Project Name:  TWD						                        */
/*   Program Name:  [PROC_TWD_MANUAL_GEN_RECKONING_SHEET]           */
/*   Called By:     界面 and service                                */
/*   Author:        xhj												*/
/*   Modify:2014-08-11 Author: Andy  Note: TWD测试状态的零件类生成的 */
/*          送货单不需发给供应商、服务商								*/
/*   Modify:2014-12-05 Author: shianyuan  Note: 添加组单零件类拆单   */
/********************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_MANUAL_GEN_RECKONING_SHEET_PACKAGEMODEL]
(
	@runsheetSN int
)
AS
BEGIN
	DECLARE @INTERFACE_ID int
	DECLARE @Sequence int
	DECLARE @SUPPLIER_TRANS nvarchar(100)
	DECLARE @ORDER_NO nvarchar(10)
	DECLARE	@result int = 0
	DECLARE @WAREHOUSE nvarchar(40)
	DECLARE @PLANT			NVARCHAR(30)
	DECLARE @SUPPLIER_NUM			NVARCHAR(8)
	DECLARE @REC_TYPE			int 
	DECLARE @TWD_RUNSHEET_NO NVARCHAR(22)
	DECLARE @DELIVERY_ORDER_SN NVARCHAR(12)
	DECLARE @BOX_PARTS NVARCHAR(12)
	DECLARE @LOAD_PLACE NVARCHAR(50)
	DECLARE @ASSEMBLY_LINE NVARCHAR(12)
	DECLARE @SEND_STATUS int
	DECLARE @WMS_SEND_STATUS int
	DECLARE @BOX_PARTS_STATE int

	-- 临时数据表
	DECLARE @Table TABLE
		(
			[RUNSHEET_DETAIL_ID][INT] NOT NULL,
			[TWD_RUNSHEET_SN]	[int] NOT NULL,
			[RECKONING_SN]		[int] NULL,
			[PLANT]				[nvarchar](5) NOT NULL,
			[ASSEMBLY_LINE]		[nvarchar](10) NOT NULL,
			[SUPPLIER_NUM]		[nvarchar](12) NOT NULL,
			[PART_NO]			[nvarchar](20) NOT NULL,
			[PART_CNAME]		[nvarchar](300) NULL,
			[PART_ENAME]		[nvarchar](300) NULL,
			[DOCK]				[nvarchar](10) NOT NULL,
			[BOX_PARTS]			[nvarchar](10) NOT NULL,
			[SEQUENCE_NO]		[int] NULL,
			[PICKUP_SEQ_NO]		[int] NULL,
			[INHOUSE_PACKAGE]	[int] NOT NULL,
			[MEASURING_UNIT_NO]	[nvarchar](1) NOT NULL,
			[INBOUND_PACKAGE_MODEL]			[nvarchar](30) NULL,
			[PACK_COUNT]		[int] NOT NULL,
			[REQUIRED_INBOUND_PACKAGE]		[int] NULL,
			[REQUIRED_INBOUND_PACKAGE_QTY]	[int] NULL,
			[ACTUAL_INBOUND_PACKAGE]		[int] NULL,
			[ACTUAL_INBOUND_PACKAGE_QTY]	[int] NULL,
			[ORDER_NO]			[nvarchar](50) NULL,
			[ITEM_NO]			[nvarchar](50) NULL,
			[TWD_RUNSHEET_NO]	[varchar](22) NULL,
			[COMMENTS]			[nvarchar](200) NULL
		)
		
	SET @INTERFACE_ID = 0

	--xuehaijun ,add 20141127
	DECLARE DETAIL_CRSR CURSOR  FAST_FORWARD  FOR 
	SELECT DISTINCT [PLANT],[ASSEMBLY_LINE],[SUPPLIER_NUM_SHEET],[BOX_PARTS_SHEET]
	FROM LES.TT_TWD_RUNSHEET_DETAIL
	WHERE TWD_RUNSHEET_SN = @RUNSHEETSN
	
	OPEN DETAIL_CRSR

	SET XACT_ABORT ON
	BEGIN TRY
	BEGIN TRANSACTION 
		FETCH NEXT FROM DETAIL_CRSR INTO @PLANT, @ASSEMBLY_LINE , @SUPPLIER_NUM ,@BOX_PARTS
	
		WHILE( @@FETCH_STATUS = 0 )
		BEGIN
			
			--判断是否还有未结算的明细	
			SET @INTERFACE_ID=-1
			SELECT @INTERFACE_ID=min([RUNSHEET_DETAIL_ID])
			FROM [LES].[TT_TWD_RUNSHEET_DETAIL]
			WHERE [RECKONING_SN] IS NULL
				AND [TWD_RUNSHEET_SN] = @runsheetSN
				AND [SUPPLIER_NUM_SHEET] = @SUPPLIER_NUM AND [BOX_PARTS_SHEET] = @BOX_PARTS

			--SELECT @SUPPLIER_TRANS = PARAMETER_VALUE FROM LES.TS_SYS_CONFIG WHERE PARAMETER_NAME = 'SUPPLIER_TRANS'
			SELECT @TWD_RUNSHEET_NO=TWD_RUNSHEET_NO, @WAREHOUSE=[DELIVERY_LOCATION], @SUPPLIER_TRANS= TRANS_SUPPLIER_NUM
			FROM [LES].[TT_TWD_RUNSHEET]
			WHERE [TWD_RUNSHEET_SN] = @runsheetSN   --Andy:添加ASSEMBLY_LINE取值

			SELECT @LOAD_PLACE = MAX([PLACE_OF_DELIVERY])
			FROM [LES].[TM_TWD_BOX_PARTS]
			WHERE [PLANT]=@PLANT
				AND [BOX_PARTS]=@BOX_PARTS
	
			--Andy:获取零件类状态
			SELECT @BOX_PARTS_STATE = BOX_PARTS_STATE FROM LES.TM_TWD_BOX_PARTS WHERE PLANT = @PLANT and ASSEMBLY_LINE = @ASSEMBLY_LINE and SUPPLIER_NUM = @SUPPLIER_NUM and BOX_PARTS = @BOX_PARTS
			IF @BOX_PARTS_STATE = 2   --测试零件
			BEGIN
				--测试零件，TWD测试状态的零件类生成的送货单不需发给供应商、服务商
				SET @SEND_STATUS = 2   --已处理
				SET @WMS_SEND_STATUS = 3 --发送成功
			END	
				ELSE
			BEGIN
				--正常零件
				SET @SEND_STATUS=1   --等待发送
				SET @WMS_SEND_STATUS = 1   --等待发送
			END
	
			-- 分单原则：
			-- 1. 满10条明细一单
			-- 2. 不同ORDER_NO号一单
			-- 3. 不同的[SUPPLIER_NUM_SHEET]一单（原供应商编号）
			-- 4. 不同的[BOX_PARTS_SHEET]一单（原零件类）

			--循环临时表，10个零件生成一张送货单
			WHILE(@INTERFACE_ID <>-1) 
			BEGIN
				EXEC  @Sequence = [LES].[PROC_JIS_GET_NEXT_SEQUENCE] 'JIS_RECKONING_SHEET'
	
				EXEC LES.PROC_TWD_GET_NEXT_RECKONING_SEQUENCE @SUPPLIER_NUM, @DELIVERY_ORDER_SN OUTPUT
	
				--插入最大10条明细, 由于不同订单不能在一个送货单上，有的送货单会有不足10条的记录
				-- 处理ORDER_NO不为空的
				DELETE FROM @Table
				INSERT INTO @Table([RUNSHEET_DETAIL_ID],[TWD_RUNSHEET_SN],[RECKONING_SN],[PLANT],[ASSEMBLY_LINE],[SUPPLIER_NUM],[PART_NO],[PART_CNAME],[PART_ENAME],[DOCK],[BOX_PARTS],[SEQUENCE_NO],[PICKUP_SEQ_NO],[INHOUSE_PACKAGE],[MEASURING_UNIT_NO],[INBOUND_PACKAGE_MODEL],[PACK_COUNT],[REQUIRED_INBOUND_PACKAGE],[REQUIRED_INBOUND_PACKAGE_QTY],[ACTUAL_INBOUND_PACKAGE],[ACTUAL_INBOUND_PACKAGE_QTY],[ORDER_NO],[ITEM_NO],[TWD_RUNSHEET_NO],[COMMENTS])
				SELECT TOP 10 [RUNSHEET_DETAIL_ID],@runsheetSN, @Sequence, T.PLANT, T.ASSEMBLY_LINE, T.SUPPLIER_NUM, T.PART_NO, T.[PART_CNAME],T.PART_ENAME,T.DOCK,T.BOX_PARTS,0,0,T.INBOUND_PACKAGE
					,T.MEASURING_UNIT_NO,T.INBOUND_PACKAGE_MODEL,T.PACK_COUNT,T.REQUIRED_INBOUND_PACKAGE,T.REQUIRED_INBOUND_PACKAGE_QTY,T.ACTUAL_INBOUND_PACKAGE,T.ACTUAL_INBOUND_PACKAGE_QTY
					,T.ORDER_NO,T.ITEM_NO,@runsheetSN,''				
				FROM [LES].[TT_TWD_RUNSHEET_DETAIL] T
				WHERE [RECKONING_SN] IS NULL
					AND [TWD_RUNSHEET_SN] = @runsheetSN
					AND [SUPPLIER_NUM_SHEET] = @SUPPLIER_NUM
					AND [BOX_PARTS_SHEET] = @BOX_PARTS
				ORDER BY ORDER_NO DESC, RUNSHEET_DETAIL_ID

				-- 获取最大的OrderNo号码
				SELECT @ORDER_NO = MAX(ORDER_NO)
				FROM @Table
	
				--插入结算主表
				INSERT	INTO LES.TT_TWD_RECKONING_SHEETS
				(
					RECKONING_SN,
					TWD_RUNSHEET_SN,
					RECKONING_NO,
					DELIVERY_ORDER,
					ORDER_NO,
					RECEIVE_LOCATION,
					RECEIVED_DATE,
					SUPPLIER_NUM,
					SUPPLIER_TRANS,
					WMS_SEND_STATUS,
					TRANS_TYPE,
					WMS_SEND_TIME,
					UNLOAD_PLACE,
					LOAD_PLACE,
					PULL_TYPE,
					PULL_DETAIL,
					SUPPLY_STATUS,
					SUPPLY_CONFIRM_DATE,
					FIRST_CONFIRM_STATUS,
					FIRST_CONFIRM_DATE,
					SECOND_CONFIRM_STATUS,
					SECOND_CONFIRM_DATE,
					COMMENTS,
					UPDATE_DATE,
					UPDATE_USER,
					CREATE_DATE,
					CREATE_USER,
					[WHAREHOUSE]
					 ,[EXPECTED_ARRIVAL_TIME]
					 ,[SEND_TIME]
					 ,[SEND_STATUS]
					 ,[TWD_RUNSHEET_NO]
					 ,[PLANT])
				SELECT 
						@Sequence,
						@runsheetSN,
						@PLANT + RIGHT(CONVERT(varchar(4) , getdate(), 112 ),2) +@SUPPLIER_NUM+ SUBSTRING('000000' + CONVERT(NVARCHAR, @Sequence), LEN(CONVERT(NVARCHAR, @Sequence))+1 , 6) ,
						@DELIVERY_ORDER_SN, 
						@ORDER_NO,
						NULL,
						NULL,
						@SUPPLIER_NUM,
						@SUPPLIER_TRANS,
						@WMS_SEND_STATUS,
						'TWD',
						NULL,
						@LOAD_PLACE,
						@WAREHOUSE,
						1,
						NULL,
						NULL,
						NULL,
						NULL,
						NULL,
						NULL,
						NULL,
						NULL,
						NULL,
						NULL,			
						GETDATE(),
						'Auto',
						@WAREHOUSE
						,[EXPECTED_ARRIVAL_TIME]
					   ,NULL
					   ,@SEND_STATUS
					   ,[TWD_RUNSHEET_NO]
					   ,[PLANT]
				FROM [LES].[TT_TWD_RUNSHEET] WHERE  [TWD_RUNSHEET_SN] = @runsheetSN

				--处理ORDER_NO为空的，更新10条明细已结算
				IF EXISTS(SELECT TOP 1 1 FROM @Table)
				BEGIN
					INSERT INTO [LES].[TT_TWD_RECKONING_SHEETS_DETAIL]([TWD_RUNSHEET_SN],[RECKONING_SN],[PLANT],[ASSEMBLY_LINE],[SUPPLIER_NUM],[PART_NO],[PART_CNAME],[PART_ENAME],[DOCK],[BOX_PARTS],[SEQUENCE_NO],[PICKUP_SEQ_NO],[INHOUSE_PACKAGE],[MEASURING_UNIT_NO],[INBOUND_PACKAGE_MODEL],[PACK_COUNT],[REQUIRED_INBOUND_PACKAGE],[REQUIRED_INBOUND_PACKAGE_QTY],[ACTUAL_INBOUND_PACKAGE],[ACTUAL_INBOUND_PACKAGE_QTY],[ORDER_NO],[ITEM_NO],[TWD_RUNSHEET_NO],[COMMENTS])
					SELECT [TWD_RUNSHEET_SN],[RECKONING_SN],[PLANT],[ASSEMBLY_LINE],[SUPPLIER_NUM],[PART_NO],[PART_CNAME],[PART_ENAME],[DOCK],[BOX_PARTS],[SEQUENCE_NO],[PICKUP_SEQ_NO],[INHOUSE_PACKAGE],[MEASURING_UNIT_NO],[INBOUND_PACKAGE_MODEL],[PACK_COUNT],[REQUIRED_INBOUND_PACKAGE],[REQUIRED_INBOUND_PACKAGE_QTY],[ACTUAL_INBOUND_PACKAGE],[ACTUAL_INBOUND_PACKAGE_QTY],[ORDER_NO],[ITEM_NO],[TWD_RUNSHEET_NO],[COMMENTS]
					FROM @Table
					WHERE ISNULL(@ORDER_NO,'') = ISNULL(ORDER_NO,'')

					UPDATE D
						SET [RECKONING_SN]=@Sequence
					FROM [LES].[TT_TWD_RUNSHEET_DETAIL] D,
					(
						SELECT [RUNSHEET_DETAIL_ID] FROM @Table WHERE ISNULL(@ORDER_NO,'') = ISNULL(ORDER_NO,'')
					) T
					WHERE D.[RUNSHEET_DETAIL_ID] = T.[RUNSHEET_DETAIL_ID]
				END
	
				--把包装明细插入到包装明细表中
				INSERT INTO [LES].[TT_TWD_RUNSHEET_PACKAGE_DETAIL]
					([TWD_RUNSHEET_SN]
					,[RECKONING_SN]
					,[PLANT]
					,[ASSEMBLY_LINE]
					,[SUPPLIER_NUM]
					,[INBOUND_PACKAGE_MODEL]
					,[RDC_DLOC]
					,[INBOUND_PACKAGE]
					,[MEASURING_UNIT_NO]
					,[PACK_COUNT]
					,[REQUIRED_INBOUND_PACKAGE]
					,[REQUIRED_INBOUND_PACKAGE_QTY]
					,TWD_RUNSHEET_NO)
				SELECT [TWD_RUNSHEET_SN]
					,A.[RECKONING_SN]
					,A.[PLANT]
					,A.[ASSEMBLY_LINE]
					,A.[SUPPLIER_NUM]
					,A.[INBOUND_PACKAGE_MODEL]
					,B.[INHOUSE_PACKAGE_MODEL_NAME]
					,A.[INHOUSE_PACKAGE]
					,A.[MEASURING_UNIT_NO]
					,A.[PACK_COUNT]
					,A.[REQUIRED_INBOUND_PACKAGE]
					,A.[REQUIRED_INBOUND_PACKAGE_QTY]
					,@TWD_RUNSHEET_NO
				FROM [LES].[TT_TWD_RECKONING_SHEETS_DETAIL] A 
				LEFT JOIN [LES].[TT_TWD_PACKAGE_MODEL] B ON A.TWD_RUNSHEET_SN=@runsheetSN AND A.[INBOUND_PACKAGE_MODEL]=B.[INHOUSE_PACKAGE_MODEL]
				WHERE [RECKONING_SN] = @Sequence

				-- 判断是否还有未结算的明细
				SET @INTERFACE_ID=-1
				SELECT @INTERFACE_ID=[RUNSHEET_DETAIL_ID]
				FROM [LES].[TT_TWD_RUNSHEET_DETAIL]
				WHERE [RECKONING_SN] IS NULL
					AND [TWD_RUNSHEET_SN] = @runsheetSN
					AND [SUPPLIER_NUM_SHEET] = @SUPPLIER_NUM
					AND [BOX_PARTS_SHEET] = @BOX_PARTS
			END

			FETCH NEXT FROM DETAIL_CRSR INTO @PLANT , @ASSEMBLY_LINE , @SUPPLIER_NUM ,@BOX_PARTS
		END

	COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		--出错，则返回执行不成功，回滚事务
		ROLLBACK TRANSACTION
		
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] (TIME_STAMP, [APPLICATION], [METHOD], CLASS,  EXCEPTION_MESSAGE, ERROR_CODE)
		SELECT GETDATE(),'TWD','[LES].[PROC_TWD_MANUAL_GEN_RECKONING_SHEET_PACKAGEMODEL]','PROCEDURE',ERROR_MESSAGE() + ' Error Line:' + CONVERT(NVARCHAR(10),ERROR_LINE()),ERROR_NUMBER()
			
	END CATCH

	--释放临时表
 	CLOSE DETAIL_CRSR
	DEALLOCATE DETAIL_CRSR
END