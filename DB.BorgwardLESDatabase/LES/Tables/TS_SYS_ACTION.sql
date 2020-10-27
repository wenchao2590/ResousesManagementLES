CREATE TABLE [LES].[TS_SYS_ACTION] (
    [ACTION_ID]      INT            IDENTITY (1, 1) NOT NULL,
    [ACTION_NAME]    NVARCHAR (100) NOT NULL,
    [ACTION_NAME_CN] NVARCHAR (100) NULL,
    [ACTION_TYPE]    INT            NULL,
    [COMMENTS]       NVARCHAR (200) NULL,
    [ACTION_URL]     NVARCHAR (100) NOT NULL,
    [ACTION_ORDER]   INT            NULL,
    [ICON_URL]       NVARCHAR (200) NULL,
    [CLIENT_JS]      NVARCHAR (MAX) NULL,
    [SECTION]        NVARCHAR (10)  NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [CREATE_DATE]    DATETIME       NOT NULL,
    [CREATE_USER]    NVARCHAR (50)  NOT NULL,
    CONSTRAINT [IDX_PK_TS_SYS_ACTION_ACTION_ID] PRIMARY KEY CLUSTERED ([ACTION_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '脚本', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'CLIENT_JS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'URL', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '动作类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '动作名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '动作ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION', @level2type = N'COLUMN', @level2name = N'ACTION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_供应商与流水线关联表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ACTION';

