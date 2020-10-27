CREATE TABLE [LES].[TE_DELIVERY_RUNSHEET] (
    [SEQ_ID]    NVARCHAR (255) NULL,
    [WERKS]     NVARCHAR (4)   NULL,
    [LIFNR]     NVARCHAR (10)  NULL,
    [F4]        NVARCHAR (255) NULL,
    [VBELN]     NVARCHAR (12)  NULL,
    [MATNR]     NVARCHAR (18)  NULL,
    [MAKTX_ZH]  NVARCHAR (40)  NULL,
    [POSNR]     NVARCHAR (18)  NULL,
    [MEINS]     NVARCHAR (8)   NULL,
    [ZYSQJC]    NVARCHAR (20)  NULL,
    [BSTRF]     FLOAT (53)     NULL,
    [LGMNG]     FLOAT (53)     NULL,
    [LFDAT]     NVARCHAR (10)  NULL,
    [LFUHR]     NVARCHAR (8)   NULL,
    [EMLIF]     NVARCHAR (10)  NULL,
    [GIDAT]     DATETIME       NULL,
    [ZDOCK]     NVARCHAR (10)  NULL,
    [STRAS]     NVARCHAR (100) NULL,
    [LGORT]     NVARCHAR (18)  NULL,
    [LFART]     NVARCHAR (4)   NULL,
    [ZVERSION]  NVARCHAR (7)   NULL,
    [DEAL_FLAG] INT            NULL,
    [COMMENTS]  NVARCHAR (200) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？版本号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ZVERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？交货单类型/采购订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LFART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？交货地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'STRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？收货口', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ZDOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？计划盘点日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'GIDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？外协供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'EMLIF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？交货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LFUHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？交货日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LFDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LGMNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'BSTRF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ZYSQJC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'POSNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'MAKTX_ZH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？交货单/采购订单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'VBELN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'F4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_DELIVERY_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEQ_ID';

