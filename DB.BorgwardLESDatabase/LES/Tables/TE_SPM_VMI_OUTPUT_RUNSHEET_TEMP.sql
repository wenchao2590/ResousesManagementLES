CREATE TABLE [LES].[TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP] (
    [ID]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [PART_NO]     NVARCHAR (20)   NULL,
    [ITEM_NO]     NVARCHAR (50)   NULL,
    [RUNSHEET_NO] NVARCHAR (30)   NULL,
    [RECEIVE_ID]  INT             NULL,
    [STOCK_NUM]   NUMERIC (18, 2) NULL,
    [CREATE_DATE] DATETIME        NULL,
    [CREATE_USER] NVARCHAR (50)   NULL,
    [UPDATE_DATE] DATETIME        NULL,
    [UPDATE_USER] NVARCHAR (50)   NULL,
    [LFART]       NVARCHAR (4)    NULL,
    CONSTRAINT [PK_TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)交货单类型/采购订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'LFART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'STOCK_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入库流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'RECEIVE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ITEM号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'ITEM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_SPM_VMI_OUTPUT_RUNSHEET_TEMP', @level2type = N'COLUMN', @level2name = N'ID';

