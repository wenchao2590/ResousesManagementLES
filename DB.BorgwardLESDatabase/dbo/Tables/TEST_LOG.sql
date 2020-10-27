﻿CREATE TABLE [dbo].[TEST_LOG] (
    [ID]   INT            IDENTITY (1, 1) NOT NULL,
    [time] DATETIME       CONSTRAINT [DF_TEST_LOG_time] DEFAULT (getdate()) NULL,
    [des]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_TEST_LOG] PRIMARY KEY CLUSTERED ([ID] ASC)
);

