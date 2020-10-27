CREATE VIEW LES.V_WMM_TRAN_DETAILS_LOG
AS
SELECT   W.TRAN_ID, W.TRAN_NO, W.PLANT, W.BATCH_NO, W.PART_NO, W.BARCODE_DATA, W.WM_NO, W.ZONE_NO, 
                W.DLOC, W.TARGET_WM, W.TARGET_ZONE, W.TARGET_DLOC, W.MEASURING_UNIT_NO, W.PACKAGE, W.MAX, 
                W.MIN, W.NUM, W.BOX_NUM, W.TRAN_STATE, W.TRAN_DATE, W.SUPPLIER_NUM, W.PART_CNAME, W.BOX_PARTS, 
                W.PICKUP_SEQ_NO, W.RDC_DLOC, W.ACTUAL_PACKAGE_QTY, W.INNER_LOCATION, W.LOCATION, 
                W.STORAGE_LOCATION, W.INHOUSE_PACKAGE_MODEL, W.REQUIRED_PACKAGE_QTY, W.BARCODE_TYPE, 
                W.REQUIRED_DATE, W.PACHAGE_TYPE, W.LINE_POSITION, W.RUNSHEET_NO, W.PART_NICKNAME, W.DOCK, 
                W.SUPPLIER_SNAME, W.PACKAGE_MODEL, W.PART_CLS, W.PART_UNITS, W.IS_BATCH, W.TRAN_TYPE, 
                W.CREATE_USER, W.CREATE_DATE, W.COMMENTS, W.UPDATE_USER, W.UPDATE_DATE, W.UPDATE_FLAG, 
                W.PROCESS_RESULT, W.PROCESS_MESSAGE, S.SUPPLIER_NAME
FROM      LES.TM_WMM_TRAN_DETAILS_LOG AS W LEFT OUTER JOIN
                LES.TM_BAS_SUPPLIER AS S ON S.SUPPLIER_NUM = W.SUPPLIER_NUM
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'VIEW', @level1name = N'V_WMM_TRAN_DETAILS_LOG';


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
         Begin Table = "W"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 146
               Right = 292
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "S"
            Begin Extent = 
               Top = 6
               Left = 330
               Bottom = 146
               Right = 568
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
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'VIEW', @level1name = N'V_WMM_TRAN_DETAILS_LOG';

