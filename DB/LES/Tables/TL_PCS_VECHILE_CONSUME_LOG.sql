CREATE TABLE [LES].[TL_PCS_VECHILE_CONSUME_LOG] (
    [CONSUME_LOG_IDENTITY] INT             IDENTITY (1, 1) NOT NULL,
    [KNR]                  NVARCHAR (20)   NULL,
    [REGION_NO]            NVARCHAR (50)   NULL,
    [FOOTPRINT]            INT             NULL,
    [LOCATION]             NVARCHAR (20)   NULL,
    [PART_NO]              NVARCHAR (20)   NULL,
    [CURRENT_PART_COUNT]   NUMERIC (18, 2) NULL,
    [DOSAGE]               NUMERIC (18, 2) NULL,
    [MISSING_TIME_STAMP]   DATETIME        NULL,
    [CONSUMED_TIME_STAMP]  DATETIME        NULL,
    [PLANT]                NVARCHAR (5)    NULL,
    [ASSEMBLY_LINE]        NVARCHAR (10)   NULL,
    [PLANT_ZONE]           NVARCHAR (5)    NULL,
    [WORKSHOP]             NVARCHAR (4)    NULL,
    [REPLACE_PART]         NVARCHAR (20)   NULL,
    CONSTRAINT [IDX_PK_VECHILE_CONSUME_LOG_CONSUME_LOG_IDENTITY] PRIMARY KEY CLUSTERED ([CONSUME_LOG_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'被替换的零件', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'REPLACE_PART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消耗时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'CONSUMED_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'MISSING_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'DOSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车当前计数器数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'CURRENT_PART_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'节拍', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'FOOTPRINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'REGION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计算标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'CONSUME_LOG_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_PCS 车辆消耗日志明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_PCS_VECHILE_CONSUME_LOG';

