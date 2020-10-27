CREATE TABLE [LES].[TT_TWD_PACKAGE_MODEL] (
    [ID]                         INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                      NVARCHAR (5)   NULL,
    [ASSEMBLY_LINE]              NVARCHAR (10)  NULL,
    [PLANT_ZONE]                 NVARCHAR (5)   NULL,
    [WORKSHOP]                   NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]               NVARCHAR (12)  NULL,
    [INHOUSE_PACKAGE_MODEL]      NVARCHAR (30)  NULL,
    [INHOUSE_PACKAGE_MODEL_NAME] NVARCHAR (50)  NULL,
    [INHOUSE_PACKAGE]            INT            NULL,
    [INBOUND_PACKAGE_MODEL]      NVARCHAR (30)  NULL,
    [INBOUND_PACKAGE]            INT            NULL,
    [PACKAGE_WIDTH]              INT            NULL,
    [PACKAGE_HEIGHT]             INT            NULL,
    [PACKAGE_LONG]               INT            NULL,
    [PACKAGE_LAYER]              INT            NULL,
    [COMMENTS]                   NVARCHAR (200) NULL,
    [UPDATE_DATE]                DATETIME       NULL,
    [UPDATE_USER]                NVARCHAR (50)  NULL,
    [CREATE_DATE]                DATETIME       NULL,
    [CREATE_USER]                NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_PACKAGE_MODEL_ID] PRIMARY KEY NONCLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMENTS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'PACKAGE_LAYER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'PACKAGE_LONG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '高', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'PACKAGE_HEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '宽', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'PACKAGE_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_TWD包装型号表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_PACKAGE_MODEL';

