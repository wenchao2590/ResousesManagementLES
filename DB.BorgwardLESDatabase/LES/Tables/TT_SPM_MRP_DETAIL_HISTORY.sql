CREATE TABLE [LES].[TT_SPM_MRP_DETAIL_HISTORY] (
    [HSEQ_ID]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [MRP_SEQ_ID]            BIGINT         NOT NULL,
    [PLAN_NO]               VARCHAR (20)   NULL,
    [PLAN_ID]               BIGINT         NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [SUPPLIER_NUM]          NVARCHAR (8)   NOT NULL,
    [PART_NO]               NVARCHAR (20)  NOT NULL,
    [PART_CNAME]            NVARCHAR (100) NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)  NULL,
    [INBOUND_PACKAGE]       INT            NULL,
    [REQUIRE_DATE]          DATETIME       NULL,
    [REQUIRE_WEEK]          NVARCHAR (20)  NULL,
    [WEEK_ORDER]            NVARCHAR (50)  NULL,
    [MQ_STATUS]             INT            DEFAULT ((1)) NULL,
    [READ_USER]             VARCHAR (200)  NULL,
    [READ_DATE]             DATETIME       NULL,
    [CHECK_RESULT]          VARCHAR (20)   NULL,
    [CHECK_USER]            VARCHAR (200)  NULL,
    [CHECK_DATE]            DATETIME       NULL,
    [IS_URGENT]             BIT            DEFAULT ((0)) NULL,
    [IS_MEET]               INT            NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [CREATE_USER]           NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]           DATETIME       NOT NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    [DISCONTENT_REASON]     INT            NULL,
    CONSTRAINT [IDX_PK_MRP_DETAIL_HSEQ_ID_HISTORY] PRIMARY KEY CLUSTERED ([HSEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'DISCONTENT_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否满足', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'IS_MEET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IsUrgent', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'IS_URGENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)确认日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'CHECK_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)确认人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'CHECK_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)满足结果', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'CHECK_RESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)阅读日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'READ_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)阅读用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'READ_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'MQ_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)周序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'WEEK_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)周', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'REQUIRE_WEEK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'REQUIRE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)PLAN_ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'PLAN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)计划行号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'PLAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'MRP_SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_MRP_DETAIL_HISTORY', @level2type = N'COLUMN', @level2name = N'HSEQ_ID';

