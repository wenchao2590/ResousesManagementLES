CREATE TABLE [LES].[TI_SYS_INBOUND] (
    [ID]                 BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]                UNIQUEIDENTIFIER NULL,
    [TRANS_NO]           NVARCHAR (64)    NULL,
    [METHORD_NAME]       NVARCHAR (64)    NULL,
    [EXECUTE_RESULT]     INT              NULL,
    [EXECUTE_START_TIME] DATETIME         NULL,
    [EXECUTE_END_TIME]   DATETIME         NULL,
    [KEY_VALUE]          NVARCHAR (256)   NULL,
    [SOURCE_XML]         NVARCHAR (MAX)   NULL,
    [ERROR_CODE]         NVARCHAR (128)   NULL,
    [ERROR_DESC]         NVARCHAR (MAX)   NULL,
    [VALID_FLAG]         BIT              NULL,
    [CREATE_USER]        NVARCHAR (32)    NULL,
    [CREATE_DATE]        DATETIME         NULL,
    [MODIFY_USER]        NVARCHAR (32)    NULL,
    [MODIFY_DATE]        DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'错误描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'ERROR_DESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'错误代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'ERROR_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'原始数据', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'SOURCE_XML';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'KEY_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'完成执行时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'EXECUTE_END_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'开始执行时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'EXECUTE_START_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务状态;0.Submit;1:Process;2:Sucess;3:Filed;4:Exception;5:Resend;6:Cancel', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'EXECUTE_RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'函数名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'METHORD_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'事件编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND', @level2type = N'COLUMN', @level2name = N'TRANS_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据导入日志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_SYS_INBOUND';

