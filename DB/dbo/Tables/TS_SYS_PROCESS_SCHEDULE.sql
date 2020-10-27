CREATE TABLE [dbo].[TS_SYS_PROCESS_SCHEDULE] (
    [ID]                  BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]                 UNIQUEIDENTIFIER NULL,
    [SYSTEM_NAME]         NVARCHAR (128)   NULL,
    [LAST_RUN_BEGIN_TIME] DATETIME         NULL,
    [LAST_RUN_END_TIME]   DATETIME         NULL,
    [LAST_RUN_STATUS]     INT              NULL,
    [RUN_INTERVAL]        INT              NULL,
    [CHECK_INTERVAL]      INT              NULL,
    [SYSTEM_PARAMETER1]   NVARCHAR (32)    NULL,
    [SYSTEM_PARAMETER2]   NVARCHAR (32)    NULL,
    [SYSTEM_PARAMETER3]   NVARCHAR (32)    NULL,
    [SYSTEM_PARAMETER4]   NVARCHAR (32)    NULL,
    [SYSTEM_PARAMETER5]   NVARCHAR (32)    NULL,
    [VALID_FLAG]          BIT              NULL,
    [CREATE_DATE]         DATETIME         NULL,
    [CREATE_USER]         NVARCHAR (32)    NULL,
    [MODIFY_DATE]         DATETIME         NULL,
    [MODIFY_USER]         NVARCHAR (32)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统参数5', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统参数4', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统参数3', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统参数2', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统参数1', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_PARAMETER1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检查时间间隔', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'CHECK_INTERVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运行时间间隔', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'RUN_INTERVAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运行状态', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'LAST_RUN_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运行结束时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'LAST_RUN_END_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运行开始时间', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'LAST_RUN_BEGIN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE', @level2type = N'COLUMN', @level2name = N'SYSTEM_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'服务运行', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_PROCESS_SCHEDULE';

