CREATE TABLE [LES].[TT_BAS_RECKONING_SHEETS] (
    [RECKONING_SN]          INT            NOT NULL,
    [RECKONING_NO]          NVARCHAR (30)  NULL,
    [DELIVERY_ORDER]        NVARCHAR (30)  NULL,
    [ORDER_NO]              NVARCHAR (30)  NULL,
    [RECEIVE_LOCATION]      NVARCHAR (30)  NULL,
    [SUPPLIER_NUM]          NVARCHAR (8)   NOT NULL,
    [WMS_SEND_STATUS]       INT            NULL,
    [WMS_SEND_TIME]         DATETIME       NULL,
    [MODEL_NO]              NVARCHAR (8)   NULL,
    [RECKONING_TYPE]        INT            NULL,
    [SUPPLY_STATUS]         INT            NULL,
    [SUPPLY_CONFIRM_DATE]   DATETIME       NULL,
    [FIRST_CONFIRM_STATUS]  INT            NULL,
    [FIRST_CONFIRM_DATE]    DATETIME       NULL,
    [SECOND_CONFIRM_STATUS] INT            NULL,
    [SECOND_CONFIRM_DATE]   DATETIME       NULL,
    [RUNSHEET_NOS]          NVARCHAR (500) NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TT_BAS_RECKONING_SHEETS] PRIMARY KEY CLUSTERED ([RECKONING_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)SVW确认时间2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SECOND_CONFIRM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)SVW第二次确认', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SECOND_CONFIRM_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)SVW确认时间1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'FIRST_CONFIRM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)SVW第一次确认', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'FIRST_CONFIRM_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商确认时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SUPPLY_CONFIRM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SUPPLY_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RECKONING_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)WMS发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)收货点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RECEIVE_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)提货单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'DELIVERY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)结算单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RECKONING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)结算报表号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_SHEETS', @level2type = N'COLUMN', @level2name = N'RECKONING_SN';

