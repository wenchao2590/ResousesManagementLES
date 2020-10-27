CREATE TABLE [LES].[TS_SYS_SEARCH_MODEL_CONDITION] (
    [CONDITION_ID]     INT             IDENTITY (1, 1) NOT NULL,
    [MODEL_ID]         INT             NOT NULL,
    [CONTROL_ID]       NVARCHAR (50)   NOT NULL,
    [CONTROL_TYPE]     NVARCHAR (50)   NOT NULL,
    [DEFAULT_VALUE]    NVARCHAR (50)   NULL,
    [DISPLAY_ORDER]    INT             NULL,
    [MAX_LENGTH]       INT             NULL,
    [LABEL_TEXT]       NVARCHAR (50)   NOT NULL,
    [TABLE_NAME]       NVARCHAR (50)   NULL,
    [COLUMN_NAME]      NVARCHAR (50)   NOT NULL,
    [COLUMN_TYPE]      NVARCHAR (50)   NOT NULL,
    [REGEX_EXPRESSION] NVARCHAR (2000) NULL,
    [DATASEARCH_TYPE]  NVARCHAR (20)   NULL,
    [CODE_NAME]        NVARCHAR (50)   NULL,
    [EXTEND_FIELD_1]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_2]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_3]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_4]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_5]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_6]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_7]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_8]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_9]   NVARCHAR (100)  NULL,
    [EXTEND_FIELD_10]  NVARCHAR (100)  NULL,
    [CREATE_USER]      NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]      DATETIME        NOT NULL,
    [UPDATE_USER]      NVARCHAR (50)   NULL,
    [UPDATE_DATE]      DATETIME        NULL,
    CONSTRAINT [PK_TS_SYS_SEARCH_MODEL_CONDITION] PRIMARY KEY CLUSTERED ([CONDITION_ID] ASC),
    CONSTRAINT [FK_TS_SEARCH_MODEL_CONDITION] FOREIGN KEY ([MODEL_ID]) REFERENCES [LES].[TS_SYS_SEARCH_MODEL] ([MODEL_ID]) ON DELETE CASCADE
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段10', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段9', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段8', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段7', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段6', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段5', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段4', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段3', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扩展字段1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD_1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '代码名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'CODE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '匹配类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'DATASEARCH_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '正则表达式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'REGEX_EXPRESSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '列类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'COLUMN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '列名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'COLUMN_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '表名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'TABLE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标签提示名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'LABEL_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最大长度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'MAX_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '显示顺序', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'DISPLAY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '缺省值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'DEFAULT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '控件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'CONTROL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '控件标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'CONTROL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条件标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'CONDITION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_搜索模型条件表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION';

