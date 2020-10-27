CREATE TABLE [LES].[TT_WMM_TRAN_DETAIL] (
    [TRAN_DETAIL_ID]    INT             IDENTITY (1, 1) NOT NULL,
    [TRAN_ID]           INT             NOT NULL,
    [WM_NO]             NVARCHAR (10)   NULL,
    [TRAN_NO]           NVARCHAR (50)   NULL,
    [PART_NO]           VARCHAR (30)    NULL,
    [PART_NICKNAME]     NVARCHAR (50)   NULL,
    [PART_CNAME]        VARCHAR (100)   NULL,
    [PACKAGE_MODEL]     NVARCHAR (30)   NULL,
    [S_DLOC]            NVARCHAR (20)   NULL,
    [D_DLOC]            NVARCHAR (20)   NULL,
    [PACKAGE]           NVARCHAR (100)  NULL,
    [NUM]               NUMERIC (18, 2) NULL,
    [BOX_NUM]           NUMERIC (18, 2) NULL,
    [MEASURING_UNIT_NO] VARCHAR (8)     NULL,
    [COMMENTS]          NVARCHAR (200)  NULL,
    [CREATE_USER]       NVARCHAR (50)   NULL,
    [CREATE_DATE]       DATETIME        NULL,
    [UPDATE_USER]       NVARCHAR (50)   NULL,
    [UPDATE_DATE]       DATETIME        NULL,
    CONSTRAINT [PK_TT_WMM_TRAN_DETAIL] PRIMARY KEY CLUSTERED ([TRAN_DETAIL_ID] ASC),
    CONSTRAINT [FK_TT_WMM_TRAN_DETAIL_TT_WMM_TRAN_HEAD] FOREIGN KEY ([TRAN_ID]) REFERENCES [LES].[TT_WMM_TRAN_HEAD] ([TRAN_ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_TRAN_DETAIL_1]
    ON [LES].[TT_WMM_TRAN_DETAIL]([WM_NO] ASC, [PART_NO] ASC, [NUM] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_TRAN_DETAIL]
    ON [LES].[TT_WMM_TRAN_DETAIL]([TRAN_ID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'D_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '源库库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'S_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易明细ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库交易数据记录操作Detail表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_TRAN_DETAIL';

