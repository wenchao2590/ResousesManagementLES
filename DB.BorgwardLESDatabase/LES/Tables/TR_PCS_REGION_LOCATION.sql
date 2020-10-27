CREATE TABLE [LES].[TR_PCS_REGION_LOCATION] (
    [SEQ_IDENTITY]  INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NOT NULL,
    [REGION_NAME]   NVARCHAR (50)  NOT NULL,
    [LOCATION]      NVARCHAR (20)  NOT NULL,
    [FOOTPRINT]     INT            NULL,
    [FOOTPRINT_NO]  INT            NULL,
    [SEQUENCE_NO]   INT            NULL,
    [PICKUP_SEQ_NO] INT            NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    CONSTRAINT [IDX_PK_REGION_LOCATION_SEQ_IDENTITY] PRIMARY KEY CLUSTERED ([SEQ_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'FOOTPRINT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '节拍序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'FOOTPRINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DCP扫描点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'REGION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '关系关联表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION', @level2type = N'COLUMN', @level2name = N'SEQ_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS 扫描点与工位关联', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_REGION_LOCATION';

