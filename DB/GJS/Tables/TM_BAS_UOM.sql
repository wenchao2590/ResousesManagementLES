CREATE TABLE [GJS].[TM_BAS_UOM] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [CODE]        NVARCHAR (8)   NULL,
    [NAME]        NVARCHAR (32)  NULL,
    [DESCRIPTION] NVARCHAR (128) NULL,
    [VALID_FLAG]  BIT            NULL,
    [CREATE_USER] NVARCHAR (32)  NULL,
    [CREATE_DATE] DATETIME       NULL,
    [MODIFY_USER] NVARCHAR (32)  NULL,
    [MODIFY_DATE] DATETIME       NULL,
    CONSTRAINT [PK_TM_BAS_UOM] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_UOM', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_UOM', @level2type = N'COLUMN', @level2name = N'NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_UOM', @level2type = N'COLUMN', @level2name = N'CODE';

