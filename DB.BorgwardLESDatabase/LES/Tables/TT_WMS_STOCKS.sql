CREATE TABLE [LES].[TT_WMS_STOCKS] (
    [STOCK_IDENTITY]   INT             IDENTITY (1, 1) NOT NULL,
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
    [OCCUPY_AREA]      NUMERIC (18, 2) NULL,
    [DLOC]             NVARCHAR (20)   NULL,
    [MAX]              NUMERIC (18, 2) NULL,
    [MIN]              NUMERIC (18, 2) NULL,
    [ROW_NUMBER]       INT             NULL,
    [LINE_NUMBER]      INT             NULL,
    [HIGH_NUMBER]      INT             NULL,
    [MATERIAL_GROUP]   NVARCHAR (100)  NULL,
    [KEEPER]           NVARCHAR (50)   NULL,
    [TRANSER]          NVARCHAR (50)   NULL,
    [INFORMATIONER]    NVARCHAR (50)   NULL,
    [ELOC]             NVARCHAR (50)   NULL,
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
    [IS_REPACK]        INT             NULL,
    [REPACK_ROUTE]     NVARCHAR (40)   NULL,
    [IS_TRIGGER_PULL]  INT             NULL,
    [TRIGGER_WM_NO]    NVARCHAR (10)   NULL,
    [TRIGGER_ZONE_NO]  NVARCHAR (30)   NULL,
    [TRIGGER_DLOC]     NVARCHAR (30)   NULL,
    [EMG_TIME]         INT             NULL,
    [SUPPER_ZONE_DLOC] NVARCHAR (50)   NULL,
    [CHECK_TYPE]       INT             NULL,
    [BUSINESS_PK]      NVARCHAR (50)   NULL,
    [BATCH_NO]         NVARCHAR (100)  NULL,
    [BARCODE_DATA]     NVARCHAR (50)   NULL,
    [BARCODE_TYPE]     NVARCHAR (10)   NULL,
    [COMMENTS]         NVARCHAR (200)  NULL,
    [CREATE_USER]      NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]      DATETIME        NOT NULL,
    [UPDATE_USER]      NVARCHAR (50)   NULL,
    [UPDATE_DATE]      DATETIME        NULL,
    CONSTRAINT [IDX_PK_PARTS_STOCKS_STOCK_IDENTITY1] PRIMARY KEY NONCLUSTERED ([STOCK_IDENTITY] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMS_STOCKS_2]
    ON [LES].[TT_WMS_STOCKS]([ZONE_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMS_STOCKS_1]
    ON [LES].[TT_WMS_STOCKS]([WM_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMS_STOCKS]
    ON [LES].[TT_WMS_STOCKS]([PART_NO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'BARCODE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '批次', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '检验类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'CHECK_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '超市区库位号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'SUPPER_ZONE_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '紧急响应时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'EMG_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'TRIGGER_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'TRIGGER_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'TRIGGER_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否触发层级拉动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'IS_TRIGGER_PULL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'REPACK_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否翻包', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'IS_REPACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类别', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PART_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PART_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存数（件）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'STOCKS_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '剩余散件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'FRAGMENT_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计数器', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'COUNTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库规则', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'WMS_RULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否批次管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'IS_BATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '可用库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'AVAILBLE_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '冻结库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'FROZEN_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '安全库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'SAFE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '空器具存放地', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'ELOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '对应信息员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'INFORMATIONER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '对应配送员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'TRANSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '对应保管员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料组', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'MATERIAL_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '堆高', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'HIGH_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '占列', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'LINE_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '占行', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'ROW_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Min', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'MIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Max', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'MAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '所需面积', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'OCCUPY_AREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流路线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '删除标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标准包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标准包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PART_UNITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS', @level2type = N'COLUMN', @level2name = N'STOCK_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库_库存表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMS_STOCKS';

