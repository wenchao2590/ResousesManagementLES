CREATE TABLE [LES].[TI_SPM_UNPAY_IN] (
    [SEQ_ID]         BIGINT          IDENTITY (1, 1) NOT NULL,
    [MJAHR]          NVARCHAR (4)    NULL,
    [ZEILE]          NVARCHAR (4)    NULL,
    [BUDAT1]         DATETIME        NULL,
    [DEAL_FLAG]      INT             NULL,
    [INVOICE_NO]     NVARCHAR (50)   NULL,
    [INVOICE_AMOUNT] NUMERIC (18, 3) NULL,
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
    [ZYSQJC]         NVARCHAR (20)   NULL,
    [BSTRF]          NUMERIC (18, 3) NULL,
    [MAKTX_ZH]       NVARCHAR (40)   NULL,
    [AMOUNT]         DECIMAL (18, 3) NULL,
    [PROCESS_FLAG]   INT             NULL,
    [PROCESS_TIME]   DATETIME        NULL,
    [COMMENTS]       NVARCHAR (200)  NULL,
    [UPDATE_DATE]    DATETIME        NULL,
    [UPDATE_USER]    NVARCHAR (50)   NULL,
    [CREATE_DATE]    DATETIME        NOT NULL,
    [CREATE_USER]    NVARCHAR (50)   NOT NULL,
    [EMLIF]          NVARCHAR (10)   NULL,
    [MWSKZ]          NVARCHAR (2)    NULL,
    [WAERS]          NVARCHAR (5)    NULL,
    [SCHPR]          NVARCHAR (1)    NULL,
    [EBELN]          NVARCHAR (10)   NULL,
    [EBELP]          NVARCHAR (5)    NULL,
    [ASN]            NVARCHAR (20)   NULL,
    CONSTRAINT [PK_TI_SPM_UNPAY_IN] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TI_SPM_UNPAY_IN_POSNR]
    ON [LES].[TI_SPM_UNPAY_IN]([POSNR] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TI_SPM_UNPAY_IN_ZEILE]
    ON [LES].[TI_SPM_UNPAY_IN]([ZEILE] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TI_SPM_UNPAY_IN_MJAHR]
    ON [LES].[TI_SPM_UNPAY_IN]([MJAHR] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '开票金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'MAKTX_ZH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收容数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'BSTRF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装器具编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'ZYSQJC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'MBLNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单类型/采购订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'LFART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'LFUHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过账日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'BUDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'LGMNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'POSNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单/采购订单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'VBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'INVOICE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'INVOICE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'BUDAT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'ZEILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料凭证年度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'MJAHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_待开票信息发布', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_UNPAY_IN';

