CREATE TABLE [LES].[TI_ODS_ORDER_BOM_IN] (
    [SUB_SEQ_ID]  BIGINT          IDENTITY (1, 1) NOT NULL,
    [ZORDNO]      NVARCHAR (12)   NULL,
    [ZKWERK]      NVARCHAR (4)    NULL,
    [ZBOMID]      NVARCHAR (10)   NULL,
    [ZCOMNO]      NVARCHAR (18)   NULL,
    [ZCOMDS]      NVARCHAR (40)   NULL,
    [ZVIN]        NVARCHAR (17)   NULL,
    [ZQTY]        NUMERIC (18, 2) NULL,
    [ZDATE]       NVARCHAR (8)    NULL,
    [ZLOC]        NVARCHAR (20)   NULL,
    [ZST]         NVARCHAR (1)    NULL,
    [ZMEMO]       NVARCHAR (80)   NULL,
    [ZMEINS]      NVARCHAR (8)    NULL,
    [DEAL_FLAG]   INT             NULL,
    [COMMENTS]    NVARCHAR (200)  NULL,
    [UPDATE_DATE] DATETIME        NULL,
    [UPDATE_USER] NVARCHAR (50)   NULL,
    [CREATE_DATE] DATETIME        NOT NULL,
    [CREATE_USER] NVARCHAR (50)   NOT NULL,
    [BATCHNO]     NVARCHAR (50)   NULL,
    CONSTRAINT [PK_TI_ODS_ORDER_BOM_IN] PRIMARY KEY CLUSTERED ([SUB_SEQ_ID] ASC)
);


GO
CREATE NONCLUSTERED INDEX [NONCLUSTERED_ZORDNO_BATCHNO]
    ON [LES].[TI_ODS_ORDER_BOM_IN]([ZORDNO] ASC, [BATCHNO] ASC)
    INCLUDE([ZKWERK], [ZBOMID], [ZCOMNO], [ZCOMDS], [ZQTY], [ZDATE], [ZLOC], [ZST], [ZMEMO], [ZMEINS], [DEAL_FLAG]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'DEAL_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZMEINS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZMEMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '操作状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计划下线日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZDATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZQTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'VIN号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZVIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件描述', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZCOMDS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZCOMNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MBOM项目号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZBOMID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZKWERK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '订单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'ZORDNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN', @level2type = N'COLUMN', @level2name = N'SUB_SEQ_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'SAP接口_APO_整车订单BOM明细表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_ODS_ORDER_BOM_IN';

