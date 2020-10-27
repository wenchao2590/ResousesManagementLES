CREATE TABLE [LES].[TT_MRP_REQUEST_LIST_CONFIRM] (
    [CLIENT]                        NVARCHAR (3)    NOT NULL,
    [SEND_PORT]                     NVARCHAR (10)   NOT NULL,
    [IDOC]                          NVARCHAR (16)   NOT NULL,
    [LINE_NO]                       NVARCHAR (6)    NOT NULL,
    [DELIVERY_DATE]                 DATETIME        NOT NULL,
    [PLAN_AMOUNT]                   NUMERIC (18, 2) NULL,
    [TRANS_SUPPLIER_NUM]            NVARCHAR (20)   NOT NULL,
    [DELIVERY_AMOUNT]               NUMERIC (18, 2) NULL,
    [CHANGE_AMOUNT]                 NUMERIC (18, 2) NULL,
    [UN_DELIVERY_AMOUNT]            NUMERIC (18, 2) NULL,
    [ACCUMULATE_UN_DELIVERY_AMOUNT] NUMERIC (18, 2) NOT NULL,
    [SUPPLY_CONFIRM_AMOUNT]         NUMERIC (18, 2) NULL,
    [PLANER_CONFIRM_AMOUNT]         NUMERIC (18, 2) NULL,
    [SUPPLY_CONFIRM_DATE]           DATETIME        NULL,
    [PLANER_CONFIRM_DATE]           DATETIME        NULL,
    [CONFIRM_STATUS]                INT             NULL,
    [FREEZE_TERM]                   INT             NULL,
    [LOGICAL_PK]                    NVARCHAR (100)  NULL,
    [COMMENTS]                      NVARCHAR (200)  NULL,
    [CREATE_USER]                   NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]                   DATETIME        NOT NULL,
    [UPDATE_USER]                   NVARCHAR (50)   NULL,
    [UPDATE_DATE]                   DATETIME        NULL,
    CONSTRAINT [PK_TT_MRP_REQUEST_LIST_CONFIRM] PRIMARY KEY CLUSTERED ([CLIENT] ASC, [SEND_PORT] ASC, [IDOC] ASC, [LINE_NO] ASC, [DELIVERY_DATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'FREEZE_TERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CONFIRM_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'PLANER_CONFIRM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商确认时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'SUPPLY_CONFIRM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'PLANER_CONFIRM_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'SUPPLY_CONFIRM_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'ACCUMULATE_UN_DELIVERY_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'UN_DELIVERY_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CHANGE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'DELIVERY_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'PLAN_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'DELIVERY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'LINE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IDOC号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'SEND_PORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_REQUEST_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CLIENT';

