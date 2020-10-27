CREATE TABLE [LES].[TT_BAS_CHANGE_ORDERS] (
    [SEQ_ID]           INT            IDENTITY (1, 1) NOT NULL,
    [ORDER_NO]         NVARCHAR (20)  NOT NULL,
    [WERK]             NVARCHAR (4)   NOT NULL,
    [RECALCULATE_FLAG] INT            NULL,
    [CHANGE_FLAG]      INT            NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [CREATE_USER]      NVARCHAR (50)  NULL,
    [CREATE_DATE]      DATETIME       NULL,
    [UPDATE_USER]      NVARCHAR (50)  NULL,
    [UPDATE_DATE]      DATETIME       NULL,
    CONSTRAINT [PK_TT_BAS_CHANGE_ORDERS] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)变更标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'CHANGE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)重算标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'RECALCULATE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)接口_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'WERK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_CHANGE_ORDERS', @level2type = N'COLUMN', @level2name = N'SEQ_ID';

