CREATE TABLE [LES].[TC_SYS_CODE_DETAIL] (
    [CODE_NAME]          NVARCHAR (50)  NOT NULL,
    [DETAIL_CODE]        NVARCHAR (50)  NOT NULL,
    [DETAIL_VALUE]       NVARCHAR (200) NOT NULL,
    [PARENT_DETAIL_CODE] NVARCHAR (50)  NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [DISPLAY_ORDER]      INT            NULL,
    [VALID_FLAG]         BIT            NULL,
    [CREATE_USER]        NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]        DATETIME       NOT NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    CONSTRAINT [PK_TC_SYS_CODE_DETAIL] PRIMARY KEY CLUSTERED ([CODE_NAME] ASC, [DETAIL_CODE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_启用标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '显示顺序', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'DISPLAY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PARENT_DETAIL_CODE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'PARENT_DETAIL_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '代码值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'DETAIL_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '详细代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'DETAIL_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '代码名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL', @level2type = N'COLUMN', @level2name = N'CODE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_代码详细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TC_SYS_CODE_DETAIL';

