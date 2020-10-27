CREATE TABLE [LES].[TS_SYS_IDENTITY_SEGMENT] (
    [NAME]                 NVARCHAR (128) NOT NULL,
    [LAST_MAX_IDENTITY_ID] BIGINT         NOT NULL,
    [LAST_MIN_IDENTITY_ID] BIGINT         NOT NULL,
    [LAST_DATETIME]        DATETIME       NULL,
    [Comments]             NVARCHAR (128) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_IDENTITY_SEGMENT', @level2type = N'COLUMN', @level2name = N'Comments';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_IDENTITY_SEGMENT', @level2type = N'COLUMN', @level2name = N'LAST_DATETIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)最小_IDENTITY_ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_IDENTITY_SEGMENT', @level2type = N'COLUMN', @level2name = N'LAST_MIN_IDENTITY_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)最大_IDENTITY_ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_IDENTITY_SEGMENT', @level2type = N'COLUMN', @level2name = N'LAST_MAX_IDENTITY_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_IDENTITY_SEGMENT', @level2type = N'COLUMN', @level2name = N'NAME';

