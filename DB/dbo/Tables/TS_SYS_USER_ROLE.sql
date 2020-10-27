CREATE TABLE [dbo].[TS_SYS_USER_ROLE] (
    [ID]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]              UNIQUEIDENTIFIER NULL,
    [USER_FID]         UNIQUEIDENTIFIER NULL,
    [ROLE_FID]         UNIQUEIDENTIFIER NULL,
    [ORGANIZATION_FID] UNIQUEIDENTIFIER NULL,
    [VALID_FLAG]       BIT              NULL,
    [CREATE_USER]      NVARCHAR (32)    NULL,
    [CREATE_DATE]      DATETIME         NULL,
    [MODIFY_USER]      NVARCHAR (32)    NULL,
    [MODIFY_DATE]      DATETIME         NULL,
    CONSTRAINT [PK__TS_SYS_U__3214EC27CB8F3A54] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE', @level2type = N'COLUMN', @level2name = N'ROLE_FID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER_ROLE', @level2type = N'COLUMN', @level2name = N'USER_FID';

