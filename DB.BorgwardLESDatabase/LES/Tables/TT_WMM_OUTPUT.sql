﻿CREATE TABLE [LES].[TT_WMM_OUTPUT] (
    [OUTPUT_ID]          INT            IDENTITY (1, 1) NOT NULL,
    [OUTPUT_NO]          NVARCHAR (50)  NULL,
    [PLANT]              NVARCHAR (5)   NULL,
    [SUPPLIER_NUM]       NVARCHAR (12)  NULL,
    [WM_NO]              NVARCHAR (10)  NULL,
    [ZONE_NO]            NVARCHAR (20)  NULL,
    [SEND_TIME]          DATETIME       NULL,
    [OUTPUT_TYPE]        INT            NULL,
    [TRAN_TIME]          DATETIME       NULL,
    [OUTPUT_REASON]      NVARCHAR (100) NULL,
    [BOOK_KEEPER]        VARCHAR (100)  NULL,
    [CONFIRM_FLAG]       INT            NULL,
    [PLAN_NO]            NVARCHAR (50)  NULL,
    [ASN_NO]             NVARCHAR (50)  NULL,
    [RUNSHEET_NO]        NVARCHAR (50)  NULL,
    [ASSEMBLY_LINE]      NVARCHAR (10)  NULL,
    [PLANT_ZONE]         NVARCHAR (5)   NULL,
    [WORKSHOP]           NVARCHAR (4)   NULL,
    [TRANS_SUPPLIER_NUM] NVARCHAR (20)  NULL,
    [PART_TYPE]          INT            NULL,
    [SUPPLIER_TYPE]      INT            NULL,
    [RUNSHEET_CODE]      VARCHAR (12)   NULL,
    [ERP_FLAG]           INT            NULL,
    [LOGICAL_PK]         NVARCHAR (50)  NULL,
    [BUSINESS_PK]        NVARCHAR (50)  NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    [CREATE_DATE]        DATETIME       NULL,
    [CREATE_USER]        NVARCHAR (50)  NULL,
    [ROUTE]              NVARCHAR (10)  NULL,
    [REQUEST_TIME]       DATETIME       NULL,
    [PTL_FLAG]           INT            CONSTRAINT [DF_TT_WMM_OUTPUT_PTL_FLAG] DEFAULT ((0)) NULL,
    [PTL_TIME]           DATETIME       NULL,
    CONSTRAINT [PK__TT_DD_Return__345EC57D] PRIMARY KEY CLUSTERED ([OUTPUT_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_OUTPUT_1]
    ON [LES].[TT_WMM_OUTPUT]([ERP_FLAG] ASC, [PTL_FLAG] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TT_WMM_OUTPUT]
    ON [LES].[TT_WMM_OUTPUT]([OUTPUT_NO] ASC);


GO
CREATE NONCLUSTERED INDEX [Uncluster_ZONE_NO_CONFIRM_FLAG_Index, sysname,>]
    ON [LES].[TT_WMM_OUTPUT]([PLANT] ASC, [ZONE_NO] ASC, [RUNSHEET_CODE] ASC, [RUNSHEET_NO] ASC, [SUPPLIER_NUM] ASC)
    INCLUDE([OUTPUT_ID], [OUTPUT_NO], [TRAN_TIME], [ROUTE], [REQUEST_TIME]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PTL处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'PTL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PTL处理标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'PTL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '请求时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'REQUEST_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '送货路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'BUSINESS_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '本地主键', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'ERP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单据号码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'RUNSHEET_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'SUPPLIER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ASN编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'ASN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划行号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'PLAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '确认标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'CONFIRM_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '出库原因', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'OUTPUT_REASON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入库时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入库类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'OUTPUT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '出库单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'OUTPUT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '出库流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT', @level2type = N'COLUMN', @level2name = N'OUTPUT_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_仓库出库主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_WMM_OUTPUT';

