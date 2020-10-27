CREATE TABLE [LES].[TT_TWD_RECKONING_SHEETS] (
    [RECKONING_SN]          INT             NOT NULL,
    [TWD_RUNSHEET_SN]       INT             NULL,
    [RECKONING_NO]          NVARCHAR (30)   NULL,
    [DELIVERY_ORDER]        NVARCHAR (30)   NULL,
    [ORDER_NO]              NVARCHAR (30)   NULL,
    [RECEIVE_LOCATION]      NVARCHAR (30)   NULL,
    [SUPPLIER_NUM]          NVARCHAR (8)    NOT NULL,
    [SUPPLIER_TRANS]        NVARCHAR (100)  NULL,
    [WMS_SEND_STATUS]       INT             NULL,
    [TRANS_TYPE]            NVARCHAR (30)   NULL,
    [WMS_SEND_TIME]         DATETIME        NULL,
    [UNLOAD_PLACE]          NVARCHAR (50)   NULL,
    [LOAD_PLACE]            NVARCHAR (50)   NULL,
    [PULL_TYPE]             NVARCHAR (10)   NULL,
    [PULL_DETAIL]           NVARCHAR (2000) NULL,
    [RECEIVED_DATE]         DATETIME        NULL,
    [SUPPLY_STATUS]         INT             NULL,
    [SUPPLY_CONFIRM_DATE]   DATETIME        NULL,
    [FIRST_CONFIRM_STATUS]  INT             NULL,
    [FIRST_CONFIRM_DATE]    DATETIME        NULL,
    [SECOND_CONFIRM_STATUS] INT             NULL,
    [SECOND_CONFIRM_DATE]   DATETIME        NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [CREATE_DATE]           DATETIME        NOT NULL,
    [CREATE_USER]           NVARCHAR (50)   NOT NULL,
    [PACKAGE_TYPE]          NVARCHAR (100)  NULL,
    [WHAREHOUSE]            NVARCHAR (100)  NULL,
    [INVOICENO]             NVARCHAR (30)   NULL,
    [REC_STATUS]            INT             NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME        NULL,
    [SEND_TIME]             DATETIME        NULL,
    [SEND_STATUS]           INT             NULL,
    [TWD_RUNSHEET_NO]       VARCHAR (22)    NULL,
    [PLANT]                 NVARCHAR (5)    NULL,
    [ACTUAL_ARRIVAL_TIME]   DATETIME        NULL,
    CONSTRAINT [IDX_PK_RECKONING_SHEETS_RECKONING_SN] PRIMARY KEY NONCLUSTERED ([RECKONING_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算单状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'REC_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'INVOICENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '卸货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'WHAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'PACKAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SVW确认时间2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SECOND_CONFIRM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SVW第二次确认', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SECOND_CONFIRM_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SVW确认时间1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'FIRST_CONFIRM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SVW第一次确认', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'FIRST_CONFIRM_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商确认时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SUPPLY_CONFIRM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商确认状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SUPPLY_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '送货日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RECEIVED_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单明细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'PULL_DETAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'PULL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '装货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'LOAD_PLACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '缷货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'UNLOAD_PLACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WMS发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'TRANS_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商及发货人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_TRANS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RECEIVE_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '提货单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'DELIVERY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RECKONING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算单序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RECKONING_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_TWD 送货单表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_RECKONING_SHEETS';

