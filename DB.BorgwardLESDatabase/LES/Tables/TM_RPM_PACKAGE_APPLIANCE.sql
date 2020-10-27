CREATE TABLE [LES].[TM_RPM_PACKAGE_APPLIANCE] (
    [PACKAGE_NO]            NVARCHAR (30)   NOT NULL,
    [PLANT]                 NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)   NULL,
    [PLANT_ZONE]            NVARCHAR (5)    NULL,
    [WORKSHOP]              NVARCHAR (4)    NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (20)   NULL,
    [PACKAGE_CNAME]         NVARCHAR (100)  NULL,
    [PACKAGE_ENAME]         NVARCHAR (100)  NULL,
    [VALID_FLAG]            BIT             NOT NULL,
    [BOX_TYPE]              NVARCHAR (30)   NULL,
    [PACKAGE_TYPE]          INT             NULL,
    [IS_REPEAT]             INT             NULL,
    [IS_GENERAL]            INT             NULL,
    [INHOUSE_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INHOUSE_PACKAGE]       INT             NULL,
    [INHOUSE_LENGTH]        NUMERIC (18, 2) NULL,
    [INHOUSE_WIDTH]         NUMERIC (18, 2) NULL,
    [INHOUSE_HEIGH]         NUMERIC (18, 2) NULL,
    [INHOUSE_WEIGHT]        NUMERIC (18, 2) NULL,
    [INHOUSE_LAYERS]        INT             NULL,
    [INHOUSE_BOXS]          INT             NULL,
    [TRANS_PACKAGE_MODEL]   NVARCHAR (30)   NULL,
    [TRANS_PACKAGE]         INT             NULL,
    [TRANS_LENGTH]          NUMERIC (18, 2) NULL,
    [TRANS_WIDTH]           NUMERIC (18, 2) NULL,
    [TRANS_HEIGH]           NUMERIC (18, 2) NULL,
    [TRANS_WEIGHT]          NUMERIC (18, 2) NULL,
    [TRANS_LAYERS]          INT             NULL,
    [TRANS_BOXS]            INT             NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INBOUND_PACKAGE]       INT             NULL,
    [INBOUND_LENGTH]        NUMERIC (18, 2) NULL,
    [INBOUND_WIDTH]         NUMERIC (18, 2) NULL,
    [INBOUND_HEIGH]         NUMERIC (18, 2) NULL,
    [INBOUND_WEIGHT]        NUMERIC (18, 2) NULL,
    [INBOUND_LAYERS]        INT             NULL,
    [INBOUND_BOXS]          INT             NULL,
    [MATERIAL]              INT             NULL,
    [BOX_WEIGHT]            DECIMAL (18, 2) NULL,
    [USE_SCHOPE]            NVARCHAR (50)   NULL,
    [START_EFFECTIVE_DATE]  DATETIME        NULL,
    [EXPIRE_DATE]           DATETIME        NULL,
    [MAX_HIGH]              DECIMAL (18, 2) NULL,
    [MAX_WEIGHT]            DECIMAL (18, 2) NULL,
    [MAX_LOAD_WEIGHT]       DECIMAL (18, 2) NULL,
    [MAX_LOAD_NUM]          DECIMAL (18, 2) NULL,
    [LAYER_BOX_NUM]         DECIMAL (18, 2) NULL,
    [STACKING_NUM]          DECIMAL (18, 2) NULL,
    [LOGICAL_PK]            NVARCHAR (50)   NULL,
    [STORAGE_LOCATION]      NVARCHAR (30)   NULL,
    [IS_ACTIVE]             INT             NULL,
    [WM_NO]                 NVARCHAR (10)   NULL,
    [ZONE_NO]               NVARCHAR (30)   NULL,
    [DLOC]                  NVARCHAR (30)   NULL,
    [PACKAGE_RESOURCE]      INT             NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [CREATE_USER]           NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]           DATETIME        NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    CONSTRAINT [PK_TM_RPM_PACKAGE_APPLIANCE] PRIMARY KEY CLUSTERED ([PACKAGE_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '器具来源', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'PACKAGE_RESOURCE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否活动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'IS_ACTIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'STORAGE_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '堆码层数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'STACKING_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '每层箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'LAYER_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最大装箱数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'MAX_LOAD_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最大装载重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'MAX_LOAD_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最大承重', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'MAX_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最大堆高', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'MAX_HIGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '失效时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'EXPIRE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '使用范围', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'USE_SCHOPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '空箱重', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'BOX_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '材质', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'MATERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂每层箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INBOUND_BOXS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂层数(堆码层数)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INBOUND_LAYERS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂单箱重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INBOUND_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂外径高', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INBOUND_HEIGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂外径宽', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INBOUND_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂外径长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INBOUND_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装数量(收容数)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输每层箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_BOXS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输层数(堆码层数)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_LAYERS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输单箱重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输外径高', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_HEIGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输外径宽', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输外径长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输包装数量(收容数)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线每层箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INHOUSE_BOXS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线层数(堆码层数)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INHOUSE_LAYERS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线单箱重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INHOUSE_WEIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线外径高', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INHOUSE_HEIGH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线外径宽', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INHOUSE_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线外径长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INHOUSE_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量(收容数)', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否通用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'IS_GENERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '重复使用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'IS_REPEAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '器具类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'PACKAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '箱型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'BOX_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '英文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'PACKAGE_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'PACKAGE_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装_包装器具编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE', @level2type = N'COLUMN', @level2name = N'PACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_包装器具', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_APPLIANCE';

