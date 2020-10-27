CREATE TABLE [LES].[TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD] (
    [INBOUND_IDENTITY]             INT            IDENTITY (1, 1) NOT NULL,
    [ERROR_MSG]                    NVARCHAR (MAX) CONSTRAINT [DF__TE_BAS_TM__ERROR__6858FA30] DEFAULT (' ') NULL,
    [BATCH_NO]                     NVARCHAR (20)  NULL,
    [PLANT]                        NVARCHAR (600) NULL,
    [ASSEMBLY_LINE]                NVARCHAR (600) NULL,
    [PLANT_ZONE]                   NVARCHAR (600) NULL,
    [WORKSHOP]                     NVARCHAR (600) NULL,
    [SUPPLIER_NUM]                 NVARCHAR (600) NULL,
    [TRANS_SUPPLIER_NUM]           NVARCHAR (600) NULL,
    [MODEL]                        NVARCHAR (600) NULL,
    [PART_NO]                      NVARCHAR (600) NULL,
    [INDENTIFY_PART_NO]            NVARCHAR (600) NULL,
    [AMOUNTRATIO_STRING]           NVARCHAR (600) NULL,
    [AMOUNTRATIO]                  INT            NULL,
    [EXTERIOR_COLOR]               NVARCHAR (600) NULL,
    [INTERNAL_COLOR]               NVARCHAR (600) NULL,
    [HAND_KEPT_RECORD]             NVARCHAR (600) NULL,
    [COLOR_CONTROL_PATCH]          NVARCHAR (600) NULL,
    [VWS]                          NVARCHAR (600) NULL,
    [RAND]                         NVARCHAR (600) NULL,
    [SECTION]                      NVARCHAR (600) NULL,
    [PART_OPTION]                  NVARCHAR (600) NULL,
    [PRODUCTION_NUMBER]            NVARCHAR (600) NULL,
    [START_PRODUCTION_DATE_STRING] NVARCHAR (600) NULL,
    [START_PRODUCTION_DATE]        DATETIME       NULL,
    [CANCEL_NUMBER]                NVARCHAR (600) NULL,
    [END_PRODUCTION_DATE_STRING]   NVARCHAR (600) NULL,
    [END_PRODUCTION_DATE]          DATETIME       NULL,
    [MODEL_YEAR]                   NVARCHAR (600) NULL,
    [DOSAGE_STRING]                NVARCHAR (600) NULL,
    [DOSAGE]                       INT            NULL,
    [MEASURING_UNIT_NO]            VARCHAR (8)    NULL,
    [DATA_DATE_STRING]             NVARCHAR (600) NULL,
    [DATA_DATE]                    DATETIME       NULL,
    [VORSERIE_STRING]              NVARCHAR (600) NULL,
    [VORSERIE]                     BIT            NULL,
    [PART_CNAME]                   NVARCHAR (600) NULL,
    [PART_ENAME]                   NVARCHAR (600) NULL,
    [LOAD_FLAG]                    NVARCHAR (600) NULL,
    [ZP_FLAG]                      NVARCHAR (600) NULL,
    [REQUIREMENT_FLAG]             NVARCHAR (600) NULL,
    [TERMAL_PK]                    NVARCHAR (200) NULL,
    [ASSEMBLY_FLAG]                NVARCHAR (600) NULL,
    [ASSEMBLY_FLAG_RECRUIT]        NVARCHAR (600) NULL,
    [MARK]                         NVARCHAR (600) NULL,
    [PURCHASE_STYLE]               NVARCHAR (600) NULL,
    [VALID_FLAG]                   BIT            NULL,
    [INBOUND_MODE]                 NVARCHAR (600) NULL,
    [INBOUND_LOGISTIC_MODE]        NVARCHAR (600) NULL,
    [INBOUND_SYSTEM_MODE]          NVARCHAR (600) NULL,
    [EFFECTIVE_STATUS]             NVARCHAR (600) NULL,
    [START_EFFECTIVE_DATE_STRING]  NVARCHAR (600) NULL,
    [START_EFFECTIVE_DATE]         DATETIME       NULL,
    [INBOUND_PART_CLASS]           NVARCHAR (600) NULL,
    [DOCK]                         NVARCHAR (600) NULL,
    [INBOUND_PACKAGE_MODEL]        NVARCHAR (600) NULL,
    [INBOUND_PACKAGE_STRING]       NVARCHAR (600) NULL,
    [INBOUND_PACKAGE]              INT            NULL,
    [IS_SPLIT_STRING]              NVARCHAR (600) NULL,
    [IS_SPLIT]                     BIT            NULL,
    [LOGICAL_PK]                   NVARCHAR (50)  NULL,
    [DIFF_FLAG_STRING]             NVARCHAR (600) NULL,
    [DIFF_FLAG]                    BIT            NULL,
    [AMOUNT_FLAG]                  NVARCHAR (1)   NULL,
    [COMMENTS]                     NVARCHAR (600) NULL,
    [CREATE_DATE]                  DATETIME       NULL,
    [CREATE_USER]                  NVARCHAR (50)  NULL,
    [UPDATE_DATE]                  DATETIME       NULL,
    [UPDATE_USER]                  NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_TMP_INBOUND_PULL_LOGISTIC_STANDARD_INBOUND_IDENTITY] PRIMARY KEY CLUSTERED ([INBOUND_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？正负标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'AMOUNT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？差异标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DIFF_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？开始生效日期STRING', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？生效状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'EFFECTIVE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？采购方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PURCHASE_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？总成标记补充', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG_RECRUIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？总成标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TERMAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求开关', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'REQUIREMENT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？ZP', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ZP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？装车标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'LOAD_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？VORSERIE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'VORSERIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？VORSERIE_string', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'VORSERIE_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？数据日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DATA_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？数据日期STRING', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DATA_DATE_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DOSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_用量STRING', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DOSAGE_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？年型年', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效结束日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'END_PRODUCTION_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效结束日期STRING', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'END_PRODUCTION_DATE_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？取消号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CANCEL_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效起始日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'START_PRODUCTION_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效起始日期STRING', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'START_PRODUCTION_DATE_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？投产号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PRODUCTION_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？汽车选装串', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_OPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？Takt', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？umfang', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'RAND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？VWS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'VWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？色标', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'COLOR_CONTROL_PATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？手工记录', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'HAND_KEPT_RECORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？内饰', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INTERNAL_COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？外色', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'EXTERIOR_COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？用量比例', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'AMOUNTRATIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？带色标零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？批号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';

