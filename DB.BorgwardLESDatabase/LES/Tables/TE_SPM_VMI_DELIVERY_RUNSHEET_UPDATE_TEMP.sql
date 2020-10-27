CREATE TABLE [LES].[TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] (
    [STOCK_IDENTITY]     INT             IDENTITY (1, 1) NOT NULL,
    [RUNSHEET_DETAIL_ID] INT             NULL,
    [DEAL_NUM]           NUMERIC (18, 2) NULL,
    [PLANT]              NVARCHAR (100)  NULL,
    [WM_NO]              NVARCHAR (10)   NULL,
    [ZONE_NO]            NVARCHAR (20)   NULL,
    [DLOC]               NVARCHAR (20)   NULL,
    [D_PLANT]            NVARCHAR (100)  NULL,
    [D_WM_NO]            NVARCHAR (10)   NULL,
    [D_ZONE_NO]          NVARCHAR (20)   NULL,
    [D_DLOC]             NVARCHAR (20)   NULL,
    [SUPPLIER_NUM]       NVARCHAR (12)   NULL,
    [PART_NO]            NVARCHAR (20)   NULL,
    [IS_BATCH]           INT             NULL,
    [BARCODE_DATA]       NVARCHAR (50)   NULL,
    [STOCKS]             NUMERIC (18, 2) NULL,
    [AVAILBLE_STOCKS]    NUMERIC (18, 2) NULL,
    [FROZEN_STOCKS]      NUMERIC (18, 2) NULL,
    [FRAGMENT_NUM]       NUMERIC (18, 2) NULL,
    [STOCKS_NUM]         NUMERIC (18, 2) NULL,
    [MODIFICATION_CODE]  NVARCHAR (50)   NULL,
    [UPDATE_DATE]        DATETIME        NULL,
    [UPDATE_USER]        NVARCHAR (50)   NULL,
    [CREATE_DATE]        DATETIME        NULL,
    [CREATE_USER]        NVARCHAR (50)   NULL,
    [ERROR_MSG]          NVARCHAR (200)  NULL,
    [VALID_FLAG]         INT             NULL,
    [UPDATE_FLAG]        INT             NULL,
    CONSTRAINT [PK_TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP] PRIMARY KEY CLUSTERED ([STOCK_IDENTITY] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)更新标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库存数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'MODIFICATION_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'FRAGMENT_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)冻结库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'FROZEN_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)可用库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'AVAILBLE_STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'STOCKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)是否批次管理', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'IS_BATCH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目标库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'D_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目标存储区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'D_ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)目标仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'D_WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)源工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'D_PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'DEAL_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)拉动单明细流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'RUNSHEET_DETAIL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库存编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_DELIVERY_RUNSHEET_UPDATE_TEMP', @level2type = N'COLUMN', @level2name = N'STOCK_IDENTITY';

