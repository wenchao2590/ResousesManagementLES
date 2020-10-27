CREATE TABLE [LES].[TS_SYS_LOG4NET_LOG] (
    [LOG_ID]      INT            IDENTITY (1, 1) NOT NULL,
    [METHOD]      NVARCHAR (MAX) NULL,
    [MESSAGE]     NVARCHAR (MAX) NOT NULL,
    [EXCEPTION]   NVARCHAR (MAX) NULL,
    [THREAD]      NVARCHAR (100) NOT NULL,
    [LOG_LEVEL]   NVARCHAR (100) NOT NULL,
    [CREATE_USER] NVARCHAR (50)  NOT NULL,
    [CREATE_DATE] DATETIME       NOT NULL,
    CONSTRAINT [IDX_PK_LOG4NET_LOG_LOG_ID] PRIMARY KEY CLUSTERED ([LOG_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'LOG_LEVEL', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG', @level2type = N'COLUMN', @level2name = N'LOG_LEVEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'THREAD', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG', @level2type = N'COLUMN', @level2name = N'THREAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'EXCEPTION', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG', @level2type = N'COLUMN', @level2name = N'EXCEPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MESSAGE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG', @level2type = N'COLUMN', @level2name = N'MESSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'METHOD', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG', @level2type = N'COLUMN', @level2name = N'METHOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'LOG_ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG', @level2type = N'COLUMN', @level2name = N'LOG_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_LOG4NET日志表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_LOG4NET_LOG';

