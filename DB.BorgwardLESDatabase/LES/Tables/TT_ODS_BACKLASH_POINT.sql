CREATE TABLE [LES].[TT_ODS_BACKLASH_POINT] (
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [PLANT]          NVARCHAR (5)  NULL,
    [ASSEMBLY_LINE]  NVARCHAR (10) NULL,
    [KNR]            NVARCHAR (16) NULL,
    [MODEL_NO]       NVARCHAR (18) NULL,
    [ORDER_ID]       NVARCHAR (36) NOT NULL,
    [PASS_TIME]      DATETIME      NULL,
    [VEHICLE_STATUS] NVARCHAR (15) NOT NULL,
    [DCP_POINT]      NVARCHAR (15) NULL,
    [VIN]            NVARCHAR (20) NULL,
    [RUNNING_NO]     NVARCHAR (18) NOT NULL,
    [TRIGGER_TYPE]   INT           NULL,
    [PROCESS_FLAG]   INT           NULL,
    [UPDATE_DATE]    DATETIME      NULL,
    [CREATE_DATE]    DATETIME      NOT NULL,
    CONSTRAINT [IDX_PK_BACKLASH_POINT_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_ODS_BACKLASH_POINT]
    ON [LES].[TT_ODS_BACKLASH_POINT]([PLANT] ASC, [ASSEMBLY_LINE] ASC, [MODEL_NO] ASC, [ORDER_ID] ASC, [DCP_POINT] ASC, [VIN] ASC, [PROCESS_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)处理标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)触发类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'TRIGGER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DCP点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_状态点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'VEHICLE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'PASS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'ORDER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_ODS_BACKLASH_POINT', @level2type = N'COLUMN', @level2name = N'ID';

