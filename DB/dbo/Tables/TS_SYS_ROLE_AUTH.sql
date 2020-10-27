CREATE TABLE [dbo].[TS_SYS_ROLE_AUTH] (
    [ID]              BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]             UNIQUEIDENTIFIER NULL,
    [ROLE_FID]        UNIQUEIDENTIFIER NULL,
    [AUTH_TYPE]       INT              NULL,
    [IS_AUTH]         BIT              NULL,
    [VALID_FLAG]      BIT              NULL,
    [CREATE_USER]     NVARCHAR (32)    NULL,
    [CREATE_DATE]     DATETIME         NULL,
    [MODIFY_USER]     NVARCHAR (32)    NULL,
    [MODIFY_DATE]     DATETIME         NULL,
    [AUTH_SOURCE_FID] UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否授权', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE_AUTH', @level2type = N'COLUMN', @level2name = N'IS_AUTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'授权类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE_AUTH', @level2type = N'COLUMN', @level2name = N'AUTH_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'角色授权', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE_AUTH';

