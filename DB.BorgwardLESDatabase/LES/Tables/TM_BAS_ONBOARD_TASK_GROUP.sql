CREATE TABLE [LES].[TM_BAS_ONBOARD_TASK_GROUP] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [GROUP_CODE]  NVARCHAR (16)  NULL,
    [GROUP_NAME]  NVARCHAR (32)  NULL,
    [TASK_TYPE]   INT            NULL,
    [PLANT]       NVARCHAR (8)   NULL,
    [WM_NO]       NVARCHAR (16)  NULL,
    [ZONE_NO]     NVARCHAR (8)   NULL,
    [COMMENTS]    NVARCHAR (512) NULL,
    [STATUS]      INT            NULL,
    [VALID_FLAG]  BIT            NULL,
    [CREATE_USER] NVARCHAR (64)  NOT NULL,
    [CREATE_DATE] DATETIME       NOT NULL,
    [MODIFY_USER] NVARCHAR (64)  NULL,
    [MODIFY_DATE] DATETIME       NULL,
    [IS_GIVEUP]   INT            NULL,
    [ROUTE]       NVARCHAR (64)  NULL,
    [DELAY_TIME]  INT            NULL,
    CONSTRAINT [PK_TM_BAS_ONBOARD_TASK_GROUP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否允许放弃', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'IS_GIVEUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'TASK_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'GROUP_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_TASK_GROUP', @level2type = N'COLUMN', @level2name = N'GROUP_CODE';

