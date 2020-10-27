CREATE TABLE [LES].[TE_WMM_OUTPUT_TEMP] (
    [OUTPUT_ID]        INT            IDENTITY (1, 1) NOT NULL,
    [OUTPUT_NO]        NVARCHAR (50)  NULL,
    [PLANT]            NVARCHAR (100) NULL,
    [SUPPLIER_NUM]     NVARCHAR (12)  NULL,
    [WM_NO]            NVARCHAR (10)  NULL,
    [ZONE_NO]          NVARCHAR (20)  NULL,
    [SEND_TIME]        DATETIME       NULL,
    [OUTPUT_TYPE]      INT            NULL,
    [TRAN_TIME]        DATETIME       NULL,
    [PART_NO]          NVARCHAR (20)  NULL,
    [REQUIRED_BOX_NUM] INT            NULL,
    [REQUIRED_QTY]     INT            NULL,
    [ACTUAL_BOX_NUM]   INT            NULL,
    [ACTUAL_QTY]       INT            NULL,
    [COMMENTS]         NVARCHAR (200) NULL,
    [UPDATE_DATE]      DATETIME       NULL,
    [UPDATE_USER]      NVARCHAR (50)  NULL,
    [CREATE_DATE]      DATETIME       NULL,
    [CREATE_USER]      NVARCHAR (50)  NULL,
    [ERROR_MSG]        NVARCHAR (200) NULL,
    [VALID_FLAG]       INT            NULL,
    [PLANT_ZONE]       NVARCHAR (5)   NULL,
    CONSTRAINT [PK_TE_WMM_OUTPUT_TEMP] PRIMARY KEY CLUSTERED ([OUTPUT_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'ACTUAL_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？实际包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'ACTUAL_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'REQUIRED_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？需求包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'REQUIRED_BOX_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入库时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'TRAN_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？入库类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'OUTPUT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？出库单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'OUTPUT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？出库编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_OUTPUT_TEMP', @level2type = N'COLUMN', @level2name = N'OUTPUT_ID';

