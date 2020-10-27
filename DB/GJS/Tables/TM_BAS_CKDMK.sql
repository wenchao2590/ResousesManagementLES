CREATE TABLE [GJS].[TM_BAS_CKDMK] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CODE]        NVARCHAR (8)   NULL,
    [NAME]        NVARCHAR (64)  NULL,
    [ADDR]        NVARCHAR (128) NULL,
    [TEL]         NVARCHAR (32)  NULL,
    [FAX]         NVARCHAR (32)  NULL,
    [LXR]         NVARCHAR (16)  NULL,
    [BZ]          NVARCHAR (256) NULL,
    [VALID_FLAG]  BIT            NULL,
    [CREATE_DATE] DATETIME       NULL,
    [CREATE_USER] NVARCHAR (32)  NULL,
    [MODIFY_DATE] DATETIME       NULL,
    [MODIFY_USER] NVARCHAR (32)  NULL,
    CONSTRAINT [PK_TM_BAS_CKDMK] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CKDMK', @level2type = N'COLUMN', @level2name = N'BZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CKDMK', @level2type = N'COLUMN', @level2name = N'LXR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'传真', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CKDMK', @level2type = N'COLUMN', @level2name = N'FAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电话', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CKDMK', @level2type = N'COLUMN', @level2name = N'TEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地址', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CKDMK', @level2type = N'COLUMN', @level2name = N'ADDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CKDMK', @level2type = N'COLUMN', @level2name = N'NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CKDMK', @level2type = N'COLUMN', @level2name = N'CODE';

