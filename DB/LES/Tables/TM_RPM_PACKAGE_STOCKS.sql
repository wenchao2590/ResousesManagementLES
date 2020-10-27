CREATE TABLE [LES].[TM_RPM_PACKAGE_STOCKS] (
    [STOCK_ID]                INT             IDENTITY (1, 1) NOT NULL,
    [PACKAGE_NO]              NVARCHAR (30)   NOT NULL,
    [PLANT]                   NVARCHAR (5)    NOT NULL,
    [WM_NO]                   NVARCHAR (10)   NOT NULL,
    [ZONE_NO]                 NVARCHAR (20)   NOT NULL,
    [ASSEMBLY_LINE]           NVARCHAR (10)   NULL,
    [PLANT_ZONE]              NVARCHAR (5)    NULL,
    [WORKSHOP]                NVARCHAR (4)    NULL,
    [TRAN_TYPE]               INT             NULL,
    [STOCK_TYPE]              INT             NOT NULL,
    [STOCK_STATE]             INT             NOT NULL,
    [LOGISTICES_LEADTIME]     INT             NULL,
    [PACKAGE_FEE]             NUMERIC (18, 2) NULL,
    [TRANS_FEE]               NUMERIC (18, 2) NULL,
    [WH_FEE]                  NUMERIC (18, 2) NULL,
    [OCCUPY_AREA]             NUMERIC (18, 2) NULL,
    [DLOC]                    NVARCHAR (30)   NOT NULL,
    [DOCK]                    NVARCHAR (10)   NULL,
    [MAX]                     NUMERIC (18, 2) NULL,
    [MIN]                     NUMERIC (18, 2) NULL,
    [HIGH_NUMBER]             INT             NULL,
    [KEEPER]                  NVARCHAR (50)   NULL,
    [TRANSER]                 NVARCHAR (50)   NULL,
    [INFORMATIONER]           NVARCHAR (50)   NULL,
    [ELOC]                    NVARCHAR (30)   NULL,
    [LOGISTIC_LATION]         NVARCHAR (50)   NULL,
    [ROUTE]                   NVARCHAR (50)   NULL,
    [SAGE]                    NUMERIC (18, 2) NULL,
    [STOCK]                   NUMERIC (18, 2) NOT NULL,
    [COUNTER]                 NUMERIC (18, 2) NULL,
    [FREEZE_STOCK]            NUMERIC (18, 2) NULL,
    [AVAILABLE_STOCK]         NUMERIC (18, 2) NULL,
    [COMMENTS]                NVARCHAR (200)  NULL,
    [CREATE_USER]             NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]             DATETIME        NOT NULL,
    [UPDATE_USER]             NVARCHAR (50)   NULL,
    [UPDATE_DATE]             DATETIME        NULL,
    [PACKAGE_STOCK]           NUMERIC (18, 2) NULL,
    [PACKAGE_FREEZE_STOCK]    NUMERIC (18, 2) NULL,
    [PACKAGE_AVAILABLE_STOCK] NUMERIC (18, 2) NULL,
    CONSTRAINT [IDX_PK_TM_RPM_PACKAGE_STOCKS_ID] PRIMARY KEY CLUSTERED ([STOCK_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'可用包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'PACKAGE_AVAILABLE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'冻结包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'PACKAGE_FREEZE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'PACKAGE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'可用库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'AVAILABLE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'冻结库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'FREEZE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计数器', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'COUNTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'安全库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'SAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'线路号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'LOGISTIC_LATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'空器具存放地', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'ELOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'对应信息员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'INFORMATIONER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'对应配送员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'TRANSER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'对应保管员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'堆高', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'HIGH_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Min', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'MIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Max', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'MAX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'所需面积', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'OCCUPY_AREA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓储配送费', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'WH_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'运输费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'TRANS_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'PACKAGE_FEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'物流提前期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'LOGISTICES_LEADTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库存状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'STOCK_STATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'STOCK_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'交易类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'TRAN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装_包装器具编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'PACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'包装库存编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS', @level2type = N'COLUMN', @level2name = N'STOCK_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'基础数据_包装库存表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_RPM_PACKAGE_STOCKS';

