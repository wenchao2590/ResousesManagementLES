CREATE TABLE [LES].[TM_WMM_TRAN_DETAILS_LOG] (
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
    [PROCESS_RESULT]        INT             CONSTRAINT [DF_TM_WMM_TRAN_DETAILS_LOG_PROCESS_RESULT] DEFAULT ((0)) NULL,
    [PROCESS_MESSAGE]       NVARCHAR (200)  NULL,
    [TRAN_ID]               INT             IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_TM_WMM_TRAN_DETAILS_LOG] PRIMARY KEY CLUSTERED ([TRAN_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实收箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实收件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'TARGET_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'TARGET_WM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRAN_DETAILS_LOG', @level2type = N'COLUMN', @level2name = N'TRAN_NO';

