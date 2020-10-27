CREATE TABLE [LES].[TT_WMS_VECHILE_CONSUME_DETAIL] (
    [CONSUME_LOG_IDENTITY] INT            IDENTITY (1, 1) NOT NULL,
    [KNR]                  NVARCHAR (20)  NULL,
    [REGION_NO]            INT            NULL,
    [FOOTPRINT]            INT            NULL,
    [LOCATION]             NVARCHAR (20)  NULL,
    [PART_NO]              NVARCHAR (20)  NULL,
    [CURRENT_PART_COUNT]   INT            NULL,
    [DOSAGE]               INT            NULL,
    [MISSING_TIME_STAMP]   DATETIME       NULL,
    [CONSUMED_TIME_STAMP]  DATETIME       NULL,
    [PLANT]                NVARCHAR (5)   NULL,
    [ASSEMBLY_LINE]        NVARCHAR (10)  NULL,
    [PLANT_ZONE]           NVARCHAR (5)   NULL,
    [WORKSHOP]             NVARCHAR (4)   NULL,
    [WM_NO]                NVARCHAR (10)  NOT NULL,
    [ZONE_NO]              NVARCHAR (20)  NOT NULL,
    [DLOC]                 NVARCHAR (20)  NULL,
    [REPLACE_PART]         NVARCHAR (20)  NULL,
    [VIN]                  NVARCHAR (20)  NULL,
    [COMMENTS]             NVARCHAR (200) NULL,
    [CREATE_USER]          NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]          DATETIME       NOT NULL,
    [UPDATE_USER]          NVARCHAR (50)  NULL,
    [UPDATE_DATE]          DATETIME       NULL,
    CONSTRAINT [IDX_PK_VECHILE_CONSUME_LOG_IDENTITY] PRIMARY KEY CLUSTERED ([CONSUME_LOG_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '被替换的零件', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'REPLACE_PART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '消耗时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'CONSUMED_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'MISSING_TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'DOSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '当前计数器数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'CURRENT_PART_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '节拍', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'FOOTPRINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'REGION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计算标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL', @level2type = N'COLUMN', @level2name = N'CONSUME_LOG_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_盘点差异主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_VECHILE_CONSUME_DETAIL';

