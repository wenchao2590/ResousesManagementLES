CREATE TABLE [LES].[TM_BAS_VMI_SUPPLIER] (
    [SEQ_ID]        BIGINT         IDENTITY (1, 1) NOT NULL,
    [PLANT]         NVARCHAR (5)   NULL,
    [WM_NO]         NVARCHAR (10)  NULL,
    [ZONE_NO]       NVARCHAR (20)  NULL,
    [SUPPLIER_NUM]  NVARCHAR (20)  NULL,
    [SUPPLIER_NAME] NVARCHAR (100) NULL,
    [COMMENTS]      NVARCHAR (400) NULL,
    [CREATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    [ASN_FLAG]      BIT            NULL,
    CONSTRAINT [PK_TM_BAS_VMI_SUPPLIER] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否启用ASN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_VMI_SUPPLIER', @level2type = N'COLUMN', @level2name = N'ASN_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_VMI_SUPPLIER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_VMI_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_VMI_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_VMI_SUPPLIER', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_VMI_SUPPLIER', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_VMI_SUPPLIER', @level2type = N'COLUMN', @level2name = N'PLANT';

