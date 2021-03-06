﻿CREATE TABLE [LES].[TL_SYS_OPERATION_LOG_ARC] (
    [OPERATION_ID]          UNIQUEIDENTIFIER NOT NULL,
    [PLANT]                 NVARCHAR (5)     NULL,
    [ASSEMBLYLINE]          NVARCHAR (10)    NULL,
    [OPERATION_TIME]        DATETIME         NOT NULL,
    [USER_LOGIN_NAME]       NVARCHAR (50)    NOT NULL,
    [OPERATION_ACTION]      NVARCHAR (200)   NOT NULL,
    [OPERATION_DETAIL]      NVARCHAR (MAX)   NULL,
    [OPERATION_PARAMETER1]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER2]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER3]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER4]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER5]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER6]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER7]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER8]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER9]  NVARCHAR (100)   NULL,
    [OPERATION_PARAMETER10] NVARCHAR (100)   NULL,
    CONSTRAINT [IDX_PK_OPERATION_LOG_OPERATION_ID_ARC] PRIMARY KEY CLUSTERED ([OPERATION_ID] ASC)
);

