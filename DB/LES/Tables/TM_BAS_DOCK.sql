CREATE TABLE [LES].[TM_BAS_DOCK] (
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NOT NULL,
    [DOCK]          NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]    NVARCHAR (5)   NULL,
    [WORKSHOP]      NVARCHAR (4)   NULL,
    [DOCK_NAME]     NVARCHAR (100) NOT NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [IS_URGENT]     INT            NULL,
    [AREA_ID]       INT            NULL,
    CONSTRAINT [PK_TM_BAS_DOCK] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [DOCK] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Area ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'AREA_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'	是否紧急', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'IS_URGENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_DOCK名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'DOCK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_交货DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DOCK';

