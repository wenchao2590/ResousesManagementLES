CREATE TABLE [LES].[TI_MRP_REQUEST_LIST] (
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
    [COMMENTS]                      NVARCHAR (200)  NULL,
    [CREATE_USER]                   NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]                   DATETIME        NOT NULL,
    [UPDATE_USER]                   NVARCHAR (50)   NULL,
    [UPDATE_DATE]                   DATETIME        NULL,
    CONSTRAINT [PK_TI_MRP_REQUEST_LIST] PRIMARY KEY CLUSTERED ([CLIENT] ASC, [SEND_PORT] ASC, [IDOC] ASC, [LINE_NO] ASC, [DELIVERY_DATE] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'ACCUMULATE_UN_DELIVERY_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'UN_DELIVERY_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'CHANGE_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'DELIVERY_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'PLAN_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'DELIVERY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'LINE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IDOC号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'SEND_PORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_REQUEST_LIST', @level2type = N'COLUMN', @level2name = N'CLIENT';

