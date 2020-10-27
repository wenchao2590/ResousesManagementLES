CREATE TABLE [dbo].[TS_SYS_USER_ROLE_CONDITION] (
    [ID]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]              UNIQUEIDENTIFIER NULL,
    [CONDITION_NAME]   NVARCHAR (64)    NULL,
    [TABLE_NAME]       NVARCHAR (64)    NULL,
    [FIELD_NAME]       NVARCHAR (64)    NULL,
    [ATTRIBUTE_NAME]   NVARCHAR (64)    NULL,
    [DATA_TYPE]        INT              NULL,
    [CONTROL_TYPE]     INT              NULL,
    [EXTEND_ATTRIBUTE] NVARCHAR (1024)  NULL,
    [VALID_FLAG]       BIT              NULL,
    [CREATE_USER]      NVARCHAR (32)    NULL,
    [CREATE_DATE]      DATETIME         NULL,
    [MODIFY_USER]      NVARCHAR (32)    NULL,
    [MODIFY_DATE]      DATETIME         NULL,
    CONSTRAINT [PK__TS_SYS_U__3214EC27A0BA13A2] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扩展属性', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE_CONDITION', @level2type = N'COLUMN', @level2name = N'EXTEND_ATTRIBUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'控件类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE_CONDITION', @level2type = N'COLUMN', @level2name = N'CONTROL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE_CONDITION', @level2type = N'COLUMN', @level2name = N'DATA_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'属性名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE_CONDITION', @level2type = N'COLUMN', @level2name = N'ATTRIBUTE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'字段名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE_CONDITION', @level2type = N'COLUMN', @level2name = N'FIELD_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据库表名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE_CONDITION', @level2type = N'COLUMN', @level2name = N'TABLE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE_CONDITION', @level2type = N'COLUMN', @level2name = N'CONDITION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'例外权限条件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE_CONDITION';

