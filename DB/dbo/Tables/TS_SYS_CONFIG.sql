CREATE TABLE [dbo].[TS_SYS_CONFIG] (
    [ID]           BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]          UNIQUEIDENTIFIER NULL,
    [NAME]         NVARCHAR (128)   NULL,
    [CODE]         NVARCHAR (64)    NULL,
    [CONFIG_VALUE] NVARCHAR (MAX)   NULL,
    [COMMENTS]     NVARCHAR (512)   NULL,
    [VALID_FLAG]   BIT              NULL,
    [CREATE_USER]  NVARCHAR (32)    NULL,
    [CREATE_DATE]  DATETIME         NULL,
    [MODIFY_USER]  NVARCHAR (32)    NULL,
    [MODIFY_DATE]  DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'CONFIG_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统配置', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG';

