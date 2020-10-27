CREATE TABLE [LES].[TI_ODS_ORDERS_IN] (
    [SEQ_ID]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [ZORDNO]       NVARCHAR (12)   NULL,
    [ZKWERK]       NVARCHAR (4)    NULL,
    [ZMATNR]       NVARCHAR (18)   NULL,
    [ZIDNKD]       NVARCHAR (18)   NULL,
    [ZMODEL]       NVARCHAR (30)   NULL,
    [ZMODEL_D]     NVARCHAR (30)   NULL,
    [ZPACK]        NVARCHAR (100)  NULL,
    [ZPACK_D]      NVARCHAR (100)  NULL,
    [ZVIN]         NVARCHAR (17)   NULL,
    [ZQTY]         NUMERIC (18, 2) NULL,
    [ZDATE]        NVARCHAR (8)    NULL,
    [ZDISPO]       NVARCHAR (3)    NULL,
    [ZST]          NVARCHAR (1)    NULL,
    [ZMEMO]        NVARCHAR (80)   NULL,
    [ZMEINS]       NVARCHAR (8)    NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    [ZCOLORE]      NVARCHAR (30)   NULL,
    [ZCOLORE_D]    NVARCHAR (30)   NULL,
    [ZCOLORI]      NVARCHAR (30)   NULL,
    [ZCOLORI_D]    NVARCHAR (30)   NULL,
    [BATCHNO]      NVARCHAR (50)   NULL,
    CONSTRAINT [PK_TI_ODS_ORDERS_IN] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZMEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZMEMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZDISPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划下线日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZQTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VIN号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '选项包代码描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZPACK_D';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '选项包代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZPACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '特征包代码描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZMODEL_D';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '特征包代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZMODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZIDNKD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物料号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZMATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZKWERK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'ZORDNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_APO_整车订单表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDERS_IN';

