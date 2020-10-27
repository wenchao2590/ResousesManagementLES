CREATE TABLE [LES].[TS_SYS_SEQ_DEFINE] (
    [SEQ_CODE]    NVARCHAR (32) NOT NULL,
    [SEQ_NAME]    NVARCHAR (64) NULL,
    [SECTION_NUM] INT           NULL,
    [JOIN_CHAR]   NVARCHAR (4)  NULL,
    [VALID_FLAG]  BIT           NULL,
    [CREATE_USER] NVARCHAR (32) NOT NULL,
    [CREATE_DATE] DATETIME      NOT NULL,
    [UPDATE_USER] NVARCHAR (32) NULL,
    [UPDATE_DATE] DATETIME      NULL,
    CONSTRAINT [PK_TS_SYS_SEQ_DEFINE] PRIMARY KEY CLUSTERED ([SEQ_CODE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'JOIN_CHAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'SECTION_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'SEQ_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'SEQ_CODE';

