CREATE TABLE [LES].[TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF] (
    [INBOUND_IDENTITY]      INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (20)  NULL,
    [MODEL]                 NVARCHAR (10)  NOT NULL,
    [PART_NO]               NVARCHAR (20)  NOT NULL,
    [COLOR_DEPARTMENT]      NVARCHAR (500) NULL,
    [HAND_KEPT_RECORD]      NVARCHAR (2)   NULL,
    [COLOR_CONTROL_PATCH]   NVARCHAR (5)   NULL,
    [VWS]                   NVARCHAR (15)  NULL,
    [MODEL_YEAR]            NVARCHAR (30)  NULL,
    [PART_CNAME]            NVARCHAR (300) NULL,
    [PART_ENAME]            NVARCHAR (300) NULL,
    [INBOUND_MODE]          NVARCHAR (50)  NULL,
    [INBOUND_LOGISTIC_MODE] NVARCHAR (50)  NULL,
    [INBOUND_SYSTEM_MODE]   NVARCHAR (10)  NULL,
    [START_EFFECTIVE_DATE]  DATETIME       NULL,
    [INBOUND_PART_CLASS]    NVARCHAR (10)  NULL,
    [DOCK]                  NVARCHAR (10)  NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)  NULL,
    [INBOUND_PACKAGE]       INT            NULL,
    [LOAD_FLAG]             NVARCHAR (20)  NULL,
    [ZP_FLAG]               NVARCHAR (100) NULL,
    [REQUIREMENT_FLAG]      NVARCHAR (20)  NULL,
    [ASSEMBLY_FLAG]         NVARCHAR (200) NULL,
    [ASSEMBLY_FLAG_RECRUIT] NVARCHAR (30)  NULL,
    [MARK]                  NVARCHAR (20)  NULL,
    [PURCHASE_STYLE]        NVARCHAR (30)  NULL,
    [DELETE_FLAG]           BIT            NULL,
    [PLATFORM_CHANGE_DATE]  DATETIME       NULL,
    [IS_SPLIT]              BIT            NULL,
    [LOGICAL_PK]            NVARCHAR (50)  NULL,
    [DIFF_FLAG]             INT            NULL,
    [CONFIRM_FLAG]          INT            NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL,
    [HAND_KEP_VWS]          NVARCHAR (MAX) NULL,
    [VALID_FLAG]            BIT            NULL,
    [SUPPLIER_NAME]         NVARCHAR (300) NULL,
    CONSTRAINT [IDX_PK_INBOUND_LOGISTIC_STANDARD_DIFF_INBOUND_IDENTITY] PRIMARY KEY CLUSTERED ([INBOUND_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_启用标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？手工记录-VMW', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'HAND_KEP_VWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？确认标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？差异标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'DIFF_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'IS_SPLIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？变更时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PLATFORM_CHANGE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？删除标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？采购方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PURCHASE_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'MARK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？总成标记补充', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG_RECRUIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？总成标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求开关', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'REQUIREMENT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？使用车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'ZP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？装车标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'LOAD_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INBOUND_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INBOUND_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INBOUND_LOGISTIC_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INBOUND_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车型颜色代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？VWS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'VWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？色标', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'COLOR_CONTROL_PATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？手工记录', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'HAND_KEPT_RECORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_色系', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'COLOR_DEPARTMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INBOUND_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INBOUND_IDENTITY';

