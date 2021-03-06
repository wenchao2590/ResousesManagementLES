﻿CREATE TABLE [LES].[TM_PCS_VECHICLE_MOVEMENT_TEMPLATE] (
    [TEMPLATE_IDENTITY]   INT           IDENTITY (1, 1) NOT NULL,
    [KNR]                 NVARCHAR (20) NULL,
    [REGION_IDENTITY]     INT           NULL,
    [REGION_NAME]         VARCHAR (20)  NULL,
    [FOOTPRINT]           INT           NULL,
    [ARRIVAL_TIME_STAMP]  DATETIME      NULL,
    [CONSUMED_TIME_STAMP] DATETIME      NULL,
    [PERMANENT_KNR_INDEX] INT           NULL,
    [PLANT]               NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE]       NVARCHAR (10) NOT NULL,
    [PLANT_ZONE]          NVARCHAR (5)  NULL,
    [WORKSHOP]            NVARCHAR (4)  NULL,
    CONSTRAINT [IDX_PK_VECHICLE_MOVEMENT_TEMPLATE_TEMPLATE_IDENTITY] PRIMARY KEY CLUSTERED ([TEMPLATE_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '固定区域索引', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'PERMANENT_KNR_INDEX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '消耗时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'CONSUMED_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扫描时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'ARRIVAL_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '节拍', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'FOOTPRINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扫描点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'REGION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扫描点标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'REGION_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模板标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE', @level2type = N'COLUMN', @level2name = N'TEMPLATE_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS 位移计算模板', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_VECHICLE_MOVEMENT_TEMPLATE';

