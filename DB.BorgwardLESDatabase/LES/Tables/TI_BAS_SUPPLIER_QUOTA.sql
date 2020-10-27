CREATE TABLE [LES].[TI_BAS_SUPPLIER_QUOTA] (
    [QUOTA_ID]             INT             IDENTITY (1, 1) NOT NULL,
    [PART_NO]              NVARCHAR (20)   NULL,
    [SUPPLIER_NUM]         NVARCHAR (12)   NULL,
    [PLANT]                NVARCHAR (5)    NULL,
    [START_EFFECTIVE_DATE] DATETIME        NULL,
    [END_EFFECTIVE_DATE]   DATETIME        NULL,
    [QUOTE]                NUMERIC (18, 2) NULL,
    [COMMENTS]             NVARCHAR (200)  NULL,
    [UPDATE_DATE]          DATETIME        NULL,
    [UPDATE_USER]          NVARCHAR (50)   NULL,
    [CREATE_DATE]          DATETIME        NOT NULL,
    [CREATE_USER]          NVARCHAR (50)   NOT NULL,
    [AGREEMENT_NO]         NVARCHAR (20)   NULL,
    [PROJECT]              NVARCHAR (20)   NULL,
    [LOEKZ]                NVARCHAR (1)    NULL,
    [PROCESS_FLAG]         INT             NULL,
    CONSTRAINT [IDX_PK_SUPPLIER_QUOTA_QUOTA_ID] PRIMARY KEY CLUSTERED ([QUOTA_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '配额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'QUOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结束有效期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'END_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '起始有效期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA', @level2type = N'COLUMN', @level2name = N'QUOTA_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_供应商配额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_QUOTA';

