CREATE TABLE [LES].[TM_EPS_BUTTON] (
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]    NVARCHAR (5)   NULL,
    [WORKSHOP]      NVARCHAR (4)   NULL,
    [BUTTON_ID]     NVARCHAR (20)  NOT NULL,
    [BUTTON_TYPE]   INT            NULL,
    [BUTTON_STATE]  INT            NULL,
    [UNLOCK_TIME]   INT            NULL,
    [LOCK_TIME]     DATETIME       NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    CONSTRAINT [IDX_PK_BUTTON_PLANT_ASSEMBLY_LINE_BUTTON_ID] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BUTTON_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'锁定时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'LOCK_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'解锁时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'UNLOCK_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'BUTTON_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'按钮类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'BUTTON_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'按钮编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'BUTTON_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_EPS_BUTTON', @level2type = N'COLUMN', @level2name = N'PLANT';

