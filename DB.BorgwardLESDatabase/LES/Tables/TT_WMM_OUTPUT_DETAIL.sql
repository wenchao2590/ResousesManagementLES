CREATE TABLE [LES].[TT_WMM_OUTPUT_DETAIL] (
    [OUTPUT_DETAIL_ID]      INT             IDENTITY (1, 1) NOT NULL,
    [OUTPUT_ID]             INT             NOT NULL,
    [PLANT]                 NVARCHAR (5)    NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NULL,
    [WM_NO]                 NVARCHAR (10)   NULL,
    [ZONE_NO]               NVARCHAR (20)   NULL,
    [DLOC]                  NVARCHAR (30)   NULL,
    [TRAN_NO]               NVARCHAR (50)   NULL,
    [TARGET_WM]             NVARCHAR (10)   NULL,
    [TARGET_ZONE]           NVARCHAR (20)   NULL,
    [TARGET_DLOC]           NVARCHAR (30)   NULL,
    [PART_NO]               NVARCHAR (20)   NULL,
    [PART_CNAME]            NVARCHAR (100)  NULL,
    [PACK_COUNT]            INT             NULL,
    [REQUIRED_BOX_NUM]      INT             NULL,
    [REQUIRED_QTY]          INT             NULL,
    [ACTUAL_BOX_NUM]        INT             NULL,
    [ACTUAL_QTY]            INT             NULL,
    [PACKAGE_MODEL]         NVARCHAR (30)   NULL,
    [BARCODE_DATA]          NVARCHAR (50)   NULL,
    [MEASURING_UNIT_NO]     VARCHAR (8)     NULL,
    [PACKAGE]               NVARCHAR (100)  NULL,
    [NUM]                   NUMERIC (18, 2) NULL,
    [BOX_NUM]               NUMERIC (18, 2) NULL,
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
    [SUPPLIER_NUM_SHEET]    NVARCHAR (12)   NULL,
    [BOX_PARTS_SHEET]       NVARCHAR (10)   NULL,
    [ORDER_NO]              NVARCHAR (50)   NULL,
    [ITEM_NO]               NVARCHAR (50)   NULL,
    [TWD_RUNSHEET_NO]       VARCHAR (22)    NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [CREATE_DATE]           DATETIME        NULL,
    [CREATE_USER]           NVARCHAR (50)   NULL,
    [REPACKAGE_FLAG]        INT             NULL,
    CONSTRAINT [PK__TT_DD_ReturnDeta__36470DEF] PRIMARY KEY CLUSTERED ([OUTPUT_DETAIL_ID] ASC),
    CONSTRAINT [FK_TT_WMM_OUTPUT_DETAIL_TT_WMM_OUTPUT] FOREIGN KEY ([OUTPUT_ID]) REFERENCES [LES].[TT_WMM_OUTPUT] ([OUTPUT_ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [INDEX_PLANT_WM_ZONE]
    ON [LES].[TT_WMM_OUTPUT_DETAIL]([PLANT] ASC, [WM_NO] ASC, [ZONE_NO] ASC)
    INCLUDE([OUTPUT_DETAIL_ID], [PART_NO], [REQUIRED_BOX_NUM], [PACKAGE_MODEL], [PACKAGE], [REPACKAGE_FLAG]);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_OUTPUT_DETAIL]
    ON [LES].[TT_WMM_OUTPUT_DETAIL]([PART_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [Uncluster_OUTPUT_ID_Index, sysname,>]
    ON [LES].[TT_WMM_OUTPUT_DETAIL]([OUTPUT_ID] ASC)
    INCLUDE([PART_NO], [PART_CNAME], [REQUIRED_BOX_NUM]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包处理标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'REPACKAGE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ITEM号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'ITEM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件类组单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据组单_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_标识零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'IDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的存贮区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_WM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '出库编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'OUTPUT_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '出库明细编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL', @level2type = N'COLUMN', @level2name = N'OUTPUT_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库出库明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT_DETAIL';

