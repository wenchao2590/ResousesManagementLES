﻿CREATE TABLE [dbo].[OutPutTraceAnaly4] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [OUTPUT_ID]    INT           NOT NULL,
    [PART_NO]      NVARCHAR (50) NULL,
    [ActualQty]    INT           NULL,
    [BARCODE_DATA] NVARCHAR (50) NULL
);

