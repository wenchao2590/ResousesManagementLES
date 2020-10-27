CREATE TABLE [LES].[TM_BAS_FINANCIAL_ACCOUNT] (
    [FINANCIAL_ID]   INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]          NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]  NVARCHAR (10)  NULL,
    [PLANT_ZONE]     NVARCHAR (200) NULL,
    [WORKSHOP]       NVARCHAR (4)   NULL,
    [FINANCIAL_CODE] NVARCHAR (100) NOT NULL,
    [FINANCIAL_NAME] NVARCHAR (200) NOT NULL,
    [COMMENTS]       NVARCHAR (200) NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [CREATE_DATE]    DATETIME       NOT NULL,
    [CREATE_USER]    NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TM_BAS_FINANCIAL_ACCOUNT] PRIMARY KEY CLUSTERED ([FINANCIAL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '财务科目名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'FINANCIAL_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '财务科目编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'FINANCIAL_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '财务科目序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT', @level2type = N'COLUMN', @level2name = N'FINANCIAL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_财务科目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_FINANCIAL_ACCOUNT';

