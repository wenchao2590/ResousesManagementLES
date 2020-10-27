CREATE TABLE [LES].[TM_TWD_SUPPLIER_QUOTA_RECORD] (
    [ID]                   INT             IDENTITY (1, 1) NOT NULL,
    [PART_NO]              NVARCHAR (20)   NULL,
    [SUPPLIER_NUM]         NVARCHAR (12)   NULL,
    [PLANT]                NVARCHAR (5)    NULL,
    [START_EFFECTIVE_DATE] DATETIME        NULL,
    [END_EFFECTIVE_DATE]   DATETIME        NULL,
    [QUOTE]                NUMERIC (18, 2) NULL,
    [TOTAL_SEND]           INT             NULL,
    [NEW_SEND_DATE]        DATETIME        NULL,
    [QUOTE_TERM]           INT             NULL,
    [TOTAL_PULL]           INT             NULL,
    [CURRENT_QUOTE]        NUMERIC (18, 2) NULL,
    [QUOTE_STEP]           VARCHAR (10)    NULL,
    [COMMENTS]             NVARCHAR (200)  NULL,
    [UPDATE_DATE]          DATETIME        NULL,
    [UPDATE_USER]          NVARCHAR (50)   NULL,
    [CREATE_DATE]          DATETIME        NOT NULL,
    [CREATE_USER]          NVARCHAR (50)   NOT NULL,
    CONSTRAINT [IDX_PK_SUPPLIER_QUOTA_RECORD_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'QUOTE_STEP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'CURRENT_QUOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'TOTAL_PULL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'QUOTE_TERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'NEW_SEND_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'TOTAL_SEND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)配额', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'QUOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)结束有效期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'END_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_TWD_SUPPLIER_QUOTA_RECORD', @level2type = N'COLUMN', @level2name = N'ID';

