CREATE TABLE [LES].[TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT] (
    [INBOUND_IDENTITY]      INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (20)  NULL,
    [MODEL]                 NVARCHAR (10)  NOT NULL,
    [PART_NO]               NVARCHAR (20)  NOT NULL,
    [INDENTIFY_PART_NO]     NVARCHAR (20)  NOT NULL,
    [AMOUNTRATIO]           INT            NULL,
    [EXTERIOR_COLOR]        NVARCHAR (4)   NULL,
    [INTERNAL_COLOR]        NVARCHAR (2)   NULL,
    [HAND_KEPT_RECORD]      NVARCHAR (2)   NULL,
    [COLOR_CONTROL_PATCH]   NVARCHAR (5)   NULL,
    [VWS]                   NVARCHAR (15)  NULL,
    [RAND]                  NVARCHAR (5)   NULL,
    [SECTION]               NVARCHAR (6)   NULL,
    [PART_OPTION]           NVARCHAR (80)  NULL,
    [PRODUCTION_NUMBER]     NVARCHAR (50)  NULL,
    [START_PRODUCTION_DATE] DATETIME       NULL,
    [CANCEL_NUMBER]         NVARCHAR (50)  NULL,
    [END_PRODUCTION_DATE]   DATETIME       NULL,
    [MODEL_YEAR]            NVARCHAR (4)   NULL,
    [DOSAGE]                INT            NULL,
    [MEASURING_UNIT_NO]     VARCHAR (8)    NULL,
    [AMOUNT_FLAG]           NVARCHAR (1)   NULL,
    [DATA_DATE]             DATETIME       NULL,
    [VORSERIE]              BIT            NULL,
    [PART_CNAME]            NVARCHAR (300) NULL,
    [PART_ENAME]            NVARCHAR (300) NULL,
    [LOAD_FLAG]             NVARCHAR (20)  NULL,
    [ZP_FLAG]               NVARCHAR (100) NULL,
    [REQUIREMENT_FLAG]      NVARCHAR (20)  NULL,
    [LOGICAL_PK]            NVARCHAR (50)  NULL,
    [ASSEMBLY_FLAG]         NVARCHAR (200) NULL,
    [ASSEMBLY_FLAG_RECRUIT] NVARCHAR (30)  NULL,
    [MARK]                  NVARCHAR (20)  NULL,
    [PURCHASE_STYLE]        NVARCHAR (30)  NULL,
    [VALID_FLAG]            BIT            NULL,
    [INBOUND_MODE]          NVARCHAR (50)  NULL,
    [INBOUND_LOGISTIC_MODE] NVARCHAR (50)  NULL,
    [INBOUND_SYSTEM_MODE]   NVARCHAR (10)  NULL,
    [EFFECTIVE_STATUS]      BIT            NULL,
    [START_EFFECTIVE_DATE]  DATETIME       NULL,
    [INBOUND_PART_CLASS]    NVARCHAR (10)  NULL,
    [DOCK]                  NVARCHAR (10)  NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)  NULL,
    [INBOUND_PACKAGE]       INT            NULL,
    [IS_SPLIT]              BIT            NULL,
    [TERMAL_PK]             NVARCHAR (200) NULL,
    [DIFF_FLAG]             INT            NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL,
    [PART_NICKNAME]         NVARCHAR (50)  NULL,
    [MIN]                   INT            NULL,
    [MAX]                   INT            NULL,
    [WAREHOUSE]             NVARCHAR (50)  NULL,
    [S_WM_NO]               NVARCHAR (10)  NULL,
    [S_ZONE_NO]             NVARCHAR (20)  NULL,
    [MIN_PULL_BOX]          INT            NULL,
    [BATCH_PULL_BOX]        INT            NULL,
    CONSTRAINT [IDX_PK_INBOUND_PULL_LOGISTIC_SUBMIT_INBOUND_IDENTITY] PRIMARY KEY CLUSTERED ([INBOUND_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'BATCH_PULL_BOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'MIN_PULL_BOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？源存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'S_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？源仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'S_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？库存点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'WAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？MAX', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'MAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？Min', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'MIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？差异标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'DIFF_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？Termal关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'TERMAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'IS_SPLIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INBOUND_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？生效状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'EFFECTIVE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INBOUND_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INBOUND_LOGISTIC_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INBOUND_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？采购方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PURCHASE_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'MARK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？总成标记补充', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG_RECRUIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？总成标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？本地主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求开关', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'REQUIREMENT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？ZP', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'ZP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？装车标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'LOAD_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？VORSERIE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'VORSERIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？数据日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'DATA_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？正负标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'AMOUNT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'DOSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？年型年', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效结束日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'END_PRODUCTION_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？取消号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'CANCEL_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效起始日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'START_PRODUCTION_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？投产号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PRODUCTION_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？汽车选装串', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PART_OPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？区域', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？umfang', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'RAND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？VWS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'VWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？色标', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'COLOR_CONTROL_PATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？手工记录', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'HAND_KEPT_RECORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？内饰', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INTERNAL_COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？外色', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'EXTERIOR_COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？用量比例', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'AMOUNTRATIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？带色标零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INBOUND_IDENTITY';

