CREATE TABLE [LES].[TE_BAS_WAREHOUSE_TEMP] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]          NVARCHAR (5)   NULL,
    [WAREHOUSE]      NVARCHAR (10)  NULL,
    [WAREHOUSE_NAME] NVARCHAR (100) NULL,
    [COMMENTS]       NVARCHAR (200) NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [CREATE_DATE]    DATETIME       NULL,
    [CREATE_USER]    NVARCHAR (50)  NULL,
    [ERROR_MSG]      NVARCHAR (200) NULL,
    [VALID_FLAG]     INT            NULL,
    CONSTRAINT [PK_TE_BAS_WAREHOUSE_TEMP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'WAREHOUSE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？收货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'WAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_WAREHOUSE_TEMP', @level2type = N'COLUMN', @level2name = N'ID';

