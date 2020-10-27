CREATE TABLE [dbo].[TS_SYS_SEQ_CURRENT_VALUE] (
    [ID]              BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]             UNIQUEIDENTIFIER NULL,
    [SEQ_CODE]        NVARCHAR (32)    NULL,
    [SEQ_SECTION_FID] UNIQUEIDENTIFIER NULL,
    [QUERY_VALUE]     NVARCHAR (128)   NULL,
    [CURRENT_VALUE]   INT              NULL,
    [VALID_FLAG]      BIT              NULL,
    [CREATE_USER]     NVARCHAR (32)    NULL,
    [CREATE_DATE]     DATETIME         NULL,
    [MODIFY_USER]     NVARCHAR (32)    NULL,
    [MODIFY_DATE]     DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前递增值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'CURRENT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'查询值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_CURRENT_VALUE', @level2type = N'COLUMN', @level2name = N'QUERY_VALUE';

