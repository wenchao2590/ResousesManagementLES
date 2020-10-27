CREATE TABLE [LES].[TE_JIS_RUNSHEET_FLEX_RECKONING] (
    [SEQ_ID]                 INT             IDENTITY (1, 1) NOT NULL,
    [JIS_RUNSHEET_FLEX_SN]   INT             NOT NULL,
    [JIS_RUNSHEET_FLEX_TIME] DATETIME        NOT NULL,
    [PLANT]                  NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]          NVARCHAR (10)   NOT NULL,
    [SUPPLIER_NUM]           NVARCHAR (12)   NOT NULL,
    [RACK]                   NVARCHAR (20)   NOT NULL,
    [BOX_NUMBER]             INT             NULL,
    [FORMAT]                 NVARCHAR (6)    NULL,
    [MODEL]                  NVARCHAR (10)   NULL,
    [CAR_NO]                 NVARCHAR (8)    NULL,
    [RUNNING_NUMBER]         NVARCHAR (5)    NULL,
    [JIS_RUNSHEET_SN]        INT             NULL,
    [JIS_RUNSHEET_NO]        NVARCHAR (20)   NULL,
    [CREATE_DATE]            DATETIME        NULL,
    [SAP_FLAG]               INT             NULL,
    [VIN]                    NVARCHAR (20)   NULL,
    [MODEL_NO]               NVARCHAR (8)    NULL,
    [PART_CNAME]             NVARCHAR (100)  NULL,
    [USAGE]                  NUMERIC (18, 2) NULL,
    [PART_NICK_NAME]         NVARCHAR (30)   NULL,
    [BARCODE_DATA]           NVARCHAR (50)   NULL,
    [ORDER_NO]               NVARCHAR (50)   NULL,
    [ITEM_NO]                NVARCHAR (50)   NULL,
    [PART_NO]                NVARCHAR (20)   NULL,
    [CACULATE_POINT]         NVARCHAR (15)   NULL,
    [CACULATE_TERM]          INT             NULL,
    [CACULATE_CHECK_POINT]   NVARCHAR (15)   NULL,
    [BOX_PARTS]              NVARCHAR (20)   NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_FLEX_RECKONING_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？结算点校验点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'CACULATE_CHECK_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？结算周期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'CACULATE_TERM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？结算点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'CACULATE_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？ITEM号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'ITEM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'PART_NICK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'USAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？结算标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'SAP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？拉动单号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？拉动单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？过点序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'RUNNING_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？Knr', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'CAR_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？格式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'FORMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'BOX_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_FLEX_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？拆分序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_FLEX_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_JIS_RUNSHEET_FLEX_RECKONING', @level2type = N'COLUMN', @level2name = N'SEQ_ID';

