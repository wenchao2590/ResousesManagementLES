CREATE TABLE [LES].[TM_ODS_ORDER_PART_RESULTS] (
    [ID]                           INT              IDENTITY (1, 1) NOT NULL,
    [ORDER_NO]                     NVARCHAR (20)    NOT NULL,
    [KNR]                          NVARCHAR (8)     NOT NULL,
    [SIGNATURE]                    NVARCHAR (256)   NULL,
    [VALID_TERMAL_ID]              INT              NULL,
    [VALID_BATCH_NO]               UNIQUEIDENTIFIER NULL,
    [PLANT]                        NVARCHAR (5)     NULL,
    [PRODUCTION_YEAR]              INT              NULL,
    [MODEL]                        NVARCHAR (10)    NULL,
    [PART_NO]                      NVARCHAR (20)    NULL,
    [PART_NAME]                    NVARCHAR (40)    NULL,
    [PART_NAME_CN]                 NVARCHAR (40)    NULL,
    [UNIT_ID]                      NVARCHAR (5)     NULL,
    [QUANTITY]                     NUMERIC (18, 2)  NULL,
    [WORKSHOP_SECTION]             NVARCHAR (20)    NULL,
    [LOCATION]                     NVARCHAR (20)    NULL,
    [IN_PLANT_LOGISTIC_MODE]       NVARCHAR (50)    NULL,
    [IN_PLANT_SYSTEM_MODE]         NVARCHAR (10)    NULL,
    [IN_PLANT_LOGISTIC_PART_CLASS] NVARCHAR (10)    NULL,
    [INHOUSE_MODE]                 NVARCHAR (50)    NULL,
    [INHOUSE_SYSTEM_MODE]          NVARCHAR (10)    NULL,
    [EFFECTIVE_STATUS]             BIT              NULL,
    [START_EFFECTIVE_DATE]         DATETIME         NULL,
    [EXPIRE_DATE]                  DATETIME         NULL,
    [INHOUSE_PART_CLASS]           NVARCHAR (10)    NULL,
    [DOCK]                         NVARCHAR (10)    NULL,
    [INHOUSE_PACKAGE_MODEL]        NVARCHAR (30)    NULL,
    [INHOUSE_PACKAGE]              INT              NULL,
    CONSTRAINT [IDX_PK_ORDER_PART_RESULTS_ID] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'INHOUSE_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)失效时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'EXPIRE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)开始生效日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'START_EFFECTIVE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)生效状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'EFFECTIVE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'INHOUSE_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'INHOUSE_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入厂零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_PART_CLASS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)厂内系统模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'IN_PLANT_SYSTEM_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)厂内物流方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'IN_PLANT_LOGISTIC_MODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'LOCATION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工段', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'WORKSHOP_SECTION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)用量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'QUANTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件单位类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'UNIT_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'PART_NAME_CN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'PART_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_生产年', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'PRODUCTION_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)VALID_BATCH_NO', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'VALID_BATCH_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)VALID_TERMAL_ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'VALID_TERMAL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单签名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'SIGNATURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_车辆唯一标识KNR', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_ODS_ORDER_PART_RESULTS', @level2type = N'COLUMN', @level2name = N'ID';

