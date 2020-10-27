CREATE TABLE [GJS].[TT_WMS_CLJCD] (
    [ID]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [JCDH]        NVARCHAR (32)   NULL,
    [JCLB]        INT             NULL,
    [JCSJ]        DATETIME        NULL,
    [GYSDM]       NVARCHAR (16)   NULL,
    [GYSMC]       NVARCHAR (32)   NULL,
    [ZSL]         DECIMAL (18, 4) NULL,
    [ZMZ]         DECIMAL (18, 4) NULL,
    [ZTJ]         DECIMAL (18, 4) NULL,
    [ZJE]         DECIMAL (18)    NULL,
    [ZT]          INT             NULL,
    [BM]          NVARCHAR (16)   NULL,
    [ZDWCBJ]      BIT             NULL,
    [ZDWCSJ]      DATETIME        NULL,
    [ZDWCR]       NVARCHAR (32)   NULL,
    [ZGSHBJ]      BIT             NULL,
    [ZGSHSJ]      DATETIME        NULL,
    [ZG]          NVARCHAR (32)   NULL,
    [CWSHBJ]      BIT             NULL,
    [CWSHSJ]      DATETIME        NULL,
    [CW]          NVARCHAR (32)   NULL,
    [HSXM]        INT             NULL,
    [CK_CODE]     NVARCHAR (8)    NULL,
    [CK_NAME]     NVARCHAR (64)   NULL,
    [BZ]          NVARCHAR (256)  NULL,
    [VALID_FLAG]  BIT             NULL,
    [CREATE_USER] NVARCHAR (32)   NULL,
    [CREATE_DATE] DATETIME        NULL,
    [MODIFY_USER] NVARCHAR (32)   NULL,
    [MODIFY_DATE] DATETIME        NULL,
    CONSTRAINT [PK_TT_WMM_CLJCD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'BZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'CK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'CK_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'核算项目', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'HSXM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'财务', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'CW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'财务审核时间', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'CWSHSJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'财务审核标记', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'CWSHBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主管', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主管审核时间', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZGSHSJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'主管审核标记', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZGSHBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'制单完成人', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZDWCR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'制单完成时间', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZDWCSJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'制单完成标记', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZDWCBJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'采购部门', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'BM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总体积', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZTJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总毛重', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZMZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'总数量', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'ZSL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商名称', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'GYSMC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'GYSDM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库时间', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'JCSJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库类别', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'JCLB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入库单号', @level0type = N'SCHEMA', @level0name = N'GJS', @level1type = N'TABLE', @level1name = N'TT_WMS_CLJCD', @level2type = N'COLUMN', @level2name = N'JCDH';

