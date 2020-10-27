CREATE TABLE [LES].[TM_JIS_RACK_BOX_PARTS] (
    [PLANT]           NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]      NVARCHAR (5)   NULL,
    [WORKSHOP]        NVARCHAR (4)   NULL,
    [BOX_PARTS]       NVARCHAR (20)  NOT NULL,
    [BOX_PARTS_CNAME] VARCHAR (200)  NULL,
    [BOX_PARTS_TYPE]  NVARCHAR (10)  NULL,
    [SUPPLIER_NUM]    NVARCHAR (8)   NOT NULL,
    [RACK]            NVARCHAR (200) NULL,
    [CACULATE_TERM]   INT            NULL,
    [COMMENTS]        NVARCHAR (200) NULL,
    [CREATE_USER]     NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]     DATETIME       NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)  NULL,
    [UPDATE_DATE]     DATETIME       NULL,
    CONSTRAINT [IDX_PK_RACK_BOX_PARTS_PLANT_ASSEMBLY_LINE_BOX_PARTS] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BOX_PARTS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算周期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CACULATE_TERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件大类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_JIS 零件大类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK_BOX_PARTS';

