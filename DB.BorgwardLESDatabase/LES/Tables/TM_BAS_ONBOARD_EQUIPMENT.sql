CREATE TABLE [LES].[TM_BAS_ONBOARD_EQUIPMENT] (
    [ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [EQUIP_CPU]   NVARCHAR (64)  NULL,
    [EQUIP_HDD]   NVARCHAR (64)  NULL,
    [COMMENTS]    NVARCHAR (512) NULL,
    [OS_VERSION]  NVARCHAR (128) NULL,
    [LES_VERSION] NVARCHAR (128) NULL,
    [IP_ADDRESS]  NVARCHAR (64)  NULL,
    [STATUS]      INT            NULL,
    [VALID_FLAG]  BIT            NULL,
    [CREATE_USER] NVARCHAR (64)  NOT NULL,
    [CREATE_DATE] DATETIME       NOT NULL,
    [MODIFY_USER] NVARCHAR (64)  NULL,
    [MODIFY_DATE] DATETIME       NULL,
    [RFID_IP]     NVARCHAR (64)  NULL,
    [RFID_PORT]   INT            NULL,
    CONSTRAINT [PK_TM_BAS_ONBOARD_EQUIPMENT] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_EQUIPMENT', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IP地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_EQUIPMENT', @level2type = N'COLUMN', @level2name = N'IP_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'LES版本', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_EQUIPMENT', @level2type = N'COLUMN', @level2name = N'LES_VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作系统版本', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_EQUIPMENT', @level2type = N'COLUMN', @level2name = N'OS_VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_EQUIPMENT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'硬盘唯一号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_EQUIPMENT', @level2type = N'COLUMN', @level2name = N'EQUIP_HDD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CPU唯一号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_ONBOARD_EQUIPMENT', @level2type = N'COLUMN', @level2name = N'EQUIP_CPU';

