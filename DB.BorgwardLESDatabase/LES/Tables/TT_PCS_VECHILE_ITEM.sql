CREATE TABLE [LES].[TT_PCS_VECHILE_ITEM] (
    [SEQUENCE_NUMBER] INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]           NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)  NOT NULL,
    [ITEM_ID]         VARCHAR (20)   NULL,
    [DCP_NAME]        NVARCHAR (100) NULL,
    [TIMESTAMP]       DATETIME       NULL,
    [ENTRY_TIME]      VARCHAR (21)   NULL,
    [PROCESSED_FLAG]  NCHAR (1)      NULL,
    [KNR]             NVARCHAR (8)   NULL,
    [RUNNING_NO]      INT            NULL,
    CONSTRAINT [IDX_PK_VECHILE_ITEM_SEQUENCE_NUMBER] PRIMARY KEY CLUSTERED ([SEQUENCE_NUMBER] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_PCS_VECHILE_ITEM_1]
    ON [LES].[TT_PCS_VECHILE_ITEM]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [ITEM_ID] ASC, [DCP_NAME] ASC, [TIMESTAMP] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_PCS_VECHILE_ITEM]
    ON [LES].[TT_PCS_VECHILE_ITEM]([PROCESSED_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '接口_车辆识别号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'PROCESSED_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'ENTRY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '时间戳', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'TIMESTAMP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '产品唯一号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'ITEM_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_PCS 车辆过点表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_VECHILE_ITEM';

