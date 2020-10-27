CREATE TABLE [LES].[TM_WMM_NOTIFICATION_DETAIL_SCAN] (
    [SCAN_ID]        BIGINT          IDENTITY (1, 1) NOT NULL,
    [NOTIFICATIONID] INT             NOT NULL,
    [BARCODE_DATA]   NVARCHAR (50)   NOT NULL,
    [PART_NO]        NVARCHAR (30)   NOT NULL,
    [ActualQty]      NUMERIC (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([SCAN_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_SCAN', @level2type = N'COLUMN', @level2name = N'ActualQty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_SCAN', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_SCAN', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)通知单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_SCAN', @level2type = N'COLUMN', @level2name = N'NOTIFICATIONID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_SCAN', @level2type = N'COLUMN', @level2name = N'SCAN_ID';

