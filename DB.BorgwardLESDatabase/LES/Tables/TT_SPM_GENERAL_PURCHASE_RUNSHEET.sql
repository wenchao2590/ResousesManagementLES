CREATE TABLE [LES].[TT_SPM_GENERAL_PURCHASE_RUNSHEET] (
    [PLAN_RUNSHEET_SN]       INT            IDENTITY (1, 1) NOT NULL,
    [PLAN_RUNSHEET_NO]       VARCHAR (30)   NOT NULL,
    [PLAN_NO]                NVARCHAR (50)  NULL,
    [ASN_NO]                 NVARCHAR (50)  NULL,
    [PLANT]                  NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]          NVARCHAR (10)  NULL,
    [WORKSHOP]               NVARCHAR (4)   NULL,
    [PLANT_ZONE]             NVARCHAR (5)   NULL,
    [PUBLISH_TIME]           DATETIME       NOT NULL,
    [RUNSHEET_TYPE]          INT            NOT NULL,
    [SUPPLIER_NUM]           NVARCHAR (8)   NOT NULL,
    [DOCK]                   NVARCHAR (10)  NULL,
    [DELIVERY_LOCATION_NO]   NVARCHAR (30)  NULL,
    [DELIVERY_LOCATION_NAME] NVARCHAR (200) NULL,
    [BOX_PARTS]              NVARCHAR (10)  NULL,
    [UNLOADING_TIME]         INT            NULL,
    [EXPECTED_ARRIVAL_TIME]  DATETIME       NULL,
    [DELIVERY_TIME]          DATETIME       NULL,
    [ACTUAL_ARRIVAL_TIME]    DATETIME       NULL,
    [VERIFY_TIME]            DATETIME       NULL,
    [TRANS_SUPPLIER_NUM]     NVARCHAR (8)   NULL,
    [FEEDBACK]               NVARCHAR (100) NULL,
    [SHEET_STATUS]           INT            NULL,
    [SEND_TIME]              DATETIME       NULL,
    [SEND_STATUS]            INT            NULL,
    [IsEmergency]            INT            NULL,
    [SUPPLY_TIME]            DATETIME       NULL,
    [SUPPLY_STATUS]          INT            NULL,
    [SAP_FLAG]               INT            NULL,
    [INVOICENO]              NVARCHAR (30)  NULL,
    [ORDER_NO]               NVARCHAR (30)  NULL,
    [RECKONING_NO]           NVARCHAR (30)  NULL,
    [PACKAGE_TYPE]           NVARCHAR (100) NULL,
    [WHAREHOUSE]             NVARCHAR (100) NULL,
    [LOAD_PLACE]             NVARCHAR (100) NULL,
    [WMS_SEND_TIME]          DATETIME       NULL,
    [WMS_SEND_STATUS]        INT            NULL,
    [CONTRACT_NO]            NVARCHAR (35)  NULL,
    [COMMENTS]               NVARCHAR (200) NULL,
    [LES_TYPE]               NVARCHAR (1)   NULL,
    [ORDER_TYPE]             NVARCHAR (4)   NULL,
    [UPDATE_DATE]            DATETIME       NULL,
    [UPDATE_USER]            NVARCHAR (50)  NULL,
    [CREATE_DATE]            DATETIME       NOT NULL,
    [CREATE_USER]            NVARCHAR (50)  NOT NULL,
    [FORIEN_SUPPLIER_NUM]    NVARCHAR (10)  NULL,
    [SHEET_Done_STATUS]      INT            NULL,
    [DISCONTENT_REASON]      INT            NULL,
    [OTHER_REASON]           NVARCHAR (500) NULL,
    [CONTENT_STATUS]         INT            CONSTRAINT [DF__TT_SPM_GE__CONTE__3FDB6521] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_TT_SPM_GENERAL_PURCHASE_RUNSHEET] PRIMARY KEY CLUSTERED ([PLAN_RUNSHEET_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单据分类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ORDER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单据类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LES_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '合同编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'CONTRACT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WMS发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WMS_SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WMS发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WMS_SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '装货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'LOAD_PLACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '卸货地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WHAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '包装方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PACKAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结算单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RECKONING_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发票号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'INVOICENO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上传SAP标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SAP_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLY_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEND_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SHEET_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商反馈', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'FEEDBACK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流平台_运输供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货验证时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'VERIFY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '实际到送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ACTUAL_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DELIVERY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '期望到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'EXPECTED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '卸货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'UNLOADING_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_发货点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_发货点编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'RUNSHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '组单时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PUBLISH_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ASN编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'ASN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划行号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '一般采购单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLAN_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '一般采购流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET', @level2type = N'COLUMN', @level2name = N'PLAN_RUNSHEET_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_一般采购主表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_GENERAL_PURCHASE_RUNSHEET';

