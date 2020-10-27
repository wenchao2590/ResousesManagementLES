CREATE TABLE [LES].[TE_JIS_RECACULATE_ORDER] (
    [SIGNATURE] NVARCHAR (256) NOT NULL,
    CONSTRAINT [IDX_PK_RECACULATE_ORDER_SIGNATURE] PRIMARY KEY CLUSTERED ([SIGNATURE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？订单签名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RECACULATE_ORDER', @level2type = N'COLUMN', @level2name = N'SIGNATURE';

