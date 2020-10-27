

CREATE VIEW [LES].[V_SPM_GENERAL_RUNSHEET_DETAIL_VM]
AS
SELECT   t_Detail.RUNSHEET_DETAIL_ID, t_Detail.PART_NO, t_Detail.PART_CNAME, t_Detail.DOCK, t_Detail.INHOUSE_PACKAGE, 
                t_Detail.SUPPLIER_NUM, t_Detail.REQUIRED_INHOUSE_PACKAGE_QTY, t_Detail.REQUIRED_INHOUSE_PACKAGE, 
                t_Detail.ACTUAL_INHOUSE_PACKAGE, t_Detail.ACTUAL_INHOUSE_PACKAGE_QTY, t_Detail.COMMENTS, 
                t_RunSheet.PLANT, t_RunSheet.PLANT_ZONE, t_RunSheet.RUNSHEET_TYPE, t_RunSheet.CREATE_DATE, 
                t_RunSheet.EXPECTED_ARRIVAL_TIME, t_RunSheet.SHEET_STATUS, t_RunSheet.WHAREHOUSE, 
                t_RunSheet.PLAN_RUNSHEET_SN, t_RunSheet.PLAN_RUNSHEET_NO, t_Detail.INHOUSE_PACKAGE_MODEL, 
                t_Detail.ASN_DRAFT_QTY, t_Detail.ASN_CONFIRM_QTY, ISNULL(t_Detail.REQUIRED_INHOUSE_PACKAGE_QTY, 0) 
                - ISNULL(t_Detail.ASN_CONFIRM_QTY, 0) - ISNULL(t_Detail.ASN_DRAFT_QTY, 0) AS ASN_EDIT_QTY, 
                t_Detail.ACTUAL_RDC_INHOUSE_PACKAGE_QTY,
                    (SELECT   TOP (1) SUPPLIER_NAME
                     FROM      LES.TM_BAS_SUPPLIER AS t_Supplier
                     WHERE   (t_RunSheet.SUPPLIER_NUM = SUPPLIER_NUM)) AS SUPPLIER_NAME, t_Detail.DISPO
FROM      [LES].[TT_SPM_GENERAL_PURCHASE_RUNSHEET_DETAIL] AS t_Detail INNER JOIN
                [LES].[TT_SPM_GENERAL_PURCHASE_RUNSHEET] AS t_RunSheet ON 
                t_RunSheet.PLAN_RUNSHEET_SN = t_Detail.PLAN_RUNSHEET_SN
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'VIEW', @level1name = N'V_SPM_GENERAL_RUNSHEET_DETAIL_VM';


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
         Begin Table = "t_Detail"
            Begin Extent = 
               Top = 253
               Left = 460
               Bottom = 393
               Right = 775
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "t_RunSheet"
            Begin Extent = 
               Top = 150
               Left = 38
               Bottom = 290
               Right = 293
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
', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'VIEW', @level1name = N'V_SPM_GENERAL_RUNSHEET_DETAIL_VM';

