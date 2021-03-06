﻿CREATE TABLE [LES].[TT_TWD_RUNSHEET] (
    [TWD_RUNSHEET_SN]       INT            IDENTITY (1, 1) NOT NULL,
    [TWD_RUNSHEET_NO]       VARCHAR (22)   NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [PLANT_ZONE]            NVARCHAR (10)  NULL,
    [PUBLISH_TIME]          DATETIME       NOT NULL,
    [RUNSHEET_TYPE]         INT            NOT NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [SUPPLIER_SN]           INT            NOT NULL,
    [DOCK]                  NVARCHAR (10)  NULL,
    [DELIVERY_LOCATION]     NVARCHAR (50)  NULL,
    [BOX_PARTS]             NVARCHAR (10)  NOT NULL,
    [PART_TYPE]             INT            NULL,
    [UNLOADING_TIME]        INT            NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME       NOT NULL,
    [SUGGEST_DELIVERY_TIME] DATETIME       NULL,
    [ACTUAL_ARRIVAL_TIME]   DATETIME       NULL,
    [VERIFY_TIME]           DATETIME       NULL,
    [REJECT_REASON]         NVARCHAR (200) NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (20)  NULL,
    [FEEDBACK]              NVARCHAR (100) NULL,
    [SHEET_STATUS]          INT            NOT NULL,
    [SEND_TIME]             DATETIME       NULL,
    [SEND_STATUS]           INT            NULL,
    [OPERATON_USER]         NVARCHAR (10)  NULL,
    [CHECK_USER]            NVARCHAR (10)  NULL,
    [RETRY_TIMES]           INT            NULL,
    [SUPPLY_TIME]           DATETIME       NULL,
    [SUPPLY_STATUS]         INT            NULL,
    [FAX_TIME]              DATETIME       NULL,
    [FAX_STATUS]            INT            NULL,
    [SAP_FLAG]              INT            NOT NULL,
    [SAP_FLAG2]             INT            NOT NULL,
    [RECKONING_NO]          NVARCHAR (30)  NULL,
    [WMS_SEND_TIME]         DATETIME       NULL,
    [WMS_SEND_STATUS]       INT            NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [GENERATE_TYPE]         INT            NULL,
    [IS_GENERATE_REC]       INT            NULL,
    [PRINT_TIMES]           INT            CONSTRAINT [DF_TT_TWD_RUNSHEET_PRINT_TIMES] DEFAULT ((0)) NOT NULL,
    [PRINT_STATE]           INT            CONSTRAINT [DF_TT_TWD_RUNSHEET_PRINT_STATE] DEFAULT ((0)) NOT NULL,
    [INHOUSE_SYSTEM_MODE]   NVARCHAR (10)  NULL,
    [IS_ASN]                INT            NULL,
    [IS_TRAY]               INT            NULL,
    [TIME_AND]              NVARCHAR (16)  NULL,
    CONSTRAINT [PK_TT_TWD_RUNSHEET] PRIMARY KEY CLUSTERED ([TWD_RUNSHEET_SN] ASC)
);

