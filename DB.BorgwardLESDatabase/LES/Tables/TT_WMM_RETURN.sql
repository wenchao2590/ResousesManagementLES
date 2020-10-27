CREATE TABLE [LES].[TT_WMM_RETURN] (
    [RETURN_ID]          INT            IDENTITY (1, 1) NOT NULL,
    [RETURN_NO]          NVARCHAR (50)  NULL,
    [PLANT]              NVARCHAR (100) NULL,
    [SUPPLIER_NUM]       NVARCHAR (12)  NULL,
    [WM_NO]              NVARCHAR (10)  NULL,
    [ZONE_NO]            NVARCHAR (20)  NULL,
    [RETURN_TYPE]        INT            NULL,
    [SEND_TIME]          DATETIME       NULL,
    [TRAN_TIME]          DATETIME       NULL,
    [BOOK_KEEPER]        VARCHAR (100)  NULL,
    [CONFIRM_FLAG]       INT            NULL,
    [ASSEMBLY_LINE]      NVARCHAR (10)  NULL,
    [PLANT_ZONE]         NVARCHAR (5)   NULL,
    [WORKSHOP]           NVARCHAR (4)   NULL,
    [TRANS_SUPPLIER_NUM] NVARCHAR (20)  NULL,
    [RUNSHEET_CODE]      VARCHAR (12)   NULL,
    [ERP_FLAG]           INT            NULL,
    [LOGICAL_PK]         NVARCHAR (50)  NULL,
    [BUSINESS_PK]        NVARCHAR (50)  NULL,
    [PART_TYPE]          INT            NULL,
    [SUPPLIER_TYPE]      INT            NULL,
    [RETURN_REASON]      NVARCHAR (200) NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [CREATE_DATE]        DATETIME       NULL,
    [CREATE_USER]        NVARCHAR (50)  NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    CONSTRAINT [PK_TT_WMM_RETURN] PRIMARY KEY CLUSTERED ([RETURN_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_RETURN_2]
    ON [LES].[TT_WMM_RETURN]([PLANT] ASC, [WM_NO] ASC, [ZONE_NO] ASC, [ASSEMBLY_LINE] ASC, [SUPPLIER_NUM] ASC, [TRAN_TIME] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_RETURN_1]
    ON [LES].[TT_WMM_RETURN]([CONFIRM_FLAG] ASC, [ERP_FLAG] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_RETURN]
    ON [LES].[TT_WMM_RETURN]([RETURN_NO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '退货原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'RETURN_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '本地主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ERP标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'RUNSHEET_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '确认标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '负责人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '交易时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '退货类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'RETURN_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '退货单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'RETURN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '退货流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN', @level2type = N'COLUMN', @level2name = N'RETURN_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_退货主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RETURN';

