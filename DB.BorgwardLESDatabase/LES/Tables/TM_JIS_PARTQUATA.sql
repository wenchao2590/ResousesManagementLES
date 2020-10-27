CREATE TABLE [LES].[TM_JIS_PARTQUATA] (
    [SEQ_ID]               INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]                NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]        NVARCHAR (10)   NOT NULL,
    [PLANT_ZONE]           NVARCHAR (5)    NULL,
    [WORKSHOP]             NVARCHAR (4)    NULL,
    [RACK]                 NVARCHAR (20)   NOT NULL,
    [RACK_CNAME]           NVARCHAR (200)  NULL,
    [PART_NO]              NVARCHAR (20)   NOT NULL,
    [IS_ACTIVE]            BIT             NULL,
    [DEFAULT_QUOTA]        INT             NULL,
    [TERMAL_QNT]           NUMERIC (18, 2) NULL,
    [CURRENT_QNT]          INT             NULL,
    [GROUP_CLS]            NVARCHAR (20)   NOT NULL,
    [CACULATE_CHECK_POINT] NVARCHAR (15)   NULL,
    [COMMENTS]             NVARCHAR (200)  NULL,
    [CREATE_USER]          NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]          DATETIME        NOT NULL,
    [UPDATE_USER]          NVARCHAR (50)   NULL,
    [UPDATE_DATE]          DATETIME        NULL,
    [MODELS]               NVARCHAR (500)  NULL,
    [FACTOR]               NUMERIC (18, 2) NULL,
    CONSTRAINT [IDX_PK_PARTQUATA_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'FACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'MODELS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)结算点校验点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'CACULATE_CHECK_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)组编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'GROUP_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'CURRENT_QNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'TERMAL_QNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'DEFAULT_QUOTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否活动', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'IS_ACTIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'RACK_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_JIS_PARTQUATA', @level2type = N'COLUMN', @level2name = N'SEQ_ID';

