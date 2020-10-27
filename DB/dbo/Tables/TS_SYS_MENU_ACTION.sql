CREATE TABLE [dbo].[TS_SYS_MENU_ACTION] (
    [ID]           BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]          UNIQUEIDENTIFIER NULL,
    [MENU_FID]     UNIQUEIDENTIFIER NULL,
    [ACTION_FID]   UNIQUEIDENTIFIER NULL,
    [CLIENT_JS]    NVARCHAR (MAX)   NULL,
    [ACTION_ORDER] INT              NULL,
    [VALID_FLAG]   BIT              NULL,
    [CREATE_USER]  NVARCHAR (32)    NULL,
    [CREATE_DATE]  DATETIME         NULL,
    [MODIFY_USER]  NVARCHAR (32)    NULL,
    [MODIFY_DATE]  DATETIME         NULL,
    [NEED_AUTH]    BIT              NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'授权', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU_ACTION', @level2type = N'COLUMN', @level2name = N'NEED_AUTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'事件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU_ACTION', @level2type = N'COLUMN', @level2name = N'CLIENT_JS';

