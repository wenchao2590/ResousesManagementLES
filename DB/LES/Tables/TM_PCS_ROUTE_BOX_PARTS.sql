CREATE TABLE [LES].[TM_PCS_ROUTE_BOX_PARTS] (
    [PLANT]                     NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]             NVARCHAR (10)  NOT NULL,
    [BOX_PARTS]                 NVARCHAR (10)  NOT NULL,
    [BOX_PARTS_NAME]            NVARCHAR (100) NULL,
    [PLANT_ZONE]                NVARCHAR (5)   NULL,
    [WORKSHOP]                  NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]              NVARCHAR (10)  NULL,
    [TRANS_SUPPLIER_NUM]        NVARCHAR (20)  NULL,
    [ADVANCE_CONSUME_FOOTPRINT] INT            NULL,
    [DELAY_TIME]                INT            NULL,
    [TRANSPORT_TIME]            INT            NULL,
    [ONLINE_TIME]               INT            NULL,
    [IS_ORGANIZE_SHEET]         INT            NULL,
    [REGION_IDENTITY]           INT            NULL,
    [BOX_PARTS_STATE]           INT            NULL,
    [COMMENTS]                  NVARCHAR (200) NULL,
    [CREATE_USER]               NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]               DATETIME       NOT NULL,
    [UPDATE_USER]               NVARCHAR (50)  NULL,
    [UPDATE_DATE]               DATETIME       NULL,
    [ROUTE]                     NVARCHAR (10)  NULL,
    [IS_CREATE_TASK]            INT            NULL,
    CONSTRAINT [IDX_PK_BOX_PARTS_PLANT_ASSEMBLY_LINE_ROUTE_BOX_PARTS] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BOX_PARTS] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运输时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'延迟时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提前消耗节拍数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DCP扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'REGION_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否组织拉动单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'IS_ORGANIZE_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ONLINE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运输时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'TRANSPORT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'延迟时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'DELAY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'提前消耗节拍数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ADVANCE_CONSUME_FOOTPRINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_PCS零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_ROUTE_BOX_PARTS';

