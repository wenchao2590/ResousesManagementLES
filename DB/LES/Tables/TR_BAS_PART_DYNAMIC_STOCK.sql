CREATE TABLE [LES].[TR_BAS_PART_DYNAMIC_STOCK] (
    [ID]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [PART_NO]     NVARCHAR (32) NULL,
    [DLOC]        NVARCHAR (32) NULL,
    [ZONE_NO]     NVARCHAR (32) NULL,
    [WM_NO]       NVARCHAR (16) NULL,
    [PLANT]       NVARCHAR (8)  NULL,
    [VALID_FLAG]  BIT           NULL,
    [CREATE_USER] NVARCHAR (32) NULL,
    [CREATE_DATE] DATETIME      NULL,
    [MODIFY_USER] NVARCHAR (32) NULL,
    [MODIFY_DATE] DATETIME      NULL,
    CONSTRAINT [PK_TR_BAS_PART_DYNAMIC_STOCK] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'工厂代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_DYNAMIC_STOCK', @level2type = N'COLUMN', @level2name = N'PLANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'仓库代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_DYNAMIC_STOCK', @level2type = N'COLUMN', @level2name = N'WM_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'存储区代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_DYNAMIC_STOCK', @level2type = N'COLUMN', @level2name = N'ZONE_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'库位代码', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_DYNAMIC_STOCK', @level2type = N'COLUMN', @level2name = N'DLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TR_BAS_PART_DYNAMIC_STOCK', @level2type = N'COLUMN', @level2name = N'PART_NO';

