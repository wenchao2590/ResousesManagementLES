CREATE TABLE [LES].[TL_SYS_OPERATION_LOG_DETAIL] (
    [ID]              INT              IDENTITY (1, 1) NOT NULL,
    [OPERATION_ID]    UNIQUEIDENTIFIER NOT NULL,
    [TYPE_NAME]       NVARCHAR (200)   NOT NULL,
    [ORIGINAL_VALUES] NVARCHAR (MAX)   NULL,
    [NEW_VALUES]      NVARCHAR (MAX)   NOT NULL,
    CONSTRAINT [IDX_PK_OPERATION_LOG_DETAIL_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '新值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG_DETAIL', @level2type = N'COLUMN', @level2name = N'NEW_VALUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '原始值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG_DETAIL', @level2type = N'COLUMN', @level2name = N'ORIGINAL_VALUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG_DETAIL', @level2type = N'COLUMN', @level2name = N'TYPE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG_DETAIL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_操作日志详细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG_DETAIL';

