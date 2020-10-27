CREATE TABLE [LES].[TM_JIS_RACK] (
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]          NVARCHAR (8)   NOT NULL,
    [RACK]                  NVARCHAR (20)  NOT NULL,
    [RACK_STATE]            INT            NOT NULL,
    [RACK_CNAME]            VARCHAR (200)  NULL,
    [RACK_ENAME]            VARCHAR (200)  NULL,
    [WORK_STATION_NO]       NVARCHAR (100) NULL,
    [CACULATE_POINT]        NVARCHAR (15)  NULL,
    [BACKLOG_QUANTITY]      INT            NOT NULL,
    [BACKLOG_TIME]          INT            NOT NULL,
    [DELIVERY_TIME]         INT            NOT NULL,
    [UNLOADING_TIME]        INT            NOT NULL,
    [DOCK]                  NVARCHAR (10)  NULL,
    [RACK_ROW]              INT            NOT NULL,
    [RACK_COLUMN]           INT            NOT NULL,
    [UNLOADBLDG]            VARCHAR (22)   NULL,
    [LOCATION]              NVARCHAR (20)  NULL,
    [PREVIEW_POINT]         NVARCHAR (200) NULL,
    [ARRANGEMENT_POINT]     NVARCHAR (15)  NULL,
    [PRINT_TYPE]            VARCHAR (2)    NULL,
    [WORKAREA_DISTANCE]     INT            NULL,
    [IS_DISPLAY_VIN]        BIT            NULL,
    [IS_ARRANGEMENT_EMPTY]  BIT            NULL,
    [CACULATE_TERM]         INT            NULL,
    [RECEIVE_SUPPLIER]      VARCHAR (8)    NULL,
    [TRANSPORT_SUPPLIER]    NVARCHAR (8)   NULL,
    [IS_SEPERATE_SHEET]     INT            NULL,
    [SEPERATE_LOC]          NVARCHAR (20)  NULL,
    [BUFFER_QNT]            INT            NULL,
    [BRIDGE_QNT]            INT            NULL,
    [WAREHOUSE]             NVARCHAR (50)  NULL,
    [IS_TRANSFER_TRANSPORT] INT            NULL,
    [RUNNING_NO]            NVARCHAR (8)   NULL,
    [KNR]                   NVARCHAR (20)  NULL,
    [BOX_PARTS]             NVARCHAR (20)  NULL,
    [CACULATE_CHECK_POINT]  NVARCHAR (20)  NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [ROUTE]                 NVARCHAR (10)  NULL,
    [IS_CREATE_TASK]        INT            NULL,
    CONSTRAINT [PK_TM_JIS_RACK] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [RACK] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否生成车载任务', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'IS_CREATE_TASK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算点校验点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'CACULATE_CHECK_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件大类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算起始KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算起始流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否传服务商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'IS_TRANSFER_TRANSPORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'WAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'天桥数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'BRIDGE_QNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'缓存数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'BUFFER_QNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拆分位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'SEPERATE_LOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拆分组单标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'IS_SEPERATE_SHEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'TRANSPORT_SUPPLIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'收货供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'RECEIVE_SUPPLIER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算周期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'CACULATE_TERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否跳空', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'IS_ARRANGEMENT_EMPTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否显示VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'IS_DISPLAY_VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SUMA上线工位距离', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'WORKAREA_DISTANCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'打印类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'PRINT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'ARRANGEMENT_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'预览点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'PREVIEW_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'暂存工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'UNLOADBLDG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'列数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'RACK_COLUMN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'行数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'RACK_ROW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'卸货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'UNLOADING_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交货Leadtime', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'DELIVERY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'累积时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'BACKLOG_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'累积数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'BACKLOG_QUANTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'结算点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'CACULATE_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工位器具编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'WORK_STATION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'英文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'RACK_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'RACK_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'RACK_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_JIS 零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_RACK';

