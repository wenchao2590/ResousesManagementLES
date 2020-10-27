CREATE TABLE [LES].[TT_PCS_VECHICLE_MOVEMENT] (
    [VEHICLE_MOVEMEN_TIDENTITY] INT           IDENTITY (1, 1) NOT NULL,
    [KNR]                       NVARCHAR (50) NULL,
    [REGION_IDENTITY]           INT           NULL,
    [REGION_NAME]               NVARCHAR (50) NULL,
    [FOOTPRINT]                 INT           NULL,
    [ARRIVAL_TIME_STAMP]        DATETIME      NULL,
    [CONSUMED_TIME_STAMP]       DATETIME      NULL,
    [PERMANENT_KNR_INDEX]       INT           NULL,
    [PLANT]                     NVARCHAR (5)  NULL,
    [ASSEMBLY_LINE]             NVARCHAR (10) NULL,
    [PLANT_ZONE]                NVARCHAR (5)  NULL,
    [WORKSHOP]                  NVARCHAR (4)  NULL,
    CONSTRAINT [IDX_PK_VECHICLE_MOVEMENT_VEHICLEMOVEMENTIDENTITY] PRIMARY KEY CLUSTERED ([VEHICLE_MOVEMEN_TIDENTITY] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_PCS_VECHICLE_MOVEMENT]
    ON [LES].[TT_PCS_VECHICLE_MOVEMENT]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [KNR] ASC, [REGION_IDENTITY] ASC, [REGION_NAME] ASC, [FOOTPRINT] ASC, [ARRIVAL_TIME_STAMP] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '固定区域索引', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'PERMANENT_KNR_INDEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '消耗时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'CONSUMED_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扫描时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'ARRIVAL_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '节拍', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'FOOTPRINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS扫描区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'REGION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS 扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'REGION_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '位移序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT', @level2type = N'COLUMN', @level2name = N'VEHICLE_MOVEMEN_TIDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_PCS 位移表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHICLE_MOVEMENT';

