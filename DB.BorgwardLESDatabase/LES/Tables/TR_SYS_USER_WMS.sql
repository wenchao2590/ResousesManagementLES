CREATE TABLE [LES].[TR_SYS_USER_WMS] (
    [SEQ_ID]         INT            IDENTITY (1, 1) NOT NULL,
    [USER_ID]        INT            NOT NULL,
    [PLANT]          NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]  NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]     NVARCHAR (5)   NULL,
    [WORKSHOP]       NVARCHAR (4)   NULL,
    [WM_NO]          NVARCHAR (10)  NULL,
    [ZONE_NO]        NVARCHAR (20)  NOT NULL,
    [OPERATION_TYPE] INT            NULL,
    [COMMENTS]       NVARCHAR (200) NULL,
    [CREATE_USER]    NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]    DATETIME       NOT NULL,
    [UPDATE_USER]    NVARCHAR (50)  NULL,
    [UPDATE_DATE]    DATETIME       NULL,
    CONSTRAINT [IDX_PK_USER_WMS_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'update_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_date', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'create_user', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'OPERATION_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统信息_用户ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'USER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系系统_用户与仓库关联表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_WMS';

