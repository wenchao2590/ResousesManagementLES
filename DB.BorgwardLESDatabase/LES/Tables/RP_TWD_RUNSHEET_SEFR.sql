CREATE TABLE [LES].[RP_TWD_RUNSHEET_SEFR] (
    [SEFRID]            INT            IDENTITY (1, 1) NOT NULL,
    [PLANT]             NVARCHAR (5)   NOT NULL,
    [ASSEMBLY_LINE]     NVARCHAR (10)  NOT NULL,
    [DELIVERY_LOCATION] NVARCHAR (50)  NULL,
    [PLANT_ZONE]        NVARCHAR (10)  NULL,
    [WORKSHOP]          NVARCHAR (4)   NULL,
    [TWD_RUNSHEET_NO]   VARCHAR (22)   NOT NULL,
    [SHEET_STATUS]      INT            NULL,
    [RUNSHEET_TYPE]     INT            NOT NULL,
    [PART_NO]           NVARCHAR (20)  NOT NULL,
    [PART_CNAME]        NVARCHAR (100) NULL,
    [SUPPLIER_NUM]      NVARCHAR (12)  NOT NULL,
    [SUPPLIER_NAME]     NVARCHAR (100) NULL,
    [BOX_PARTS]         NVARCHAR (10)  NOT NULL,
    [REQUIRED_NUMBER]   INT            NULL,
    [ACTUAL_NUMBER]     INT            NULL,
    [SEFR]              VARCHAR (10)   NULL,
    [BYYEAR]            INT            NULL,
    [BYMONTH]           INT            NULL,
    [COMMENTS]          NVARCHAR (200) NULL,
    [UPDATE_DATE]       DATETIME       NULL,
    [UPDATE_USER]       NVARCHAR (50)  NULL,
    [CREATE_DATE]       DATETIME       NULL,
    [CREATE_USER]       NVARCHAR (50)  NULL,
    CONSTRAINT [IDX_RP_TWD_RUNSHEET_SEFR_SEFRID] PRIMARY KEY CLUSTERED ([SEFRID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'满足率', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'RP_TWD_RUNSHEET_SEFR', @level2type = N'COLUMN', @level2name = N'SEFR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'实际数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'RP_TWD_RUNSHEET_SEFR', @level2type = N'COLUMN', @level2name = N'ACTUAL_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'计划数量', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'RP_TWD_RUNSHEET_SEFR', @level2type = N'COLUMN', @level2name = N'REQUIRED_NUMBER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件类', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'RP_TWD_RUNSHEET_SEFR', @level2type = N'COLUMN', @level2name = N'BOX_PARTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'供应商名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'RP_TWD_RUNSHEET_SEFR', @level2type = N'COLUMN', @level2name = N'SUPPLIER_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'零件名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'RP_TWD_RUNSHEET_SEFR', @level2type = N'COLUMN', @level2name = N'PART_CNAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'拉动仓库 ', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'RP_TWD_RUNSHEET_SEFR', @level2type = N'COLUMN', @level2name = N'DELIVERY_LOCATION';

