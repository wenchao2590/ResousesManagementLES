CREATE TABLE [LES].[TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS] (
    [REGION_PREDESSOR_IDENTITY] INT          IDENTITY (1, 1) NOT NULL,
    [REGION_IDENTITY]           INT          NULL,
    [REGION_NAME]               VARCHAR (20) NULL,
    [PREDESSOR_REGION_ID]       INT          NULL,
    [PREDESSOR_REGION]          VARCHAR (20) NULL,
    [LAST_REGION_CHECKTIME]     DATETIME     NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最后检查时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS', @level2type = N'COLUMN', @level2name = N'LAST_REGION_CHECKTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '前驱扫描点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS', @level2type = N'COLUMN', @level2name = N'PREDESSOR_REGION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理前驱扫描点标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS', @level2type = N'COLUMN', @level2name = N'PREDESSOR_REGION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '扫描点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS', @level2type = N'COLUMN', @level2name = N'REGION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '当前扫描标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS', @level2type = N'COLUMN', @level2name = N'REGION_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '前驱扫描点标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS', @level2type = N'COLUMN', @level2name = N'REGION_PREDESSOR_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_PCS消耗反推回算表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_PCS_REGION_PREDESSOR_FOR_CONSUME_BACKWARDS';

