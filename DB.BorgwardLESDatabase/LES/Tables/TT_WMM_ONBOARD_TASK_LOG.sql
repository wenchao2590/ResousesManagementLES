CREATE TABLE [LES].[TT_WMM_ONBOARD_TASK_LOG] (
    [ID]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [TASK_ID]           BIGINT         NULL,
    [EQUIP_ID]          BIGINT         NULL,
    [GROUP_ID]          BIGINT         NULL,
    [SCAN_TRAY_BARCODE] NVARCHAR (128) NULL,
    [PART_NO]           NVARCHAR (32)  NULL,
    [BOX_QTY]           INT            NULL,
    [PART_QTY]          DECIMAL (18)   NULL,
    [SCAN_LOCATION_NO]  NVARCHAR (32)  NULL,
    [STATUS]            INT            NULL,
    [VALID_FLAG]        BIT            NULL,
    [CREATE_DATE]       DATETIME       NULL,
    [CREATE_USER]       NVARCHAR (32)  NULL,
    [MODIFY_DATE]       DATETIME       NULL,
    [MODIFY_USER]       NVARCHAR (32)  NULL,
    CONSTRAINT [PK_TT_WMM_ONBOARD_TASK_LOG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扫描库位标签条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK_LOG', @level2type = N'COLUMN', @level2name = N'SCAN_LOCATION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK_LOG', @level2type = N'COLUMN', @level2name = N'PART_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK_LOG', @level2type = N'COLUMN', @level2name = N'BOX_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK_LOG', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扫描托盘标签条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK_LOG', @level2type = N'COLUMN', @level2name = N'SCAN_TRAY_BARCODE';

