CREATE TABLE [LES].[TS_SYS_PROCESS_SCHEDULE] (
    [SYSTEM_ID]           INT            NOT NULL,
    [SYSTEM_NAME]         NVARCHAR (100) NULL,
    [LAST_RUN_BEGIN_TIME] DATETIME       NULL,
    [LAST_RUN_END_TIME]   DATETIME       NULL,
    [LAST_RUN_STATUS]     INT            NULL,
    [RUN_INTERVAL]        INT            NOT NULL,
    [CHECK_INTERVAL]      INT            NOT NULL,
    [SYSTEM_PARAMETER1]   NVARCHAR (100) NULL,
    [SYSTEM_PARAMETER2]   NVARCHAR (100) NULL,
    [SYSTEM_PARAMETER3]   NVARCHAR (100) NULL,
    [SYSTEM_PARAMETER4]   NVARCHAR (100) NULL,
    [SYSTEM_PARAMETER5]   NVARCHAR (100) NULL,
    CONSTRAINT [IDX_PK_TS_SYS_PROCESS_SCHEDULE_SERVICE_ID] PRIMARY KEY NONCLUSTERED ([SYSTEM_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统参数4', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统参数3', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统参数2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统参数1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '检查间隔', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'CHECK_INTERVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行间隔', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'RUN_INTERVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上次运行状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'LAST_RUN_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上次运行结束时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'LAST_RUN_END_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上次运行开始时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'LAST_RUN_BEGIN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '服务名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_服务调度表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE';

