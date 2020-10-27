CREATE TABLE [GJS].[TM_BAS_STORE_LOC] (
    [ID]             BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]            UNIQUEIDENTIFIER NULL,
    [PID]            UNIQUEIDENTIFIER NULL,
    [STORE_LOC_CODE] NVARCHAR (16)    NULL,
    [STORE_LOC_NAME] NVARCHAR (32)    NULL,
    [VALID_FLAG]     BIT              NULL,
    [CREATE_USER]    NVARCHAR (32)    NULL,
    [CREATE_DATE]    DATETIME         NULL,
    [MODIFY_USER]    NVARCHAR (32)    NULL,
    [MODIFY_DATE]    DATETIME         NULL,
    CONSTRAINT [PK_TM_BAS_STORE_LOC] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_STORE_LOC', @level2type = N'COLUMN', @level2name = N'STORE_LOC_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_STORE_LOC', @level2type = N'COLUMN', @level2name = N'STORE_LOC_CODE';

