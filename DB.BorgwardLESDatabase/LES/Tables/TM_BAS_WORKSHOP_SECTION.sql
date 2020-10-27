CREATE TABLE [LES].[TM_BAS_WORKSHOP_SECTION] (
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [WORKSHOP_SECTION]      NVARCHAR (20)  NOT NULL,
    [WORKSHOP_SECTION_NAME] NVARCHAR (200) NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [DESCRIPTION]           NVARCHAR (200) NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    CONSTRAINT [IDX_PK_WORKSHOP_SECTION_PLANT_ASSEMBLY_LINE_WORKSHOP_SECTION] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [WORKSHOP_SECTION] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工段名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工段', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工段', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION';

