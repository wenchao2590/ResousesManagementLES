CREATE TABLE [LES].[TT_ORDER_IMPORT_RESOLVE] (
    [SEQ_ID]      INT      NOT NULL,
    [ISPASS]      INT      NULL,
    [UPDATE_DATE] DATETIME NULL,
    CONSTRAINT [PK_TT_ORDER_IMPORT_RESOLVE] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ORDER_IMPORT_RESOLVE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ORDER_IMPORT_RESOLVE', @level2type = N'COLUMN', @level2name = N'ISPASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ORDER_IMPORT_RESOLVE', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_订单导入与分解锁表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ORDER_IMPORT_RESOLVE';

