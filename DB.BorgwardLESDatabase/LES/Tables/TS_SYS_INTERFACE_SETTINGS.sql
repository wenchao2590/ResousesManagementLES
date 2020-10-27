CREATE TABLE [LES].[TS_SYS_INTERFACE_SETTINGS] (
    [ID]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [INTERFACE_CODE]    NVARCHAR (16)  NULL,
    [INTERFACE_NAME]    NVARCHAR (64)  NULL,
    [INTERFACE_NAME_CN] NVARCHAR (64)  NULL,
    [INTERFACE_DESC]    NVARCHAR (128) NULL,
    [URL]               NVARCHAR (512) NULL,
    [USERNAME]          NVARCHAR (128) NULL,
    [PASSWORD]          NVARCHAR (128) NULL,
    [TIME_OUT_SEC]      INT            NULL,
    [VALID_FLAG]        BIT            NULL,
    [CREATE_DATE]       DATETIME       NULL,
    [CREATE_USER]       NVARCHAR (64)  NULL,
    [MODIFY_DATE]       DATETIME       NULL,
    [MODIFY_USER]       NVARCHAR (64)  NULL,
    CONSTRAINT [PK_TS_SYS_INTERFACE_SETTINGS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'超时时间(秒)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_SETTINGS', @level2type = N'COLUMN', @level2name = N'TIME_OUT_SEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'密码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_SETTINGS', @level2type = N'COLUMN', @level2name = N'PASSWORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用户名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_SETTINGS', @level2type = N'COLUMN', @level2name = N'USERNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'调用地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_SETTINGS', @level2type = N'COLUMN', @level2name = N'URL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_SETTINGS', @level2type = N'COLUMN', @level2name = N'INTERFACE_DESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_SETTINGS', @level2type = N'COLUMN', @level2name = N'INTERFACE_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_SETTINGS', @level2type = N'COLUMN', @level2name = N'INTERFACE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接口代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_INTERFACE_SETTINGS', @level2type = N'COLUMN', @level2name = N'INTERFACE_CODE';

