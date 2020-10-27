CREATE TABLE [LES].[TL_SYS_OPERATION_LOG] (
    [OPERATION_ID]          UNIQUEIDENTIFIER NOT NULL,
    [PLANT]                 NVARCHAR (5)     NULL,
    [ASSEMBLYLINE]          NVARCHAR (10)    NULL,
    [OPERATION_TIME]        DATETIME         NOT NULL,
    [USER_LOGIN_NAME]       NVARCHAR (50)    NOT NULL,
    [OPERATION_ACTION]      NVARCHAR (200)   NOT NULL,
    [OPERATION_DETAIL]      NVARCHAR (MAX)   NULL,
    [OPERATION_PARAMETER1]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER2]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER3]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER4]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER5]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER6]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER7]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER8]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER9]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER10] NVARCHAR (100)   NULL,
    CONSTRAINT [IDX_PK_OPERATION_LOG_OPERATION_ID] PRIMARY KEY CLUSTERED ([OPERATION_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TL_SYS_OPERATION_LOG]
    ON [LES].[TL_SYS_OPERATION_LOG]([OPERATION_TIME] DESC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数10', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER10';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数9', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER9';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数8', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER8';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数7', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER7';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数6', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER6';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数5', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER5';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数4', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER4';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数3', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行参数1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_PARAMETER1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行详细信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_DETAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作动作', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_ACTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户登录名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'USER_LOGIN_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '装配线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'ASSEMBLYLINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG', @level2type = N'COLUMN', @level2name = N'OPERATION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_操作日志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_SYS_OPERATION_LOG';

