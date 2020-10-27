CREATE TABLE [LES].[TI_SPM_INVOICE_OUT] (
    [SEQ_ID]         BIGINT          IDENTITY (1, 1) NOT NULL,
    [VBELN]          NVARCHAR (10)   NULL,
    [POSNR]          NVARCHAR (18)   NULL,
    [MATNR]          NVARCHAR (18)   NULL,
    [LGMNG]          NUMERIC (18, 3) NULL,
    [MEINS]          NVARCHAR (8)    NULL,
    [LIFNR]          NVARCHAR (10)   NULL,
    [BUDAT]          NVARCHAR (10)   NULL,
    [LFUHR]          NVARCHAR (10)   NULL,
    [WERKS]          NVARCHAR (4)    NULL,
    [LGORT]          NVARCHAR (18)   NULL,
    [LFART]          NVARCHAR (4)    NULL,
    [MBLNR]          NVARCHAR (10)   NULL,
    [MJAHR]          NVARCHAR (4)    NULL,
    [ZEILE]          NVARCHAR (4)    NULL,
    [BUDAT1]         DATETIME        NULL,
    [PACKAGE_MODEL]  NVARCHAR (30)   NULL,
    [PACKAGE]        INT             NULL,
    [ZCOMDS]         NVARCHAR (30)   NULL,
    [DEAL_FLAG]      INT             NULL,
    [INVOICE_NO]     NVARCHAR (200)  NULL,
    [INVOICE_AMOUNT] NUMERIC (18, 3) NULL,
    [INVOICE_MONEY]  NVARCHAR (10)   NULL,
    [PROCESS_FLAG]   INT             NULL,
    [PROCESS_TIME]   DATETIME        NULL,
    [COMMENTS]       NVARCHAR (200)  NULL,
    [UPDATE_DATE]    DATETIME        NULL,
    [UPDATE_USER]    NVARCHAR (50)   NULL,
    [CREATE_DATE]    DATETIME        NOT NULL,
    [CREATE_USER]    NVARCHAR (50)   NOT NULL,
    [Z_SERIAL]       NVARCHAR (32)   NULL,
    [SAP_AMOUNT]     DECIMAL (18, 2) NULL,
    [SETTLE_AMOUNT]  DECIMAL (18, 2) NULL,
    [TAX]            DECIMAL (18, 2) NULL,
    [MWSKZ]          NVARCHAR (2)    NULL,
    [SCHPR]          NVARCHAR (1)    NULL,
    [ID]             NVARCHAR (19)   NULL,
    CONSTRAINT [PK_TI_SPM_INVOICE_OUT] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '货币', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'INVOICE_MONEY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'INVOICE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'INVOICE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'ZCOMDS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标准包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标准包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'BUDAT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'ZEILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证年度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'MJAHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'MBLNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单类型/采购订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'LFART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'LFUHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过账日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'BUDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'LGMNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'POSNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单/采购订单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'VBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_发票上传给SAP接收', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_INVOICE_OUT';

