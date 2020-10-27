CREATE TABLE [LES].[TR_SYS_USER_CUSTOM_STYLE] (
    [SEQ_ID]        INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]    NVARCHAR (5)   NULL,
    [WORKSHOP]      NVARCHAR (4)   NULL,
    [USER_ID]       INT            NOT NULL,
    [MENU_NAME]     NVARCHAR (100) NULL,
    [CONTROL_NAME]  NVARCHAR (100) NULL,
    [CONTROL_TYPE]  NVARCHAR (50)  NULL,
    [用户常用值]         NVARCHAR (200) NULL,
    [用户常用值2]        NVARCHAR (200) NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    [DISPLAY_ORDER] INT            NULL,
    CONSTRAINT [IDX_PK_USER_CUSTOM_STYLE_SEQ_ID_USER_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC, [USER_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '显示顺序', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'DISPLAY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户常用值2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'用户常用值2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '控件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'CONTROL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '控件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'CONTROL_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模块名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'MENU_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'USER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_用户与JIS 定制表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_CUSTOM_STYLE';

