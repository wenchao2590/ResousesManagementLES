CREATE TABLE [LES].[TT_SPM_TRAY_LOG] (
    [ID]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [TRAY_INFO_ID] BIGINT         NULL,
    [TRAY_NO]      NVARCHAR (32)  NULL,
    [PLANT]        NVARCHAR (8)   NULL,
    [WM_NO]        NVARCHAR (16)  NULL,
    [ZONE_NO]      NVARCHAR (32)  NULL,
    [DLOC]         NVARCHAR (32)  NULL,
    [TARGET_WM]    NVARCHAR (16)  NULL,
    [TARGET_ZONE]  NVARCHAR (32)  NULL,
    [TARGET_DLOC]  NVARCHAR (32)  NULL,
    [ORDER_NO]     NVARCHAR (64)  NULL,
    [ORDER_TYPE]   INT            NULL,
    [PART_NO]      NVARCHAR (32)  NULL,
    [PART_CNAME]   NVARCHAR (128) NULL,
    [PART_QTY]     INT            NULL,
    [BOX_QTY]      INT            NULL,
    [BATCH_NO]     NVARCHAR (32)  NULL,
    [DEAL_STATUS]  INT            NULL,
    [DEAL_TIME]    DATETIME       NULL,
    [VALID_FLAG]   BIT            NULL,
    [CREATE_USER]  NVARCHAR (32)  NULL,
    [CREATE_DATE]  DATETIME       NULL,
    [MODIFY_USER]  NVARCHAR (32)  NULL,
    [MODIFY_DATE]  DATETIME       NULL,
    CONSTRAINT [PK_TT_SPM_TRAY_LOG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPM_TRAY_LOG]
    ON [LES].[TT_SPM_TRAY_LOG]([TRAY_NO] ASC, [DEAL_STATUS] ASC, [ORDER_TYPE] ASC, [ORDER_NO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'DEAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'DEAL_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库批次号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'BOX_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PART_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单据类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'ORDER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'TARGET_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'TARGET_WM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘标签号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'TRAY_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘状态ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_LOG', @level2type = N'COLUMN', @level2name = N'TRAY_INFO_ID';

