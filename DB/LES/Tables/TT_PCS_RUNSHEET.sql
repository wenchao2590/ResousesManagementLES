CREATE TABLE [LES].[TT_PCS_RUNSHEET] (
    [PCS_RUNSHEET_SN]       INT            NOT NULL,
    [PLANT_ZONE]            NVARCHAR (10)  NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [PCS_RUNSHEET_NO]       VARCHAR (22)   NOT NULL,
    [PUBLISH_TIME]          DATETIME       NOT NULL,
    [RUNSHEET_TYPE]         INT            NOT NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [SUPPLIER_SN]           INT            NULL,
    [DOCK]                  NVARCHAR (10)  NULL,
    [BOX_PARTS]             NVARCHAR (10)  NULL,
    [UNLOADING_TIME]        INT            NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME       NOT NULL,
    [ACTUAL_ARRIVAL_TIME]   DATETIME       NULL,
    [VERIFY_TIME]           DATETIME       NULL,
    [REJECT_REASON]         NVARCHAR (200) NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (8)   NULL,
    [FEEDBACK]              NVARCHAR (100) NULL,
    [SHEET_STATUS]          INT            NOT NULL,
    [SEND_TIME]             DATETIME       NULL,
    [SEND_STATUS]           INT            NULL,
    [CHECK_USER]            NVARCHAR (10)  NULL,
    [OPERATON_USER]         NVARCHAR (10)  NULL,
    [RETRY_TIMES]           INT            NULL,
    [SUPPLY_TIME]           DATETIME       NULL,
    [SUPPLY_STATUS]         INT            NULL,
    [FAX_TIME]              DATETIME       NULL,
    [FAX_STATUS]            INT            NULL,
    [SAP_FLAG]              INT            NOT NULL,
    [WMS_SEND_STATUS]       INT            NULL,
    [WMS_SEND_TIME]         DATETIME       NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [PRINT_TIMES]           INT            DEFAULT ((0)) NOT NULL,
    [PRINT_STATE]           INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_PCS_RUNSHEET_SN] PRIMARY KEY CLUSTERED ([PCS_RUNSHEET_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PRINT_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'WMS发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上传SAP标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SAP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'传真状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FAX_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'传真时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FAX_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLY_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'重试次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RETRY_TIMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作工', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'OPERATON_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检验员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CHECK_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SHEET_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商反馈', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FEEDBACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'退货原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'REJECT_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收货验证时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'VERIFY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际到送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ACTUAL_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'期望到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'EXPECTED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'卸货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UNLOADING_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RUNSHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'组单时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PCS_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PCS_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_PCS 拉动单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET';

