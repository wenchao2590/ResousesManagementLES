﻿CREATE TABLE [LES].[TT_SPM_INVOICE_REQUEST_ARC] (
    [SEQ_ID]                     BIGINT          IDENTITY (1, 1) NOT NULL,
    [PLAN_RUNSHEET_NO]           VARCHAR (30)    NOT NULL,
    [PLAN_NO]                    NVARCHAR (50)   NULL,
    [ASN_NO]                     NVARCHAR (50)   NULL,
    [PLANT]                      NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]              NVARCHAR (10)   NULL,
    [WORKSHOP]                   NVARCHAR (4)    NULL,
    [PLANT_ZONE]                 NVARCHAR (5)    NULL,
    [SUPPLIER_NUM]               NVARCHAR (8)    NOT NULL,
    [INVOICE_NO]                 NVARCHAR (200)  NULL,
    [SUPPLIER_NAME]              NVARCHAR (100)  NOT NULL,
    [SUPPLIER_SNAME]             NVARCHAR (100)  NULL,
    [ACCOUNT_START_DATE]         DATETIME        NOT NULL,
    [MEASURING_UNIT_NO]          NVARCHAR (8)    NULL,
    [ACTUAL_INHOUSE_PACKAGE]     INT             NULL,
    [ACTUAL_INHOUSE_PACKAGE_QTY] NUMERIC (18, 3) NULL,
    [ORDER_NO]                   NVARCHAR (30)   NULL,
    [ITEM_NO]                    NVARCHAR (50)   NULL,
    [IDOC]                       NVARCHAR (16)   NOT NULL,
    [PART_NO]                    NVARCHAR (20)   NOT NULL,
    [PART_CNAME]                 NVARCHAR (300)  NULL,
    [PART_ENAME]                 NVARCHAR (300)  NULL,
    [SUPPLIER_NUM2]              NVARCHAR (8)    NOT NULL,
    [SETTLE_AMOUNT]              DECIMAL (18, 2) NULL,
    [TAX]                        DECIMAL (18, 2) NULL,
    [PRICE_TAX_TOTAL]            DECIMAL (18, 2) NULL,
    [DIFFER]                     DECIMAL (18, 2) NULL,
    [SETTLE_PRICE_VALIDDATE]     NVARCHAR (50)   NULL,
    [CHECK_RESULT]               INT             CONSTRAINT [DF_TT_FC_InvoiceRequest_CheckResult] DEFAULT ((0)) NULL,
    [READ_USER]                  NVARCHAR (50)   NULL,
    [READ_DATE]                  DATETIME        NULL,
    [CHECK_USER]                 NVARCHAR (50)   NULL,
    [CHECK_DATE]                 DATETIME        NULL,
    [ISSUE_DATE]                 DATETIME        NULL,
    [PART_INENTITY]              NVARCHAR (30)   NULL,
    [PART_INENTITY_YEAR]         NVARCHAR (20)   NULL,
    [PART_INENTITY_ITEMNO]       NVARCHAR (20)   NULL,
    [RECIEVE_DOCK]               NVARCHAR (50)   NULL,
    [INVOICE_AMOUNT]             DECIMAL (18, 2) NULL,
    [CURRENCY]                   NVARCHAR (50)   NULL,
    [INVOICE_DATE]               DATETIME        NULL,
    [COMMENTS]                   NVARCHAR (200)  NULL,
    [CREATE_USER]                NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]                DATETIME        NOT NULL,
    [UPDATE_USER]                NVARCHAR (50)   NULL,
    [UPDATE_DATE]                DATETIME        NULL,
    [SHEET_TYPE]                 NVARCHAR (4)    NULL,
    [SAP_AMOUNT]                 DECIMAL (18, 2) NULL,
    [WAERS]                      NVARCHAR (5)    NULL,
    [MWSKZ]                      NVARCHAR (5)    NULL,
    [SCHPR]                      NVARCHAR (1)    NULL,
    [Invoice_Type]               INT             CONSTRAINT [DF__TT_SPM_IN__Invoi__42ECDBF6] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [IDX_PK_INVOICE_REQUEST_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);
