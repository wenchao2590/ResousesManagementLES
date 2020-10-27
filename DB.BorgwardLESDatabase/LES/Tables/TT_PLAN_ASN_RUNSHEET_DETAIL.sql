CREATE TABLE [LES].[TT_PLAN_ASN_RUNSHEET_DETAIL] (
    [RUNSHEET_DETAIL_ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PLAN_ASN_RUNSHEET_SN]         INT            NOT NULL,
    [PLANT]                        NVARCHAR (5)   NOT NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)  NOT NULL,
    [PART_NO]                      NVARCHAR (20)  NOT NULL,
    [PART_CNAME]                   NVARCHAR (300) NULL,
    [PART_ENAME]                   NVARCHAR (300) NULL,
    [DOCK]                         NVARCHAR (10)  NULL,
    [BOX_PARTS]                    NVARCHAR (10)  NULL,
    [PICKUP_SEQ_NO]                INT            NULL,
    [RDC_DLOC]                     VARCHAR (20)   NULL,
    [INHOUSE_PACKAGE]              INT            NULL,
    [INHOUSE_PACKAGE_MODEL]        NVARCHAR (30)  NULL,
    [MEASURING_UNIT_NO]            VARCHAR (8)    NULL,
    [OUTER_PACK_COUNT]             INT            NULL,
    [INNER_PACK_COUNT]             INT            NULL,
    [OUTER_BOX_COUNT]              INT            NULL,
    [INNER_BOX_COUNT]              INT            NULL,
    [REQUIRED_INHOUSE_PACKAGE]     INT            NULL,
    [REQUIRED_INHOUSE_PACKAGE_QTY] INT            NULL,
    [ACTUAL_INHOUSE_PACKAGE]       INT            NULL,
    [ACTUAL_INHOUSE_PACKAGE_QTY]   INT            NULL,
    [BARCODE_DATA]                 NVARCHAR (50)  NULL,
    [RECIEVE_DOCK]                 NVARCHAR (50)  NULL,
    [RECKONING_NO]                 NVARCHAR (30)  NULL,
    [DELIVERY_ORDER]               NVARCHAR (30)  NULL,
    [ORDER_NO]                     NVARCHAR (30)  NULL,
    [COMMENTS]                     NVARCHAR (200) NULL,
    CONSTRAINT [PK_TT_PLAN_ASN_RUNSHEET_DETAIL] PRIMARY KEY CLUSTERED ([RUNSHEET_DETAIL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)提货单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'DELIVERY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)结算单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'RECKONING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)收货点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'RECIEVE_DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INHOUSE_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INHOUSE_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)内料箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'INNER_BOX_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)外料箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'OUTER_BOX_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)内包零件数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'INNER_PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)外包零件数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'OUTER_PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件英文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PLAN_ASN_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PLAN_ASN_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'RUNSHEET_DETAIL_ID';

