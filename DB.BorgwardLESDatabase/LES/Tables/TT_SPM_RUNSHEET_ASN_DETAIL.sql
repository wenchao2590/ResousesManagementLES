CREATE TABLE [LES].[TT_SPM_RUNSHEET_ASN_DETAIL] (
    [ID]                           BIGINT         IDENTITY (1, 1) NOT NULL,
    [ASN_ID]                       BIGINT         NULL,
    [ASN_NO]                       NVARCHAR (32)  NULL,
    [TWD_RUNSHEET_NO]              NVARCHAR (32)  NULL,
    [TWD_RUNSHEET_SN]              INT            NULL,
    [RUNSHEET_DETAIL_ID]           INT            NULL,
    [PLANT]                        NVARCHAR (8)   NULL,
    [ASSEMBLY_LINE]                NVARCHAR (16)  NULL,
    [SUPPLIER_NUM]                 NVARCHAR (16)  NULL,
    [PART_NO]                      NVARCHAR (32)  NULL,
    [IDENTIFY_PART_NO]             NVARCHAR (32)  NULL,
    [PART_CNAME]                   NVARCHAR (512) NULL,
    [PART_ENAME]                   NVARCHAR (512) NULL,
    [DOCK]                         NVARCHAR (16)  NULL,
    [DELIVERY_LOCATION]            NVARCHAR (64)  NULL,
    [BOX_PARTS]                    NVARCHAR (16)  NULL,
    [INBOUND_PACKAGE]              INT            NULL,
    [MEASURING_UNIT_NO]            NVARCHAR (8)   NULL,
    [INBOUND_PACKAGE_MODEL]        NVARCHAR (32)  NULL,
    [PACK_COUNT]                   INT            NULL,
    [REQUIRED_INBOUND_PACKAGE]     INT            NULL,
    [REQUIRED_INBOUND_PACKAGE_QTY] INT            NULL,
    [ACTUAL_INBOUND_PACKAGE]       INT            NULL,
    [ACTUAL_INBOUND_PACKAGE_QTY]   INT            NULL,
    [STATUS]                       INT            NULL,
    [VALID_FLAG]                   BIT            NULL,
    [CREATE_DATE]                  DATETIME       NOT NULL,
    [CREATE_USER]                  NVARCHAR (64)  NOT NULL,
    [MODIFY_DATE]                  DATETIME       NULL,
    [MODIFY_USER]                  NVARCHAR (64)  NULL,
    [PRINT_STATE]                  INT            NULL,
    [PRINT_SUPPLEMENT]             INT            NULL,
    [PRINT_TIMES]                  INT            NULL,
    [Z_SERIAL]                     NVARCHAR (32)  NULL,
    CONSTRAINT [PK_TT_SPM_RUNSHEET_ASN_DETAIL] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'唯一码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'Z_SERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'PRINT_TIMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否补打', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'PRINT_SUPPLEMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'PRINT_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际入库件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际入库箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库标准包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计量单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库单包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件英文描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件中文描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ASN单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_ASN_DETAIL', @level2type = N'COLUMN', @level2name = N'ASN_NO';

