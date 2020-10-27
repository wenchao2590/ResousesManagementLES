CREATE TABLE [dbo].[TS_SYS_CODE] (
    [ID]           BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]          UNIQUEIDENTIFIER NULL,
    [CODE_NAME]    NVARCHAR (64)    NULL,
    [CODE_NAME_CN] NVARCHAR (64)    NULL,
    [COMMENTS]     NVARCHAR (512)   NULL,
    [VALID_FLAG]   BIT              NULL,
    [CREATE_USER]  NVARCHAR (32)    NULL,
    [CREATE_DATE]  DATETIME         NULL,
    [MODIFY_USER]  NVARCHAR (32)    NULL,
    [MODIFY_DATE]  DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CODE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码描述', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CODE', @level2type = N'COLUMN', @level2name = N'CODE_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CODE', @level2type = N'COLUMN', @level2name = N'CODE_NAME';

