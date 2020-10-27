CREATE TABLE [LES].[TM_BAS_MAINTAIN_PARTS] (
    [INHOUSE_IDENTITY]      INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)    NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)   NULL,
    [PLANT_ZONE]            NVARCHAR (5)    NULL,
    [WORKSHOP]              NVARCHAR (4)    NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NULL,
    [PART_NO]               NVARCHAR (20)   NOT NULL,
    [PART_CNAME]            NVARCHAR (100)  NULL,
    [PART_ENAME]            NVARCHAR (100)  NULL,
    [PART_NICKNAME]         NVARCHAR (50)   NULL,
    [PART_UNITS]            NVARCHAR (20)   NULL,
    [PART_WEIGHT]           NUMERIC (18, 2) NULL,
    [PART_CLS]              NVARCHAR (50)   NULL,
    [PART_STATE]            INT             NULL,
    [PART_ENGINEER]         NVARCHAR (30)   NULL,
    [PART_PURCHASER]        NVARCHAR (30)   NULL,
    [DOCK]                  NVARCHAR (10)   NULL,
    [INHOUSE_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INHOUSE_PACKAGE]       INT             NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INBOUND_PACKAGE]       INT             NULL,
    [PACKAGE_MODEL]         NVARCHAR (30)   NULL,
    [PACKAGE]               INT             NULL,
    [LOGICAL_PK]            NVARCHAR (50)   NULL,
    [MRP_TYPE]              NVARCHAR (20)   NULL,
    [MRP_CONTROL]           NVARCHAR (30)   NULL,
    [MRP_GROUP]             NVARCHAR (20)   NULL,
    [ORDER_BATCH]           INT             NULL,
    [ORDER_ADVANCE]         INT             NULL,
    [REORDER_POINT]         NVARCHAR (30)   NULL,
    [PART_GROUP]            NVARCHAR (50)   NULL,
    [PULL_PERSON]           NVARCHAR (20)   NULL,
    [INFO_PERSON]           NVARCHAR (20)   NULL,
    [TRANS_STYLE]           NVARCHAR (30)   NULL,
    [TRANS_TRUCK]           NVARCHAR (50)   NULL,
    [TRUCK_SIZE]            NVARCHAR (50)   NULL,
    [PACKAGE_FEE]           NUMERIC (18, 2) NULL,
    [TRANS_FEE]             NUMERIC (18, 2) NULL,
    [STOCK_FEE]             NUMERIC (18, 2) NULL,
    [DELETE_FLAG]           INT             NULL,
    [PURCHASE_STYLE]        NVARCHAR (600)  NULL,
    [SUPPLY_STYLE]          NVARCHAR (100)  NULL,
    [ROUTE]                 NVARCHAR (50)   NULL,
    [TRAN_TYPE]             NVARCHAR (50)   NULL,
    [TRAN_SIZES]            NVARCHAR (100)  NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [CREATE_USER]           NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]           DATETIME        NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    CONSTRAINT [IDX_PK_MAINTAIN_PARTS_INHOUSE_IDENTITY] PRIMARY KEY NONCLUSTERED ([INHOUSE_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装载量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'TRAN_SIZES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流路线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLY_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'采购方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PURCHASE_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'删除标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓储配送费', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'STOCK_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运输费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'TRANS_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PACKAGE_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车厢尺寸', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'TRUCK_SIZE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运输车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'TRANS_TRUCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'TRANS_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'信息工程师', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'INFO_PERSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉料工程师', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PULL_PERSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件分类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'再订购点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'REORDER_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订货提前期(天)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'ORDER_ADVANCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订货批量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'ORDER_BATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MRP组', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'MRP_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MRP控制者', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'MRP_CONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MRP类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'MRP_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标准包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标准包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'采购工程师', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_PURCHASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物料工程师', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_ENGINEER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件分类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件重量(KG)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件计量单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_UNITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件简称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件英文描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件中文描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'生产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'自增主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_PARTS', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';

