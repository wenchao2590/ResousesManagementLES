CREATE TABLE [LES].[TE_PCS_MULTI_COUNTER_DELIVERY] (
    [INHOUSE_IDENTITY] INT           NOT NULL,
    [MULTI_IDENTITY]   INT           NOT NULL,
    [PLANT]            NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE]    NVARCHAR (10) NOT NULL,
    CONSTRAINT [IDX_PK_MULTI_COUNTER_DELIVERY_INHOUSE_IDENTITY] PRIMARY KEY NONCLUSTERED ([INHOUSE_IDENTITY] ASC, [MULTI_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_MULTI_COUNTER_DELIVERY', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_MULTI_COUNTER_DELIVERY', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'INHOUSE序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_MULTI_COUNTER_DELIVERY', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_临时表多工位计算', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_MULTI_COUNTER_DELIVERY';

