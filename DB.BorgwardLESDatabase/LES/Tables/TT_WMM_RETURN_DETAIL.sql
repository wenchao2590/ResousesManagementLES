CREATE TABLE [LES].[TT_WMM_RETURN_DETAIL] (
    [RETURN_DETAIL_ID]   INT            IDENTITY (1, 1) NOT NULL,
    [RETURN_ID]          INT            NULL,
    [PLANT]              NVARCHAR (100) NULL,
    [SUPPLIER_NUM]       NVARCHAR (12)  NULL,
    [WM_NO]              NVARCHAR (10)  NULL,
    [ZONE_NO]            NVARCHAR (20)  NULL,
    [DLOC]               NVARCHAR (30)  NULL,
    [PART_NO]            NVARCHAR (20)  NULL,
    [PART_CNAME]         VARCHAR (200)  NULL,
    [PART_CLS]           NVARCHAR (50)  NULL,
    [PART_COUNT]         INT            NULL,
    [PACK_COUNT]         INT            NULL,
    [PACK_SIZE]          INT            NULL,
    [PACKAGE_MODEL]      NVARCHAR (30)  NULL,
    [ROUTE_CODE]         VARCHAR (3)    NULL,
    [RETURN_REASON]      VARCHAR (100)  NULL,
    [RETURN_REPORT_FLAG] INT            NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [CREATE_USER]        NVARCHAR (50)  NULL,
    [CREATE_DATE]        DATETIME       NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    CONSTRAINT [PK_TT_WMM_RETURN_DETAIL] PRIMARY KEY CLUSTERED ([RETURN_DETAIL_ID] ASC),
    CONSTRAINT [FK_TT_WMM_RETURN_DETAIL_TT_WMM_RETURN] FOREIGN KEY ([RETURN_ID]) REFERENCES [LES].[TT_WMM_RETURN] ([RETURN_ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_RETURN_DETAIL_1]
    ON [LES].[TT_WMM_RETURN_DETAIL]([PLANT] ASC, [WM_NO] ASC, [ZONE_NO] ASC, [PART_NO] ASC, [SUPPLIER_NUM] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_RETURN_DETAIL]
    ON [LES].[TT_WMM_RETURN_DETAIL]([RETURN_ID] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'RETURN_REPORT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '退货原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'RETURN_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'ROUTE_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实退件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_SIZE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实退箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '退货流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'RETURN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '退货明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL', @level2type = N'COLUMN', @level2name = N'RETURN_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_退货明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN_DETAIL';

