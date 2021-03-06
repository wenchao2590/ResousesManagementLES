﻿CREATE TABLE [LES].[TT_BAS_PULL_ORDERS_ARC_3_21] (
    [SEQID]              INT             IDENTITY (1, 1) NOT NULL,
    [ORDER_NO]           NVARCHAR (20)   NOT NULL,
    [WERK]               NVARCHAR (4)    NOT NULL,
    [SPJ]                NVARCHAR (8)    NOT NULL,
    [KNR]                NVARCHAR (8)    NOT NULL,
    [MODEL_YEAR]         NVARCHAR (30)   NULL,
    [MODEL]              NVARCHAR (30)   NOT NULL,
    [FARBAU]             NVARCHAR (30)   NOT NULL,
    [FARBIN]             NVARCHAR (30)   NOT NULL,
    [PNR_STRING]         NVARCHAR (200)  NOT NULL,
    [PNR_STRING_COMPUTE] NVARCHAR (200)  NOT NULL,
    [VEHICLE_ORDER]      NVARCHAR (8)    NOT NULL,
    [ORDER_DATE]         DATETIME        NOT NULL,
    [DEAL_FLAG]          INT             NOT NULL,
    [STATUS_FLAG]        NVARCHAR (8)    NOT NULL,
    [VORSERIE]           BIT             NOT NULL,
    [SIGNATURE]          NVARCHAR (256)  NOT NULL,
    [ORDER_FILE_NAME]    NVARCHAR (256)  NULL,
    [ORDER_TYPE]         VARCHAR (2)     NULL,
    [RECALCULATE_FLAG]   INT             NULL,
    [CHANGE_FLAG]        INT             NULL,
    [ASSEMBLY_LINE]      NVARCHAR (10)   NULL,
    [PROCESS_LINE_SN]    INT             NULL,
    [INIT_STSTUS]        INT             NULL,
    [VIN]                NVARCHAR (30)   NULL,
    [PART_NO]            NVARCHAR (18)   NULL,
    [QTY]                NUMERIC (18, 2) NULL,
    [MEASURING_UNIT]     NVARCHAR (8)    NULL,
    [PLAN_FLAG]          NVARCHAR (3)    NULL,
    [COMMENTS]           NVARCHAR (200)  NULL,
    [CREATE_USER]        NVARCHAR (50)   NULL,
    [CREATE_DATE]        DATETIME        NULL,
    [UPDATE_USER]        NVARCHAR (50)   NULL,
    [UPDATE_DATE]        DATETIME        NULL,
    [ZCOLORI]            NVARCHAR (30)   NULL,
    [ZCOLORI_D]          NVARCHAR (30)   NULL
);

