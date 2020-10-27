CREATE TABLE [LES].[TT_SPM_PAY_PLAN] (
    [PAY_SEQ_ID]                 BIGINT          IDENTITY (1, 1) NOT NULL,
    [SEQ_ID]                     BIGINT          NOT NULL,
    [PLAN_NO]                    NVARCHAR (50)   NULL,
    [PLANT]                      NVARCHAR (5)    NOT NULL,
    [SUPPLIER_NUM]               NVARCHAR (8)    NOT NULL,
    [INVOICE_NO]                 NVARCHAR (200)  NULL,
    [SUPPLIER_NAME]              NVARCHAR (100)  NOT NULL,
    [SUPPLIER_SNAME]             NVARCHAR (100)  NULL,
    [ACCOUNT_START_DATE]         DATETIME        NOT NULL,
    [MEASURING_UNIT_NO]          VARCHAR (8)     NULL,
    [ORDER_NO]                   NVARCHAR (30)   NULL,
    [ITEM_NO]                    NVARCHAR (50)   NULL,
    [IDOC]                       NVARCHAR (16)   NOT NULL,
    [PART_NO]                    NVARCHAR (20)   NOT NULL,
    [PART_CNAME]                 NVARCHAR (300)  NULL,
    [SUPPLIER_NUM2]              NVARCHAR (8)    NOT NULL,
    [ISSUE_DATE]                 DATETIME        NULL,
    [PART_INENTITY]              NVARCHAR (30)   NULL,
    [PART_INENTITY_YEAR]         NVARCHAR (20)   NULL,
    [PART_INENTITY_ITEMNO]       NVARCHAR (20)   NULL,
    [INVOICE_AMOUNT]             DECIMAL (18, 2) NULL,
    [INVOICE_DATE]               DATETIME        NULL,
    [RECIEVE_DOCK]               NVARCHAR (50)   NULL,
    [PAY_STATUS]                 INT             NULL,
    [COMMENTS]                   NVARCHAR (200)  NULL,
    [CREATE_USER]                NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]                DATETIME        NOT NULL,
    [UPDATE_USER]                NVARCHAR (50)   NULL,
    [UPDATE_DATE]                DATETIME        NULL,
    [TAX]                        DECIMAL (18, 2) NULL,
    [SAP_AMOUNT]                 DECIMAL (18, 2) NULL,
    [SETTLE_AMOUNT]              DECIMAL (18, 2) NULL,
    [OPERATION_ID]               NVARCHAR (50)   NULL,
    [INVOICE_TYPE]               NVARCHAR (20)   NULL,
    [ACTUAL_INHOUSE_PACKAGE_QTY] NUMERIC (18, 3) NULL,
    CONSTRAINT [IDX_PK_PAY_PLAN_PAY_SEQ_ID] PRIMARY KEY CLUSTERED ([PAY_SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PAY_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货点(存存地点）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'RECIEVE_DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'INVOICE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'INVOICE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PART_INENTITY_ITEMNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证年度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PART_INENTITY_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PART_INENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'ISSUE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_外协供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IDOC号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ITEM号（行项目号）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'ITEM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过账日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'ACCOUNT_START_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商简称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商名称2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'INVOICE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划行号(交货单号）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PLAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '开票通知流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '付款流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN', @level2type = N'COLUMN', @level2name = N'PAY_SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_付款计划表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAY_PLAN';

