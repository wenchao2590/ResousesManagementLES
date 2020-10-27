CREATE TABLE [LES].[TI_SPM_DELIVERY_RUNSHEET_IN] (
    [SEQ_ID]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [LGMNG]        NUMERIC (18, 3) NULL,
    [MATNR]        NVARCHAR (18)   NULL,
    [POSNR]        NVARCHAR (18)   NULL,
    [VBELN]        NVARCHAR (12)   NULL,
    [MEINS]        NVARCHAR (8)    NULL,
    [LIFNR]        NVARCHAR (10)   NULL,
    [LFDAT]        NVARCHAR (10)   NULL,
    [LFUHR]        NVARCHAR (10)   NULL,
    [WERKS]        NVARCHAR (4)    NULL,
    [LGORT]        NVARCHAR (18)   NULL,
    [EMLIF]        NVARCHAR (10)   NULL,
    [GIDAT]        DATETIME        NULL,
    [ZDOCK]        NVARCHAR (10)   NULL,
    [DISPO]        NVARCHAR (20)   NULL,
    [STRAS]        NVARCHAR (100)  NULL,
    [LFART]        NVARCHAR (4)    NULL,
    [ZVERSION]     NVARCHAR (7)    NULL,
    [ZYSQJC]       NVARCHAR (20)   NULL,
    [BSTRF]        NUMERIC (18, 3) NULL,
    [MAKTX_ZH]     NVARCHAR (40)   NULL,
    [LESTYPE]      NVARCHAR (1)    NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [Z_SERIAL]     NVARCHAR (32)   NULL,
    [STRAS_FROM]   NVARCHAR (60)   NULL,
    [GROES]        NVARCHAR (32)   NULL,
    [NORMT]        NVARCHAR (18)   NULL,
    [BISMT]        NVARCHAR (18)   NULL,
    CONSTRAINT [PK_TI_SPM_DELIVERY_RUNSHEET_IN] PRIMARY KEY NONCLUSTERED ([SEQ_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [NONCLUSTERED_VBELN_Index, sysname,>]
    ON [LES].[TI_SPM_DELIVERY_RUNSHEET_IN]([VBELN] ASC);


GO
CREATE NONCLUSTERED INDEX [NONCLUSTERED_PROCESS_FLAG_Index, sysname,>]
    ON [LES].[TI_SPM_DELIVERY_RUNSHEET_IN]([PROCESS_FLAG] ASC)
    INCLUDE([SEQ_ID], [VBELN], [LIFNR], [ZDOCK]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'MAKTX_ZH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收容数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'BSTRF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装器具编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'ZYSQJC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '版本号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'ZVERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单类型/采购订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LFART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'STRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货口', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'ZDOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划盘点日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'GIDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '外协供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'EMLIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LFUHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LFDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交货单/采购订单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'VBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'POSNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'LGMNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_交货单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_RUNSHEET_IN';

