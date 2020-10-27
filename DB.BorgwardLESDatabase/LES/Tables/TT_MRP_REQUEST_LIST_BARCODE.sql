CREATE TABLE [LES].[TT_MRP_REQUEST_LIST_BARCODE] (
    [SEQ_ID]                INT            IDENTITY (1, 1) NOT NULL,
    [CLIENT]                NVARCHAR (3)   NOT NULL,
    [SEND_PORT]             NVARCHAR (10)  NOT NULL,
    [IDOC]                  NVARCHAR (16)  NOT NULL,
    [LINE_NO]               NVARCHAR (6)   NOT NULL,
    [DELIVERY_DATE]         DATETIME       NOT NULL,
    [BARCODE]               NVARCHAR (50)  NULL,
    [LOGICAL_PK]            NVARCHAR (100) NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [PART_NO]               NVARCHAR (20)  NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [BOX_PARTS]             NVARCHAR (10)  NOT NULL,
    [MEASURING_UNIT_NO]     NVARCHAR (1)   NOT NULL,
    [INBOUND_PACKAGE]       INT            NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)  NULL,
    [BATTH_NO]              NVARCHAR (100) NULL,
    [PACHAGE_TYPE]          NVARCHAR (50)  NULL,
    [LINE_POSITION]         NVARCHAR (100) NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    CONSTRAINT [PK_TT_MRP_REQUEST_LIST_BARCODE] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)线旁位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'LINE_POSITION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)收货包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'PACHAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)批次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'BATTH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'BARCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'DELIVERY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'LINE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IDOC号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'SEND_PORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'CLIENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_BARCODE', @level2type = N'COLUMN', @level2name = N'SEQ_ID';

