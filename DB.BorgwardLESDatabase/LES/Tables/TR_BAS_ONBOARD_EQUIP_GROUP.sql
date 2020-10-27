CREATE TABLE [LES].[TR_BAS_ONBOARD_EQUIP_GROUP] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [EQUIP_ID]    BIGINT        NULL,
    [GROUP_ID]    BIGINT        NULL,
    [VALID_FLAG]  BIT           NULL,
    [CREATE_USER] NVARCHAR (64) NOT NULL,
    [CREATE_DATE] DATETIME      NOT NULL,
    [MODIFY_USER] NVARCHAR (64) NULL,
    [MODIFY_DATE] DATETIME      NULL,
    CONSTRAINT [PK_TR_BAS_ONBOARD_EQUIP_GROUP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车载任务ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_ONBOARD_EQUIP_GROUP', @level2type = N'COLUMN', @level2name = N'GROUP_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车载设备ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_ONBOARD_EQUIP_GROUP', @level2type = N'COLUMN', @level2name = N'EQUIP_ID';

