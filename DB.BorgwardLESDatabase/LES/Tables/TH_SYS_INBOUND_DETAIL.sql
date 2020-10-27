CREATE TABLE [LES].[TH_SYS_INBOUND_DETAIL] (
    [ID]                 BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]                UNIQUEIDENTIFIER NULL,
    [INBOUND_FID]        UNIQUEIDENTIFIER NULL,
    [EXECUTE_RESULT]     INT              NULL,
    [EXECUTE_START_TIME] DATETIME         NULL,
    [EXECUTE_END_TIME]   DATETIME         NULL,
    [SOURCE_XML]         NVARCHAR (MAX)   NULL,
    [ERROR_CODE]         NVARCHAR (128)   NULL,
    [ERROR_DESC]         NVARCHAR (MAX)   NULL,
    [VALID_FLAG]         BIT              NULL,
    [CREATE_USER]        NVARCHAR (32)    NULL,
    [CREATE_DATE]        DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'错误描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TH_SYS_INBOUND_DETAIL', @level2type = N'COLUMN', @level2name = N'ERROR_DESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'错误代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TH_SYS_INBOUND_DETAIL', @level2type = N'COLUMN', @level2name = N'ERROR_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'原始数据', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TH_SYS_INBOUND_DETAIL', @level2type = N'COLUMN', @level2name = N'SOURCE_XML';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'完成执行时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TH_SYS_INBOUND_DETAIL', @level2type = N'COLUMN', @level2name = N'EXECUTE_END_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开始执行时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TH_SYS_INBOUND_DETAIL', @level2type = N'COLUMN', @level2name = N'EXECUTE_START_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TH_SYS_INBOUND_DETAIL', @level2type = N'COLUMN', @level2name = N'EXECUTE_RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据导入日志明细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TH_SYS_INBOUND_DETAIL';

