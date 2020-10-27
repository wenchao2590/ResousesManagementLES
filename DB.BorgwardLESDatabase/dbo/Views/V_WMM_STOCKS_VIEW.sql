﻿
CREATE VIEW [dbo].[V_WMM_STOCKS_VIEW]
AS
SELECT   MIN(P.STOCK_IDENTITY) AS STOCK_IDENTITY, S.PLANT, S.WM_NO, S.ZONE_NO, MAX(S.DLOC) AS DLOC, S.PART_NO, 
                MAX(S.PART_CNAME) AS PART_CNAME, MAX(S.PART_NICKNAME) AS PART_NICKNAME, SUM(S.STOCKS) AS STOCKS, 
                SUM(S.STOCKS_NUM) AS STOCKS_NUM, SUM(S.FRAGMENT_NUM) AS FRAGMENT_NUM, SUM(S.AVAILBLE_STOCKS) 
                AS AVAILBLE_STOCKS, SUM(S.FROZEN_STOCKS) AS FROZEN_STOCKS, MAX(S.PART_CLS) AS PART_CLS, 
                MAX(S.PACKAGE) AS PACKAGE, MAX(S.PACKAGE_MODEL) AS PACKAGE_MODEL
FROM      LES.TT_WMS_STOCKS AS S WITH (NOLOCK) LEFT OUTER JOIN
                LES.TM_BAS_PARTS_STOCK AS P WITH (NOLOCK) ON S.PLANT = P.PLANT AND S.WM_NO = P.WM_NO AND 
                S.ZONE_NO = P.ZONE_NO AND S.PART_NO = P.PART_NO
WHERE   (P.STOCK_IDENTITY IS NOT NULL)
GROUP BY S.PLANT, S.WM_NO, S.ZONE_NO, S.PART_NO
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_WMM_STOCKS_VIEW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "S"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 299
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "P"
            Begin Extent = 
               Top = 7
               Left = 347
               Bottom = 170
               Right = 651
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'V_WMM_STOCKS_VIEW';

