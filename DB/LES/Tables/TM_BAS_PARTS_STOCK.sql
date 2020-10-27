CREATE TABLE [LES].[TM_BAS_PARTS_STOCK] (
    [STOCK_IDENTITY]        INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)   NULL,
    [PLANT_ZONE]            NVARCHAR (5)    NULL,
    [WORKSHOP]              NVARCHAR (4)    NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NULL,
    [PART_NO]               NVARCHAR (20)   NOT NULL,
    [PART_CNAME]            NVARCHAR (100)  NULL,
    [PART_ENAME]            NVARCHAR (100)  NULL,
    [PART_NICKNAME]         NVARCHAR (50)   NULL,
    [PART_UNITS]            NVARCHAR (20)   NULL,
    [INHOUSE_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INHOUSE_PACKAGE]       INT             NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INBOUND_PACKAGE]       INT             NULL,
    [PACKAGE_MODEL]         NVARCHAR (30)   NULL,
    [PACKAGE]               INT             NULL,
    [LOGICAL_PK]            NVARCHAR (50)   NULL,
    [DELETE_FLAG]           INT             NULL,
    [ROUTE]                 NVARCHAR (50)   NULL,
    [WM_NO]                 NVARCHAR (10)   NOT NULL,
    [ZONE_NO]               NVARCHAR (20)   NOT NULL,
    [OCCUPY_AREA]           NUMERIC (18, 2) NULL,
    [DLOC]                  NVARCHAR (20)   NULL,
    [MAX]                   NUMERIC (18, 2) NULL,
    [MIN]                   NUMERIC (18, 2) NULL,
    [ROW_NUMBER]            INT             NULL,
    [LINE_NUMBER]           INT             NULL,
    [HIGH_NUMBER]           INT             NULL,
    [MATERIAL_GROUP]        NVARCHAR (100)  NULL,
    [KEEPER]                NVARCHAR (50)   NULL,
    [TRANSER]               NVARCHAR (50)   NULL,
    [INFORMATIONER]         NVARCHAR (50)   NULL,
    [ELOC]                  NVARCHAR (50)   NULL,
    [SAFE_STOCK]            INT             NULL,
    [STOCKS]                NUMERIC (18, 2) NULL,
    [FROZEN_STOCKS]         NUMERIC (18, 2) NULL,
    [AVAILBLE_STOCKS]       NUMERIC (18, 2) NULL,
    [IS_BATCH]              INT             CONSTRAINT [DF_TM_BAS_PARTS_STOCK_IS_BATCH] DEFAULT ((0)) NULL,
    [WMS_RULE]              NVARCHAR (20)   NULL,
    [COUNTER]               NUMERIC (18, 2) NULL,
    [FRAGMENT_NUM]          NUMERIC (18, 2) NULL,
    [PART_WEIGHT]           NUMERIC (18, 2) NULL,
    [PART_CLS]              NVARCHAR (50)   NULL,
    [IS_REPACK]             INT             NULL,
    [REPACK_ROUTE]          NVARCHAR (40)   NULL,
    [IS_TRIGGER_PULL]       INT             NULL,
    [TRIGGER_WM_NO]         NVARCHAR (10)   NULL,
    [TRIGGER_ZONE_NO]       NVARCHAR (30)   NULL,
    [TRIGGER_DLOC]          NVARCHAR (30)   NULL,
    [EMG_TIME]              INT             NULL,
    [SUPPER_ZONE_DLOC]      NVARCHAR (50)   NULL,
    [CHECK_TYPE]            INT             NULL,
    [BUSINESS_PK]           NVARCHAR (50)   NULL,
    [REPACKAGE_AMOUNT]      INT             NULL,
    [PICKUP_ROUTE]          NVARCHAR (50)   NULL,
    [SHELF_ROUTE]           NVARCHAR (50)   NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [CREATE_USER]           NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]           DATETIME        NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    [TRAY_OUT_ISALL]        INT             NULL,
    [IS_CREATE_TASK]        INT             NULL,
    [TRAY_PACKAGE_MODEL]    INT             NULL,
    [TRAY_MODEL]            NVARCHAR (32)   NULL,
    [BATCH_COMMEND_WAY]     INT             NULL,
    [BACK_STOCK_RULE]       INT             NULL,
    [PART_CLASSIFY_AREA_NO] NVARCHAR (8)    NULL,
    CONSTRAINT [IDX_PK_PARTS_STOCKS_STOCK_IDENTITY] PRIMARY KEY NONCLUSTERED ([STOCK_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件分类区域编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_CLASSIFY_AREA_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'回库规则', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'BACK_STOCK_RULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批次推荐方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'BATCH_COMMEND_WAY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标准托盘箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'TRAY_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标准托盘型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'TRAY_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否车载作业', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'IS_CREATE_TASK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否整托出库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'TRAY_OUT_ISALL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'投棚路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'SHELF_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拣选路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PICKUP_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包拉动量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'REPACKAGE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CHECK_TYPE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'CHECK_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'超市区库位号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'SUPPER_ZONE_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'紧急响应时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'EMG_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'层级拉动库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'TRIGGER_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'层级拉动存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'TRIGGER_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'层级拉动仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'TRIGGER_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否触发层级拉动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'IS_TRIGGER_PULL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'REPACK_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否翻包', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'IS_REPACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类别', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单件重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计数器', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'COUNTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'WMS_RULE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'WMS_RULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否批次管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'IS_BATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'可用库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'AVAILBLE_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'冻结库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'FROZEN_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安全库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'SAFE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ELOC', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'ELOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INFORMATIONER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'INFORMATIONER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TRANSER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'TRANSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'保管员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MATERIAL_GROUP', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'MATERIAL_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'层号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'HIGH_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'列号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'LINE_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'行号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'ROW_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'MIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最大库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'MAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'OCCUPY_AREA', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'OCCUPY_AREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'删除标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'逻辑主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标准包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标准包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件计量单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_UNITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件简码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件英文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PLANT';

