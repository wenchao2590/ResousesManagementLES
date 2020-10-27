CREATE TABLE [LES].[TT_BAS_RECKONING_DETAILS] (
    [RECKONING_DETAIL_SN] INT            IDENTITY (1, 1) NOT NULL,
    [RECKONING_SN]        INT            NOT NULL,
    [PLANT]               NVARCHAR (5)   NULL,
    [ASSEMBLY_LINE]       NVARCHAR (10)  NULL,
    [MODEL]               NVARCHAR (10)  NULL,
    [PART_NO]             NVARCHAR (20)  NULL,
    [BOX_PARTS]           NVARCHAR (20)  NOT NULL,
    [SUPPLIER_NUM]        NVARCHAR (12)  NOT NULL,
    [PART_CNAME]          NVARCHAR (300) NULL,
    [TOTAL_QTY]           INT            NULL,
    [CONFIRM_QTY]         INT            NULL,
    [DETAIL_ID]           INT            NULL,
    [MEASURING_UNIT_NO]   VARCHAR (8)    NULL,
    [PART_NICK_NAME]      NVARCHAR (30)  NULL,
    [BARCODE_DATA]        NVARCHAR (50)  NULL,
    [COMMENTS]            NVARCHAR (200) NULL,
    CONSTRAINT [PK_TT_BAS_RECKONING_DETAILS] PRIMARY KEY NONCLUSTERED ([RECKONING_DETAIL_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'PART_NICK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '明细ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '确认数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'CONFIRM_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '总数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'TOTAL_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件大类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算报表号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'RECKONING_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算明细序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS', @level2type = N'COLUMN', @level2name = N'RECKONING_DETAIL_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_基础送货单明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_RECKONING_DETAILS';

