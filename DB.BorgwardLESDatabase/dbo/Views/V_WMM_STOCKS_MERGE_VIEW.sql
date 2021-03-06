﻿CREATE VIEW [dbo].[V_WMM_STOCKS_MERGE_VIEW]
AS
SELECT TABLEA.[PLANT], TABLEA.[WM_NO], TABLEA.[ZONE_NO], TABLEA.[DLOC], TABLEA.[PART_NO], TABLEA.[PART_CNAME],
       TABLEA.[IS_BATCH], TABLEA.[BARCODE_DATA], TABLEA.[AVAILBLE_STOCKS], TABLEA.[STOCKS_NUM], TABLEA.STOCKS,
       TABLEA.[FROZEN_STOCKS], TABLEA.[FRAGMENT_NUM],
       --TABLEA.MIN, TABLEA.MAX, sp.MIN AS mins, sp.MAX AS maxs,

       CASE
            WHEN ( SP.MIN IS NULL
               AND TABLEA.MIN IS NULL) THEN 0
            WHEN ( SP.MIN IS NULL
               AND TABLEA.MIN IS NOT NULL) THEN TABLEA.MIN
            WHEN ( TABLEA.MIN IS NULL
               AND SP.MIN IS NOT NULL) THEN SP.MIN
            WHEN ( TABLEA.MIN IS NOT NULL
               AND SP.MIN IS NOT NULL) THEN SP.MIN END AS MIN, 
	   CASE
            WHEN ( SP.MAX IS NULL
                AND TABLEA.MAX IS NULL) THEN 0
            WHEN ( SP.MAX IS NULL
                AND TABLEA.MAX IS NOT NULL) THEN TABLEA.MAX
            WHEN ( TABLEA.MAX IS NULL
                AND SP.MAX IS NOT NULL) THEN SP.MAX
            WHEN ( TABLEA.MAX IS NOT NULL
                AND SP.MAX IS NOT NULL) THEN SP.MAX END MAX,
       TABLEA.[PACKAGE]
  FROM (   SELECT [PLANT], [WM_NO], [ZONE_NO], '' AS [DLOC], [PART_NO], MAX([PART_CNAME]) AS [PART_CNAME],
                  NULL AS [IS_BATCH], '' AS [BARCODE_DATA],
                  CAST((CAST(SUM(ISNULL([STOCKS_NUM], 0) - ISNULL([FROZEN_STOCKS], 0)) AS INT)
                        / MAX(ISNULL([PACKAGE], 1))) AS DECIMAL) AS [AVAILBLE_STOCKS],
                  SUM(ISNULL([STOCKS_NUM], 0)) AS [STOCKS_NUM],
                  CAST((CAST(SUM(ISNULL([STOCKS_NUM], 0)) AS INT) / MAX(ISNULL([PACKAGE], 1))) AS DECIMAL) AS [STOCKS],
                  SUM(ISNULL([FROZEN_STOCKS], 0)) AS [FROZEN_STOCKS],
                  CAST((CAST(SUM(ISNULL([STOCKS_NUM], 0) - ISNULL([FROZEN_STOCKS], 0)) AS INT)
                        % MAX(ISNULL([PACKAGE], 1))) AS DECIMAL) AS [FRAGMENT_NUM], MAX([MIN]) AS [MIN],
                  MAX([MAX]) AS [MAX], MAX(ISNULL([PACKAGE], 1)) AS [PACKAGE]
             FROM [LES].[TT_WMS_STOCKS] WITH (NOLOCK)
            WHERE ([DELETE_FLAG] = 0)
               OR ([DELETE_FLAG] IS NULL)
            GROUP BY [PLANT], [WM_NO], [ZONE_NO], [PART_NO]) TABLEA
  LEFT JOIN 
  (
   SELECT [PLANT], [WM_NO], [ZONE_NO],  [PART_NO],MAX(MAX) AS MAX, MAX(MIN)AS MIN FROM 	LES.TM_BAS_PARTS_STOCK WITH (NOLOCK) GROUP BY [PLANT], [WM_NO], [ZONE_NO], [PART_NO]

) SP
    ON SP.PLANT   = TABLEA.PLANT
   AND SP.WM_NO   = TABLEA.WM_NO
   AND SP.ZONE_NO = TABLEA.ZONE_NO
   AND SP.PART_NO = TABLEA.PART_NO