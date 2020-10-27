CREATE TABLE [LES].[TM_BAS_MEASURING_UNIT] (
    [MEASURING_UNIT]      INT            NOT NULL,
    [MEASURING_UNIT_NO]   VARCHAR (8)    NULL,
    [MEASURING_UNIT_NAME] NVARCHAR (100) NOT NULL,
    [COMMENTS]            NVARCHAR (200) NULL,
    [CREATE_USER]         NVARCHAR (50)  NOT NULL,
    [CREATE_DATE]         DATETIME       NOT NULL,
    [UPDATE_USER]         NVARCHAR (50)  NULL,
    [UPDATE_DATE]         DATETIME       NULL,
    CONSTRAINT [IDX_PK_MEASURING_UNIT_measuring_unit] PRIMARY KEY NONCLUSTERED ([MEASURING_UNIT] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT', @level2type = N'COLUMN', @level2name = N'UPDATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_UPDATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT', @level2type = N'COLUMN', @level2name = N'UPDATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_DATE', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT', @level2type = N'COLUMN', @level2name = N'CREATE_DATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_CREATE_USER', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT', @level2type = N'COLUMN', @level2name = N'CREATE_USER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COMMON_备注', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT', @level2type = N'COLUMN', @level2name = N'COMMENTS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计量单位名称', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '计量单位编号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT_NO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '序号', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT', @level2type = N'COLUMN', @level2name = N'MEASURING_UNIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '基础数据_计量单位维护', @level0type = N'SCHEMA', @level0name = N'LES', @level1type = N'TABLE', @level1name = N'TM_BAS_MEASURING_UNIT';

