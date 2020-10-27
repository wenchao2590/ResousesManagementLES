CREATE TABLE [LES].[TI_SPM_DELIVERY_RUNSHEET_ASN_OUT] (
    [ID]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [ASN]          NVARCHAR (20)   NULL,
    [MATNR]        NVARCHAR (18)   NULL,
    [LGMNG]        DECIMAL (13, 3) NULL,
    [LIFNR]        NVARCHAR (10)   NULL,
    [LFDAT]        DATETIME        NULL,
    [WERKS]        NVARCHAR (4)    NULL,
    [LFART]        NVARCHAR (4)    NULL,
    [MAKTX_ZH]     NVARCHAR (40)   NULL,
    [VBELN]        NVARCHAR (32)   NULL,
    [POSNR]        INT             NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    CONSTRAINT [PK_TI_SPM_DELIVERY_RUNSHEET_ASN_SEND] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'POSNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'VBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'MAKTX_ZH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'LFART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交货日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'LFDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'LGMNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ASN单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_ASN_OUT', @level2type = N'COLUMN', @level2name = N'ASN';

