CREATE TABLE [LES].[TT_RPM_PACKAGE_BARCODE_DETAIL] (
    [BARCODE_DETAIL_ID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [BARCODE_ID]        BIGINT          NOT NULL,
    [PLANT]             NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]     NVARCHAR (10)   NULL,
    [SUPPLIER_NUM]      NVARCHAR (12)   NOT NULL,
    [WM_NO]             NVARCHAR (10)   NULL,
    [ZONE_NO]           NVARCHAR (20)   NOT NULL,
    [DLOC]              NVARCHAR (30)   NULL,
    [TRAN_NO]           NVARCHAR (10)   NOT NULL,
    [MEASURING_UNIT_NO] VARCHAR (8)     NULL,
    [PACKAGE]           NVARCHAR (100)  NULL,
    [NUM]               NUMERIC (18, 2) NOT NULL,
    [PACKAGE_NO]        NVARCHAR (30)   NOT NULL,
    [PACKAGE_CNAME]     NVARCHAR (100)  NULL,
    [PACK_COUNT]        INT             NOT NULL,
    [BARCODE_DATA]      NVARCHAR (50)   NULL,
    [COMMENTS]          NVARCHAR (200)  NULL,
    CONSTRAINT [IDX_PK_RPM_PACKAGE_BARCODE_DETAIL_ID] PRIMARY KEY CLUSTERED ([BARCODE_DETAIL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装_包装器具编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_包装托标签明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_BARCODE_DETAIL';

