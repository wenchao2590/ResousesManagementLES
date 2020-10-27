CREATE TABLE [LES].[TM_PCS_REGION] (
    [REGION_IDENTITY]   INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]             NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]     NVARCHAR (10)  NOT NULL,
    [REGION_NAME]       NVARCHAR (50)  NULL,
    [PLANT_ZONE]        NVARCHAR (5)   NULL,
    [WORKSHOP]          NVARCHAR (4)   NULL,
    [REGION_SIZE]       INT            NULL,
    [REGION_ORDER]      INT            NOT NULL,
    [PERMANENT_REGION]  INT            NULL,
    [RECALCULATE_FLAG]  INT            NULL,
    [ORDER_TYPE]        NVARCHAR (2)   NULL,
    [REPLACE_DCP_POINT] NVARCHAR (50)  NULL,
    [IS_REPLACE]        BIT            NULL,
    [OFFSET]            INT            NULL,
    [COMMENTS]          NVARCHAR (200) NULL,
    [CREATE_USER]       NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]       DATETIME       NOT NULL,
    [UPDATE_USER]       NVARCHAR (50)  NULL,
    [UPDATE_DATE]       DATETIME       NULL,
    CONSTRAINT [IDX_PK_REGION_REGION_IDENTITY] PRIMARY KEY CLUSTERED ([REGION_IDENTITY] ASC, [PLANT] ASC, [ASSEMBLY_LINE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '偏移OffSet', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'OFFSET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否替换', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'IS_REPLACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '替换点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'REPLACE_DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'ORDER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '重算标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'RECALCULATE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否是分装DCP扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'PERMANENT_REGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DCP扫描点排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'REGION_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DCP扫描点区域大小', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'REGION_SIZE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DCP扫描点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'REGION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DCP扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION', @level2type = N'COLUMN', @level2name = N'REGION_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS DCP扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION';

