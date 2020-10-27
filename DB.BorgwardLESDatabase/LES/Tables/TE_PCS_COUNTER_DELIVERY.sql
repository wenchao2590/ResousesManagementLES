CREATE TABLE [LES].[TE_PCS_COUNTER_DELIVERY] (
    [INHOUSE_IDENTITY] INT           NOT NULL,
    [PLANT]            NVARCHAR (5)  NULL,
    [ASSEMBLY_LINE]    NVARCHAR (10) NULL,
    CONSTRAINT [IDX_PK_COUNTER_DELIVERY_INHOUSE_IDENTITY] PRIMARY KEY NONCLUSTERED ([INHOUSE_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_COUNTER_DELIVERY', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_COUNTER_DELIVERY', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_COUNTER_DELIVERY', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_PCS 计数器临时表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_COUNTER_DELIVERY';

