CREATE TABLE [LES].[TT_SPS_MANUAL_PULL] (
    [SEQUENCE_NO]           BIGINT         NOT NULL,
    [PULL_FLAG]             NVARCHAR (10)  NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [BOX_PARTS]             NVARCHAR (10)  NOT NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [VIN]                   NVARCHAR (20)  NOT NULL,
    [SET_NUM]               INT            NOT NULL,
    [PROCESS_FLAG]          INT            NOT NULL,
    [PROCESS_TIME]          DATETIME       NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME       NULL,
    [TWD_RUNSHEET_NO]       NVARCHAR (22)  NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TT_SPS_MANUAL_PULL] PRIMARY KEY CLUSTERED ([SEQUENCE_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'TWD_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'期望到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'EXPECTED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理状态（0-初始化，1-生成拉动）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'成套标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'SET_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VIN号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'PULL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_MANUAL_PULL', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';

