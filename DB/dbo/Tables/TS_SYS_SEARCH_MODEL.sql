CREATE TABLE [dbo].[TS_SYS_SEARCH_MODEL] (
    [ID]            BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]           UNIQUEIDENTIFIER NULL,
    [SEARCH_NAME]   NVARCHAR (512)   NULL,
    [SEARCH_TYPE]   INT              NULL,
    [COLUMN_LENGTH] INT              NULL,
    [VALID_FLAG]    BIT              NULL,
    [CREATE_USER]   NVARCHAR (32)    NULL,
    [CREATE_DATE]   DATETIME         NULL,
    [MODIFY_USER]   NVARCHAR (32)    NULL,
    [MODIFY_DATE]   DATETIME         NULL,
    CONSTRAINT [PK_TM_SYS_SEARCH_MODEL] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'列数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'COLUMN_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检索规则', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'SEARCH_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检索类型名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEARCH_MODEL', @level2type = N'COLUMN', @level2name = N'SEARCH_NAME';

