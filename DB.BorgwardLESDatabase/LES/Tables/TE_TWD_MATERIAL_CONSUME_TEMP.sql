CREATE TABLE [LES].[TE_TWD_MATERIAL_CONSUME_TEMP] (
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
    [MEASURING_UNIT_NO]     VARCHAR (8)    NULL,
    [RDC_DLOC]              NVARCHAR (50)  NULL,
    [MODEL]                 NVARCHAR (10)  NULL,
    [CARD_NO]               NVARCHAR (20)  NULL,
    [PART_SUPPLIER_NUM]     NVARCHAR (12)  NULL,
    [PROCESS_FLAG]          INT            NULL,
    [PROCESS_DATE]          DATETIME       NULL,
    [CREATE_DATE]           DATETIME       NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_MATERIAL_CONSUME_TEMP_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TE_TWD_MATERIAL_CONSUME_TEMP]
    ON [LES].[TE_TWD_MATERIAL_CONSUME_TEMP]([PROCESS_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '看板号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'CARD_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '带色标零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'INDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_TWD 物料消耗临时表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_MATERIAL_CONSUME_TEMP';

