CREATE TABLE [LES].[TS_SYS_USER] (
    [USER_ID]              INT            IDENTITY (1, 1) NOT NULL,
    [USER_LOGIN_NAME]      NVARCHAR (50)  NOT NULL,
    [USER_PASSWORD]        NVARCHAR (100) NULL,
    [USER_TYPE]            INT            NOT NULL,
    [EMPLOYEE_NAME]        NVARCHAR (50)  NOT NULL,
    [EMAIL]                NVARCHAR (100) NULL,
    [MOBILE]               NVARCHAR (100) NULL,
    [OFFICE_PHONE]         NVARCHAR (100) NULL,
    [COMPANY]              NVARCHAR (100) NULL,
    [DEPARTMENT]           NVARCHAR (100) NULL,
    [USER_STATUS]          INT            NOT NULL,
    [SHIFT]                INT            NULL,
    [PASSWORD1]            NVARCHAR (100) NULL,
    [PASSWORD2]            NVARCHAR (100) NULL,
    [PASSWORD3]            NVARCHAR (100) NULL,
    [PASSWORD_EXPIRE_TIME] DATETIME       NULL,
    [FAIL_LOGIN]           INT            NOT NULL,
    [LAST_LOGIN_TIME]      DATETIME       NULL,
    [CREATE_USER]          NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]          DATETIME       NOT NULL,
    [UPDATE_USER]          NVARCHAR (50)  NULL,
    [UPDATE_DATE]          DATETIME       NULL,
    [COMMENTS]             NVARCHAR (256) NULL,
    CONSTRAINT [IDX_PK_TS_SYS_USER] PRIMARY KEY CLUSTERED ([USER_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上次登录时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'LAST_LOGIN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '登录失败次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'FAIL_LOGIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '密码过期时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'PASSWORD_EXPIRE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '密码3', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'PASSWORD3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '密码2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'PASSWORD2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '密码1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'PASSWORD1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_班次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'SHIFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'USER_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '部门', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'DEPARTMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '公司', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'COMPANY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '办公室电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'OFFICE_PHONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '移动电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'MOBILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '电子邮件', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户姓名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'EMPLOYEE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'USER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '密码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'USER_PASSWORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户登录名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'USER_LOGIN_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'USER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_用户表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_USER';

