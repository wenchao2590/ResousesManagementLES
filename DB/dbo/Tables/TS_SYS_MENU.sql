CREATE TABLE [dbo].[TS_SYS_MENU] (
    [ID]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]              UNIQUEIDENTIFIER NULL,
    [MENU_NAME]        NVARCHAR (64)    NULL,
    [PARENT_MENU_FID]  UNIQUEIDENTIFIER NULL,
    [DISPLAY_ORDER]    INT              NULL,
    [IMAGE_URL]        NVARCHAR (256)   NULL,
    [LINK_URL]         NVARCHAR (512)   NULL,
    [COMMENTS]         NVARCHAR (1024)  NULL,
    [MENU_TYPE]        INT              NULL,
    [NEED_AUTH]        BIT              NULL,
    [CREATE_USER]      NVARCHAR (32)    NULL,
    [CREATE_DATE]      DATETIME         NULL,
    [MODIFY_USER]      NVARCHAR (32)    NULL,
    [MODIFY_DATE]      DATETIME         NULL,
    [VALID_FLAG]       BIT              NULL,
    [MENU_NAME_CN]     NVARCHAR (64)    NULL,
    [FAVORITE_PIC]     NVARCHAR (512)   NULL,
    [EDIT_FORM_WIDTH]  INT              NULL,
    [EDIT_FORM_HEIGHT] INT              NULL,
    CONSTRAINT [PK_TM_SYS_MENU] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'编辑窗体高度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'EDIT_FORM_HEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'编辑窗体宽度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'EDIT_FORM_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收藏夹图片', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'FAVORITE_PIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'中文名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'MENU_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否授权', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'NEED_AUTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'菜单类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'MENU_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'链接地址', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'LINK_URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图标', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'IMAGE_URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'DISPLAY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'菜单名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_MENU', @level2type = N'COLUMN', @level2name = N'MENU_NAME';

