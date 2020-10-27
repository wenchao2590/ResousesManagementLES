﻿CREATE TABLE [dbo].[TI_ODS_ORDER_BOM_IN_TEST] (
    [SUB_SEQ_ID]  BIGINT          IDENTITY (1, 1) NOT NULL,
    [ZORDNO]      NVARCHAR (10)   NULL,
    [ZKWERK]      NVARCHAR (4)    NULL,
    [ZBOMID]      NVARCHAR (10)   NULL,
    [ZCOMNO]      NVARCHAR (18)   NULL,
    [ZCOMDS]      NVARCHAR (30)   NULL,
    [ZVIN]        NVARCHAR (17)   NULL,
    [ZQTY]        NUMERIC (18, 2) NULL,
    [ZDATE]       DATETIME        NULL,
    [ZLOC]        NVARCHAR (8)    NULL,
    [ZST]         NVARCHAR (1)    NULL,
    [ZMEMO]       NVARCHAR (80)   NULL,
    [ZMEINS]      NVARCHAR (8)    NULL,
    [DEAL_FLAG]   INT             NULL,
    [COMMENTS]    NVARCHAR (200)  NULL,
    [UPDATE_DATE] DATETIME        NULL,
    [UPDATE_USER] NVARCHAR (50)   NULL,
    [CREATE_DATE] DATETIME        NOT NULL,
    [CREATE_USER] NVARCHAR (50)   NOT NULL
);

