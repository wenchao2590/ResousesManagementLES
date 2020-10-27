CREATE TABLE [LES].[TT_WMM_REFILL_REQUEST] (
    [ID]               BIGINT           IDENTITY (1, 1) NOT NULL,
    [PLANT]            NVARCHAR (5)     NULL,
    [WM_NO]            NVARCHAR (10)    NULL,
    [ZONE_NO]          NVARCHAR (20)    NULL,
    [PART_NO]          NVARCHAR (30)    NULL,
    [PART_CNAME]       NVARCHAR (100)   NULL,
    [PART_NICKNAME]    NVARCHAR (50)    NULL,
    [REPACKAGE_ROUTE]  NVARCHAR (30)    NULL,
    [DLOC]             NVARCHAR (30)    NULL,
    [PACKAGE_MODEL]    NVARCHAR (30)    NULL,
    [PACKAGE]          INT              NULL,
    [REQUIRED_BOX_NUM] NUMERIC (18, 2)  NULL,
    [DEAL_FLAG]        INT              NULL,
    [DEAL_DATE]        DATETIME         NULL,
    [ASSEMBLY_LINE]    NVARCHAR (10)    NULL,
    [COMMENTS]         NVARCHAR (MAX)   NULL,
    [CREATE_USER]      NVARCHAR (50)    NULL,
    [CREATE_DATE]      DATETIME         CONSTRAINT [DF_TT_WMM_REFILL_REQUEST_CREATE_DATE] DEFAULT (getdate()) NULL,
    [UPDATE_USER]      NVARCHAR (50)    NULL,
    [UPDATE_DATE]      DATETIME         NULL,
    [OPERATION_ID]     UNIQUEIDENTIFIER NULL,
    [REFILL_NO]        NVARCHAR (50)    NULL,
    CONSTRAINT [PK_TT_WMM_REFILL_REQUEST] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'REFILL_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)操作ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'OPERATION_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)处理日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'DEAL_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'REQUIRED_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)翻包路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件呢称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_NICKNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_REFILL_REQUEST', @level2type = N'COLUMN', @level2name = N'ID';

