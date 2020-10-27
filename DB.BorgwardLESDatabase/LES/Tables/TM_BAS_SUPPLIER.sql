CREATE TABLE [LES].[TM_BAS_SUPPLIER] (
    [SUPPLIER_NUM]        VARCHAR (20)   NOT NULL,
    [DUNS]                VARCHAR (10)   NULL,
    [SUPPLIER_NAME]       NVARCHAR (100) NOT NULL,
    [SUPPLIER_SNAME]      NVARCHAR (100) NULL,
    [SUPPLIER_ADDRESS]    VARCHAR (100)  NOT NULL,
    [SUPPLIER_TYPE]       INT            NOT NULL,
    [CONTACT_NAME]        VARCHAR (100)  NULL,
    [CONTACT_TEL]         VARCHAR (100)  NULL,
    [CONTACT_FAX]         VARCHAR (100)  NULL,
    [CONTACT_MOBILE]      VARCHAR (100)  NULL,
    [CONTACT_EMAIL]       VARCHAR (100)  NULL,
    [NIGHTCONTACT_NAME]   VARCHAR (100)  NULL,
    [NIGHTCONTACT_TEL]    VARCHAR (100)  NULL,
    [NIGHTCONTACT_FAX]    VARCHAR (100)  NULL,
    [NIGHTCONTACT_MOBILE] VARCHAR (100)  NULL,
    [NIGHTCONTACT_EMAIL]  VARCHAR (100)  NULL,
    [DAYCONTACT_NAME]     VARCHAR (100)  NULL,
    [DAYCONTACT_TEL]      VARCHAR (100)  NULL,
    [DAYCONTACT_FAX]      VARCHAR (100)  NULL,
    [DAYCONTACT_MOBILE]   VARCHAR (100)  NULL,
    [DAYCONTACT_EMAIL]    VARCHAR (100)  NULL,
    [PROVINCE]            VARCHAR (100)  NULL,
    [CITY]                VARCHAR (100)  NULL,
    [SUPPLIER_GROUP]      VARCHAR (100)  NULL,
    [COMMENTS]            NVARCHAR (200) NULL,
    [CREATE_USER]         NVARCHAR (50)  NULL,
    [CREATE_DATE]         DATETIME       NOT NULL,
    [UPDATE_USER]         NVARCHAR (50)  NULL,
    [UPDATE_DATE]         DATETIME       NULL,
    [ASN_FLAG]            BIT            NULL,
    [BATCH_FLAG]          BIT            NULL,
    CONSTRAINT [IDX_PK_SUPPLIER_SUPPLIER_NUM] PRIMARY KEY CLUSTERED ([SUPPLIER_NUM] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '夜间联系人email', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '夜间联系人手机', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_MOBILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '夜间传真', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_FAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '夜间联系电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_TEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '夜间联系人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'email', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系手机', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_MOBILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '传真', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_FAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_TEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '联系人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '邓白氏码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'DUNS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER';

