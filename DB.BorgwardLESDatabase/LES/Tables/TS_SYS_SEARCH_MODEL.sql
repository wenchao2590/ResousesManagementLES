CREATE TABLE [LES].[TS_SYS_SEARCH_MODEL] (
    [MODEL_ID]        INT             IDENTITY (1, 1) NOT NULL,
    [MODEL_NAME]      NVARCHAR (100)  NOT NULL,
    [USER_LOGIN_NAME] NVARCHAR (50)   CONSTRAINT [DF_TS_SYS_SEARCH_MODEL_USER_LOGIN_NAME] DEFAULT ((0)) NOT NULL,
    [SEARCH_RULE]     NVARCHAR (4000) NULL,
    [COLUMN_LENGTH]   INT             CONSTRAINT [DF__TS_SYS_SE__COLUM__6BAEFA67] DEFAULT ((3)) NOT NULL,
    [CREATE_USER]     NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]     DATETIME        NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)   NULL,
    [UPDATE_DATE]     DATETIME        NULL,
    CONSTRAINT [PK_TS_SYS_SEARCH_MODEL] PRIMARY KEY CLUSTERED ([MODEL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '显示列数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'COLUMN_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '查找规则', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'SEARCH_RULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户登录名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'USER_LOGIN_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模型名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'MODEL_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模型标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'MODEL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_搜索模型表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL';

