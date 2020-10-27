CREATE TABLE [LES].[TR_SYS_USER_ROLE] (
    [ROLE_ID]     INT           NOT NULL,
    [USER_ID]     INT           NOT NULL,
    [CREATE_USER] NVARCHAR (50) NOT NULL,
    [CREATE_DATE] DATETIME      NOT NULL,
    CONSTRAINT [IDX_PK_TR_SYS_USER_ROLE] PRIMARY KEY CLUSTERED ([ROLE_ID] ASC, [USER_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_ROLE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_ROLE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_ROLE', @level2type = N'COLUMN', @level2name = N'ROLE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_用户角色关联表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_ROLE';

