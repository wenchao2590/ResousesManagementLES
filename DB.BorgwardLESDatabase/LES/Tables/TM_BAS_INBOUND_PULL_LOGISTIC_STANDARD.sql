CREATE TABLE [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD] (
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
    [MARK]                  NVARCHAR (20)  NULL,
    [PART_NICKNAME]         NVARCHAR (50)  NULL,
    [MIN]                   INT            NULL,
    [MAX]                   INT            NULL,
    [WAREHOUSE]             NVARCHAR (50)  NULL,
    [S_WM_NO]               NVARCHAR (10)  NULL,
    [S_ZONE_NO]             NVARCHAR (20)  NULL,
    [MIN_PULL_BOX]          INT            NULL,
    [BATCH_PULL_BOX]        INT            NULL,
    CONSTRAINT [IDX_PK_INBOUND_PULL_LOGISTIC_STANDARD_INBOUND_IDENTITY] PRIMARY KEY CLUSTERED ([INBOUND_IDENTITY] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]
    ON [LES].[TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [SUPPLIER_NUM] ASC, [INBOUND_PART_CLASS] ASC, [PART_NO] ASC, [INBOUND_SYSTEM_MODE] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)生效状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'EFFECTIVE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)总成标记补充', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG_RECRUIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_PULL_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INBOUND_IDENTITY';

