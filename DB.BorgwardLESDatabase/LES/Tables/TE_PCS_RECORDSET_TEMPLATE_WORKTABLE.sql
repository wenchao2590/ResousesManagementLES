CREATE TABLE [LES].[TE_PCS_RECORDSET_TEMPLATE_WORKTABLE] (
    [TMP_IDENTITY]        INT IDENTITY (1, 1) NOT NULL,
    [TMP_REGION_IDENTITY] INT NULL,
    CONSTRAINT [PK_TG_PCS_RecordSetTemplateWorkTable] PRIMARY KEY CLUSTERED ([TMP_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '临时区域标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_RECORDSET_TEMPLATE_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_REGION_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '临时标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_RECORDSET_TEMPLATE_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_PCS 模板工作表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_RECORDSET_TEMPLATE_WORKTABLE';

