CREATE TABLE [dbo].[TS_SYS_REPORT_COLUMN] (
    [Id]              BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]             UNIQUEIDENTIFIER NULL,
    [PID]             UNIQUEIDENTIFIER NULL,
    [FIELD_NAME]      NVARCHAR (128)   NULL,
    [DISPLAY_NAME_CN] NVARCHAR (16)    NULL,
    [DISPLAY_NAME_EN] NVARCHAR (32)    NULL,
    [FIELD_TYPE]      INT              NULL,
    [DISPLAY_ORDER]   INT              NULL,
    [DISPLAY_WIDTH]   INT              NULL,
    [VALID_FLAG]      BIT              NULL,
    [CREATE_USER]     NVARCHAR (32)    NULL,
    [CREATE_DATE]     DATETIME         NULL,
    [MODIFY_USER]     NVARCHAR (32)    NULL,
    [MODIFY_DATE]     DATETIME         NULL,
    CONSTRAINT [PK_TM_SYS_REPORT_COLUMN] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'宽度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT_COLUMN', @level2type = N'COLUMN', @level2name = N'DISPLAY_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顺序', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT_COLUMN', @level2type = N'COLUMN', @level2name = N'DISPLAY_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'字段类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT_COLUMN', @level2type = N'COLUMN', @level2name = N'FIELD_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示英文名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT_COLUMN', @level2type = N'COLUMN', @level2name = N'DISPLAY_NAME_EN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示中文名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT_COLUMN', @level2type = N'COLUMN', @level2name = N'DISPLAY_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'字段名', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_REPORT_COLUMN', @level2type = N'COLUMN', @level2name = N'FIELD_NAME';

