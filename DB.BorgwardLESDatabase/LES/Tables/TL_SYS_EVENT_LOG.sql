CREATE TABLE [LES].[TL_SYS_EVENT_LOG] (
    [SEQUENCE_ID]  INT            IDENTITY (1, 1) NOT NULL,
    [EVENT_TIME]   DATETIME       NOT NULL,
    [EVENT_ID]     INT            NULL,
    [EVENT_SOURCE] VARCHAR (100)  NULL,
    [EVENT_STATE]  INT            NOT NULL,
    [EVENT_TYPE]   INT            NOT NULL,
    [EVENT_LEVEL]  INT            CONSTRAINT [DF_TS_BAS_EventLog_event_level] DEFAULT ((2)) NULL,
    [EVENT_DETAIL] VARCHAR (4000) NULL,
    [PARAMETER1]   VARCHAR (100)  NULL,
    [PARAMETER2]   VARCHAR (100)  NULL,
    [PARAMETER3]   VARCHAR (100)  NULL,
    [PARAMETER4]   VARCHAR (100)  NULL,
    [PARAMETER5]   VARCHAR (100)  NULL,
    [PARAMETER6]   VARCHAR (100)  NULL,
    [PARAMETER7]   VARCHAR (100)  NULL,
    [PARAMETER8]   VARCHAR (100)  NULL,
    [PARAMETER9]   VARCHAR (100)  NULL,
    [PARAMETER10]  VARCHAR (100)  NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_SYS_EVENT_LOG] PRIMARY KEY CLUSTERED ([SEQUENCE_ID] ASC),
    CONSTRAINT [FK_TS_SYS_EVENT_EVENT_LOG] FOREIGN KEY ([EVENT_ID]) REFERENCES [LES].[TS_SYS_EVENT] ([EVENT_ID]) ON UPDATE CASCADE
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数10', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数9', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数8', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数7', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数6', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数5', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数4', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数3', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'PARAMETER1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '详细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'EVENT_DETAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '等级', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'EVENT_LEVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'EVENT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'EVENT_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'EVENT_SOURCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '来源', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG', @level2type = N'COLUMN', @level2name = N'SEQUENCE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_事件日志表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_EVENT_LOG';

