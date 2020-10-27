﻿CREATE TABLE [LES].[TT_WMS_STOCKS_20171012] (
    [STOCK_IDENTITY]   INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]            NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]    NVARCHAR (10)   NULL,
    [PLANT_ZONE]       NVARCHAR (5)    NULL,
    [WORKSHOP]         NVARCHAR (4)    NULL,
    [SUPPLIER_NUM]     NVARCHAR (12)   NULL,
    [PART_NO]          NVARCHAR (20)   NOT NULL,
    [PART_CNAME]       NVARCHAR (100)  NULL,
    [PART_ENAME]       NVARCHAR (100)  NULL,
    [PART_NICKNAME]    NVARCHAR (50)   NULL,
    [PART_UNITS]       NVARCHAR (20)   NULL,
    [PACKAGE_MODEL]    NVARCHAR (30)   NULL,
    [PACKAGE]          INT             NULL,
    [LOGICAL_PK]       NVARCHAR (50)   NULL,
    [DELETE_FLAG]      BIT             NULL,
    [ROUTE]            NVARCHAR (50)   NULL,
    [ZONE_NO]          NVARCHAR (20)   NOT NULL,
    [WM_NO]            NVARCHAR (10)   NOT NULL,
    [OCCUPY_AREA]      NUMERIC (18, 2) NULL,
    [DLOC]             NVARCHAR (20)   NULL,
    [MAX]              NUMERIC (18, 2) NULL,
    [MIN]              NUMERIC (18, 2) NULL,
    [ROW_NUMBER]       INT             NULL,
    [LINE_NUMBER]      INT             NULL,
    [HIGH_NUMBER]      INT             NULL,
    [MATERIAL_GROUP]   NVARCHAR (100)  NULL,
    [KEEPER]           NVARCHAR (50)   NULL,
    [TRANSER]          NVARCHAR (50)   NULL,
    [INFORMATIONER]    NVARCHAR (50)   NULL,
    [ELOC]             NVARCHAR (50)   NULL,
    [SAFE_STOCK]       INT             NULL,
    [STOCKS]           NUMERIC (18, 2) NULL,
    [FROZEN_STOCKS]    NUMERIC (18, 2) NULL,
    [AVAILBLE_STOCKS]  NUMERIC (18, 2) NULL,
    [IS_BATCH]         INT             NULL,
    [WMS_RULE]         NVARCHAR (20)   NULL,
    [COUNTER]          NUMERIC (18, 2) NULL,
    [FRAGMENT_NUM]     NUMERIC (18, 2) NULL,
    [STOCKS_NUM]       NUMERIC (18, 2) NULL,
    [PART_WEIGHT]      NUMERIC (18, 2) NULL,
    [PART_CLS]         NVARCHAR (50)   NULL,
    [IS_REPACK]        INT             NULL,
    [REPACK_ROUTE]     NVARCHAR (40)   NULL,
    [IS_TRIGGER_PULL]  INT             NULL,
    [TRIGGER_WM_NO]    NVARCHAR (10)   NULL,
    [TRIGGER_ZONE_NO]  NVARCHAR (30)   NULL,
    [TRIGGER_DLOC]     NVARCHAR (30)   NULL,
    [EMG_TIME]         INT             NULL,
    [SUPPER_ZONE_DLOC] NVARCHAR (50)   NULL,
    [CHECK_TYPE]       INT             NULL,
    [BUSINESS_PK]      NVARCHAR (50)   NULL,
    [BATCH_NO]         NVARCHAR (100)  NULL,
    [BARCODE_DATA]     NVARCHAR (50)   NULL,
    [BARCODE_TYPE]     NVARCHAR (10)   NULL,
    [COMMENTS]         NVARCHAR (200)  NULL,
    [CREATE_USER]      NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]      DATETIME        NOT NULL,
    [UPDATE_USER]      NVARCHAR (50)   NULL,
    [UPDATE_DATE]      DATETIME        NULL
);

