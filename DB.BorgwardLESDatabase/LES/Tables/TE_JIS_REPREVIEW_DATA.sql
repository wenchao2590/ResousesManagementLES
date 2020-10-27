CREATE TABLE [LES].[TE_JIS_REPREVIEW_DATA] (
    [PREVIEW_DATA_SN]   INT             IDENTITY (1, 1) NOT NULL,
    [PREVIEW_DATA_TIME] DATETIME        NOT NULL,
    [PREVIEW_TYPE]      NVARCHAR (20)   NULL,
    [RUNNING_NUMBER]    NVARCHAR (5)    NULL,
    [SEQUENCE_NUMBER]   NVARCHAR (10)   NULL,
    [SOP_FLAG]          INT             NULL,
    [PLANT]             NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]     NVARCHAR (10)   NOT NULL,
    [SUPPLIER_NUM]      NVARCHAR (12)   NOT NULL,
    [VIN]               NVARCHAR (20)   NULL,
    [MODEL_YEAR]        NVARCHAR (20)   NULL,
    [MODEL_NO]          NVARCHAR (8)    NULL,
    [VEHICLE_STATUS]    NVARCHAR (15)   NULL,
    [DCP_POINT]         NVARCHAR (15)   NULL,
    [DCP_NAME]          NVARCHAR (100)  NULL,
    [MEASURING_UNIT_NO] VARCHAR (8)     NULL,
    [PART_NO]           NVARCHAR (20)   NULL,
    [PART_CNAME]        NVARCHAR (100)  NULL,
    [USAGE]             NUMERIC (18, 2) NULL,
    [PART_NICK_NAME]    NVARCHAR (30)   NULL,
    [ITEM_NUMBER_TYPE]  NVARCHAR (200)  NULL,
    [RACK]              NVARCHAR (20)   NOT NULL,
    [BOX_NUMBER]        INT             NULL,
    [FORMAT]            NVARCHAR (6)    NULL,
    [MODEL]             NVARCHAR (10)   NULL,
    [ORDER_ID]          NVARCHAR (36)   NULL,
    [CAR_NO]            NVARCHAR (8)    NULL,
    [JIS_RUNSHEET_SN]   INT             NULL,
    [JIS_RUNSHEET_NO]   NVARCHAR (18)   NULL,
    [SEND_STATUS]       INT             NULL,
    [SEND_TIME]         DATETIME        NULL,
    [WMS_SEND_STATUS]   INT             NULL,
    [WMS_SEND_TIME]     DATETIME        NULL,
    [EXTERIOR_COLOR]    NVARCHAR (4)    NULL,
    [INTERNAL_COLOR]    NVARCHAR (2)    NULL,
    [CHANGE_TYPE]       NVARCHAR (30)   NULL,
    [CHANGE_REASON]     NVARCHAR (100)  NULL,
    [COMMENTS]          NVARCHAR (200)  NULL,
    [UPDATE_DATE]       DATETIME        NULL,
    [UPDATE_USER]       NVARCHAR (50)   NULL,
    [CREATE_DATE]       DATETIME        NULL,
    [CREATE_USER]       NVARCHAR (50)   NULL,
    [BOX_PARTS_NAME]    NVARCHAR (200)  NULL,
    [SIGNATURE]         NVARCHAR (256)  NOT NULL,
    CONSTRAINT [IDX_PK_REPREVIEW_DATA_PREVIEW_DATA_SN] PRIMARY KEY CLUSTERED ([PREVIEW_DATA_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？订单签名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'SIGNATURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件类名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'BOX_PARTS_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'CHANGE_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'CHANGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？内饰', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'INTERNAL_COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？外色', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'EXTERIOR_COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？仓库发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？仓库发送标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？拉动单号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？拉动单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？Knr', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'CAR_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'ORDER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？格式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'FORMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'BOX_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'ITEM_NUMBER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'PART_NICK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'USAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_DCP名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'DCP_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？MES扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_状态点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'VEHICLE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？年型年', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？是否显示VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_SOP标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'SOP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？过点序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'RUNNING_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'PREVIEW_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'PREVIEW_DATA_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_REPREVIEW_DATA', @level2type = N'COLUMN', @level2name = N'PREVIEW_DATA_SN';

