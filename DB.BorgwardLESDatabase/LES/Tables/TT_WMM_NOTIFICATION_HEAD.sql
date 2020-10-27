CREATE TABLE [LES].[TT_WMM_NOTIFICATION_HEAD] (
    [NOTIFICATION_ID] INT             IDENTITY (1, 1) NOT NULL,
    [NOTIFICATION_NO] NVARCHAR (50)   NULL,
    [PLANT]           NVARCHAR (5)    NULL,
    [WM_NO]           NVARCHAR (10)   NULL,
    [ZONE_NO]         NVARCHAR (20)   NULL,
    [COUNT_TYPE]      INT             NULL,
    [COUNT_TIME]      DATETIME        NULL,
    [APPLY_KEEPER]    NVARCHAR (50)   NULL,
    [BOOK_KEEPER]     NVARCHAR (50)   NULL,
    [PUBLISH_KEEPER]  NVARCHAR (50)   NULL,
    [PUBLISH_TIME]    DATETIME        NULL,
    [CONFIRM_FLAG]    INT             NULL,
    [COUNT_STATUS]    INT             NULL,
    [EMERGENCY_TYPE]  INT             NULL,
    [COUNT_REASON]    NVARCHAR (1000) NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)   NULL,
    [PLANT_ZONE]      NVARCHAR (5)    NULL,
    [WORKSHOP]        NVARCHAR (4)    NULL,
    [FROM_SAP]        INT             NULL,
    [COMMENTS]        NVARCHAR (200)  NULL,
    [CREATE_USER]     NVARCHAR (50)   NULL,
    [CREATE_DATE]     DATETIME        NULL,
    [UPDATE_USER]     NVARCHAR (50)   NULL,
    [UPDATE_DATE]     DATETIME        NULL,
    [ERP_FLAG]        INT             NULL,
    [COUNT_PARTITION] NVARCHAR (100)  NULL,
    CONSTRAINT [PK_TT_WMM_NOTIFICATION_HEAD] PRIMARY KEY CLUSTERED ([NOTIFICATION_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'盘点分区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_PARTITION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ERP标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否SAP下发', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'FROM_SAP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '盘点原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '盘点紧急程度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'EMERGENCY_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '盘点状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '确认标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'PUBLISH_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '盘点人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '申请人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'APPLY_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划盘点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '动静类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'NOTIFICATION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'NOTIFICATION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库盘点通知单主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_NOTIFICATION_HEAD';

