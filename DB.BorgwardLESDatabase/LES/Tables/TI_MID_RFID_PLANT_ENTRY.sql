CREATE TABLE [LES].[TI_MID_RFID_PLANT_ENTRY] (
    [ID]            INT              IDENTITY (1, 1) NOT NULL,
    [FID]           UNIQUEIDENTIFIER NULL,
    [CARGO_LICENCE] NVARCHAR (16)    NULL,
    [DRIVER_TEL]    NVARCHAR (32)    NULL,
    [ORDER_NO]      NVARCHAR (32)    NULL,
    [PASS_TIME]     DATETIME         NULL,
    [PASS_FLAG]     INT              NULL,
    [DOOR_NO]       NVARCHAR (16)    NULL,
    CONSTRAINT [PK_LES.TT_SYS_MIDDLE_PLANT_ENTRY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入场or出场;1.入场2.出场', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_PLANT_ENTRY', @level2type = N'COLUMN', @level2name = N'PASS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通过门禁时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_PLANT_ENTRY', @level2type = N'COLUMN', @level2name = N'PASS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货单据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_PLANT_ENTRY', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'驾驶员手机号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_PLANT_ENTRY', @level2type = N'COLUMN', @level2name = N'DRIVER_TEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆牌照号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_PLANT_ENTRY', @level2type = N'COLUMN', @level2name = N'CARGO_LICENCE';

