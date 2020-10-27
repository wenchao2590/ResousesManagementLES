CREATE TABLE [dbo].[TS_SYS_REPORT] (
    [Id]          BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]         UNIQUEIDENTIFIER NULL,
    [NAME]        NVARCHAR (64)    NULL,
    [NAME_EN]     NVARCHAR (128)   NULL,
    [IS_AUTH]     BIT              NULL,
    [REPORT_TYPE] INT              NULL,
    [SORT_FIELD]  NVARCHAR (32)    NULL,
    [KEY_FIELD]   NVARCHAR (MAX)   NULL,
    [SQL_STRING]  NVARCHAR (MAX)   NULL,
    [PLANT]       NVARCHAR (8)     NULL,
    [VALID_FLAG]  BIT              NULL,
    [CREATE_USER] NVARCHAR (32)    NULL,
    [CREATE_DATE] DATETIME         NULL,
    [MODIFY_USER] NVARCHAR (32)    NULL,
    [MODIFY_DATE] DATETIME         NULL,
    CONSTRAINT [PK_TM_SYS_REPORT] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'厂区', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'查询语句', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT', @level2type = N'COLUMN', @level2name = N'SQL_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'关键字段', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT', @level2type = N'COLUMN', @level2name = N'KEY_FIELD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序字段', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT', @level2type = N'COLUMN', @level2name = N'SORT_FIELD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报表类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT', @level2type = N'COLUMN', @level2name = N'REPORT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否需要授权', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT', @level2type = N'COLUMN', @level2name = N'IS_AUTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'英文名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT', @level2type = N'COLUMN', @level2name = N'NAME_EN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT', @level2type = N'COLUMN', @level2name = N'NAME';

