CREATE TABLE [LES].[TT_SPM_SUPPLIER_PART_QUOTA_101115Back] (
    [QUOTA_ID]             INT             IDENTITY (1, 1) NOT NULL,
    [PART_NO]              NVARCHAR (20)   NULL,
    [SUPPLIER_NUM]         NVARCHAR (12)   NULL,
    [PLANT]                NVARCHAR (5)    NULL,
    [START_EFFECTIVE_DATE] DATETIME        NULL,
    [END_EFFECTIVE_DATE]   DATETIME        NULL,
    [QUOTE]                NUMERIC (18, 2) NULL,
    [COMMENTS]             NVARCHAR (200)  NULL,
    [UPDATE_DATE]          DATETIME        NULL,
    [UPDATE_USER]          NVARCHAR (50)   NULL,
    [CREATE_DATE]          DATETIME        NOT NULL,
    [CREATE_USER]          NVARCHAR (50)   NOT NULL,
    [AGREEMENT_NO]         NVARCHAR (20)   NULL,
    [PROJECT]              NVARCHAR (20)   NULL,
    [LOEKZ]                NVARCHAR (1)    NULL,
    [PROCESS_FLAG]         INT             NULL
);

