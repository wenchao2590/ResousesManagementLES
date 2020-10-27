CREATE TABLE [LES].[TM_BAS_WAREHOUSE_LOCATION] (
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [WM_NO]                 NVARCHAR (10)  NOT NULL,
    [ZONE_NO]               NVARCHAR (20)  NOT NULL,
    [DLOC]                  NVARCHAR (30)  NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [STORAGE_LOCATION_NAME] NVARCHAR (100) NULL,
    [LOCATION_TYPE]         INT            NULL,
    [SEQUENCE_NO]           INT            NULL,
    [COUNT_PARTITION]       NVARCHAR (100) NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [LANE_NO]               INT            NULL,
    [SHELVES_NO]            INT            NULL,
    [LAYER_NO]              INT            NULL,
    [GRID_NO]               INT            NULL,
    [PART_CLASSIFY_AREA_NO] NVARCHAR (128) NULL,
    CONSTRAINT [IDX_PK_WAREHOUSE_LOCATION_PLANT_ASSEMBLY_LINE_STORAGE_LOCATION] PRIMARY KEY CLUSTERED ([PLANT] ASC, [WM_NO] ASC, [DLOC] ASC, [ZONE_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件分类区域编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'PART_CLASSIFY_AREA_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'格号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'GRID_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'层号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'LAYER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'架号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'SHELVES_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'巷号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'LANE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'盘点分区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'COUNT_PARTITION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'LOCATION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'STORAGE_LOCATION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WAREHOUSE_LOCATION', @level2type = N'COLUMN', @level2name = N'PLANT';

