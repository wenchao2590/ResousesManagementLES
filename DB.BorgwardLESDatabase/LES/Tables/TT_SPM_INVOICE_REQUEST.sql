CREATE TABLE [LES].[TT_SPM_INVOICE_REQUEST] (
    [SEQ_ID]                     BIGINT          IDENTITY (1, 1) NOT NULL,
    [PLAN_RUNSHEET_NO]           VARCHAR (30)    NOT NULL,
    [PLAN_NO]                    NVARCHAR (50)   NULL,
    [ASN_NO]                     NVARCHAR (50)   NULL,
    [PLANT]                      NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]              NVARCHAR (10)   NULL,
    [WORKSHOP]                   NVARCHAR (4)    NULL,
    [PLANT_ZONE]                 NVARCHAR (5)    NULL,
    [SUPPLIER_NUM]               NVARCHAR (8)    NOT NULL,
    [INVOICE_NO]                 NVARCHAR (500)  NULL,
    [SUPPLIER_NAME]              NVARCHAR (100)  NOT NULL,
    [SUPPLIER_SNAME]             NVARCHAR (100)  NULL,
    [ACCOUNT_START_DATE]         DATETIME        NOT NULL,
    [MEASURING_UNIT_NO]          NVARCHAR (8)    NULL,
    [ACTUAL_INHOUSE_PACKAGE]     INT             NULL,
    [ACTUAL_INHOUSE_PACKAGE_QTY] NUMERIC (18, 3) NULL,
    [ORDER_NO]                   NVARCHAR (30)   NULL,
    [ITEM_NO]                    NVARCHAR (50)   NULL,
    [IDOC]                       NVARCHAR (16)   NOT NULL,
    [PART_NO]                    NVARCHAR (20)   NOT NULL,
    [PART_CNAME]                 NVARCHAR (300)  NULL,
    [PART_ENAME]                 NVARCHAR (300)  NULL,
    [SUPPLIER_NUM2]              NVARCHAR (8)    NOT NULL,
    [SETTLE_AMOUNT]              DECIMAL (18, 2) NULL,
    [TAX]                        DECIMAL (18, 2) NULL,
    [PRICE_TAX_TOTAL]            DECIMAL (18, 2) NULL,
    [DIFFER]                     DECIMAL (18, 2) NULL,
    [SETTLE_PRICE_VALIDDATE]     NVARCHAR (50)   NULL,
    [CHECK_RESULT]               INT             NULL,
    [READ_USER]                  NVARCHAR (50)   NULL,
    [READ_DATE]                  DATETIME        NULL,
    [CHECK_USER]                 NVARCHAR (50)   NULL,
    [CHECK_DATE]                 DATETIME        NULL,
    [ISSUE_DATE]                 DATETIME        NULL,
    [PART_INENTITY]              NVARCHAR (30)   NULL,
    [PART_INENTITY_YEAR]         NVARCHAR (20)   NULL,
    [PART_INENTITY_ITEMNO]       NVARCHAR (20)   NULL,
    [RECIEVE_DOCK]               NVARCHAR (50)   NULL,
    [INVOICE_AMOUNT]             DECIMAL (18, 2) NULL,
    [CURRENCY]                   NVARCHAR (50)   NULL,
    [INVOICE_DATE]               DATETIME        NULL,
    [COMMENTS]                   NVARCHAR (200)  NULL,
    [CREATE_USER]                NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]                DATETIME        NOT NULL,
    [UPDATE_USER]                NVARCHAR (50)   NULL,
    [UPDATE_DATE]                DATETIME        NULL,
    [SHEET_TYPE]                 NVARCHAR (4)    NULL,
    [SAP_AMOUNT]                 DECIMAL (18, 2) NULL,
    [WAERS]                      NVARCHAR (5)    NULL,
    [MWSKZ]                      NVARCHAR (5)    NULL,
    [SCHPR]                      NVARCHAR (1)    NULL,
    [Invoice_Type]               INT             CONSTRAINT [DF_TT_SPM_INVOICE_REQUEST_Invoice_Type] DEFAULT ((0)) NOT NULL,
    [PO_NO]                      NVARCHAR (10)   NULL,
    [PO_ITEMNO]                  NVARCHAR (5)    NULL,
    CONSTRAINT [PK_TT_SPM_INVOICE_REQUEST] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'INVOICE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '币种', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'CURRENCY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'INVOICE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货点(存存地点）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'RECIEVE_DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_INENTITY_ITEMNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证年度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_INENTITY_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_INENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'ISSUE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '审核日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'CHECK_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '审核人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'CHECK_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '阅读日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'READ_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '阅读人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'READ_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '审核结果', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'CHECK_RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算价生效期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'SETTLE_PRICE_VALIDDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '差异', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'DIFFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '价税合计', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PRICE_TAX_TOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '税金', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'TAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'SETTLE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_外协供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件英文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IDOC号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ITEM号（行项目号）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'ITEM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'ACTUAL_INHOUSE_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'ACTUAL_INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过账日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'ACCOUNT_START_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商简称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'INVOICE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ASN编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'ASN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PLAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'PLAN_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_开票通知单表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST';

