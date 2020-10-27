CREATE TABLE [LES].[TT_TWD_TRIGGER_ORDER_STATUS] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]           NVARCHAR (5)   NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)  NULL,
    [KNR]             NVARCHAR (16)  NULL,
    [MODEL_NO]        NVARCHAR (8)   NULL,
    [ORDER_ID]        NVARCHAR (36)  NOT NULL,
    [PASS_TIME]       DATETIME       NULL,
    [VEHICLE_STATUS]  NVARCHAR (15)  NOT NULL,
    [DCP_NAME]        NVARCHAR (100) NULL,
    [RUNNING_NO]      NVARCHAR (8)   NOT NULL,
    [TRIGGER_TYPE]    INT            NULL,
    [CONSUME_FLAG]    INT            NULL,
    [PROCESS_FLAG]    INT            NULL,
    [A500_TIME_SN]    INT            NULL,
    [ORDER_CAL_DATE]  DATETIME       NULL,
    [WINDOWS_TIME]    NVARCHAR (50)  NULL,
    [UPDATE_DATE]     DATETIME       NULL,
    [CREATE_DATE]     DATETIME       NOT NULL,
    [WORK_QUANITY_ID] INT            NULL,
    CONSTRAINT [IDX_PK_TRIGGER_ORDER_STATUS_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'WORK_QUANITY_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)窗口时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'WINDOWS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'ORDER_CAL_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'A500_TIME_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)处理标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'CONSUME_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)触发类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'TRIGGER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DCP名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'DCP_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_状态点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'VEHICLE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)过点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'PASS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)顺序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'ORDER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)接口_车辆识别号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_TRIGGER_ORDER_STATUS', @level2type = N'COLUMN', @level2name = N'ID';

