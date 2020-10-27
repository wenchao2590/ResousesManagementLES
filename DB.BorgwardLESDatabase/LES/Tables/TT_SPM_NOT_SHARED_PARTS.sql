CREATE TABLE [LES].[TT_SPM_NOT_SHARED_PARTS] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]              NVARCHAR (8)   NULL,
    [SUPPLIER_NUM]       NVARCHAR (12)  NULL,
    [SUPPLIER_NAME]      NVARCHAR (100) NULL,
    [PART_NO]            NVARCHAR (20)  NULL,
    [PART_NAME]          NVARCHAR (100) NULL,
    [MIN_ORDER_QUANTITY] INT            NULL,
    [MIN_PACKING_NUM]    INT            NULL,
    [DELIVERY_CYCLE]     INT            NULL,
    [COMMENTS]           NVARCHAR (200) NULL,
    [CREATE_USER]        NVARCHAR (50)  NULL,
    [CREATE_DATE]        DATETIME       NULL,
    [UPDATE_USER]        NVARCHAR (50)  NULL,
    [UPDATE_DATE]        DATETIME       NULL,
    CONSTRAINT [PK_LES.TT_SPS_NOT_SHARED_PARTS] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'修改人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'创建人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供货周期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'DELIVERY_CYCLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小包装数', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'MIN_PACKING_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'最小起订量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'MIN_ORDER_QUANTITY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'PART_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TT_SPM_NOT_SHARED_PARTS', @level2type = N'COLUMN', @level2name = N'PLANT';

