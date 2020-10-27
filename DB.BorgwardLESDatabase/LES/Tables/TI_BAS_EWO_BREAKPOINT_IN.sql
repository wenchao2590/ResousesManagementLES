CREATE TABLE [LES].[TI_BAS_EWO_BREAKPOINT_IN] (
    [SEQ_ID]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [ZAENNR]       NVARCHAR (12)  NULL,
    [ZWERKS]       NVARCHAR (4)   NULL,
    [ZMATNR]       NVARCHAR (18)  NULL,
    [ZMAT1]        NVARCHAR (18)  NULL,
    [ZMAT1_DES]    NVARCHAR (40)  NULL,
    [ZMAT2]        NVARCHAR (18)  NULL,
    [ZMAT2_DES]    NVARCHAR (40)  NULL,
    [ZDATUV]       NVARCHAR (8)   NULL,
    [DEAL_FLAG]    INT            NULL,
    [PROCESS_FLAG] INT            NULL,
    [PROCESS_TIME] DATETIME       NULL,
    [COMMENTS]     NVARCHAR (200) NULL,
    [UPDATE_DATE]  DATETIME       NULL,
    [UPDATE_USER]  NVARCHAR (50)  NULL,
    [CREATE_DATE]  DATETIME       NOT NULL,
    [CREATE_USER]  NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TI_BAS_EWO_BREAKPOINT_IN] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '预计切换日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'ZDATUV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '新零件描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'ZMAT2_DES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '新零件', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'ZMAT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '旧零件描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'ZMAT1_DES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '旧零件', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'ZMAT1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'ZMATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'ZWERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '变更号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'ZAENNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_APO_断点计划', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_EWO_BREAKPOINT_IN';

