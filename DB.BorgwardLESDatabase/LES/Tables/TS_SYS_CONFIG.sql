CREATE TABLE [LES].[TS_SYS_CONFIG] (
    [PARAMETER_NAME]  NVARCHAR (100) NOT NULL,
    [PARAMETER_VALUE] NVARCHAR (255) NOT NULL,
    [DEFAULT_VALUE]   NVARCHAR (255) NOT NULL,
    [CHANGEABLE]      INT            NOT NULL,
    [COMMENTS]        NVARCHAR (200) NULL,
    CONSTRAINT [IDX_PK_SYS_CONFIG_PARAMETER_NAME] PRIMARY KEY CLUSTERED ([PARAMETER_NAME] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否可变', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'CHANGEABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '默认值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'DEFAULT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'PARAMETER_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '参数配置KEY', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG', @level2type = N'COLUMN', @level2name = N'PARAMETER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_供应商与流水线关联表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_CONFIG';

