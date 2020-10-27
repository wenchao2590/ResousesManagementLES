


-- =============================================
-- Author:		XinPeng Zhang
-- Create date: 2017-11-8
-- Description:	库存调整导入
-- =============================================
CREATE PROCEDURE [LES].[PROC_WMS_STOCK_ADJUSTMENT_IMPORT]
AS
    BEGIN
	
  --      INSERT  INTO [LES].[TT_WMS_STOCKS]
  --              ( [PLANT] ,
  --                [WM_NO] ,
  --                [ZONE_NO] ,
  --                [DLOC] ,
  --                [PART_NO] ,
  --                SUPPLIER_NUM ,
  --                [PART_CNAME] ,
  --                [PACKAGE] ,
  --                [PACKAGE_MODEL] ,
  --                [PART_CLS] ,
  --                [MAX] ,
  --                [MIN] ,
  --                [SAFE_STOCK] ,
  --                [IS_REPACK] ,
  --                [IS_BATCH] ,
  --                [BARCODE_DATA] ,
  --                [STOCKS_NUM] ,--总件数
  --                [STOCKS] ,--总箱数
  --                [FROZEN_STOCKS] ,--冻结件数
  --                [AVAILBLE_STOCKS] ,--可用箱数
  --                [FRAGMENT_NUM] ,--可用余数
  --                [CREATE_USER] ,
  --                [CREATE_DATE]
		--        )
  --              SELECT DISTINCT
  --                      PS.[PLANT] ,
  --                      PS.[WM_NO] ,
  --                      PS.[ZONE_NO] ,
  --                      PS.[DLOC] ,
  --                      PS.[PART_NO] ,
  --                      PS.SUPPLIER_NUM ,
  --                      PS.[PART_CNAME] ,
  --                      PS.[PACKAGE] ,
  --                      PS.[PACKAGE_MODEL] ,
  --                      PS.[PART_CLS] ,
  --                      PS.[MAX] ,
  --                      PS.[MIN] ,
  --                      PS.[SAFE_STOCK] ,
  --                      PS.[IS_REPACK] ,
  --                      ISNULL(PS.[IS_BATCH], 0) ,
  --                      CASE WHEN ISNULL(PS.[IS_BATCH], 0) = 0 THEN ''
  --                           ELSE TE.[BARCODE_DATA]
  --                      END ,--[BARCODE_DATA] 非批次管理  条码号应为空			
  --                      TE.STOCKS_NUM ,
  --                      CASE WHEN ISNULL(TE.STOCKS_NUM, 0) >= PS.PACKAGE
  --                           THEN CAST(ISNULL(TE.STOCKS_NUM, 0) / PS.PACKAGE AS INT)
  --                           ELSE 0
  --                      END ,--[STOCKS]			
  --                      TE.FROZEN_STOCKS ,
  --                      CASE WHEN ( ISNULL(TE.STOCKS_NUM, 0)
  --                                  - ISNULL(TE.FROZEN_STOCKS, 0) ) >= PS.PACKAGE
  --                           THEN CAST(( ISNULL(TE.STOCKS_NUM, 0)
  --                                       - ISNULL(TE.FROZEN_STOCKS, 0) )
  --                                / PS.PACKAGE AS INT)
  --                           ELSE 0
  --                      END ,--[AVAILBLE_STOCKS]
  --                      ( ISNULL(TE.STOCKS_NUM, 0) - ISNULL(TE.FROZEN_STOCKS,
  --                                                          0) ) % PS.PACKAGE ,--[FRAGMENT_NUM]
  --                      TE.[CREATE_USER] ,
  --                      TE.[CREATE_DATE]
  --              FROM    [LES].[TE_WMS_STOCK_ADJUSTMENT_TEMP] TE
  --                      JOIN [LES].[TM_BAS_PARTS_STOCK] PS ON TE.PLANT = PS.PLANT
  --                                                            AND TE.PART_NO = PS.PART_NO
  --                                                            AND TE.ZONE_NO = PS.ZONE_NO
  --                                                            AND TE.SUPPLIER_NUM = PS.SUPPLIER_NUM
  --                                                            AND TE.PACKAGE_MODEL = PS.PACKAGE_MODEL 
		----AND TE.BARCODE_DATA = PS.BARCODE_DATA
  --              WHERE   TE.VALID_FLAG = 1
  --                      AND NOT EXISTS ( SELECT *
  --                                       FROM   [LES].[TT_WMS_STOCKS] ST
  --                                       WHERE  TE.PLANT = ST.PLANT
  --                                              AND TE.PART_NO = ST.PART_NO
  --                                              AND TE.ZONE_NO = ST.ZONE_NO
  --                                              AND TE.SUPPLIER_NUM = ST.SUPPLIER_NUM
  --                                              AND TE.PACKAGE_MODEL = ST.PACKAGE_MODEL
  --                                              AND TE.BARCODE_DATA = ST.BARCODE_DATA ); 
				
        UPDATE  TS
        SET     STOCKS_NUM = TE.STOCKS_NUM ,
		--TS.AVAILBLE_STOCKS=TE.AVAILBLE_STOCKS,
                TS.PACKAGE = TE.PACKAGE ,
                [STOCKS] = ( CASE WHEN ISNULL(TE.STOCKS_NUM, 0) >= TS.PACKAGE
                                  THEN CAST(ISNULL(TE.STOCKS_NUM, 0)
                                       / TS.PACKAGE AS INT)
                                  ELSE 0
                             END ) ,
                FROZEN_STOCKS = TE.FROZEN_STOCKS ,
                [AVAILBLE_STOCKS] = ( CASE WHEN ( ISNULL(TE.STOCKS_NUM, 0)
                                                  - ISNULL(TE.FROZEN_STOCKS, 0) ) >= TS.PACKAGE
                                           THEN CAST(( ISNULL(TE.STOCKS_NUM, 0)
                                                       - ISNULL(TE.FROZEN_STOCKS,
                                                              0) )
                                                / TS.PACKAGE AS INT)
                                           ELSE 0
                                      END ) ,
                [FRAGMENT_NUM] = ( ISNULL(TE.STOCKS_NUM, 0)
                                   - ISNULL(TE.FROZEN_STOCKS, 0) )
                % TS.PACKAGE ,
                [UPDATE_USER] = TE.CREATE_USER ,
                [UPDATE_DATE] = TE.CREATE_DATE
        FROM    [LES].[TE_WMS_STOCK_ADJUSTMENT_TEMP] TE
                JOIN [LES].[TT_WMS_STOCKS] TS ON TE.PLANT = TS.PLANT
                                                 AND TE.PART_NO = TS.PART_NO
												 AND TE.WM_NO = TS.WM_NO--jinmiao
                                                 AND TE.ZONE_NO = TS.ZONE_NO
												 AND TE.DLOC = TS.DLOC--jinmiao
                                                 AND TE.SUPPLIER_NUM = TS.SUPPLIER_NUM
                                                 AND TE.PACKAGE_MODEL = TS.PACKAGE_MODEL
                                                 AND TE.BARCODE_DATA = TS.BARCODE_DATA
        WHERE   TE.VALID_FLAG = 1;
    END;