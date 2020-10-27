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
    [WAREHOUSE]                    NVARCHAR (50)  NULL,
    [S_WM_NO]                      NVARCHAR (10)  NULL,
    [S_ZONE_NO]                    NVARCHAR (20)  NULL,
    [MIN_PULL_BOX]                 INT            NULL,
    [BATCH_PULL_BOX]               INT            NULL,
    CONSTRAINT [IDX_PK_MAINTAIN_INHOUSE_LOGISTIC_STANDARD_INHOUSE_IDENTITY] PRIMARY KEY NONCLUSTERED ([INHOUSE_IDENTITY] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD]
    ON [LES].[TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [INHOUSE_PART_CLASS] ASC, [SUPPLIER_NUM] ASC, [PART_NO] ASC, [LOCATION] ASC, [ZONE_NO] ASC, [PLANT_ZONE] ASC, [LOGICAL_PK] ASC, [INHOUSE_SYSTEM_MODE] ASC, [IS_TRIGGER_PULL] ASC, [DELETE_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '源存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'S_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '源仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'S_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计数类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'COUNT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '返器具返回位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ELOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '紧急响应时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'EMG_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '看板号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'CARD_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车厢尺寸', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRAN_SIZES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRAN_STYLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '承运类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流路线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '层级拉动仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否触发层级拉动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IS_TRIGGER_PULL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'REPACK_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否翻包', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IS_REPACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否活动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IS_ACTIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'SEQUENCE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线工位(线边地址）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'STORAGE_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '删除标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'DELETE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INHOUSE_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INHOUSE_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INHOUSE_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '厂内零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '厂内系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IN_PLANT_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '厂内物流方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工段', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件信息_拉动表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MAINTAIN_INHOUSE_LOGISTIC_STANDARD';

