CREATE TABLE [dbo].[TS_SYS_SEARCH_MODEL_CONDITION] (
    [ID]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]              UNIQUEIDENTIFIER NULL,
    [SEARCH_FID]       UNIQUEIDENTIFIER NULL,
    [CONTROL_ID]       NVARCHAR (64)    NULL,
    [CONTROL_TYPE]     NVARCHAR (64)    NULL,
    [DEFAULT_VALUE]    NVARCHAR (64)    NULL,
    [DISPLAY_ORDER]    INT              NULL,
    [MAX_LENGTH]       INT              NULL,
    [LABEL_TEXT]       NVARCHAR (64)    NULL,
    [TABLE_NAME]       NVARCHAR (64)    NULL,
    [COLUMN_NAME]      NVARCHAR (64)    NULL,
    [COLUMN_TYPE]      NVARCHAR (64)    NULL,
    [REGEX_EXPRESSION] NVARCHAR (64)    NULL,
    [DATASEARCH_TYPE]  NVARCHAR (32)    NULL,
    [CODE_NAME]        NVARCHAR (64)    NULL,
    [EXTEND_FIELD1]    NVARCHAR (128)   NULL,
    [EXTEND_FIELD2]    NVARCHAR (128)   NULL,
    [EXTEND_FIELD3]    NVARCHAR (MAX)   NULL,
    [EXTEND_FIELD4]    NVARCHAR (128)   NULL,
    [EXTEND_FIELD5]    NVARCHAR (128)   NULL,
    [EXTEND_FIELD6]    NVARCHAR (128)   NULL,
    [EXTEND_FIELD7]    NVARCHAR (128)   NULL,
    [EXTEND_FIELD8]    NVARCHAR (128)   NULL,
    [EXTEND_FIELD9]    NVARCHAR (128)   NULL,
    [EXTEND_FIELD10]   NVARCHAR (128)   NULL,
    [VALID_FLAG]       BIT              NULL,
    [CREATE_USER]      NVARCHAR (32)    NULL,
    [CREATE_DATE]      DATETIME         NULL,
    [MODIFY_USER]      NVARCHAR (32)    NULL,
    [MODIFY_DATE]      DATETIME         NULL,
    CONSTRAINT [PK_TM_SYS_SEARCH_MODEL_CONDITION] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性10', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性9', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性8', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性7', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性6', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_FIELD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'CODE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'匹配类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'DATASEARCH_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'正则表达式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'REGEX_EXPRESSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据字段类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'COLUMN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据字段名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'COLUMN_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据表名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'TABLE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'LABEL_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'字段最大值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'MAX_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示顺序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'DISPLAY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'默认值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'DEFAULT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'控件类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION', @level2type = N'COLUMN', @level2name = N'CONTROL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检索条件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL_CONDITION';

