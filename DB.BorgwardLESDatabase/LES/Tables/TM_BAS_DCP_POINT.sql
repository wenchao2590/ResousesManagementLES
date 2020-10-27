CREATE TABLE [LES].[TM_BAS_DCP_POINT] (
    [PLANT]          NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]  NVARCHAR (10)  NOT NULL,
    [DCP_POINT]      NVARCHAR (15)  NOT NULL,
    [PLANT_ZONE]     NVARCHAR (5)   NULL,
    [WORKSHOP]       NVARCHAR (4)   NULL,
    [DCP_NAME]       NVARCHAR (100) NOT NULL,
    [VEHICLE_STATUS] NVARCHAR (15)  NULL,
    [DCP_SEQUENCE]   NVARCHAR (4)   NULL,
    [COMMENTS]       NVARCHAR (200) NULL,
    [CREATE_USER]    NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]    DATETIME       NOT NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    CONSTRAINT [IDX_PK_DCP_POINT_PLANT_ASSEMBLY_LINE_DCP_POINT] PRIMARY KEY NONCLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [DCP_POINT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DCP排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'DCP_SEQUENCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_状态点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'VEHICLE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DCP名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'DCP_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DCP点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线DCP点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT';

