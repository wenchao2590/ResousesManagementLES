CREATE TABLE [LES].[TE_WMM_OUTPUT_DETAIL_TEMP] (
    [OUTPUT_DETAIL_ID] INT            IDENTITY (1, 1) NOT NULL,
    [OUTPUT_NO]        NVARCHAR (50)  NULL,
    [PART_NO]          NVARCHAR (20)  NULL,
    [REQUIRED_BOX_NUM] INT            NULL,
    [REQUIRED_QTY]     INT            NULL,
    [ACTUAL_BOX_NUM]   INT            NULL,
    [ACTUAL_QTY]       INT            NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [UPDATE_DATE]      DATETIME       NULL,
    [UPDATE_USER]      NVARCHAR (50)  NULL,
    [CREATE_DATE]      DATETIME       NULL,
    [CREATE_USER]      NVARCHAR (50)  NULL,
    [ERROR_MSG]        NVARCHAR (200) NULL,
    [VALID_FLAG]       INT            NULL,
    CONSTRAINT [PK_TE_WMM_OUTPUT_DETAIL_TEMP] PRIMARY KEY CLUSTERED ([OUTPUT_DETAIL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'ACTUAL_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'ACTUAL_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'REQUIRED_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'REQUIRED_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？出库单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'OUTPUT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？出库明细编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'OUTPUT_DETAIL_ID';

