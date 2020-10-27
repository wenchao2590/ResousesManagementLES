CREATE TABLE [LES].[TT_SPM_VMI_TRAN_DETAIL] (
    [TRAN_DETAIL_ID]    INT             IDENTITY (1, 1) NOT NULL,
    [BILL_NO]           NVARCHAR (50)   NULL,
    [PLANT]             NVARCHAR (5)    NULL,
    [WM_NO]             NVARCHAR (10)   NULL,
    [ZONE_NO]           NVARCHAR (20)   NULL,
    [D_PLANT]           NVARCHAR (5)    NULL,
    [D_WM_NO]           NVARCHAR (10)   NULL,
    [D_ZONE_NO]         NVARCHAR (20)   NULL,
    [SUPPLIER_NUM]      NVARCHAR (12)   NULL,
    [PART_NO]           VARCHAR (30)    NULL,
    [PART_NICKNAME]     NVARCHAR (50)   NULL,
    [PART_CNAME]        VARCHAR (100)   NULL,
    [PACKAGE_MODEL]     NVARCHAR (30)   NULL,
    [DLOC]              NVARCHAR (20)   NULL,
    [D_DLOC]            NVARCHAR (20)   NULL,
    [PACKAGE]           NVARCHAR (100)  NULL,
    [NUM]               NUMERIC (18, 2) NULL,
    [BOX_NUM]           NUMERIC (18, 2) NULL,
    [MEASURING_UNIT_NO] VARCHAR (8)     NULL,
    [MODIFICATION_CODE] NVARCHAR (50)   NULL,
    [COMMENTS]          NVARCHAR (200)  NULL,
    [CREATE_USER]       NVARCHAR (50)   NULL,
    [CREATE_DATE]       DATETIME        NULL,
    [UPDATE_USER]       NVARCHAR (50)   NULL,
    [UPDATE_DATE]       DATETIME        NULL,
    CONSTRAINT [PK_TT_SPM_VMI_TRAN_DETAIL] PRIMARY KEY CLUSTERED ([TRAN_DETAIL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实收箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实收件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目标库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'D_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目标存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'D_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目标仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'D_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'BILL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL', @level2type = N'COLUMN', @level2name = N'TRAN_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SPM供应商门户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_VMI_TRAN_DETAIL';

