CREATE TABLE [LES].[TM_BAS_PLANT_ZONE] (
    [PLANT]               NVARCHAR (5)   NOT NULL,
    [PLANT_ZONE]          NVARCHAR (5)   NOT NULL,
    [PLANT_ZONE_NAME]     NVARCHAR (100) NOT NULL,
    [PLANT_ZONE_NICKNAME] NVARCHAR (20)  NULL,
    [ADDRESS]             NVARCHAR (200) NULL,
    [CREATE_USER]         NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]         DATETIME       NOT NULL,
    [UPDATE_USER]         NVARCHAR (50)  NULL,
    [UPDATE_DATE]         DATETIME       NULL,
    CONSTRAINT [IDX_PK_PLANT_ZONE_PLANT_PLANT_ZONE] PRIMARY KEY CLUSTERED ([PLANT] ASC, [PLANT_ZONE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '厂区缩写', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '厂区名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_PLANT_ZONE';

