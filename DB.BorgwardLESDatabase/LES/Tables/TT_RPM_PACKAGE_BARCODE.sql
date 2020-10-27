CREATE TABLE [LES].[TT_RPM_PACKAGE_BARCODE] (
    [BARCODE_ID]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [PLANT]          NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]  NVARCHAR (10)  NULL,
    [PLANT_ZONE]     NVARCHAR (5)   NULL,
    [WORKSHOP]       NVARCHAR (4)   NULL,
    [WM_NO]          NVARCHAR (10)  NULL,
    [ZONE_NO]        NVARCHAR (20)  NOT NULL,
    [SUPPLIER_NUM]   NVARCHAR (12)  NOT NULL,
    [BARCODE_DATA]   NVARCHAR (50)  NULL,
    [INNER_LOCATION] NVARCHAR (50)  NULL,
    [PRINT_TIMES]    INT            NULL,
    [BARCODE_STATUS] INT            NULL,
    [PRINT_DATE]     DATETIME       NULL,
    [BARCODE_TYPE]   NVARCHAR (10)  NULL,
    [BATTH_NO]       NVARCHAR (100) NULL,
    [COMMENTS]       NVARCHAR (200) NULL,
    [CREATE_DATE]    DATETIME       NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [CREATE_USER]    NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_PACKAGE_BARCODE_BARCODE_ID] PRIMARY KEY CLUSTERED ([BARCODE_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '批次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'BATTH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'BARCODE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'PRINT_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'PRINT_TIMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '内库位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'INNER_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE', @level2type = N'COLUMN', @level2name = N'BARCODE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_包装托标签主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE';

