CREATE TABLE [LES].[TM_BAS_LOCATION] (
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NOT NULL,
    [LOCATION]      NVARCHAR (20)  NOT NULL,
    [PLANT_ZONE]    NVARCHAR (5)   NULL,
    [WORKSHOP]      NVARCHAR (4)   NULL,
    [LOCATION_TYPE] INT            NULL,
    [REGION]        NVARCHAR (20)  NULL,
    [FOOTPRINT]     INT            NULL,
    [FOOTPRINT_NO]  INT            NULL,
    [SEQUENCE_NO]   INT            NULL,
    [PICKUP_SEQ_NO] INT            NULL,
    [DCP_POINT]     NVARCHAR (15)  NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    CONSTRAINT [IDX_PK_LOCATION_PLANT_ASSEMBLY_LINE_LOCATION] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [LOCATION] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'信息点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拣料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'节拍编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'FOOTPRINT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'节拍', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'FOOTPRINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工段代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'REGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工位类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'LOCATION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工位代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION', @level2type = N'COLUMN', @level2name = N'PLANT';

