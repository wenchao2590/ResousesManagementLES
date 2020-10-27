CREATE TABLE [LES].[TE_TWD_SUPPLIER_SENDTIME] (
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [PLANT]         NVARCHAR (5)  NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10) NOT NULL,
    [SUPPLIER_NUM]  NVARCHAR (12) NOT NULL,
    [BOX_PARTS]     NVARCHAR (10) NOT NULL,
    [SEND_TIME]     DATETIME      NOT NULL,
    [WINDOW_TIME]   DATETIME      NOT NULL,
    CONSTRAINT [PK_TE_TWD_SUPPLIER_SENDTIME] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？窗口时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'WINDOW_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？发送时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'SEND_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？基础数据_供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？DCP扫描点名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '？ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_TWD_SUPPLIER_SENDTIME', @level2type = N'COLUMN', @level2name = N'ID';

