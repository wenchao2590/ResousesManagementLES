CREATE TABLE [LES].[TT_JIS_RUNSHEET_FLEX] (
    [JIS_RUNSHEET_FLEX_SN]   INT           NOT NULL,
    [JIS_RUNSHEET_FLEX_TIME] DATETIME      NOT NULL,
    [PLANT]                  NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE]          NVARCHAR (10) NOT NULL,
    [SUPPLIER_NUM]           NVARCHAR (8)  NOT NULL,
    [RACK]                   NVARCHAR (20) NOT NULL,
    [BOX_NUMBER]             INT           NOT NULL,
    [FORMAT]                 NVARCHAR (6)  NOT NULL,
    [MODEL]                  NVARCHAR (10) NULL,
    [CAR_NO]                 NVARCHAR (8)  NOT NULL,
    [RUNNING_NUMBER]         NVARCHAR (5)  NOT NULL,
    [JIS_RUNSHEET_SN]        INT           NULL,
    [JIS_RUNSHEET_NO]        NVARCHAR (30) NULL,
    [CREATE_DATE]            DATETIME      NULL,
    [SAP_FLAG]               INT           NULL,
    [VIN]                    NVARCHAR (20) NULL,
    [MODEL_NO]               NVARCHAR (8)  NULL,
    [JIS_BOX_SN]             INT           NULL,
    [PROFILE1]               NVARCHAR (8)  NULL,
    [PROFILE2]               NVARCHAR (8)  NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_FLEX_JIS_RUNSHEET_FLEX_SN] PRIMARY KEY CLUSTERED ([JIS_RUNSHEET_FLEX_SN] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_JIS_RUNSHEET_FLEX_1]
    ON [LES].[TT_JIS_RUNSHEET_FLEX]([CAR_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_JIS_RUNSHEET_FLEX]
    ON [LES].[TT_JIS_RUNSHEET_FLEX]([JIS_RUNSHEET_SN] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '特征2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'PROFILE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '特征1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'PROFILE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '盒子流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'JIS_BOX_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算标记', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'SAP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过点序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'RUNNING_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Knr', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'CAR_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '格式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'FORMAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'BOX_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_FLEX_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拆分序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_FLEX_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_JIS 零件拆分表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_FLEX';

