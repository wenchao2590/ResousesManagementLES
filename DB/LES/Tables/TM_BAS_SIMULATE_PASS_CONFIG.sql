CREATE TABLE [LES].[TM_BAS_SIMULATE_PASS_CONFIG] (
    [ID]                 BIGINT        IDENTITY (1, 1) NOT NULL,
    [ASSEMBLY_LINE_FROM] NVARCHAR (16) NULL,
    [DCP_POINT_FROM]     NVARCHAR (16) NULL,
    [ASSEMBLY_LINE_TO]   NVARCHAR (16) NULL,
    [DCP_POINT_TO]       NVARCHAR (16) NULL,
    [DELAY_TYPE]         INT           NULL,
    [DELAY_COUNT]        INT           NULL,
    [PLANT]              NVARCHAR (8)  NULL,
    [VALID_FLAG]         BIT           NULL,
    [CREATE_USER]        NVARCHAR (50) NULL,
    [CREATE_DATE]        DATETIME      NULL,
    [MODIFY_USER]        NCHAR (10)    NULL,
    [MODIFY_DATE]        DATETIME      NULL,
    CONSTRAINT [PK_TM_BAS_SIMULATE_PASS_CONFIG] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否有效', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'延迟量级', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'DELAY_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'延迟方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'DELAY_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'终结信息点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'DCP_POINT_TO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'终结产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_TO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'起始信息点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'DCP_POINT_FROM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'起始产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE_FROM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_SIMULATE_PASS_CONFIG', @level2type = N'COLUMN', @level2name = N'ID';

