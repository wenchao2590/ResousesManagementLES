CREATE TABLE [LES].[TT_BAS_PULL_ORDERS] (
    [SEQID]              INT             IDENTITY (1, 1) NOT NULL,
    [ORDER_NO]           NVARCHAR (20)   NOT NULL,
    [WERK]               NVARCHAR (4)    NOT NULL,
    [SPJ]                NVARCHAR (8)    NOT NULL,
    [KNR]                NVARCHAR (8)    NOT NULL,
    [MODEL_YEAR]         NVARCHAR (30)   NULL,
    [MODEL]              NVARCHAR (30)   NOT NULL,
    [FARBAU]             NVARCHAR (30)   NOT NULL,
    [FARBIN]             NVARCHAR (30)   NOT NULL,
    [PNR_STRING]         NVARCHAR (200)  NOT NULL,
    [PNR_STRING_COMPUTE] NVARCHAR (200)  NOT NULL,
    [VEHICLE_ORDER]      NVARCHAR (8)    NOT NULL,
    [ORDER_DATE]         DATETIME        NOT NULL,
    [DEAL_FLAG]          INT             NOT NULL,
    [STATUS_FLAG]        NVARCHAR (8)    NOT NULL,
    [VORSERIE]           BIT             NOT NULL,
    [SIGNATURE]          NVARCHAR (256)  NOT NULL,
    [ORDER_FILE_NAME]    NVARCHAR (256)  NULL,
    [ORDER_TYPE]         VARCHAR (2)     NULL,
    [RECALCULATE_FLAG]   INT             NULL,
    [CHANGE_FLAG]        INT             NULL,
    [ASSEMBLY_LINE]      NVARCHAR (10)   NULL,
    [PROCESS_LINE_SN]    INT             NULL,
    [INIT_STSTUS]        INT             NULL,
    [VIN]                NVARCHAR (30)   NULL,
    [PART_NO]            NVARCHAR (18)   NULL,
    [QTY]                NUMERIC (18, 2) NULL,
    [MEASURING_UNIT]     NVARCHAR (8)    NULL,
    [PLAN_FLAG]          NVARCHAR (3)    NULL,
    [COMMENTS]           NVARCHAR (200)  NULL,
    [CREATE_USER]        NVARCHAR (50)   NULL,
    [CREATE_DATE]        DATETIME        NULL,
    [UPDATE_USER]        NVARCHAR (50)   NULL,
    [UPDATE_DATE]        DATETIME        NULL,
    [ZCOLORI]            NVARCHAR (30)   NULL,
    [ZCOLORI_D]          NVARCHAR (30)   NULL,
    CONSTRAINT [IDX_PK_ORDERS_SEQID] PRIMARY KEY CLUSTERED ([SEQID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_TT_BAS_PULL_ORDERS]
    ON [LES].[TT_BAS_PULL_ORDERS]([WERK] ASC, [ORDER_NO] ASC, [VIN] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VIN', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'VIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'A00订单分解状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'INIT_STSTUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工艺路线编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'PROCESS_LINE_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '变更标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'CHANGE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '重算标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'RECALCULATE_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'ORDER_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单文件名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'ORDER_FILE_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单签名', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'SIGNATURE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '接口_VORSERIE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'VORSERIE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'STATUS_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '处理标志', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'ORDER_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆顺序', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'VEHICLE_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '选项包代码描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'PNR_STRING_COMPUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '选项包代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'PNR_STRING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '特征包代码描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'FARBIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '特征包代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'FARBAU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车型颜色代码描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车型颜色代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'MODEL_YEAR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '接口_车辆识别号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'KNR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '接口_SPJ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'SPJ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '接口_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'WERK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'ORDER_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'seqid', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS', @level2type = N'COLUMN', @level2name = N'SEQID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_车辆订单表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_BAS_PULL_ORDERS';

