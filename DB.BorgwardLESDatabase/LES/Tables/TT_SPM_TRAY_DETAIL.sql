CREATE TABLE [LES].[TT_SPM_TRAY_DETAIL] (
    [ID]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [TRAY_NO]      NVARCHAR (20)  NOT NULL,
    [BARCODE_DATA] NVARCHAR (50)  NOT NULL,
    [PLANT]        NVARCHAR (5)   NULL,
    [WM_NO]        NVARCHAR (10)  NULL,
    [ZONE_NO]      NVARCHAR (20)  NULL,
    [DLOC]         NVARCHAR (30)  NULL,
    [PART_NO]      NVARCHAR (20)  NULL,
    [PART_CNAME]   NVARCHAR (100) NULL,
    [NUM]          INT            NULL,
    [BIND_TIME]    DATETIME       NULL,
    [UNBIND_TIME]  DATETIME       NULL,
    [BATCH_NO]     NVARCHAR (20)  NULL,
    [TRAY_ID]      BIGINT         NULL,
    [BAR_ID]       BIGINT         NULL,
    [BIND_FLAG]    INT            CONSTRAINT [DF_TT_SPM_TRAY_DETAIL_BIND_FLAG] DEFAULT ((1)) NOT NULL,
    [VALID_FLAG]   INT            CONSTRAINT [DF_TT_SPM_TRAY_DETAIL_VALID_FLAG] DEFAULT ((1)) NOT NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [CREATE_USER]  NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]  DATETIME       CONSTRAINT [DF_TT_SPM_TRAY_DETAIL_CREATE_DATE] DEFAULT (getdate()) NOT NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    CONSTRAINT [PK_TT_SPM_TRAY_DETAIL] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPM_TRAY_DETAIL_1]
    ON [LES].[TT_SPM_TRAY_DETAIL]([PLANT] ASC, [WM_NO] ASC, [ZONE_NO] ASC, [DLOC] ASC, [PART_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPM_TRAY_DETAIL]
    ON [LES].[TT_SPM_TRAY_DETAIL]([TRAY_NO] ASC, [BARCODE_DATA] ASC, [VALID_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'删除标记（0-未删除，1-已删除）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'绑定状态（0-绑定，1-解绑）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'BIND_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'BAR_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAY_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批次号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'解绑时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'UNBIND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'绑定时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'BIND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAY_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，自增长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_DETAIL', @level2type = N'COLUMN', @level2name = N'ID';

