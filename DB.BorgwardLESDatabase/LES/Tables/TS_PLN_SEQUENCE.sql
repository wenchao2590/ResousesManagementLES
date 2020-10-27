CREATE TABLE [LES].[TS_PLN_SEQUENCE] (
    [SEQUENCE_NAME]    NVARCHAR (32)  NOT NULL,
    [CURRENT_VALUE]    INT            NOT NULL,
    [INIT_VALUE]       INT            NOT NULL,
    [MAX_VALUE]        INT            NULL,
    [STEP_VALUE]       INT            NOT NULL,
    [PREFIX_STRING]    NVARCHAR (12)  NULL,
    [POSTFIX_STRING]   NVARCHAR (12)  NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [LAST_UPDATE_DATE] DATETIME       NULL,
    CONSTRAINT [PK_TS_PLN_SEQUENCE] PRIMARY KEY CLUSTERED ([SEQUENCE_NAME] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)最后更新日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'LAST_UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)后缀', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'POSTFIX_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)前缀', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'PREFIX_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)步长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'STEP_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)最大值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'MAX_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)初始值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'INIT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)当前值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'CURRENT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)序列名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_PLN_SEQUENCE', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NAME';

