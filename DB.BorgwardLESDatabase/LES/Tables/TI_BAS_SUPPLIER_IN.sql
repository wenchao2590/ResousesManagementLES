CREATE TABLE [LES].[TI_BAS_SUPPLIER_IN] (
    [SEQ_ID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [LIFNR]        NVARCHAR (10)  NOT NULL,
    [NAME1]        NVARCHAR (100) NOT NULL,
    [STRAS]        NVARCHAR (100) NULL,
    [KTOKK]        NVARCHAR (6)   NULL,
    [REGIO]        NVARCHAR (10)  NULL,
    [ORT00]        NVARCHAR (35)  NULL,
    [NAME0]        VARCHAR (100)  NULL,
    [TELF0]        VARCHAR (100)  NULL,
    [SMTP_ADDR]    NVARCHAR (241) NULL,
    [PROCESS_FLAG] INT            NULL,
    [PROCESS_TIME] DATETIME       NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [CREATE_USER]  NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]  DATETIME       NOT NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    CONSTRAINT [IDX_PK_SUPPLIER] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'email', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'SMTP_ADDR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'TELF0';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'NAME0';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '城市', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'ORT00';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '省份', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'REGIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商账户组', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'KTOKK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'STRAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'NAME1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'LIFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SUPPLIER_IN';

