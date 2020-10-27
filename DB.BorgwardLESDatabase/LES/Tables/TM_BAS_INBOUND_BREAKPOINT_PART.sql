CREATE TABLE [LES].[TM_BAS_INBOUND_BREAKPOINT_PART] (
    [INBOUND_BREAKPOINT_NO] NVARCHAR (10)   NOT NULL,
    [PLANT]                 NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)   NOT NULL,
    [PLANT_ZONE]            NVARCHAR (5)    NULL,
    [WORKSHOP]              NVARCHAR (4)    NULL,
    [PART_NO]               NVARCHAR (20)   NOT NULL,
    [PART_CNAME]            NVARCHAR (300)  NULL,
    [KNR]                   NVARCHAR (20)   NULL,
    [RUNNING_NO]            NVARCHAR (8)    NOT NULL,
    [BREAKPOINT_TYPE]       INT             NULL,
    [INBOUND_IDENTITY]      INT             IDENTITY (1, 1) NOT NULL,
    [VIN]                   NVARCHAR (20)   NULL,
    [INBOUND_SYSTEM_MODE]   NVARCHAR (10)   NULL,
    [MODEL]                 NVARCHAR (100)  NULL,
    [MODEL_NO]              NVARCHAR (8)    NULL,
    [STATUS]                INT             NULL,
    [BREAKPOINT_STATUS]     INT             NULL,
    [REMAIN_COUNT]          INT             NULL,
    [ENFORE_SAVE]           INT             NULL,
    [ACTUAL_REMAIN_COUNT]   INT             NULL,
    [DIFFERENT_COUNT]       INT             NULL,
    [MODIFY_REMAIN_COUNT]   INT             NULL,
    [BREAK_TIME]            DATETIME        NULL,
    [INBOUNDID_STRING]      NVARCHAR (1500) NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [CREATE_DATE]           DATETIME        NULL,
    [CREATE_USER]           NVARCHAR (50)   NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    CONSTRAINT [IDX_PK_INBOUND_BREAKPOINT_PART_INBOUND_BREAKPOINT_NO] PRIMARY KEY CLUSTERED ([INBOUND_BREAKPOINT_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'INBOUNDID_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)断点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'BREAK_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)修改差量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'MODIFY_REMAIN_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)差量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'DIFFERENT_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)实际剩余数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'ACTUAL_REMAIN_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)强制保存标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'ENFORE_SAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件剩余数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'REMAIN_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)断点执行状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'BREAKPOINT_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'INBOUND_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'INBOUND_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)断点类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'BREAKPOINT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INBOUND_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'INBOUND_BREAKPOINT_NO';

