CREATE TABLE [LES].[TE_WMM_NOTIFICATION_RESULT_TEMP] (
    [NOTIFICATION_NO] NVARCHAR (50)   NULL,
    [PART_NO]         VARCHAR (30)    NULL,
    [REAL_NUM]        NUMERIC (18, 2) NULL,
    [COMMENTS]        NVARCHAR (200)  NULL,
    [CREATE_USER]     NVARCHAR (50)   NULL,
    [CREATE_DATE]     DATETIME        NULL,
    [ERROR_MSG]       NVARCHAR (200)  NULL,
    [VALID_FLAG]      INT             NULL,
    [REAL_NUM_TEXT]   NVARCHAR (100)  NULL,
    [SUPPLIER_NUM]    NVARCHAR (12)   NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'REAL_NUM_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？实盘数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'REAL_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？通知单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_RESULT_TEMP', @level2type = N'COLUMN', @level2name = N'NOTIFICATION_NO';

