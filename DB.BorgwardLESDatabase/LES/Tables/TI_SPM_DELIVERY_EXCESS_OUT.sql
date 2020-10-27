CREATE TABLE [LES].[TI_SPM_DELIVERY_EXCESS_OUT] (
    [ID]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [Z_SERIAL]     NVARCHAR (32)   NULL,
    [WERKS]        NVARCHAR (4)    NULL,
    [LGORT]        NVARCHAR (4)    NULL,
    [MATNR]        NVARCHAR (18)   NULL,
    [MAKTX_ZH]     NVARCHAR (40)   NULL,
    [MEINS]        NVARCHAR (3)    NULL,
    [LIFNR]        NVARCHAR (10)   NULL,
    [LFDAT]        DATETIME        NULL,
    [LFUHR]        DATETIME        NULL,
    [MENGE]        DECIMAL (13, 3) NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [CREATE_USER]  NVARCHAR (50)   NULL,
    [CREATE_DATE]  DATETIME        NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    CONSTRAINT [PK_TI_SPM_DELIVERY_EXCESS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'MENGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'LFUHR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交货日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'LFDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'MAKTX_ZH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SPM_DELIVERY_EXCESS_OUT', @level2type = N'COLUMN', @level2name = N'Z_SERIAL';

