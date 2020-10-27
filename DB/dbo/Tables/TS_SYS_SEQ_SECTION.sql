CREATE TABLE [dbo].[TS_SYS_SEQ_SECTION] (
    [ID]                 BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]                UNIQUEIDENTIFIER NULL,
    [DEFINE_FID]         UNIQUEIDENTIFIER NULL,
    [SEQ_CODE]           NVARCHAR (32)    NULL,
    [SECTION_SEQ]        INT              NULL,
    [IS_FIXED_LENGTH]    BIT              NULL,
    [LENGTH]             INT              NULL,
    [FILL_TYPE]          INT              NULL,
    [FILL_CHAR]          NVARCHAR (1)     NULL,
    [DATA_GENERATE_TYPE] INT              NULL,
    [STEP_LENGTH]        INT              NULL,
    [DEFAULT_VALUE]      NVARCHAR (16)    NULL,
    [MIN_VALUE]          INT              NULL,
    [MAX_VALUE]          INT              NULL,
    [IS_CYCLE]           BIT              NULL,
    [IS_AUTOUP]          BIT              NULL,
    [VALID_FLAG]         BIT              NULL,
    [CREATE_USER]        NVARCHAR (32)    NULL,
    [CREATE_DATE]        DATETIME         NULL,
    [MODIFY_USER]        NVARCHAR (32)    NULL,
    [MODIFY_DATE]        DATETIME         NULL,
    [IS_SEED_VALUE]      BIT              NULL,
    CONSTRAINT [PK_TM_SYS_SEQ_SECTION] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否自动增长', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'IS_AUTOUP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否循环', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'IS_CYCLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最大值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'MAX_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'MIN_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'默认值', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'DEFAULT_VALUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'步长', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'STEP_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据生产方式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'DATA_GENERATE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'填充字符', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'FILL_CHAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'填充方式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'FILL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'长度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否固定长度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'IS_FIXED_LENGTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区段号', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'SECTION_SEQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序号代码', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION', @level2type = N'COLUMN', @level2name = N'SEQ_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序列号段数', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_SEQ_SECTION';

