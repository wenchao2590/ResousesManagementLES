CREATE TABLE [LES].[TI_MID_PTL_MATERIAL_REDUCE_HISTORY] (
    [ID]            INT              IDENTITY (1, 1) NOT NULL,
    [FID]           UNIQUEIDENTIFIER NULL,
    [OrderNo]       NVARCHAR (32)    NULL,
    [OrderStatus]   INT              NULL,
    [VIN]           NVARCHAR (32)    NULL,
    [SeqNo]         NVARCHAR (16)    NULL,
    [PartNo]        NVARCHAR (20)    NULL,
    [PartName]      NVARCHAR (100)   NULL,
    [Qty]           DECIMAL (18, 2)  NULL,
    [BoxPart]       NVARCHAR (16)    NULL,
    [OrderSyncTime] DATETIME         NULL,
    [CreateTime]    DATETIME         NULL,
    [WM_NO]         NVARCHAR (10)    NULL,
    [PLANT]         NVARCHAR (5)     NULL,
    CONSTRAINT [PK_TI_MID_PTL_MATERIAL_REDUCE_HISTORY] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单同步时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_MATERIAL_REDUCE_HISTORY', @level2type = N'COLUMN', @level2name = N'OrderSyncTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件所属区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_MATERIAL_REDUCE_HISTORY', @level2type = N'COLUMN', @level2name = N'BoxPart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_MATERIAL_REDUCE_HISTORY', @level2type = N'COLUMN', @level2name = N'Qty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_MATERIAL_REDUCE_HISTORY', @level2type = N'COLUMN', @level2name = N'PartName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_MATERIAL_REDUCE_HISTORY', @level2type = N'COLUMN', @level2name = N'PartNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_MATERIAL_REDUCE_HISTORY', @level2type = N'COLUMN', @level2name = N'SeqNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'0.未关闭；1.已关闭', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_MATERIAL_REDUCE_HISTORY', @level2type = N'COLUMN', @level2name = N'OrderStatus';

