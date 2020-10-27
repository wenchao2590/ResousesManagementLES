CREATE TABLE [LES].[TT_PCS_MISSING_MOVEMENT] (
    [VEHICLE_MOVEMENT_IDENTITY] INT           NOT NULL,
    [KNR]                       NVARCHAR (50) NULL,
    [REGION_NO]                 INT           NULL,
    [FOOTPRINT]                 INT           NULL,
    [MISSING_TIME_STAMP]        DATETIME      NULL,
    [CONSUMED_TIME_STAMP]       DATETIME      NULL,
    [PLANT]                     NVARCHAR (5)  NULL,
    [ASSEMBLY_LINE]             NVARCHAR (10) NULL,
    [PLANT_ZONE]                NVARCHAR (5)  NULL,
    [WORKSHOP]                  NVARCHAR (4)  NULL,
    CONSTRAINT [IDX_PK_MISSING_KNR_MOVEMENT_VEHICLE_MOVEMENT_IDENTITY] PRIMARY KEY CLUSTERED ([VEHICLE_MOVEMENT_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '消耗时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'CONSUMED_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'MISSING_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '节拍', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'FOOTPRINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'REGION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计算标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT', @level2type = N'COLUMN', @level2name = N'VEHICLE_MOVEMENT_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_PCS 车辆无订单计算表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_MISSING_MOVEMENT';

