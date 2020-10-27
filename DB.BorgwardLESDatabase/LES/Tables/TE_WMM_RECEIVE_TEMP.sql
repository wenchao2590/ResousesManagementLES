CREATE TABLE [LES].[TE_WMM_RECEIVE_TEMP] (
    [RECEIVE_NO]             NVARCHAR (50)  NULL,
    [RECEIVE_TYPE]           INT            NULL,
    [PLANT]                  NVARCHAR (100) NULL,
    [WM_NO]                  NVARCHAR (10)  NULL,
    [ZONE_NO]                NVARCHAR (20)  NULL,
    [SUPPLIER_NUM]           NVARCHAR (12)  NULL,
    [SEND_TIME]              DATETIME       NULL,
    [PART_NO]                NVARCHAR (20)  NULL,
    [DELIVERY_LOCATION_NAME] NVARCHAR (30)  NULL,
    [Dock]                   NVARCHAR (20)  NULL,
    [REQUIRED_BOX_NUM]       INT            NULL,
    [REQUIRED_QTY]           INT            NULL,
    [ACTUAL_BOX_NUM]         INT            NULL,
    [ACTUAL_QTY]             INT            NULL,
    [RECEIVE_REASON]         NVARCHAR (200) NULL,
    [COMMENTS]               NVARCHAR (200) NULL,
    [CREATE_DATE]            DATETIME       NULL,
    [CREATE_USER]            NVARCHAR (50)  NULL,
    [ERROR_MSG]              NVARCHAR (200) NULL,
    [VALID_FLAG]             INT            NULL,
    [REQUIRED_BOX_NUM_TEXT]  NVARCHAR (30)  NULL,
    [REQUIRED_QTY_TEXT]      NVARCHAR (30)  NULL,
    [ACTUAL_BOX_NUM_TEXT]    NVARCHAR (30)  NULL,
    [ACTUAL_QTY_TEXT]        NVARCHAR (30)  NULL,
    [Current_BOX_NUM]        INT            NULL,
    [Current_QTY]            INT            NULL,
    [Current_BOX_NUM_TEXT]   NVARCHAR (30)  NULL,
    [Current_QTY_TEXT]       NVARCHAR (30)  NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'Current_QTY_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'Current_BOX_NUM_TEXT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'Current_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'Current_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'REQUIRED_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'Dock';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_发货点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入库类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'RECEIVE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入库单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_RECEIVE_TEMP', @level2type = N'COLUMN', @level2name = N'RECEIVE_NO';

