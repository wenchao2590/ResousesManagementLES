CREATE TABLE [LES].[TT_MRP_PARTS_LIST_CONFIRM] (
    [CLIENT]                     NVARCHAR (3)    NOT NULL,
    [SEND_PORT]                  NVARCHAR (10)   NOT NULL,
    [IDOC]                       NVARCHAR (16)   NOT NULL,
    [LINE_NO]                    NVARCHAR (6)    NOT NULL,
    [PART_NO]                    NVARCHAR (20)   NOT NULL,
    [PART_CNAME]                 NVARCHAR (100)  NULL,
    [TRANS_SUPPLIER_NUM]         NVARCHAR (20)   NULL,
    [MATERIAL_STATE]             NVARCHAR (50)   NULL,
    [PLANNING_CLERK]             NVARCHAR (20)   NULL,
    [PLANNING_CLERK_NAME]        NVARCHAR (30)   NULL,
    [PLACE_OF_DELIVERY]          NVARCHAR (16)   NULL,
    [UNIT]                       NVARCHAR (3)    NULL,
    [ACCUMULATE_PLAN_AMOUNT]     NUMERIC (18, 2) NOT NULL,
    [ACCUMULATE_DELIVERY_AMOUNT] NUMERIC (18, 2) NULL,
    [LAST_DELIVERY_AMOUNT]       NUMERIC (18, 2) NULL,
    [LAST_DELIVERY_DATE]         DATETIME        NULL,
    [LAST_IDOC]                  NVARCHAR (35)   NULL,
    [CONFIRM_STATUS]             INT             NULL,
    [INBOUND_PACKAGE_MODEL]      NVARCHAR (30)   NULL,
    [INBOUND_PACKAGE]            INT             NULL,
    [COMMENTS]                   NVARCHAR (200)  NULL,
    [CREATE_USER]                NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]                DATETIME        NOT NULL,
    [UPDATE_USER]                NVARCHAR (50)   NULL,
    [UPDATE_DATE]                DATETIME        NULL,
    CONSTRAINT [PK_TT_MRP_PARTS_LIST_CONFIRM] PRIMARY KEY CLUSTERED ([CLIENT] ASC, [SEND_PORT] ASC, [IDOC] ASC, [LINE_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂包装数量(收容数)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CONFIRM_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'LAST_IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'LAST_DELIVERY_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'LAST_DELIVERY_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'ACCUMULATE_DELIVERY_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'ACCUMULATE_PLAN_AMOUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'UNIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'PLACE_OF_DELIVERY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'PLANNING_CLERK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'PLANNING_CLERK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'MATERIAL_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'LINE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IDOC号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'SEND_PORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_MRP_PARTS_LIST_CONFIRM', @level2type = N'COLUMN', @level2name = N'CLIENT';

