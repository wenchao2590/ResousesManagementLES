CREATE TABLE [LES].[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT] (
    [SEQ_ID]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [VBELN]        NVARCHAR (10)   NOT NULL,
    [POSNR]        NVARCHAR (6)    NULL,
    [MATNR]        NVARCHAR (18)   NULL,
    [WERKS]        NVARCHAR (4)    NULL,
    [LGORT]        NVARCHAR (4)    NULL,
    [LFIMG]        NUMERIC (18, 3) NULL,
    [MEINS]        NVARCHAR (3)    NULL,
    [LFART]        NVARCHAR (4)    NULL,
    [WADAT_IST]    NVARCHAR (8)    NULL,
    [BUDAT]        NVARCHAR (8)    NULL,
    [OPTIM]        NVARCHAR (6)    NULL,
    [OPRTR]        NVARCHAR (50)   NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货操作人员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'OPRTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际收货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'OPTIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际收货日期，作为SAP过账日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'BUDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过账日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'WADAT_IST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '销售交货单类型/销售退货单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'LFART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'LFIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'POSNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单/采购订单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'VBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_销售交货/退货过账接口', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_OUT';

