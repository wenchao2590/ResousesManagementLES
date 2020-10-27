CREATE TABLE [LES].[TT_SPM_ATTACHMENTS] (
    [ATTACHMENT_ID] INT             IDENTITY (1, 1) NOT NULL,
    [CONTENT_NO]    INT             NOT NULL,
    [FILEURL]       NVARCHAR (200)  NOT NULL,
    [FILENAME]      NVARCHAR (100)  NOT NULL,
    [COMMENTS]      NVARCHAR (200)  NULL,
    [CREATE_USER]   NVARCHAR (100)  NOT NULL,
    [CREATE_DATE]   DATETIME        NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)   NULL,
    [UPDATE_DATE]   DATETIME        NULL,
    [Contents]      VARBINARY (MAX) NULL,
    CONSTRAINT [PK_OA_Attachments] PRIMARY KEY CLUSTERED ([ATTACHMENT_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '文件地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS', @level2type = N'COLUMN', @level2name = N'FILEURL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '公告号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS', @level2type = N'COLUMN', @level2name = N'CONTENT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS', @level2type = N'COLUMN', @level2name = N'ATTACHMENT_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_通知公告表附件表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_ATTACHMENTS';

