CREATE TABLE [LES].[TR_BAS_MODEL_ASSEMBLY_LINE] (
    [MODEL_ID]      INT            NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NOT NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    CONSTRAINT [IDX_PK_MODEL_ASSEMBLY_LINE_MODEL_ID_ASSEMBLY_LINE] PRIMARY KEY NONCLUSTERED ([MODEL_ID] ASC, [ASSEMBLY_LINE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_MODEL_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_MODEL_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_MODEL_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_MODEL_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_MODEL_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_MODEL_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)模型标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_MODEL_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'MODEL_ID';

