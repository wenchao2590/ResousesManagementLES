CREATE TABLE [LES].[TT_TWD_CONSUME_COUNTER] (
    [ID]                    INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)   NULL,
    [PLANT_ZONE]            NVARCHAR (5)    NULL,
    [WORKSHOP]              NVARCHAR (4)    NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NULL,
    [MODEL]                 NVARCHAR (10)   NULL,
    [PART_NO]               NVARCHAR (20)   NOT NULL,
    [INDENTIFY_PART_NO]     NVARCHAR (20)   NULL,
    [AMOUNTRATIO]           INT             NULL,
    [INBOUND_PART_CLASS]    NVARCHAR (10)   NULL,
    [MEASURING_UNIT_NO]     VARCHAR (8)     NULL,
    [PART_CNAME]            NVARCHAR (100)  NULL,
    [PART_ENAME]            NVARCHAR (100)  NULL,
    [DOCK]                  NVARCHAR (10)   NULL,
    [INHOUSE_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INHOUSE_PACKAGE]       INT             NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INBOUND_PACKAGE]       INT             NULL,
    [CURRENT_PART_COUNT]    NUMERIC (18, 2) NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [CREATE_DATE]           DATETIME        NULL,
    [CREATE_USER]           NVARCHAR (50)   NULL,
    [PALLET_NO]             NVARCHAR (30)   NULL,
    [PALLET_NAME]           NVARCHAR (100)  NULL,
    [PALLET_PACKAGE]        INT             NULL,
    [PALLET_COUNT]          INT             NULL,
    [TOP_COVER_NO]          NVARCHAR (30)   NULL,
    [TOP_COVER_NAME]        NVARCHAR (100)  NULL,
    [TOP_COVER_PACKAGE]     INT             NULL,
    [PULL_MODE]             INT             NULL,
    [MIN]                   INT             NULL,
    [MAX]                   INT             NULL,
    [CURRENT_PULL_COUNT]    INT             NULL,
    [MIN_PULL_BOX]          INT             NULL,
    [BATCH_PULL_BOX]        INT             NULL,
    CONSTRAINT [IDX_PK_CONSUME_COUNTER_ID] PRIMARY KEY NONCLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批量拉动箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'BATCH_PULL_BOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小拉动箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'MIN_PULL_BOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顶盖包装数量      ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'TOP_COVER_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顶盖名称          ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'TOP_COVER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'顶盖编号          ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'TOP_COVER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘数            ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PALLET_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘包装数量      ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PALLET_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘名称          ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PALLET_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'托盘编号          ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PALLET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注       ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'当前数量          ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'CURRENT_PART_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂包装数量      ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂包装型号      ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装数量      ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线包装型号      ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_DOCK     ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单位              ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂零件类        ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'INBOUND_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用量比例          ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'AMOUNTRATIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'带色标零件号      ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'INDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件号   ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_车型     ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_供应商   ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间     ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区     ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线   ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂     ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水号            ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_TWD 消耗计数器', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_CONSUME_COUNTER';

