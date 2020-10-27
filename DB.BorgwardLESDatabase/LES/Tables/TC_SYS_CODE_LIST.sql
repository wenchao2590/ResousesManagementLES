CREATE TABLE [LES].[TC_SYS_CODE_LIST] (
    [CODE_NAME]    NVARCHAR (50)  NOT NULL,
    [DESCRIPTION]  NVARCHAR (200) NULL,
    [EDESCRIPTION] NVARCHAR (200) NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [CREATE_DATE]  DATETIME       NOT NULL,
    [CREATE_USER]  NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TC_SYS_CODE_LIST] PRIMARY KEY CLUSTERED ([CODE_NAME] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_LIST', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_LIST', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_LIST', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_LIST', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_英文描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_LIST', @level2type = N'COLUMN', @level2name = N'EDESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_LIST', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '代码名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_LIST', @level2type = N'COLUMN', @level2name = N'CODE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_代码列表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_LIST';

