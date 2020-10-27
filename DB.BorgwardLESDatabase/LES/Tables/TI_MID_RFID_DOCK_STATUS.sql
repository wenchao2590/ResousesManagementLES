CREATE TABLE [LES].[TI_MID_RFID_DOCK_STATUS] (
    [ID]          INT              IDENTITY (1, 1) NOT NULL,
    [FID]         UNIQUEIDENTIFIER NULL,
    [DOCK_NO]     NVARCHAR (16)    NULL,
    [DOCK_STATUS] INT              NULL,
    [MODIFY_DATE] DATETIME         NULL,
    CONSTRAINT [PK_TT_SYS_MIDDLE_DOCK_STATUS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'变化时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'MODIFY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口状态；0：释放；1：占用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'DOCK_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_DOCK_STATUS', @level2type = N'COLUMN', @level2name = N'DOCK_NO';

