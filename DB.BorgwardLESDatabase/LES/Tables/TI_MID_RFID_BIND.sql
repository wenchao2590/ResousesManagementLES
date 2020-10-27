CREATE TABLE [LES].[TI_MID_RFID_BIND] (
    [ID]           INT              IDENTITY (1, 1) NOT NULL,
    [FID]          UNIQUEIDENTIFIER NULL,
    [LPN_NO]       NVARCHAR (20)    NULL,
    [LPN_RFID_NO]  NVARCHAR (128)   NULL,
    [DATA_OP_TYPE] INT              NULL,
    CONSTRAINT [PK_TI_MID_RFID_BIND] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1.绑定 2.解绑 3.出库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BIND', @level2type = N'COLUMN', @level2name = N'DATA_OP_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘RFID号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BIND', @level2type = N'COLUMN', @level2name = N'LPN_RFID_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BIND', @level2type = N'COLUMN', @level2name = N'LPN_NO';

