CREATE TABLE [LES].[TM_EPS_PART_PULL] (
    [RELATION_ID]      INT            IDENTITY (1, 1) NOT NULL,
    [PART_NO]          VARCHAR (20)   NOT NULL,
    [PLANT]            NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]    NVARCHAR (10)  NOT NULL,
    [LOCATION]         NVARCHAR (20)  NOT NULL,
    [D_PLANT]          VARCHAR (5)    NULL,
    [D_ASSEMBLY_LINE]  VARCHAR (10)   NULL,
    [D_LOCATION]       VARCHAR (20)   NULL,
    [E_PLANT]          VARCHAR (5)    NULL,
    [E_ASSEMBLY_LINE]  VARCHAR (10)   NULL,
    [E_LOCATION]       VARCHAR (20)   NULL,
    [USAGE]            INT            NULL,
    [PACKAGE]          INT            NULL,
    [SCREEN_LOCATION]  NVARCHAR (100) NULL,
    [PULL_TYPE]        INT            NOT NULL,
    [COMBINATION_TYPE] INT            NOT NULL,
    [DELIVER_TIME]     INT            NOT NULL,
    [ALARM_TIME]       INT            NOT NULL,
    [TRIGGER_STATUS]   INT            NOT NULL,
    [BUTTON_ID]        NVARCHAR (20)  NULL,
    [BAHNHOF_NO]       NVARCHAR (100) NULL,
    [ROUTE]            NVARCHAR (10)  NULL,
    [DOLLY]            NVARCHAR (100) NULL,
    [UPDATE_DATE]      DATETIME       NULL,
    [UPDATE_USER]      NVARCHAR (50)  NULL,
    [CREATE_DATE]      DATETIME       NOT NULL,
    [CREATE_USER]      NVARCHAR (50)  NOT NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [TARGET_ZONE]      NVARCHAR (10)  NULL,
    [TARGET_WM]        NVARCHAR (16)  NULL,
    CONSTRAINT [IDX_PK_PART_PULL_RELATION_ID] PRIMARY KEY CLUSTERED ([RELATION_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'TARGET_WM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'TARGET_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DOLLY型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'DOLLY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'BAHNHOF_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'按钮编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'BUTTON_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'触发状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'TRIGGER_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'报警时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'ALARM_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'送达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'DELIVER_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'组合类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'COMBINATION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'PULL_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'显示屏位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'SCREEN_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'USAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'空箱返回位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'E_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'空箱产线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'E_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'空箱工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'E_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'源位置', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'D_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'源产线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'D_ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'源工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'D_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工位代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_PART_PULL', @level2type = N'COLUMN', @level2name = N'PART_NO';

