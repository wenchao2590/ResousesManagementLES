CREATE TABLE [LES].[TS_SYS_INTERFACE_CONFIG] (
    [ID]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]              UNIQUEIDENTIFIER NULL,
    [METHROD_NAME]     NVARCHAR (64)    NULL,
    [SYS_METHROD_NAME] NVARCHAR (128)   NULL,
    [CALL_URL]         NVARCHAR (1024)  NULL,
    [USER_NAME]        NVARCHAR (64)    NULL,
    [PASS_WORD]        NVARCHAR (128)   NULL,
    [VALID_FLAG]       BIT              NULL,
    [CREATE_USER]      NVARCHAR (32)    NULL,
    [CREATE_DATE]      DATETIME         NULL,
    [MODIFY_USER]      NVARCHAR (32)    NULL,
    [MODIFY_DATE]      DATETIME         NULL,
    [SYS_NAME]         NVARCHAR (32)    NULL,
    [PARAM1]           NVARCHAR (64)    NULL,
    [PARAM2]           NVARCHAR (64)    NULL,
    [PARAM3]           NVARCHAR (64)    NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'参数3', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'PARAM3';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'参数2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'PARAM2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'参数1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'PARAM1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'外部系统名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'SYS_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'密码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'PASS_WORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'USER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'访问地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'CALL_URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'外部函数名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'SYS_METHROD_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'函数名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG', @level2type = N'COLUMN', @level2name = N'METHROD_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'系统接口配置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_CONFIG';

