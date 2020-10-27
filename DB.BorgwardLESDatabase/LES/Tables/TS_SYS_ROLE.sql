CREATE TABLE [LES].[TS_SYS_ROLE] (
    [ROLE_ID]     INT            IDENTITY (1, 1) NOT NULL,
    [ROLE_NAME]   NVARCHAR (64)  NULL,
    [ROLE_TYPE]   INT            NULL,
    [COMMENTS]    NVARCHAR (256) NULL,
    [CREATE_USER] NVARCHAR (50)  NOT NULL,
    [CREATE_DATE] DATETIME       NOT NULL,
    [UPDATE_USER] NVARCHAR (50)  NULL,
    [UPDATE_DATE] DATETIME       NULL,
    CONSTRAINT [IDX_PK_TS_SYS_ROLE] PRIMARY KEY CLUSTERED ([ROLE_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '角色类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'ROLE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '角色ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE', @level2type = N'COLUMN', @level2name = N'ROLE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_角色表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_ROLE';

