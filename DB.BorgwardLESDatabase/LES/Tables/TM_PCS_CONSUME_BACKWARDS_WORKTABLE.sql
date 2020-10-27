CREATE TABLE [LES].[TM_PCS_CONSUME_BACKWARDS_WORKTABLE] (
    [TMP_IDENTITY]          INT IDENTITY (1, 1) NOT NULL,
    [TMP_PREVIOUS_IDENTITY] INT NULL,
    CONSTRAINT [IDX_PK_CONSUME_BACKWARDS_WORKTABLE_TMP_IDENTITY] PRIMARY KEY CLUSTERED ([TMP_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '前推ID标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_CONSUME_BACKWARDS_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_PREVIOUS_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '当前标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_CONSUME_BACKWARDS_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS 消耗反推表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_CONSUME_BACKWARDS_WORKTABLE';

