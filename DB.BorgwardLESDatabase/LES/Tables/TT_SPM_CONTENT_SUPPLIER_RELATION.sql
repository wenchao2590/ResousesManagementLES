CREATE TABLE [LES].[TT_SPM_CONTENT_SUPPLIER_RELATION] (
    [RELATION_ID]  INT            IDENTITY (1, 1) NOT NULL,
    [CONTENT_NO]   INT            NOT NULL,
    [SUPPLIER_NUM] VARCHAR (20)   NOT NULL,
    [READSTATUS]   INT            CONSTRAINT [DF_TT_SPM_CONTENT_SUPPLIER_RELATION_READSTATUS] DEFAULT ((2)) NOT NULL,
    [READ_USER]    NVARCHAR (50)  NULL,
    [READ_DATE]    DATETIME       NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [CREATE_USER]  NVARCHAR (50)  NULL,
    [CREATE_DATE]  DATETIME       NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    CONSTRAINT [PK_TT_SPM_CONTENT_SUPPLIER_RELATION] PRIMARY KEY CLUSTERED ([RELATION_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '阅读时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'READ_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '阅读用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'READ_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '阅读状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'READSTATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '公告号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'CONTENT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION', @level2type = N'COLUMN', @level2name = N'RELATION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_通知公告供应商关系表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT_SUPPLIER_RELATION';

