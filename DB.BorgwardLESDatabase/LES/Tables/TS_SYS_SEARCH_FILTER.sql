CREATE TABLE [LES].[TS_SYS_SEARCH_FILTER] (
    [FILTER_ID]               INT            IDENTITY (1, 1) NOT NULL,
    [USER_ID]                 INT            NOT NULL,
    [FILTER_NAME]             NVARCHAR (50)  NOT NULL,
    [FILTER_CODE]             NVARCHAR (50)  NOT NULL,
    [FILTER_VALUE]            NVARCHAR (100) NOT NULL,
    [FILTER_VALUE_NAME]       NVARCHAR (100) NULL,
    [CHILD_FILTER_NAME]       NVARCHAR (50)  NULL,
    [CHILD_FILTER_CODE]       NVARCHAR (50)  NULL,
    [CHILD_FILTER_VALUE]      VARCHAR (100)  NULL,
    [CHILD_FILTER_VALUE_NAME] NVARCHAR (50)  NULL,
    [CREATE_USER]             NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]             DATETIME       NOT NULL,
    [UPDATE_USER]             NVARCHAR (50)  NOT NULL,
    [UPDATE_DATE]             DATETIME       NOT NULL,
    CONSTRAINT [PK_TS_SYS_SEARCH_FILTER] PRIMARY KEY CLUSTERED ([FILTER_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '子过滤条件值名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'CHILD_FILTER_VALUE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '子过滤条件值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'CHILD_FILTER_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '子过滤条件编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'CHILD_FILTER_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '子过滤条件名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'CHILD_FILTER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过滤条件值名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'FILTER_VALUE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过滤条件值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'FILTER_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过滤条件名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'FILTER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'USER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过滤标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER', @level2type = N'COLUMN', @level2name = N'FILTER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_搜索过滤表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_FILTER';

