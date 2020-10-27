CREATE TABLE [dbo].[TS_SYS_ROLE_USER_CONDITION] (
    [ID]                BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]               UNIQUEIDENTIFIER NULL,
    [USER_ROLE_FID]     UNIQUEIDENTIFIER NULL,
    [USER_FID]          UNIQUEIDENTIFIER NULL,
    [ROLE_FID]          UNIQUEIDENTIFIER NULL,
    [CONDITION_FID]     UNIQUEIDENTIFIER NULL,
    [CONDITION_CONTEXT] NVARCHAR (512)   NULL,
    [VALID_FLAG]        BIT              NULL,
    [CREATE_USER]       NVARCHAR (32)    NULL,
    [CREATE_DATE]       DATETIME         NULL,
    [MODIFY_USER]       NVARCHAR (32)    NULL,
    [MODIFY_DATE]       DATETIME         NULL,
    CONSTRAINT [PK__TS_SYS_R__3214EC27CFC2814F] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'条件内容', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE_USER_CONDITION', @level2type = N'COLUMN', @level2name = N'CONDITION_CONTEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'条件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE_USER_CONDITION', @level2type = N'COLUMN', @level2name = N'CONDITION_FID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE_USER_CONDITION', @level2type = N'COLUMN', @level2name = N'ROLE_FID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE_USER_CONDITION', @level2type = N'COLUMN', @level2name = N'USER_FID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户角色', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE_USER_CONDITION', @level2type = N'COLUMN', @level2name = N'USER_ROLE_FID';

