CREATE TABLE [LES].[TE_WMM_NOTIFICATION_DETAIL_TEMP] (
    [NOTIFICATION_NO] NVARCHAR (50)   NULL,
    [PART_NO]         VARCHAR (30)    NULL,
    [PACK_NUM]        NUMERIC (18, 2) NULL,
    [NUM]             NUMERIC (18, 2) NULL,
    [COMMENTS]        NVARCHAR (200)  NULL,
    [CREATE_USER]     NVARCHAR (50)   NULL,
    [CREATE_DATE]     DATETIME        NULL,
    [ERROR_MSG]       NVARCHAR (200)  NULL,
    [VALID_FLAG]      INT             NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'PACK_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？通知单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'NOTIFICATION_NO';

