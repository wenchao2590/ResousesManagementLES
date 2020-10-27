CREATE TABLE [dbo].[TS_SYS_USER] (
    [ID]            BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]           UNIQUEIDENTIFIER NULL,
    [LOGIN_NAME]    NVARCHAR (32)    NULL,
    [PASSWORD]      NVARCHAR (128)   NULL,
    [EMPLOYEE_NAME] NVARCHAR (32)    NULL,
    [EMAIL]         NVARCHAR (256)   NULL,
    [MOBILE]        NVARCHAR (32)    NULL,
    [OFFICE_PHONE]  NVARCHAR (32)    NULL,
    [USER_STATUS]   INT              NULL,
    [VALID_FLAG]    BIT              NULL,
    [CREATE_USER]   NVARCHAR (32)    NULL,
    [CREATE_DATE]   DATETIME         NULL,
    [MODIFY_USER]   NVARCHAR (32)    NULL,
    [MODIFY_DATE]   DATETIME         NULL,
    [COMMENTS]      NVARCHAR (512)   NULL,
    [FAVORITE_PIC]  NVARCHAR (512)   NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收藏夹图片', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'FAVORITE_PIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'USER_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'办公室电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'OFFICE_PHONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'移动电话', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'MOBILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电子邮件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'真实姓名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'EMPLOYEE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户密码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'PASSWORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_USER', @level2type = N'COLUMN', @level2name = N'LOGIN_NAME';

