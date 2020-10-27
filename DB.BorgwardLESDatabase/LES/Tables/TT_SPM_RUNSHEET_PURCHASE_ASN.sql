CREATE TABLE [LES].[TT_SPM_RUNSHEET_PURCHASE_ASN] (
    [ID]                             BIGINT        IDENTITY (1, 1) NOT NULL,
    [ASN_NO]                         NVARCHAR (32) NULL,
    [PLANT]                          NVARCHAR (8)  NULL,
    [ASSEMBLY_LINE]                  NVARCHAR (16) NULL,
    [WORKSHOP]                       NVARCHAR (4)  NULL,
    [PLANT_ZONE]                     NVARCHAR (16) NULL,
    [RUNSHEET_TYPE]                  INT           NULL,
    [SUPPLIER_NUM]                   NVARCHAR (16) NULL,
    [SUPPLIER_SN]                    INT           NULL,
    [DOCK]                           NVARCHAR (12) NULL,
    [DELIVERY_LOCATION]              NVARCHAR (64) NULL,
    [BOX_PARTS]                      NVARCHAR (16) NULL,
    [PART_TYPE]                      INT           NULL,
    [PROMISE_DELIVERY_TIME]          DATETIME      NULL,
    [STATUS]                         INT           NULL,
    [CREATE_DATE]                    DATETIME      NOT NULL,
    [CREATE_USER]                    NVARCHAR (64) NOT NULL,
    [MODIFY_DATE]                    DATETIME      NULL,
    [MODIFY_USER]                    NVARCHAR (64) NULL,
    [VALID_FLAG]                     BIT           NULL,
    [PRINT_TIMES]                    INT           NULL,
    [LAST_PRINT_TIME]                DATETIME      NULL,
    [CARRIER_DRIVER]                 NVARCHAR (16) NULL,
    [CARRIER_TRUCK_LICENCE_PLATE_NO] NVARCHAR (16) NULL,
    [REAL_ARRIVAL_TIME]              DATETIME      NULL,
    [REAL_INTO_PLANT_TIME]           DATETIME      NULL,
    [REAL_UNLOAD_START_TIME]         DATETIME      NULL,
    [REAL_UNLOAD_END_TIME]           DATETIME      NULL,
    [REAL_LEAVE_TIME]                DATETIME      NULL,
    [IS_URGENT]                      INT           NULL,
    [CARRIER_PHONE]                  NVARCHAR (25) NULL,
    [PRINT_STATE]                    INT           DEFAULT ((0)) NOT NULL,
    [RECEIVE_TYPE]                   INT           DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_TT_SPM_RUNSHEET_PURCHASE_ASN] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'是否紧急', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'IS_URGENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际离厂时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'REAL_LEAVE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际结束卸货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'REAL_UNLOAD_END_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际开始卸货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'REAL_UNLOAD_START_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际入厂时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'REAL_INTO_PLANT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际到厂时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'REAL_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运车辆牌照号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'CARRIER_TRUCK_LICENCE_PLATE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承运司机姓名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'CARRIER_DRIVER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最后打印时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'LAST_PRINT_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单据打印次数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'PRINT_TIMES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'单据状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'承诺送货时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'PROMISE_DELIVERY_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'PART_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'目标仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'道口代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'RUNSHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'区域代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'车间代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'产线代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ASN单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_RUNSHEET_PURCHASE_ASN', @level2type = N'COLUMN', @level2name = N'ASN_NO';

