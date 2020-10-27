CREATE TABLE [LES].[TT_TWD_RUNSHEET_PACKAGE_DETAIL] (
    [RUNSHEET_DETAIL_ID]           INT            IDENTITY (1, 1) NOT NULL,
    [TWD_RUNSHEET_SN]              INT            NOT NULL,
    [RECKONING_SN]                 INT            NOT NULL,
    [PLANT]                        NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)  NOT NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)  NOT NULL,
    [INBOUND_PACKAGE_MODEL]        NVARCHAR (30)  NULL,
    [RDC_DLOC]                     NVARCHAR (50)  NULL,
    [INBOUND_PACKAGE]              INT            NOT NULL,
    [MEASURING_UNIT_NO]            VARCHAR (8)    NULL,
    [PACK_COUNT]                   INT            NOT NULL,
    [REQUIRED_INBOUND_PACKAGE]     INT            NOT NULL,
    [REQUIRED_INBOUND_PACKAGE_QTY] INT            NOT NULL,
    [ACTUAL_INBOUND_PACKAGE]       INT            NULL,
    [ACTUAL_INBOUND_PACKAGE_QTY]   INT            NULL,
    [BARCODE_DATA]                 NVARCHAR (50)  NULL,
    [COMMENTS]                     NVARCHAR (200) NULL,
    [TWD_RUNSHEET_NO]              VARCHAR (22)   NOT NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_PACKAGE_DETAIL_RUNSHEET_DETAIL_ID] PRIMARY KEY CLUSTERED ([RUNSHEET_DETAIL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算单序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'RECKONING_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL', @level2type = N'COLUMN', @level2name = N'RUNSHEET_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_TWD 拉动单工位器具明细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RUNSHEET_PACKAGE_DETAIL';

