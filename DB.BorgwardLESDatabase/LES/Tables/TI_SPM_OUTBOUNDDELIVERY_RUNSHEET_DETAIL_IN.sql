CREATE TABLE [LES].[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN] (
    [SUB_SEQ_ID]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [SEQ_ID]       BIGINT          NULL,
    [VBELN]        NVARCHAR (10)   NOT NULL,
    [POSNR]        NVARCHAR (6)    NULL,
    [MATNR]        NVARCHAR (18)   NULL,
    [LFIMG]        NUMERIC (13, 3) NULL,
    [WERKS]        NVARCHAR (4)    NULL,
    [LGORT]        NVARCHAR (4)    NULL,
    [EBELN]        NVARCHAR (10)   NULL,
    [EBELP]        NVARCHAR (6)    NULL,
    [MEINS]        NVARCHAR (3)    NULL,
    [ZYSQJC]       NVARCHAR (20)   NULL,
    [ZYSQJQ]       INT             NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    CONSTRAINT [PK_TI_SPM_OUTBOUNDDELIVERY_Detail] PRIMARY KEY CLUSTERED ([SUB_SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收容数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'ZYSQJQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装器具编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'ZYSQJC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '采购订单项目号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'EBELP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '采购订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'EBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'LFIMG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单项目号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'POSNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '销售交货单/销售退货单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'VBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '主表流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '子表流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN', @level2type = N'COLUMN', @level2name = N'SUB_SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_销售出库明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_DETAIL_IN';

