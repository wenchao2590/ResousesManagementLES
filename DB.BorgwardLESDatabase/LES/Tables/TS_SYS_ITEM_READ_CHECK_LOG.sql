CREATE TABLE [LES].[TS_SYS_ITEM_READ_CHECK_LOG] (
    [SEQ_ID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [MODULE]       NVARCHAR (50)  NULL,
    [ITEMNO]       NVARCHAR (50)  NULL,
    [SUPPLIER_NUM] NVARCHAR (30)  NULL,
    [READUSER]     NVARCHAR (20)  NULL,
    [READDATE]     DATETIME       NULL,
    [CHECKRESULT]  NVARCHAR (500) NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [CREATE_USER]  NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]  DATETIME       NOT NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    CONSTRAINT [PK_TS_SYS_ITEM_READ_CHECK_LOG] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结果', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'CHECKRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '阅读日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'READDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '阅读人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'READUSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作数据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'ITEMNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'MODULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_供应商阅读记录表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ITEM_READ_CHECK_LOG';

