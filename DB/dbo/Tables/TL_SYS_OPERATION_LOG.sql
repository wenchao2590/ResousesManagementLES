CREATE TABLE [dbo].[TL_SYS_OPERATION_LOG] (
    [ID]                BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]               UNIQUEIDENTIFIER NULL,
    [TABLE_NAME]        NVARCHAR (64)    NULL,
    [OPERATION_CONTEXT] NVARCHAR (MAX)   NULL,
    [IP_ADDRESS]        NVARCHAR (32)    NULL,
    [PAGE_URL]          NVARCHAR (512)   NULL,
    [BROWSER_INFO]      NVARCHAR (MAX)   NULL,
    [VALID_FLAG]        BIT              NULL,
    [CREATE_USER]       NVARCHAR (32)    NULL,
    [CREATE_DATE]       DATETIME         NULL,
    [OPERATION_TYPE]    INT              NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

