CREATE TABLE [LES].[TS_SYS_REPRINT_BARCODE_LOG] (
    [SEQ_ID]               BIGINT         IDENTITY (1, 1) NOT NULL,
    [PLANT]                NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]        NVARCHAR (10)  NULL,
    [WORKSHOP]             NVARCHAR (4)   NULL,
    [PLANT_ZONE]           NVARCHAR (5)   NULL,
    [MODULE]               NVARCHAR (50)  NULL,
    [ITEMNO]               NVARCHAR (50)  NULL,
    [SUPPLIER_NUM]         NVARCHAR (30)  NULL,
    [REPRINTER]            NVARCHAR (20)  NULL,
    [REPRINT_DATE]         DATETIME       NULL,
    [CHECKRESULT]          NVARCHAR (500) NULL,
    [CLS_TYPE]             NVARCHAR (20)  NULL,
    [PLAN_ASN_RUNSHEET_NO] NVARCHAR (30)  NULL,
    [PART_NO]              NVARCHAR (20)  NULL,
    [BARCODE_DATA]         NVARCHAR (50)  NULL,
    [COMMENTS]             NVARCHAR (200) NULL,
    [CREATE_USER]          NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]          DATETIME       NOT NULL,
    [UPDATE_USER]          NVARCHAR (50)  NULL,
    [UPDATE_DATE]          DATETIME       NULL,
    CONSTRAINT [PK_TS_SYS_REPRINT_BARCODE_LOG] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '条码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'BARCODE_DATA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '拉动单流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'PLAN_ASN_RUNSHEET_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '类别', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'CLS_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结果', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'CHECKRESULT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'REPRINT_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'REPRINTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作数据号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'ITEMNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模块', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'MODULE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_供应商补打条码记录表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TS_SYS_REPRINT_BARCODE_LOG';

