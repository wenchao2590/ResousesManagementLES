CREATE TABLE [dbo].[TS_SYS_MESSAGE] (
    [ID]           BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]          UNIQUEIDENTIFIER NULL,
    [MESSAGE_CODE] NVARCHAR (32)    NULL,
    [MESSAGE_CN]   NVARCHAR (256)   NULL,
    [MESSAGE_EN]   NVARCHAR (256)   NULL,
    [VALID_FLAG]   BIT              NULL,
    [CREATE_DATE]  DATETIME         NULL,
    [CREATE_USER]  NVARCHAR (32)    NULL,
    [MODIFY_DATE]  DATETIME         NULL,
    [MODIFY_USER]  NVARCHAR (32)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'英文信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MESSAGE', @level2type = N'COLUMN', @level2name = N'MESSAGE_EN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'中文信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MESSAGE', @level2type = N'COLUMN', @level2name = N'MESSAGE_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'信息代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MESSAGE', @level2type = N'COLUMN', @level2name = N'MESSAGE_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统提示信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MESSAGE';

