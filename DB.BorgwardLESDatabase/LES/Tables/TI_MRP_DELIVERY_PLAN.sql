CREATE TABLE [LES].[TI_MRP_DELIVERY_PLAN] (
    [PLAN_SN]       INT            IDENTITY (1, 1) NOT NULL,
    [CLIENT]        NVARCHAR (3)   NOT NULL,
    [SEND_PORT]     NVARCHAR (10)  NOT NULL,
    [IDOC]          NVARCHAR (16)  NOT NULL,
    [SUPPLIER_NUM]  NVARCHAR (12)  NOT NULL,
    [SUPPLIER_NAME] NVARCHAR (100) NULL,
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [CONTRACT_NO]   NVARCHAR (35)  NULL,
    [PLANT_NAME]    NVARCHAR (35)  NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    CONSTRAINT [PK_TI_MRP_DELIVERY_PLAN] PRIMARY KEY CLUSTERED ([CLIENT] ASC, [SEND_PORT] ASC, [IDOC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'PLANT_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)合同编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'CONTRACT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)供应商名称2', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)IDOC号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'IDOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'SEND_PORT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'CLIENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TI_MRP_DELIVERY_PLAN', @level2type = N'COLUMN', @level2name = N'PLAN_SN';

