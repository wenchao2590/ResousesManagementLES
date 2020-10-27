CREATE TABLE [LES].[TE_SUPPLIER_IMPORT] (
    [F1]     FLOAT (53)     NULL,
    [供应商名称]  NVARCHAR (255) NULL,
    [主要零部件]  NVARCHAR (255) NULL,
    [供应商代码]  NVARCHAR (255) NULL,
    [供应商名称1] NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商名称1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SUPPLIER_IMPORT', @level2type = N'COLUMN', @level2name = N'供应商名称1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SUPPLIER_IMPORT', @level2type = N'COLUMN', @level2name = N'供应商代码';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '主要零部件', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SUPPLIER_IMPORT', @level2type = N'COLUMN', @level2name = N'主要零部件';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SUPPLIER_IMPORT', @level2type = N'COLUMN', @level2name = N'供应商名称';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'F1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SUPPLIER_IMPORT', @level2type = N'COLUMN', @level2name = N'F1';

