CREATE TABLE [LES].[TM_BAS_MULTI_LOCATION] (
    [MULTI_IDENTITY]               INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]                        NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]                NVARCHAR (10)   NOT NULL,
    [PLANT_ZONE]                   NVARCHAR (5)    NULL,
    [WORKSHOP]                     NVARCHAR (4)    NULL,
    [PART_NO]                      NVARCHAR (200)  NOT NULL,
    [PART_CNAME]                   NVARCHAR (100)  NULL,
    [WORKSHOP_SECTION]             NVARCHAR (20)   NULL,
    [LOCATION]                     NVARCHAR (20)   NOT NULL,
    [IN_PLANT_LOGISTIC_PART_CLASS] NVARCHAR (10)   NULL,
    [INHOUSE_PART_CLASS]           NVARCHAR (10)   NULL,
    [START_EFFECTIVE_DATE]         DATETIME        NULL,
    [SPLIT_RATIO]                  NUMERIC (18, 1) NULL,
    [DELAY_TIME]                   INT             NULL,
    [CURRENT_PART_COUNT]           NUMERIC (18, 2) NULL,
    [COMMENTS]                     NVARCHAR (200)  NULL,
    [CREATE_USER]                  NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]                  DATETIME        NOT NULL,
    [UPDATE_USER]                  NVARCHAR (50)   NULL,
    [UPDATE_DATE]                  DATETIME        NULL,
    [CAL_TYPE]                     INT             NULL,
    [LOGICAL_PK]                   NVARCHAR (50)   NULL,
    [BUSINESS_PK]                  NVARCHAR (50)   NULL,
    [INHOUSE_IDENTITY]             INT             NULL,
    CONSTRAINT [IDX_PK_MULTI_LOCATION_MULTI_IDENTITY] PRIMARY KEY NONCLUSTERED ([MULTI_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'CAL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车当前计数器数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'CURRENT_PART_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)延迟时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'DELAY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'SPLIT_RATIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'INHOUSE_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工段', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MULTI_LOCATION', @level2type = N'COLUMN', @level2name = N'MULTI_IDENTITY';

