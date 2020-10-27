CREATE TABLE [GJS].[TM_BAS_DB1] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [F9]          NVARCHAR (16)  NULL,
    [F8]          NVARCHAR (32)  NULL,
    [KHQC]        NVARCHAR (256) NULL,
    [LXR]         NVARCHAR (32)  NULL,
    [DH]          NVARCHAR (32)  NULL,
    [KHDZ]        NVARCHAR (128) NULL,
    [DWLX]        INT            NULL,
    [VALID_FLAG]  BIT            NULL,
    [CREATE_USER] NVARCHAR (32)  NULL,
    [CREATE_DATE] DATETIME       NULL,
    [MODIFY_USER] NVARCHAR (32)  NULL,
    [MODIFY_DATE] DATETIME       NULL,
    CONSTRAINT [PK_TM_BAS_DB1] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位类型', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_DB1', @level2type = N'COLUMN', @level2name = N'DWLX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地址', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_DB1', @level2type = N'COLUMN', @level2name = N'KHDZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电话', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_DB1', @level2type = N'COLUMN', @level2name = N'DH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_DB1', @level2type = N'COLUMN', @level2name = N'LXR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位全称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_DB1', @level2type = N'COLUMN', @level2name = N'KHQC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_DB1', @level2type = N'COLUMN', @level2name = N'F8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_DB1', @level2type = N'COLUMN', @level2name = N'F9';

