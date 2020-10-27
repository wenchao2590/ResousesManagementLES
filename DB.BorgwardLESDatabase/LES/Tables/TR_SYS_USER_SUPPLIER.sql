CREATE TABLE [LES].[TR_SYS_USER_SUPPLIER] (
    [SUPPLIER_NUM] NVARCHAR (12) NOT NULL,
    [USER_ID]      INT           NOT NULL,
    [CREATE_USER]  NVARCHAR (50) NOT NULL,
    [CREATE_DATE]  DATETIME      NOT NULL,
    CONSTRAINT [IDX_PK_USER_SUPPLIER_USER_ID] PRIMARY KEY CLUSTERED ([SUPPLIER_NUM] ASC, [USER_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建日期', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '创建用户', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_SUPPLIER', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '用户ID', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_SUPPLIER', @level2type = N'COLUMN', @level2name = N'USER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '供应商号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_SUPPLIER', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_用户与供应商关系表', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_SYS_USER_SUPPLIER';

