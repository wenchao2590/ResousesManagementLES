CREATE TABLE [dbo].[TS_SYS_ENTITY_FIELD] (
    [ID]                 BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]                UNIQUEIDENTIFIER NULL,
    [ENTITY_FID]         UNIQUEIDENTIFIER NULL,
    [FIELD_NAME]         NVARCHAR (64)    NULL,
    [TABLE_FIELD_NAME]   NVARCHAR (64)    NULL,
    [DISPLAY_NAME_CN]    NVARCHAR (32)    NULL,
    [DISPLAY_NAME_EN]    NVARCHAR (64)    NULL,
    [DISPLAY_ORDER]      INT              NULL,
    [DATA_TYPE]          INT              NULL,
    [CONTROL_TYPE]       INT              NULL,
    [DATA_LENGTH]        INT              NULL,
    [PRECISION]          INT              NULL,
    [DEFAULT_VALUE]      NVARCHAR (64)    NULL,
    [NULLENABLE]         BIT              NULL,
    [REGEX]              NVARCHAR (128)   NULL,
    [ERROR_MSG]          NVARCHAR (MAX)   NULL,
    [MIN_VALUE]          INT              NULL,
    [MAX_VALUE]          INT              NULL,
    [EDITABLE]           BIT              NULL,
    [EDIT_DISPLAY_WIDTH] NVARCHAR (16)    NULL,
    [LISTABLE]           BIT              NULL,
    [LIST_DISPLAY_WIDTH] NVARCHAR (16)    NULL,
    [EXTEND1]            NVARCHAR (MAX)   NULL,
    [EXTEND2]            NVARCHAR (MAX)   NULL,
    [EXTEND3]            NVARCHAR (MAX)   NULL,
    [VALID_FLAG]         BIT              NULL,
    [CREATE_USER]        NVARCHAR (32)    NULL,
    [CREATE_DATE]        DATETIME         NULL,
    [MODIFY_USER]        NVARCHAR (32)    NULL,
    [MODIFY_DATE]        DATETIME         NULL,
    [EDIT_READONLY]      INT              NULL,
    [TAB_TITLE_CODE]     NVARCHAR (128)   NULL,
    [SORTABLE]           BIT              NULL,
    [EXPORT_EXCEL_FLAG]  BIT              NULL,
    [EXPORT_EXCEL_ORDER] INT              NULL,
    [TOOLTIP_HELPER_CN]  NVARCHAR (512)   NULL,
    [TOOLTIP_HELPER_EN]  NVARCHAR (512)   NULL,
    CONSTRAINT [PK_TM_SYS_ENTITY_FIELD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'导出顺序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'EXPORT_EXCEL_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否导出', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'EXPORT_EXCEL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否允许排序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'SORTABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'选项卡', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'TAB_TITLE_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'只读类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'EDIT_READONLY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'EXTEND3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'EXTEND2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'EXTEND1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'列表显示宽度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'LIST_DISPLAY_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'列表显示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'LISTABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'编辑显示宽度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'EDIT_DISPLAY_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'编辑显示', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'EDITABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最大值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'MAX_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'MIN_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报错信息', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'正则表达式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'REGEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否为空', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'NULLENABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'默认值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'DEFAULT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'精度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'PRECISION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据长度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'DATA_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'控件类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'CONTROL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'DATA_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'DISPLAY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'英文名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'DISPLAY_NAME_EN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'中文名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'DISPLAY_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'字段名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'TABLE_FIELD_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'属性名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY_FIELD', @level2type = N'COLUMN', @level2name = N'FIELD_NAME';

