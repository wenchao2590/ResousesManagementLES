CREATE TABLE [LES].[TS_JIS_PRINT_CONFIG] (
    [PRINT_ID]        INT           IDENTITY (1, 1) NOT NULL,
    [START_ROW]       INT           NOT NULL,
    [END_ROW]         INT           NOT NULL,
    [FONT_SIZE]       NVARCHAR (10) NOT NULL,
    [TOTAL_FONT_SIZE] NVARCHAR (10) NULL,
    [CREATE_USER]     NVARCHAR (50) NOT NULL,
    [CREATE_DATE]     DATETIME      NOT NULL,
    [UPDATE_USER]     NVARCHAR (50) NULL,
    [UPDATE_DATE]     DATETIME      NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '总字体大小', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'TOTAL_FONT_SIZE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '字体大小', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'FONT_SIZE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结束行数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'END_ROW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '开始行数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'START_ROW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG', @level2type = N'COLUMN', @level2name = N'PRINT_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_JIS打印配置表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_JIS_PRINT_CONFIG';

