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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'DESCRIPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工段名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工段代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_WORKSHOP_SECTION', @level2type = N'COLUMN', @level2name = N'PLANT';

