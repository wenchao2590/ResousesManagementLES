CREATE TABLE [LES].[TM_SYS_APPLICATION_CONFIGURATION] (
    [APPLICATION]   NVARCHAR (50)  NOT NULL,
    [PLANT]         NVARCHAR (5)   NULL,
    [WORKSHOP]      NVARCHAR (4)   NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NULL,
    [PARAMETER1]    NVARCHAR (200) NULL,
    [PARAMETER2]    NVARCHAR (200) NULL,
    [PARAMETER3]    NVARCHAR (200) NULL,
    [PARAMETER4]    NVARCHAR (200) NULL,
    [PARAMETER5]    NVARCHAR (200) NULL,
    [PARAMETER6]    NVARCHAR (200) NULL,
    [PARAMETER7]    NVARCHAR (200) NULL,
    [PARAMETER8]    NVARCHAR (200) NULL,
    [PARAMETER9]    NVARCHAR (200) NULL,
    [PARAMETER10]   NVARCHAR (200) NULL,
    [DESCRIPTION]   NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    CONSTRAINT [IDX_PK_APPLICATION_CONFIGURATION_APPLICATION] PRIMARY KEY CLUSTERED ([APPLICATION] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数10', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数9', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数8', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数7', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数6', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数5', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数4', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数3', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PARAMETER1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '应用程序', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION', @level2type = N'COLUMN', @level2name = N'APPLICATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_供应商与流水线关联表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_APPLICATION_CONFIGURATION';

