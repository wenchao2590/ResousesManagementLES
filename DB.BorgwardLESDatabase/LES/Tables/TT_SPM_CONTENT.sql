CREATE TABLE [LES].[TT_SPM_CONTENT] (
    [CONTENT_NO]           INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]        NVARCHAR (10)  NULL,
    [WORKSHOP]             NVARCHAR (4)   NULL,
    [PLANT_ZONE]           NVARCHAR (5)   NULL,
    [CONTENT_TYPE]         INT            NOT NULL,
    [CONTENT_STATUS]       INT            CONSTRAINT [DF_TT_OC_Content_ContentStatus] DEFAULT ('草稿') NOT NULL,
    [CLOSE_DATE]           DATETIME       CONSTRAINT [DF_TT_OC_Content_CloseDate] DEFAULT (getdate()+(60)) NULL,
    [START_EFFECTIVE_DATE] DATETIME       NULL,
    [EXPIRE_DATE]          DATETIME       NULL,
    [SUBJECT]              NVARCHAR (200) NOT NULL,
    [BODY]                 NVARCHAR (MAX) NULL,
    [DEPT_CODE]            NVARCHAR (20)  NULL,
    [DEPT_NAME]            NVARCHAR (20)  NULL,
    [SUPPLIER_NUM]         NVARCHAR (MAX) NULL,
    [SUPPLIER_GROUP_ID]    INT            NULL,
    [CUSTOM_NO]            NVARCHAR (20)  NULL,
    [PLANNER_CODE]         NVARCHAR (20)  NULL,
    [PUBLISH_TIME]         DATETIME       NULL,
    [PUBLISHER]            NVARCHAR (50)  NULL,
    [COMMENTS]             NVARCHAR (200) NULL,
    [CREATE_USER]          NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]          DATETIME       NOT NULL,
    [UPDATE_USER]          NVARCHAR (50)  NULL,
    [UPDATE_DATE]          DATETIME       NULL,
    CONSTRAINT [PK_OA_Content] PRIMARY KEY CLUSTERED ([CONTENT_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'PUBLISHER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划员号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'PLANNER_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '定制号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'CUSTOM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '群组编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'SUPPLIER_GROUP_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '部门编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'DEPT_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '正文', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'BODY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '主题', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'SUBJECT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '失效时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'EXPIRE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '关闭日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'CLOSE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'CONTENT_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'CONTENT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '公告号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT', @level2type = N'COLUMN', @level2name = N'CONTENT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_通知公告表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_CONTENT';

