CREATE TABLE [GJS].[TT_WMS_CLJCK] (
    [ID]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [JCDH]        NVARCHAR (32)   NULL,
    [DM]          NVARCHAR (32)   NULL,
    [MC]          NVARCHAR (128)  NULL,
    [TM]          NVARCHAR (64)   NULL,
    [JCSL]        DECIMAL (18, 4) NULL,
    [CCSL]        DECIMAL (18, 4) NULL,
    [KCSL]        DECIMAL (18, 4) NULL,
    [HWCW]        NVARCHAR (16)   NULL,
    [JSMZ]        DECIMAL (18, 4) NULL,
    [HWC]         DECIMAL (18, 4) NULL,
    [HWK]         DECIMAL (18, 4) NULL,
    [HWG]         DECIMAL (18, 4) NULL,
    [JSTJ]        DECIMAL (18, 4) NULL,
    [DWMZ]        DECIMAL (18, 4) NULL,
    [DWTJ]        DECIMAL (18, 4) NULL,
    [SCRQ]        DATETIME        NULL,
    [YXRQ]        DATETIME        NULL,
    [JSDW]        NVARCHAR (16)   NULL,
    [BZDW]        NVARCHAR (16)   NULL,
    [BZSL]        DECIMAL (18, 4) NULL,
    [BZ]          NVARCHAR (128)  NULL,
    [XJS]         DECIMAL (18, 4) NULL,
    [DJ]          DECIMAL (18, 4) NULL,
    [ZJ]          DECIMAL (18, 4) NULL,
    [MS]          NVARCHAR (256)  NULL,
    [XH]          NVARCHAR (64)   NULL,
    [CDDM]        NVARCHAR (8)    NULL,
    [CD]          NVARCHAR (32)   NULL,
    [SXH]         INT             NULL,
    [VALID_FLAG]  BIT             NULL,
    [CREATE_USER] NVARCHAR (32)   NULL,
    [CREATE_DATE] DATETIME        NULL,
    [MODIFY_USER] NVARCHAR (32)   NULL,
    [MODIFY_DATE] DATETIME        NULL,
    CONSTRAINT [PK_TT_WMS_CLJCK] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序号', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'SXH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产地', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'CD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产地代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'CDDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'规格型号', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'XH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'MS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总价', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'ZJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单价', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'DJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单箱数量', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'XJS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'BZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装数量', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'BZSL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装单位', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'BZDW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计量单位', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'JSDW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'有效日期', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'YXRQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'生产日期', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'SCRQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位体积', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'DWTJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单件毛重', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'DWMZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'体积', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'JSTJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'高', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'HWG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'宽', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'HWK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'长', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'HWC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'毛重', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'JSMZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'HWCW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库存数量', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'KCSL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'出仓数量', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'CCSL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库数量', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'JCSL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'条码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'TM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'MC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料编号', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'DM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库单号', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCK', @level2type = N'COLUMN', @level2name = N'JCDH';

