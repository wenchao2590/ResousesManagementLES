CREATE TABLE [LES].[TT_WMM_VMI_RECEIVE_DETAIL] (
    [RECEIVE_DETAIL_ID]     INT             IDENTITY (1, 1) NOT NULL,
    [RECEIVE_ID]            INT             NULL,
    [PLANT]                 NVARCHAR (5)    NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NULL,
    [WM_NO]                 NVARCHAR (10)   NULL,
    [ZONE_NO]               NVARCHAR (20)   NULL,
    [DLOC]                  NVARCHAR (30)   NULL,
    [TARGET_WM]             NVARCHAR (10)   NULL,
    [TARGET_ZONE]           NVARCHAR (20)   NULL,
    [TARGET_DLOC]           NVARCHAR (30)   NULL,
    [MEASURING_UNIT_NO]     VARCHAR (8)     NULL,
    [PACKAGE]               NVARCHAR (100)  NULL,
    [NUM]                   NUMERIC (18, 2) NULL,
    [BOX_NUM]               NUMERIC (18, 2) NULL,
    [PART_NO]               NVARCHAR (20)   NULL,
    [PART_CNAME]            NVARCHAR (100)  NULL,
    [PART_TYPE]             INT             NULL,
    [REQUIRED_BOX_NUM]      INT             NULL,
    [REQUIRED_QTY]          INT             NULL,
    [ACTUAL_BOX_NUM]        INT             NULL,
    [ACTUAL_QTY]            INT             NULL,
    [PACKAGE_MODEL]         NVARCHAR (30)   NULL,
    [BARCODE_DATA]          NVARCHAR (50)   NULL,
    [TRAN_NO]               NVARCHAR (10)   NULL,
    [IDENTIFY_PART_NO]      NVARCHAR (20)   NULL,
    [PART_ENAME]            NVARCHAR (100)  NULL,
    [DOCK]                  NVARCHAR (10)   NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)   NULL,
    [BOX_PARTS]             NVARCHAR (10)   NULL,
    [SEQUENCE_NO]           INT             NULL,
    [PICKUP_SEQ_NO]         INT             NULL,
    [RDC_DLOC]              VARCHAR (20)    NULL,
    [INHOUSE_PACKAGE]       INT             NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [PACK_COUNT]            INT             NULL,
    [TWD_RUNSHEET_NO]       VARCHAR (100)   NULL,
    [SUPPLIER_NUM_SHEET]    NVARCHAR (12)   NULL,
    [BOX_PARTS_SHEET]       NVARCHAR (10)   NULL,
    [RETURN_REPORT_FLAG]    INT             NULL,
    [ORDER_NO]              NVARCHAR (50)   NULL,
    [ITEM_NO]               NVARCHAR (50)   NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [CREATE_USER]           NVARCHAR (50)   NULL,
    [CREATE_DATE]           DATETIME        NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    [Current_BOX_NUM]       INT             NULL,
    [Current_QTY]           INT             NULL,
    [Z_SERIAL]              NVARCHAR (32)   NULL,
    CONSTRAINT [PK_TT_WMM_VMI_RECEIVE_DETAIL] PRIMARY KEY CLUSTERED ([RECEIVE_DETAIL_ID] ASC),
    CONSTRAINT [FK_TT_WMM_VMI_RECEIVE_DETAIL_TT_WMM_VMI_RECEIVE] FOREIGN KEY ([RECEIVE_ID]) REFERENCES [LES].[TT_WMM_VMI_RECEIVE] ([RECEIVE_ID]) ON DELETE CASCADE
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'Z_SERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'Current_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'Current_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ITEM号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'ITEM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'RETURN_REPORT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_零件类组单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据组单_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)TWD 零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件英文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_标识零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'IDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物料类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实收箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实收件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目的库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目的存贮区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目的仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_WM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入库流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'RECEIVE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_VMI_RECEIVE_DETAIL', @level2type = N'COLUMN', @level2name = N'RECEIVE_DETAIL_ID';

