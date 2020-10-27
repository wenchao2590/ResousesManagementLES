CREATE TABLE [LES].[TT_PCS_VECHILE_ITEM_BY_SEQUENCE] (
    [SEQUENCE_NUMBER]        INT           IDENTITY (1, 1) NOT NULL,
    [PLANT]                  NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE]          NVARCHAR (10) NOT NULL,
    [ITEM_ID]                VARCHAR (20)  NULL,
    [REGION_NAME]            NVARCHAR (50) NULL,
    [TIMESTAMP]              DATETIME      NULL,
    [ENTRY_TIME]             DATETIME      NULL,
    [PROCESSED_FLAG]         NCHAR (1)     NULL,
    [KNR]                    NVARCHAR (8)  NULL,
    [RUNNING_NO]             INT           NULL,
    [SEQUENCE_ASSEMBLY_LINE] INT           NULL,
    [TOTAL_TAKT]             INT           NULL,
    [CURRENT_TAKT]           INT           NULL,
    [REGION_IDENTITY]        INT           NULL,
    CONSTRAINT [IDX_PK_VECHILE_ITEM_BY_SEQUENCE_SEQUENCE_NUMBER] PRIMARY KEY CLUSTERED ([SEQUENCE_NUMBER] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'REGION ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'REGION_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '当前车辆位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'CURRENT_TAKT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '总的节拍数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'TOTAL_TAKT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线车辆序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'SEQUENCE_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '接口_车辆识别号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'PROCESSED_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'ENTRY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '时间戳', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'TIMESTAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS扫描区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'REGION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '产品唯一号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'ITEM_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_PCS 车辆顺序表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM_BY_SEQUENCE';

