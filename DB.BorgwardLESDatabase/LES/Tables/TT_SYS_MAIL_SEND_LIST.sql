CREATE TABLE [LES].[TT_SYS_MAIL_SEND_LIST] (
    [MAIL_SEQ_ID]   INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]         NVARCHAR (5)    NULL,
    [WORKSHOP]      NVARCHAR (4)    NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)   NULL,
    [PRODUCT]       NVARCHAR (6)    NULL,
    [SYS_ID]        INT             NULL,
    [ALARM_NAME]    NVARCHAR (200)  NULL,
    [ALARM_SUBJECT] NVARCHAR (200)  NULL,
    [MAIL_BODY]     NVARCHAR (MAX)  NULL,
    [CC_MAIL_GROUP] NVARCHAR (2000) NULL,
    [MAILS]         NVARCHAR (MAX)  NULL,
    [SEND_STATUS]   INT             NULL,
    [SEND_DATE]     DATETIME        NULL,
    [CREATE_USER]   NVARCHAR (50)   NULL,
    [CREATE_DATE]   DATETIME        NULL,
    [UPDATE_USER]   NVARCHAR (50)   NULL,
    [UPDATE_DATE]   DATETIME        NULL,
    CONSTRAINT [IDX_PK_MAIL_SEND_LIST_MAIL_SEQ_ID] PRIMARY KEY CLUSTERED ([MAIL_SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'SEND_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MAILS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'MAILS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '抄送', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'CC_MAIL_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '邮件内容', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'MAIL_BODY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '邮件主题', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'ALARM_SUBJECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '报警名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'ALARM_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'SYS_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'PRODUCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '邮件组代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST', @level2type = N'COLUMN', @level2name = N'MAIL_SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_邮件发送清单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SYS_MAIL_SEND_LIST';

