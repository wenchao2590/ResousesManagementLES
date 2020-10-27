CREATE TABLE [LES].[TS_SYS_SEQ_CURRENT_VALUE] (
    [ID]             BIGINT        IDENTITY (1, 1) NOT NULL,
    [SEQ_CODE]       NVARCHAR (32) NULL,
    [SEQ_SECTION_ID] BIGINT        NULL,
    [QUERY_VALUE]    NVARCHAR (64) NULL,
    [CURRENT_VALUE]  INT           NULL,
    [VALID_FLAG]     BIT           NULL,
    [CREATE_USER]    NVARCHAR (32) NOT NULL,
    [CREATE_DATE]    DATETIME      NOT NULL,
    [UPDATE_USER]    NVARCHAR (32) NULL,
    [UPDATE_DATE]    DATETIME      NULL,
    CONSTRAINT [PK_TS_SYS_SEQ_CURRENT_VALUE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)当前值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'CURRENT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'QUERY_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'SEQ_SECTION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'SEQ_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'ID';

