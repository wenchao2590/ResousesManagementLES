CREATE TABLE [LES].[TT_WMM_OUTBOUNDDELIVERY] (
    [OUTBOUNDDELIVERY_ID]          BIGINT         IDENTITY (1, 1) NOT NULL,
    [OUTBOUNDDELIVERY_NO]          NVARCHAR (50)  NOT NULL,
    [OUTBOUNDDELIVERY_TYPE]        INT            NULL,
    [PLANT]                        NVARCHAR (5)   NULL,
    [WM_NO]                        NVARCHAR (10)  NULL,
    [ZONE_NO]                      NVARCHAR (20)  NULL,
    [DOCK]                         NVARCHAR (10)  NULL,
    [SUPPLIER_NUM]                 NVARCHAR (16)  NULL,
    [SUPPLIER_NAME]                NVARCHAR (60)  NULL,
    [SUPPLIER_ADDRESS]             NVARCHAR (60)  NULL,
    [TRANS_SUPPLIER_NUM]           NVARCHAR (20)  NULL,
    [RECEIVE_COMPANY_NUM]          NVARCHAR (10)  NULL,
    [RECEIVE_COMPANY_NAME]         NVARCHAR (60)  NULL,
    [RECEIVE_COMPANY_ADDRESS]      NVARCHAR (60)  NULL,
    [RECEIVEER]                    NVARCHAR (40)  NULL,
    [PHONENUM]                     NVARCHAR (30)  NULL,
    [TRAN_TIME]                    DATETIME       NULL,
    [ACTUAL_OUTBOUNDDELIVERY_TIME] DATETIME       NULL,
    [EXPECT_ARRIVAL_TIME]          DATETIME       NULL,
    [ACTUAL_ARRIVAL_TIME]          DATETIME       NULL,
    [CONFIRM_FLAG]                 INT            NULL,
    [OPRTR]                        NVARCHAR (50)  NULL,
    [ERP_FLAG]                     INT            NOT NULL,
    [COMMENTS]                     NVARCHAR (200) NULL,
    [CREATE_USER]                  NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]                  DATETIME       NOT NULL,
    [UPDATE_USER]                  NVARCHAR (50)  NULL,
    [UPDATE_DATE]                  DATETIME       NULL,
    CONSTRAINT [PK_TT_WMM_OUTBOUNDDELIVERY] PRIMARY KEY CLUSTERED ([OUTBOUNDDELIVERY_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)收货操作人员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'OPRTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)确认标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)实际到送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'ACTUAL_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'EXPECT_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'ACTUAL_OUTBOUNDDELIVERY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)交易时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'PHONENUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'RECEIVEER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'RECEIVE_COMPANY_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'RECEIVE_COMPANY_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'RECEIVE_COMPANY_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)供应商地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'SUPPLIER_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)供应商名称2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)基础数据_仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'OUTBOUNDDELIVERY_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'OUTBOUNDDELIVERY_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERY', @level2type = N'COLUMN', @level2name = N'OUTBOUNDDELIVERY_ID';

