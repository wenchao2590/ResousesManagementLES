CREATE TABLE [LES].[TT_WMM_REPACKAGE_HEAD] (
    [REPACKAGE_ID]           INT             IDENTITY (1, 1) NOT NULL,
    [REPACKAGE_NO]           NVARCHAR (50)   NULL,
    [PLANT]                  NVARCHAR (5)    NULL,
    [WM_NO]                  NVARCHAR (10)   NULL,
    [ZONE_NO]                NVARCHAR (20)   NULL,
    [REPACKAGE_TIME]         DATETIME        NULL,
    [REPACKAGE_BTIME]        DATETIME        NULL,
    [REPACKAGE_ETIME]        DATETIME        NULL,
    [REPACKAGE_PICKUP_TIME]  DATETIME        NULL,
    [REPACKAGE_ROUTE]        NVARCHAR (30)   NULL,
    [APPLY_KEEPER]           NVARCHAR (50)   NULL,
    [BOOK_KEEPER]            NVARCHAR (50)   NULL,
    [PUBLISH_KEEPER]         NVARCHAR (50)   NULL,
    [PUBLISH_TIME]           DATETIME        NULL,
    [COUNT_STATUS]           INT             NULL,
    [REPACKAGE_COUNT]        INT             NULL,
    [EMERGENCY_TYPE]         INT             NULL,
    [COUNT_REASON]           NVARCHAR (1000) NULL,
    [ASSEMBLY_LINE]          NVARCHAR (10)   NULL,
    [PLANT_ZONE]             NVARCHAR (5)    NULL,
    [WORKSHOP]               NVARCHAR (4)    NULL,
    [COMMENTS]               NVARCHAR (200)  NULL,
    [CREATE_USER]            NVARCHAR (50)   NULL,
    [CREATE_DATE]            DATETIME        NULL,
    [UPDATE_USER]            NVARCHAR (50)   NULL,
    [UPDATE_DATE]            DATETIME        NULL,
    [REPACKAGE_PICKUP_ETIME] DATETIME        NULL,
    [REPACKAGE_TYPE]         NVARCHAR (50)   CONSTRAINT [DF_TT_WMM_REPACKAGE_HEAD_REPACKAGE_TYPE] DEFAULT ('1') NULL,
    CONSTRAINT [PK_TT_WMM_REPACKAGE_HEAD] PRIMARY KEY CLUSTERED ([REPACKAGE_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包结束时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_PICKUP_ETIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包紧急程度', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'EMERGENCY_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'COUNT_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'发布人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'PUBLISH_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'申请人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'APPLY_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计划翻包捡选时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_PICKUP_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计划翻包结束时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ETIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计划翻包起始时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_BTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计划翻包时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'翻包通知单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'通知单ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_仓库翻包单主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REPACKAGE_HEAD';

