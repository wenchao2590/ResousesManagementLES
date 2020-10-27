CREATE TABLE [dbo].[TS_SYS_ORGANIZATION] (
    [ID]                BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]               UNIQUEIDENTIFIER NULL,
    [PARENT_FID]        UNIQUEIDENTIFIER NULL,
    [CODE]              NVARCHAR (64)    NULL,
    [NAME]              NVARCHAR (128)   NULL,
    [COMMENTS]          NVARCHAR (512)   NULL,
    [ORGANZATION_LEVEL] INT              NULL,
    [VALID_FLAG]        BIT              NULL,
    [CREATE_USER]       NVARCHAR (32)    NULL,
    [CREATE_DATE]       DATETIME         NULL,
    [MODIFY_USER]       NVARCHAR (32)    NULL,
    [MODIFY_DATE]       DATETIME         NULL,
    CONSTRAINT [PK_TS_SYS_ORGANZATION] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'级别', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ORGANIZATION', @level2type = N'COLUMN', @level2name = N'ORGANZATION_LEVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ORGANIZATION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ORGANIZATION', @level2type = N'COLUMN', @level2name = N'NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ORGANIZATION', @level2type = N'COLUMN', @level2name = N'CODE';

