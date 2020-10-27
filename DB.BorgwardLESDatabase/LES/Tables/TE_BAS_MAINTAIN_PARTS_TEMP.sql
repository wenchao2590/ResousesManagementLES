CREATE TABLE [LES].[TE_BAS_MAINTAIN_PARTS_TEMP] (
    [ID]                    INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (20)   NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)   NULL,
    [PLANT_ZONE]            NVARCHAR (5)    NULL,
    [WORKSHOP]              NVARCHAR (4)    NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NULL,
    [PART_NO]               NVARCHAR (50)   NOT NULL,
    [PART_CNAME]            NVARCHAR (100)  NULL,
    [PART_ENAME]            NVARCHAR (100)  NULL,
    [PART_NICKNAME]         NVARCHAR (50)   NULL,
    [PART_UNITS]            NVARCHAR (20)   NULL,
    [PART_WEIGHT]           NUMERIC (18, 2) NULL,
    [PART_CLS]              NVARCHAR (50)   NULL,
    [PART_STATE]            NVARCHAR (50)   NULL,
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
    [DELETE_FLAG]           NVARCHAR (50)   NULL,
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
    [ERROR_MSG]             NVARCHAR (MAX)  NULL,
    [VALID_FLAG]            INT             NULL,
    CONSTRAINT [IDX_PK_TE_MAINTAIN_PARTS_ID] PRIMARY KEY NONCLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车厢尺寸', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'TRAN_SIZES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '承运类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流路线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供货方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLY_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '采购方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PURCHASE_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '删除标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓储配送费', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'STOCK_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'TRANS_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PACKAGE_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车厢尺寸', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'TRUCK_SIZE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'TRANS_TRUCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'TRANS_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '对应信息员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'INFO_PERSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '对应配送员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PULL_PERSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件分类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '再订购点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'REORDER_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流提前期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'ORDER_ADVANCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订货批量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'ORDER_BATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MRP组', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'MRP_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MRP控制', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'MRP_CONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MRP类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'MRP_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标准包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标准包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '福田采购员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_PURCHASER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料工程师', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_ENGINEER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类别', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_UNITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件信息临时表(excel导入)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_MAINTAIN_PARTS_TEMP';

