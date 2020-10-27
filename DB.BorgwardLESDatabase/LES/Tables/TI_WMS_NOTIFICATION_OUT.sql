CREATE TABLE [LES].[TI_WMS_NOTIFICATION_OUT] (
    [SEQ_ID]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [IBLNR]        NVARCHAR (10)   NULL,
    [GJAHR]        NVARCHAR (4)    NULL,
    [WERKS]        NVARCHAR (4)    NULL,
    [LGORT]        NVARCHAR (18)   NULL,
    [MATNR]        NVARCHAR (18)   NULL,
    [GIDAT]        DATETIME        NULL,
    [MENGE]        NUMERIC (18, 2) NULL,
    [MEINS]        NVARCHAR (8)    NULL,
    [XLOEK]        NVARCHAR (1)    NULL,
    [XZAEL]        NVARCHAR (1)    NULL,
    [XDIFF]        NVARCHAR (1)    NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    CONSTRAINT [PK_TI_WMS_NOTIFICATION_OUT] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '差异调整状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'XDIFF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '盘点状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'XZAEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '删除标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'XLOEK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '账面库存数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'MENGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划盘点日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'GIDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '年度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'GJAHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '盘点凭证', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'IBLNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_盘点结果', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_NOTIFICATION_OUT';

