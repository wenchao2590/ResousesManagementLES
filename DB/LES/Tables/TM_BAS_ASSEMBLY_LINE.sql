CREATE TABLE [LES].[TM_BAS_ASSEMBLY_LINE] (
    [PLANT]                  NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]          NVARCHAR (10)  NOT NULL,
    [ASSEMBLY_LINE_NAME]     NVARCHAR (100) NOT NULL,
    [ASSEMBLY_LINE_NICKNAME] NVARCHAR (100) NULL,
    [ADDRESS]                NVARCHAR (200) NULL,
    [ASSEMBLY_LINE_TYPE]     INT            NULL,
    [ASSEMBLY_LINE_PULSE]    NVARCHAR (10)  NULL,
    [JPH]                    INT            NULL,
    [TIME_REFERENCE_TYPE]    INT            NOT NULL,
    [CMANAGER]               NVARCHAR (50)  NULL,
    [EMANAGER]               NVARCHAR (50)  NULL,
    [WORK_SCHEDULE_TYPE]     INT            NOT NULL,
    [FIS_LINE_CODE]          NVARCHAR (10)  NULL,
    [PLANT_ZONE]             NVARCHAR (10)  NULL,
    [WORKSHOP]               NVARCHAR (4)   NULL,
    [SAP_ASSEMBLY_LINE]      NVARCHAR (20)  NULL,
    [COMMENTS]               NVARCHAR (200) NULL,
    [CREATE_USER]            NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]            DATETIME       NOT NULL,
    [UPDATE_USER]            NVARCHAR (50)  NULL,
    [UPDATE_DATE]            DATETIME       NULL,
    CONSTRAINT [IDX_PK_ASSEMBLY_LINE_PLANT_ASSEMBLY_LINE1] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SAP产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'SAP_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FIS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'FIS_LINE_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工作计划类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'WORK_SCHEDULE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工艺经理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'EMANAGER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'生产经理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'CMANAGER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'时间参考类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'TIME_REFERENCE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'JPH', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'JPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线节拍', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_PULSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线简称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'PLANT';

