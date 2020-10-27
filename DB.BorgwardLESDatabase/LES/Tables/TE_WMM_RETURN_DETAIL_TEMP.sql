CREATE TABLE [LES].[TE_WMM_RETURN_DETAIL_TEMP] (
    [RETURN_NO]   NVARCHAR (50)  NULL,
    [PART_NO]     NVARCHAR (20)  NULL,
    [PART_COUNT]  INT            NULL,
    [PACK_COUNT]  INT            NULL,
    [COMMENTS]    NVARCHAR (200) NULL,
    [CREATE_USER] NVARCHAR (50)  NULL,
    [CREATE_DATE] DATETIME       NULL,
    [ERROR_MSG]   NVARCHAR (200) NULL,
    [VALID_FLAG]  INT            NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实退箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'PART_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)退货单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_DETAIL_TEMP', @level2type = N'COLUMN', @level2name = N'RETURN_NO';

