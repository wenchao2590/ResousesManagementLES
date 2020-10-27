CREATE TABLE [LES].[TE_PCS_PROCESS_VEHICLE_WORKTABLE] (
    [TMP_IDENTITY]        INT          IDENTITY (1, 1) NOT NULL,
    [TMP_SEQUENCE_NUMBER] INT          NULL,
    [TMP_KNR]             VARCHAR (20) NULL,
    [TMP_REGION]          VARCHAR (20) NULL,
    [TMP_PLANT]           VARCHAR (5)  NULL,
    [TMP_ASSEMBLYLINE]    VARCHAR (10) NULL,
    CONSTRAINT [IDX_PK_PROCESS_VEHICLE_WORKTABLE_TMP_IDENTITY] PRIMARY KEY CLUSTERED ([TMP_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_PROCESS_VEHICLE_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_ASSEMBLYLINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_PROCESS_VEHICLE_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_PROCESS_VEHICLE_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_REGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '临时KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_PROCESS_VEHICLE_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计算序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_PROCESS_VEHICLE_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_SEQUENCE_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '唯一标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_PROCESS_VEHICLE_WORKTABLE', @level2type = N'COLUMN', @level2name = N'TMP_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_PCS 位移处理临时表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_PCS_PROCESS_VEHICLE_WORKTABLE';

