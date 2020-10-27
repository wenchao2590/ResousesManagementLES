CREATE TABLE [LES].[TM_BAS_LOCATION_COORDINATE] (
    [PLANT]         NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10) NOT NULL,
    [LOCATION]      NVARCHAR (20) NOT NULL,
    [PLANT_ZONE]    NVARCHAR (5)  NULL,
    [WORKSHOP]      NVARCHAR (4)  NULL,
    [LOCATION_TYPE] INT           NOT NULL,
    [REGION]        NVARCHAR (20) NULL,
    [X]             INT           NULL,
    [Y]             INT           NULL,
    [DIRECTION]     INT           NULL,
    [CREATE_USER]   NVARCHAR (50) NOT NULL,
    [CREATE_DATE]   DATETIME      NOT NULL,
    [UPDATE_USER]   NVARCHAR (50) NULL,
    [UPDATE_DATE]   DATETIME      NULL,
    CONSTRAINT [IDX_PK_LOCATION_PLANT_ASSEMBLY_LINE_LOCATION_COORDINATE] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [LOCATION] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '方向', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'DIRECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Y坐标', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'Y';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'X坐标', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'X';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工段', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'REGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'LOCATION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位坐标表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_LOCATION_COORDINATE';

