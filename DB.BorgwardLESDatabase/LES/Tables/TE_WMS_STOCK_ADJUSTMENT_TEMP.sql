CREATE TABLE [LES].[TE_WMS_STOCK_ADJUSTMENT_TEMP] (
    [STOCK_IDENTITY]  INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]           NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)   NULL,
    [SUPPLIER_NUM]    NVARCHAR (12)   NULL,
    [PART_NO]         NVARCHAR (20)   NOT NULL,
    [PART_CNAME]      NVARCHAR (100)  NULL,
    [PACKAGE_MODEL]   NVARCHAR (30)   NULL,
    [PACKAGE]         INT             NULL,
    [ZONE_NO]         NVARCHAR (20)   NOT NULL,
    [WM_NO]           NVARCHAR (10)   NOT NULL,
    [DLOC]            NVARCHAR (20)   NULL,
    [STOCKS]          NUMERIC (18, 2) NULL,
    [FROZEN_STOCKS]   NUMERIC (18, 2) NULL,
    [AVAILBLE_STOCKS] NUMERIC (18, 2) NULL,
    [FRAGMENT_NUM]    NUMERIC (18, 2) NULL,
    [STOCKS_NUM]      NUMERIC (18, 2) NULL,
    [BATCH_NO]        NVARCHAR (100)  NULL,
    [BARCODE_DATA]    NVARCHAR (50)   NULL,
    [ERROR_MSG]       NVARCHAR (1024) NULL,
    [VALID_FLAG]      BIT             NULL,
    [CREATE_USER]     NVARCHAR (50)   NULL,
    [CREATE_DATE]     DATETIME        NULL,
    [UPDATE_USER]     NVARCHAR (50)   NULL,
    [UPDATE_DATE]     DATETIME        NULL,
    CONSTRAINT [IDX_PK_PARTS_STOCK_ADJUSTMENT_TEMP_IDENTITY1] PRIMARY KEY NONCLUSTERED ([STOCK_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批次号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'STOCKS_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'散件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'FRAGMENT_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'可用库存量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'AVAILBLE_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'冻结件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'FROZEN_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总库存量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'生产线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMS_STOCK_ADJUSTMENT_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';

