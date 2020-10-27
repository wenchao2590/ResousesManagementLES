CREATE TABLE [LES].[TM_WMM_TRANTYPE] (
    [TRAN_NO]             NVARCHAR (10)  NOT NULL,
    [TRAN_NAME]           NVARCHAR (200) NULL,
    [PLANT]               NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]       NVARCHAR (10)  NULL,
    [PLANT_ZONE]          NVARCHAR (5)   NULL,
    [WORKSHOP]            NVARCHAR (4)   NULL,
    [WM_NO]               NVARCHAR (10)  NULL,
    [WM_TRAN]             NVARCHAR (20)  NULL,
    [IM_TRAN]             NVARCHAR (20)  NULL,
    [TARGET_STORAGE_TYPE] NVARCHAR (100) NULL,
    [TARGET_DLOC]         NVARCHAR (30)  NULL,
    [SOURCE_STORAGE_TYPE] NVARCHAR (100) NULL,
    [SOURCE_DLOC]         NVARCHAR (30)  NULL,
    [SEARCH_STRATEGY]     NVARCHAR (100) NULL,
    [REDUCE_STOCK]        INT            NULL,
    [ADDS_STOCK]          INT            NULL,
    [REQIUREMENT_TYPE]    NVARCHAR (30)  NULL,
    [COMMENTS]            NVARCHAR (200) NULL,
    [CREATE_USER]         NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]         DATETIME       NOT NULL,
    [UPDATE_USER]         NVARCHAR (50)  NULL,
    [UPDATE_DATE]         DATETIME       NULL,
    CONSTRAINT [PK_TM_WMM_TRANTYPE] PRIMARY KEY CLUSTERED ([TRAN_NO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '需求类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'REQIUREMENT_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否增加目地', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'ADDS_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '是否扣源', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'REDUCE_STOCK';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '上下架标识', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'SEARCH_STRATEGY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '源库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'SOURCE_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '源存储类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'SOURCE_STORAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目标库位', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'TARGET_DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '目标存储类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'TARGET_STORAGE_TYPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'IM移动类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'IM_TRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'WM移动类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'WM_TRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '仓库编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_车间', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'WORKSHOP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_厂区', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'PLANT_ZONE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_流水线', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'ASSEMBLY_LINE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '工厂模型_工厂', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '移动类型名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'TRAN_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '移动类型编码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE', @level2type = N'COLUMN', @level2name = N'TRAN_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_移动类型', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_WMM_TRANTYPE';

