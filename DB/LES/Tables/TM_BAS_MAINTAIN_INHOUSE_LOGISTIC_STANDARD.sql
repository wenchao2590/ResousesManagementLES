CREATE TABLE [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD] (
    [INHOUSE_IDENTITY]             INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                        NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]                   NVARCHAR (5)   NULL,
    [WORKSHOP]                     NVARCHAR (4)   NULL,
    [SUPPLIER_NUM]                 NVARCHAR (12)  NULL,
    [TRANS_SUPPLIER_NUM]           NVARCHAR (20)  NULL,
    [PART_NO]                      NVARCHAR (20)  NOT NULL,
    [PART_CNAME]                   NVARCHAR (100) NULL,
    [PART_ENAME]                   NVARCHAR (100) NULL,
    [PART_NICKNAME]                NVARCHAR (50)  NULL,
    [WORKSHOP_SECTION]             NVARCHAR (20)  NULL,
    [LOCATION]                     NVARCHAR (20)  NOT NULL,
    [IN_PLANT_LOGISTIC_MODE]       NVARCHAR (50)  NULL,
    [IN_PLANT_SYSTEM_MODE]         NVARCHAR (10)  NULL,
    [IN_PLANT_LOGISTIC_PART_CLASS] NVARCHAR (10)  NULL,
    [INHOUSE_MODE]                 NVARCHAR (50)  NULL,
    [INHOUSE_SYSTEM_MODE]          NVARCHAR (10)  NULL,
    [INHOUSE_PART_CLASS]           NVARCHAR (10)  NULL,
    [LOGICAL_PK]                   NVARCHAR (50)  NULL,
    [DELETE_FLAG]                  INT            NULL,
    [STORAGE_LOCATION]             NVARCHAR (30)  NULL,
    [SEQUENCE_NO]                  NVARCHAR (5)   NULL,
    [IS_ACTIVE]                    INT            NULL,
    [IS_REPACK]                    INT            NULL,
    [REPACK_ROUTE]                 NVARCHAR (40)  NULL,
    [IS_TRIGGER_PULL]              INT            NULL,
    [WM_NO]                        NVARCHAR (10)  NULL,
    [ZONE_NO]                      NVARCHAR (30)  NULL,
    [DLOC]                         NVARCHAR (30)  NULL,
    [INBOUND_PACKAGE_MODEL]        NVARCHAR (30)  NULL,
    [INBOUND_PACKAGE]              INT            NULL,
    [ROUTE]                        NVARCHAR (50)  NULL,
    [TRAN_TYPE]                    NVARCHAR (50)  NULL,
    [TRAN_STYLE]                   NVARCHAR (50)  NULL,
    [TRAN_SIZES]                   NVARCHAR (100) NULL,
    [CARD_NO]                      NVARCHAR (20)  NULL,
    [EMG_TIME]                     INT            NULL,
    [ELOC]                         NVARCHAR (50)  NULL,
    [BUSINESS_PK]                  NVARCHAR (50)  NULL,
    [COUNT_TYPE]                   NVARCHAR (50)  NULL,
    [DOCK]                         NVARCHAR (10)  NULL,
    [COMMENTS]                     NVARCHAR (200) NULL,
    [CREATE_USER]                  NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]                  DATETIME       NOT NULL,
    [UPDATE_USER]                  NVARCHAR (50)  NULL,
    [UPDATE_DATE]                  DATETIME       NULL,
    [MIN]                          INT            NULL,
    [MAX]                          INT            NULL,
    [T_WM_NO]                      NVARCHAR (10)  NULL,
    [S_WM_NO]                      NVARCHAR (10)  NULL,
    [T_ZONE_NO]                    NVARCHAR (20)  NULL,
    [S_ZONE_NO]                    NVARCHAR (20)  NULL,
    [MIN_PULL_BOX]                 INT            NULL,
    [BATCH_PULL_BOX]               INT            NULL,
    CONSTRAINT [IDX_PK_MAINTAIN_INHOUSE_LOGISTIC_STANDARD_INHOUSE_IDENTITY] PRIMARY KEY NONCLUSTERED ([INHOUSE_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'批量拉动箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'BATCH_PULL_BOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小拉动箱数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'MIN_PULL_BOX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'S_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'T_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'S_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'T_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最大库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'MAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'MIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计数方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'COUNT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ELOC', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ELOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EMG_TIME', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'EMG_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'看板号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CARD_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'装载量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRAN_SIZES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRAN_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'路径代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'层级拉动存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'层级拉动仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否触发层级拉动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IS_TRIGGER_PULL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'REPACK_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否翻包', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IS_REPACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否启用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IS_ACTIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'线旁地址', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'STORAGE_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'逻辑删除标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'逻辑主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INHOUSE_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线系统拉动标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INHOUSE_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线拉动模式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INHOUSE_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂系统拉动标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IN_PLANT_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'入厂拉动模式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工位代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工段代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件简码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件英文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT';

