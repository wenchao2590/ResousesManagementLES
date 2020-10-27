CREATE TABLE [LES].[TR_BAS_PART_TRAY_STOCK] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [PLANT]       NVARCHAR (8)  NOT NULL,
    [WM_NO]       NVARCHAR (16) NOT NULL,
    [ZONE_NO]     NVARCHAR (32) NOT NULL,
    [DLOC]        NVARCHAR (32) NOT NULL,
    [PART_NO]     NVARCHAR (32) NULL,
    [TRAY_NO]     NVARCHAR (32) NULL,
    [BATCH_NO]    NVARCHAR (32) NULL,
    [NUM]         INT           NULL,
    [BOX_NUM]     INT           NULL,
    [DLOC_STATUS] INT           NULL,
    [VALID_FLAG]  BIT           NULL,
    [CREATE_USER] NVARCHAR (32) NULL,
    [CREATE_DATE] DATETIME      NULL,
    [MODIFY_USER] NVARCHAR (32) NULL,
    [MODIFY_DATE] DATETIME      NULL,
    CONSTRAINT [PK_TR_BAS_PART_TRAY_STOCK] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TR_BAS_PART_TRAY_STOCK]
    ON [LES].[TR_BAS_PART_TRAY_STOCK]([PLANT] ASC, [WM_NO] ASC, [ZONE_NO] ASC, [DLOC] ASC, [PART_NO] ASC, [TRAY_NO] ASC, [BATCH_NO] ASC, [DLOC_STATUS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否有效', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位状态，0-空闲，1-预占用，2-占用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'DLOC_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批次号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'TRAY_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_TRAY_STOCK', @level2type = N'COLUMN', @level2name = N'PLANT';

