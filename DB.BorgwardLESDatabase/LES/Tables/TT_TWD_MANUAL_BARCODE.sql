CREATE TABLE [LES].[TT_TWD_MANUAL_BARCODE] (
    [BARCODE_DETAIL_ID]            INT            IDENTITY (1, 1) NOT NULL,
    [PLANT_ZONE]                   NVARCHAR (5)   NULL,
    [WORKSHOP]                     NVARCHAR (4)   NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)  NOT NULL,
    [PLANT]                        NVARCHAR (5)   NOT NULL,
    [TWD_RUNSHEET_SN]              INT            NOT NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)  NOT NULL,
    [PART_NO]                      NVARCHAR (20)  NOT NULL,
    [BARCODE_DATA]                 NVARCHAR (50)  NULL,
    [IDENTIFY_PART_NO]             NVARCHAR (20)  NULL,
    [PART_CNAME]                   NVARCHAR (100) NULL,
    [BOX_PARTS]                    NVARCHAR (10)  NOT NULL,
    [PICKUP_SEQ_NO]                INT            NULL,
    [RDC_DLOC]                     VARCHAR (20)   NULL,
    [MEASURING_UNIT_NO]            VARCHAR (8)    NULL,
    [INNER_LOCATION]               NVARCHAR (50)  NULL,
    [LOCATION]                     NVARCHAR (20)  NULL,
    [STORAGE_LOCATION]             NVARCHAR (30)  NULL,
    [INHOUSE_PACKAGE_MODEL]        NVARCHAR (30)  NULL,
    [REQUIRED_INBOUND_PACKAGE_QTY] INT            NOT NULL,
    [PRINT_TIMES]                  INT            NULL,
    [PRINT_DATE]                   DATETIME       NULL,
    [WMS_SEND_TIME]                DATETIME       NULL,
    [WMS_SEND_STATUS]              INT            NULL,
    [BARCODE_TYPE]                 NVARCHAR (10)  NULL,
    [REQUIRED_DATE]                DATETIME       NULL,
    [BATTH_NO]                     NVARCHAR (100) NULL,
    [PACHAGE_TYPE]                 NVARCHAR (50)  NULL,
    [LINE_POSITION]                NVARCHAR (100) NULL,
    [COMMENTS]                     NVARCHAR (200) NULL,
    [CREATE_DATE]                  DATETIME       NULL,
    [UPDATE_DATE]                  DATETIME       NULL,
    [UPDATE_USER]                  NVARCHAR (50)  NULL,
    [CREATE_USER]                  NVARCHAR (50)  NULL,
    [TRANS_SUPPLIER_NUM]           NVARCHAR (20)  NULL,
    [RESUMENO]                     NVARCHAR (20)  NULL,
    CONSTRAINT [IDX_PK_MANUAL_BARCODE_BARCODE_DETAIL_ID] PRIMARY KEY CLUSTERED ([BARCODE_DETAIL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件履历号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'RESUMENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '线旁位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'LINE_POSITION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'PACHAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '批次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'BATTH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'REQUIRED_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'BARCODE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WMS发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'PRINT_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'PRINT_TIMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'REQUIRED_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'STORAGE_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '内库位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'INNER_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_标识零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'IDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE', @level2type = N'COLUMN', @level2name = N'BARCODE_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_TWD 拉动单条码表_手工申请', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_MANUAL_BARCODE';

