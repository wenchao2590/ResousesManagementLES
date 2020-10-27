CREATE TABLE [LES].[TT_JIS_RUNSHEET] (
    [JIS_RUNSHEET_SN]        INT            NOT NULL,
    [JIS_RUNSHEET_NO]        VARCHAR (30)   NOT NULL,
    [JIS_RUNSHEET_TIME]      DATETIME       NOT NULL,
    [PLANT]                  NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]          NVARCHAR (10)  NOT NULL,
    [RACK]                   NVARCHAR (20)  NOT NULL,
    [SUPPLIER_NUM]           NVARCHAR (8)   NOT NULL,
    [WORKSHOP]               NVARCHAR (4)   NULL,
    [PLANT_ZONE]             NVARCHAR (10)  NULL,
    [LOCATION]               NVARCHAR (20)  NULL,
    [JIS_SUPPLIER_SN]        INT            NOT NULL,
    [DOCK]                   NVARCHAR (10)  NOT NULL,
    [FIRST_TIME]             DATETIME       NOT NULL,
    [EXPECTED_ARRIVAL_TIME]  DATETIME       NOT NULL,
    [SUPPLIER_CONFIRM_TIME]  DATETIME       NULL,
    [ESTIMATED_ARRIVAL_TIME] DATETIME       NULL,
    [ACTUAL_ARRIVAL_TIME]    DATETIME       NULL,
    [PRINT_TYPE]             VARCHAR (2)    NOT NULL,
    [FORMAT]                 VARCHAR (6)    NOT NULL,
    [CARS]                   VARCHAR (200)  NOT NULL,
    [START_RUNNING_NO]       VARCHAR (8)    NOT NULL,
    [END_RUNNING_NO]         VARCHAR (8)    NOT NULL,
    [FEEDBACK]               VARCHAR (100)  NULL,
    [BOOKKEEPER]             VARCHAR (100)  NULL,
    [REDO_FLAG]              BIT            NULL,
    [JIS_RUNSHEET_STATUS]    INT            NOT NULL,
    [SEND_STATUS]            INT            NULL,
    [SEND_TIME]              DATETIME       NULL,
    [FAX_STATUS]             INT            NULL,
    [FAX_TIME]               DATETIME       NULL,
    [SUPPLY_STATUS]          INT            NULL,
    [SUPPLY_TIME]            DATETIME       NULL,
    [SAP_FLAG]               INT            NOT NULL,
    [RETRY_TIMES]            INT            NULL,
    [RECKONING_NO]           NVARCHAR (30)  NULL,
    [OPERATION_USER]         NVARCHAR (10)  NULL,
    [CHECK_USER]             NVARCHAR (10)  NULL,
    [TRANS_SUPPLIER_NUM]     NVARCHAR (20)  NULL,
    [WMS_SEND_STATUS]        INT            NULL,
    [WMS_SEND_TIME]          DATETIME       NULL,
    [COMMENTS]               NVARCHAR (200) NULL,
    [UPDATE_DATE]            DATETIME       NULL,
    [UPDATE_USER]            NVARCHAR (50)  NULL,
    [CREATE_DATE]            DATETIME       NOT NULL,
    [CREATE_USER]            NVARCHAR (50)  NOT NULL,
    [PRINT_TIMES]            INT            DEFAULT ((0)) NOT NULL,
    [PRINT_STATE]            INT            DEFAULT ((0)) NOT NULL,
    [RUNSHEET_TYPE]          INT            NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_JIS_RUNSHEET_SN] PRIMARY KEY CLUSTERED ([JIS_RUNSHEET_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RUNSHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PRINT_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'WMS发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检验员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CHECK_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'操作工', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'OPERATION_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RECKONING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'重试次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RETRY_TIMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SAP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLY_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'传真时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FAX_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'传真状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FAX_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'重做标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'REDO_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'做账', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'BOOKKEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商返馈信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FEEDBACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结束流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'END_RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'起始流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'START_RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车号序列', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CARS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'格式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FORMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PRINT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ACTUAL_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'建议到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ESTIMATED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商发运时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLIER_CONFIRM_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'期望到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'EXPECTED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'第一个过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FIRST_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'JIS_SUPPLIER_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_JIS 拉动单表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET';

