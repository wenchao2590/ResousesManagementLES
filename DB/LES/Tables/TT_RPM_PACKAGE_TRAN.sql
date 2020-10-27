CREATE TABLE [LES].[TT_RPM_PACKAGE_TRAN] (
    [TRAN_ID]            BIGINT         IDENTITY (1, 1) NOT NULL,
    [TRAN_NO]            NVARCHAR (50)  NULL,
    [TRAN_TYPE]          INT            NOT NULL,
    [PLANT]              NVARCHAR (5)   NOT NULL,
    [ASN_NO]             NVARCHAR (50)  NULL,
    [ASN_SN]             INT            NULL,
    [WM_NO]              NVARCHAR (10)  NOT NULL,
    [ZONE_NO]            NVARCHAR (20)  NOT NULL,
    [RECEIVE_TYPE]       INT            NULL,
    [TRAN_TIME]          DATETIME       NOT NULL,
    [ASSEMBLY_LINE]      NVARCHAR (10)  NULL,
    [PLANT_ZONE]         NVARCHAR (5)   NULL,
    [WORKSHOP]           NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]       NVARCHAR (12)  NULL,
    [TRANS_SUPPLIER_NUM] NVARCHAR (20)  NULL,
    [SUPPLIER_TYPE]      INT            NULL,
    [PACKAGE_TYPE]       INT            NOT NULL,
    [CONFIRM_FLAG]       INT            NULL,
    [BOOK_KEEPER]        NVARCHAR (100) NULL,
    [ERP_FLAG]           INT            NULL,
    [RESOURCE_ADDRESS]   NVARCHAR (200) NULL,
    [LOGICAL_PK]         NVARCHAR (50)  NULL,
    [BUSINESS_PK]        NVARCHAR (50)  NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    [CREATE_DATE]        DATETIME       NOT NULL,
    [CREATE_USER]        NVARCHAR (50)  NOT NULL,
    CONSTRAINT [IDX_PK_PACKAGE_TRAN_ID] PRIMARY KEY CLUSTERED ([TRAN_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'本地主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'来源地', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'RESOURCE_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收货员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'确认标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'PACKAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'RECEIVE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单据流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'ASN_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ASN编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'ASN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交易类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交易单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交易流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN', @level2type = N'COLUMN', @level2name = N'TRAN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_包装交易主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN';

