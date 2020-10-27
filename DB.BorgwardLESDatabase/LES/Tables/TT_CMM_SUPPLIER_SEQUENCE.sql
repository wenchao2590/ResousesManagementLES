CREATE TABLE [LES].[TT_CMM_SUPPLIER_SEQUENCE] (
    [SEQ_ID]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)  NOT NULL,
    [PLANT_ZONE]            NVARCHAR (5)   NULL,
    [WORKSHOP]              NVARCHAR (4)   NULL,
    [PLAN_RUNSHEET_NO]      VARCHAR (30)   NOT NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)  NOT NULL,
    [EXPECTED_ARRIVAL_TIME] DATETIME       NULL,
    [LINE_SN]               INT            NULL,
    [FIRST_LINE_SN]         INT            NULL,
    [CAR_NO]                NVARCHAR (20)  NULL,
    [LEAVE_BUFFER_TIME]     DATETIME       NULL,
    [DRIVER_NAME]           NVARCHAR (20)  NULL,
    [STATUS]                INT            NULL,
    [TRANS_SUPPLIER_NUM]    NVARCHAR (20)  NULL,
    [ORIGINAL_DOCK]         NVARCHAR (10)  NULL,
    [NEW_DOCK]              NVARCHAR (10)  NULL,
    [WM_NO]                 NVARCHAR (10)  NULL,
    [RUNSHEET_TYPE]         INT            NULL,
    [COMMENTS]              NVARCHAR (200) NULL,
    [CREATE_USER]           NVARCHAR (50)  NULL,
    [CREATE_DATE]           DATETIME       NULL,
    [UPDATE_USER]           NVARCHAR (50)  NULL,
    [UPDATE_DATE]           DATETIME       NULL,
    CONSTRAINT [PK_TT_CMM_SUPPLIER_SEQUENCE] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'RUNSHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '收货仓库', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '新分配DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'NEW_DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '原始DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'ORIGINAL_DOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'TRANS_SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '司机', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'DRIVER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '出缓存区时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'LEAVE_BUFFER_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车牌号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'CAR_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '优先排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'FIRST_LINE_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '排序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'LINE_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '期望到达时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'EXPECTED_ARRIVAL_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '送货单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'PLAN_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商排序表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_CMM_SUPPLIER_SEQUENCE';

