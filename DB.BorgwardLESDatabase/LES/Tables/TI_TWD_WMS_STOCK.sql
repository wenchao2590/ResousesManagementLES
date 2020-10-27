CREATE TABLE [LES].[TI_TWD_WMS_STOCK] (
    [INTERFACE_ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]                  NVARCHAR (5)   NOT NULL,
    [COST_CENTER]            NVARCHAR (50)  NULL,
    [REQUEST_TIME]           DATETIME       NULL,
    [INTERFACE_STATUS]       INT            NULL,
    [PROCESS_TIME]           DATETIME       NULL,
    [PART_NO]                NVARCHAR (20)  NOT NULL,
    [PART_CNAME]             NVARCHAR (100) NULL,
    [PART_ENAME]             NVARCHAR (100) NULL,
    [SUPPLIER_NUM]           NVARCHAR (12)  NULL,
    [SUPPLIER_NAME]          NVARCHAR (100) NOT NULL,
    [SUPPLIER_TYPE]          NVARCHAR (10)  NULL,
    [INTERFACE_TYPE]         NVARCHAR (40)  NULL,
    [PACK_COUNT]             INT            NULL,
    [INPLANT_PACKAGE_MODEL2] NVARCHAR (30)  NULL,
    [INPLANT_PACKAGE2]       INT            NULL,
    [INHOUSE_PACKAGE_MODEL]  NVARCHAR (30)  NULL,
    [INHOUSE_PACKAGE]        INT            NULL,
    [AVAILABLE_STOCK]        INT            NULL,
    [UNAVAILABLE_STOCK]      INT            NULL,
    [MEASURING_UNIT_NO]      VARCHAR (8)    NULL,
    [WAREHOUSE]              NVARCHAR (50)  NULL,
    [WAREHOUSE_NAME]         VARCHAR (30)   NULL,
    [SUM_DATE]               DATETIME       NULL,
    [COMMENTS]               NVARCHAR (200) NULL,
    [UPDATE_DATE]            DATETIME       NULL,
    [UPDATE_USER]            NVARCHAR (50)  NULL,
    [CREATE_DATE]            DATETIME       NULL,
    [CREATE_USER]            NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_PK_WMS_STOCK_INTERFACE_ID] PRIMARY KEY CLUSTERED ([INTERFACE_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'SUM_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'WAREHOUSE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)库存点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'WAREHOUSE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'UNAVAILABLE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)可用库存', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'AVAILABLE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'INPLANT_PACKAGE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'INPLANT_PACKAGE_MODEL2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)需求件数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'PACK_COUNT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)接口类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'INTERFACE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'SUPPLIER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商名称2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_ENAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)接口状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'INTERFACE_STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)请求时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'REQUEST_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'COST_CENTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)接口流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_TWD_WMS_STOCK', @level2type = N'COLUMN', @level2name = N'INTERFACE_ID';

