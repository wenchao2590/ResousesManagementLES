CREATE TABLE [dbo].[TS_SYS_CHART] (
    [Id]                 BIGINT           IDENTITY (1, 1) NOT NULL,
    [FID]                UNIQUEIDENTIFIER NULL,
    [NAME]               NVARCHAR (64)    NULL,
    [NAME_EN]            NVARCHAR (128)   NULL,
    [RECEIVER_LAYER]     NVARCHAR (32)    NULL,
    [IS_AUTH]            BIT              NULL,
    [CHART_TYPE]         NVARCHAR (32)    NULL,
    [MIX_CONDITION]      NVARCHAR (32)    NULL,
    [CHART_LABEL_NAME]   NVARCHAR (32)    NULL,
    [CHART_STYLE]        NVARCHAR (512)   NULL,
    [TIP_FORMAT]         NVARCHAR (512)   NULL,
    [CHART_WIDTH]        INT              NULL,
    [CHART_HIGHT]        INT              NULL,
    [CHART_COLUMN_NAME]  NVARCHAR (512)   NULL,
    [CHART_ROW_NAME]     NVARCHAR (512)   NULL,
    [SQL_STRING]         NVARCHAR (MAX)   NULL,
    [CHART_OTHER_FORMAT] NVARCHAR (512)   NULL,
    [REMARK]             NVARCHAR (512)   NULL,
    [VALID_FLAG]         BIT              NULL,
    [CREATE_USER]        NVARCHAR (32)    NULL,
    [CREATE_DATE]        DATETIME         NULL,
    [MODIFY_USER]        NVARCHAR (32)    NULL,
    [MODIFY_DATE]        DATETIME         NULL,
    CONSTRAINT [PK_TS_SYS_CHART] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'REMARK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表其他格式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'CHART_OTHER_FORMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'数据源语句', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'SQL_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表纵坐标名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'CHART_ROW_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表横坐标名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'CHART_COLUMN_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表高度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'CHART_HIGHT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表宽度', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'CHART_WIDTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'TIP格式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'TIP_FORMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表样式', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'CHART_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表标签名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'CHART_LABEL_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'混合条件', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'MIX_CONDITION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'图表类型', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'CHART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否授权', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'IS_AUTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'接受层', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'RECEIVER_LAYER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'英文名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'NAME_EN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'名称', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TS_SYS_CHART', @level2type = N'COLUMN', @level2name = N'NAME';

