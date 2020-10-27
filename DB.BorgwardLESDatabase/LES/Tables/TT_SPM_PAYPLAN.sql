CREATE TABLE [LES].[TT_SPM_PAYPLAN] (
    [ID]                 INT             IDENTITY (1, 1) NOT NULL,
    [SUPPLIERCODE]       VARCHAR (20)    NOT NULL,
    [SUPPLIERNAME]       NVARCHAR (50)   NOT NULL,
    [PLANTCODE]          NVARCHAR (50)   NOT NULL,
    [PLANTNAME]          NVARCHAR (50)   NOT NULL,
    [FINANCEMONTH]       NVARCHAR (50)   NOT NULL,
    [CURRENCY]           NVARCHAR (50)   NULL,
    [AMOUNT]             DECIMAL (18, 2) NULL,
    [NONEDISCOUNTAMOUNT] DECIMAL (18, 2) NULL,
    [PAYMODE1AMOUNT]     DECIMAL (18, 2) NULL,
    [PAYMODE2AMOUNT]     DECIMAL (18, 2) NULL,
    [AMOUNT2]            DECIMAL (18, 2) NULL,
    [USEROPERATESTATUS]  INT             CONSTRAINT [DF_TT_FC_PayPlan_UserOperateStatus] DEFAULT ((1)) NULL,
    [CHECKRESULT]        INT             CONSTRAINT [DF_TT_FC_PayPlan_CheckResult] DEFAULT ((0)) NULL,
    [REMARK]             NVARCHAR (2048) NULL,
    [READUSER]           NVARCHAR (50)   NULL,
    [READDATE]           DATETIME        NULL,
    [CHECKUSER]          NVARCHAR (50)   NULL,
    [CHECKDATE]          DATETIME        NULL,
    [ISSUEDATE]          DATETIME        NULL,
    [COMPANYCODE]        NVARCHAR (50)   NULL,
    [COMPANYNAME]        NVARCHAR (50)   NULL,
    [COMMENTS]           NVARCHAR (200)  NULL,
    [CREATE_USER]        NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]        DATETIME        NOT NULL,
    [UPDATE_USER]        NVARCHAR (50)   NULL,
    [UPDATE_DATE]        DATETIME        NULL,
    CONSTRAINT [PK_TT_FC_PayPlan] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMPANYNAME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'COMPANYNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IssueDate', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'COMPANYCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IssueDate', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'ISSUEDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)CheckDate', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'CHECKDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)CheckUser', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'CHECKUSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)阅读日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'READDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)阅读人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'READUSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)REMARK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'REMARK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)CheckResult', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'CHECKRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)UserOperateStatus', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'USEROPERATESTATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'AMOUNT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'PAYMODE2AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'PAYMODE1AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'NONEDISCOUNTAMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)CURRENCY', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'CURRENCY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)FinanceMonth', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'FINANCEMONTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'PLANTNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'PLANTCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)SupplierName', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'SUPPLIERNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)SupplierCode', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'SUPPLIERCODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_PAYPLAN', @level2type = N'COLUMN', @level2name = N'ID';

