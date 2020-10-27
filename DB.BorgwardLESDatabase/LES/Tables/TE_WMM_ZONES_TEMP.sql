CREATE TABLE [LES].[TE_WMM_ZONES_TEMP] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [ZONE_NO]         NVARCHAR (20)  NOT NULL,
    [ZONE_NAME]       NCHAR (100)    NULL,
    [PLANT]           NVARCHAR (5)   NOT NULL,
    [WM_NO]           NVARCHAR (10)  NOT NULL,
    [STOCK_PLACE_NO]  NVARCHAR (20)  NULL,
    [IS_MANAGE]       INT            NULL,
    [IS_IM]           INT            NULL,
    [IS_MIX]          INT            NULL,
    [IM_LOCATION]     NVARCHAR (50)  NULL,
    [IS_STOCK_CHECK]  INT            NULL,
    [STRATEGY]        NVARCHAR (50)  NULL,
    [IS_NEGATIVE]     INT            NULL,
    [COMMENTS]        NVARCHAR (200) NULL,
    [CREATE_USER]     NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]     DATETIME       NOT NULL,
    [UPDATE_USER]     NVARCHAR (50)  NULL,
    [UPDATE_DATE]     DATETIME       NULL,
    [ERROR_MSG]       NVARCHAR (200) NULL,
    [VALID_FLAG]      INT            NULL,
    [REPACKAGE_ZONE]  NVARCHAR (50)  NULL,
    [IS_DYNAMIC_DLOC] INT            NULL,
    [IS_OUTPUT_SOLE]  INT            NULL,
    [OVERFLOW_DLOC]   NVARCHAR (32)  NULL,
    CONSTRAINT [PK_TE_WMM_ZONES_TEMP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)溢库库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'OVERFLOW_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否上下架管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'IS_OUTPUT_SOLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否动态库位管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'IS_DYNAMIC_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)翻包存储区编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否允许负库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'IS_NEGATIVE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)堆放策略', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'STRATEGY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库容检查', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'IS_STOCK_CHECK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'IM_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否混物料', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'IS_MIX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'IS_IM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)存储区类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'IS_MANAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库存地标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'STOCK_PLACE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)厂区名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_ZONES_TEMP', @level2type = N'COLUMN', @level2name = N'ID';

