CREATE TABLE [LES].[TE_WMM_NOTIFICATION_TEMP] (
    [NOTIFICATION_NO]     NVARCHAR (50)   NULL,
    [PLANT]               NVARCHAR (5)    NULL,
    [WM_NO]               NVARCHAR (10)   NULL,
    [ZONE_NO]             NVARCHAR (20)   NULL,
    [EMERGENCY_TYPE]      INT             NULL,
    [COUNT_TYPE]          INT             NULL,
    [COUNT_TIME]          DATETIME        NULL,
    [APPLY_KEEPER]        NVARCHAR (50)   NULL,
    [BOOK_KEEPER]         NVARCHAR (50)   NULL,
    [PART_NO]             VARCHAR (30)    NULL,
    [PACK_NUM]            NUMERIC (18, 2) NULL,
    [NUM]                 NUMERIC (18, 2) NULL,
    [COUNT_REASON]        NVARCHAR (1000) NULL,
    [COMMENTS]            NVARCHAR (200)  NULL,
    [CREATE_USER]         NVARCHAR (50)   NULL,
    [CREATE_DATE]         DATETIME        NULL,
    [ERROR_MSG]           NVARCHAR (200)  NULL,
    [VALID_FLAG]          INT             NULL,
    [EMERGENCY_TYPE_TEXT] NVARCHAR (200)  NULL,
    [COUNT_TYPE_TEXT]     NVARCHAR (200)  NULL,
    [SUPPLIER_NUM]        NVARCHAR (12)   NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'COUNT_TYPE_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'EMERGENCY_TYPE_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？盘点原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'COUNT_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'PACK_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？盘点人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？申请人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'APPLY_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？计划盘点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'COUNT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？计数类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'COUNT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？盘点紧急程度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'EMERGENCY_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？通知单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_NOTIFICATION_TEMP', @level2type = N'COLUMN', @level2name = N'NOTIFICATION_NO';

