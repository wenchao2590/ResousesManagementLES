CREATE TABLE [LES].[TI_MID_RFID_BOX_LIST] (
    [ID]          INT              IDENTITY (1, 1) NOT NULL,
    [FID]         UNIQUEIDENTIFIER NULL,
    [BOX_NO]      NVARCHAR (50)    NULL,
    [BOX_RFID_NO] NVARCHAR (128)   NULL,
    CONSTRAINT [PK_TI_MID_RFID_BOX_LIST] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱标签RFID号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BOX_LIST', @level2type = N'COLUMN', @level2name = N'BOX_RFID_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱标签', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BOX_LIST', @level2type = N'COLUMN', @level2name = N'BOX_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GUID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BOX_LIST', @level2type = N'COLUMN', @level2name = N'FID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_BOX_LIST', @level2type = N'COLUMN', @level2name = N'ID';

