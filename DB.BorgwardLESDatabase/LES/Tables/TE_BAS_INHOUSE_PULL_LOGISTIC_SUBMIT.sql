CREATE TABLE [LES].[TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT] (
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
    CONSTRAINT [IDX_PK_INHOUSE_PULL_LOGISTIC_SUBMIT_INHOUSE_IDENTITY] PRIMARY KEY NONCLUSTERED ([INHOUSE_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INHOUSE_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线系统拉动标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'INHOUSE_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件简码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目标存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件维护-拉动信息  ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_PULL_LOGISTIC_SUBMIT';

