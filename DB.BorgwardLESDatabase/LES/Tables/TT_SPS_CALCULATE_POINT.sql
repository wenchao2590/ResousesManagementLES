CREATE TABLE [LES].[TT_SPS_CALCULATE_POINT] (
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
    [RUNNING_NO]     NVARCHAR (12) NULL,
    [TRIGGER_TYPE]   INT           NULL,
    [PROCESS_FLAG]   INT           NULL,
    [UPDATE_DATE]    DATETIME      NULL,
    [CREATE_DATE]    DATETIME      NOT NULL,
    [PTL_SEND_FLAG]  BIT           DEFAULT ((0)) NULL,
    CONSTRAINT [PK_TT_SPS_CALCULATE_POINT] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_SPS_CALCULATE_POINT]
    ON [LES].[TT_SPS_CALCULATE_POINT]([PROCESS_FLAG] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'更新时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'处理标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'触发类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'TRIGGER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'VIN号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DCP点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'DCP_POINT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'VEHICLE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'PASS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'ORDER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID号，自增长', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPS_CALCULATE_POINT', @level2type = N'COLUMN', @level2name = N'ID';

