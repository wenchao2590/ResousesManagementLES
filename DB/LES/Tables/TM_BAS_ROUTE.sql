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
    CONSTRAINT [PK_TM_BAS_ROUTE] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [ROUTE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'路径组合极限', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ROUTE_COMBINATION_LIMIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'路径类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ROUTE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'路径名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ROUTE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'路径代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ROUTE', @level2type = N'COLUMN', @level2name = N'PLANT';

