CREATE TABLE [GJS].[TM_BAS_CLK] (
    [ID]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [DM]          NVARCHAR (32)   NULL,
    [MC]          NVARCHAR (128)  NULL,
    [XH]          NVARCHAR (64)   NULL,
    [CDDM]        NVARCHAR (8)    NULL,
    [CD]          NVARCHAR (32)   NULL,
    [JSDW]        NVARCHAR (16)   NULL,
    [CBDJ]        DECIMAL (18, 4) NULL,
    [XSDJ]        DECIMAL (18, 4) NULL,
    [MIN_QTY]     DECIMAL (18, 4) NULL,
    [MAX_QTY]     DECIMAL (18, 4) NULL,
    [HWCW]        NVARCHAR (16)   NULL,
    [GYSDM]       NVARCHAR (16)   NULL,
    [GYSMC]       NVARCHAR (32)   NULL,
    [BZ1]         NVARCHAR (256)  NULL,
    [BZ2]         NVARCHAR (256)  NULL,
    [BZ3]         NVARCHAR (256)  NULL,
    [TM]          NVARCHAR (64)   NULL,
    [HWC]         DECIMAL (18, 4) NULL,
    [HWK]         DECIMAL (18, 4) NULL,
    [HWG]         DECIMAL (18, 4) NULL,
    [DWMZ]        DECIMAL (18, 4) NULL,
    [DWTJ]        DECIMAL (18, 4) NULL,
    [VALID_FLAG]  BIT             NULL,
    [CREATE_USER] NVARCHAR (32)   NULL,
    [CREATE_DATE] DATETIME        NULL,
    [MODIFY_USER] NVARCHAR (32)   NULL,
    [MODIFY_DATE] DATETIME        NULL,
    CONSTRAINT [PK_TM_BAS_CLK] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位体积', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'DWTJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位毛重', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'DWMZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'高', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'HWG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'宽', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'HWK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'长', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'HWC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'条码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'TM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'销售备注', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'BZ3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'采购备注', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'BZ2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'使用备注', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'BZ1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'GYSMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'GYSDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'HWCW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最大库存', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'MAX_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安全库存', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'MIN_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'销售单价', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'XSDJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'采购单价', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'CBDJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计量单位', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'JSDW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产地', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'CD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产地代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'CDDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规格型号', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'XH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'MC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料编号', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLK', @level2type = N'COLUMN', @level2name = N'DM';

