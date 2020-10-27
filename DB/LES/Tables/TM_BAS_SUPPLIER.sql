CREATE TABLE [LES].[TM_BAS_SUPPLIER] (
    [SUPPLIER_NUM]        VARCHAR (50)   NOT NULL,
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
    [ASN_FLAG]            INT            NULL,
    [BATCH_FLAG]          BIT            NULL,
    [ASN_VMI_FLAG]        BIT            NULL,
    CONSTRAINT [IDX_PK_SUPPLIER_SUPPLIER_NUM] PRIMARY KEY CLUSTERED ([SUPPLIER_NUM] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建生产批次标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'BATCH_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否启用ASN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'ASN_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商组', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_GROUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'城市', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'省份代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'PROVINCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'白天联系人邮箱', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'DAYCONTACT_EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'白天联系人手机', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'DAYCONTACT_MOBILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'白天联系人传真', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'DAYCONTACT_FAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'白天联系人电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'DAYCONTACT_TEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'白天联系人姓名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'DAYCONTACT_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'夜间联系人邮箱', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'夜间联系人手机', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_MOBILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'夜间联系人传真', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_FAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'夜间联系人电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_TEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'夜间联系人姓名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'NIGHTCONTACT_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人邮箱', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_EMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人手机', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_MOBILE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人传真', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_FAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人电话', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_TEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'联系人姓名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CONTACT_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商简称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'邓氏编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'DUNS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';

