CREATE TABLE [LES].[TM_SYS_OPERATION_LOG_DICTIONARY] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [TYPE_NAME]     NVARCHAR (200) NOT NULL,
    [PROPERTY_NAME] NVARCHAR (50)  NULL,
    [CHINESE_NAME]  NVARCHAR (50)  NOT NULL,
    CONSTRAINT [IDX_PK_OPERATION_LOG_DICTIONARY_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_OPERATION_LOG_DICTIONARY', @level2type = N'COLUMN', @level2name = N'CHINESE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '属性名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_OPERATION_LOG_DICTIONARY', @level2type = N'COLUMN', @level2name = N'PROPERTY_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_OPERATION_LOG_DICTIONARY', @level2type = N'COLUMN', @level2name = N'TYPE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_OPERATION_LOG_DICTIONARY', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_操作日志详细表中英文对照表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_OPERATION_LOG_DICTIONARY';

