CREATE TABLE [LES].[TM_BAS_INTERNAL_ORDER] (
    [INTERNAL_ID]   INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NULL,
    [PLANT_ZONE]    NVARCHAR (200) NULL,
    [WORKSHOP]      NVARCHAR (4)   NULL,
    [INTERNAL_CODE] NVARCHAR (100) NOT NULL,
    [INTERNAL_NAME] NVARCHAR (200) NOT NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    CONSTRAINT [PK_TM_BAS_INTERNAL_ORDER] PRIMARY KEY CLUSTERED ([INTERNAL_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '内部订单名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER', @level2type = N'COLUMN', @level2name = N'INTERNAL_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '内部订单编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER', @level2type = N'COLUMN', @level2name = N'INTERNAL_CODE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '内部订单序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER', @level2type = N'COLUMN', @level2name = N'INTERNAL_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_内部订单', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_INTERNAL_ORDER';

