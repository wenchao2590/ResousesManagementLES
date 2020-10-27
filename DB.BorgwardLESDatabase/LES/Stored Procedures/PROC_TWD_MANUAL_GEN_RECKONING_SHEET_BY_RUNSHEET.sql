
/************************************************************************/
/*   Project Name:  TWD						                            */
/*   Program Name:  [PROC_TWD_MANUAL_GEN_RECKONING_SHEET_BY_RUNSHEET]   */
/*   Called By:     界面                                                */
/*   Author:        Yinxuefeng                                          */
/************************************************************************/
CREATE PROCEDURE [LES].[PROC_TWD_MANUAL_GEN_RECKONING_SHEET_BY_RUNSHEET]
(
	@runsheetSN INT,
	@SelectedRunsheetDetailIds NVARCHAR(MAX),
	@CreateUser NVARCHAR(50)
)
AS

DECLARE @RecordCount INT
SET @RecordCount=0
DECLARE @Sequence INT
DECLARE @SUPPLIER_TRANS NVARCHAR(100)
DECLARE @ORDER_NO NVARCHAR(10)

DECLARE @WAREHOUSE NVARCHAR(40)
DECLARE @PLANT NVARCHAR(30)
DECLARE @SUPPLIER_NUM NVARCHAR(8)
DECLARE @REC_TYPE INT 
DECLARE @TWD_RUNSHEET_NO NVARCHAR(22)
DECLARE @EXPECTED_ARRIVAL_TIME DATETIME
DECLARE @DELIVERY_ORDER_SN NVARCHAR(12)
DECLARE @BOX_PARTS NVARCHAR(12)
DECLARE @LOAD_PLACE NVARCHAR(50)
DECLARE @ASSEMBLY_LINE NVARCHAR(12)
DECLARE @TmpSQL NVARCHAR(MAX)
SET xact_abort ON
BEGIN TRY
    BEGIN TRANSACTION 

	--创建临时表保存待生成结算单的明细数据
        CREATE TABLE #TempRunsheetDetail
            (
              RunsheetDetailId INT NOT NULL ,
              OrderNo NVARCHAR(20) NULL ,
              DealFlag BIT NOT NULL DEFAULT 0
            )
	
	--将明细数据保存到临时表
	SET @TmpSQL='INSERT INTO #TempRunsheetDetail SELECT RUNSHEET_DETAIL_ID,ORDER_NO,0 FROM LES.TT_TWD_RUNSHEET_DETAIL WHERE RUNSHEET_DETAIL_ID IN '+@SelectedRunsheetDetailIds
	
	EXEC sp_executesql @TmpSQL
	
	
	--判断是否还有未结算的明细	
    SELECT @RecordCount = COUNT(1) FROM #TempRunsheetDetail WHERE DealFlag=0
    
    --SELECT * FROM  #TempRunsheetDetail

    --从配置中找出服务商
    --SELECT  @SUPPLIER_TRANS = PARAMETER_VALUE FROM    LES.TS_SYS_CONFIG WHERE   PARAMETER_NAME = 'SUPPLIER_TRANS'
    --从拉动单中找出工厂，供应商，拉动单号，零件类，仓库代码,期望到达时间
    SELECT  @PLANT = PLANT ,
            @SUPPLIER_NUM = SUPPLIER_NUM ,
            @TWD_RUNSHEET_NO = TWD_RUNSHEET_NO ,
            @BOX_PARTS = [BOX_PARTS] ,
            @WAREHOUSE = [DELIVERY_LOCATION],
            @EXPECTED_ARRIVAL_TIME=EXPECTED_ARRIVAL_TIME,
            @SUPPLIER_TRANS= TRANS_SUPPLIER_NUM
    FROM    [LES].[TT_TWD_RUNSHEET]
    WHERE   [TWD_RUNSHEET_SN] = @runsheetSN
    
    --从零件类信息中找出装货地点
    SELECT  @LOAD_PLACE = MAX([PLACE_OF_DELIVERY])
    FROM    [LES].[TM_TWD_BOX_PARTS]
    WHERE   [PLANT] = @PLANT AND [BOX_PARTS] = @BOX_PARTS
    
	--循环临时表，10个零件生成一张送货单
    WHILE ( @RecordCount > 0 ) 
        BEGIN
			--生成结算单序列号
            EXEC @Sequence = [LES].[PROC_JIS_GET_NEXT_SEQUENCE] 'JIS_RECKONING_SHEET'
			--生成结算单号
            EXEC LES.PROC_TWD_GET_NEXT_RECKONING_SEQUENCE @SUPPLIER_NUM,@DELIVERY_ORDER_SN OUTPUT
	
			--插入最大10条明细, 由于不同订单不能在一个送货单上，有的送货单会有不足10条的记录
            SELECT TOP 1 @ORDER_NO = OrderNo FROM #TempRunsheetDetail WHERE DealFlag=0
            ORDER BY RunsheetDetailId

	
			--插入结算主表
            INSERT  INTO LES.TT_TWD_RECKONING_SHEETS
                    ( RECKONING_SN ,
                      TWD_RUNSHEET_SN ,
                      RECKONING_NO ,
                      DELIVERY_ORDER ,
                      ORDER_NO ,
                      RECEIVE_LOCATION ,
                      RECEIVED_DATE ,
                      SUPPLIER_NUM ,
                      SUPPLIER_TRANS ,
                      WMS_SEND_STATUS ,
                      TRANS_TYPE ,
                      WMS_SEND_TIME ,
                      UNLOAD_PLACE ,
                      LOAD_PLACE ,
                      PULL_TYPE ,
                      PULL_DETAIL ,
                      SUPPLY_STATUS ,
                      SUPPLY_CONFIRM_DATE ,
                      FIRST_CONFIRM_STATUS ,
                      FIRST_CONFIRM_DATE ,
                      SECOND_CONFIRM_STATUS ,
                      SECOND_CONFIRM_DATE ,
                      COMMENTS ,
                      UPDATE_DATE ,
                      UPDATE_USER ,
                      CREATE_DATE ,
                      CREATE_USER ,
                      [WHAREHOUSE],
                      EXPECTED_ARRIVAL_TIME,
                      SEND_STATUS,
                      PLANT,
                      TWD_RUNSHEET_NO
	                )
            VALUES  ( @Sequence ,
                      @runsheetSN ,
                      @PLANT + RIGHT(CONVERT(VARCHAR(4), GETDATE(), 112), 2)
                      + @SUPPLIER_NUM + SUBSTRING('000000'
                                                  + CONVERT(NVARCHAR, @Sequence),
                                                  LEN(CONVERT(NVARCHAR, @Sequence))
                                                  + 1, 6) ,
                      @DELIVERY_ORDER_SN ,
                      @ORDER_NO ,
                      NULL ,
                      NULL ,
                      @SUPPLIER_NUM ,
                      @SUPPLIER_TRANS ,
                      0 ,--Bug5131初始状态由1改为0
                      'TWD' ,
                      NULL ,
                      @LOAD_PLACE ,
                      @WAREHOUSE ,
                      2 ,
                      NULL ,
                      NULL ,
                      NULL ,
                      NULL ,
                      NULL ,
                      NULL ,
                      NULL ,
                      NULL ,
                      NULL ,
                      NULL ,
                      GETDATE() ,
                      @CreateUser ,
                      @WAREHOUSE,
                      @EXPECTED_ARRIVAL_TIME,
                      0,
                      @PLANT,
                      @TWD_RUNSHEET_NO
                    )

			--更新10条明细已结算
            IF @ORDER_NO IS NULL 
                BEGIN
                    INSERT  INTO [LES].[TT_TWD_RECKONING_SHEETS_DETAIL]
                            ( [TWD_RUNSHEET_SN] ,
                              [RECKONING_SN] ,
                              [PLANT] ,
                              [ASSEMBLY_LINE] ,
                              [SUPPLIER_NUM] ,
                              [PART_NO] ,
                              [PART_CNAME] ,
                              [PART_ENAME] ,
                              [DOCK] ,
                              [BOX_PARTS] ,
                              [SEQUENCE_NO] ,
                              [PICKUP_SEQ_NO] ,
                              [INHOUSE_PACKAGE] ,
                              [MEASURING_UNIT_NO] ,
                              [INBOUND_PACKAGE_MODEL] ,
                              [PACK_COUNT] ,
                              [REQUIRED_INBOUND_PACKAGE] ,
                              [REQUIRED_INBOUND_PACKAGE_QTY] ,
                              [ACTUAL_INBOUND_PACKAGE] ,
                              [ACTUAL_INBOUND_PACKAGE_QTY] ,
                              [ORDER_NO] ,
                              [ITEM_NO] ,
                              [TWD_RUNSHEET_NO] ,
                              [COMMENTS]
                            )
                            SELECT TOP 10
                                    @runsheetSN ,
                                    @Sequence ,
                                    T.PLANT ,
                                    T.ASSEMBLY_LINE ,
                                    T.SUPPLIER_NUM ,
                                    T.PART_NO ,
                                    T.[PART_CNAME] ,
                                    T.PART_ENAME ,
                                    T.DOCK ,
                                    T.BOX_PARTS ,
                                    0 ,
                                    0 ,
                                    T.INBOUND_PACKAGE ,
                                    T.MEASURING_UNIT_NO ,
                                    T.INBOUND_PACKAGE_MODEL ,
                                    T.PACK_COUNT ,
                                    T.REQUIRED_INBOUND_PACKAGE ,
                                    T.REQUIRED_INBOUND_PACKAGE_QTY ,
                                    T.ACTUAL_INBOUND_PACKAGE ,
                                    T.ACTUAL_INBOUND_PACKAGE_QTY ,
                                    T.ORDER_NO ,
                                    T.ITEM_NO ,
                                    @TWD_RUNSHEET_NO ,
                                    ''
                            FROM    [LES].[TT_TWD_RUNSHEET_DETAIL] T
                            WHERE   [RUNSHEET_DETAIL_ID] IN 
                            (
								SELECT TOP 10 TD.RunsheetDetailId FROM #TempRunsheetDetail TD
								WHERE TD.OrderNo is NULL AND TD.DealFlag=0
								ORDER BY TD.RunsheetDetailId	
                            )
                            
                            UPDATE #TempRunsheetDetail SET DealFlag=1
                            WHERE RunsheetDetailId IN
                            (
							    SELECT TOP 10 TD.RunsheetDetailId FROM #TempRunsheetDetail  TD
								WHERE TD.OrderNo is NULL AND TD.DealFlag=0
								ORDER BY TD.RunsheetDetailId	
                            )
                END
            ELSE 
                BEGIN
                    INSERT  INTO [LES].[TT_TWD_RECKONING_SHEETS_DETAIL]
                            ( [TWD_RUNSHEET_SN] ,
                              [RECKONING_SN] ,
                              [PLANT] ,
                              [ASSEMBLY_LINE] ,
                              [SUPPLIER_NUM] ,
                              [PART_NO] ,
                              [PART_CNAME] ,
                              [PART_ENAME] ,
                              [DOCK] ,
                              [BOX_PARTS] ,
                              [SEQUENCE_NO] ,
                              [PICKUP_SEQ_NO] ,
                              [INHOUSE_PACKAGE] ,
                              [MEASURING_UNIT_NO] ,
                              [INBOUND_PACKAGE_MODEL] ,
                              [PACK_COUNT] ,
                              [REQUIRED_INBOUND_PACKAGE] ,
                              [REQUIRED_INBOUND_PACKAGE_QTY] ,
                              [ACTUAL_INBOUND_PACKAGE] ,
                              [ACTUAL_INBOUND_PACKAGE_QTY] ,
                              [ORDER_NO] ,
                              [ITEM_NO] ,
                              [TWD_RUNSHEET_NO] ,
                              [COMMENTS]
                            )
                            SELECT TOP 10
                                    @runsheetSN ,
                                    @Sequence ,
                                    T.PLANT ,
                                    T.ASSEMBLY_LINE ,
                                    T.SUPPLIER_NUM ,
                                    T.PART_NO ,
                                    T.[PART_CNAME] ,
                                    T.PART_ENAME ,
                                    T.DOCK ,
                                    T.BOX_PARTS ,
                                    0 ,
                                    0 ,
                                    T.INBOUND_PACKAGE ,
                                    T.MEASURING_UNIT_NO ,
                                    T.INBOUND_PACKAGE_MODEL ,
                                    T.PACK_COUNT ,
                                    T.REQUIRED_INBOUND_PACKAGE ,
                                    T.REQUIRED_INBOUND_PACKAGE_QTY ,
                                    T.ACTUAL_INBOUND_PACKAGE ,
                                    T.ACTUAL_INBOUND_PACKAGE_QTY ,
                                    T.ORDER_NO ,
                                    T.ITEM_NO ,
                                    @TWD_RUNSHEET_NO ,
                                    ''
                            FROM    [LES].[TT_TWD_RUNSHEET_DETAIL] T
                            WHERE   [RUNSHEET_DETAIL_ID] IN 
                            (
								SELECT TOP 10 TD.RunsheetDetailId FROM #TempRunsheetDetail  TD
								WHERE TD.OrderNo = @order_no AND TD.DealFlag=0
								ORDER BY TD.RunsheetDetailId	
                            )
                            
                            UPDATE #TempRunsheetDetail SET DealFlag=1
                            WHERE RunsheetDetailId IN
                            (
							    SELECT TOP 10 TD.RunsheetDetailId FROM #TempRunsheetDetail  TD
								WHERE TD.OrderNo = @order_no AND TD.DealFlag=0
								ORDER BY TD.RunsheetDetailId	
                            )
                            
                END
	
			--把包装明细插入到包装明细表中
            INSERT  INTO [LES].[TT_TWD_RUNSHEET_PACKAGE_DETAIL]
                    ( [TWD_RUNSHEET_SN] ,
                      [RECKONING_SN] ,
                      [PLANT] ,
                      [ASSEMBLY_LINE] ,
                      [SUPPLIER_NUM] ,
                      [INBOUND_PACKAGE_MODEL] ,
                      [RDC_DLOC] ,
                      [INBOUND_PACKAGE] ,
                      [MEASURING_UNIT_NO] ,
                      [PACK_COUNT] ,
                      [REQUIRED_INBOUND_PACKAGE] ,
                      [REQUIRED_INBOUND_PACKAGE_QTY] ,
                      TWD_RUNSHEET_NO
                    )
                    SELECT  A.[TWD_RUNSHEET_SN] ,
                            A.[RECKONING_SN] ,
                            A.[PLANT] ,
                            A.[ASSEMBLY_LINE] ,
                            A.[SUPPLIER_NUM] ,
                            A.[INBOUND_PACKAGE_MODEL] ,
                            ISNULL(b.INHOUSE_PACKAGE_MODEL_NAME,'') ,
                            A.[INHOUSE_PACKAGE] ,
                            A.[MEASURING_UNIT_NO] ,
                            A.[PACK_COUNT] ,
                            A.[REQUIRED_INBOUND_PACKAGE] ,
                            A.[REQUIRED_INBOUND_PACKAGE_QTY] ,
                            @TWD_RUNSHEET_NO
                    FROM    [LES].[TT_TWD_RECKONING_SHEETS_DETAIL] A
                    LEFT JOIN [LES].[TT_TWD_PACKAGE_MODEL] B ON A.[INBOUND_PACKAGE_MODEL]=B.[INHOUSE_PACKAGE_MODEL]
                    WHERE   [RECKONING_SN] = @Sequence

			--判断是否还有未结算的明细
            SET @RecordCount = 0
            SELECT @RecordCount = COUNT(1) FROM #TempRunsheetDetail WHERE DealFlag=0

        END
        DROP TABLE #TempRunsheetDetail
    COMMIT TRANSACTION
END TRY

BEGIN CATCH
	--出错，则返回执行不成功，回滚事务
    ROLLBACK TRANSACTION
    PRINT ERROR_MESSAGE()
END CATCH