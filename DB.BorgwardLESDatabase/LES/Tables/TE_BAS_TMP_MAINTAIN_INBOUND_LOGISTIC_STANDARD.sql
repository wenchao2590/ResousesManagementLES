CREATE TABLE [LES].[TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD] (
    [INBOUND_IDENTITY]            INT            IDENTITY (1, 1) NOT NULL,
    [ERROR_MSG]                   NVARCHAR (MAX) CONSTRAINT [DF__TE_BAS_TM__ERROR__283E751B] DEFAULT (' ') NULL,
    [BATCH_NO]                    NVARCHAR (20)  NOT NULL,
    [PLANT]                       NVARCHAR (600) NOT NULL,
    [ASSEMBLY_LINE]               NVARCHAR (600) NOT NULL,
    [PLANT_ZONE]                  NVARCHAR (600) NULL,
    [WORKSHOP]                    NVARCHAR (600) NULL,
    [SUPPLIER_NUM]                NVARCHAR (600) NOT NULL,
    [TRANS_SUPPLIER_NUM]          NVARCHAR (600) NULL,
    [MODEL]                       NVARCHAR (600) NOT NULL,
    [PART_NO]                     NVARCHAR (600) NOT NULL,
    [COLOR_DEPARTMENT]            NVARCHAR (600) NULL,
    [HAND_KEPT_RECORD]            NVARCHAR (600) NULL,
    [COLOR_CONTROL_PATCH]         NVARCHAR (600) NULL,
    [VWS]                         NVARCHAR (600) NULL,
    [MODEL_YEAR]                  NVARCHAR (600) NULL,
    [PART_CNAME]                  NVARCHAR (600) NULL,
    [PART_ENAME]                  NVARCHAR (600) NULL,
    [INBOUND_MODE]                NVARCHAR (600) NULL,
    [INBOUND_LOGISTIC_MODE]       NVARCHAR (600) NULL,
    [INBOUND_SYSTEM_MODE]         NVARCHAR (600) NULL,
    [START_EFFECTIVE_DATE_STRING] NVARCHAR (600) NULL,
    [START_EFFECTIVE_DATE]        DATETIME       NULL,
    [INBOUND_PART_CLASS]          NVARCHAR (600) NULL,
    [DOCK]                        NVARCHAR (600) NULL,
    [INBOUND_PACKAGE_MODEL]       NVARCHAR (600) NULL,
    [INBOUND_PACKAGE_STRING]      NVARCHAR (600) NULL,
    [INBOUND_PACKAGE]             INT            NULL,
    [LOAD_FLAG]                   NVARCHAR (600) NULL,
    [ZP_FLAG]                     NVARCHAR (600) NULL,
    [REQUIREMENT_FLAG]            NVARCHAR (600) NULL,
    [ASSEMBLY_FLAG]               NVARCHAR (600) NULL,
    [ASSEMBLY_FLAG_RECRUIT]       NVARCHAR (600) NULL,
    [MARK]                        NVARCHAR (600) NULL,
    [PURCHASE_STYLE]              NVARCHAR (600) NULL,
    [DELETE_FLAG_STRING]          NVARCHAR (600) NULL,
    [DELETE_FLAG]                 BIT            NULL,
    [PLATFORM_CHANGE_DATE_STRING] NVARCHAR (600) NULL,
    [PLATFORM_CHANGE_DATE]        DATETIME       NULL,
    [IS_SPLIT_STRING]             NVARCHAR (600) NULL,
    [IS_SPLIT]                    BIT            NULL,
    [LOGICAL_PK]                  NVARCHAR (50)  NULL,
    [VALID_FLAG]                  BIT            NULL,
    [COMMENTS]                    NVARCHAR (500) NULL,
    [CREATE_DATE]                 DATETIME       NULL,
    [CREATE_USER]                 NVARCHAR (50)  NULL,
    [UPDATE_DATE]                 DATETIME       NULL,
    [UPDATE_USER]                 NVARCHAR (50)  NULL,
    [HAND_KEP_VWS]                NVARCHAR (MAX) NULL,
    [SUPPLIER_NAME]               NVARCHAR (300) NULL,
    CONSTRAINT [IDX_PK_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD_INBOUND_IDENTITY] PRIMARY KEY CLUSTERED ([INBOUND_IDENTITY] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD]
    ON [LES].[TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD]([LOGICAL_PK] ASC, [VALID_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？删除标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_BAS_TMP_MAINTAIN_INBOUND_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT';

