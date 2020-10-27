CREATE TABLE [LES].[TT_TWD_TIMEZONE_COUNTER_TEMP] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [PLANT]         NVARCHAR (50) NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (50) NULL,
    [DCP_POINT]     NCHAR (50)    NULL,
    [QUANTITY]      INT           NULL,
    [VALIDITY_FLAG] BIT           NULL,
    [CREATE_DATE]   DATETIME      NULL,
    [UPDATE_DATE]   DATETIME      NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TIMEZONE_COUNTER_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TIMEZONE_COUNTER_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MES扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TIMEZONE_COUNTER_TEMP', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TIMEZONE_COUNTER_TEMP', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TIMEZONE_COUNTER_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '补货看板', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TIMEZONE_COUNTER_TEMP';

