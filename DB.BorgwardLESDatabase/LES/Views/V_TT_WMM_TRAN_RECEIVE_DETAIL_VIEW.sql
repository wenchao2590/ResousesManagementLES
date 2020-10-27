﻿/********************************************************************/
/* Project Name:  WMM							                    */
/* Program Name:  [V_TT_WMM_TRAN_RECEIVE_DETAIL_VIEW]				*/
/* Called By:     WMM多次收货明细                                   */
/* Author:       zhangheng											*/
/********************************************************************/
CREATE VIEW [LES].[V_TT_WMM_TRAN_RECEIVE_DETAIL_VIEW]
AS
SELECT
	A.[PLAN_RUNSHEET_SN],
	A.[PLAN_RUNSHEET_NO],
	A.[PLANT],
	A.[SUPPLIER_NUM],
	C.[WM_NO],
	C.[ZONE_NO],
	A.[DOCK], 
	A.[PUBLISH_TIME],
	B.[PART_NO],
	B.[PART_CNAME],
	B.[INHOUSE_PACKAGE],
	B.[INHOUSE_PACKAGE_MODEL],
	B.[REQUIRED_INHOUSE_PACKAGE_QTY] AS [REQUIRED_INHOUSE_PACKAGE],
	ISNULL(B.[ACTUAL_INHOUSE_PACKAGE], 0) AS [ACTUAL_INHOUSE_PACKAGE],
	B.[REQUIRED_INHOUSE_PACKAGE] AS [REQUIRED_INHOUSE_PACKAGE_QTY],
	ISNULL(B.[ACTUAL_INHOUSE_PACKAGE_QTY], 0) AS [ACTUAL_INHOUSE_PACKAGE_QTY],
	ISNULL(B.[REQUIRED_INHOUSE_PACKAGE_QTY], 0) - ISNULL(B.[ACTUAL_INHOUSE_PACKAGE], 0) AS [SUB_BOX_NUM],
	ISNULL(B.[REQUIRED_INHOUSE_PACKAGE], 0) - ISNULL(B.[ACTUAL_INHOUSE_PACKAGE_QTY], 0) AS [SUB_QTY],
	B.[DISPO],
	A.[EXPECTED_ARRIVAL_TIME],
	ISNULL(A.[IS_ASN], 0) AS [IS_ASN]
FROM [LES].[TT_SPM_DELIVERY_RUNSHEET] A WITH (NOLOCK)
INNER JOIN [LES].[TT_SPM_DELIVERY_RUNSHEET_DETAIL] B WITH (NOLOCK) ON A.[PLAN_RUNSHEET_SN] = B.[PLAN_RUNSHEET_SN]
LEFT JOIN [LES].[TM_WMM_ZONES] C WITH (NOLOCK) ON A.[PLANT_ZONE] = C.[ZONE_NO]
WHERE A.[SHEET_STATUS] <> 10 AND A.[RUNSHEET_TYPE] NOT IN (-3, -11, -15, -22, -32, -50, -51)
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'VIEW', @level1name = N'V_TT_WMM_TRAN_RECEIVE_DETAIL_VIEW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[21] 4[40] 2[20] 3) )"
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
         Top = -4200
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 168
               Right = 349
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 168
               Left = 48
               Bottom = 329
               Right = 352
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "d"
            Begin Extent = 
               Top = 490
               Left = 48
               Bottom = 651
               Right = 349
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 2887
               Left = 48
               Bottom = 3048
               Right = 406
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
         Column = 2904
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
', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'VIEW', @level1name = N'V_TT_WMM_TRAN_RECEIVE_DETAIL_VIEW';

