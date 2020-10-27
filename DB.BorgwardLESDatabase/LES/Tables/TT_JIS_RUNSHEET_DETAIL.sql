CREATE TABLE [LES].[TT_JIS_RUNSHEET_DETAIL] (
    [JIS_RUNSHEET_FLEX_SN] INT             NOT NULL,
    [JIS_RUNSHEET_BOX_SN]  INT             NOT NULL,
    [JIS_RUNSHEET_PART_SN] INT             NOT NULL,
    [PART_NO]              NVARCHAR (20)   NULL,
    [PLANT]                NVARCHAR (5)    NULL,
    [ASSEMBLY_LINE]        NVARCHAR (10)   NULL,
    [PART_CNAME]           NVARCHAR (300)  NULL,
    [USAGE]                NUMERIC (18, 2) NULL,
    [PART_NICK_NAME]       NVARCHAR (30)   NULL,
    [BARCODE_DATA]         NVARCHAR (50)   NULL,
    [ORDER_NO]             NVARCHAR (50)   NULL,
    [ITEM_NO]              NVARCHAR (50)   NULL,
    [COMMENTS]             NVARCHAR (200)  NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_DETAIL_JIS_RUNSHEET_FLEX_SN_JIS_RUNSHEET_BOX_SN_JIS_RUNSHEET_PART_SN] PRIMARY KEY CLUSTERED ([JIS_RUNSHEET_FLEX_SN] ASC, [JIS_RUNSHEET_BOX_SN] ASC, [JIS_RUNSHEET_PART_SN] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_JIS_RUNSHEET_DETAIL]
    ON [LES].[TT_JIS_RUNSHEET_DETAIL]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [PART_NO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ITEM号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ITEM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NICK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'USAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拆分零件序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_PART_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拆分BOX序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_BOX_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拆分序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_FLEX_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_JIS 拉动单零件明细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_DETAIL';

