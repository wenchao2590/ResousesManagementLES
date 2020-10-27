CREATE TABLE [LES].[TE_WMM_TRAN_HEAD_TEMP] (
    [TRAN_ID]        INT             IDENTITY (1, 1) NOT NULL,
    [TRAN_NO]        NVARCHAR (50)   NULL,
    [PLANT]          NVARCHAR (100)  NULL,
    [S_WM_NO]        NVARCHAR (10)   NULL,
    [S_ZONE_NO]      NVARCHAR (20)   NULL,
    [O_WM_NO]        NVARCHAR (10)   NULL,
    [O_ZONE_NO]      NVARCHAR (20)   NULL,
    [TRAN_TYPE]      NVARCHAR (50)   NULL,
    [TRAN_TIME]      DATETIME        NULL,
    [COST_CODE]      NVARCHAR (100)  NULL,
    [FINANCIAL_CODE] NVARCHAR (100)  NULL,
    [INTERNAL_CODE]  NVARCHAR (100)  NULL,
    [WBS_CODE]       NVARCHAR (100)  NULL,
    [PART_NO]        NVARCHAR (20)   NULL,
    [BOX_NUM]        NUMERIC (18, 2) NULL,
    [NUM]            NUMERIC (18, 2) NULL,
    [PUBLISH_TIME]   DATETIME        NULL,
    [COMMENTS]       NVARCHAR (200)  NULL,
    [UPDATE_DATE]    DATETIME        NULL,
    [UPDATE_USER]    NVARCHAR (50)   NULL,
    [CREATE_DATE]    DATETIME        NULL,
    [CREATE_USER]    NVARCHAR (50)   NULL,
    [ERROR_MSG]      NVARCHAR (200)  NULL,
    [VALID_FLAG]     INT             NULL,
    [BOX_NUM_TEXT]   NVARCHAR (30)   NULL,
    [NUM_TEXT]       NVARCHAR (30)   NULL,
    CONSTRAINT [PK_TE_WMM_TRAN_TEMP] PRIMARY KEY CLUSTERED ([TRAN_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'NUM_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'BOX_NUM_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)发布时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)WBS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'WBS_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)内部订单编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'INTERNAL_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)财务科目编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'FINANCIAL_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)成本中心编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'COST_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)交易时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)承运类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目的存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'O_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目的仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'O_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)源存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'S_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)源仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'S_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)交易单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)交易流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_TRAN_HEAD_TEMP', @level2type = N'COLUMN', @level2name = N'TRAN_ID';

