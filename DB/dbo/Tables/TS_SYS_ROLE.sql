CREATE TABLE [dbo].[TS_SYS_ROLE] (
    [ID]          BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]         UNIQUEIDENTIFIER NULL,
    [ROLE_NAME]   NVARCHAR (32)    NULL,
    [ROLE_TYPE]   INT              NULL,
    [COMMENTS]    NVARCHAR (1024)  NULL,
    [VALID_FLAG]  BIT              NULL,
    [CREATE_USER] NVARCHAR (32)    NULL,
    [CREATE_DATE] DATETIME         NULL,
    [MODIFY_USER] NVARCHAR (32)    NULL,
    [MODIFY_DATE] DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'ROLE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色名字', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'ROLE_NAME';

