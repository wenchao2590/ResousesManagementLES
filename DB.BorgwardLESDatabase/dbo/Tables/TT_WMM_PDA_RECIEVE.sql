CREATE TABLE [dbo].[TT_WMM_PDA_RECIEVE] (
    [RecvID]     INT             IDENTITY (1, 1) NOT NULL,
    [Barcode]    NVARCHAR (50)   NOT NULL,
    [PartName]   NVARCHAR (100)  NOT NULL,
    [PartNUM]    DECIMAL (18, 2) NOT NULL,
    [RecvPerson] NVARCHAR (20)   NULL,
    [Location]   NVARCHAR (20)   NOT NULL,
    CONSTRAINT [PK_TT_WMM_PDA_RECIEVE] PRIMARY KEY CLUSTERED ([RecvID] ASC)
);

