CREATE TABLE [LES].[TM_BAS_WORKSHOP] (
    [PLANT]               NVARCHAR (5)   NOT NULL,
    [WORKSHOP]            NVARCHAR (4)   NOT NULL,
    [WORKSHOP_NAME]       NVARCHAR (100) NOT NULL,
    [TIME_REFERENCE_TYPE] INT            NULL,
    [WORK_SCHEDULE_TYPE]  INT            NULL,
    [WORKSHOP_TYPE]       INT            NULL,
    [CREATE_USER]         NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]         DATETIME       NOT NULL,
    [UPDATE_USER]         NVARCHAR (50)  NULL,
    [UPDATE_DATE]         DATETIME       NULL,
    CONSTRAINT [IDX_PK_WORKSHOP_PLANT_WORKSHOP] PRIMARY KEY CLUSTERED ([PLANT] ASC, [WORKSHOP] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车间类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'WORKSHOP_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '时间调度类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'WORK_SCHEDULE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否参照工作日历', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'TIME_REFERENCE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'WORKSHOP_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP';

