CREATE TABLE [LES].[TT_SPM_SUPPLIER_GROUP] (
    [GROUP_ID]      INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]         NVARCHAR (20)  NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (20)  NULL,
    [WORKSHOP]      NVARCHAR (20)  NULL,
    [PLANT_ZONE]    NVARCHAR (20)  NULL,
    [GROUP_NAME]    NVARCHAR (50)  NOT NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    CONSTRAINT [PK_TT_SPM_SUPPLIER_GROUP] PRIMARY KEY CLUSTERED ([GROUP_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '群组名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'GROUP_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '群组编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP', @level2type = N'COLUMN', @level2name = N'GROUP_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商群组', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_SUPPLIER_GROUP';

