CREATE TABLE [LES].[TM_TWD_SUPPLY_BOX_PARTS] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]           NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)  NOT NULL,
    [BOX_PARTS]       NVARCHAR (10)  NOT NULL,
    [BOX_PARTS_NAME]  NVARCHAR (100) NULL,
    [PLANT_ZONE]      NVARCHAR (5)   NULL,
    [WORKSHOP]        NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]    NVARCHAR (12)  NOT NULL,
    [BOX_PARTS_STATE] INT            NULL,
    [SERVICE_TYPE]    INT            NULL,
    [COMMENTS]        NVARCHAR (200) NULL,
    [CREATE_USER]     NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]     DATETIME       NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)  NULL,
    [UPDATE_DATE]     DATETIME       NULL,
    CONSTRAINT [IDX_PK_SUPPLY_BOX_PARTS_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'SERVICE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_TWD 服务商与零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLY_BOX_PARTS';

