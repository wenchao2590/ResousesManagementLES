CREATE TABLE [LES].[TT_SPS_RUNSHEET_DETAIL] (
    [RUNSHEET_DETAIL_ID]           INT            IDENTITY (1, 1) NOT NULL,
    [SPS_RUNSHEET_SN]              INT            NOT NULL,
    [PLANT]                        NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)  NOT NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)  NOT NULL,
    [PART_NO]                      NVARCHAR (20)  NOT NULL,
    [IDENTIFY_PART_NO]             NVARCHAR (20)  NULL,
    [PART_CNAME]                   NVARCHAR (300) NULL,
    [PART_ENAME]                   NVARCHAR (300) NULL,
    [DOCK]                         NVARCHAR (10)  NOT NULL,
    [BOX_PARTS]                    NVARCHAR (10)  NOT NULL,
    [SEQUENCE_NO]                  INT            NULL,
    [PICKUP_SEQ_NO]                INT            NULL,
    [RDC_DLOC]                     VARCHAR (20)   NULL,
    [INBOUND_PACKAGE]              INT            NOT NULL,
    [MEASURING_UNIT_NO]            NVARCHAR (50)  NULL,
    [INBOUND_PACKAGE_MODEL]        NVARCHAR (30)  NULL,
    [PACK_COUNT]                   INT            NOT NULL,
    [REQUIRED_INBOUND_PACKAGE]     INT            NOT NULL,
    [REQUIRED_INBOUND_PACKAGE_QTY] INT            NOT NULL,
    [ACTUAL_INBOUND_PACKAGE]       INT            NULL,
    [ACTUAL_INBOUND_PACKAGE_QTY]   INT            NULL,
    [MANUAL_LOCATION]              NVARCHAR (20)  NULL,
    CONSTRAINT [PK_TT_SPS_RUNSHEET_DETAIL] PRIMARY KEY CLUSTERED ([RUNSHEET_DETAIL_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPS_RUNSHEET_DETAIL_1]
    ON [LES].[TT_SPS_RUNSHEET_DETAIL]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [SUPPLIER_NUM] ASC, [BOX_PARTS] ASC, [PART_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPS_RUNSHEET_DETAIL]
    ON [LES].[TT_SPS_RUNSHEET_DETAIL]([SPS_RUNSHEET_SN] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'MANUAL_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商库位（源库位）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'RDC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'检料顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PICKUP_SEQ_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'标识零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'IDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'SPS_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_RUNSHEET_DETAIL', @level2type = N'COLUMN', @level2name = N'RUNSHEET_DETAIL_ID';

