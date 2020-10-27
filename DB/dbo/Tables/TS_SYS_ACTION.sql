CREATE TABLE [dbo].[TS_SYS_ACTION] (
    [ID]             BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]            UNIQUEIDENTIFIER NULL,
    [ACTION_NAME]    NVARCHAR (64)    NULL,
    [ACTION_NAME_CN] NVARCHAR (64)    NULL,
    [ACTION_TYPE]    INT              NULL,
    [COMMENTS]       NVARCHAR (256)   NULL,
    [ICON_URL]       NVARCHAR (1024)  NULL,
    [VALID_FLAG]     BIT              NULL,
    [CREATE_USER]    NVARCHAR (32)    NULL,
    [CREATE_DATE]    DATETIME         NULL,
    [MODIFY_USER]    NVARCHAR (32)    NULL,
    [MODIFY_DATE]    DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ICON_URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'动作类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'中文名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'动作名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_NAME';

