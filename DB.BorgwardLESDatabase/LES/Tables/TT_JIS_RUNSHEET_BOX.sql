CREATE TABLE [LES].[TT_JIS_RUNSHEET_BOX] (
    [JIS_RUNSHEET_FLEX_SN] INT           NOT NULL,
    [JIS_RUNSHEET_BOX_SN]  INT           NOT NULL,
    [JIS_BOX_SN]           INT           NULL,
    [PROFILE1]             VARCHAR (8)   NULL,
    [PROFILE2]             VARCHAR (8)   NULL,
    [MODEL]                NVARCHAR (10) NULL,
    [CAR_NO]               NVARCHAR (8)  NOT NULL,
    [VIN]                  NVARCHAR (20) NULL,
    [RUNNING_NUMBER]       NVARCHAR (5)  NOT NULL,
    [MODEL_NO]             NVARCHAR (18) NULL,
    CONSTRAINT [IDX_PK_RUNSHEET_BOX_JIS_RUNSHEET_FLEX_SN_JIS_RUNSHEET_BOX_SN] PRIMARY KEY CLUSTERED ([JIS_RUNSHEET_FLEX_SN] ASC, [JIS_RUNSHEET_BOX_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)过点序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'RUNNING_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车牌号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'CAR_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)特征2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'PROFILE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)特征1', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'PROFILE1';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)盒子流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'JIS_BOX_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)拆分BOX序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_BOX_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)拆分序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_JIS_RUNSHEET_BOX', @level2type = N'COLUMN', @level2name = N'JIS_RUNSHEET_FLEX_SN';

