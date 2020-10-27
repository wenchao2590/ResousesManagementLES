CREATE TABLE [LES].[TM_WMM_ZONES] (
    [ZONE_NO]         NVARCHAR (20)  NOT NULL,
    [ZONE_NAME]       NVARCHAR (100) NULL,
    [PLANT]           NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)  NULL,
    [PLANT_ZONE]      NVARCHAR (5)   NULL,
    [WORKSHOP]        NVARCHAR (4)   NULL,
    [WM_NO]           NVARCHAR (10)  NULL,
    [STOCK_PLACE_NO]  NVARCHAR (20)  NULL,
    [IS_MANAGE]       INT            NULL,
    [IS_IM]           INT            NULL,
    [IS_MIX]          INT            NULL,
    [IM_LOCATION]     NVARCHAR (50)  NULL,
    [IS_STOCK_CHECK]  INT            NULL,
    [STRATEGY]        NVARCHAR (50)  NULL,
    [IS_NEGATIVE]     INT            NULL,
    [COMMENTS]        NVARCHAR (200) NULL,
    [CREATE_USER]     NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]     DATETIME       NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)  NULL,
    [UPDATE_DATE]     DATETIME       NULL,
    [REPACKAGE_ZONE]  NVARCHAR (50)  NULL,
    [IS_DYNAMIC_DLOC] INT            NULL,
    [IS_OUTPUT_SOLE]  INT            NULL,
    [OVERFLOW_DLOC]   NVARCHAR (32)  NULL,
    CONSTRAINT [PK_TM_WMM_ZONES] PRIMARY KEY CLUSTERED ([ZONE_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	溢库库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'OVERFLOW_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否上下架管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'IS_OUTPUT_SOLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	是否动态库位管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'IS_DYNAMIC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	翻包存储区编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	是否允许负库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'IS_NEGATIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	堆放策略', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'STRATEGY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	库容检查', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'IS_STOCK_CHECK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	混储类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'IS_MIX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	存储区类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'IS_MANAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '	库存地标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'STOCK_PLACE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存储区名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'ZONE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存储区编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存储区维护', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_ZONES';

