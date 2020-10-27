CREATE TABLE [LES].[TI_MID_PTL_PART_LABLE] (
    [ID]         INT              IDENTITY (1, 1) NOT NULL,
    [FID]        UNIQUEIDENTIFIER NULL,
    [BarCode]    NVARCHAR (64)    NULL,
    [PartNo]     NVARCHAR (20)    NULL,
    [PartName]   NVARCHAR (100)   NULL,
    [OrderNo]    NVARCHAR (32)    NULL,
    [Qty]        DECIMAL (18, 2)  NULL,
    [CreateTime] DATETIME         NULL,
    [BoxPart]    NVARCHAR (16)    NULL,
    CONSTRAINT [PK_TT_SYS_MIDDLE_PART_LABLE] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PART_LABLE', @level2type = N'COLUMN', @level2name = N'Qty';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PART_LABLE', @level2type = N'COLUMN', @level2name = N'OrderNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PART_LABLE', @level2type = N'COLUMN', @level2name = N'PartName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PART_LABLE', @level2type = N'COLUMN', @level2name = N'PartNo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MID_PTL_PART_LABLE', @level2type = N'COLUMN', @level2name = N'BarCode';

