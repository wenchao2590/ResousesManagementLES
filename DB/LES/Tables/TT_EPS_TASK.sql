CREATE TABLE [LES].[TT_EPS_TASK] (
    [TASK_SN]                 INT            NOT NULL,
    [TASK_TIME]               DATETIME       NOT NULL,
    [TASK_STATUS]             INT            NOT NULL,
    [TASK_PRIORITY]           INT            NOT NULL,
    [PLANT]                   NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]           NVARCHAR (10)  NOT NULL,
    [LOCATION]                NVARCHAR (20)  NOT NULL,
    [PACKAGE]                 INT            NULL,
    [USAGE]                   INT            NULL,
    [E_LOCATION]              VARCHAR (20)   NULL,
    [E_ASSEMBLY_LINE]         VARCHAR (10)   NULL,
    [E_PLANT]                 VARCHAR (5)    NULL,
    [D_LOCATION]              VARCHAR (20)   NULL,
    [D_ASSEMBLY_LINE]         VARCHAR (10)   NULL,
    [D_PLANT]                 VARCHAR (5)    NULL,
    [PART_NO]                 NVARCHAR (20)  NOT NULL,
    [REQUEST_TIME]            DATETIME       NOT NULL,
    [DELIVER_TIME]            DATETIME       NOT NULL,
    [ALARM_TIME]              DATETIME       NOT NULL,
    [ACTUAL_QUANTITY]         INT            NOT NULL,
    [ROUTE]                   NVARCHAR (10)  NULL,
    [ROUTE_COMBINATION_LIMIT] INT            NULL,
    [ZONE]                    NVARCHAR (10)  NULL,
    [ZONE_SCHEDULE_TYPE]      INT            NULL,
    [TRIGGER_STATUS]          INT            NULL,
    [COMBINATION_TYPE]        INT            NULL,
    [PULL_TYPE]               INT            NULL,
    [COMPULSORY_FLAG]         INT            NULL,
    [MIN_STORAGE]             INT            NULL,
    [MAX_STORAGE]             INT            NULL,
    [CURRENT_STORAGE]         INT            NULL,
    [BUTTON_ID]               NVARCHAR (20)  NULL,
    [TAG_REQUEST_SN]          INT            NULL,
    [SESSION_SN]              INT            NULL,
    [SCREEN_LOCATION]         NVARCHAR (100) NULL,
    [BAHNHOF_NO]              NVARCHAR (100) NULL,
    [DOLLY]                   NVARCHAR (100) NULL,
    [WMS_SEND_TIME]           DATETIME       NULL,
    [WMS_SEND_STATUS]         INT            NULL,
    [SUPPLIER_NUM]            NVARCHAR (12)  NULL,
    [TRANS_SUPPLIER_NUM]      NVARCHAR (20)  NULL,
    [INHOUSE_PACKAGE_MODEL]   NVARCHAR (30)  NULL,
    [INHOUSE_PACKAGE]         INT            NULL,
    [PICKUP_TIME]             DATETIME       NULL,
    [ARRIVE_TIME]             DATETIME       NULL,
    [COMMENTS]                NVARCHAR (200) NULL,
    [CREATE_USER]             NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]             DATETIME       NOT NULL,
    [UPDATE_USER]             NVARCHAR (50)  NULL,
    [UPDATE_DATE]             DATETIME       NULL,
    [PLANT_ZONE]              NVARCHAR (10)  NULL,
    [RUNSHEET_NO]             NVARCHAR (22)  NULL,
    [PRINT_TIMES]             INT            DEFAULT ((0)) NOT NULL,
    [PRINT_STATE]             INT            DEFAULT ((0)) NOT NULL,
    CONSTRAINT [IDX_PK_TASK_TASK_SN] PRIMARY KEY CLUSTERED ([TASK_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'PRINT_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'ARRIVE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'捡选时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'PICKUP_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'WMS发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DOLLY 型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'DOLLY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Bahnhof编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'BAHNHOF_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示屏位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'SCREEN_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'会话ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'SESSION_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'请求序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'TAG_REQUEST_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_按钮号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'BUTTON_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'CURRENT_STORAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最大数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'MAX_STORAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'MIN_STORAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'强制标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'COMPULSORY_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'PULL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'组合类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'COMBINATION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'触发状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'TRIGGER_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'调度类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'ZONE_SCHEDULE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_送货区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货限制', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'ROUTE_COMBINATION_LIMIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'ACTUAL_QUANTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报警时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'ALARM_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'DELIVER_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'请求时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'REQUEST_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'源工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'D_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'源流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'D_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'源位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'D_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'空箱返回工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'E_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'空箱返回流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'E_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'空箱返回位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'E_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'USAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务优先级', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'TASK_PRIORITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'TASK_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'生成任务时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'TASK_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'任务流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK', @level2type = N'COLUMN', @level2name = N'TASK_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_EPS 生成拉动任务表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_TASK';

