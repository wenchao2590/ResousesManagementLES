CREATE TABLE [LES].[TT_EPS_COUNTER_MATERIAL_REQUESTS] (
    [INTERFACE_ID]          INT            IDENTITY (1, 1) NOT NULL,
    [PART_NO]               VARCHAR (20)   NOT NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [LOCATION]              NVARCHAR (20)  NOT NULL,
    [MODEL]                 NVARCHAR (100) NULL,
    [DCP_POINT]             NVARCHAR (15)  NULL,
    [USAGE]                 INT            NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [INTERFACE_TYPE]        INT            NOT NULL,
    [PACKAGE]               INT            NULL,
    [SCREEN_LOCATION]       NVARCHAR (100) NULL,
    [REQUEST_TIME]          DATETIME       NOT NULL,
    [DELIVER_TIME]          DATETIME       NOT NULL,
    [ALARM_TIME]            DATETIME       NOT NULL,
    [CURRENT_PART_COUNT]    INT            NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME       NULL,
    [RDC_DLOC]              VARCHAR (20)   NULL,
    [PICKUP_SEQ_NO]         INT            NULL,
    [IS_ORGANIZE_SHEET]     INT            NULL,
    [SEND_STATUS]           INT            NULL,
    [SEND_TIME]             DATETIME       NULL,
    [IS_CANCEL]             BIT            NULL,
    [WAREHOUSE]             NVARCHAR (50)  NULL,
    [BAHNHOF_NO]            NVARCHAR (100) NULL,
    [ROUTE]                 NVARCHAR (10)  NULL,
    [DOLLY]                 NVARCHAR (100) NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [WMS_SEND_TIME]         DATETIME       NULL,
    [WMS_SEND_STATUS]       INT            NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (20)  NULL,
    CONSTRAINT [IDX_PK_COUNTER_MATERIAL_REQUESTS_INTERFACE_ID] PRIMARY KEY CLUSTERED ([INTERFACE_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)仓库发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)DOLLY 型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'DOLLY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)Bahnhof编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'BAHNHOF_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)收货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'WAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否取消', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'IS_CANCEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否组织拉动单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'IS_ORGANIZE_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)期望到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'EXPECTED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)当前计数器数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'CURRENT_PART_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)报警时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'ALARM_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)送货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'DELIVER_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)请求时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'REQUEST_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)显示屏位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'SCREEN_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)标准包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)接口类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'INTERFACE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'USAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DCP点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_EPS_COUNTER_MATERIAL_REQUESTS', @level2type = N'COLUMN', @level2name = N'INTERFACE_ID';

