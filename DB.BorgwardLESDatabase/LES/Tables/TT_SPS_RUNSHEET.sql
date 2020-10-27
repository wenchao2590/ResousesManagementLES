CREATE TABLE [LES].[TT_SPS_RUNSHEET] (
    [SPS_RUNSHEET_SN]       INT            IDENTITY (1, 1) NOT NULL,
    [SPS_RUNSHEET_NO]       NVARCHAR (22)  NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [PLANT_ZONE]            NVARCHAR (10)  NULL,
    [PUBLISH_TIME]          DATETIME       NOT NULL,
    [RUNSHEET_TYPE]         INT            NOT NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [SUPPLIER_SN]           INT            NOT NULL,
    [DOCK]                  NVARCHAR (10)  NULL,
    [DELIVERY_LOCATION]     NVARCHAR (50)  NULL,
    [BOX_PARTS]             NVARCHAR (10)  NOT NULL,
    [PART_TYPE]             INT            NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME       NOT NULL,
    [SUGGEST_DELIVERY_TIME] DATETIME       NULL,
    [ACTUAL_ARRIVAL_TIME]   DATETIME       NULL,
    [VERIFY_TIME]           DATETIME       NULL,
    [REJECT_REASON]         NVARCHAR (200) NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (20)  NULL,
    [FEEDBACK]              NVARCHAR (100) NULL,
    [SHEET_STATUS]          INT            NOT NULL,
    [SEND_TIME]             DATETIME       NULL,
    [SEND_STATUS]           INT            NULL,
    [OPERATON_USER]         NVARCHAR (10)  NULL,
    [CHECK_USER]            NVARCHAR (10)  NULL,
    [RETRY_TIMES]           INT            NULL,
    [SUPPLY_TIME]           DATETIME       NULL,
    [SUPPLY_STATUS]         INT            NULL,
    [FAX_TIME]              DATETIME       NULL,
    [FAX_STATUS]            INT            NULL,
    [ORDER_NO]              NVARCHAR (36)  NULL,
    [VIN]                   NVARCHAR (20)  NULL,
    [PTL_SEND_TIME]         DATETIME       NULL,
    [PTL_SEND_STATUS]       INT            NULL,
    [PRINT_TIMES]           INT            DEFAULT ((0)) NOT NULL,
    [PRINT_STATE]           INT            DEFAULT ((0)) NOT NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TT_SPS_RUNSHEET] PRIMARY KEY CLUSTERED ([SPS_RUNSHEET_SN] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPS_RUNSHEET_1]
    ON [LES].[TT_SPS_RUNSHEET]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BOX_PARTS] ASC, [SUPPLIER_NUM] ASC, [PUBLISH_TIME] ASC, [RUNSHEET_TYPE] ASC, [DOCK] ASC, [SEND_STATUS] ASC, [ORDER_NO] ASC, [VIN] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPS_RUNSHEET]
    ON [LES].[TT_SPS_RUNSHEET]([SPS_RUNSHEET_NO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PRINT_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PRINT_TIMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PTL发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PTL_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PTL发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PTL_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VIN号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'传真状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FAX_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'传真时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FAX_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLY_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'重试次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RETRY_TIMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检验员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CHECK_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作工', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'OPERATON_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SHEET_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商反馈', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FEEDBACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'退货原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'REJECT_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收货验证时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'VERIFY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ACTUAL_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建议发货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUGGEST_DELIVERY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'期望到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'EXPECTED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单类型（1-正常拉动单、2-紧急拉动单、3-空白拉动单）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RUNSHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SPS_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SPS_RUNSHEET_SN';

