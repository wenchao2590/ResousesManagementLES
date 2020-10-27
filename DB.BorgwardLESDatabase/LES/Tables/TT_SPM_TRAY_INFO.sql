CREATE TABLE [LES].[TT_SPM_TRAY_INFO] (
    [ID]              BIGINT         IDENTITY (1, 1) NOT NULL,
    [TRAY_NO]         NVARCHAR (20)  NOT NULL,
    [PLANT]           NVARCHAR (5)   NULL,
    [WM_NO]           NVARCHAR (10)  NULL,
    [ZONE_NO]         NVARCHAR (20)  NULL,
    [DLOC]            NVARCHAR (30)  NULL,
    [BILL_NO]         NVARCHAR (50)  NULL,
    [PART_NO]         NVARCHAR (20)  NULL,
    [PART_CNAME]      NVARCHAR (100) NULL,
    [NUM]             INT            NULL,
    [BOX_NUM]         INT            NULL,
    [BIND_TIME]       DATETIME       NULL,
    [TRAY_STATUS]     INT            NOT NULL,
    [BATCH_NO]        NVARCHAR (20)  NULL,
    [VALID_FLAG]      INT            CONSTRAINT [DF_TT_SPM_TRAY_INFO_VALID_FLAG] DEFAULT ((1)) NOT NULL,
    [COMMENTS]        NVARCHAR (200) NULL,
    [CREATE_USER]     NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]     DATETIME       CONSTRAINT [DF_TT_SPM_TRAY_INFO_CREATE_DATE] DEFAULT (getdate()) NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)  NULL,
    [UPDATE_DATE]     DATETIME       NULL,
    [FARBAU]          NVARCHAR (30)  NULL,
    [FARBIN]          NVARCHAR (30)  NULL,
    [MODEL_YEAR]      NVARCHAR (30)  NULL,
    [MODEL]           NVARCHAR (30)  NULL,
    [ZCOLORI]         NVARCHAR (30)  NULL,
    [ZCOLORI_D]       NVARCHAR (30)  NULL,
    [TWD_RUNSHEET_NO] NVARCHAR (30)  NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)  NULL,
    [DMSNO]           NVARCHAR (50)  NULL,
    [RFID_NO]         NVARCHAR (128) NULL,
    CONSTRAINT [PK_TT_SPM_TRAY_INFO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPM_TRAY_INFO_3]
    ON [LES].[TT_SPM_TRAY_INFO]([RFID_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPM_TRAY_INFO_2]
    ON [LES].[TT_SPM_TRAY_INFO]([PLANT] ASC, [WM_NO] ASC, [ZONE_NO] ASC, [DLOC] ASC, [PART_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPM_TRAY_INFO_1]
    ON [LES].[TT_SPM_TRAY_INFO]([BILL_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPM_TRAY_INFO]
    ON [LES].[TT_SPM_TRAY_INFO]([TRAY_NO] ASC, [VALID_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'电子标签', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'RFID_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DMS号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'DMSNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'外饰颜色描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'ZCOLORI_D';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'外饰颜色代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'ZCOLORI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内饰颜色描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'内饰颜色代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'特征包代码描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'FARBIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'特征包代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'FARBAU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'删除标记（0-未删除，1-已删除）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批次号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'TRAY_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'绑定时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'BIND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'BILL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'TRAY_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主键，自增长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_TRAY_INFO', @level2type = N'COLUMN', @level2name = N'ID';

