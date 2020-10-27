CREATE TABLE [LES].[TM_SYS_ALARM_MAIL_CONFIG] (
    [SYS_ID]        INT             NOT NULL,
    [ALARM_NAME]    NVARCHAR (200)  NULL,
    [ALARM_SUBJECT] NVARCHAR (200)  NULL,
    [MAIL_BODY]     NVARCHAR (1000) NULL,
    [CC_MAIL_GROUP] NVARCHAR (2000) NULL,
    [PLANT]         NVARCHAR (5)    NULL,
    [WORKSHOP]      NVARCHAR (4)    NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)   NULL,
    [PRODUCT]       NVARCHAR (6)    NULL,
    [MAILS]         NVARCHAR (MAX)  NULL,
    [MOBILES]       NVARCHAR (MAX)  NULL,
    [CREATE_USER]   NVARCHAR (50)   NULL,
    [CREATE_DATE]   DATETIME        NULL,
    [UPDATE_USER]   NVARCHAR (50)   NULL,
    [UPDATE_DATE]   DATETIME        NULL,
    CONSTRAINT [IDX_PK_ALARM_MAIL_CONFIG_SYS_ID_ALIAS] PRIMARY KEY CLUSTERED ([SYS_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MOBILES', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'MOBILES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MAILS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'MAILS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'PRODUCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '抄送', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'CC_MAIL_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '邮件内容', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'MAIL_BODY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '邮件主题', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'ALARM_SUBJECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '报警名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'ALARM_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG', @level2type = N'COLUMN', @level2name = N'SYS_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_邮件发送配置配置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_ALARM_MAIL_CONFIG';

