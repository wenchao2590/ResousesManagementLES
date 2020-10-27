CREATE TABLE [LES].[TS_SYS_EDITION_LIST_DETAIL] (
    [CODE_ID]         INT             NOT NULL,
    [CODE_NAME]       NVARCHAR (50)   NOT NULL,
    [DETAIL_CODE]     NVARCHAR (50)   NOT NULL,
    [DETAIL_VALUE]    NVARCHAR (200)  NOT NULL,
    [TFS_LABLE]       NVARCHAR (200)  NULL,
    [COMMENTS]        NVARCHAR (200)  NULL,
    [DEPLOY_LOCATION] NVARCHAR (500)  NULL,
    [DEPLOY_CONTENT]  NVARCHAR (2000) NULL,
    [CHANGE_CONTENT]  NVARCHAR (MAX)  NULL,
    [VALID_FLAG]      BIT             NULL,
    [CREATE_USER]     NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]     DATETIME        NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)   NULL,
    [UPDATE_DATE]     DATETIME        NULL,
    CONSTRAINT [IDX_PK_EDITION_LIST_DETAIL_DETAIL_CODE] PRIMARY KEY CLUSTERED ([CODE_ID] ASC, [DETAIL_CODE] ASC, [DETAIL_VALUE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_启用标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '更改内容', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'CHANGE_CONTENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '更改功能点说明', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'DEPLOY_CONTENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '部署位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'DEPLOY_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '对应TFS中的Lable', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'TFS_LABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '代码值--部署内容', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'DETAIL_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '详细代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'CODE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '版本序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL', @level2type = N'COLUMN', @level2name = N'CODE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_版本管理明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST_DETAIL';

