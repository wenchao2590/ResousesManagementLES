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
CREATE NONCLUSTERED INDEX [IX_TM_WMM_TRAN_DETAILS_LOG_1]
    ON [LES].[TM_WMM_TRAN_DETAILS_LOG]([TRAN_NO] ASC, [TRAN_TYPE] ASC, [PLANT] ASC, [WM_NO] ASC, [ZONE_NO] ASC, [SUPPLIER_NUM] ASC, [PART_NO] ASC, [PART_CNAME] ASC, [TRAN_DATE] ASC, [CREATE_DATE] ASC, [UPDATE_DATE] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TM_WMM_TRAN_DETAILS_LOG]
    ON [LES].[TM_WMM_TRAN_DETAILS_LOG]([BARCODE_DATA] ASC);

