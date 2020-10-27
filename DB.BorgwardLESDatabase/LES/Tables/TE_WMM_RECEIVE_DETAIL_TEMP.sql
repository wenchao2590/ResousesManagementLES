CREATE TABLE [LES].[TE_WMM_RECEIVE_DETAIL_TEMP] (
    [RECEIVE_NO]       NVARCHAR (50)  NULL,
    [PART_NO]          NVARCHAR (20)  NULL,
    [REQUIRED_BOX_NUM] NVARCHAR (20)  NULL,
    [REQUIRED_QTY]     NVARCHAR (20)  NULL,
    [ACTUAL_BOX_NUM]   INT            NULL,
    [ACTUAL_QTY]       INT            NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [CREATE_USER]      NVARCHAR (50)  NULL,
    [CREATE_DATE]      DATETIME       NULL,
    [ERROR_MSG]        NVARCHAR (200) NULL,
    [VALID_FLAG]       INT            NULL,
    [Current_BOX_NUM]  NVARCHAR (20)  NULL,
    [Current_QTY]      NVARCHAR (20)  NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'Current_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'Current_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'ACTUAL_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'ACTUAL_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'REQUIRED_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'REQUIRED_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入库单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'RECEIVE_NO';

