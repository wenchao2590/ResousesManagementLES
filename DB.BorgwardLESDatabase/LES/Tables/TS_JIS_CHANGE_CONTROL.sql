CREATE TABLE [LES].[TS_JIS_CHANGE_CONTROL] (
    [SEQID]            INT            NOT NULL,
    [PLANT]            NVARCHAR (5)   NULL,
    [ASSEMBLY_LINE]    NVARCHAR (10)  NULL,
    [ISPASS]           INT            NOT NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [LAST_UPDATE_DATE] DATETIME       NULL,
    CONSTRAINT [PK_TS_JIS_CHANGE_CONTROL] PRIMARY KEY CLUSTERED ([SEQID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)最后更新日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_CHANGE_CONTROL', @level2type = N'COLUMN', @level2name = N'LAST_UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_CHANGE_CONTROL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_CHANGE_CONTROL', @level2type = N'COLUMN', @level2name = N'ISPASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_CHANGE_CONTROL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_CHANGE_CONTROL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_CHANGE_CONTROL', @level2type = N'COLUMN', @level2name = N'SEQID';

