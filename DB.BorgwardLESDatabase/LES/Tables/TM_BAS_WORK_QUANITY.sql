CREATE TABLE [LES].[TM_BAS_WORK_QUANITY] (
    [ID]              INT           IDENTITY (1, 1) NOT NULL,
    [PLANT]           NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10) NOT NULL,
    [DATE]            DATETIME      NOT NULL,
    [DAY_TYPE]        NVARCHAR (1)  NOT NULL,
    [SHIFT]           INT           NOT NULL,
    [BEGIN_TIME]      DATETIME      NOT NULL,
    [END_TIME]        DATETIME      NOT NULL,
    [ALLOW_OVERRIDE]  BIT           NOT NULL,
    [DAY_PRODUCTIONS] INT           NULL,
    [CAL_STATE]       INT           NULL,
    [CREATE_USER]     NVARCHAR (50) NOT NULL,
    [CREATE_DATE]     DATETIME      NOT NULL,
    [UPDATE_USER]     NVARCHAR (50) NULL,
    [UPDATE_DATE]     DATETIME      NULL,
    CONSTRAINT [PK_TM_BAS_WORK_QUANITY] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [AK_KEY_1_TM_BAS_W] UNIQUE NONCLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'CAL_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '当前产量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'DAY_PRODUCTIONS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '允许覆盖', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'ALLOW_OVERRIDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结束时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'END_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '开始时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'BEGIN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_班次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'SHIFT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工作时间类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'DAY_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_TWD生产产量维护', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORK_QUANITY';

