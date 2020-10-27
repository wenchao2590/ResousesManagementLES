CREATE TABLE [LES].[TM_TWD_BOX_PARTS] (
    [PLANT]                       NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]               NVARCHAR (10)  NOT NULL,
    [BOX_PARTS]                   NVARCHAR (10)  NOT NULL,
    [BOX_PARTS_NAME]              NVARCHAR (100) NULL,
    [PLANT_ZONE]                  NVARCHAR (5)   NULL,
    [WORKSHOP]                    NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]                NVARCHAR (12)  NOT NULL,
    [CACULATE_TYPE]               INT            NULL,
    [DOCK]                        NVARCHAR (10)  NULL,
    [VEHICLE_STATUS]              NVARCHAR (15)  NULL,
    [DCP_POINT]                   NVARCHAR (15)  NULL,
    [BOX_PARTS_STATE]             INT            NULL,
    [TRANS_SUPPLIER_NUM]          NVARCHAR (20)  NULL,
    [TRANSPORT_TIME]              INT            NULL,
    [REQUIREMENT_ACCUMULATE_TIME] INT            NULL,
    [LOAD_TIME]                   INT            NULL,
    [DELAY_TIME]                  INT            NOT NULL,
    [UNLOAD_TIME]                 INT            NULL,
    [ONLINE_TIME]                 INT            NULL,
    [PLACE_OF_DELIVERY]           NVARCHAR (100) NULL,
    [IS_ORGANIZE_SHEET]           INT            NULL,
    [WAREHOUSE]                   NVARCHAR (50)  NULL,
    [COMMENTS]                    NVARCHAR (200) NULL,
    [CREATE_USER]                 NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]                 DATETIME       NOT NULL,
    [UPDATE_USER]                 NVARCHAR (50)  NULL,
    [UPDATE_DATE]                 DATETIME       NULL,
    [ROUTE]                       NVARCHAR (10)  NULL,
    [PULL_MODE]                   INT            NULL,
    [S_WM_NO]                     NVARCHAR (10)  NULL,
    [S_ZONE_NO]                   NVARCHAR (20)  NULL,
    [ZONE_NO]                     NVARCHAR (20)  NULL,
    [IS_TRAY]                     INT            CONSTRAINT [DF_TM_TWD_BOX_PARTS_IS_SET] DEFAULT ((0)) NULL,
    [IS_SPS]                      INT            CONSTRAINT [DF_TM_TWD_BOX_PARTS_IS_SPS] DEFAULT ((0)) NULL,
    CONSTRAINT [IDX_PK_BOX_PARTS_PLANT_ASSEMBLY_LINE_BOX_PARTS] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BOX_PARTS] ASC, [SUPPLIER_NUM] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TM_TWD_BOX_PARTS]
    ON [LES].[TM_TWD_BOX_PARTS]([BOX_PARTS_STATE] ASC, [PULL_MODE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否SPS层级拉动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'IS_SPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'WAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否组织拉动单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'IS_ORGANIZE_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLACE_OF_DELIVERY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '取货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ONLINE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '卸货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'UNLOAD_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '延迟时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'DELAY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '装货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'LOAD_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求累积时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'REQUIREMENT_ACCUMULATE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'TRANSPORT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DCP点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_状态点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'VEHICLE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计算类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'CACULATE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_TWD 零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_BOX_PARTS';

