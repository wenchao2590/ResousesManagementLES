CREATE TABLE [LES].[TT_WMM_OUTBOUNDDELIVERYRETURN] (
    [OUTBOUNDDELIVERYRETURN_ID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [OUTBOUNDDELIVERYRETURN_NO] NVARCHAR (50)  NOT NULL,
    [RETURN_TYPE]               INT            NULL,
    [PLANT]                     NVARCHAR (5)   NULL,
    [WM_NO]                     NVARCHAR (10)  NULL,
    [ZONE_NO]                   NVARCHAR (20)  NULL,
    [DOCK]                      NVARCHAR (10)  NULL,
    [SUPPLIER_NUM]              NVARCHAR (16)  NULL,
    [SUPPLIER_NAME]             NVARCHAR (60)  NULL,
    [SUPPLIER_ADDRESS]          NVARCHAR (60)  NULL,
    [TRANS_SUPPLIER_NUM]        NVARCHAR (20)  NULL,
    [RETURN_COMPANY_NUM]        NVARCHAR (10)  NULL,
    [RETURN_COMPANY_NAME]       NVARCHAR (60)  NULL,
    [RETURN_COMPANY_ADDRESS]    NVARCHAR (60)  NULL,
    [RETURNER]                  NVARCHAR (40)  NULL,
    [PHONENUM]                  NVARCHAR (30)  NULL,
    [TRAN_TIME]                 DATETIME       NULL,
    [EXPECT_ARRIVAL_TIME]       DATETIME       NULL,
    [ACTUAL_ARRIVAL_TIME]       DATETIME       NULL,
    [CONFIRM_FLAG]              INT            NULL,
    [OPRTR]                     NVARCHAR (50)  NULL,
    [ERP_FLAG]                  INT            NOT NULL,
    [COMMENTS]                  NVARCHAR (200) NULL,
    [CREATE_USER]               NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]               DATETIME       NOT NULL,
    [UPDATE_USER]               NVARCHAR (50)  NULL,
    [UPDATE_DATE]               DATETIME       NULL,
    CONSTRAINT [PK_TT_WMM_OUTBOUNDDELIVERYRETU] PRIMARY KEY CLUSTERED ([OUTBOUNDDELIVERYRETURN_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)收货操作人员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'OPRTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)确认标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实际到送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'ACTUAL_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'EXPECT_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)交易时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'PHONENUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'RETURNER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'RETURN_COMPANY_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'RETURN_COMPANY_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'RETURN_COMPANY_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)退货类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'RETURN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'OUTBOUNDDELIVERYRETURN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTBOUNDDELIVERYRETURN', @level2type = N'COLUMN', @level2name = N'OUTBOUNDDELIVERYRETURN_ID';

