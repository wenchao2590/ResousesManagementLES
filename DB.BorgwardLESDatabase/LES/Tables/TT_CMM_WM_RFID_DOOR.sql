CREATE TABLE [LES].[TT_CMM_WM_RFID_DOOR] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]        NVARCHAR (8)   NULL,
    [WM_NO]        NVARCHAR (16)  NULL,
    [ZONE_NO]      NVARCHAR (32)  NULL,
    [RFID_DOOR_NO] NVARCHAR (16)  NULL,
    [COMMENTS]     NVARCHAR (256) NULL,
    CONSTRAINT [PK_TT_CMM_WM_RFID_DOOR] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RFID物流门编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WM_RFID_DOOR', @level2type = N'COLUMN', @level2name = N'RFID_DOOR_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WM_RFID_DOOR', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_WM_RFID_DOOR', @level2type = N'COLUMN', @level2name = N'WM_NO';

