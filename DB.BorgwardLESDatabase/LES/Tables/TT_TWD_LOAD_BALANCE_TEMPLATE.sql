CREATE TABLE [LES].[TT_TWD_LOAD_BALANCE_TEMPLATE] (
    [TEMPLATE_SN]           INT             IDENTITY (1, 1) NOT NULL,
    [PLANT]                 NVARCHAR (5)    NOT NULL,
    [ASSEMBLY_LINE]         NVARCHAR (10)   NOT NULL,
    [WORKSHOP]              NVARCHAR (4)    NULL,
    [PLANT_ZONE]            NVARCHAR (5)    NULL,
    [SUPPLIER_NUM]          NVARCHAR (12)   NOT NULL,
    [BOX_PARTS]             VARCHAR (20)    NOT NULL,
    [PART_NO]               NVARCHAR (20)   NULL,
    [BEGIN_QTY]             INT             NULL,
    [END_QTY]               INT             NULL,
    [TOP_QTY]               INT             NULL,
    [GROUP_CLS]             NVARCHAR (10)   NULL,
    [ACC_COUNTER]           NUMERIC (18, 2) NULL,
    [IS_TEMPLATE]           INT             NULL,
    [INHOUSE_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [INBOUND_PACKAGE_MODEL] NVARCHAR (30)   NULL,
    [COMMENTS]              NVARCHAR (200)  NULL,
    [UPDATE_DATE]           DATETIME        NULL,
    [UPDATE_USER]           NVARCHAR (50)   NULL,
    [CREATE_DATE]           DATETIME        NOT NULL,
    [CREATE_USER]           NVARCHAR (50)   NOT NULL,
    CONSTRAINT [IDX_PK_LOAD_BALANCE_TEMPLATE_TEMPLATE_SN] PRIMARY KEY CLUSTERED ([TEMPLATE_SN] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '入厂包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'INBOUND_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否是模板', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'IS_TEMPLATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上一次多拉计数器', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'ACC_COUNTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '组编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'GROUP_CLS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '最优数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'TOP_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '结束数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'END_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '起始数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'BEGIN_QTY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '车辆模型_零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'TWD 零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '模板序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE', @level2type = N'COLUMN', @level2name = N'TEMPLATE_SN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '业务_TWD 配载优化模板', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_TWD_LOAD_BALANCE_TEMPLATE';

