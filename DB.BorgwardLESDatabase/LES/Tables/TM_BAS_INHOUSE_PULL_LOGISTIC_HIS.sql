CREATE TABLE [LES].[TM_BAS_INHOUSE_PULL_LOGISTIC_HIS] (
    [INHOUSE_IDENTITY]             INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                        NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]                   NVARCHAR (5)   NULL,
    [WORKSHOP]                     NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)  NULL,
    [TRANS_SUPPLIER_NUM]           NVARCHAR (20)  NULL,
    [MODEL]                        NVARCHAR (10)  NOT NULL,
    [PART_NO]                      NVARCHAR (20)  NOT NULL,
    [INDENTIFY_PART_NO]            NVARCHAR (20)  NOT NULL,
    [AMOUNTRATIO]                  INT            NOT NULL,
    [EXTERIOR_COLOR]               NVARCHAR (4)   NULL,
    [INTERNAL_COLOR]               NVARCHAR (2)   NULL,
    [HAND_KEPT_RECORD]             NVARCHAR (2)   NULL,
    [COLOR_CONTROL_PATCH]          NVARCHAR (5)   NULL,
    [VWS]                          NVARCHAR (15)  NULL,
    [RAND]                         NVARCHAR (5)   NULL,
    [SECTION]                      NVARCHAR (6)   NULL,
    [PART_OPTION]                  NVARCHAR (80)  NULL,
    [PRODUCTION_NUMBER]            NVARCHAR (50)  NULL,
    [START_PRODUCTION_DATE]        DATETIME       NULL,
    [CANCEL_NUMBER]                NVARCHAR (50)  NULL,
    [END_PRODUCTION_DATE]          DATETIME       NULL,
    [MODEL_YEAR]                   NVARCHAR (4)   NULL,
    [DOSAGE]                       INT            NULL,
    [MEASURING_UNIT_NO]            VARCHAR (8)    NULL,
    [AMOUNT_FLAG]                  NVARCHAR (1)   NULL,
    [DATA_DATE]                    DATETIME       NULL,
    [VORSERIE]                     BIT            NULL,
    [PART_CNAME]                   NVARCHAR (300) NULL,
    [PART_ENAME]                   NVARCHAR (300) NULL,
    [PART_NICKNAME]                NVARCHAR (50)  NULL,
    [LOAD_FLAG]                    NVARCHAR (20)  NULL,
    [ZP_FLAG]                      NVARCHAR (100) NULL,
    [REQUIREMENT_FLAG]             NVARCHAR (20)  NULL,
    [ASSEMBLY_FLAG]                NVARCHAR (30)  NULL,
    [ASSEMBLY_FLAG_RECRUIT]        NVARCHAR (30)  NULL,
    [PURCHASE_STYLE]               NVARCHAR (30)  NULL,
    [LOGICAL_PK]                   NVARCHAR (50)  NOT NULL,
    [VALID_FLAG]                   BIT            NULL,
    [WORKSHOP_SECTION]             NVARCHAR (20)  NULL,
    [LOCATION]                     NVARCHAR (20)  NULL,
    [IN_PLANT_LOGISTIC_MODE]       NVARCHAR (50)  NULL,
    [IN_PLANT_SYSTEM_MODE]         NVARCHAR (10)  NULL,
    [IN_PLANT_LOGISTIC_PART_CLASS] NVARCHAR (10)  NULL,
    [INHOUSE_MODE]                 NVARCHAR (50)  NULL,
    [INHOUSE_SYSTEM_MODE]          NVARCHAR (10)  NULL,
    [EFFECTIVE_STATUS]             BIT            NULL,
    [START_EFFECTIVE_DATE]         DATETIME       NULL,
    [EXPIRE_DATE]                  DATETIME       NULL,
    [INHOUSE_PART_CLASS]           NVARCHAR (10)  NULL,
    [DOCK]                         NVARCHAR (10)  NULL,
    [INHOUSE_PACKAGE_MODEL]        NVARCHAR (30)  NULL,
    [INHOUSE_PACKAGE]              INT            NULL,
    [DIFF_FLAG]                    INT            NULL,
    [TERMAL_PK]                    NVARCHAR (50)  NULL,
    [STORAGE_LOCATION]             NVARCHAR (30)  NULL,
    [SEQUENCE_NO]                  NVARCHAR (5)   NULL,
    [IS_SPLIT_LOCATION]            INT            NULL,
    [COMMENTS]                     NVARCHAR (200) NULL,
    [CREATE_USER]                  NVARCHAR (50)  NULL,
    [CREATE_DATE]                  DATETIME       NULL,
    [UPDATE_USER]                  NVARCHAR (50)  NULL,
    [UPDATE_DATE]                  DATETIME       NULL,
    [BUSINESS_PK]                  NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_INHOUSE_PULL_LOGISTIC_HIS_INHOUSE_IDENTITY] PRIMARY KEY NONCLUSTERED ([INHOUSE_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)业务主键2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否拆分工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'IS_SPLIT_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'STORAGE_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)Termal关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'TERMAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)差异标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'DIFF_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'INHOUSE_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)失效时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'EXPIRE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)生效状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'EFFECTIVE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'INHOUSE_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'INHOUSE_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)厂内系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'IN_PLANT_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)厂内物流方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工段', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)本地主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)采购方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PURCHASE_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)总成标记补充', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG_RECRUIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)总成标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求开关', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'REQUIREMENT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ZP', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'ZP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)装车标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'LOAD_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)VORSERIE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'VORSERIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)数据日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'DATA_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)正负标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'AMOUNT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'DOSAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)年型年', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效结束日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'END_PRODUCTION_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)取消号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'CANCEL_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效起始日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'START_PRODUCTION_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)投产号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PRODUCTION_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)汽车选装串', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PART_OPTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)SCHEMA', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)umfang', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'RAND';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)VWS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'VWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)色标', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'COLOR_CONTROL_PATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)手工记录', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'HAND_KEPT_RECORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)内饰', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'INTERNAL_COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)外色', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'EXTERIOR_COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)用量比例', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'AMOUNTRATIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)带色标零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'INDENTIFY_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_PULL_LOGISTIC_HIS', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';

