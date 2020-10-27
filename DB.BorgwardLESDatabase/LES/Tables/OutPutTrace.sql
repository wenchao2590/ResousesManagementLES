CREATE TABLE [LES].[OutPutTrace] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OUTPUT_No]    NVARCHAR (50)  NULL,
    [OUTPUT_ID]    INT            NULL,
    [PART_NO]      NVARCHAR (50)  NULL,
    [BARCODE_DATA] NVARCHAR (50)  NULL,
    [ActualQty]    INT            NULL,
    [Create_User]  NVARCHAR (50)  NULL,
    [Create_Time]  DATETIME       NULL,
    [Operation]    NVARCHAR (500) NULL,
    CONSTRAINT [PK_OutPutTrace] PRIMARY KEY CLUSTERED ([Id] ASC)
);

