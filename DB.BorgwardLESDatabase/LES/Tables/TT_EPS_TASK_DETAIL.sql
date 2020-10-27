CREATE TABLE [LES].[TT_EPS_TASK_DETAIL] (
    [TASK_DETAIL_SN] INT            IDENTITY (1, 1) NOT NULL,
    [TASK_TIME]      DATETIME       NOT NULL,
    [TASK_STATUS]    INT            NOT NULL,
    [SESSION_SN]     INT            NULL,
    [TASK_SN]        INT            NULL,
    [COMMENTS]       NVARCHAR (200) NULL,
    [CREATE_USER]    NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]    DATETIME       NOT NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    CONSTRAINT [IDX_PK_TASK_DETAIL_TASK_DETAIL_SN] PRIMARY KEY CLUSTERED ([TASK_DETAIL_SN] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_EPS_TASK_DETAIL_1]
    ON [LES].[TT_EPS_TASK_DETAIL]([TASK_TIME] ASC, [TASK_STATUS] ASC, [SESSION_SN] ASC, [CREATE_DATE] ASC, [UPDATE_DATE] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_EPS_TASK_DETAIL]
    ON [LES].[TT_EPS_TASK_DETAIL]([TASK_SN] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '任务号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'TASK_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '会话序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'SESSION_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '任务状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'TASK_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '明细时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'TASK_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '任务明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL', @level2type = N'COLUMN', @level2name = N'TASK_DETAIL_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_EPS 任务明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK_DETAIL';

