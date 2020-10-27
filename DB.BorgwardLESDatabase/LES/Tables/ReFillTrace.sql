CREATE TABLE [LES].[ReFillTrace] (
    [Id]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [REPACKAGE_NO] NVARCHAR (50)  NULL,
    [REPACKAGE_ID] INT            NULL,
    [PART_NO]      NVARCHAR (50)  NULL,
    [BARCODE_DATA] NVARCHAR (50)  NULL,
    [ActualQty]    INT            NULL,
    [Create_User]  NVARCHAR (50)  NULL,
    [Create_Time]  DATETIME       NULL,
    [Operation]    NVARCHAR (500) NULL,
    CONSTRAINT [PK_ReFillTrace] PRIMARY KEY CLUSTERED ([Id] ASC)
);

