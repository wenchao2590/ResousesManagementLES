CREATE TABLE [LES].[TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF] (
    [INHOUSE_IDENTITY]             INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                        NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]                   NVARCHAR (5)   NULL,
    [WORKSHOP]                     NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)  NULL,
    [TRANS_SUPPLIER_NUM]           NVARCHAR (8)   NULL,
    [MODEL]                        NVARCHAR (10)  NOT NULL,
    [PART_NO]                      NVARCHAR (20)  NOT NULL,
    [COLOR_DEPARTMENT]             NVARCHAR (500) NULL,
    [HAND_KEPT_RECORD]             NVARCHAR (2)   NULL,
    [COLOR_CONTROL_PATCH]          NVARCHAR (20)  NULL,
    [VWS]                          NVARCHAR (15)  NULL,
    [MODEL_YEAR]                   NVARCHAR (20)  NULL,
    [PART_CNAME]                   NVARCHAR (300) NULL,
    [PART_ENAME]                   NVARCHAR (300) NULL,
    [PART_NICKNAME]                NVARCHAR (50)  NULL,
    [VALID_FLAG]                   BIT            NOT NULL,
    [WORKSHOP_SECTION]             NVARCHAR (20)  NULL,
    [LOCATION]                     NVARCHAR (20)  NULL,
    [IN_PLANT_LOGISTIC_MODE]       NVARCHAR (50)  NULL,
    [IN_PLANT_SYSTEM_MODE]         NVARCHAR (10)  NULL,
    [IN_PLANT_LOGISTIC_PART_CLASS] NVARCHAR (10)  NULL,
    [INHOUSE_MODE]                 NVARCHAR (50)  NULL,
    [INHOUSE_SYSTEM_MODE]          NVARCHAR (10)  NULL,
    [INHOUSE_PART_CLASS]           NVARCHAR (10)  NULL,
    [DOCK]                         NVARCHAR (10)  NULL,
    [INHOUSE_PACKAGE_MODEL]        NVARCHAR (30)  NULL,
    [INHOUSE_PACKAGE]              INT            NULL,
    [START_EFFECTIVE_DATE]         DATETIME       NULL,
    [PLATFORM_CHANGE_DATE]         DATETIME       NULL,
    [LOGICAL_PK]                   NVARCHAR (200) NULL,
    [DIFF_FLAG]                    INT            NULL,
    [CONFIRM_FLAG]                 INT            NULL,
    [EXPIRE_DATE]                  DATETIME       NULL,
    [IS_ACTIVE]                    INT            NULL,
    [REQUIREMENT_FLAG]             NVARCHAR (600) NULL,
    [ASSEMBLY_FLAG]                NVARCHAR (600) NULL,
    [ASSEMBLY_FLAG_RECRUIT]        NVARCHAR (600) NULL,
    [PURCHASE_STYLE]               NVARCHAR (600) NULL,
    [ZP_FLAG]                      NVARCHAR (100) NULL,
    [COMMENTS]                     NVARCHAR (200) NULL,
    [CREATE_USER]                  NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]                  DATETIME       NOT NULL,
    [UPDATE_USER]                  NVARCHAR (50)  NULL,
    [UPDATE_DATE]                  DATETIME       NULL,
    [HAND_KEP_VWS]                 VARCHAR (MAX)  NULL,
    [IS_SPLIT_LOCATION]            INT            NULL,
    [LOAD_FLAG]                    NVARCHAR (20)  NULL,
    CONSTRAINT [IDX_PK_INHOUSE_LOGISTIC_STANDARD_DIFF_INHOUSE_IDENTITY] PRIMARY KEY NONCLUSTERED ([INHOUSE_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？装车标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'LOAD_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？是否拆分工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'IS_SPLIT_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？手工记录-VMW', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'HAND_KEP_VWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？ZP', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'ZP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？采购方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PURCHASE_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？总成标记补充', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG_RECRUIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？总成标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求开关', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'REQUIREMENT_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？是否活动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'IS_ACTIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？失效时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'EXPIRE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？确认标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？差异标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'DIFF_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？变更时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PLATFORM_CHANGE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？上线零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INHOUSE_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？上线系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INHOUSE_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？上线方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INHOUSE_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？厂内零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？厂内系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'IN_PLANT_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？厂内物流方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工段', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？年型年', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？VWS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'VWS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？色标', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'COLOR_CONTROL_PATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？手工记录', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'HAND_KEPT_RECORD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_色系', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'COLOR_DEPARTMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_INHOUSE_LOGISTIC_STANDARD_DIFF', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';

