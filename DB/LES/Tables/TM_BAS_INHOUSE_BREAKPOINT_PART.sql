CREATE TABLE [LES].[TM_BAS_INHOUSE_BREAKPOINT_PART] (
    [INHOUSE_BREAKPOINT_NO] NVARCHAR (10)  NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [PART_NO]               NVARCHAR (20)  NOT NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [KNR]                   NVARCHAR (20)  NULL,
    [RUNNING_NO]            NVARCHAR (8)   NULL,
    [INHOUSE_IDENTITY]      INT            NULL,
    [VIN]                   NVARCHAR (20)  NULL,
    [IN_PLANT_SYSTEM_MODE]  NVARCHAR (10)  NULL,
    [INHOUSE_SYSTEM_MODE]   NVARCHAR (10)  NULL,
    [MODEL]                 NVARCHAR (100) NULL,
    [MODEL_NO]              NVARCHAR (8)   NULL,
    [STATUS]                INT            NULL,
    [BREAKPOINT_STATUS]     INT            NULL,
    [BREAKPOINT_TYPE]       INT            NULL,
    [REMAIN_COUNT]          INT            NULL,
    [ENFORE_SAVE]           INT            NULL,
    [ACTUAL_REMAIN_COUNT]   INT            NULL,
    [DIFFERENT_COUNT]       INT            NULL,
    [MODIFY_REMAIN_COUNT]   INT            NULL,
    [BREAK_TIME]            DATETIME       NULL,
    [INHOUSEID_STRING]      NVARCHAR (500) NULL,
    [EWO]                   NVARCHAR (30)  NULL,
    [NEW_PART_NO]           NVARCHAR (20)  NULL,
    [NEW_PART_CNAME]        NVARCHAR (100) NULL,
    [NEW_PART_NICK_NAME]    NVARCHAR (30)  NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    CONSTRAINT [PK_TM_BAS_INHOUSE_BREAKPOINT_PART] PRIMARY KEY CLUSTERED ([INHOUSE_BREAKPOINT_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件昵称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'NEW_PART_NICK_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'NEW_PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_新零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'NEW_PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'EWO号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'EWO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INHOUSEID集合', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'INHOUSEID_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'断点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'BREAK_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改差量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'MODIFY_REMAIN_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'差量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'DIFFERENT_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际剩余数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'ACTUAL_REMAIN_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'强制保存标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'ENFORE_SAVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件剩余数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'REMAIN_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'断点类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'BREAKPOINT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'断点执行状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'BREAKPOINT_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_6位车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'MODEL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_产品', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'上线系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'INHOUSE_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'厂内系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'IN_PLANT_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INHOUSE标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'INHOUSE_IDENTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'RUNNING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'INHOUSE断点标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART', @level2type = N'COLUMN', @level2name = N'INHOUSE_BREAKPOINT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_零件信息表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INHOUSE_BREAKPOINT_PART';

