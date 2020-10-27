CREATE TABLE [dbo].[TABLE_20180117] (
    [FID]           UNIQUEIDENTIFIER NULL,
    [PLANT]         NVARCHAR (5)     NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)    NOT NULL,
    [VIN]           NVARCHAR (20)    NULL,
    [SEQNO]         VARCHAR (1)      NOT NULL,
    [PARTNO]        NVARCHAR (20)    NOT NULL,
    [PARTNAME]      NVARCHAR (300)   NULL,
    [QTY]           DECIMAL (18)     NULL,
    [BOXPART]       NVARCHAR (10)    NULL,
    [ORDERTYPE]     VARCHAR (1)      NOT NULL,
    [ORDERNO]       NVARCHAR (22)    NOT NULL,
    [RUNSHEETTYPE]  INT              NOT NULL,
    [ZORDNO]        NVARCHAR (36)    NULL,
    [FARBIN]        NVARCHAR (30)    NOT NULL,
    [ZCOLORE_D]     NVARCHAR (30)    NOT NULL,
    [ZCOLORI_D]     NVARCHAR (30)    NULL
);

