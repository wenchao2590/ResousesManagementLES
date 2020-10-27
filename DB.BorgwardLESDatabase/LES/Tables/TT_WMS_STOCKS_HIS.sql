CREATE TABLE [LES].[TT_WMS_STOCKS_HIS] (
    [STOCK_IDENTITY]   INT             NOT NULL,
    [PLANT]            NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]    NVARCHAR (10)   NULL,
    [PLANT_ZONE]       NVARCHAR (5)    NULL,
    [WORKSHOP]         NVARCHAR (4)    NULL,
    [SUPPLIER_NUM]     NVARCHAR (12)   NULL,
    [PART_NO]          NVARCHAR (20)   NOT NULL,
    [PART_CNAME]       NVARCHAR (100)  NULL,
    [PART_ENAME]       NVARCHAR (100)  NULL,
    [PART_NICKNAME]    NVARCHAR (50)   NULL,
    [PART_UNITS]       NVARCHAR (20)   NULL,
    [PACKAGE_MODEL]    NVARCHAR (30)   NULL,
    [PACKAGE]          INT             NULL,
    [LOGICAL_PK]       NVARCHAR (50)   NULL,
    [DELETE_FLAG]      BIT             NULL,
    [ROUTE]            NVARCHAR (50)   NULL,
    [ZONE_NO]          NVARCHAR (20)   NOT NULL,
    [WM_NO]            NVARCHAR (10)   NOT NULL,
    [DLOC]             NVARCHAR (20)   NULL,
    [MATERIAL_GROUP]   NVARCHAR (100)  NULL,
    [SAFE_STOCK]       INT             NULL,
    [STOCKS]           NUMERIC (18, 2) NULL,
    [FROZEN_STOCKS]    NUMERIC (18, 2) NULL,
    [AVAILBLE_STOCKS]  NUMERIC (18, 2) NULL,
    [IS_BATCH]         INT             NULL,
    [WMS_RULE]         NVARCHAR (20)   NULL,
    [COUNTER]          NUMERIC (18, 2) NULL,
    [FRAGMENT_NUM]     NUMERIC (18, 2) NULL,
    [STOCKS_NUM]       NUMERIC (18, 2) NULL,
    [PART_WEIGHT]      NUMERIC (18, 2) NULL,
    [PART_CLS]         NVARCHAR (50)   NULL,
    [SUPPER_ZONE_DLOC] NVARCHAR (50)   NULL,
    [CHECK_TYPE]       INT             NULL,
    [BUSINESS_PK]      NVARCHAR (50)   NULL,
    [BATCH_NO]         NVARCHAR (100)  NULL,
    [COMMENTS]         NVARCHAR (200)  NULL,
    [CREATE_USER]      NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]      DATETIME        NOT NULL,
    [UPDATE_USER]      NVARCHAR (50)   NULL,
    [UPDATE_DATE]      DATETIME        NULL,
    [BARCODE_DATA]     NVARCHAR (50)   NULL,
    [BARCODE_TYPE]     NVARCHAR (10)   NULL,
    CONSTRAINT [IDX_PK_PARTS_STOCKS_HIS_IDENTITY] PRIMARY KEY NONCLUSTERED ([STOCK_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'BARCODE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '批次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '检验类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'CHECK_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '超市区库位号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'SUPPER_ZONE_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类别', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PART_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PART_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'STOCKS_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'FRAGMENT_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计数器', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'COUNTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库规则', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'WMS_RULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否批次管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'IS_BATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '可用库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'AVAILBLE_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '冻结库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'FROZEN_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '安全库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'SAFE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料组', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'MATERIAL_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流路线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '删除标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标准包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标准包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PART_UNITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS', @level2type = N'COLUMN', @level2name = N'STOCK_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库_库存表历史表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS_HIS';

