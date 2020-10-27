CREATE TABLE [LES].[TT_WMM_ONBOARD_TASK] (
    [ID]                     BIGINT          IDENTITY (1, 1) NOT NULL,
    [TASK_NO]                NVARCHAR (32)   NULL,
    [EQUIP_ID]               BIGINT          NULL,
    [GROUP_ID]               BIGINT          NULL,
    [TASK_TYPE]              INT             NULL,
    [PART_NO]                NVARCHAR (32)   NULL,
    [PART_CNAME]             NVARCHAR (128)  NULL,
    [PART_ENAME]             NVARCHAR (128)  NULL,
    [BOX_QTY]                INT             NULL,
    [RECOMMEND_TRAY_BARCODE] NVARCHAR (128)  NULL,
    [REAL_TRAY_BARCODE]      NVARCHAR (128)  NULL,
    [PART_QTY]               DECIMAL (18, 4) NULL,
    [PLANT]                  NVARCHAR (8)    NULL,
    [SOURCE_WM_NO]           NVARCHAR (16)   NULL,
    [SOURCE_ZONE_NO]         NVARCHAR (8)    NULL,
    [RECOMMEND_SOURCE_DLOC]  NVARCHAR (32)   NULL,
    [REAL_SOURCE_DLOC]       NVARCHAR (32)   NULL,
    [TARGET_WM_NO]           NVARCHAR (16)   NULL,
    [TARGET_ZONE_NO]         NVARCHAR (8)    NULL,
    [RECOMMEND_TARGET_DLOC]  NVARCHAR (32)   NULL,
    [REAL_TARGET_DLOC]       NVARCHAR (32)   NULL,
    [STATUS]                 INT             NULL,
    [ACCEPTED_TIME]          DATETIME        NULL,
    [FINISHED_TIME]          DATETIME        NULL,
    [DIFF_REASON]            NVARCHAR (64)   NULL,
    [VALID_FLAG]             BIT             NULL,
    [CREATE_DATE]            DATETIME        NULL,
    [CREATE_USER]            NVARCHAR (32)   NULL,
    [MODIFY_DATE]            DATETIME        NULL,
    [MODIFY_USER]            NVARCHAR (32)   NULL,
    [ROUTE]                  NVARCHAR (64)   NULL,
    CONSTRAINT [PK_TT_WMM_ONBOARD_TASK] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'差异理由', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'DIFF_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'完成时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'FINISHED_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接受时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'ACCEPTED_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际目标库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'REAL_TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'推荐目标库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'RECOMMEND_TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'TARGET_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'TARGET_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际来源库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'REAL_SOURCE_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'推荐来源库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'RECOMMEND_SOURCE_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'来源存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'SOURCE_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'来源仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'SOURCE_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'PART_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际托盘标签条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'REAL_TRAY_BARCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'推荐托盘标签条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'RECOMMEND_TRAY_BARCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'BOX_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件英文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'TASK_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车载任务ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'GROUP_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车载设备ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'EQUIP_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_ONBOARD_TASK', @level2type = N'COLUMN', @level2name = N'TASK_NO';

