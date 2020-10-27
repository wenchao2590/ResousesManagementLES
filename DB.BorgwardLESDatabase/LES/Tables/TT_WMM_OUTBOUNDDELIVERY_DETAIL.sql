CREATE TABLE [LES].[TT_WMM_OUTBOUNDDELIVERY_DETAIL] (
    [OUTBOUNDDELIVERY_DETAIL_ID]    BIGINT          IDENTITY (1, 1) NOT NULL,
    [OUTBOUNDDELIVERY_ID]           BIGINT          NULL,
    [OUTBOUNDDELIVERY_NO]           NVARCHAR (50)   NOT NULL,
    [PLANT]                         NVARCHAR (5)    NULL,
    [ASSEMBLY_LINE]                 NVARCHAR (10)   NULL,
    [WM_NO]                         NVARCHAR (10)   NULL,
    [ZONE_NO]                       NVARCHAR (20)   NULL,
    [DLOC]                          NVARCHAR (30)   NULL,
    [DOCK]                          NVARCHAR (10)   NULL,
    [SUPPLIER_NUM]                  NVARCHAR (16)   NULL,
    [MEASURING_UNIT_NO]             NVARCHAR (10)   NULL,
    [PACKAGE_MODEL]                 NVARCHAR (30)   NULL,
    [PACKAGE]                       NVARCHAR (100)  NULL,
    [NUM]                           NUMERIC (18, 2) NULL,
    [BOX_NUM]                       NUMERIC (18, 2) NULL,
    [PART_NO]                       NVARCHAR (20)   NULL,
    [IDENTIFY_PART_NO]              NVARCHAR (20)   NULL,
    [PART_CNAME]                    NVARCHAR (100)  NULL,
    [PART_ENAME]                    NVARCHAR (100)  NULL,
    [PART_TYPE]                     INT             NULL,
    [BOX_PARTS]                     NVARCHAR (10)   NULL,
    [REQUIRED_OUTBOUND_PACKAGE]     INT             NULL,
    [REQUIRED_OUTBOUND_PACKAGE_QTY] INT             NOT NULL,
    [ACTUAL_OUTBOUND_PACKAGE]       INT             NULL,
    [ACTUAL_OUTBOUND_PACKAGE_QTY]   INT             NULL,
    [CURRENT_OUTBOUND_PACKAGE]      INT             NULL,
    [CURRENT_OUTBOUND_PACKAGE_QTY]  INT             NULL,
    [PO_NO]                         NVARCHAR (10)   NULL,
    [POSNR]                         NVARCHAR (6)    NULL,
    [PACKAGE_UTENSIL]               NVARCHAR (20)   NULL,
    [CAPACITY]                      INT             NULL,
    [OPRTR]                         NVARCHAR (50)   NULL,
    [COMMENTS]                      NVARCHAR (200)  NULL,
    [CREATE_USER]                   NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]                   DATETIME        NOT NULL,
    [UPDATE_USER]                   NVARCHAR (50)   NULL,
    [UPDATE_DATE]                   DATETIME        NULL,
    CONSTRAINT [PK_TT_WMM_OUTBOUNDDELIVERY_DET] PRIMARY KEY CLUSTERED ([OUTBOUNDDELIVERY_DETAIL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)收货操作人员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'OPRTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'CAPACITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_UTENSIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)行项目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'POSNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PO_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'CURRENT_OUTBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'CURRENT_OUTBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_OUTBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_OUTBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_OUTBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_OUTBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_标识零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'IDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实收箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)标准包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)标准包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未存贮区编码找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'OUTBOUNDDELIVERY_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'OUTBOUNDDELIVERY_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY_DETAIL', @level2type = N'COLUMN', @level2name = N'OUTBOUNDDELIVERY_DETAIL_ID';

