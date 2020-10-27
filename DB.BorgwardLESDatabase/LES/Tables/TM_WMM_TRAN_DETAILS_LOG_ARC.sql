CREATE TABLE [LES].[TM_WMM_TRAN_DETAILS_LOG_ARC] (
    [TRAN_NO]               NVARCHAR (50)   NULL,
    [PLANT]                 NVARCHAR (5)    NULL,
    [BATCH_NO]              NVARCHAR (50)   NULL,
    [PART_NO]               NVARCHAR (20)   NULL,
    [BARCODE_DATA]          NVARCHAR (50)   NULL,
    [WM_NO]                 NVARCHAR (10)   NULL,
    [ZONE_NO]               NVARCHAR (20)   NULL,
    [DLOC]                  NVARCHAR (30)   NULL,
    [TARGET_WM]             NVARCHAR (10)   NULL,
    [TARGET_ZONE]           NVARCHAR (20)   NULL,
    [TARGET_DLOC]           NVARCHAR (30)   NULL,
    [MEASURING_UNIT_NO]     VARCHAR (8)     NULL,
    [PACKAGE]               INT             NULL,
    [MAX]                   NUMERIC (18, 2) NULL,
    [MIN]                   NUMERIC (18, 2) NULL,
    [NUM]                   NUMERIC (18, 2) NULL,
    [BOX_NUM]               NUMERIC (18, 2) NULL,
    [TRAN_STATE]            INT             NULL,
    [TRAN_DATE]             DATETIME        NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NULL,
    [PART_CNAME]            NVARCHAR (100)  NULL,
    [BOX_PARTS]             NVARCHAR (10)   NULL,
    [PICKUP_SEQ_NO]         INT             NULL,
    [RDC_DLOC]              VARCHAR (20)    NULL,
    [ACTUAL_PACKAGE_QTY]    INT             NULL,
    [INNER_LOCATION]        NVARCHAR (50)   NULL,
    [LOCATION]              NVARCHAR (20)   NULL,
    [STORAGE_LOCATION]      NVARCHAR (30)   NULL,
    [INHOUSE_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [REQUIRED_PACKAGE_QTY]  INT             NULL,
    [BARCODE_TYPE]          NVARCHAR (10)   NULL,
    [REQUIRED_DATE]         DATETIME        NULL,
    [PACHAGE_TYPE]          NVARCHAR (50)   NULL,
    [LINE_POSITION]         NVARCHAR (100)  NULL,
    [RUNSHEET_NO]           VARCHAR (22)    NULL,
    [PART_NICKNAME]         NVARCHAR (50)   NULL,
    [SUPPLIER_NAME]         NVARCHAR (100)  NULL,
    [DOCK]                  NVARCHAR (10)   NULL,
    [SUPPLIER_SNAME]        NVARCHAR (100)  NULL,
    [PACKAGE_MODEL]         NVARCHAR (30)   NULL,
    [PART_CLS]              NVARCHAR (50)   NULL,
    [PART_UNITS]            NVARCHAR (20)   NULL,
    [IS_BATCH]              INT             NULL,
    [TRAN_TYPE]             INT             NOT NULL,
    [CREATE_USER]           NVARCHAR (50)   NULL,
    [CREATE_DATE]           DATETIME        NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    [UPDATE_FLAG]           INT             NULL,
    [PROCESS_RESULT]        INT             CONSTRAINT [DF_TM_WMM_TRAN_DETAILS_LOG_PROCESS_RESULT_ARC] DEFAULT ((0)) NULL,
    [PROCESS_MESSAGE]       NVARCHAR (200)  NULL,
    [TRAN_ID]               INT             IDENTITY (1, 1) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'TRAN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PROCESS_MESSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PROCESS_RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'UPDATE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)交易类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否批次管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'IS_BATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PART_UNITS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件类别', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PART_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商简称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)线旁位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'LINE_POSITION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)收货包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PACHAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'REQUIRED_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)条码类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'BARCODE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'REQUIRED_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'STORAGE_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)内库位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'INNER_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实收数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'ACTUAL_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)交易时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'TRAN_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)交易状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'TRAN_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实收箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)MIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'MIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)MAX', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'MAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目的库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目的存贮区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'TARGET_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目的仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'TARGET_WM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)源存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)源仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)批次号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG_ARC', @level2type = N'COLUMN', @level2name = N'TRAN_NO';

