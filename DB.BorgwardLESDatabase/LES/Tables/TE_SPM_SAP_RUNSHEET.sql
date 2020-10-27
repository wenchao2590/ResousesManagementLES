CREATE TABLE [LES].[TE_SPM_SAP_RUNSHEET] (
    [PLAN_RUNSHEET_SN] INT           NOT NULL,
    [PROCESS_STATUS]   INT           CONSTRAINT [DF_LES]].[TE_SPM_SAP_RUNSHEET_PROCESS_STATUS] DEFAULT ((0)) NULL,
    [PROCESS_TIME]     DATETIME      NULL,
    [CREATE_DATE]      DATETIME      NULL,
    [CREATE_USER]      NVARCHAR (50) NULL,
    CONSTRAINT [PK_LES]].[TE_SPM_SAP_RUNSHEET] PRIMARY KEY CLUSTERED ([PLAN_RUNSHEET_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_SAP_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_SAP_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_SAP_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理状态（0-未处理，1-已处理）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_SAP_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PROCESS_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交货单SN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_SAP_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLAN_RUNSHEET_SN';

