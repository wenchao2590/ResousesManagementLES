CREATE TABLE [LES].[TS_SYS_MENU_ACTION] (
    [MENU_ID]     INT           NOT NULL,
    [ACTION_ID]   INT           NOT NULL,
    [CREATE_USER] NVARCHAR (50) NOT NULL,
    [CREATE_DATE] DATETIME      NOT NULL,
    CONSTRAINT [IDX_PK_TR_SYS_MENU_ACTION] PRIMARY KEY CLUSTERED ([MENU_ID] ASC, [ACTION_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU_ACTION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU_ACTION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '动作ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '菜单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU_ACTION', @level2type = N'COLUMN', @level2name = N'MENU_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_菜单动作关联表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU_ACTION';

