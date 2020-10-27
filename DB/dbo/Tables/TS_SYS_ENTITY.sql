CREATE TABLE [dbo].[TS_SYS_ENTITY] (
    [ID]           BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]          UNIQUEIDENTIFIER NULL,
    [ENTITY_NAME]  NVARCHAR (32)    NULL,
    [TABLE_NAMES]  NVARCHAR (128)   NULL,
    [COMMENTS]     NVARCHAR (MAX)   NULL,
    [VALID_FLAG]   BIT              NULL,
    [CREATE_USER]  NVARCHAR (32)    NULL,
    [CREATE_DATE]  DATETIME         NULL,
    [MODIFY_USER]  NVARCHAR (32)    NULL,
    [MODIFY_DATE]  DATETIME         NULL,
    [ENTITY_TYPE]  INT              NULL,
    [PARENT_FIELD] NVARCHAR (128)   NULL,
    [DEFAULT_SORT] NVARCHAR (64)    NULL,
    [AUTH_CONFIG]  NVARCHAR (128)   NULL,
    [TAB_TITLES]   NVARCHAR (512)   NULL,
    [KEY_FIELDS]   NVARCHAR (128)   NULL,
    CONSTRAINT [PK_TM_SYS_ENTITY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY', @level2type = N'COLUMN', @level2name = N'KEY_FIELDS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'权限配置', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY', @level2type = N'COLUMN', @level2name = N'AUTH_CONFIG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'默认排序字段', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY', @level2type = N'COLUMN', @level2name = N'DEFAULT_SORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展配置', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY', @level2type = N'COLUMN', @level2name = N'PARENT_FIELD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据模型类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY', @level2type = N'COLUMN', @level2name = N'ENTITY_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'设计表名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY', @level2type = N'COLUMN', @level2name = N'TABLE_NAMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据模型名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ENTITY', @level2type = N'COLUMN', @level2name = N'ENTITY_NAME';

