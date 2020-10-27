CREATE TABLE [LES].[TT_SPM_INVOICE_EXEC] (
    [SEQ_ID]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [INVOICE_NO]      NVARCHAR (200)  NULL,
    [PLANT]           NVARCHAR (5)    NOT NULL,
    [SUPPLIER_NUM]    NVARCHAR (8)    NOT NULL,
    [INVOICE_AMOUNT]  DECIMAL (18, 2) NULL,
    [CURRENCY]        NVARCHAR (50)   NULL,
    [INVOICE_DATE]    DATETIME        NULL,
    [INVOICE_STATUS]  INT             NULL,
    [DOCUMENT_NUMBER] NVARCHAR (60)   NULL,
    [INVOICE_REASON]  NVARCHAR (200)  NULL,
    [DEAL_DATE]       DATETIME        NULL,
    [SERIAL_NO]       NVARCHAR (60)   NOT NULL,
    [COMMENTS]        NVARCHAR (200)  NULL,
    [CREATE_USER]     NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]     DATETIME        NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)   NULL,
    [UPDATE_DATE]     DATETIME        NULL,
    [TAX]             DECIMAL (18, 2) NULL,
    [SAP_AMOUNT]      DECIMAL (18, 2) NULL,
    [SETTLE_AMOUNT]   DECIMAL (18, 2) NULL,
    CONSTRAINT [IDX_PK_INVOICE_EXEC_IN_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC),
    CONSTRAINT [AK_KEY_1_TT_SPM_I] UNIQUE NONCLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序列号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'SERIAL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'DEAL_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '原因说明', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'INVOICE_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票金税号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'DOCUMENT_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'INVOICE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'INVOICE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '币种', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'CURRENCY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票金额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'INVOICE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'INVOICE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_发票执行表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_INVOICE_EXEC';

