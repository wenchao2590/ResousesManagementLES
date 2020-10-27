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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'SAP_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'FIS流水线编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'FIS_LINE_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '时间调度类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'WORK_SCHEDULE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '英文协调员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'EMANAGER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '中文协调员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'CMANAGER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否参照工作日历', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'TIME_REFERENCE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'JPH', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'JPH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线节拍', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_PULSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线缩写', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ASSEMBLY_LINE';

