CREATE TABLE [LES].[TI_WMS_TRAN_OUT] (
    [SEQ_ID]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [MATNR]        NVARCHAR (18)   NULL,
    [WERKS]        NVARCHAR (4)    NULL,
    [LGMNG]        NUMERIC (18, 2) NULL,
    [MEINS]        NVARCHAR (8)    NULL,
    [LGORT]        NVARCHAR (18)   NULL,
    [UMLGO]        NVARCHAR (18)   NULL,
    [BWART]        NVARCHAR (3)    NULL,
    [KONTO]        NVARCHAR (10)   NULL,
    [KOSTL]        NVARCHAR (10)   NULL,
    [AUFNR]        NVARCHAR (24)   NULL,
    [WBS]          NVARCHAR (12)   NULL,
    [BUDAT]        DATETIME        NULL,
    [OPTIM]        DATETIME        NULL,
    [OPRTR]        NVARCHAR (50)   NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    [ANLN1]        NVARCHAR (12)   NULL,
    [ZSERIAL]      NVARCHAR (32)   NULL,
    CONSTRAINT [PK_TI_WMS_TRAN_OUT] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '移动操作人员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'OPRTR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际移动时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'OPTIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际移动日期，作为SAP过账日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'BUDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WBS', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'WBS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '内部订单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'AUFNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '成本中心', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'KOSTL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '总帐科目', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'KONTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '移动类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'BWART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收入库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'UMLGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发出库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'LGMNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_移库接口', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_TRAN_OUT';

