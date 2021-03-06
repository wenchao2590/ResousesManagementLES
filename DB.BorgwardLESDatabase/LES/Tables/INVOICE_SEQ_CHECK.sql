﻿CREATE TABLE [LES].[INVOICE_SEQ_CHECK] (
    [Id]          BIGINT   IDENTITY (1, 1) NOT NULL,
    [SEQ_ID]      BIGINT   NOT NULL,
    [Create_Time] DATETIME CONSTRAINT [DF_INVOICE_SEQ_CHECK_Create_Time] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK_INVOICE_SEQ_CHECK] PRIMARY KEY CLUSTERED ([Id] ASC)
);

