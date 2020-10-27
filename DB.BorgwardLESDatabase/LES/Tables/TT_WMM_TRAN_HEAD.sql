CREATE TABLE [LES].[TT_WMM_TRAN_HEAD] (
    [TRAN_ID]        INT            IDENTITY (1, 1) NOT NULL,
    [TRAN_NO]        NVARCHAR (50)  NULL,
    [PLANT]          NVARCHAR (5)   NULL,
    [S_WM_NO]        NVARCHAR (10)  NULL,
    [S_ZONE_NO]      NVARCHAR (20)  NULL,
    [O_WM_NO]        NVARCHAR (10)  NULL,
    [O_ZONE_NO]      NVARCHAR (20)  NULL,
    [TRAN_TYPE]      INT            NULL,
    [TRAN_TIME]      DATETIME       NULL,
    [BATCH_NO]       NVARCHAR (50)  NULL,
    [FINANCIAL_CODE] NVARCHAR (100) NULL,
    [COST_CODE]      NVARCHAR (100) NULL,
    [INTERNAL_CODE]  NVARCHAR (100) NULL,
    [WBS_CODE]       NVARCHAR (100) NULL,
    [TRAN_STATUS]    INT            NULL,
    [PUBLISH_TIME]   DATETIME       NULL,
    [TRAN_KEEPER]    NVARCHAR (50)  NULL,
    [PUBLISH_KEEPER] NVARCHAR (50)  NULL,
    [COMMENTS]       NVARCHAR (200) NULL,
    [CREATE_USER]    NVARCHAR (50)  NULL,
    [CREATE_DATE]    DATETIME       NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    [ERP_FLAG]       INT            NULL,
    CONSTRAINT [PK_TT_WMM_TRAN_HEAD] PRIMARY KEY CLUSTERED ([TRAN_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_TRAN_HEAD_2]
    ON [LES].[TT_WMM_TRAN_HEAD]([ERP_FLAG] ASC, [TRAN_STATUS] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_TRAN_HEAD_1]
    ON [LES].[TT_WMM_TRAN_HEAD]([PLANT] ASC, [S_WM_NO] ASC, [S_ZONE_NO] ASC, [O_WM_NO] ASC, [O_ZONE_NO] ASC, [TRAN_TYPE] ASC, [TRAN_TIME] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_TRAN_HEAD]
    ON [LES].[TT_WMM_TRAN_HEAD]([TRAN_NO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否为SAP工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'PUBLISH_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'TRAN_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发单时间时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '盘点状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'TRAN_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WBS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'WBS_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '内部订单编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'INTERNAL_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '成本中心编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'COST_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '财务科目编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'FINANCIAL_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '批次号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'O_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'O_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '源存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'S_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '源仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'S_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD', @level2type = N'COLUMN', @level2name = N'TRAN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库交易数据记录HEAD表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_HEAD';

