CREATE TABLE [GJS].[TM_BAS_CLCDDM] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [DM]          NVARCHAR (8)  NULL,
    [MC]          NVARCHAR (32) NULL,
    [SM]          NVARCHAR (64) NULL,
    [VALID_FLAG]  BIT           NULL,
    [CREATE_USER] NVARCHAR (32) NULL,
    [CREATE_DATE] DATETIME      NULL,
    [MODIFY_USER] NVARCHAR (32) NULL,
    [MODIFY_DATE] DATETIME      NULL,
    CONSTRAINT [PK_TM_BAS_CLCDDM] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'描述', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLCDDM', @level2type = N'COLUMN', @level2name = N'SM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLCDDM', @level2type = N'COLUMN', @level2name = N'MC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TM_BAS_CLCDDM', @level2type = N'COLUMN', @level2name = N'DM';

