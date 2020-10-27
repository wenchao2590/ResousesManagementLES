CREATE TABLE [LES].[TL_TWD_MATERIAL_CONSUME_LOG] (
    [ID]                    INT            IDENTITY (1, 1) NOT NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NULL,
    [PLANT]                 NVARCHAR (5)   NULL,
    [LOCATION]              NVARCHAR (20)  NULL,
    [PART_NO]               NVARCHAR (20)  NULL,
    [INDENTIFY_PART_NO]     NVARCHAR (20)  NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [PART_ENAME]            NVARCHAR (100) NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NULL,
    [DOCK]                  NVARCHAR (10)  NULL,
    [BOX_PARTS]             NVARCHAR (10)  NULL,
    [PACK_COUNT]            INT            NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)  NULL,
    [INBOUND_PACKAGE]       INT            NULL,
    [MEASURING_UNIT_NO]     NVARCHAR (8)   NULL,
    [RDC_DLOC]              VARCHAR (50)   NULL,
    [MODEL]                 NVARCHAR (10)  NULL,
    [DMSNO]                 NVARCHAR (50)  NULL,
    [PRODUCTION_NO]         NVARCHAR (50)  NULL,
    [PULL_MODE]             NVARCHAR (50)  NULL,
    [BILLING_FLAG]          INT            NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL,
    [PART_SUPPLIER_NUM]     NVARCHAR (12)  NULL,
    [IS_CHANGE_SUPPLIER]    INT            NULL,
    [PART_SUPPLIER_NAME]    NVARCHAR (200) NULL,
    CONSTRAINT [PK_TL_TWD_MATERIAL_CONSUME_LOG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商名称（零件供应商）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PART_SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否轮换供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'IS_CHANGE_SUPPLIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商（零件供应商）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PART_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'BILLING_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动模式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PULL_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产品线编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PRODUCTION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DMS号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'DMSNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商（零件类供应商）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'带色标零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'INDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_TWD 物料消耗日志表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TL_TWD_MATERIAL_CONSUME_LOG';

