CREATE TABLE [LES].[TS_SYS_EDITION_LIST] (
    [CODE_ID]       INT            NOT NULL,
    [CODE_NAME]     NVARCHAR (50)  NOT NULL,
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NULL,
    [PLANT_ZONE]    NVARCHAR (5)   NULL,
    [WORKSHOP]      NVARCHAR (4)   NULL,
    [DESCRIPTION]   NVARCHAR (200) NULL,
    [EDESCRIPTION]  NVARCHAR (200) NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    [BUILD_DATE]    DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [DEPLOY_DATE]   DATETIME       NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [COMPILE_DATE]  DATETIME       NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [COMPILE_USER]  NVARCHAR (50)  NULL,
    [DEPLOY_USER]   NVARCHAR (50)  NULL,
    [BUILD_USER]    NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_EDITION_LIST_CODE_ID] PRIMARY KEY CLUSTERED ([CODE_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Build_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'BUILD_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deploy_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'DEPLOY_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Compile_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'COMPILE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Compile_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'COMPILE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Deploy_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'DEPLOY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Build_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'BUILD_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_英文描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'EDESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '版本代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'CODE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST', @level2type = N'COLUMN', @level2name = N'CODE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_版本管理主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EDITION_LIST';

