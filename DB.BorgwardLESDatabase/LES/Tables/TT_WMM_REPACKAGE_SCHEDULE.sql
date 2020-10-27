CREATE TABLE [LES].[TT_WMM_REPACKAGE_SCHEDULE] (
    [SCHEDULE_IDENTITY] INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]             NVARCHAR (5)   NULL,
    [ASSEMBLY_LINE]     NVARCHAR (10)  NULL,
    [WORKSHOP]          NVARCHAR (4)   NULL,
    [PLANT_ZONE]        NVARCHAR (5)   NULL,
    [ROUTES]            NVARCHAR (MAX) NULL,
    [WINDOWS_TIME]      NVARCHAR (5)   CONSTRAINT [DF.TT_WMM_REPACKAGE_SCHEDULE_WINDOWS_TIME] DEFAULT ('00:00') NOT NULL,
    [IS_ACTIVE]         INT            CONSTRAINT [DF_TT_WMM_REPACKAGE_SCHEDULE_IS_ACTIVE] DEFAULT ((1)) NULL,
    [LAST_RUN]          DATETIME       NULL,
    [COMMENTS]          NVARCHAR (200) NULL,
    [CREATE_USER]       NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]       DATETIME       CONSTRAINT [DF.TT_WMM_REPACKAGE_SCHEDULE_CREATE_DATE] DEFAULT (getdate()) NOT NULL,
    [UPDATE_USER]       NVARCHAR (50)  NULL,
    [UPDATE_DATE]       DATETIME       NULL,
    CONSTRAINT [PK.TT_WMM_REPACKAGE_SCHEDULE] PRIMARY KEY CLUSTERED ([SCHEDULE_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'LAST_RUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否活动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'IS_ACTIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)窗口时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'WINDOWS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'ROUTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)窗口时间序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SCHEDULE_IDENTITY';

