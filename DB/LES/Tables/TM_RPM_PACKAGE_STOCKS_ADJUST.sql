CREATE TABLE [LES].[TM_RPM_PACKAGE_STOCKS_ADJUST] (
    [ADJUST_ID]     INT             IDENTITY (1, 1) NOT NULL,
    [STOCK_ID]      INT             NULL,
    [PACKAGE_NO]    NVARCHAR (30)   NOT NULL,
    [PLANT]         NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)   NULL,
    [PLANT_ZONE]    NVARCHAR (5)    NULL,
    [WORKSHOP]      NVARCHAR (4)    NULL,
    [WM_NO]         NVARCHAR (10)   NOT NULL,
    [ZONE_NO]       NVARCHAR (20)   NOT NULL,
    [DLOC]          NVARCHAR (30)   NOT NULL,
    [ADJUST_TYPE]   INT             NOT NULL,
    [ADJUST_NUM]    NUMERIC (18, 2) NOT NULL,
    [COMMENTS]      NVARCHAR (200)  NULL,
    [CREATE_USER]   NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]   DATETIME        NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)   NULL,
    [UPDATE_DATE]   DATETIME        NULL,
    CONSTRAINT [IDX_PK_PACKAGE_STOCKS_ADJUST_ID] PRIMARY KEY CLUSTERED ([ADJUST_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'调整库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'ADJUST_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'调整类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'ADJUST_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装_包装器具编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'PACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装库存编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'STOCK_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST', @level2type = N'COLUMN', @level2name = N'ADJUST_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'业务_包装库存调整表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS_ADJUST';

