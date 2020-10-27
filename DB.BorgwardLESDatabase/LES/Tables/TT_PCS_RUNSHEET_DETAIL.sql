CREATE TABLE [LES].[TT_PCS_RUNSHEET_DETAIL] (
    [RUNSHEET_DETAIL_ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PCS_RUNSHEET_SN]              INT            NOT NULL,
    [PLANT]                        NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)  NOT NULL,
    [LOCATION]                     NVARCHAR (20)  NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)  NOT NULL,
    [PART_NO]                      NVARCHAR (20)  NOT NULL,
    [PART_CNAME]                   NVARCHAR (300) NULL,
    [PART_ENAME]                   NVARCHAR (300) NULL,
    [DOCK]                         NVARCHAR (10)  NULL,
    [BOX_PARTS]                    NVARCHAR (10)  NOT NULL,
    [SEQUENCE_NO]                  INT            NULL,
    [PICKUP_SEQ_NO]                INT            NULL,
    [RDC_DLOC]                     VARCHAR (20)   NULL,
    [INHOUSE_PACKAGE]              INT            NOT NULL,
    [MEASURING_UNIT_NO]            VARCHAR (8)    NULL,
    [INHOUSE_PACKAGE_MODEL]        NVARCHAR (30)  NULL,
    [PACK_COUNT]                   INT            NOT NULL,
    [REQUIRED_INHOUSE_PACKAGE]     INT            NOT NULL,
    [REQUIRED_INHOUSE_PACKAGE_QTY] INT            NOT NULL,
    [ACTUAL_INHOUSE_PACKAGE]       INT            NULL,
    [ACTUAL_INHOUSE_PACKAGE_QTY]   INT            NULL,
    [COMMENTS]                     NVARCHAR (200) NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_DETAIL_RUNSHEET_DETAIL_ID] PRIMARY KEY CLUSTERED ([RUNSHEET_DETAIL_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_PCS_RUNSHEET_DETAIL]
    ON [LES].[TT_PCS_RUNSHEET_DETAIL]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BOX_PARTS] ASC, [LOCATION] ASC, [PART_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [NONCLUSTERED_PCS_RUNSHEET_SN_Index, sysname,>]
    ON [LES].[TT_PCS_RUNSHEET_DETAIL]([PCS_RUNSHEET_SN] ASC)
    INCLUDE([PART_NO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INHOUSE_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INHOUSE_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '捡料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PCS_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'RUNSHEET_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_PCS 拉动单明细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_PCS_RUNSHEET_DETAIL';

