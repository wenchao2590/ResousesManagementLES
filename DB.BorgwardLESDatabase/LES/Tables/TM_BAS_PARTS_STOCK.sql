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
    [TRAY_PACKAGE_MODEL]    INT             NULL,
    [TRAY_MODEL]            NVARCHAR (32)   NULL,
    [TRAY_OUT_ISALL]        INT             NULL,
    [BATCH_COMMEND_WAY]     INT             NULL,
    [BACK_STOCK_RULE]       INT             NULL,
    [PART_CLASSIFY_AREA_NO] NVARCHAR (8)    NULL,
    [IS_CREATE_TASK]        INT             NULL,
    [VALUE_SORT]            VARCHAR (10)    NULL,
    [PARTS_ATTRIBUTE]       VARCHAR (10)    NULL,
    CONSTRAINT [IDX_PK_PARTS_STOCKS_STOCK_IDENTITY] PRIMARY KEY NONCLUSTERED ([STOCK_IDENTITY] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TM_BAS_PARTS_STOCK_PARTNO]
    ON [LES].[TM_BAS_PARTS_STOCK]([PART_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [NONCLUSTERED_PLANT_PART_NO_ZONE_NO_Index, sysname,>]
    ON [LES].[TM_BAS_PARTS_STOCK]([PLANT] ASC, [PART_NO] ASC, [ZONE_NO] ASC)
    INCLUDE([PACKAGE_MODEL], [WM_NO], [DLOC]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件属性', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PARTS_ATTRIBUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'价值分类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'VALUE_SORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '总库存量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最小库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'MIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最大库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'MAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存储区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存预警 ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PARTS_STOCK';

