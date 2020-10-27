CREATE TABLE [LES].[TM_WMM_NOTIFICATION_DETAIL_IMPORT] (
    [TRAN_ID]         INT             NOT NULL,
    [NOTIFICATIONID]  INT             NOT NULL,
    [WM_NO]           NVARCHAR (10)   NULL,
    [PART_NO]         NVARCHAR (30)   NULL,
    [PART_CNAME]      NVARCHAR (100)  NULL,
    [ZONE_NO]         NVARCHAR (20)   NOT NULL,
    [ULOC]            NVARCHAR (30)   NOT NULL,
    [LOCATION]        NVARCHAR (20)   NULL,
    [PART_TYPE]       NVARCHAR (20)   NULL,
    [NUM]             NUMERIC (18, 2) NULL,
    [REAL_NUM]        NUMERIC (18, 2) NULL,
    [FREEZE_NUM]      NUMERIC (18, 2) NULL,
    [COMMENTS]        NVARCHAR (200)  NULL,
    [CREATE_USER]     NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]     DATETIME        NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)   NULL,
    [UPDATE_DATE]     DATETIME        NULL,
    [BARCODE_DATA]    NVARCHAR (50)   NULL,
    [PLANT]           NVARCHAR (5)    NULL,
    [BARCODE_TYPE]    NVARCHAR (10)   NULL,
    [TRAN_STATE]      INT             NULL,
    [MEINS]           NVARCHAR (8)    NULL,
    [SUPPLIER_NUM]    NVARCHAR (12)   NULL,
    [VALUE_SORT]      NVARCHAR (10)   NULL,
    [PARTS_ATTRIBUTE] NVARCHAR (10)   NULL,
    [COUNT_PARTITION] NVARCHAR (100)  NULL,
    CONSTRAINT [PK_TM_WMM_NOTIFICATION_DETAIL_] PRIMARY KEY CLUSTERED ([TRAN_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MEINS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'TRAN_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'BARCODE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '冻结数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'FREEZE_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实盘数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'REAL_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'ULOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'NOTIFICATIONID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT', @level2type = N'COLUMN', @level2name = N'TRAN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库盘点通知单录入表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_NOTIFICATION_DETAIL_IMPORT';

