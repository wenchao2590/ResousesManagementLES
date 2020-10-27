CREATE TABLE [LES].[TM_SYS_MENU] (
    [MENU_ID]        INT            IDENTITY (1, 1) NOT NULL,
    [MENU_NAME]      NVARCHAR (50)  NULL,
    [PARENT_MENU_ID] INT            NULL,
    [ORDER_ID]       INT            NULL,
    [IMAGE_URL]      NVARCHAR (256) NULL,
    [LINK_URL]       NVARCHAR (256) NULL,
    [COMMENTS]       NVARCHAR (256) NULL,
    [CREATE_USER]    NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]    DATETIME       NOT NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    CONSTRAINT [IDX_PK_TM_SYS_MENU_MENU_ID] PRIMARY KEY CLUSTERED ([MENU_ID] ASC),
    CONSTRAINT [FK_TM_SYS_MENU_SELF_REF] FOREIGN KEY ([PARENT_MENU_ID]) REFERENCES [LES].[TM_SYS_MENU] ([MENU_ID])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '链接URL', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'LINK_URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '图片URL', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'IMAGE_URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'ORDER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '菜单名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'MENU_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '菜单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU', @level2type = N'COLUMN', @level2name = N'MENU_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_菜单表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_MENU';

