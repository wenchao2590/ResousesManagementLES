CREATE TABLE [dbo].[TS_SYS_HELP] (
    [ID]              BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]             UNIQUEIDENTIFIER NULL,
    [MENU_FID]        UNIQUEIDENTIFIER NULL,
    [HELP_CONTEXT_CN] NVARCHAR (MAX)   NULL,
    [HELP_CONTEXT_EN] NVARCHAR (MAX)   NULL,
    [VALID_FLAG]      BIT              NULL,
    [CREATE_USER]     NVARCHAR (32)    NULL,
    [CREATE_DATE]     DATETIME         NULL,
    [MODIFY_USER]     NVARCHAR (32)    NULL,
    [MODIFY_DATE]     DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'英文帮助', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HELP', @level2type = N'COLUMN', @level2name = N'HELP_CONTEXT_EN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'中文帮助', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HELP', @level2type = N'COLUMN', @level2name = N'HELP_CONTEXT_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'菜单', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HELP', @level2type = N'COLUMN', @level2name = N'MENU_FID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统帮助', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_HELP';

