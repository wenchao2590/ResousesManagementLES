CREATE TABLE [LES].[TM_RPM_PACKAGE_NOTIFICATION_HEAD] (
    [NOTIFICATION_ID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [NOTIFICATION_NO] NVARCHAR (50)  NOT NULL,
    [PLANT]           NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]   NVARCHAR (10)  NULL,
    [PLANT_ZONE]      NVARCHAR (5)   NULL,
    [WORKSHOP]        NVARCHAR (4)   NULL,
    [WM_NO]           NVARCHAR (10)  NULL,
    [ZONE_NO]         NVARCHAR (20)  NOT NULL,
    [COUNT_TYPE]      INT            NULL,
    [COUNT_TIME]      DATETIME       NULL,
    [APPLY_KEEPER]    NVARCHAR (50)  NULL,
    [BOOK_KEEPER]     NVARCHAR (50)  NULL,
    [PUBLISH_KEEPER]  NVARCHAR (50)  NULL,
    [PUBLISH_TIME]    DATETIME       NULL,
    [COUNT_STATUS]    INT            NULL,
    [COMMENTS]        NVARCHAR (200) NULL,
    [CREATE_USER]     NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]     DATETIME       NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)  NULL,
    [UPDATE_DATE]     DATETIME       NULL,
    CONSTRAINT [IDX_PK_TM_RPM_PACKAGE_NOTIFICATION_ID] PRIMARY KEY CLUSTERED ([NOTIFICATION_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'盘点状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'PUBLISH_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'盘点人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'申请人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'APPLY_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计划盘点时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'盘点类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'NOTIFICATION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD', @level2type = N'COLUMN', @level2name = N'NOTIFICATION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_包装盘点通知单主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_NOTIFICATION_HEAD';

