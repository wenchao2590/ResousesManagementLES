CREATE TABLE [LES].[TI_BAS_EWO_BREAKPOINT_EXCUTE] (
    [SEQ_ID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [ZAENNR]       NVARCHAR (12)  NULL,
    [ZWERKS]       NVARCHAR (4)   NULL,
    [EXCUTE_VIN]   NVARCHAR (40)  NULL,
    [EXCUTE_TIME]  DATETIME       NULL,
    [PROCESS_FLAG] INT            NULL,
    [PROCESS_TIME] DATETIME       NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [CREATE_DATE]  DATETIME       NOT NULL,
    [CREATE_USER]  NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TI_BAS_EWO_BREAKPOINT_EXCUT] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'EXCUTE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '执行VIN号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'EXCUTE_VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'ZWERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '变更号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'ZAENNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MES接口_断点计划执行表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_EXCUTE';

