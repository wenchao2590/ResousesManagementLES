CREATE TABLE [LES].[TT_WMM_RECEIVE] (
    [RECEIVE_ID]             INT            IDENTITY (1, 1) NOT NULL,
    [RECEIVE_NO]             NVARCHAR (50)  NULL,
    [PLANT]                  NVARCHAR (5)   NULL,
    [SUPPLIER_NUM]           NVARCHAR (12)  NULL,
    [WM_NO]                  NVARCHAR (10)  NULL,
    [ZONE_NO]                NVARCHAR (20)  NULL,
    [DELIVERY_LOCATION_NO]   NVARCHAR (30)  NULL,
    [DOCK]                   NVARCHAR (10)  NULL,
    [SEND_TIME]              DATETIME       NULL,
    [RECEIVE_TYPE]           INT            NULL,
    [TRAN_TIME]              DATETIME       NULL,
    [RECEIVE_REASON]         NVARCHAR (100) NULL,
    [BOOK_KEEPER]            NVARCHAR (100) NULL,
    [CONFIRM_FLAG]           INT            NULL,
    [PART_TYPE]              INT            NULL,
    [TRANS_SUPPLIER_NUM]     NVARCHAR (20)  NULL,
    [PLAN_NO]                NVARCHAR (50)  NULL,
    [ASN_NO]                 NVARCHAR (50)  NULL,
    [LOGICAL_PK]             NVARCHAR (50)  NULL,
    [BUSINESS_PK]            NVARCHAR (50)  NULL,
    [RUNSHEET_NO]            NVARCHAR (50)  NULL,
    [ASSEMBLY_LINE]          NVARCHAR (10)  NULL,
    [PLANT_ZONE]             NVARCHAR (5)   NULL,
    [WORKSHOP]               NVARCHAR (4)   NULL,
    [SUPPLIER_TYPE]          INT            NULL,
    [ERP_FLAG]               INT            NULL,
    [RUNSHEET_CODE]          NVARCHAR (12)  NULL,
    [DELIVERY_LOCATION_NAME] NVARCHAR (200) NULL,
    [COMMENTS]               NVARCHAR (200) NULL,
    [UPDATE_DATE]            DATETIME       NULL,
    [UPDATE_USER]            NVARCHAR (50)  NULL,
    [CREATE_DATE]            DATETIME       NULL,
    [CREATE_USER]            NVARCHAR (50)  NULL,
    [PTL_FLAG]               INT            CONSTRAINT [DF_TT_WMM_RECEIVE_PTL_FLAG] DEFAULT ((0)) NULL,
    [PTL_TIME]               DATETIME       NULL,
    CONSTRAINT [PK_TT_WMM_RECEIVE] PRIMARY KEY CLUSTERED ([RECEIVE_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_RECEIVE_2]
    ON [LES].[TT_WMM_RECEIVE]([CREATE_DATE] ASC, [CONFIRM_FLAG] ASC, [ERP_FLAG] ASC, [PTL_FLAG] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_RECEIVE_1]
    ON [LES].[TT_WMM_RECEIVE]([ASSEMBLY_LINE] ASC, [PLANT] ASC, [SUPPLIER_NUM] ASC, [SUPPLIER_TYPE] ASC, [RUNSHEET_CODE] ASC, [WM_NO] ASC, [ZONE_NO] ASC, [TRAN_TIME] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_RECEIVE]
    ON [LES].[TT_WMM_RECEIVE]([RECEIVE_NO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PTL处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'PTL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PTL处理标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'PTL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_发货点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单据号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'RUNSHEET_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'SUPPLIER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '本地主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ASN编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'ASN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划行号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'PLAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '确认标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入库原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'RECEIVE_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入库时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入库类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'RECEIVE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_发货点编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入库单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'RECEIVE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入库流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE', @level2type = N'COLUMN', @level2name = N'RECEIVE_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库入库主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_RECEIVE';

