CREATE TABLE [LES].[TT_SPM_INVOICE_REQUEST_BAK201610111345] (
    [SEQ_ID]                     BIGINT          IDENTITY (1, 1) NOT NULL,
    [PLAN_RUNSHEET_NO]           VARCHAR (30)    NOT NULL,
    [PLAN_NO]                    NVARCHAR (50)   NULL,
    [ASN_NO]                     NVARCHAR (50)   NULL,
    [PLANT]                      NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]              NVARCHAR (10)   NULL,
    [WORKSHOP]                   NVARCHAR (4)    NULL,
    [PLANT_ZONE]                 NVARCHAR (5)    NULL,
    [SUPPLIER_NUM]               NVARCHAR (8)    NOT NULL,
    [INVOICE_NO]                 NVARCHAR (200)  NULL,
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
    [Invoice_Type]               INT             NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'Invoice_Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SCHPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'MWSKZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'WAERS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SAP_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单据类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发票日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'INVOICE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)币种', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'CURRENCY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发票金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'INVOICE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)收货点(存存地点）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'RECIEVE_DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物料凭证行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PART_INENTITY_ITEMNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物料凭证年度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PART_INENTITY_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物料凭证', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PART_INENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发布日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'ISSUE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)审核日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'CHECK_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)审核人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'CHECK_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)阅读日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'READ_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)阅读人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'READ_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)审核结果', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'CHECK_RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)结算价生效期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SETTLE_PRICE_VALIDDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)差异', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'DIFFER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)价税合计', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PRICE_TAX_TOTAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)税金', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'TAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)结算金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SETTLE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_外协供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件英文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IDOC号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ITEM号（行项目号）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'ITEM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'ACTUAL_INHOUSE_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'ACTUAL_INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)过账日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'ACCOUNT_START_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商简称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发票号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'INVOICE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ASN编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'ASN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)计划行号(交货单号）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PLAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'PLAN_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_REQUEST_BAK201610111345', @level2type = N'COLUMN', @level2name = N'SEQ_ID';

