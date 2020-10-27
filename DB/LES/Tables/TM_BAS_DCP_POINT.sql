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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扫描点序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'DCP_SEQUENCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'VEHICLE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扫描点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'DCP_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_DCP_POINT', @level2type = N'COLUMN', @level2name = N'PLANT';

