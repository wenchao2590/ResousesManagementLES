CREATE TABLE [LES].[TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN] (
    [SEQ_ID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [VBELN]        NVARCHAR (10)  NOT NULL,
    [ERDAT]        NVARCHAR (8)   NULL,
    [ERZET]        NVARCHAR (6)   NULL,
    [LFART]        NVARCHAR (4)   NULL,
    [KUNNR]        NVARCHAR (10)  NULL,
    [LFDAT]        NVARCHAR (8)   NULL,
    [LFUHR]        NVARCHAR (6)   NULL,
    [KUNAME]       NVARCHAR (60)  NULL,
    [KUADRC]       NVARCHAR (60)  NULL,
    [LXRNAM]       NVARCHAR (40)  NULL,
    [LXRTEL]       NVARCHAR (30)  NULL,
    [LIFNR]        NVARCHAR (16)  NULL,
    [LINAME]       NVARCHAR (60)  NULL,
    [LIADRC]       NVARCHAR (60)  NULL,
    [TEXT1]        NVARCHAR (200) NULL,
    [DEAL_FLAG]    INT            NULL,
    [PROCESS_FLAG] INT            NULL,
    [PROCESS_TIME] DATETIME       NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [CREATE_USER]  NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]  DATETIME       NOT NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    CONSTRAINT [PK_TI_SPM_OUTBOUNDDELIVERY_RUN] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'TEXT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LIADRC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LINAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系人电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LXRTEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LXRNAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '送达方地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'KUADRC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '送达方名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'KUNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '期望交货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LFUHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '期望交货日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LFDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '送达方', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'KUNNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LFART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'ERZET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'ERDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '销售交货单/销售退货单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'VBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_销售出库主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_OUTBOUNDDELIVERY_RUNSHEET_IN';

