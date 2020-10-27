CREATE TABLE [LES].[TT_RPM_PACKAGE_TRAN_DETAIL] (
    [TRAN_DETAIL_ID]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [TRAN_ID]              BIGINT          NOT NULL,
    [PLANT]                NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]        NVARCHAR (10)   NULL,
    [SUPPLIER_NUM]         NVARCHAR (12)   NOT NULL,
    [WM_NO]                NVARCHAR (10)   NULL,
    [ZONE_NO]              NVARCHAR (20)   NOT NULL,
    [DLOC]                 NVARCHAR (30)   NULL,
    [TRAN_NO]              NVARCHAR (10)   NULL,
    [TARGET_WM]            NVARCHAR (10)   NULL,
    [TARGET_ZONE]          NVARCHAR (20)   NULL,
    [TARGET_DLOC]          NVARCHAR (30)   NULL,
    [MEASURING_UNIT_NO]    VARCHAR (8)     NULL,
    [PACKAGE]              NVARCHAR (100)  NULL,
    [NUM]                  NUMERIC (18, 2) NOT NULL,
    [PACKAGE_NO]           NVARCHAR (30)   NOT NULL,
    [PACKAGE_CNAME]        NVARCHAR (100)  NULL,
    [PACKAGE_ENAME]        NVARCHAR (100)  NULL,
    [DOCK]                 NVARCHAR (10)   NULL,
    [SEQUENCE_NO]          INT             NULL,
    [PICKUP_SEQ_NO]        INT             NULL,
    [PACK_COUNT]           INT             NULL,
    [REQUIRED_PACKAGE]     NUMERIC (18, 2) NULL,
    [REQUIRED_PACKAGE_QTY] NUMERIC (18, 2) NULL,
    [ACTUAL_PACKAGE]       NUMERIC (18, 2) NULL,
    [ACTUAL_PACKAGE_QTY]   NUMERIC (18, 2) NULL,
    [BARCODE_DATA]         NVARCHAR (50)   NULL,
    [COMMENTS]             NVARCHAR (200)  NULL,
    [Current_BOX_NUM]      INT             NULL,
    [PART_NO]              NVARCHAR (50)   NULL,
    CONSTRAINT [PK_RPM_PACKAGE_TRAN_DETAIL_ID] PRIMARY KEY CLUSTERED ([TRAN_DETAIL_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_RPM_PACKAGE_TRAN_DETAIL_1]
    ON [LES].[TT_RPM_PACKAGE_TRAN_DETAIL]([PACKAGE_NO] ASC, [PLANT] ASC, [WM_NO] ASC, [TARGET_WM] ASC, [SUPPLIER_NUM] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_RPM_PACKAGE_TRAN_DETAIL]
    ON [LES].[TT_RPM_PACKAGE_TRAN_DETAIL]([TRAN_ID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '英文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装_包装器具编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的存贮区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目的仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TARGET_WM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_包装交易明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_RPM_PACKAGE_TRAN_DETAIL';

