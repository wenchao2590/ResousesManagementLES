CREATE VIEW [LES].[V_SPM_DELIVERY_RUNSHEET_DETAIL_VM]
AS
SELECT
	D.[RUNSHEET_DETAIL_ID],
	D.[PART_NO],
	D.[PART_CNAME],
	D.[DOCK],
	D.[INHOUSE_PACKAGE],
	D.[SUPPLIER_NUM],
	D.[REQUIRED_INHOUSE_PACKAGE_QTY],
	D.[REQUIRED_INHOUSE_PACKAGE],
    D.[ACTUAL_INHOUSE_PACKAGE],
	D.[ACTUAL_INHOUSE_PACKAGE_QTY],
	D.[COMMENTS],
    T.[PLANT],
	T.[PLANT_ZONE],
	T.[RUNSHEET_TYPE],
	T.[CREATE_DATE],
    T.[EXPECTED_ARRIVAL_TIME],
	T.[SHEET_STATUS],
	T.[WHAREHOUSE],
    T.[PLAN_RUNSHEET_SN],
	T.[PLAN_RUNSHEET_NO],
	T.[IS_ASN],
	D.[INHOUSE_PACKAGE_MODEL], 
    D.[ASN_DRAFT_QTY],
	D.[ASN_CONFIRM_QTY],
	ISNULL(D.[REQUIRED_INHOUSE_PACKAGE], 0) - ISNULL(D.[ASN_CONFIRM_QTY], 0) - ISNULL(D.[ASN_DRAFT_QTY], 0) AS ASN_EDIT_QTY, 
    D.[ACTUAL_RDC_INHOUSE_PACKAGE_QTY],
    S.[SUPPLIER_NAME],
	D.[DISPO]
FROM [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL] D WITH(NOLOCK)
INNER JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET] T WITH(NOLOCK) ON T.[PLAN_RUNSHEET_SN] = D.[PLAN_RUNSHEET_SN]
LEFT JOIN [LES].[TM_BAS_SUPPLIER] S WITH (NOLOCK) ON T.[SUPPLIER_NUM] = S.[SUPPLIER_NUM]
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'VIEW', @level1name = N'V_SPM_DELIVERY_RUNSHEET_DETAIL_VM';


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
', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'VIEW', @level1name = N'V_SPM_DELIVERY_RUNSHEET_DETAIL_VM';

