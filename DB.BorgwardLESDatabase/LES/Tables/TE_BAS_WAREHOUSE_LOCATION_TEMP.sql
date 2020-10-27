CREATE TABLE [LES].[TE_BAS_WAREHOUSE_LOCATION_TEMP] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [WM_NO]                 NVARCHAR (10)  NOT NULL,
    [ZONE_NO]               NVARCHAR (20)  NOT NULL,
    [DLOC]                  NVARCHAR (30)  NOT NULL,
    [STORAGE_LOCATION_NAME] NVARCHAR (100) NULL,
    [LOCATION_TYPE]         INT            NULL,
    [SEQUENCE_NO]           INT            NULL,
    [COUNT_PARTITION]       NVARCHAR (100) NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [ERROR_MSG]             NVARCHAR (200) NULL,
    [VALID_FLAG]            INT            NULL,
    [LANE_NO]               INT            NULL,
    [SHELVES_NO]            INT            NULL,
    [LAYER_NO]              INT            NULL,
    [GRID_NO]               INT            NULL,
    [PART_CLASSIFY_AREA_NO] NVARCHAR (128) NULL,
    CONSTRAINT [IDX_PK_WAREHOUSE_LOCATION_TEMP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'PART_CLASSIFY_AREA_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'GRID_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'LAYER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'SHELVES_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'LANE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？盘点分区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'COUNT_PARTITION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'LOCATION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？库位名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'STORAGE_LOCATION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_LOCATION_TEMP', @level2type = N'COLUMN', @level2name = N'ID';

