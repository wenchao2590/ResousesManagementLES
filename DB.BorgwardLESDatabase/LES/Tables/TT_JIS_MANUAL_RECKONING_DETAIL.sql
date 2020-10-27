CREATE TABLE [LES].[TT_JIS_MANUAL_RECKONING_DETAIL] (
    [RECKONING_DETAIL_SN]          INT             IDENTITY (1, 1) NOT NULL,
    [RECKONING_SN]                 INT             NOT NULL,
    [PLANT]                        NVARCHAR (5)    NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)   NULL,
    [MODEL]                        NVARCHAR (10)   NULL,
    [PART_NO]                      NVARCHAR (20)   NULL,
    [CACULATE_POINT]               NVARCHAR (15)   NULL,
    [SOP_FLAG]                     INT             NULL,
    [BOX_PARTS]                    NVARCHAR (20)   NOT NULL,
    [RACK]                         NVARCHAR (20)   NOT NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)   NOT NULL,
    [PART_CNAME]                   NVARCHAR (100)  NULL,
    [KNR]                          NVARCHAR (20)   NOT NULL,
    [VIN]                          NVARCHAR (20)   NULL,
    [RUNNING_NUMBER]               NVARCHAR (5)    NOT NULL,
    [MODEL_NO]                     NVARCHAR (8)    NULL,
    [USAGE]                        NUMERIC (18, 2) NULL,
    [MEASURING_UNIT_NO]            VARCHAR (8)     NULL,
    [PART_NICK_NAME]               NVARCHAR (30)   NULL,
    [JIS_RUNSHEET_FLEX_TIME]       DATETIME        NOT NULL,
    [BARCODE_DATA]                 NVARCHAR (200)  NULL,
    [REQUIRED_INBOUND_PACKAGE_QTY] INT             NULL,
    [ACTUAL_INBOUND_PACKAGE_QTY]   INT             NULL,
    [WMSIDS]                       NVARCHAR (1000) NULL,
    [WAREHOUSE]                    NVARCHAR (300)  NULL,
    [COMMENTS]                     NVARCHAR (200)  NULL,
    [RECKONING_NO]                 NVARCHAR (100)  NULL,
    [UPDATE_DATE]                  DATETIME        NULL,
    [UPDATE_USER]                  NVARCHAR (50)   NULL,
    [CREATE_DATE]                  DATETIME        NOT NULL,
    [CREATE_USER]                  NVARCHAR (50)   NOT NULL,
    CONSTRAINT [IDX_PK_MANUAL_RECKONING_DETAIL_RECKONING_DETAIL_SN] PRIMARY KEY CLUSTERED ([RECKONING_DETAIL_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'RECKONING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMENTS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'WAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WMSID明细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'WMSIDS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实收数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'ACTUAL_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'REQUIRED_INBOUND_PACKAGE_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_FLEX_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NICK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'USAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过点序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'RUNNING_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件大类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_SOP标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'SOP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'CACULATE_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算报表号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'RECKONING_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算明细序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL', @level2type = N'COLUMN', @level2name = N'RECKONING_DETAIL_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_JIS手工生成结算单明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_MANUAL_RECKONING_DETAIL';

