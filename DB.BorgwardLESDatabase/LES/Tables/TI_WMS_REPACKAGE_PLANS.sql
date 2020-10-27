CREATE TABLE [LES].[TI_WMS_REPACKAGE_PLANS] (
    [SEQ_ID]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [WERKS]        NVARCHAR (4)    NULL,
    [LGORT]        NVARCHAR (4)    NULL,
    [LGPBE]        NVARCHAR (14)   NULL,
    [FBTYPE]       NVARCHAR (1)    NULL,
    [LGMNG]        NUMERIC (18, 3) NULL,
    [BUDAT]        NVARCHAR (8)    NULL,
    [CPUTM]        NVARCHAR (6)    NULL,
    [MATNR]        NVARCHAR (18)   NULL,
    [MAKTX_ZH]     NVARCHAR (40)   NULL,
    [R_MENGE]      NUMERIC (18, 3) NULL,
    [ZXBQJC]       NVARCHAR (40)   NULL,
    [ZXBQJQ]       NVARCHAR (6)    NULL,
    [ZYSQJC]       NVARCHAR (20)   NULL,
    [BSTRF]        NUMERIC (18, 3) NULL,
    [L_MENGE]      NUMERIC (18, 3) NULL,
    [DEAL_FLAG]    INT             NULL,
    [PROCESS_FLAG] INT             NULL,
    [PROCESS_TIME] DATETIME        NULL,
    [COMMENTS]     NVARCHAR (200)  NULL,
    [CREATE_USER]  NVARCHAR (50)   NOT NULL,
    [UPDATE_DATE]  DATETIME        NULL,
    [UPDATE_USER]  NVARCHAR (50)   NULL,
    [CREATE_DATE]  DATETIME        NOT NULL,
    [ID]           NVARCHAR (18)   NULL,
    [TIMS]         NVARCHAR (6)    NULL,
    [ZFBLJ]        NVARCHAR (30)   NULL,
    CONSTRAINT [PK_TI_WMS_REPACKAGE_PLANS_SEQ_ID] PRIMARY KEY CLUSTERED ([SEQ_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'PROCESS_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'PROCESS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包本地剩余数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'L_MENGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'BSTRF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'ZYSQJC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'ZXBQJQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'ZXBQJC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'R_MENGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'MAKTX_ZH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'MATNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '发布日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'CPUTM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划翻包日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'BUDAT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'LGMNG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '翻包类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'FBTYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'LGPBE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '库存地点', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'LGORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'WERKS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS', @level2type = N'COLUMN', @level2name = N'SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_MM_翻包计划表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_WMS_REPACKAGE_PLANS';

