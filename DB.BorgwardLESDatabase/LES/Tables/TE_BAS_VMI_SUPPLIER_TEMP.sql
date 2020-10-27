CREATE TABLE [LES].[TE_BAS_VMI_SUPPLIER_TEMP] (
    [PLANT]        NVARCHAR (5)   NULL,
    [WM_NO]        NVARCHAR (10)  NULL,
    [ZONE_NO]      NVARCHAR (20)  NULL,
    [SUPPLIER_NUM] NVARCHAR (20)  NULL,
    [COMMENTS]     NVARCHAR (400) NULL,
    [CREATE_USER]  NVARCHAR (50)  NULL,
    [CREATE_DATE]  DATETIME       NULL,
    [ERROR_MSG]    NVARCHAR (200) NULL,
    [VALID_FLAG]   INT            NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？层级拉动存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？层级拉动仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_VMI_SUPPLIER_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';

