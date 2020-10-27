CREATE TABLE [LES].[TE_BAS_INHOUSE_CHANGE_PULL] (
    [SEQ_ID]      INT            IDENTITY (1, 1) NOT NULL,
    [PART_NO]     NVARCHAR (20)  NOT NULL,
    [LOGICAL_PK]  NVARCHAR (50)  NOT NULL,
    [DIFF_FLAG]   INT            NULL,
    [TERMAL_PK]   NVARCHAR (50)  NULL,
    [COMMENTS]    NVARCHAR (200) NULL,
    [CREATE_USER] NVARCHAR (50)  NULL,
    [CREATE_DATE] DATETIME       NULL,
    [UPDATE_USER] NVARCHAR (50)  NULL,
    [UPDATE_DATE] DATETIME       NULL,
    CONSTRAINT [IDX_PK_INHOUSE_CHANGE_PULL_SEQ_ID] PRIMARY KEY NONCLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'TERMAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？差异标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'DIFF_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_CHANGE_PULL', @level2type = N'COLUMN', @level2name = N'SEQ_ID';

