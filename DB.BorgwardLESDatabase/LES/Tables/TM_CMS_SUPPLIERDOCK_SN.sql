CREATE TABLE [LES].[TM_CMS_SUPPLIERDOCK_SN] (
    [PLANT]            NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]    NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]       NVARCHAR (5)   NULL,
    [WORKSHOP]         NVARCHAR (4)   NULL,
    [DOCK]             NVARCHAR (20)  NOT NULL,
    [LINE_SN]          INT            NULL,
    [PRIORITY_LINE_SN] INT            NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [CREATE_USER]      NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]      DATETIME       NOT NULL,
    [UPDATE_USER]      NVARCHAR (50)  NULL,
    [UPDATE_DATE]      DATETIME       NULL,
    CONSTRAINT [IDX_PK_SUPPLIER_SN_PLANT_ASSEMBLY_LINE_SUPPLIER_NUM_RACK1] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [DOCK] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '优先排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'PRIORITY_LINE_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'LINE_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '卸货区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_CMS  供应商排队序号表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMS_SUPPLIERDOCK_SN';

