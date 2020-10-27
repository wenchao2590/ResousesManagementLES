CREATE TABLE [LES].[TS_SYS_EVENT] (
    [EVENT_ID]          INT            NOT NULL,
    [EVENT_NAME]        NVARCHAR (100) NOT NULL,
    [EVENT_TYPE]        INT            NOT NULL,
    [NOTIFY_PLANNER]    INT            NOT NULL,
    [NOTIFY_SUPPLIER]   INT            NOT NULL,
    [NOTIFY_HELPDESK]   INT            NOT NULL,
    [NOTIFY_MOM_SYSTEM] INT            NOT NULL,
    CONSTRAINT [IDX_PK_TS_SYS_EVENT_EVENT_ID] PRIMARY KEY CLUSTERED ([EVENT_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知监控系统', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EVENT', @level2type = N'COLUMN', @level2name = N'NOTIFY_MOM_SYSTEM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知HELPDESK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EVENT', @level2type = N'COLUMN', @level2name = N'NOTIFY_HELPDESK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EVENT', @level2type = N'COLUMN', @level2name = N'NOTIFY_SUPPLIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知计划人员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EVENT', @level2type = N'COLUMN', @level2name = N'NOTIFY_PLANNER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '事件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EVENT', @level2type = N'COLUMN', @level2name = N'EVENT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '事件ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EVENT', @level2type = N'COLUMN', @level2name = N'EVENT_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_系统事件表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EVENT';

