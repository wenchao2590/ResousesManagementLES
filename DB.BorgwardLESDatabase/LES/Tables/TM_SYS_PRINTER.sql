CREATE TABLE [LES].[TM_SYS_PRINTER] (
    [PRINTER_ID]    INT            IDENTITY (1, 1) NOT NULL,
    [SHEET_TYPE]    INT            NULL,
    [BOX_PARTS]     NVARCHAR (10)  NULL,
    [PLANT]         NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE] NVARCHAR (10)  NULL,
    [ZONE_NO]       NVARCHAR (20)  NULL,
    [WM_NO]         NVARCHAR (10)  NULL,
    [PRINT_ADDRESS] VARCHAR (200)  NOT NULL,
    [PRINTER_NAME]  NVARCHAR (50)  NOT NULL,
    [PRINT_ORDER]   INT            NOT NULL,
    [STATUS]        INT            NULL,
    [COMMENTS]      NVARCHAR (200) NULL,
    [UPDATE_DATE]   DATETIME       NULL,
    [UPDATE_USER]   NVARCHAR (50)  NULL,
    [CREATE_DATE]   DATETIME       NOT NULL,
    [CREATE_USER]   NVARCHAR (50)  NOT NULL,
    CONSTRAINT [IDX_PK_PRINTER_PRINTER_ID] PRIMARY KEY CLUSTERED ([PRINTER_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '状态', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'STATUS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印机序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'PRINT_ORDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印机', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'PRINT_ADDRESS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '单据类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'SHEET_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '打印机流水号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER', @level2type = N'COLUMN', @level2name = N'PRINTER_ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '系统_代码详细', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_SYS_PRINTER';

