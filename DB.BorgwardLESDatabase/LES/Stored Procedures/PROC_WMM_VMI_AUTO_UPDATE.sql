/*****************************************************/
/*   Program Name:  [LES].[PROC_WMM_VMI_AUTO_UPDATE] */
/*   Called By:     web page						 */
/*   Author:        孙述霄							 */
/*   Create date:	2017-06-08						 */
/*   Note:			外库库存调整,更新相关数据		 */
/*****************************************************/
CREATE PROCEDURE [LES].[PROC_WMM_VMI_AUTO_UPDATE]
(
	@ReceiveDetials XML,
	@Stocks XML
)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @CurrentTime DATETIME
			SET @CurrentTime=GETDATE()

			DECLARE @ReceiveDetailTable TABLE
			(
				[ReceiveDetailId] [INT],
				[ReceiveId] [INT],
				[Plant] [NVARCHAR](5),
				[WmNo] [NVARCHAR](10),
				[ZoneNo] [NVARCHAR](20),
				[PartNo] [NVARCHAR](20),
				[CurrentQty] [INT],
				[CurrentBoxNum] [INT],
				[UserName] [NVARCHAR](50),
				[IsBatch] [INT]
			)

			DECLARE @StocksTable TABLE
			(
				[StockIdentity] [INT],
				[stocksNum] [NUMERIC](18, 2)
			)

			--更新临时表数据
			INSERT INTO @ReceiveDetailTable
			SELECT T.c.query('ReceiveDetailId').value('.[1]', '[INT]') AS ReceiveDetailId,
				T.c.query('ReceiveId').value('.[1]', '[INT]') AS ReceiveId,
				T.c.query('Plant').value('.[1]', 'VARCHAR(50)') AS Plant,
				T.c.query('WmNo').value('.[1]', 'NVARCHAR(50)') AS WmNo,
				T.c.query('ZoneNo').value('.[1]', 'NVARCHAR(50)') AS ZoneNo,
				T.c.query('PartNo').value('.[1]', 'NVARCHAR(50)') AS PartNo,
				T.c.query('CurrentQty').value('.[1]', '[INT]') AS CurrentQty,
				T.c.query('CurrentBoxNum').value('.[1]', '[INT]') AS CurrentBoxNum,
				T.c.query('UserName').value('.[1]', 'NVARCHAR(50)') AS UserName,
				T.c.query('IsBatch').value('.[1]', '[INT]') AS IsBatch
			FROM @ReceiveDetials.nodes('/ReceiveDetials/item') AS T(c)
			INSERT INTO @StocksTable
			SELECT T.c.query('StockIdentity').value('.[1]', '[int]') AS StockIdentity,
			T.c.query('stocksNum').value('.[1]', '[numeric](18, 2)') AS stocksNum
			FROM @Stocks.nodes('/Stocks/item') AS T(c)
		
			--更新收获明细数量
			UPDATE A SET 
			A.[NUM]=ISNULL(A.[NUM], 0)+ISNULL(B.CurrentQty, 0), 
			A.[BOX_NUM]=ISNULL(A.[BOX_NUM], 0)+ISNULL(B.CurrentBoxNum, 0),
			A.[ACTUAL_QTY]=ISNULL(A.[ACTUAL_QTY], 0)+ISNULL(B.CurrentQty, 0),
			A.[ACTUAL_BOX_NUM]=ISNULL(A.[ACTUAL_BOX_NUM], 0)+ISNULL(B.CurrentBoxNum, 0),
			A.[Current_BOX_NUM]=NULL,
			A.[Current_QTY]=NULL
			FROM LES.TT_WMM_RECEIVE_DETAIL A
			LEFT JOIN @ReceiveDetailTable B ON A.RECEIVE_DETAIL_ID=B.ReceiveDetailId
			WHERE A.RECEIVE_ID IN(SELECT DISTINCT(ReceiveId) FROM @ReceiveDetailTable)
			--更新仓库入库表状态————用于winserivce同步数据到接口表中
			--是否关单
			UPDATE A SET 
				A.CONFIRM_FLAG=2,	--关单
				A.RECEIVE_TYPE=1	--完全收货
			FROM LES.TT_WMM_RECEIVE A
			JOIN (SELECT C.RECEIVE_ID FROM LES.TT_WMM_RECEIVE_DETAIL C
				WHERE C.RECEIVE_ID IN(SELECT B.ReceiveId FROM @ReceiveDetailTable B)
				GROUP BY C.RECEIVE_ID
				HAVING SUM(C.REQUIRED_QTY)=SUM(C.ACTUAL_QTY)) B 
			ON A.RECEIVE_ID=B.RECEIVE_ID
			UPDATE A SET 
				A.TRAN_TIME=@CurrentTime,
				A.UPDATE_USER=B.UserName,
				A.UPDATE_DATE=@CurrentTime,
				A.ERP_FLAG=2
			FROM LES.TT_WMM_RECEIVE A
				JOIN (SELECT ReceiveId,UserName FROM @ReceiveDetailTable GROUP BY ReceiveId,UserName) B 
				ON A.RECEIVE_ID=B.ReceiveId

			--同步入库明细中 实际数量到 供应商门户对应的 实际数量中
			update spm set ACTUAL_INHOUSE_PACKAGE=rece.ACTUAL_BOX_NUM,ACTUAL_INHOUSE_PACKAGE_QTY=rece.ACTUAL_QTY
			from  [LES].TT_SPM_DELIVERY_RUNSHEET_DETAIL spm
			inner join [LES].[TT_SPM_DELIVERY_RUNSHEET] del 
			on del.PLAN_RUNSHEET_SN=spm.PLAN_RUNSHEET_SN
			join (select wmm.RECEIVE_NO as RECEIVE_NO,wmmdetail.ACTUAL_QTY as ACTUAL_QTY,
				wmmdetail.ACTUAL_BOX_NUM as ACTUAL_BOX_NUM,wmmdetail.PART_NO as PART_NO 
				from @ReceiveDetailTable A
					JOIN les.[TT_WMM_RECEIVE_DETAIL] wmmdetail ON wmmdetail.RECEIVE_DETAIL_ID=A.ReceiveDetailId
					JOIN [LES].[TT_WMM_RECEIVE] wmm ON wmmdetail.RECEIVE_ID=wmm.RECEIVE_ID) rece
			on rece.PART_NO=spm.PART_NO and del.PLAN_RUNSHEET_no=rece.RECEIVE_NO
				
			--完成交易操作记录
			INSERT INTO LES.TM_WMM_TRAN_DETAILS_LOG ( 
				TRAN_NO,
				TRAN_TYPE,
				TRAN_STATE,
				TRAN_DATE,

				PLANT,
				WM_NO,
				ZONE_NO,
				SUPPLIER_NUM,
				DLOC,
				PACKAGE,
				PACKAGE_MODEL,
				PART_NO,
				PART_CNAME,
				PART_CLS,
				[MAX],
				[MIN],
				IS_BATCH,

				BOX_PARTS,
				PART_UNITS,
				MEASURING_UNIT_NO,
				COMMENTS,

				PROCESS_RESULT,
				PROCESS_MESSAGE,

				NUM,
				BOX_NUM,
				CREATE_USER,
				CREATE_DATE)
			SELECT 
				B.RECEIVE_NO AS TRAN_NO,
				1 AS TRAN_TYPE,
				1 AS TRAN_STATE,
				@CurrentTime AS TRAN_DATE,

				S.Plant AS PLANT,
				S.WM_NO AS WM_NO,
				S.ZONE_NO AS ZONE_NO,
				S.SUPPLIER_NUM AS SUPPLIER_NUM,
				S.DLOC AS DLOC,
				(CASE WHEN ISNULL(C.PACKAGE, 0) = 0 THEN ISNULL(S.PACKAGE,1) ELSE C.PACKAGE END) AS PACKAGE,
				S.PACKAGE_MODEL AS PACKAGE_MODEL,

				S.PART_NO AS PART_NO,
				S.PART_CNAME AS PART_CNAME,
				S.PART_CLS AS PART_CLS,
				S.[MAX],
				S.[MIN],
				S.IS_BATCH,

				C.BOX_PARTS AS BOX_PARTS,
				C.MEASURING_UNIT_NO AS MEASURING_UNIT_NO,
				C.MEASURING_UNIT_NO AS MEASURING_UNIT_NO,
				C.COMMENTS,

				0,
				NULL,

				A.CurrentQty AS NUM,
				A.CurrentBoxNum AS BOX_NUM,
				A.UserName,
				@CurrentTime
			FROM @ReceiveDetailTable A
			JOIN LES.TT_WMM_RECEIVE_DETAIL (NOLOCK) C ON C.RECEIVE_DETAIL_ID=A.ReceiveDetailId
			JOIN LES.TT_WMM_RECEIVE (NOLOCK) B ON B.RECEIVE_ID =A.ReceiveId
			JOIN LES.TM_BAS_PARTS_STOCK (NOLOCK) S ON S.PART_NO=C.PART_NO AND S.ZONE_NO=C.ZONE_NO AND S.PLANT=C.PLANT

			--完成入库明细日志记录————用于winserivce同步数据到接口表中————对应收货件数
			INSERT INTO LES.TT_WMM_RECEIVE_DETAIL_LOG (
				RECEIVE_ID,
				RECEIVE_NO,
				PART_NO,
				NUM,
				BOX_NUM,
				SEQUENCE,
				ERP_FLAG,
				SEND_TIME,
				CREATE_USER,
				CREATE_DATE) 
			SELECT
				B.RECEIVE_ID,
				B.RECEIVE_NO,
				C.PART_NO,
				ISNULL(A.CurrentQty,0),
				ISNULL(A.CurrentBoxNum,0),
				(select isnull(MAX([SEQUENCE]),0)+1 from [LES].[TT_WMM_RECEIVE_DETAIL_LOG] E where E.RECEIVE_ID=B.RECEIVE_ID),
				2,
				@CurrentTime,
				B.UserName,
				@CurrentTime
			FROM (SELECT N.RECEIVE_ID,N.RECEIVE_NO,M.UserName FROM @ReceiveDetailTable M 
				JOIN LES.TT_WMM_RECEIVE N ON M.ReceiveId=N.RECEIVE_ID GROUP BY N.RECEIVE_ID,N.RECEIVE_NO,M.UserName) B
			JOIN  LES.TT_WMM_RECEIVE_DETAIL C ON C.RECEIVE_ID=B.RECEIVE_ID
			left join @ReceiveDetailTable A ON C.RECEIVE_DETAIL_ID=A.ReceiveDetailId
				
			--更新库存
			UPDATE S
				SET STOCKS_NUM = T.stocksNum,									
					STOCKS = CAST(T.stocksNum / ISNULL(S.PACKAGE,1) AS INT),                                            
					AVAILBLE_STOCKS = CAST((T.stocksNum - ISNULL(S.FROZEN_STOCKS, 0)) / ISNULL(S.PACKAGE,1) AS INT),		 							
					FRAGMENT_NUM = (T.stocksNum - ISNULL(S.FROZEN_STOCKS, 0)) % ISNULL(S.PACKAGE,1),
					UPDATE_DATE = @CurrentTime
				FROM LES.TT_WMS_STOCKS S
				JOIN @StocksTable T ON S.STOCK_IDENTITY = T.StockIdentity 
				
			--完成器具入库
			DECLARE @ReceiveId int
			DECLARE NOTIFICATION_CURS1  CURSOR FOR 
				SELECT 
					distinct(ReceiveId) 
				FROM @ReceiveDetailTable 
			OPEN NOTIFICATION_CURS1
			FETCH NEXT FROM NOTIFICATION_CURS1 INTO @ReceiveId
			WHILE( @@fetch_status = 0 )
			BEGIN
				EXEC [LES].[PROC_RPM_PACKAGE_TRAN] @ReceiveId
				FETCH NEXT FROM NOTIFICATION_CURS1 INTO @ReceiveId
			END
			CLOSE  NOTIFICATION_CURS1
			DEALLOCATE NOTIFICATION_CURS1 --游标结束

			--更新订单状态
			UPDATE D SET 
				D.SHEET_STATUS = 10,
				D.ACTUAL_ARRIVAL_TIME=@CurrentTime,
                D.[UPDATE_USER]='',
                D.[UPDATE_DATE]=@CurrentTime
			from LES.TT_SPM_DELIVERY_RUNSHEET D
			JOIN LES.TT_WMM_RECEIVE A ON D.PLAN_RUNSHEET_NO=A.RECEIVE_NO
			JOIN (SELECT CC.RECEIVE_ID FROM LES.TT_WMM_RECEIVE_DETAIL CC
				WHERE CC.RECEIVE_ID IN(SELECT BB.ReceiveId FROM @ReceiveDetailTable BB)
				GROUP BY CC.RECEIVE_ID
				HAVING SUM(CC.REQUIRED_QTY)=SUM(CC.ACTUAL_QTY)) E 
				ON A.RECEIVE_ID=E.RECEIVE_ID
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION   
		--记录错误信息
		INSERT INTO [LES].[TS_SYS_EXCEPTION] ([TIME_STAMP], [APPLICATION], [METHOD], [CLASS],  [EXCEPTION_MESSAGE], [ERROR_CODE])
		SELECT GETDATE(), 'INTERFACE', '[LES].[PROC_WMM_VMI_AUTO_UPDATE]', 'Procedure', ERROR_MESSAGE(), ERROR_LINE()
	END CATCH
END