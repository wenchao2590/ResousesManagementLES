CREATE TABLE [LES].[TM_BAS_ROUTE] (
    [PLANT]                   NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]           NVARCHAR (10)  NOT NULL,
    [ROUTE]                   NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]              NVARCHAR (5)   NULL,
    [WORKSHOP]                NVARCHAR (4)   NULL,
    [ROUTE_NAME]              VARCHAR (100)  NOT NULL,
    [ROUTE_TYPE]              INT            NOT NULL,
    [ROUTE_COMBINATION_LIMIT] INT            NULL,
    [ZONE]                    NVARCHAR (50)  NULL,
    [CREATE_USER]             NVARCHAR (50)  NULL,
    [CREATE_DATE]             DATETIME       NULL,
    [UPDATE_USER]             NVARCHAR (50)  NULL,
    [UPDATE_DATE]             DATETIME       NULL,
    [COMMENTS]                NVARCHAR (200) NULL,
    CONSTRAINT [PK_TM_BAS_ROUTE] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [ROUTE] ASC, [ROUTE_TYPE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_送货区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '送货限制组合数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ROUTE_COMBINATION_LIMIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '路径类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ROUTE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_送货路径名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ROUTE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE';

