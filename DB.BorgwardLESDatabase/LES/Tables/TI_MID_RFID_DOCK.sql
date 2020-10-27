CREATE TABLE [LES].[TI_MID_RFID_DOCK] (
    [ID]           INT              IDENTITY (1, 1) NOT NULL,
    [FID]          UNIQUEIDENTIFIER NULL,
    [DOCK_NO]      NVARCHAR (16)    NULL,
    [DOCK_NAME]    NVARCHAR (32)    NULL,
    [DATA_OP_TYPE] INT              NULL,
    [MODIFY_TIME]  DATETIME         NULL,
    CONSTRAINT [PK_TT_SYS_MIDDLE_DOCK] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_DOCK', @level2type = N'COLUMN', @level2name = N'MODIFY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据操作方式；1.新增2.更新3.删除', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_DOCK', @level2type = N'COLUMN', @level2name = N'DATA_OP_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_DOCK', @level2type = N'COLUMN', @level2name = N'DOCK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_DOCK', @level2type = N'COLUMN', @level2name = N'DOCK_NO';

