CREATE TABLE [LES].[TS_SYS_AUTH] (
    [ROLE_ID]       INT            NOT NULL,
    [RESOURCE_TYPE] NVARCHAR (10)  NOT NULL,
    [RESOURCE_NAME] NVARCHAR (100) NULL,
    [RESOURCE_ID]   INT            NOT NULL,
    CONSTRAINT [IDX_PK_AUTH_ROLE_ID_RESOURCE_TYPE_RESOURCE_ID] PRIMARY KEY CLUSTERED ([ROLE_ID] ASC, [RESOURCE_TYPE] ASC, [RESOURCE_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESOURCE_ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_AUTH', @level2type = N'COLUMN', @level2name = N'RESOURCE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESOURCE_NAME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_AUTH', @level2type = N'COLUMN', @level2name = N'RESOURCE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RESOURCE_TYPE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_AUTH', @level2type = N'COLUMN', @level2name = N'RESOURCE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ROLE_ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_AUTH', @level2type = N'COLUMN', @level2name = N'ROLE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_授权表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_AUTH';

