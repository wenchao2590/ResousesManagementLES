CREATE TABLE [LES].[TE_WMM_REPACKAGE_TEMP] (
    [REPACKAGE_TEMP_ID]      INT             IDENTITY (1, 1) NOT NULL,
    [REPACKAGE_NO]           NVARCHAR (50)   NULL,
    [REPACKAGE_TIME]         DATETIME        NULL,
    [REPACKAGE_BTIME]        DATETIME        NULL,
    [REPACKAGE_ETIME]        DATETIME        NULL,
    [REPACKAGE_PICKUP_TIME]  DATETIME        NULL,
    [BOOK_KEEPER]            NVARCHAR (50)   NULL,
    [OUTPUT_TYPE]            INT             NULL,
    [PLANT]                  NVARCHAR (50)   NULL,
    [WM_NO]                  NVARCHAR (10)   NULL,
    [ZONE_NO]                NVARCHAR (20)   NULL,
    [REPACKAGE_ROUTE]        NVARCHAR (30)   NULL,
    [PART_NO]                NVARCHAR (30)   NULL,
    [INHOUSE_PACKAGE_MODEL]  NVARCHAR (30)   NULL,
    [INHOUSE_PACKAGE]        INT             NULL,
    [NUM]                    NUMERIC (18, 2) NULL,
    [UPDATE_DATE]            DATETIME        NULL,
    [UPDATE_USER]            NVARCHAR (50)   NULL,
    [CREATE_DATE]            DATETIME        NULL,
    [CREATE_USER]            NVARCHAR (50)   NULL,
    [ERROR_MSG]              NVARCHAR (200)  NULL,
    [VALID_FLAG]             INT             NULL,
    [REPACKAGE_PICKUP_ETIME] DATETIME        NULL,
    CONSTRAINT [PK_TE_WMM_REPACKAGE_TEMP] PRIMARY KEY CLUSTERED ([REPACKAGE_TEMP_ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)翻包结束时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_PICKUP_ETIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)有效标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'VALID_FLAG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)错误信息', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'ERROR_MSG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)数量（件数）', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'NUM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)上线包装型号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'INHOUSE_PACKAGE_MODEL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'PART_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)翻包路径', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ROUTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)存贮区编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)入库类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'OUTPUT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)翻包人', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'BOOK_KEEPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)计划翻包捡选时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_PICKUP_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)计划翻包结束时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_ETIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)计划翻包起始时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_BTIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)计划翻包时间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_TIME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)翻包通知单号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '(备注可能为)未找到', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TE_WMM_REPACKAGE_TEMP', @level2type = N'COLUMN', @level2name = N'REPACKAGE_TEMP_ID';

