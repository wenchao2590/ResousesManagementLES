CREATE TABLE [LES].[TT_JIS_UNFIT_ASSEMBLY_ORDER] (
    [ORDER_NO]      NVARCHAR (20)  NOT NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [SIGNATURE]     NVARCHAR (256) NOT NULL,
    [KNR]           NVARCHAR (8)   NOT NULL,
    [VORSERIE]      INT            NULL,
    [STATUS_TYPE]   NVARCHAR (50)  NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NULL,
    [BATCH_NO]      NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_UNFIT_ASSEMBLY_ORDER_ORDER_NO] PRIMARY KEY CLUSTERED ([ORDER_NO] ASC, [CREATE_DATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)批号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_ORDER', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_ORDER', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_ORDER', @level2type = N'COLUMN', @level2name = N'STATUS_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)接口_VORSERIE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_ORDER', @level2type = N'COLUMN', @level2name = N'VORSERIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_ORDER', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单签名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_ORDER', @level2type = N'COLUMN', @level2name = N'SIGNATURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_ORDER', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_ORDER', @level2type = N'COLUMN', @level2name = N'ORDER_NO';

