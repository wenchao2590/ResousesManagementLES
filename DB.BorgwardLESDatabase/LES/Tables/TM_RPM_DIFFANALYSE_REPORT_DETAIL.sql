CREATE TABLE [LES].[TM_RPM_DIFFANALYSE_REPORT_DETAIL] (
    [REPORT_DETAIL_ID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [REPORT_ID]        BIGINT          NOT NULL,
    [WM_NO]            NVARCHAR (10)   NULL,
    [PACKAGE_NO]       NVARCHAR (30)   NOT NULL,
    [PACKAGE_CNAME]    NVARCHAR (100)  NULL,
    [PACKAGE_ENAME]    NVARCHAR (100)  NULL,
    [ZONE_NO]          NVARCHAR (20)   NOT NULL,
    [DLOC]             NVARCHAR (30)   NOT NULL,
    [LOCATION]         NVARCHAR (20)   NULL,
    [PACK_TYPE]        NVARCHAR (20)   NULL,
    [NUM]              NUMERIC (18, 2) NOT NULL,
    [REAL_NUM]         NUMERIC (18, 2) NULL,
    [DIFF_NUM]         NUMERIC (18, 2) NULL,
    [DIFF_REASON]      NVARCHAR (200)  NULL,
    [COMMENTS]         NVARCHAR (200)  NULL,
    [CREATE_USER]      NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]      DATETIME        NOT NULL,
    [UPDATE_USER]      NVARCHAR (50)   NULL,
    [UPDATE_DATE]      DATETIME        NULL,
    CONSTRAINT [IDX_PK_DIFFANALYSE_REPORT_DETAIL_REPORT_DETAIL_ID] PRIMARY KEY CLUSTERED ([REPORT_DETAIL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '差异原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'DIFF_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '差异', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'DIFF_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实盘数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'REAL_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '英文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装_包装器具编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '差异报表ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'REPORT_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '通知明细ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL', @level2type = N'COLUMN', @level2name = N'REPORT_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_包装差异明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_DIFFANALYSE_REPORT_DETAIL';

