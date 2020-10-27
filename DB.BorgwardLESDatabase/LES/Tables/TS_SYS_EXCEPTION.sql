CREATE TABLE [LES].[TS_SYS_EXCEPTION] (
    [ERROR_ID]          INT            IDENTITY (1, 1) NOT NULL,
    [TIME_STAMP]        DATETIME       NOT NULL,
    [APPLICATION]       NVARCHAR (50)  NULL,
    [ERROR_CODE]        NVARCHAR (300) NULL,
    [EXCEPTION_MESSAGE] NVARCHAR (MAX) NOT NULL,
    [CLASS]             NVARCHAR (MAX) NULL,
    [METHOD]            NVARCHAR (MAX) NULL,
    [STACK]             NVARCHAR (MAX) NULL,
    CONSTRAINT [IDX_PK_TS_SYS_EXCEPTION] PRIMARY KEY CLUSTERED ([ERROR_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '栈', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION', @level2type = N'COLUMN', @level2name = N'STACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '函数名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION', @level2type = N'COLUMN', @level2name = N'METHOD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION', @level2type = N'COLUMN', @level2name = N'CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '异常信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION', @level2type = N'COLUMN', @level2name = N'EXCEPTION_MESSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '错误号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION', @level2type = N'COLUMN', @level2name = N'ERROR_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '应用系统', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION', @level2type = N'COLUMN', @level2name = N'APPLICATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION', @level2type = N'COLUMN', @level2name = N'TIME_STAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '异常ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION', @level2type = N'COLUMN', @level2name = N'ERROR_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_异常信息表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_EXCEPTION';

