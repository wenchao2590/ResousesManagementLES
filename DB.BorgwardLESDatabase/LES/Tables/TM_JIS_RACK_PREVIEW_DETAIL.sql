CREATE TABLE [LES].[TM_JIS_RACK_PREVIEW_DETAIL] (
    [PLANT]             NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE]     NVARCHAR (10) NOT NULL,
    [PLANT_ZONE]        NVARCHAR (5)  NULL,
    [WORKSHOP]          NVARCHAR (4)  NULL,
    [SUPPLIER_NUM]      NVARCHAR (8)  NOT NULL,
    [RACK]              NVARCHAR (20) NOT NULL,
    [PREVIEW_POINT]     NVARCHAR (15) NOT NULL,
    [ARRANGEMENT_POINT] NVARCHAR (15) NULL,
    CONSTRAINT [IDX_PK_RACK_PREVIEW_DETAIL_PLANT_ASSEMBLY_LINE_SUPPLIER_NUM_RACK_PREVIEW_POINT] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [SUPPLIER_NUM] ASC, [RACK] ASC, [PREVIEW_POINT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)排序点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_PREVIEW_DETAIL', @level2type = N'COLUMN', @level2name = N'ARRANGEMENT_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)预览点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_PREVIEW_DETAIL', @level2type = N'COLUMN', @level2name = N'PREVIEW_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_PREVIEW_DETAIL', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_PREVIEW_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_PREVIEW_DETAIL', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_PREVIEW_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_PREVIEW_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_PREVIEW_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';

