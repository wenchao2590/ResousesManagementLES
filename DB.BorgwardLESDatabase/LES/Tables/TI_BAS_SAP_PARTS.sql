CREATE TABLE [LES].[TI_BAS_SAP_PARTS] (
    [SEQ_ID]       INT             IDENTITY (1, 1) NOT NULL,
    [WERKS]        NVARCHAR (5)    NULL,
    [MATNR]        NVARCHAR (20)   NOT NULL,
    [MAKTX_EN]     NVARCHAR (100)  NULL,
    [MAKTX_ZH]     NVARCHAR (100)  NULL,
    [MEINS]        NVARCHAR (20)   NULL,
    [MTART]        NVARCHAR (50)   NULL,
    [BRGEW]        NUMERIC (18, 3) NULL,
    [EKGRP]        NVARCHAR (30)   NULL,
    [ZYSQJC]       NVARCHAR (30)   NULL,
    [BSTRF]        NUMERIC (18, 3) NULL,
    [ZYSBZ]        NUMERIC (18, 2) NULL,
    [ZXBQJC]       NVARCHAR (40)   NULL,
    [ZXBQJQ]       INT             NULL,
    [ZXBBZ]        NUMERIC (18, 2) NULL,
    [DISMM]        NVARCHAR (20)   NULL,
    [DISPO]        NVARCHAR (30)   NULL,
    [DISGR]        NVARCHAR (20)   NULL,
    [BESKZ]        NVARCHAR (100)  NULL,
    [BSTMI]        NUMERIC (18, 3) NULL,
    [PLIFZ]        INT             NULL,
    [ZDOCK]        NVARCHAR (10)   NULL,
    [ZYSFY]        NUMERIC (18, 2) NULL,
    [LGRAD]        NUMERIC (18, 3) NULL,
    [EISBE]        NUMERIC (18, 3) NULL,
    [MINBE]        NUMERIC (18, 3) NULL,
    [MSTAE]        NVARCHAR (1)    NULL,
    [LOGICAL_PK]   NVARCHAR (50)   NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    [KAUSF]        NUMERIC (18, 2) NULL,
    CONSTRAINT [IDX_PK_TI_BAS_SAP_PARTS] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务主关键字', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'LOGICAL_PK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '删除标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'MSTAE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '再订购点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'MINBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '安全库存量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'EISBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MAX库存量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'LGRAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'ZYSFY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_DOCK', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'ZDOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '物流提前期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'PLIFZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订货批量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'BSTMI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供货方式', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'BESKZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MRP组', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'DISGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MRP控制', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'DISPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MRP类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'DISMM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '线边包装费', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'ZXBBZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'ZXBQJQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'ZXBQJC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '运输包装费用', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'ZYSBZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'BSTRF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'ZYSQJC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '福田采购员', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'EKGRP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件重量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'BRGEW';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类别', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'MTART';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'MEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件中文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'MAKTX_ZH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件德文名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'MAKTX_EN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM 零件信息表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_BAS_SAP_PARTS';

