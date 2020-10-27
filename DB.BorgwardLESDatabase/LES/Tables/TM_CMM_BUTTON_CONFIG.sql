CREATE TABLE [LES].[TM_CMM_BUTTON_CONFIG] (
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]    NVARCHAR (5)   NULL,
    [WORKSHOP]      NVARCHAR (4)   NULL,
    [BUTTON_ID]     NVARCHAR (20)  NOT NULL,
    [DOCK]          NVARCHAR (10)  NULL,
    [SUPPLIER_NUM]  NVARCHAR (12)  NULL,
    [BUTTON_TYPE]   INT            NULL,
    [PROCESS_FLAG]  INT            NULL,
    [PROCESS_TIME]  DATETIME       NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    CONSTRAINT [PK_TM_CMM_BUTTON_CONFIG] PRIMARY KEY CLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BUTTON_ID] ASC),
    CONSTRAINT [AK_KEY_1_TM_CMM_B] UNIQUE NONCLUSTERED ([PLANT] ASC, [ASSEMBLY_LINE] ASC, [BUTTON_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '按钮类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'BUTTON_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '按钮号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'BUTTON_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'DOCK 按钮与DOCK配置表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_CMM_BUTTON_CONFIG';

