CREATE TABLE [LES].[TI_BAS_SUPPLIER_SOURCE_LIST_SAP] (
    [SOURCE_LIST_ID]       INT            IDENTITY (1, 1) NOT NULL,
    [PART_NO]              NVARCHAR (20)  NULL,
    [SUPPLIER_NUM]         NVARCHAR (12)  NULL,
    [PLANT]                NVARCHAR (5)   NULL,
    [START_EFFECTIVE_DATE] NVARCHAR (8)   NULL,
    [END_EFFECTIVE_DATE]   NVARCHAR (8)   NULL,
    [BUYER]                NVARCHAR (10)  NULL,
    [AGREEMENT_NO]         NVARCHAR (20)  NULL,
    [PROJECT]              NVARCHAR (20)  NULL,
    [FIX_AGREEMENT_TERMS]  NVARCHAR (5)   NULL,
    [FREEZE_SUPPLY_SOURCE] NVARCHAR (5)   NULL,
    [MRP]                  NVARCHAR (1)   NULL,
    [COMMENTS]             NVARCHAR (200) NULL,
    [UPDATE_DATE]          DATETIME       NULL,
    [UPDATE_USER]          NVARCHAR (50)  NULL,
    [CREATE_DATE]          DATETIME       NOT NULL,
    [CREATE_USER]          NVARCHAR (50)  NOT NULL,
    [LOEKZ]                NVARCHAR (1)   NULL,
    CONSTRAINT [IDX_PK_SUPPLIER_SOURCE_LIST_SOURCE_LIST_ID] PRIMARY KEY CLUSTERED ([SOURCE_LIST_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'LOEKZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)MRP', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'MRP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)冻结的供货源', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'FREEZE_SUPPLY_SOURCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)固定协议条款', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'FIX_AGREEMENT_TERMS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'PROJECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)协议号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'AGREEMENT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)采购组织', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'BUYER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)结束有效期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'END_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)起始有效期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)PLANT', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_SOURCE_LIST_SAP', @level2type = N'COLUMN', @level2name = N'SOURCE_LIST_ID';

