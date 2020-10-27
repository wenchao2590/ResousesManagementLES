CREATE TABLE [LES].[TE_WMM_RETURN_TEMP] (
    [RETURN_NO]        NVARCHAR (50)  NULL,
    [RETURN_TYPE]      INT            NULL,
    [PLANT]            NVARCHAR (100) NULL,
    [WM_NO]            NVARCHAR (10)  NULL,
    [ZONE_NO]          NVARCHAR (20)  NULL,
    [SUPPLIER_NUM]     NVARCHAR (12)  NULL,
    [SEND_TIME]        DATETIME       NULL,
    [PART_NO]          NVARCHAR (20)  NULL,
    [PART_COUNT]       INT            NULL,
    [PACK_COUNT]       INT            NULL,
    [RETURN_REASON]    NVARCHAR (200) NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [CREATE_DATE]      DATETIME       NULL,
    [CREATE_USER]      NVARCHAR (50)  NULL,
    [ERROR_MSG]        NVARCHAR (200) NULL,
    [VALID_FLAG]       INT            NULL,
    [RETURN_TYPE_TEXT] NVARCHAR (30)  NULL,
    [PART_COUNT_TEXT]  NVARCHAR (30)  NULL,
    [PACK_COUNT_TEXT]  NVARCHAR (30)  NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'PACK_COUNT_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'PART_COUNT_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'RETURN_TYPE_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)退货原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'RETURN_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)当前计数器数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'PART_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)退货单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)退货类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'RETURN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)退货单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RETURN_TEMP', @level2type = N'COLUMN', @level2name = N'RETURN_NO';

