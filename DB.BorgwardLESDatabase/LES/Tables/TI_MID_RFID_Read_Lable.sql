CREATE TABLE [LES].[TI_MID_RFID_Read_Lable] (
    [ID]           INT              IDENTITY (1, 1) NOT NULL,
    [FID]          UNIQUEIDENTIFIER NULL,
    [LPN_NO]       NVARCHAR (16)    NULL,
    [RFID_NO]      NVARCHAR (512)   NULL,
    [RECEIVE_TIME] DATETIME         NULL,
    [DOOR_NO]      NVARCHAR (16)    NULL,
    [DATA_OP_TYPE] INT              NOT NULL,
    CONSTRAINT [PK_TT_SYS_MIDDLE_RFID_Read_Lable] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据类型 1.收货 2.配送出库 3.空箱入库 4.空箱返3PL', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_Read_Lable', @level2type = N'COLUMN', @level2name = N'DATA_OP_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'门编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_Read_Lable', @level2type = N'COLUMN', @level2name = N'DOOR_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_Read_Lable', @level2type = N'COLUMN', @level2name = N'RECEIVE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'料箱RFID号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_Read_Lable', @level2type = N'COLUMN', @level2name = N'RFID_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_RFID_Read_Lable', @level2type = N'COLUMN', @level2name = N'LPN_NO';

