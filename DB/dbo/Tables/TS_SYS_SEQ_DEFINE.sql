CREATE TABLE [dbo].[TS_SYS_SEQ_DEFINE] (
    [Id]          BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]         UNIQUEIDENTIFIER NULL,
    [SEQ_CODE]    NVARCHAR (32)    NULL,
    [SEQ_NAME]    NVARCHAR (64)    NULL,
    [SECTION_NUM] INT              NULL,
    [JOIN_CHAR]   NVARCHAR (4)     NULL,
    [VALID_FLAG]  BIT              NULL,
    [CREATE_USER] NVARCHAR (32)    NULL,
    [CREATE_DATE] DATETIME         NULL,
    [MODIFY_USER] NVARCHAR (32)    NULL,
    [MODIFY_DATE] DATETIME         NULL,
    CONSTRAINT [PK_TM_SYS_SEQ_DEFINE] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区段链接符', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'JOIN_CHAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'段数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'SECTION_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列号名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'SEQ_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列号代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_DEFINE', @level2type = N'COLUMN', @level2name = N'SEQ_CODE';

