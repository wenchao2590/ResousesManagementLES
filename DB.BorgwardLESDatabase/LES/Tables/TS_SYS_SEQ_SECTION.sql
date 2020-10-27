CREATE TABLE [LES].[TS_SYS_SEQ_SECTION] (
    [ID]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [SEQ_CODE]           NVARCHAR (32) NULL,
    [SECTION_SEQ]        INT           NULL,
    [IS_FIXED_LENGTH]    BIT           NULL,
    [LENGTH]             INT           NULL,
    [FILL_TYPE]          INT           NULL,
    [FILL_CHAR]          NVARCHAR (1)  NULL,
    [DATA_GENERATE_TYPE] INT           NULL,
    [STEP_LENGTH]        INT           NULL,
    [DEFAULT_VALUE]      NVARCHAR (16) NULL,
    [MIN_VALUE]          INT           NULL,
    [MAX_VALUE]          INT           NULL,
    [IS_CYCLE]           BIT           NULL,
    [IS_AUTOUP]          BIT           NULL,
    [VALID_FLAG]         BIT           NULL,
    [CREATE_USER]        NVARCHAR (32) NOT NULL,
    [CREATE_DATE]        DATETIME      NOT NULL,
    [UPDATE_USER]        NVARCHAR (32) NULL,
    [UPDATE_DATE]        DATETIME      NULL,
    CONSTRAINT [PK_SEQSECTION] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'IS_AUTOUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否专用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'IS_CYCLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)最大值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'MAX_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)最小值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'MIN_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)默认值', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'DEFAULT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'STEP_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'DATA_GENERATE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'FILL_CHAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'FILL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)径长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'IS_FIXED_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'SECTION_SEQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'SEQ_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'ID';

