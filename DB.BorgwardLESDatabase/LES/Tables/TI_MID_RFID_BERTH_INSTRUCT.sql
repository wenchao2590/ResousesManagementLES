CREATE TABLE [LES].[TI_MID_RFID_BERTH_INSTRUCT] (
    [ID]            INT              IDENTITY (1, 1) NOT NULL,
    [FID]           UNIQUEIDENTIFIER NULL,
    [CARGO_LICENCE] NVARCHAR (16)    NULL,
    [DRIVER_TEL]    NVARCHAR (32)    NULL,
    [MSG_CONTEXT]   NVARCHAR (512)   NULL,
    [MSG_DATE]      DATETIME         NULL,
    [IS_ADMIT]      BIT              NULL,
    [DOOR_NO]       NVARCHAR (16)    NULL,
    CONSTRAINT [PK_TT_SYS_MIDDLE_BERTH_INSTRUCT] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'消息时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BERTH_INSTRUCT', @level2type = N'COLUMN', @level2name = N'MSG_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示消息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BERTH_INSTRUCT', @level2type = N'COLUMN', @level2name = N'MSG_CONTEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'驾驶员手机号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BERTH_INSTRUCT', @level2type = N'COLUMN', @level2name = N'DRIVER_TEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆牌照号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BERTH_INSTRUCT', @level2type = N'COLUMN', @level2name = N'CARGO_LICENCE';

