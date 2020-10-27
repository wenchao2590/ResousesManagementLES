CREATE TABLE [LES].[TR_WMM_REPACKAGE_RELATION] (
    [SEQ_ID]                INT             IDENTITY (1, 1) NOT NULL,
    [REPACKAGE_ID]          INT             NOT NULL,
    [REPACKAGE_NO]          NVARCHAR (50)   NULL,
    [TRAN_ID]               INT             NOT NULL,
    [WM_NO]                 NVARCHAR (10)   NULL,
    [PART_NO]               VARCHAR (30)    NULL,
    [PART_NICKNAME]         NVARCHAR (50)   NULL,
    [PART_CNAME]            VARCHAR (100)   NULL,
    [ZONE_NO]               NVARCHAR (20)   NOT NULL,
    [ULOC]                  NVARCHAR (30)   NOT NULL,
    [LOCATION]              NVARCHAR (20)   NULL,
    [PART_TYPE]             NVARCHAR (20)   NULL,
    [NUM]                   NUMERIC (18, 2) NOT NULL,
    [INHOUSE_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INHOUSE_PACKAGE]       INT             NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INBOUND_PACKAGE]       INT             NULL,
    [REPACKGE_STATE]        INT             NULL,
    [REPACKGE_DATE]         DATETIME        NULL,
    [PRE_BARCODE_DATA]      NVARCHAR (50)   NULL,
    [POST_BARCODE_DATA]     NVARCHAR (50)   NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [CREATE_USER]           NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]           DATETIME        NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    CONSTRAINT [PK_TR_WMM_REPACKAGE_RELATION] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包后条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'POST_BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包前条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'PRE_BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'REPACKGE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'REPACKGE_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装数量(翻包前）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号(翻包前）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量(翻包后）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号(翻包后）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'ULOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'TRAN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'REPACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知单ID2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库翻包条码对应', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_WMM_REPACKAGE_RELATION';

