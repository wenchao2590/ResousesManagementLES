﻿CREATE TABLE [LES].[TT_JIS_UNFIT_ASSEMBLY_RULE] (
    [VALIDATE_SN]        INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]              NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]      NVARCHAR (10)  NOT NULL,
    [RULE_TYPE]          INT            NULL,
    [ASSEMBLY_RULE]      NVARCHAR (15)  NULL,
    [ASSEMBLY_RULE_NAME] NVARCHAR (100) NULL,
    [PLANT_ZONE]         NVARCHAR (5)   NULL,
    [WORKSHOP]           NVARCHAR (4)   NULL,
    [MODEL]              NVARCHAR (10)  NULL,
    [RACK]               NVARCHAR (20)  NOT NULL,
    [RACK_CNAME]         NVARCHAR (200) NULL,
    [UN_FIT_COUNT]       INT            NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [CREATE_USER]        NVARCHAR (50)  NULL,
    [CREATE_DATE]        DATETIME       NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    CONSTRAINT [IDX_PK_UNFIT_ASSEMBLY_RULE_VALIDATE_SN] PRIMARY KEY CLUSTERED ([VALIDATE_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'UN_FIT_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)中文名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'RACK_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_料架', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'RACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_RULE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_RULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'RULE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_UNFIT_ASSEMBLY_RULE', @level2type = N'COLUMN', @level2name = N'VALIDATE_SN';

