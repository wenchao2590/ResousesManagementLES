CREATE TABLE [LES].[TR_PCS_SCHEDULE_BOX_PART] (
    [SCHEDULEID]    INT           NOT NULL,
    [PLANT_ZONE]    NVARCHAR (5)  NULL,
    [WORKSHOP]      NVARCHAR (4)  NULL,
    [ASSEMBLY_LINE] NVARCHAR (10) NOT NULL,
    [PLANT]         NVARCHAR (5)  NOT NULL,
    [BOX_PARTS]     NVARCHAR (10) NOT NULL,
    CONSTRAINT [PK_TR_PCS_SCHEDULE_BOX_PART] PRIMARY KEY CLUSTERED ([SCHEDULEID] ASC, [ASSEMBLY_LINE] ASC, [PLANT] ASC, [BOX_PARTS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'PCS零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_SCHEDULE_BOX_PART', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_SCHEDULE_BOX_PART', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_SCHEDULE_BOX_PART', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_SCHEDULE_BOX_PART', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_SCHEDULE_BOX_PART', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '关联序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_SCHEDULE_BOX_PART', @level2type = N'COLUMN', @level2name = N'SCHEDULEID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS 零件类与窗口时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_PCS_SCHEDULE_BOX_PART';

